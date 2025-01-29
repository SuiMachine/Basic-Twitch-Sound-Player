using BasicTwitchSoundPlayer.Structs;
using Meebey.SmartIrc4net;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.IRC
{
	public class IRCBot
	{
		public bool BotRunning;
		public OldIRCClient irc;
		private MainForm parent;
		private string m_ChannelToJoin;
		private char PrefixChar;

		private SoundDB SndDB { get; set; }

		public IRCBot(SoundDB soundDb, char PrefixChar)
		{
			var privateSettings = PrivateSettings.GetInstance();

			irc = new OldIRCClient(MainForm.Instance, privateSettings.TwitchServer, privateSettings.BotUsername, privateSettings.BotAuth, privateSettings.UserName);
			m_ChannelToJoin = privateSettings.UserName;
			parent = MainForm.Instance;
			this.PrefixChar = PrefixChar;
			SndDB = soundDb;
		}

		public void Run()
		{
			InitBot(m_ChannelToJoin);
			this.BotRunning = true;
			try
			{
				irc.MeebyIrc.Listen();
			}
			catch (ThreadInterruptedException e)
			{
				Debug.WriteLine("Nop" + e);
			}
		}

		public void StopBot()
		{
			SndDB.Close();
			PrivateSettings.GetInstance().SaveSettings();
			irc.SaveIgnoredList();

			irc.MeebyIrc.OnError -= MeebyIrc_OnError;
			irc.MeebyIrc.OnErrorMessage -= MeebyIrc_OnErrorMessage;
			irc.MeebyIrc.OnConnecting -= MeebyIrc_OnConnecting;
			irc.MeebyIrc.OnConnected -= MeebyIrc_OnConnected;
			irc.MeebyIrc.OnAutoConnectError -= MeebyIrc_OnAutoConnectError;
			irc.MeebyIrc.OnDisconnecting -= MeebyIrc_OnDisconnecting;
			irc.MeebyIrc.OnDisconnected -= MeebyIrc_OnDisconnected;
			irc.MeebyIrc.OnRegistered -= MeebyIrc_OnRegistered;
			irc.MeebyIrc.OnPart -= MeebyIrc_OnPart;
			irc.MeebyIrc.OnJoin -= MeebyIrc_OnJoin;
			irc.MeebyIrc.OnChannelAction -= MeebyIrc_OnChannelAction;
			irc.MeebyIrc.OnReadLine -= MeebyIrc_OnReadLine;
			irc.MeebyIrc.OnRawMessage -= MeebyIrc_OnRawMessage;

			Task.Factory.StartNew(() =>
				irc.MeebyIrc.Disconnect()
			);

			System.Threading.Thread.Sleep(200);
		}

		private void InitBot(string channel)
		{
			irc.MeebyIrc.OnError += MeebyIrc_OnError;
			irc.MeebyIrc.OnErrorMessage += MeebyIrc_OnErrorMessage;
			irc.MeebyIrc.OnConnecting += MeebyIrc_OnConnecting;
			irc.MeebyIrc.OnConnected += MeebyIrc_OnConnected;
			irc.MeebyIrc.OnAutoConnectError += MeebyIrc_OnAutoConnectError;
			irc.MeebyIrc.OnDisconnecting += MeebyIrc_OnDisconnecting;
			irc.MeebyIrc.OnDisconnected += MeebyIrc_OnDisconnected;
			irc.MeebyIrc.OnRegistered += MeebyIrc_OnRegistered;
			irc.MeebyIrc.OnPart += MeebyIrc_OnPart;
			irc.MeebyIrc.OnJoin += MeebyIrc_OnJoin;
			irc.MeebyIrc.OnChannelAction += MeebyIrc_OnChannelAction;
			irc.MeebyIrc.OnReadLine += MeebyIrc_OnReadLine;
			irc.MeebyIrc.OnOp += MeebyIrc_OnOp;
			irc.MeebyIrc.OnDeop += MeebyIrc_OnDeop;
			irc.MeebyIrc.OnRawMessage += MeebyIrc_OnRawMessage;

			//Request capabilities - https://dev.twitch.tv/docs/irc/guide/#twitch-irc-capabilities
			irc.MeebyIrc.WriteLine("CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
			irc.MeebyIrc.RfcJoin("#" + channel);

			MainForm.TwitchSocket.SetIrcReference(this);
			VoiceModHandling.GetInstance().ConnectToVoiceMod();
		}

		private void MeebyIrc_OnRawMessage(object sender, IrcEventArgs e)
		{
			try
			{
				if (e.Data.Channel != null && e.Data.Nick != null && e.Data.Message != null)
				{
					ReadMessage msg = new ReadMessage();
					if (e.Data.Tags.ContainsKey("custom-reward-id"))
					{
						var settings = PrivateSettings.GetInstance();
						//var rewardID = e.Data.Tags["custom-reward-id"];
						msg.userID = e.Data.Tags["user-id"];
						msg.RewardID = "";
					}
					msg.user = e.Data.Nick;
					msg.message = e.Data.Message;
					msg.rights = GetRoleFromTags(e);
					bool result = RunBot(msg);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				Logger.AddLine("Exception on Raw Message: " + ex.ToString());
			}

		}

		private TwitchRightsEnum GetRoleFromTags(IrcEventArgs e)
		{
			if (e.Data.Nick.ToLower() == PrivateSettings.GetInstance().UserName.ToLower())
				return TwitchRightsEnum.Admin;
			else
			{
				if (e.Data.Tags != null)
				{
					//Ref: https://dev.twitch.tv/docs/irc/tags/
					if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("broadcaster/1"))
						return TwitchRightsEnum.Admin;
					if (e.Data.Tags.ContainsKey("mod") && e.Data.Tags["mod"] == "1")
						return TwitchRightsEnum.Mod;
					else if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("vip/1"))
						return TwitchRightsEnum.TrustedSub;
					else if (e.Data.Tags.ContainsKey("badges") && e.Data.Tags["badges"].Contains("subscriber/1"))
						return TwitchRightsEnum.TrustedSub;
					else
						return TwitchRightsEnum.Public;
				}
				else
					return TwitchRightsEnum.Public;
			}
		}

		private bool RunBot(ReadMessage formattedMessage)
		{
			if (!formattedMessage.message.StartsWith(PrefixChar.ToString()) || irc.IgnoreList.Contains(formattedMessage.user))
			{
				parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.Generic);
				//literally nothing else happens in your code if this is false
				return true;
			}
			else
			{
				string text = formattedMessage.message.Remove(0, 1).ToLower();

				//Mod Commands
				if (formattedMessage.rights >= TwitchRightsEnum.Mod || irc.Moderators.Contains(formattedMessage.user))
				{
					if (text == "stopallsounds")
					{
						parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
						SndDB.StopAllSounds();
						return true;
					}

					if (text.ToLower().StartsWith("delay "))
					{
						var split = text.Split(' ');
						text = split[split.Length - 1];
						if (int.TryParse(text, out int delayValue))
						{
							if (delayValue < 0)
								delayValue = 0;
							this.SndDB.SetDelay(delayValue);
						}

						parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
						return true;
					}
				}

				return true;
			}
		}

		internal void UpdateVolume() => SndDB.ChangeVolume(PrivateSettings.GetInstance().Volume);

		#region EventHandlers
		private void MeebyIrc_OnJoin(object sender, Meebey.SmartIrc4net.JoinEventArgs e)
		{
			if (PrivateSettings.GetInstance().Debug_mode)
				parent.ThreadSafeAddPreviewText("! JOINED: " + e.Data.Nick, LineType.IrcCommand);
		}

		private void MeebyIrc_OnRegistered(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("! LOGIN VERIFIED", LineType.IrcCommand);
		}

		private void MeebyIrc_OnDisconnected(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Disconnected.", LineType.IrcCommand);
			BotRunning = false;
		}

		private void MeebyIrc_OnDisconnecting(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Disconnecting...", LineType.IrcCommand);
		}

		private void MeebyIrc_OnAutoConnectError(object sender, Meebey.SmartIrc4net.AutoConnectErrorEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("OnAutoConnectError Event: " + e.Exception, LineType.IrcCommand);
		}

		private void MeebyIrc_OnConnecting(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Connecting: " + e, LineType.IrcCommand);
		}

		private void MeebyIrc_OnConnected(object sender, EventArgs e)
		{
			parent.ThreadSafeAddPreviewText("Connected: " + e, LineType.IrcCommand);
		}

		private void MeebyIrc_OnChannelAction(object sender, Meebey.SmartIrc4net.ActionEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("OnChannelAction Event: " + e.Data, LineType.IrcCommand);
		}

		private void MeebyIrc_OnErrorMessage(object sender, Meebey.SmartIrc4net.IrcEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("! " + e.Data.Message + " !", LineType.IrcCommand);
		}

		private void MeebyIrc_OnError(object sender, Meebey.SmartIrc4net.ErrorEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("OnError Event: " + e.Data.Message, LineType.IrcCommand);
		}

		private void MeebyIrc_OnReadLine(object sender, Meebey.SmartIrc4net.ReadLineEventArgs e)
		{
			/*			if (PrivateSettings.GetInstance().Debug_mode)
							parent.ThreadSafeAddPreviewText($"Raw message: {e.Line}", LineType.IrcCommand);*/
		}

		private void MeebyIrc_OnPart(object sender, PartEventArgs e)
		{
			if (PrivateSettings.GetInstance().Debug_mode)
				parent.ThreadSafeAddPreviewText("! PART: " + e.Data.Nick, LineType.IrcCommand);
		}

		private void MeebyIrc_OnOp(object sender, OpEventArgs e)
		{
			if (!irc.Moderators.Contains(e.Whom))
				irc.Moderators.Add(e.Whom);
			parent.ThreadSafeAddPreviewText("! +OP: " + e.Whom, LineType.IrcCommand);
		}

		private void MeebyIrc_OnDeop(object sender, DeopEventArgs e)
		{
			parent.ThreadSafeAddPreviewText("! -OP: " + e.Whom, LineType.IrcCommand);
		}
		#endregion
	}
}
