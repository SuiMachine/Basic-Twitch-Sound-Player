using SuiBot_TwitchSocket.API.EventSub;
using System;

namespace BasicTwitchSoundPlayer
{
	public class EventBridge
	{
		public Action<ES_ChatMessage> OnChannelMessage;
		public Action<ES_ChannelPoints.ES_ChannelPointRedeemRequest> OnChannelPointsRedeem;
		public Action<ES_ChannelGoal> ChannelGoalAchieved;

		internal void Clear()
		{
			OnChannelMessage = null;
			OnChannelPointsRedeem = null;
			ChannelGoalAchieved = null;
		}
	}
}
