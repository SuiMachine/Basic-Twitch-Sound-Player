using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.Structs.Gemini;
using BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes;
using SuiBotAI.Components;
using SuiBotAI.Components.Other.Gemini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static SuiBot_TwitchSocket.API.EventSub.ES_ChannelPoints;
using static SuiBotAI.Components.SuiBotAIProcessor;

namespace BasicTwitchSoundPlayer
{
	public class GeminiAI
	{
		private SuiBotAIProcessor m_Processor;
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

			if (string.IsNullOrEmpty(aiConfig.TwitchUsername))
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Can't setup AI - no streamer username provided", LineType.GeminiAI);
				return false;
			}

			if (string.IsNullOrEmpty(aiConfig.TwitchAwardID))
			{
				MainForm.Instance.ThreadSafeAddPreviewText($"Can't setup AI - no {nameof(aiConfig.TwitchAwardID)} set up.", LineType.GeminiAI);
				return false;
			}

			var path = AIConfig.GetAIHistoryPath(aiConfig.TwitchUsername);
			StreamerContent = XML_Utils.Load(path, new GeminiContent()
			{
				contents = new List<GeminiMessage>(),
				generationConfig = new GeminiContent.GenerationConfig(),
				safetySettings = null,
			});
			m_Processor = new SuiBotAIProcessor(aiConfig.ApiKey, aiConfig.Model);

			return true;
		}

		public void Register()
		{
			IsRegistered = true;
			MainForm.Instance.TwitchEvents.OnChannelPointsRedeem += PointsRedeem;
		}

		public void Unregister()
		{
			IsRegistered = false;
		}

		public void PointsRedeem(ES_ChannelPointRedeemRequest request)
		{
			var rewardID = AIConfig.GetInstance().TwitchAwardID;
			if (string.IsNullOrEmpty(rewardID))
				return;
			if (request.reward.id != rewardID)
				return;

			Task.Run(async () =>
			{
				await GetResponse(request);
			});

		}

		private async Task GetResponse(ES_ChannelPointRedeemRequest request)
		{
			ChatBot bot = MainForm.Instance.TwitchBot;

			try
			{
				GeminiContent content = null;
				AIConfig aiConfig = AIConfig.GetInstance();
				ChannelInstance channelInstance = bot?.ChannelInstance;
				if (bot == null || bot.IsDisposed || channelInstance == null)
				{
					Logger.AddLine("Either bot or channel were already disposed.");
					return;
				}

				int tokenLimit = 1000;

				string path;
				GeminiMessage instructions = null;
				if (request.user_login == channelInstance.Channel)
				{
					path = AIConfig.GetAIHistoryPath(aiConfig.TwitchUsername);
					content = StreamerContent;

					tokenLimit = aiConfig.TokenLimit_Streamer;
					content.safetySettings = aiConfig.GetSafetySettingsStreamer();
					instructions = aiConfig.GetInstruction(request.user_name, true, true);
					content.generationConfig.temperature = aiConfig.Temperature_Streamer;
				}
				else
				{
					path = AIConfig.GetAIHistoryPath(request.user_id);

					if (!UserContents.TryGetValue(request.user_id, out content))
					{
						if (File.Exists(path))
						{
							content = XML_Utils.Load(path, new GeminiContent()
							{
								contents = new List<GeminiMessage>(),
								generationConfig = new GeminiContent.GenerationConfig(),
							});
						}
						else
						{
							content = new GeminiContent()
							{
								contents = new List<GeminiMessage>(),
								generationConfig = new GeminiContent.GenerationConfig(),
							};
						}

						UserContents.Add(request.user_id, content);
					}

					tokenLimit = aiConfig.TokenLimit_User;
					GeminiCharacterOverride overrides = GeminiCharacterOverride.GetOverride(GeminiCharacterOverride.GetOverridePath(request.user_login));
					if (overrides != null)
					{
						//content.systemInstruction = 
						content.safetySettings = overrides.GetSafetyOverrides();
						instructions = overrides.GetFullInstruction(aiConfig);
					}
					else
					{
						content.safetySettings = aiConfig.GetSafetySettingsGeneral();
						instructions = aiConfig.GetInstruction(request.user_name, false, channelInstance.StreamStatus?.IsOnline ?? false);
					}
					content.generationConfig.temperature = aiConfig.Temperature_User;
					content.tools = new List<GeminiTools>()
					{
						new GeminiTools()
						{
							functionDeclarations = new List<GeminiTools.GeminiFunction>()
							{
								GeminiFunctionCall.CreateTimeoutFunction(),
								GeminiFunctionCall.CreateBanFunction()
							}
						}
					};
				}

				if (content == null)
				{
					bot?.ChannelInstance?.SendChatMessage($"{request.user_name}: Failed to load user history.");
					bot?.HelixAPI_User?.UpdateRedemptionStatus(request, RedemptionStates.CANCELED);
					return;
				}
				var result = await m_Processor.GetAIResponse(content, instructions, request.user_input);

				if (result == null)
				{
					bot?.ChannelInstance.SendChatMessage($"{request.user_name} - Failed to get a response. Please debug me :(");
					bot?.HelixAPI_User.UpdateRedemptionStatus(request, RedemptionStates.CANCELED);
					return;
				}
				else
				{
					content.generationConfig.TokenCount = result.usageMetadata.totalTokenCount;

					var lastResponse = result.candidates.Last().content;
					content.contents.Add(lastResponse);
					var text = lastResponse.parts.Last().text;
					bot?.HelixAPI_User.UpdateRedemptionStatus(request, RedemptionStates.FULFILLED);

					if (text != null)
					{
						SuiBotAIProcessor.CleanupResponse(ref text);

						channelInstance?.SendChatMessage($"{request.user_name}: {text}");

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

						XML_Utils.Save(path, content);
					}

					var func = lastResponse.parts.Last().functionCall;
					if (func != null)
					{
						HandleChatFunctionCall(channelInstance, request, func);
					}
				}
			}
			catch(SafetyFilterTrippedException ex)
			{
				MainForm.Instance.TwitchBot.ChannelInstance.SendChatMessage($"Failed to get a response. Safety filter tripped!");
				MainForm.Instance.ThreadSafeAddPreviewText($"Safety was tripped {ex}", LineType.GeminiAI);
				bot?.HelixAPI_User.UpdateRedemptionStatus(request, RedemptionStates.CANCELED);
			}
			catch (Exception ex)
			{
				MainForm.Instance.TwitchBot.ChannelInstance.SendChatMessage($"Failed to get a response. Something was written in log. Help! :(");
				MainForm.Instance.ThreadSafeAddPreviewText($"There was an error trying to do AI: {ex}", LineType.GeminiAI);
			}
		}

		private void HandleChatFunctionCall(ChannelInstance channelInstance, ES_ChannelPointRedeemRequest message, GeminiResponseFunctionCall func)
		{
			if (func.name == "timeout")
				func.args.ToObject<TimeOutUser>().Perform(channelInstance, message);
			else if (func.name == "ban")
				func.args.ToObject<BanUser>().Perform(channelInstance, message);
		}
	}
}
