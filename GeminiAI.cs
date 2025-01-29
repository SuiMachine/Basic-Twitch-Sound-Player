using BasicTwitchSoundPlayer.Structs.Gemini;
using System;
using System.Collections.Generic;
using static BasicTwitchSoundPlayer.IRC.KrakenConnections;

namespace BasicTwitchSoundPlayer
{
	public class GeminiAI
	{
		public bool IsRegistered { get; private set; }
		public GeminiContent StreamerContent = new GeminiContent();
		public Dictionary<string, GeminiContent> UserContents = new Dictionary<string, GeminiContent>();
		public Dictionary<string, DateTime> Cooldowns = new Dictionary<string, DateTime>();

		internal bool IsConfigured()
		{
			var aiConfig = AIConfig.GetInstance();

			if (string.IsNullOrEmpty(aiConfig.ApiKey))
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Can't setup AI - no {nameof(aiConfig.ApiKey)} provided. This might be OK?", LineType.GeminiAI);
				return false;
			}

			var privateSettings = PrivateSettings.GetInstance();
			if (string.IsNullOrEmpty(privateSettings.UserName))
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Can't setup AI - no streamer {nameof(privateSettings.UserName)} provided.", LineType.GeminiAI);
				return false;
			}

			if (string.IsNullOrEmpty(aiConfig.TwitchAwardID))
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Can't setup AI - no {nameof(aiConfig.TwitchAwardID)} set up.", LineType.GeminiAI);
				return false;
			}

			StreamerContent = XML_Utils.Load(AIConfig.GetAIHistory(privateSettings.UserName), new GeminiContent()
			{
				contents = new List<GeminiMessage>(),
				generationConfig = new GeminiContent.GenerationConfig(),
				systemInstruction = new GeminiMessage()
				{
					role = Role.user,
					parts = new GeminiMessagePart[]
					{
						new GeminiMessagePart()
						{
							text = ""
						}
					}
				},
				safetySettings = new SafetySettingsCategory[]
				{
					new SafetySettingsCategory("HARM_CATEGORY_HARASSMENT", aiConfig.FilterSet_Streamer.Harassment),
					new SafetySettingsCategory("HARM_CATEGORY_HATE_SPEECH", aiConfig.FilterSet_Streamer.Hate),
					new SafetySettingsCategory("HARM_CATEGORY_SEXUALLY_EXPLICIT", aiConfig.FilterSet_User.Sexually_Explicit),
					new SafetySettingsCategory("HARM_CATEGORY_DANGEROUS_CONTENT", aiConfig.FilterSet_User.Dangerous_Content),
					new SafetySettingsCategory("HARM_CATEGORY_CIVIC_INTEGRITY", aiConfig.FilterSet_User.Civic_Integrity),
				}
			});

			return true;
		}

		public void Register()
		{
			IsRegistered = true;
			MainForm.TwitchSocket.OnChannelPointsRedeem += PointsRedeem;
		}

		public void Unregister()
		{
			IsRegistered = false;
			MainForm.TwitchSocket.OnChannelPointsRedeem -= PointsRedeem;
		}

		public void PointsRedeem(ChannelPointRedeemRequest request)
		{
			var rewardID = AIConfig.GetInstance().TwitchAwardID;
			if (string.IsNullOrEmpty(rewardID))
				return;
			if (request.rewardId != rewardID)
				return;

			if (request.userName.ToLower() == PrivateSettings.GetInstance().UserName.ToLower())
			{

			}
			else
			{
				MainForm.Instance.TwitchBot.irc.SendChatMessage($"{request.userName} - sorry, this feature is currently not available to users!");

			}
		}

		/*public override bool DoWork(ChatMessage lastMessage)
		{
			if (lastMessage.UserRole == Role.)
			{
				if (lastMessage.Username != channelInstance.Channel)
				{
					if (Cooldowns.TryGetValue(lastMessage.Username, out var cooldown))
					{
						if (cooldown + TimeSpan.FromMinutes(45) > DateTime.UtcNow)
						{
							TimeSpan timespan = cooldown + TimeSpan.FromMinutes(45) - DateTime.UtcNow;
							channelInstance.SendChatMessageResponse(lastMessage, $"Chill there for {Math.Ceiling(Math.Abs(timespan.TotalMinutes))} min. We have request limits!");
							return true;
						}
						else
						{
							Cooldowns[lastMessage.Username] = DateTime.UtcNow;
						}
					}
					else
					{
						Cooldowns.Add(lastMessage.Username, DateTime.UtcNow);
					}
				}

				Task.Run(async () =>
				{
					await GetResponse(channelInstance, lastMessage);
				});
			}

			return true;
		}

		private async Task GetResponse()
		{
			try
			{
				bool isStreamer = channelInstance.Channel == lastMessage.Username; //Streamer responses are stored permanently


				Gemini.GeminiContent content = null;
				if (channelInstance.Channel == lastMessage.Username)
				{
					content = StreamerContent;
				}
				else if (!UserContents.TryGetValue(lastMessage.Username, out content))
				{
					string instruction = null;
					if (channelInstance.Channel == lastMessage.Username)
					{
						instruction = InstanceConfig.Instruction_Streamer;
						isStreamer = true;
					}
					else
					{
						var path = $"Bot/Channels/{channelInstance.Channel}/MemeComponents/AI_UserInstructions/{lastMessage.Username}.txt";
						if (File.Exists(path))
						{
							instruction = File.ReadAllText(path);
						}

						if (instruction == null)
						{
							instruction = string.Format(InstanceConfig.Instruction_Generic, lastMessage.Username);
						}
					}

					if (instruction == null)
					{
						channelInstance.SendChatMessageResponse(lastMessage, "Sorry, there is no response configured for non-streamer or you specifically");
					}

					content = new Gemini.GeminiContent()
					{
						contents = new List<Gemini.GeminiMessage>(),
						generationConfig = new Gemini.GeminiContent.GenerationConfig(),
						systemInstruction = new Gemini.GeminiMessage()
						{
							role = Gemini.Role.user,
							parts = new Gemini.GeminiMessagePart[]
							{
								new Gemini.GeminiMessagePart()
								{
									text = instruction
								}
							}
						}
					};
					UserContents.Add(lastMessage.Username, content);
				}

				if (content == null)
				{
					channelInstance.SendChatMessageResponse(lastMessage, "Sorry, no history for the user is setup");
					return;
				}

				content.contents.Add(GeminiMessage.CreateUserResponse(lastMessage.Message.StripSingleWord()));

				string json = JsonConvert.SerializeObject(content);

				string result = await HttpWebRequestHandlers.PerformPost(
					new Uri($"https://generativelanguage.googleapis.com/v1beta/{InstanceConfig.Model}:generateContent?key={InstanceConfig.API_Key}"),
					new Dictionary<string, string>(),
					json
					);

				if (string.IsNullOrEmpty(result))
				{
					channelInstance.SendChatMessageResponse(lastMessage, "Failed to get a response. Please debug me, Sui :(");
				}
				else
				{
					Gemini.GeminiResponse response = JsonConvert.DeserializeObject<Gemini.GeminiResponse>(result);
					content.generationConfig.TokenCount = response.usageMetadata.totalTokenCount;

					if (response.candidates.Length > 0 && response.candidates.Last().content.parts.Length > 0)
					{
						var lastResponse = response.candidates.Last().content;
						content.contents.Add(lastResponse);
						var text = lastResponse.parts.Last().text;
						List<string> splitText = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
						for (int i = splitText.Count - 1; i >= 0; i--)
						{
							var line = splitText[i].Trim();
							if (line.StartsWith("*") && line.StartsWith("*"))
							{
								var count = line.Count(x => x == '*');
								if (count == 2)
								{
									splitText.RemoveAt(i);
									continue;
								}
							}

							if (line.Contains("*"))
							{
								line = CleanDescriptors(line);
								splitText[i] = line;
							}
						}

						text = string.Join(" ", splitText);

						channelInstance.SendChatMessageResponse(lastMessage, text);

						while (content.generationConfig.TokenCount > InstanceConfig.TokenLimit)
						{
							if (content.contents.Count > 2)
							{
								//This isn't weird - we want to make sure we start from user message
								if (content.contents[0].role == Structs.Gemini.Role.user)
								{
									content.contents.RemoveAt(0);
								}

								if (content.contents[0].role == Structs.Gemini.Role.model)
								{
									content.contents.RemoveAt(0);
								}
							}
						}

						if (isStreamer)
						{
							XML_Utils.Save(StreamerPath, content);
						}
						else
						{
							while (content.contents.Count > 10)
							{
								content.contents.RemoveAt(0);
							}
						}
					}
					else
					{
						channelInstance.SendChatMessageResponse(lastMessage, "Failed to get a response. Please debug me, Sui :(");
					}
				}
			}
			catch (Exception ex)
			{
				channelInstance.SendChatMessageResponse(lastMessage, "Failed to get a response. Something was written in log. Sui help! :(");
				MainForm.Instance.ThreadSafeAddPreviewText($"There was an error trying to do AI: {ex}", LineType.GeminiAI);
			}
		}*/

		private string CleanDescriptors(string text)
		{
			int endIndex = text.Length;
			bool isDescription = false;

			for (int i = text.Length - 1; i >= 0; i--)
			{
				if (text[i] == '*')
				{
					if (!isDescription)
					{
						endIndex = i;
						isDescription = true;
					}
					else
					{
						var length = i - endIndex;
						var substring = text.Substring(i + 1, endIndex - i - 1);
						if (substring.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 5)
						{
							text = text.Remove(i, endIndex - i + 1);
						}
						isDescription = false;
					}
				}
			}

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}

			text = text.Trim();
			return text;
		}
	}
}
