using BasicTwitchSoundPlayer.SoundDatabaseEditor;
using BasicTwitchSoundPlayer.SoundStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuiBot_TwitchSocket.API.EventSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.IRC
{
	public class KrakenConnections
	{

		public List<ES_ChannelPoints.ChannelReward> CachedRewards { get; internal set; }


/*		public async Task VerifyChannelRewardsAsync(MainForm mainForm, string soundRedeemId)
		{
			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if (BroadcasterID == null || BroadcasterID == "")
			{
				mainForm.ThreadSafeAddPreviewText("[ERROR] No broadcaster ID to verify Channel Rewards!", LineType.IrcCommand);
				return;
			}

			DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Getting custom rewards list");
			string response = await HTTPS_Requests.GetAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, new Dictionary<string, string>()
			{
				{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
				{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
			});

			if (response == null || response == "")
			{
				mainForm.ThreadSafeAddPreviewText("[ERROR] Incorrect response when verifying Channel Rewards!", LineType.IrcCommand);
				return;
			}
			else
			{
				JObject jReader = JObject.Parse(response);
				if (jReader["data"] != null)
				{
					ChannelReward soundRedeemReward = null;

					var dataNode = jReader["data"];
					foreach (var customReward in dataNode)
					{
						var parseNode = customReward.ToObject<ChannelReward>();
						if (soundRedeemId != null && parseNode.id.Equals(soundRedeemId, StringComparison.InvariantCultureIgnoreCase))
							soundRedeemReward = parseNode;
					}

					if (soundRedeemId != null && soundRedeemId != "")
					{
						if (soundRedeemReward == null)
						{
							mainForm.ThreadSafeAddPreviewText("[ERROR] Haven't found Channel Reward with Sound Reward ID!", LineType.IrcCommand);
						}
						else
						{
							if (!soundRedeemReward.is_enabled)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward is not enabled!", LineType.IrcCommand);
							else if (soundRedeemReward.is_paused)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward redemption is paused!", LineType.IrcCommand);
							else if (!soundRedeemReward.is_user_input_required)
								mainForm.ThreadSafeAddPreviewText("[ERROR] Sound reward is incorrectly configured - it needs to require player input!", LineType.IrcCommand);
							else if (soundRedeemReward.should_redemptions_skip_request_queue)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound redemption via points shouldn't skip request queue to allow for returning points, if request fails!", LineType.IrcCommand);
						}
					}

					//Clear up redemption queue
					if (soundRedeemReward != null)
					{
						if (soundRedeemReward != null)
						{
							DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Clearing up redeem queue");
							string pointsRewardResponse = await HTTPS_Requests.GetAsync(HELIXURI, "channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID + "&reward_id=" + soundRedeemReward.id + "&status=UNFULFILLED", new Dictionary<string, string>()
							{
								{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
								{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
							}, true);

							//Should have thought this through...
							if (pointsRewardResponse.StartsWith("Error: "))
							{
								mainForm.ThreadSafeAddPreviewText("[ERROR] Error verifying Sound award. It has ID, but bot doesn't have access to it. Maybe it was defined by a user instead of bot?", LineType.IrcCommand);
								mainForm.ThreadSafeAddPreviewText("[ERROR] " + pointsRewardResponse, LineType.IrcCommand);
							}
							else
							{
								jReader = JObject.Parse(pointsRewardResponse);
								dataNode = jReader["data"];

								mainForm.ThreadSafeAddPreviewText("[Verification Status] Sound reward seems OK." + (dataNode.Count() > 0 ? " Clearing up request queue!" : " Request queue empty."), LineType.IrcCommand);

								int totalIDsToCancel = dataNode.Count() > 50 ? 50 : dataNode.Count();
								if (totalIDsToCancel > 0)
								{
									var idsToCancel = dataNode.Take(totalIDsToCancel).Select(x => x["id"].ToString()).ToArray();

									UpdateRedemptionStatus(soundRedeemReward.id, idsToCancel, RedemptionStates.CANCELED);
								}
							}
						}
					}
				}
			}
		}

		public async Task<SuiBot_Core.API.EventSub.ES_ChannelPoints.ChannelReward[]> GetUnredeemedRewardsForUser(MainForm mainFormReference, string rewardID, string userID)
		{
			if (BroadcasterID == null | BroadcasterID == "")
			{
				mainFormReference.ThreadSafeAddPreviewText("Broadcaster ID is null or empty! Something is really wrong.", LineType.IrcCommand);
				return null;
			}

			DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Getting unredeemed rewards");
			var response = await HTTPS_Requests.GetAsync(HELIXURI, "channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID + "&reward_id=" + rewardID + "&status=UNFULFILLED&sort=NEWEST", new Dictionary<string, string>()
			{
				{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
				{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
			}, true);

			if (response != null && response != "")
			{
				if (response.StartsWith("Error:"))
				{
					mainFormReference.ThreadSafeAddPreviewText("Error getting redeemed rewards for user!", LineType.IrcCommand);
					mainFormReference.ThreadSafeAddPreviewText(response, LineType.IrcCommand);
				}
				else
				{
					var jsonParse = JObject.Parse(response);
					var dataNode = jsonParse["data"];
					int totalIDsToCancel = dataNode.Count() > 50 ? 50 : dataNode.Count();
					if (totalIDsToCancel > 0)
					{
						var rewardsToUpdate = dataNode.Take(totalIDsToCancel).Where(x => x["user_id"].ToString() == userID).Select(y => y.ToObject<ChannelReward>()).ToArray();
						return rewardsToUpdate;
					}
					return null;
				}
			}

			return null;
		}

		public async void UpdateRedemptionStatus(string RewardTypeID, string[] RewardRequestIDs, RedemptionStates redemptionState)
		{
			if (BroadcasterID == null | BroadcasterID == "")
			{
				MainForm.Instance.ThreadSafeAddPreviewText("Broadcaster ID is null or empty! Something is really wrong.", LineType.IrcCommand);
				return;
			}

			string reformatIDs = "";

			for (int i = 0; i < RewardRequestIDs.Length; i++)
			{
				if (i > 0)
					reformatIDs += "&";
				reformatIDs += "id=" + RewardRequestIDs[i];
			}

			if (PrivateSettings.GetInstance().Debug_mode)
				MainForm.Instance.ThreadSafeAddPreviewText($"Updating reward {string.Join(", ", RewardRequestIDs)} with status {redemptionState}", LineType.IrcCommand);
			var result = await HTTPS_Requests.PatchAsync(HELIXURI, "channel_points/custom_rewards/redemptions", $"?broadcaster_id={BroadcasterID}&reward_id={RewardTypeID}&{reformatIDs}", ConvertDictionaryToJsonString(new Dictionary<string, string>() { { "status", redemptionState.ToString() } }),
				new Dictionary<string, string>()
				{
					{"Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
					{"Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
				});

			if (result != "")
			{
				if (PrivateSettings.GetInstance().Debug_mode)
				{

				}
			}
		}

		private string ConvertDictionaryToJsonString(Dictionary<string, string> bodyContent)
		{
			string jsonContent = "{";
			for (int i = 0; i < bodyContent.Count; i++)
			{
				var keyPair = bodyContent.ElementAt(i);
				jsonContent += $"\"{keyPair.Key}\":\"{keyPair.Value}\"";
				if (i + 1 < bodyContent.Count)
					jsonContent += ",\n";

			}
			jsonContent += "}";
			return jsonContent;
		}

		internal async Task<ChannelReward> CreateOrUpdateReward(VoiceModConfig.VoiceModReward reward)
		{
			if (CachedRewards == null)
				return null;

			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if ((BroadcasterID == null || BroadcasterID == ""))
				return null;

			ChannelReward currentReward = CachedRewards.Find(x => x.id == reward.RewardID); ;

			if (currentReward != null)
			{
				var newReward = new ChannelReward()
				{
					title = reward.RewardTitle,
					prompt = reward.RewardDescription,
					id = reward.RewardID,
					is_enabled = reward.Enabled,
					is_paused = false,
					cost = reward.RewardCost,
					is_user_input_required = false,
					should_redemptions_skip_request_queue = false,

					global_cooldown_setting = new ChannelReward.Global_Cooldown_Setting()
					{
						is_enabled = true,
						global_cooldown_seconds = reward.RewardCooldown * 60,
					}
				};

				if (ChannelReward.Differs(currentReward, newReward))
				{
					DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Updating reward");
					var json = JsonConvert.SerializeObject(newReward);

					var response = await HTTPS_Requests.PatchAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, json, new Dictionary<string, string>()
					{
						{"Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
						{"Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
					});
					if (response != "")
					{
						JObject jReader = JObject.Parse(response);
						var dataNode = jReader["data"].First;
						if (dataNode["id"] != null)
						{
							newReward = dataNode.ToObject<ChannelReward>();
							return newReward;
						}
					}
					return newReward;
				}
				return newReward;
			}
			else
			{
				var jObject = new JObject()
				{
					["title"] = reward.RewardTitle,
					["cost"] = reward.RewardCost,
					["is_enabled"] = reward.Enabled.ToString().ToLower(),
					["prompt"] = reward.RewardDescription,
					["is_user_input_required"] = "false",
					["should_redemptions_skip_request_queue"] = "false",
					["global_cooldown_setting"] = new JObject()
					{
						["is_global_cooldown_enabled"] = "true",
						["global_cooldown_seconds"] = reward.RewardCooldown * 60
					}
				}.ToString();


				DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Creating reward");

				string response = await HTTPS_Requests.PostAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, new Dictionary<string, string>()
				{
					{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
					{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
				});

				if (response != "")
				{
					JObject jReader = JObject.Parse(response);
					var dataNode = jReader["data"].First;
					if (dataNode["id"] != null)
					{
						var newReward = dataNode.ToObject<ChannelReward>();
						return newReward;
					}
				}
			}
			return null;
		}

		internal async Task<ChannelReward> CreateOrUpdateReward(SoundEntry soundEntry)
		{

			if (CachedRewards == null)
				return null;

			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if ((BroadcasterID == null || BroadcasterID == ""))
				return null;

			ChannelReward currentReward = CachedRewards.Find(x => x.id == soundEntry.RewardID); ;

			if (currentReward != null)
			{
				ChannelRewardRequest newReward;
				if (soundEntry.Cooldown > 0)
				{
					newReward = new ChannelRewardRequest()
					{
						title = soundEntry.RewardName,
						prompt = soundEntry.Description,
						id = soundEntry.RewardID,
						is_enabled = true,
						is_paused = false,
						cost = soundEntry.AmountOfPoints,
						is_user_input_required = false,
						should_redemptions_skip_request_queue = false,
						is_global_cooldown_enabled = true,
						global_cooldown_seconds = soundEntry.Cooldown
					};
				}
				else
				{
					newReward = new ChannelRewardRequest()
					{
						title = soundEntry.RewardName,
						prompt = soundEntry.Description,
						id = soundEntry.RewardID,
						is_enabled = true,
						is_paused = false,
						cost = soundEntry.AmountOfPoints,
						is_user_input_required = false,
						should_redemptions_skip_request_queue = false,
						is_global_cooldown_enabled = false
					};
				}

				ChannelReward result = null;
				if (ChannelRewardRequest.Differs(newReward, currentReward))
				{
					DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Updating reward");

					var json = JsonConvert.SerializeObject(newReward);
					var response = await HTTPS_Requests.PatchAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, json, new Dictionary<string, string>()
					{
						{"Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
						{"Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}"  }
					});
					if (response != "")
					{
						JObject jReader = JObject.Parse(response);
						var dataNode = jReader["data"].First;
						if (dataNode["id"] != null)
							result = dataNode.ToObject<ChannelReward>();
					}
					return result;
				}
				return result;
			}
			else
			{
				string jObject;
				if (soundEntry.Cooldown > 0)
				{
					jObject = new JObject()
					{
						["title"] = soundEntry.RewardName,
						["cost"] = soundEntry.AmountOfPoints,
						["is_enabled"] = true.ToString().ToLower(),
						["prompt"] = soundEntry.Description,
						["is_user_input_required"] = "false",
						["should_redemptions_skip_request_queue"] = "false",
						["is_global_cooldown_enabled"] = "true",
						["global_cooldown_seconds"] = soundEntry.Cooldown.ToString(),
					}.ToString();
				}
				else
				{
					jObject = new JObject()
					{
						["title"] = soundEntry.RewardName,
						["cost"] = soundEntry.AmountOfPoints,
						["is_enabled"] = true.ToString().ToLower(),
						["prompt"] = soundEntry.Description,
						["is_user_input_required"] = "false",
						["should_redemptions_skip_request_queue"] = "false",
						["is_global_cooldown_enabled"] = "false"
					}.ToString();
				}

				DialogBoxes.ProgressDisplay.Instance?.SetProgressText("Creating reward");

				string response = await HTTPS_Requests.PostAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, new Dictionary<string, string>()
				{
					{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
					{"Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
				});
				if (response != "")
				{
					JObject jReader = JObject.Parse(response);
					var dataNode = jReader["data"].First;
					if (dataNode["id"] != null)
					{
						var newReward = dataNode.ToObject<ChannelReward>();
						return newReward;
					}
				}
			}
			return null;
		}

		internal async Task<ChannelReward> CreateOrUpdateReward(string name, string prompt, int cost, bool isUserInputRequired, int globalCooldown, string rewardID = "")
		{
			if (CachedRewards == null)
				return null;

			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if ((BroadcasterID == null || BroadcasterID == ""))
				return null;

			ChannelReward currentReward = CachedRewards.Find(x => x.id == rewardID); ;

			if (currentReward != null)
			{
				return currentReward;
			}
			else
			{
				string jObject = new JObject()
				{
					["title"] = name,
					["cost"] = cost,
					["is_enabled"] = true.ToString().ToLower(),
					["prompt"] = prompt,
					["is_user_input_required"] = isUserInputRequired.ToString().ToLower(),
					["should_redemptions_skip_request_queue"] = "false",
					["is_global_cooldown_enabled"] = globalCooldown > 0 ? "true" : "false",
					["global_cooldown_seconds"] = globalCooldown,
				}.ToString();

				string response = await HTTPS_Requests.PostAsync(HELIXURI, "channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, new Dictionary<string, string>()
				{
					{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
					{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
				});
				if (response != "")
				{
					JObject jReader = JObject.Parse(response);
					var dataNode = jReader["data"].First;
					if (dataNode["id"] != null)
					{
						var newReward = dataNode.ToObject<ChannelReward>();
						return newReward;
					}
				}
			}
			return null;
		}

		

		#endregion

		public async Task<List<ChannelReward>> GetRewardsList()
		{
			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if (BroadcasterID == null || BroadcasterID == "")
			{
				MainForm.Instance.ThreadSafeAddPreviewText("[ERROR] No broadcaster ID to verify VoiceMod Rewards!", LineType.IrcCommand);
				return null;
			}

			string response = await HTTPS_Requests.GetAsync(HELIXURI, "channel_points/custom_rewards", $"?broadcaster_id={BroadcasterID}", new Dictionary<string, string>()
			{
				{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
				{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
			});
			if (response == null || response == "")
			{
				MainForm.Instance.ThreadSafeAddPreviewText("[ERROR] Incorrect response when verifying Channel Rewards!", LineType.IrcCommand);
				return null;
			}

			List<ChannelReward> list = new List<ChannelReward>();
			JObject jReader = JObject.Parse(response);
			if (jReader["data"] != null)
			{
				var dataNode = jReader["data"];
				foreach (var customReward in dataNode)
				{
					ChannelReward parseNode = customReward.ToObject<ChannelReward>();
					list.Add(parseNode);
				}
			}

			foreach (VoiceModConfig.VoiceModReward voiceModReward in VoiceModConfig.GetInstance().Rewards)
			{
				if (!list.Any(x => x.id == voiceModReward.RewardID))
				{
					voiceModReward.RewardID = "";
					voiceModReward.IsSetup = false;
				}
			}

			if (DB_Editor.Instance != null)
			{
				foreach (SoundEntry soundReward in DB_Editor.Instance.SoundsCopy)
				{
					if (!list.Any(x => x.id == soundReward.RewardID))
					{
						soundReward.RewardID = "";
					}
				}
			}

			if (SoundDatabaseEditor.EditDialogues.AddEditNewEntryDialog.Instance != null)
			{
				if (!list.Any(x => x.id == SoundDatabaseEditor.EditDialogues.AddEditNewEntryDialog.Instance.TB_RewardID.Text))
				{
					SoundDatabaseEditor.EditDialogues.AddEditNewEntryDialog.Instance.TB_RewardID.Text = "";
				}
			}

			CachedRewards = list;
			return list;

		}

		public async Task<bool> DeleteCustomReward(ChannelReward reward)
		{
			if (CachedRewards == null)
				return false;

			{
				int endTimer = 5;
				while ((BroadcasterID == null || BroadcasterID == "") && endTimer >= 0)
				{
					await Task.Delay(1000);
					endTimer--;
				}
			}

			if ((BroadcasterID == null || BroadcasterID == ""))
				return false;

			HttpStatusCode response = await HTTPS_Requests.DeleteAsync(HELIXURI, "channel_points/custom_rewards", $"?broadcaster_id={BroadcasterID}&id={reward.id}", new Dictionary<string, string>()
			{
				{ "Client-ID", BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID },
				{ "Authorization", $"Bearer {PrivateSettings.GetInstance().UserAuth}" }
			});
			if (response == HttpStatusCode.OK || response == HttpStatusCode.NoContent)
			{
				return true;
			}
			return false;
		}*/
	}
}
