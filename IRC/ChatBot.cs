using BasicTwitchSoundPlayer.Structs;
using SuiBot_Core;
using SuiBot_TwitchSocket;
using SuiBot_TwitchSocket.API;
using SuiBot_TwitchSocket.API.EventSub;
using SuiBot_TwitchSocket.API.EventSub.Subscription.Responses;
using SuiBot_TwitchSocket.Interfaces;
using System;
using System.Threading.Tasks;
using static SuiBot_TwitchSocket.API.EventSub.ES_ChannelPoints;

namespace BasicTwitchSoundPlayer.IRC
{
	public class ChatBot : IBotInstance, IDisposable
	{
		public const string BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID = "9z58zy6ak0ejk9lme6dy6nyugydaes";

		public bool BotRunning;
		public ChannelInstance ChannelInstance;
		internal TwitchSocket TwitchSocket { get; private set; }
		internal HelixAPI HelixAPI_User { get; private set; }
		private HelixAPI m_HelixAPI_Bot;
		internal HelixAPI HelixAPI_Bot => m_HelixAPI_Bot ?? HelixAPI_User; //Use user if bot is null

		public bool ShouldRun { get; set; }
		public bool IsDisposed { get; private set; }

		private MainForm m_Parent;
		private string m_ChannelToJoin;
		private char m_PrefixChar;
		public System.Timers.Timer StatusUpdateTimer;

		private SoundDB SndDB { get; set; }

		public ChatBot(SoundDB soundDb, char PrefixChar)
		{
			var privateSettings = PrivateSettings.GetInstance();

			ChannelInstance = new ChannelInstance(this);
			HelixAPI_User = new HelixAPI(BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, this, privateSettings.UserAuth);

			if (privateSettings.BotAuth != "")
				m_HelixAPI_Bot = new HelixAPI(BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, this, privateSettings.BotAuth);

			var authVerify = HelixAPI_User.GetValidation();
			if (authVerify == null || string.IsNullOrEmpty(authVerify.login))
				throw new Exception("Failed to validate user token - a new one might need to be generated?");
			m_ChannelToJoin = authVerify.login;
			ChannelInstance.Channel = m_ChannelToJoin;
			ChannelInstance.ChannelID = authVerify.user_id;

			if (m_HelixAPI_Bot != null)
			{
				var validationResult = m_HelixAPI_Bot.ValidateToken();
				if (validationResult != HelixAPI.ValidationResult.Successful)
					throw new Exception($"Failed to validate bot token. Validation status was {validationResult}");
			}

			m_Parent = MainForm.Instance;
			this.StatusUpdateTimer = new System.Timers.Timer(5 * 1000 * 60) { AutoReset = true };
			this.StatusUpdateTimer.Elapsed += StatusUpdateTimer_Elapsed;
			this.m_PrefixChar = PrefixChar;
			SndDB = soundDb;
			SndDB.Register();
		}

		private void StatusUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			ChannelInstance.UpdateTwitchStatus();
		}

		internal void Connect()
		{
			VoiceModHandling.GetInstance().ConnectToVoiceMod();

			ShouldRun = true;
			TwitchSocket = new TwitchSocket(this);
		}

		public void StopBot()
		{
			SndDB.Close();
			PrivateSettings.GetInstance().SaveSettings();
			ChannelInstance.SaveIgnoredList();

			System.Threading.Thread.Sleep(200);
		}

		internal void UpdateVolume() => SndDB.ChangeVolume(PrivateSettings.GetInstance().Volume);

		#region EventHandlers
		public bool GetChannelInstanceUsingLogin(string login, out IChannelInstance channel)
		{
			if (login == ChannelInstance.Channel)
			{
				channel = ChannelInstance;
				return true;
			}
			else
			{
				channel = null;
				return false;
			}
		}

		public void TwitchSocket_Connected()
		{
			Logger.AddLine("Connected!");

			Task.Factory.StartNew(async () =>
			{
				Response_SubscribeTo.Subscription_Response_Data result = await HelixAPI_User.SubscribeToChatMessageUsingID(HelixAPI_User.BotUserId, TwitchSocket.SessionID);
				await Task.Delay(2000);

				Response_SubscribeTo currentSubscriptionChecks = await HelixAPI_User.GetCurrentSubscriptions();
				foreach (var subscription in currentSubscriptionChecks.data)
				{
					if (subscription.status != "enabled" || subscription.transport.session_id != TwitchSocket.SessionID)
					{
						m_Parent?.ThreadSafeAddPreviewText($"Unsubscribing from {subscription.type} ({subscription.status})", LineType.TwitchSocketCommand);
						Logger.AddLine($"Unsubscribing from {subscription.type} ({subscription.status})");
						await HelixAPI_User.CloseSubscription(subscription);
						await Task.Delay(100);
					}
				}

				Logger.AddLine($"Subscribing to additional events for {result.condition.broadcaster_user_id}");
				var onLineSub = await HelixAPI_User.SubscribeToOnlineStatus(result.condition.broadcaster_user_id, TwitchSocket.SessionID);
				await Task.Delay(2000);
				var offlineSub = await HelixAPI_User.SubscribeToOfflineStatus(result.condition.broadcaster_user_id, TwitchSocket.SessionID);
				await Task.Delay(2000);
				var subscribe = await HelixAPI_User.SubscribeToChannelRedeem(result.condition.broadcaster_user_id, TwitchSocket.SessionID);
				await Task.Delay(2000);
				m_Parent?.ThreadSafeAddPreviewText("Registered to events", LineType.TwitchSocketCommand);
				Logger.AddLine($"Done!");
			});

			StatusUpdateTimer.Start();
		}

		public void TwitchSocket_Disconnected()
		{
			m_Parent.ThreadSafeAddPreviewText("Socket disconnected", LineType.TwitchSocketCommand);

			StatusUpdateTimer.Stop();
		}

		public void TwitchSocket_ClosedViaSocket()
		{
			m_Parent.ThreadSafeAddPreviewText("Connection closed via socket", LineType.TwitchSocketCommand);

		}

		public void TwitchSocket_ChatMessage(ES_ChatMessage chatMessage)
		{
			if (!chatMessage.message.text.StartsWith(m_PrefixChar.ToString()) || ChannelInstance.IgnoreList.Contains(chatMessage.chatter_user_login))
			{
				//literally nothing else happens in your code if this is false
				m_Parent.ThreadSafeAddPreviewText($"{chatMessage.broadcaster_user_name}: {chatMessage.message.text}", LineType.Generic);
				return;
			}
			else
			{
				string text = chatMessage.message.text.Remove(0, 1).ToLower();
				var role = TwitchRightsEnum.Public;

				//Mod Commands
				if (role >= TwitchRightsEnum.Mod || chatMessage.UserRole <= ES_ChatMessage.Role.Mod)
				{
					if (text == "stopallsounds")
					{
						m_Parent.ThreadSafeAddPreviewText($"{chatMessage.chatter_user_name}: {chatMessage.message.text}", LineType.ModCommand);
						SndDB.StopAllSounds();
						return;
					}

					if (text.StartsWith("cooldown "))
					{
						var split = text.Split(' ');
						text = split[split.Length - 1];
						if (int.TryParse(text, out int delayValue))
						{
							if (delayValue < 0)
								delayValue = 0;
							this.SndDB.SetDelay(delayValue);
						}

						m_Parent.ThreadSafeAddPreviewText($"{chatMessage.broadcaster_user_name}: {chatMessage.message.text}", LineType.ModCommand);
						return;
					}
				}
			}
		}

		public void TwitchSocket_StreamWentOnline(ES_StreamOnline onlineData)
		{
			m_Parent.ThreadSafeAddPreviewText("Streamer went online", LineType.TwitchSocketCommand);
		}

		public void TwitchSocket_StreamWentOffline(ES_StreamOffline offlineData)
		{
			m_Parent.ThreadSafeAddPreviewText("Streamer went offline", LineType.TwitchSocketCommand);
		}

		public void TwitchSocket_AutoModMessageHold(ES_AutomodMessageHold messageHold)
		{
		}

		public void TwitchSocket_SuspiciousMessageReceived(ES_Suspicious_UserMessage suspiciousMessage)
		{
		}

		public void TwitchSocket_ChannelPointsRedeem(ES_ChannelPointRedeemRequest redeemInfo)
		{
			if (redeemInfo.broadcaster_user_id == ChannelInstance.ChannelID)
				m_Parent.TwitchEvents.OnChannelPointsRedeem?.Invoke(redeemInfo);
		}

		public void Dispose()
		{
			m_Parent?.TwitchEvents.Clear();

			StatusUpdateTimer.Elapsed -= StatusUpdateTimer_Elapsed;
			StatusUpdateTimer.Dispose();
		}
		#endregion
	}
}
