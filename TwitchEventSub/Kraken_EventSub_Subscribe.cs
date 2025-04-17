using System;

namespace BasicTwitchSoundPlayer.TwitchEventSub.KrakenSubscribe
{
	public class Kraken_EventSub_Condition
	{
		public string broadcaster_user_id = null;
		public string moderator_user_id = null;
		public Kraken_EventSub_Condition() { }

		public Kraken_EventSub_Condition(string broadcaster_user_id)
		{
			this.broadcaster_user_id = broadcaster_user_id;
		}
	}

	public class Kraken_EventSub_Transport_Websocket
	{
		public string method = "websocket";
		public string session_id = "";

		public Kraken_EventSub_Transport_Websocket()
		{
			method = "websocket";
			this.session_id = "";
		}

		public Kraken_EventSub_Transport_Websocket(string session_id)
		{
			this.method = "websocket";
			this.session_id = session_id;
		}
	}


	[Serializable]
	public class Kraken_EventSub_Subscribe_RedemptionAdd
	{
		public string type = "channel.channel_points_custom_reward_redemption.add";
		public int version = 1;
		public Kraken_EventSub_Condition condition = null;
		public Kraken_EventSub_Transport_Websocket transport = null;

		public Kraken_EventSub_Subscribe_RedemptionAdd() { }

		public Kraken_EventSub_Subscribe_RedemptionAdd(string broadcasterID, string sessionId)
		{
			this.condition = new Kraken_EventSub_Condition(broadcasterID);
			transport = new Kraken_EventSub_Transport_Websocket(sessionId);
		}
	}
}
