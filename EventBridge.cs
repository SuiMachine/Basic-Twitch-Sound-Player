using SuiBot_TwitchSocket.API.EventSub;
using System;

namespace BasicTwitchSoundPlayer
{
	public class EventBridge
	{
		public Action<ES_ChannelPoints.ES_ChannelPointRedeemRequest> OnChannelPointsRedeem;

		internal void Clear()
		{
			OnChannelPointsRedeem = null;
		}
	}
}
