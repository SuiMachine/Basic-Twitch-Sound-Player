using BasicTwitchSoundPlayer.IRC;
using SuiBot_TwitchSocket.API.EventSub;
using System;

namespace BasicTwitchSoundPlayer.Structs.Gemini.FunctionTypes
{
	public abstract class FunctionCall
	{
		public abstract void Perform(ChannelInstance channelInstance, ES_ChatMessage message);
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
	}

	[Serializable]
	public class BanUser : FunctionCall
	{
		public string response = null;

		public override void Perform(ChannelInstance channelInstance, ES_ChatMessage message)
		{
			if (message.UserRole >= ES_ChatMessage.Role.VIP)
			{
				channelInstance.UserBan(message, response);
			}
		}
	}
}
