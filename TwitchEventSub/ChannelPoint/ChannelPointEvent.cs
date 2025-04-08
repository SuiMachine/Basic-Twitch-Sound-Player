using BasicTwitchSoundPlayer.IRC;
using System;

namespace BasicTwitchSoundPlayer.TwitchEventSub.ChannelPoint
{
	[Serializable]
	public class ChannelPointEvent
	{
		public class Reward
		{
			public string id;
			public string title;
			public int cost;
			public string prompt;
		}

		public string broadcaster_user_id;
		public string broadcaster_user_login;
		public string broadcaster_user_name;
		public string id;
		public string user_id;
		public string user_login;
		public string user_name;
		public string user_input;
		public KrakenConnections.RedemptionStates status;
		public Reward reward;
		public DateTime redeemed_at;
	}
}
