using SuiBot_TwitchSocket.API.EventSub;
using System;

namespace SSC
{
	public class EventBridge
	{
		public Action<ES_ChatMessage> OnChannelMessage;
		public Action<ES_ChannelPoints.ES_ChannelPointRedeemRequest> OnChannelPointsRedeem;
		public Action<ES_ChannelGoal> OnChannelGoalAchieved;
		public Action<ES_AdBreakBeginNotification> OnAdBreakStarted;
		public Action<ES_AdBreakBeginNotification, int> OnAdBreakFinished;
		public Action OnAdPrerollsActive;
		public Action<ES_ChannelRaid> OnChannelRaid;

		internal void Clear()
		{
			OnChannelMessage = null;
			OnChannelPointsRedeem = null;
			OnChannelGoalAchieved = null;
			OnAdBreakStarted = null;
			OnAdBreakFinished = null;
			OnAdPrerollsActive = null;
			OnChannelRaid = null;
		}
	}
}
