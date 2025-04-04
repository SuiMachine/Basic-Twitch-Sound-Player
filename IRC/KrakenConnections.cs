using BasicTwitchSoundPlayer.SoundDatabaseEditor;
using BasicTwitchSoundPlayer.SoundStorage;
using BasicTwitchSoundPlayer.Structs.Gemini;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;

namespace BasicTwitchSoundPlayer.IRC
{
	public class KrakenConnections
	{
		public enum RewardType
		{
			Sound,
		}

		public enum RedemptionStates
		{
			UNFULFILLED,
			FULFILLED,
			CANCELED
		}

		public class ChannelPointRedeemRequest
		{
			public string userName;
			public string userId;
			public string rewardId;
			public string redemptionId;
			public RedemptionStates state;
			public string userInput;

			public ChannelPointRedeemRequest(string userName, string userId, string rewardId, string redemptionId, RedemptionStates state, string userInput)
			{
				this.userName = userName;
				this.userId = userId;
				this.rewardId = rewardId;
				this.redemptionId = redemptionId;
				this.state = state;
				this.userInput = userInput;
			}
		}

		[Serializable]
		public class ChannelReward
		{
			[Serializable]
			public class Global_Cooldown_Setting
			{
				public bool is_enabled;
				public int global_cooldown_seconds;
			}

			public string id = "";
			public bool is_enabled = false;
			public int cost = 0;
			public string title = "";
			public string prompt = "";
			public bool is_user_input_required = false;
			public bool is_paused = false;
			public bool should_redemptions_skip_request_queue = false;
			public Global_Cooldown_Setting global_cooldown_setting;

			public static bool Differs(ChannelReward l, ChannelReward r)
			{
				return l.id != r.id
					|| l.is_enabled != r.is_enabled
					|| l.cost != r.cost
					|| l.title != r.title
					|| l.prompt != r.prompt
					|| l.is_user_input_required != r.is_user_input_required
					|| l.is_paused != r.is_paused
					|| l.should_redemptions_skip_request_queue != r.should_redemptions_skip_request_queue
					|| l.global_cooldown_setting.is_enabled != r.global_cooldown_setting.is_enabled
					|| l.global_cooldown_setting.global_cooldown_seconds != r.global_cooldown_setting.global_cooldown_seconds;
			}
		}

		//Because Twitch API is a bit of a mess
		[Serializable]
		public class ChannelRewardRequest
		{
			public string id = "";
			public bool is_enabled = false;
			public int cost = 0;
			public string title = "";
			public string prompt = "";
			public bool is_user_input_required = false;
			public bool is_paused = false;
			public bool should_redemptions_skip_request_queue = false;
			public bool is_global_cooldown_enabled = false;
			public int global_cooldown_seconds = 0;

			public static bool Differs(ChannelRewardRequest l, ChannelReward r)
			{
				return l.id != r.id
					|| l.is_enabled != r.is_enabled
					|| l.cost != r.cost
					|| l.title != r.title
					|| l.prompt != r.prompt
					|| l.is_user_input_required != r.is_user_input_required
					|| l.is_paused != r.is_paused
					|| l.should_redemptions_skip_request_queue != r.should_redemptions_skip_request_queue
					|| l.is_global_cooldown_enabled != r.global_cooldown_setting.is_enabled
					|| l.global_cooldown_seconds != r.global_cooldown_setting.global_cooldown_seconds;
			}
		}


		private string HELIXURI { get; set; }
		private string Channel { get; set; }
		public string BroadcasterID { get; set; }
		public bool IsLive { get; private set; } = false;
		public string GameID { get; private set; } = "";
		public string GameTitle { get; private set; } = "";
		public string StreamTitle { get; private set; } = "";

		public Task SubscribingToEvents { get; internal set; }
		public List<ChannelReward> CachedRewards { get; internal set; }

		private const string BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID = "9z58zy6ak0ejk9lme6dy6nyugydaes";

		public KrakenConnections(string Channel)
		{
			this.Channel = Channel;
			HELIXURI = "https://api.twitch.tv/helix/";
		}

		#region Async
		public async Task GetBroadcasterIDAsync()
		{
			string responseID = await GetNewUpdateAsync("users", "?login=" + Channel, true);
			if (responseID == null || responseID == "")
				return;
			else
			{
				JObject jReader = JObject.Parse(responseID);
				if (jReader["data"] != null)
				{
					var dataNode = jReader["data"];
					var firstDataChild = dataNode.First;
					if (firstDataChild["id"] != null)
					{
						BroadcasterID = firstDataChild["id"].ToString();
					}
					else
						return;
				}
				else
					return;
			}
		}

		public async Task<string[]> GetSubscribersAsync()
		{
			if (BroadcasterID == null || BroadcasterID == "")
			{
				await GetBroadcasterIDAsync();
			}
			if (BroadcasterID == null || BroadcasterID == "")
				throw new Exception("Didn't obtain broadcaster ID. Can't proceed!");

			string response = await GetNewUpdateAsync("subscriptions", "?broadcaster_id=" + BroadcasterID, true);
			if (response == null || response == "")
			{
				return new string[0];
			}
			else
			{
				JObject jReader = JObject.Parse(response);
				if (jReader["data"] != null)
				{
					var dataNode = jReader["data"];

					List<string> subscribers = new List<string>();

					foreach (var sub in dataNode)
					{
						if (sub["user_name"] != null)
						{
							var curSub = sub["user_name"].ToString();
							if (!subscribers.Contains(curSub))
								subscribers.Add(curSub);
						}
					}
					return subscribers.ToArray();
				}
			}

			return new string[0];
		}

		public async Task VerifyChannelRewardsAsync(MainForm mainForm, string soundredemptionid)
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

			string response = await GetNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, true);
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
					ChannelReward soundredemptionReward = null;

					var dataNode = jReader["data"];
					foreach (var customReward in dataNode)
					{
						var parseNode = customReward.ToObject<ChannelReward>();
						if (soundredemptionid != null && parseNode.id.Equals(soundredemptionid, StringComparison.InvariantCultureIgnoreCase))
							soundredemptionReward = parseNode;
					}

					if (soundredemptionid != null && soundredemptionid != "")
					{
						if (soundredemptionReward == null)
						{
							mainForm.ThreadSafeAddPreviewText("[ERROR] Haven't found Channel Reward with Sound Reward ID!", LineType.IrcCommand);
						}
						else
						{
							if (!soundredemptionReward.is_enabled)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward is not enabled!", LineType.IrcCommand);
							else if (soundredemptionReward.is_paused)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound reward redemption is paused!", LineType.IrcCommand);
							else if (!soundredemptionReward.is_user_input_required)
								mainForm.ThreadSafeAddPreviewText("[ERROR] Sound reward is incorrectly configured - it needs to require player input!", LineType.IrcCommand);
							else if (soundredemptionReward.should_redemptions_skip_request_queue)
								mainForm.ThreadSafeAddPreviewText("[WARNING] Sound redemption via points shouldn't skip request queue to allow for returning points, if request fails!", LineType.IrcCommand);
						}
					}

					//Clear up redemption queue
					if (soundredemptionReward != null)
					{
						if (soundredemptionReward != null)
						{
							string pointsRewardResponse = await GetNewUpdateAsync("channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID + "&reward_id=" + soundredemptionReward.id + "&status=UNFULFILLED", true, true);


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

									UpdateRedemptionStatus(soundredemptionReward.id, idsToCancel, RedemptionStates.CANCELED);
								}
							}
						}
					}
				}
			}
		}

		public async Task<ChannelReward[]> GetUnredeemedRewardsForUser(MainForm mainFormReference, string rewardID, string userID)
		{
			if (BroadcasterID == null | BroadcasterID == "")
			{
				mainFormReference.ThreadSafeAddPreviewText("Broadcaster ID is null or empty! Something is really wrong.", LineType.IrcCommand);
				return null;
			}

			var response = await GetNewUpdateAsync("channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID + "&reward_id=" + rewardID + "&status=UNFULFILLED&sort=NEWEST", true, true);

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
			var result = await PatchNewUpdateAsync("channel_points/custom_rewards/redemptions", "?broadcaster_id=" + BroadcasterID + "&reward_id=" + RewardTypeID + "&" + reformatIDs, ConvertDictionaryToJsonString(new Dictionary<string, string>() { { "status", redemptionState.ToString() } }), true);

			if (result != "")
			{
				if (PrivateSettings.GetInstance().Debug_mode)
				{

				}
			}
		}

		public async Task<ChannelReward> CreateOrUpdateReward(VoiceModConfig.VoiceModReward reward)
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
					var json = JsonConvert.SerializeObject(newReward);

					var response = await PatchNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, json, true);
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


				var response = await PostNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, true);
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
					var json = JsonConvert.SerializeObject(newReward);

					var response = await PatchNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, json, true);
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

				var response = await PostNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, true);
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

		internal async Task<ChannelReward> CreateOrUpdateReward(string rewardID = "")
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
					["title"] = "Ask AI",
					["cost"] = 10_000,
					["is_enabled"] = true.ToString().ToLower(),
					["prompt"] = "Ask or write message to AI",
					["is_user_input_required"] = "true",
					["should_redemptions_skip_request_queue"] = "false",
					["is_global_cooldown_enabled"] = "true",
					["global_cooldown_seconds"] = "600",
				}.ToString();

				var response = await PostNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, jObject, true);
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

		private async Task<string> GetNewUpdateAsync(string scope, string parameters = "", bool RequireBearerToken = false, bool returnErrorCode = false)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HELIXURI + scope + parameters);

			try
			{
				request.Headers["Client-ID"] = BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID;
				if (!RequireBearerToken)
					request.Headers["Authorization"] = "OAuth " + PrivateSettings.GetInstance().UserAuth;
				else
					request.Headers["Authorization"] = "Bearer " + PrivateSettings.GetInstance().UserAuth;

				request.Timeout = 5000;
				request.Method = "GET";


				using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream))
				{
					return await reader.ReadToEndAsync();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				if (returnErrorCode)
				{
					return "Error: " + e.Message;
				}
				return "";
			}


		}

		private async Task<string> PostNewUpdateAsync(string scope, string parameters, string jsonContent, bool RequireBearerToken = false)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HELIXURI + scope + parameters);
			request.ContentType = "application/json";
			request.Method = "POST";

			try
			{
				request.Headers["Client-ID"] = BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID;
				if (!RequireBearerToken)
					request.Headers["Authorization"] = "OAuth " + PrivateSettings.GetInstance().UserAuth;
				else
					request.Headers["Authorization"] = "Bearer " + PrivateSettings.GetInstance().UserAuth;

				using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
				{
					streamWriter.Write(jsonContent);
				}

				var httpResponse = (HttpWebResponse)await request.GetResponseAsync();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return result;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				MessageBox.Show(e.Message);
				return "";
			}
		}
		private async Task<string> PatchNewUpdateAsync(string scope, string parameters, string jsonContent, bool RequireBearerToken = false)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HELIXURI + scope + parameters);
			request.ContentType = "application/json";
			request.Method = "PATCH";

			try
			{
				request.Headers["Client-ID"] = BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID;
				if (!RequireBearerToken)
					request.Headers["Authorization"] = "OAuth " + PrivateSettings.GetInstance().UserAuth;
				else
					request.Headers["Authorization"] = "Bearer " + PrivateSettings.GetInstance().UserAuth;

				using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
				{
					streamWriter.Write(jsonContent);
				}

				var httpResponse = (HttpWebResponse)await request.GetResponseAsync();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return result;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				MainForm.Instance.ThreadSafeAddPreviewText($"Error with patch request: {e.Message}", LineType.IrcCommand);
				return "";
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

			string response = await GetNewUpdateAsync("channel_points/custom_rewards", "?broadcaster_id=" + BroadcasterID, true);
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

		public async Task GetStreamerStatus()
		{
			string res = await GetNewUpdateAsync("streams", "?user_login=" + Channel, true);
			if(!string.IsNullOrEmpty(res))
			{
				try
				{
					var response = JObject.Parse(res);
					if (response["data"] != null && response["data"].Children().Count() > 0)
					{
						var dataNode = response["data"].First;
						if (dataNode["title"] != null)
						{
							this.IsLive = true;

							if (dataNode["type"] != null)
							{
								var streamType = dataNode["type"].ToString();
								if (streamType == "live")
								{
									this.IsLive = true;
									this.StreamTitle = dataNode["title"].ToString();
								}
								else
								{
									this.IsLive = false;
									this.StreamTitle = "";
									this.GameID = "";
									this.GameTitle = "";
									MainForm.Instance.ThreadSafeAddPreviewText($"{Channel} - Checked stream status. Is offline.", LineType.IrcCommand);
								}
							}

							if (dataNode["game_id"] != null)
							{
								string newGameId = dataNode["game_id"].ToString();
								if(newGameId != GameID)
								{
									GameTitle = await GetGameTitleFromID(newGameId);
									if(GameTitle == "ul")
									{
										GameTitle = "";
									}
									GameID = newGameId;
								}

								MainForm.Instance.ThreadSafeAddPreviewText($"{Channel} - Checked stream status. Is online, streaming {GameTitle}.", LineType.IrcCommand);
								return;
							}
							else
							{
								MainForm.Instance.ThreadSafeAddPreviewText($"{Channel} - Checked stream status. Is offline.", LineType.IrcCommand);
							}
						}
					}

					this.IsLive = false;
					this.GameID = "";
					this.GameTitle = "";
					MainForm.Instance.ThreadSafeAddPreviewText($"{Channel} - Checked stream status. Is offline.", LineType.IrcCommand);
				}
				catch (Exception e)
				{
					MainForm.Instance.ThreadSafeAddPreviewText("Error trying to parse Json when doing stream update request: " + e.Message, LineType.IrcCommand);
					this.IsLive = false;
					this.GameID = "";
					this.GameTitle = "";
				}
			}
		}

		private async Task<string> GetGameTitleFromID(string newGameId)
		{
			string res = await GetNewUpdateAsync("games", "?id=" + newGameId, true);
			if (string.IsNullOrEmpty(res))
				return "";

			JObject jObjectNode = JObject.Parse(res);
			JToken dataNode = jObjectNode["data"].First;
			if (dataNode["name"] != null)
			{
				return dataNode["name"].ToString();
			}
			return "";
		}
	}
}
