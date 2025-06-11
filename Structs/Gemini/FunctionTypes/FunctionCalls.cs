using BasicTwitchSoundPlayer.IRC;
using SuiBot_TwitchSocket.API.EventSub;
using System;
using static SuiBotAI.Components.Other.Gemini.GeminiTools;

namespace BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes
{
	public abstract class FunctionCall
	{
		public virtual void Perform(ChannelInstance channelInstance, ES_ChatMessage message, SuiBotAI.Components.Other.Gemini.GeminiContent content) { }
	}

	[Serializable]
	public class TimeOutUser : FunctionCall
	{
		public double duration_in_seconds = 1;
		public string text_response = null;

		public override void Perform(ChannelInstance channelInstance, ES_ChatMessage message, SuiBotAI.Components.Other.Gemini.GeminiContent content)
		{
			if (message.UserRole >= ES_ChatMessage.Role.VIP)
			{
				channelInstance.UserTimeout(message, (uint)duration_in_seconds, text_response);
			}
		}
	}

	[Serializable]
	public class BanUser : FunctionCall
	{
		public string text_response = null;

		public override void Perform(ChannelInstance channelInstance, ES_ChatMessage message, SuiBotAI.Components.Other.Gemini.GeminiContent content)
		{
			if (message.UserRole >= ES_ChatMessage.Role.VIP)
			{
				channelInstance.UserBan(message, text_response);
			}
		}
	}

	public static class GeminiFunctionCall
	{
		public static GeminiFunction CreateBanFunction() => new GeminiFunction("ban", "bans a user", new BanParameters());
		public static GeminiFunction CreateTimeoutFunction() => new GeminiFunction("timeout", "time outs a user in the chat", new TimeOutParameters());
		public static GeminiFunction CreateWRFunction() => new GeminiFunction("world_record", "Gets best time (world record) speedrunning leaderboard if it exists", new Speedrun.WorldRecordRequest());
		public static GeminiFunction CreatePBFunction() => new GeminiFunction("personal_best", "Gets streamer's personal best from speedrunning leaderboard if it exists", new Speedrun.PersonalBestRequest());
	}
}
