using Newtonsoft.Json;
using SuiBot_TwitchSocket;
using SuiBot_TwitchSocket.API.EventSub;
using System.Threading.Tasks;

namespace BasicTwitchSoundPlayer.MixItUpBridge
{
	public class MixItUp
	{
		internal void Register()
		{
			if (MainForm.Instance != null && MainForm.Instance.TwitchEvents != null)
				MainForm.Instance.TwitchEvents.ChannelGoalAchieved += GoalReached;
		}

		internal void Unregister()
		{
			if (MainForm.Instance != null && MainForm.Instance.TwitchEvents != null)
				MainForm.Instance.TwitchEvents.ChannelGoalAchieved -= GoalReached;
		}

		public void BTSP_RewardsStatusChanged(bool allPaused, bool soundRewardsPaused, bool voiceRewardsPaused)
		{
			var url = PrivateSettings.GetInstance().MixItUpWebookURL;
			if (string.IsNullOrEmpty(url))
				return;

			var l = new MixItUpWebHookRequests_SoundRewardsStatus()
			{
				SoundRewards = !soundRewardsPaused && !allPaused,
				VoiceRewards = !voiceRewardsPaused && !allPaused,
			};
			var serialize = JsonConvert.SerializeObject(l);

			Task.Run(async () =>
			{
				var result = await HttpWebRequestHandlers.PerformPostAsync(url, "", "", serialize, new System.Collections.Generic.Dictionary<string, string>());

			});
		}

		private void GoalReached(ES_ChannelGoal goal)
		{
			var url = PrivateSettings.GetInstance().MixItUpWebookURL;
			if (string.IsNullOrEmpty(url))
				return;

			var l = new MixItUpWebHookRequests_ChannelGoalReached();
			var serialize = JsonConvert.SerializeObject(l);

			Task.Run(async () =>
			{
				var result = await HttpWebRequestHandlers.PerformPostAsync(url, "", "", serialize, new System.Collections.Generic.Dictionary<string, string>());
			});
		}
	}
}
