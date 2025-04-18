﻿using Meebey.SmartIrc4net;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BasicTwitchSoundPlayer.IRC
{
	//Some of the functions here may be a bit weird. Often the reason is, they were changed to use SmartIRC4Net from my original TCP socket client
	public struct ReadMessage
	{
		public string user;
		public string userID;
		public string message;
		public Structs.TwitchRightsEnum rights;
		public string RewardID;
	}

	public class OldIRCClient
	{
		MainForm parent;
		public bool ConnectedStatus = true;
		static readonly string IgnoredUsers = "ignored_users.txt";
		#region properties
		private string Config_Server { get; set; }
		private string Config_Username { get; set; }
		private string Config_Password { get; set; }
		private string Config_Channel { get; set; }
		#endregion

		public List<string> Moderators = new List<string>();
		public List<string> IgnoreList = new List<string>();
		public string[] Subscribers = new string[0];
		public KrakenConnections KrakenConnection { get; private set; }
		private Timer krakenUpdateTimer;

		//Because I really don't want to rewrite half of this
		public IrcClient MeebyIrc = new IrcClient();

		#region Constructor
		public OldIRCClient(MainForm parentReference, string Server, string Username, string Password, string Channel, string SoundRewardID = null)
		{
			parent = parentReference;
			Config_Server = Server;
			Config_Username = Username;
			Config_Password = Password;
			Config_Channel = Channel;

			MeebyIrc.Encoding = System.Text.Encoding.UTF8;
			MeebyIrc.SendDelay = 200;
			MeebyIrc.AutoRetry = true;
			MeebyIrc.AutoReconnect = true;

			try
			{
				MeebyIrc.Connect(Config_Server, 6667);
				while (!MeebyIrc.IsConnected)
					System.Threading.Thread.Sleep(50);
				KrakenConnection = new KrakenConnections(Channel);
				MeebyIrc.Login(Config_Username, Config_Username, 4, Config_Username, "oauth:" + Config_Password);
				krakenUpdateTimer = new Timer();
				krakenUpdateTimer.Start();
				krakenUpdateTimer.Elapsed += KrakenUpdateTimer_Elapsed;
				krakenUpdateTimer.Interval = 10;

				if (SoundRewardID != null)
				{
					Task run = KrakenConnection.VerifyChannelRewardsAsync(parentReference, SoundRewardID);
					Debug.WriteLine("Verifying reward IDs.");
				}
			}
			catch (ConnectionException e)
			{
				parent.ThreadSafeAddPreviewText("Could not connect! Reason:" + e.Message, LineType.IrcCommand);
			}

			LoadIgnoredList();
		}

		private void KrakenUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			krakenUpdateTimer.Interval = 2 * 60 * 1000;

			Task.Run(async () =>
			{
				await KrakenConnection.GetStreamerStatus();

				var newSubscribers = await KrakenConnection.GetSubscribersAsync();
				Debug.WriteLine("Updating subscribers");
				if (newSubscribers != null)
				{
					int PreviousSubscribers = newSubscribers.Length;
					if (PreviousSubscribers != newSubscribers.Length)
					{
						parent.ThreadSafeAddPreviewText("Subscriber amount changed to " + newSubscribers.Length, LineType.IrcCommand);
					}
					Subscribers = newSubscribers;
				}
			});
		}
		#endregion

		public void SendChatMessage(string message)
		{
			if (message.Length <= 500)
			{
				MeebyIrc.SendMessage(SendType.Message, "#" + Config_Channel, message);
			}
			else
			{
				var messages = SplitMessage(message, 500);
				foreach (var subMessage in messages)
				{
					MeebyIrc.SendMessage(SendType.Message, "#" + Config_Channel, subMessage);
				}
			}
		}

		public static List<string> SplitMessage(string v, int length)
		{
			if (v.Length <= length)
				return new List<string>() { v };

			var result = new List<string>();
			var split = v.Split(' ');

			var stringBuilder = new StringBuilder(500);
			for (int i = 0; i < split.Length; i++)
			{
				if (stringBuilder.Length + 1 + split[i].Length > 500)
				{
					result.Add(stringBuilder.ToString());
					stringBuilder.Clear();
				}

				if (stringBuilder.Length > 0)
					stringBuilder.Append(' ');

				stringBuilder.Append(split[i]);
			}

			result.Add(stringBuilder.ToString());
			return result;
		}

		public void SendChatMessage_NoDelays(string message)
		{
			int originalDelay = MeebyIrc.SendDelay;
			MeebyIrc.SendDelay = 0;
			MeebyIrc.SendMessage(SendType.Message, "#" + Config_Channel, message);
			MeebyIrc.SendDelay = originalDelay;
		}

		#region IgnoredList
		public void IgnoreListAdd(ReadMessage msg)
		{
			if (Moderators.Contains(msg.user))
			{
				string[] helper = msg.message.Split(new char[] { ' ' }, 2);
				if (!Moderators.Contains(helper[1].ToLower()))
				{
					if (!IgnoreList.Contains(helper[1].ToLower()))
					{
						IgnoreList.Add(helper[1].ToLower());
						SaveIgnoredList();
						SendChatMessage("Added " + helper[1] + " to ignored list.");
					}
					else
					{
						SendChatMessage(helper[1] + " is already on ignored list.");
					}
				}
				else
					SendChatMessage("Moderators can't be added to ignored list!");

			}
		}

		public void IgnoreListRemove(ReadMessage msg)
		{
			if (Moderators.Contains(msg.user))
			{
				string[] helper = msg.message.Split(new char[] { ' ' }, 2);

				if (IgnoreList.Contains(helper[1].ToLower()))
				{
					IgnoreList.Remove(helper[1].ToLower());
					SaveIgnoredList();
					SendChatMessage("Removed " + helper[1] + " from ignored list.");
				}
				else
				{
					SendChatMessage(helper[1] + " is not present on ignored list.");
				}
			}
		}

		public void LoadIgnoredList()
		{
			IgnoreList.Clear();
			if (File.Exists(IgnoredUsers))
			{
				StreamReader SR = new StreamReader(IgnoredUsers);
				string line = "";

				while ((line = SR.ReadLine()) != null)
				{
					if (line != "")
						IgnoreList.Add(line.ToLower());
				}
				SR.Close();
				SR.Dispose();
			}
		}

		public void SaveIgnoredList()
		{
			File.WriteAllLines(IgnoredUsers, IgnoreList);
		}
		#endregion
	}
}
