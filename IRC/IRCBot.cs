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
		public OldIRCClient Irc;
		private MainForm m_Parent;
		private string m_ChannelToJoin;
		private char m_PrefixChar;

		private SoundDB SndDB { get; set; }

		public IRCBot(SoundDB soundDb, char PrefixChar)
		{
			var privateSettings = PrivateSettings.GetInstance();

			Irc = new OldIRCClient(MainForm.Instance, privateSettings.TwitchServer, privateSettings.BotUsername, privateSettings.BotAuth, privateSettings.UserName);
			m_ChannelToJoin = privateSettings.UserName;
			m_Parent = MainForm.Instance;
			this.m_PrefixChar = PrefixChar;
			SndDB = soundDb;
		}

		public void Run()
		{
			InitBot(m_ChannelToJoin);
			this.BotRunning = true;
			try
			{
				Irc.MeebyIrc.Listen();
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
			Irc.SaveIgnoredList();

			Irc.MeebyIrc.OnError -= MeebyIrc_OnError;
			Irc.MeebyIrc.OnErrorMessage -= MeebyIrc_OnErrorMessage;
			Irc.MeebyIrc.OnConnecting -= MeebyIrc_OnConnecting;
			Irc.MeebyIrc.OnConnected -= MeebyIrc_OnConnected;
			Irc.MeebyIrc.OnAutoConnectError -= MeebyIrc_OnAutoConnectError;
			Irc.MeebyIrc.OnDisconnecting -= MeebyIrc_OnDisconnecting;
			Irc.MeebyIrc.OnDisconnected -= MeebyIrc_OnDisconnected;
			Irc.MeebyIrc.OnRegistered -= MeebyIrc_OnRegistered;
			Irc.MeebyIrc.OnPart -= MeebyIrc_OnPart;
			Irc.MeebyIrc.OnJoin -= MeebyIrc_OnJoin;
			Irc.MeebyIrc.OnChannelAction -= MeebyIrc_OnChannelAction;
			Irc.MeebyIrc.OnReadLine -= MeebyIrc_OnReadLine;
			Irc.MeebyIrc.OnRawMessage -= MeebyIrc_OnRawMessage;

			Task.Factory.StartNew(() =>
				Irc.MeebyIrc.Disconnect()
			);

			System.Threading.Thread.Sleep(200);
		}

		private void InitBot(string channel)
		{
			Irc.MeebyIrc.OnError += MeebyIrc_OnError;
			Irc.MeebyIrc.OnErrorMessage += MeebyIrc_OnErrorMessage;
			Irc.MeebyIrc.OnConnecting += MeebyIrc_OnConnecting;
			Irc.MeebyIrc.OnConnected += MeebyIrc_OnConnected;
			Irc.MeebyIrc.OnAutoConnectError += MeebyIrc_OnAutoConnectError;
			Irc.MeebyIrc.OnDisconnecting += MeebyIrc_OnDisconnecting;
			Irc.MeebyIrc.OnDisconnected += MeebyIrc_OnDisconnected;
			Irc.MeebyIrc.OnRegistered += MeebyIrc_OnRegistered;
			Irc.MeebyIrc.OnPart += MeebyIrc_OnPart;
			Irc.MeebyIrc.OnJoin += MeebyIrc_OnJoin;
			Irc.MeebyIrc.OnChannelAction += MeebyIrc_OnChannelAction;
			Irc.MeebyIrc.OnReadLine += MeebyIrc_OnReadLine;
			Irc.MeebyIrc.OnOp += MeebyIrc_OnOp;
			Irc.MeebyIrc.OnDeop += MeebyIrc_OnDeop;
			Irc.MeebyIrc.OnRawMessage += MeebyIrc_OnRawMessage;

			//Request capabilities - https://dev.twitch.tv/docs/irc/guide/#twitch-irc-capabilities
			Irc.MeebyIrc.WriteLine("CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
			Irc.MeebyIrc.RfcJoin("#" + channel);

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
			if (!formattedMessage.message.StartsWith(m_PrefixChar.ToString()) || Irc.IgnoreList.Contains(formattedMessage.user))
			{
				m_Parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.Generic);
				//literally nothing else happens in your code if this is false
				return true;
			}
			else
			{
				string text = formattedMessage.message.Remove(0, 1).ToLower();

				//Mod Commands
				if (formattedMessage.rights >= TwitchRightsEnum.Mod || Irc.Moderators.Contains(formattedMessage.user))
				{
					if (text == "stopallsounds")
					{
						m_Parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
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

						m_Parent.ThreadSafeAddPreviewText(formattedMessage.user + ": " + formattedMessage.message, LineType.ModCommand);
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
				m_Parent.ThreadSafeAddPreviewText("! JOINED: " + e.Data.Nick, LineType.IrcCommand);
		}

		private void MeebyIrc_OnRegistered(object sender, EventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("! LOGIN VERIFIED", LineType.IrcCommand);
		}

		private void MeebyIrc_OnDisconnected(object sender, EventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("Disconnected.", LineType.IrcCommand);
			BotRunning = false;
		}

		private void MeebyIrc_OnDisconnecting(object sender, EventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("Disconnecting...", LineType.IrcCommand);
		}

		private void MeebyIrc_OnAutoConnectError(object sender, Meebey.SmartIrc4net.AutoConnectErrorEventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("OnAutoConnectError Event: " + e.Exception, LineType.IrcCommand);
		}

		private void MeebyIrc_OnConnecting(object sender, EventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("Connecting: " + e, LineType.IrcCommand);
		}

		private void MeebyIrc_OnConnected(object sender, EventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("Connected: " + e, LineType.IrcCommand);
		}

		private void MeebyIrc_OnChannelAction(object sender, Meebey.SmartIrc4net.ActionEventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("OnChannelAction Event: " + e.Data, LineType.IrcCommand);
		}

		private void MeebyIrc_OnErrorMessage(object sender, Meebey.SmartIrc4net.IrcEventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("! " + e.Data.Message + " !", LineType.IrcCommand);
		}

		private void MeebyIrc_OnError(object sender, Meebey.SmartIrc4net.ErrorEventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("OnError Event: " + e.Data.Message, LineType.IrcCommand);
		}

		private void MeebyIrc_OnReadLine(object sender, Meebey.SmartIrc4net.ReadLineEventArgs e)
		{
			/*			if (PrivateSettings.GetInstance().Debug_mode)
							parent.ThreadSafeAddPreviewText($"Raw message: {e.Line}", LineType.IrcCommand);*/
		}

		private void MeebyIrc_OnPart(object sender, PartEventArgs e)
		{
			if (PrivateSettings.GetInstance().Debug_mode)
				m_Parent.ThreadSafeAddPreviewText("! PART: " + e.Data.Nick, LineType.IrcCommand);
		}

		private void MeebyIrc_OnOp(object sender, OpEventArgs e)
		{
			if (!Irc.Moderators.Contains(e.Whom))
				Irc.Moderators.Add(e.Whom);
			m_Parent.ThreadSafeAddPreviewText("! +OP: " + e.Whom, LineType.IrcCommand);
		}

		private void MeebyIrc_OnDeop(object sender, DeopEventArgs e)
		{
			m_Parent.ThreadSafeAddPreviewText("! -OP: " + e.Whom, LineType.IrcCommand);
		}
		#endregion
	}
}
