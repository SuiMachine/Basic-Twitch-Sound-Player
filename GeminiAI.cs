using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.Structs.Gemini;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

			var path = AIConfig.GetAIHistoryPath(privateSettings.UserName);
			StreamerContent = XML_Utils.Load(path, new GeminiContent()
			{
				StoragePath = path,
				contents = new List<GeminiMessage>(),
				generationConfig = new GeminiContent.GenerationConfig(),
				systemInstruction = null,
				safetySettings = null,
			});
			StreamerContent.StoragePath = path;

			return true;
		}

		public void Register()
		{
			//TODO: There is some bug with registering it, probably order of execution :-/
			IsRegistered = true;
			MainForm.TwitchSocket.OnChannelPointsRedeem += PointsRedeem;

			//Safety tripping test
			/*			Task.Run(async () =>
						{
							var newr = new ChannelPointRedeemRequest("userName", "69", "69", "69", RedemptionStates.FULFILLED, "What is 69? Make it as lewd as possible.");
							await GetResponse(newr);
						});*/
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

			Task.Run(async () =>
			{
				await GetResponse(request);
			});

		}

		private async Task GetResponse(ChannelPointRedeemRequest request)
		{
			try
			{
				GeminiContent content = null;
				AIConfig aiConfig = AIConfig.GetInstance();
				OldIRCClient irc = MainForm.Instance.TwitchBot.Irc;

				int tokenLimit = 1000;
				var lowerCaseUserName = request.userName.ToLower();

				if (lowerCaseUserName == PrivateSettings.GetInstance().UserName.ToLower())
				{
					content = StreamerContent;

					tokenLimit = aiConfig.TokenLimit_Streamer;
					content.safetySettings = aiConfig.GetSafetySettingsStreamer();
					content.systemInstruction = aiConfig.GetInstruction(request.userName, true, true);
				}
				else
				{
					if (!UserContents.TryGetValue(request.userId, out content))
					{
						var path = AIConfig.GetAIHistoryPath(request.userId);
						if (File.Exists(path))
						{
							content = XML_Utils.Load(path, new GeminiContent()
							{
								contents = new List<GeminiMessage>(),
								generationConfig = new GeminiContent.GenerationConfig(),
							});
							content.StoragePath = path;
						}
						else
						{
							content = new GeminiContent()
							{
								contents = new List<GeminiMessage>(),
								generationConfig = new GeminiContent.GenerationConfig(),
							};
						}

						UserContents.Add(request.userId, content);
					}

					if (content.StoragePath == null)
						content.StoragePath = AIConfig.GetAIHistoryPath(request.userId);

					tokenLimit = aiConfig.TokenLimit_User;
					GeminiCharacterOverride overrides = GeminiCharacterOverride.GetOverride(GeminiCharacterOverride.GetOverridePath(request.userName));
					if (overrides != null)
					{
						//content.systemInstruction = 
						content.safetySettings = overrides.GetSafetyOverrides();
						content.systemInstruction = overrides.GetFullInstruction();
					}
					else
					{
						content.safetySettings = aiConfig.GetSafetySettingsGeneral();
						content.systemInstruction = aiConfig.GetInstruction(request.userName, false, irc.KrakenConnection.IsLive);
					}

				}

				if (content == null)
				{
					MainForm.Instance.TwitchBot.Irc.SendChatMessage($"{request.userName}: Failed to load user history.");
					irc.KrakenConnection.UpdateRedemptionStatus(request.rewardId, new string[] { request.redemptionId }, RedemptionStates.CANCELED);
					return;
				}

				content.contents.Add(GeminiMessage.CreateUserResponse(request.userInput));
				string json = JsonConvert.SerializeObject(content);

				string result = await HttpWebRequestHandlers.PerformPost(
					new Uri($"https://generativelanguage.googleapis.com/v1beta/{aiConfig.Model}:generateContent?key={aiConfig.ApiKey}"),
					new Dictionary<string, string>(),
					json
					);

				if (string.IsNullOrEmpty(result))
				{
					MainForm.Instance.TwitchBot.Irc.SendChatMessage($"{request.userName} - Failed to get a response. Please debug me, Sui :(");
					irc.KrakenConnection.UpdateRedemptionStatus(request.rewardId, new string[] { request.redemptionId }, RedemptionStates.CANCELED);
					return;
				}
				else
				{
					GeminiResponse response = JsonConvert.DeserializeObject<GeminiResponse>(result);
					content.generationConfig.TokenCount = response.usageMetadata.totalTokenCount;

					if (response.candidates.Length > 0)
					{
						var lastFullResponse = response.candidates.Last();
						string finishReason = lastFullResponse.finishReason;
						if (finishReason == "STOP")
						{
							var lastResponse = lastFullResponse.content;
							foreach (var part in lastFullResponse.content.parts)
							{
								part.text = part.text.Trim('\n', '\r', ' ');
								part.text = ClearUpBackwardSlashCharacters(part.text);
							}

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
							irc.SendChatMessage($"@{request.userName}: {text}");

							while (content.generationConfig.TokenCount > tokenLimit)
							{
								if (content.contents.Count > 2)
								{
									//This isn't weird - we want to make sure we start from user message
									if (content.contents[0].role == Role.user)
									{
										content.contents.RemoveAt(0);
									}

									if (content.contents[0].role == Role.model)
									{
										content.contents.RemoveAt(0);
									}
								}
							}

							irc.KrakenConnection.UpdateRedemptionStatus(request.rewardId, new string[] { request.redemptionId }, RedemptionStates.FULFILLED);
							XML_Utils.Save(content.StoragePath, content);
						}
						else
						{
							content.contents.RemoveAt(content.contents.Count - 1);
							if (finishReason == "SAFETY")
								irc.SendChatMessage($"@{request.userName}: AI tripped a safety setting.");
							else
								irc.SendChatMessage($"@{request.userName}: AI couldn't deliver the answer - unhandled finish reason: {finishReason}");

							irc.KrakenConnection.UpdateRedemptionStatus(request.rewardId, new string[] { request.redemptionId }, RedemptionStates.CANCELED);
							XML_Utils.Save(content.StoragePath, content);
						}
					}
					else
					{
						irc.KrakenConnection.UpdateRedemptionStatus(request.rewardId, new string[] { request.redemptionId }, RedemptionStates.CANCELED);
						irc.SendChatMessage($"@{request.userName}: Failed to get a response. Please debug me, Sui :(");
					}
				}
			}
			catch (Exception ex)
			{
				MainForm.Instance.TwitchBot.Irc.SendChatMessage($"Failed to get a response. Something was written in log. Sui help! :(");
				MainForm.Instance.ThreadSafeAddPreviewText($"There was an error trying to do AI: {ex}", LineType.GeminiAI);
			}
		}

		private string ClearUpBackwardSlashCharacters(string text)
		{
			text = text.Replace("\\*", "*");
			text = text.Replace("\\_", "_");
			return text;
		}

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
