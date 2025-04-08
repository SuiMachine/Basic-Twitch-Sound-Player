using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.TwitchEventSub;
using BasicTwitchSoundPlayer.TwitchEventSub.ChannelPoint;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BasicTwitchSoundPlayer
{
	public class TwitchSocket
	{
		public const string WEBSOCKET_URI = "wss://eventsub.wss.twitch.tv/ws?keepalive_timeout_seconds=30";
		private IRCBot iRCBot;
		private Task SubscribingTask;
		private volatile bool m_Connected;
		private volatile bool m_Connecting;

		public string SessionID { get; private set; }
		public bool Connected => m_Connected;
		public volatile bool AutoReconnect;
		public DateTime LastMessageAt { get; private set; }
		public WebSocket Socket { get; private set; }

		public Action<KrakenConnections.ChannelPointRedeemRequest> OnChannelPointsRedeem;
		private System.Timers.Timer KeepAliveCheck;

		public async Task CreateSessionAndSocket()
		{
			m_Connecting = true;
			while (!iRCBot.BotRunning || iRCBot.Irc == null || !iRCBot.Irc.ConnectedStatus)
				await Task.Delay(2500);
			var rewards = await iRCBot.Irc.KrakenConnection.GetRewardsList();

			Socket = new WebSocket(WEBSOCKET_URI);
			Socket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
			Socket.OnMessage += Socket_OnMessage;
			Socket.OnOpen += Socket_OnOpen;
			Socket.OnClose += Socket_OnClose;
			Socket.EmitOnPing = true;
			Socket.ConnectAsync();
		}

		private void Socket_OnOpen(object sender, EventArgs e)
		{
			m_Connected = true;
			Debug.WriteLine("Opened Twitch socket");
			KeepAliveCheck = new System.Timers.Timer(30 * 1000);
			KeepAliveCheck.Elapsed += KeepAliveCheck_Elapsed;
			KeepAliveCheck.Start();
		}

		private void KeepAliveCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			var currentTime = DateTime.UtcNow;
			if (LastMessageAt + TimeSpan.FromSeconds(31) < currentTime)
			{
				Debug.WriteLine("Should reconnect?");
			}
		}

		private void Socket_OnClose(object sender, CloseEventArgs e)
		{
			EventSubClose_Code closeType = (EventSubClose_Code)e.Code;
			m_Connected = false;
			KeepAliveCheck?.Stop();

			if(AutoReconnect)
			{
				//THIS IS NOT SAFE!
				Task.Factory.StartNew(async () =>
				{
					await CreateSessionAndSocket();
				});
			}
		}

		private void Socket_OnMessage(object sender, MessageEventArgs e)
		{
			var message = JsonConvert.DeserializeObject<EventSub_Message>(e.Data);
			if (message == null)
			{
				Socket.Ping();
				return;
			}

			LastMessageAt = message.metadata.message_timestamp;
			switch (message.metadata.message_type)
			{
				case EventSub_MessageType.session_welcome:
					ProcessWelcome(message.payload);
					break;
				case EventSub_MessageType.session_keepalive:
					break;
				case EventSub_MessageType.notification:
					ProcessNotification(message);
					break;
				default:
					Debug.WriteLine($"Unhandled message: {message}");
					break;
			}
		}

		private void ProcessNotification(EventSub_Message message)
		{
			switch (message.metadata.subscription_type)
			{
				case null:
					return;
				case "channel.channel_points_custom_reward_redemption.add":
					ProcessChannelRedeem(message.payload);
					break;

			}
		}

		private void ProcessChannelRedeem(JToken payload)
		{
			if (payload["event"] == null)
				return;
			ChannelPointEvent obj = payload["event"].ToObject<ChannelPointEvent>();
			var passedData = new KrakenConnections.ChannelPointRedeemRequest(obj.user_name, obj.user_id, obj.reward.id, obj.id, obj.status, obj.user_input);
			OnChannelPointsRedeem?.Invoke(passedData);
		}

		private void ProcessWelcome(JToken payload)
		{
			var content = payload["session"].ToObject<EventSub_Session>();
			SessionID = content.id;
			AutoReconnect = true;

			while (MainForm.Instance.TwitchBot.Irc.KrakenConnection.BroadcasterID == null)
				Task.Delay(100);

			if (!Connected)
			{
				//Do reconnect
				if (AutoReconnect && !m_Connecting)
				{
					Task.Factory.StartNew(async () =>
					{
						await CreateSessionAndSocket();
					});
				}
				return;
			}

			//THIS IS NOT SAFE!
			Task.Factory.StartNew(async () =>
			{
				await MainForm.Instance.TwitchBot.Irc.KrakenConnection.EventSub_SubscribeToChannelPoints(MainForm.Instance.TwitchBot.Irc.KrakenConnection.BroadcasterID, SessionID);
			});
		}

		public void SetIrcReference(IRCBot iRCBot)
		{
			this.iRCBot = iRCBot;

			SubscribingTask = Task.Run(CreateSessionAndSocket);
		}

		public void UpdateRedemptionStatus(KrakenConnections.ChannelPointRedeemRequest redeem, KrakenConnections.RedemptionStates status)
		{
			if (redeem.state != KrakenConnections.RedemptionStates.UNFULFILLED)
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Can't change the state of already accepted/rejected redeem - this needs to be fixed!", LineType.IrcCommand);
				return;
			}

			if (status == KrakenConnections.RedemptionStates.UNFULFILLED)
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Can't set state to UNFULFILLED - this needs to be fixed!", LineType.IrcCommand);
				return;
			}

			redeem.state = status;
			iRCBot.Irc.KrakenConnection.UpdateRedemptionStatus(redeem.rewardId, new string[]
			{
				redeem.redemptionId,
			}, status);
		}
	}
}
