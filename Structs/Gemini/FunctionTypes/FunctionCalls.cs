using BasicTwitchSoundPlayer.IRC;
using SuiBot_TwitchSocket.API.EventSub;
using System;
using static SuiBot_TwitchSocket.API.EventSub.ES_ChannelPoints;
using static SuiBotAI.Components.Other.Gemini.GeminiTools;

namespace BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes
{
	public abstract class FunctionCall
	{
		public virtual void Perform(ChannelInstance channelInstance, ES_ChatMessage message) { }
		public virtual void Perform(ChannelInstance channelInstance, ES_ChannelPointRedeemRequest message) { }

	}

	[Serializable]
	public class TimeOutUser : FunctionCall
	{
		public double duration_in_seconds = 1;
		public string text_response = null;

		public override void Perform(ChannelInstance channelInstance, ES_ChatMessage message)
		{
			if (message.UserRole >= ES_ChatMessage.Role.VIP)
			{
				channelInstance.UserTimeout(message, (uint)duration_in_seconds, text_response);
			}
		}

		public override void Perform(ChannelInstance channelInstance, ES_ChannelPointRedeemRequest message)
		{
			channelInstance.UserTimeout(message.broadcaster_user_id, message.user_id, (uint)duration_in_seconds, text_response);
		}
	}

	[Serializable]
	public class BanUser : FunctionCall
	{
		public string text_response = null;

		public override void Perform(ChannelInstance channelInstance, ES_ChatMessage message)
		{
			if (message.UserRole >= ES_ChatMessage.Role.VIP)
			{
				channelInstance.UserBan(message, text_response);
			}
		}

		public override void Perform(ChannelInstance channelInstance, ES_ChannelPointRedeemRequest message)
		{
			channelInstance.UserBan(message.broadcaster_user_id, message.user_id, text_response);
		}
	}

	public static class GeminiFunctionCall
	{
		public static GeminiFunction CreateBanFunction() => new GeminiFunction("ban", "bans a user", new BanParameters());
		public static GeminiFunction CreateTimeoutFunction() => new GeminiFunction("timeout", "time outs a user in the chat", new TimeOutParameters());
	}
}
