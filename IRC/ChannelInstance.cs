using SuiBot_TwitchSocket.API.EventSub;
using SuiBot_TwitchSocket.API.Helix.Responses;
using SuiBot_TwitchSocket.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.IRC
{
	public class ChannelInstance : IChannelInstance
	{
		private readonly MainForm parent;
		public bool ConnectedStatus = true;
		static readonly string IgnoredUsers = "ignored_users.txt";
		#region properties
		private string Config_Password { get; set; }
		private string Config_Channel { get; set; }
		#endregion

		public List<string> IgnoreList = new List<string>();
		public string[] Subscribers = new string[0];
		public string ChannelID { get; set; }
		public string Channel { get; set; }

		public Response_StreamStatus StreamStatus { get; set; }
		private ChatBot m_ChatBot;

		//Because I really don't want to rewrite half of this

		public ChannelInstance(ChatBot chatBotInstance, string SoundRewardID = null)
		{
			m_ChatBot = chatBotInstance;
			StreamStatus = new Response_StreamStatus();
			LoadIgnoredList();
		}

		public void SendChatMessage(string message)
		{
			Task.Run(async () =>
			{
				if (message.Length <= 500)
				{
					await m_ChatBot?.HelixAPI_Bot.SendMessageAsync(this, message);
				}
				else
				{
					var messages = SplitMessage(message, 500);
					foreach (var subMessage in messages)
					{
						await m_ChatBot?.HelixAPI_Bot.SendMessageAsync(this, subMessage);
					}
				}
			});
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

		public void IgnoreListAdd(ES_ChatMessage msg)
		{
			if (msg.UserRole <= ES_ChatMessage.Role.Mod)
			{
				string[] helper = msg.message.text.Split(new char[] { ' ' }, 2);
				if (!IgnoreList.Contains(helper[1].ToLower()))
				{
					IgnoreList.Add(helper[1].ToLower());
					SaveIgnoredList();
					SendChatMessageResponse(msg, "Added " + helper[1] + " to ignored list.");
				}
				else
				{
					SendChatMessage(helper[1] + " is already on ignored list.");
				}

				//TODO: Reimplement check if we are not adding a moderator?
				/*				if (!Moderators.Contains(helper[1].ToLower()))
								{
									....
								}
								else
									SendChatMessage("Moderators can't be added to ignored list!");*/

			}
		}

		private void SendChatMessageResponse(ES_ChatMessage messageToRespondTo, string text)
		{
			Task.Run(async () =>
			{
				if (text.Length <= 500)
				{
					await m_ChatBot?.HelixAPI_Bot.SendResponseAsync(messageToRespondTo, text);
				}
				else
				{
					var messages = SplitMessage(text, 500);
					foreach (var subMessage in messages)
					{
						await m_ChatBot?.HelixAPI_Bot.SendResponseAsync(messageToRespondTo, subMessage);
					}
				}
			});
		}

		public void IgnoreListRemove(ES_ChatMessage msg)
		{
			if (msg.UserRole <= ES_ChatMessage.Role.Mod)
			{
				string[] helper = msg.message.text.Split(new char[] { ' ' }, 2);

				if (IgnoreList.Contains(helper[1].ToLower()))
				{
					IgnoreList.Remove(helper[1].ToLower());
					SaveIgnoredList();
					SendChatMessageResponse(msg, $"Removed {helper[1]} from ignored list.");
				}
				else
				{
					SendChatMessageResponse(msg, $"{helper[1]} is not present on ignored list.");
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

		public bool IsSuperMod(string username) => username == Channel;

		internal void UpdateTwitchStatus()
		{
			m_ChatBot?.HelixAPI_Bot.GetStatus(this);
		}

		internal void UserTimeout(ES_ChatMessage message, uint duration_in_seconds, string text_response)
		{
			m_ChatBot?.HelixAPI_Bot.RequestTimeout(message, duration_in_seconds, text_response);
		}

		internal void UserBan(ES_ChatMessage message, string response)
		{
			m_ChatBot?.HelixAPI_Bot.RequestBan(message, response);
		}
	}
}
