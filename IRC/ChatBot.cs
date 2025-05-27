using BasicTwitchSoundPlayer.Structs;
using SuiBot_Core;
using SuiBot_Core.API.EventSub;
using SuiBot_TwitchSocket.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.IRC
{
	public class ChatBot : IBotInstance, IDisposable
	{
		public const string BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID = "9z58zy6ak0ejk9lme6dy6nyugydaes";

		public bool BotRunning;
		public ChannelInstance ChannelInstance;
		internal TwitchSocket TwitchSocket { get; private set; }
		internal SuiBot_Core.API.HelixAPI HelixAPI_User { get; private set; }
		private SuiBot_Core.API.HelixAPI m_HelixAPI_Bot;
		internal SuiBot_Core.API.HelixAPI HelixAPI_Bot => m_HelixAPI_Bot != null ? m_HelixAPI_Bot : HelixAPI_User;


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
			HelixAPI_User = new SuiBot_Core.API.HelixAPI(BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, this, privateSettings.UserAuth);

			if (privateSettings.BotAuth != "")
				m_HelixAPI_Bot = new SuiBot_Core.API.HelixAPI(BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, this, privateSettings.BotAuth);

			var authVerify = HelixAPI_User.GetValidation();
			if(authVerify == null || string.IsNullOrEmpty(authVerify.login))
				throw new Exception("Failed to validate user token - a new one might need to be generated?");
			m_ChannelToJoin = authVerify.login;

			if(m_HelixAPI_Bot != null)
			{
				var validationResult = m_HelixAPI_Bot.ValidateToken();
				if (validationResult != SuiBot_Core.API.HelixAPI.ValidationResult.Successful)
					throw new Exception($"Failed to validate bot token. Validation status was {validationResult}");
			}

			m_Parent = MainForm.Instance;
			this.StatusUpdateTimer = new System.Timers.Timer(5 * 1000 * 60) { AutoReset = true };
			this.m_PrefixChar = PrefixChar;
			SndDB = soundDb;
		}


		internal void Connect()
		{
			VoiceModHandling.GetInstance().ConnectToVoiceMod();

		}

		public void Run()
		{
/*			InitBot(m_ChannelToJoin);
			this.BotRunning = true;
			SndDB.Register();
			try
			{
				ChannelInstance.MeebyIrc.Listen();
			}
			catch (ThreadInterruptedException e)
			{
				Debug.WriteLine("Nop" + e);
			}*/
		}

		public void StopBot()
		{
/*			SndDB.Close();
			PrivateSettings.GetInstance().SaveSettings();
			ChannelInstance.SaveIgnoredList();

			Task.Factory.StartNew(() =>
				ChannelInstance.MeebyIrc.Disconnect()
			);

			System.Threading.Thread.Sleep(200);*/
		}

		private void InitBot(string channel)
		{

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

		}

		public void TwitchSocket_Disconnected()
		{
		}

		public void TwitchSocket_ClosedViaSocket()
		{
		}

		public void TwitchSocket_ChatMessage(ES_ChatMessage chatMessage)
		{
			if (!chatMessage.message.text.StartsWith(m_PrefixChar.ToString()) || ChannelInstance.IgnoreList.Contains(chatMessage.chatter_user_login))
			{
				m_Parent.ThreadSafeAddPreviewText($"{chatMessage.broadcaster_user_name}: {chatMessage.message.text}", LineType.Generic);
				//literally nothing else happens in your code if this is false
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

						m_Parent.ThreadSafeAddPreviewText($"{chatMessage.broadcaster_user_name}: {chatMessage.message.text}", LineType.ModCommand);
						return;
					}
				}
			}
		}

		public void TwitchSocket_StreamWentOnline(ES_StreamOnline onlineData)
		{
		}

		public void TwitchSocket_StreamWentOffline(ES_StreamOffline offlineData)
		{
		}

		public void TwitchSocket_AutoModMessageHold(ES_AutomodMessageHold messageHold)
		{
		}

		public void TwitchSocket_SuspiciousMessageReceived(ES_Suspicious_UserMessage suspiciousMessage)
		{
		}

		public void TwitchSocket_ChannelPointsRedeem(ES_ChannelPoints redeemInfo)
		{
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
