using BasicTwitchSoundPlayer.IRC;
using System;
using System.Text;

namespace BasicTwitchSoundPlayer.Extensions
{
	public static class GeminiExtensions
	{
		public static void AppendStreamInstructionPostfix(this StringBuilder sb, bool attachTimeDate, bool attachIsLive)
		{
			sb.AppendLine("");

			if (attachTimeDate)
			{
				System.Globalization.CultureInfo globalizationOverride = new System.Globalization.CultureInfo("en-US");

				sb.AppendLine($"The current local time is {DateTime.Now:H:mm}. The local date is {DateTime.Now.ToString("MMMM dd, yyy", globalizationOverride)}.");
				sb.AppendLine($"The current UTC time {DateTime.UtcNow:H:mm}. The UTC date is {DateTime.Now.ToString("MMMM dd, yyy", globalizationOverride)}.");
			}

			if (attachIsLive)
			{
				ChannelInstance channelInstance = MainForm.Instance.TwitchBot.ChannelInstance;

				if (channelInstance.StreamStatus?.IsOnline ?? false)
				{

					sb.AppendLine($"{channelInstance.Channel} is now streaming {channelInstance.StreamStatus.game_name}.");
					sb.AppendLine($"The stream title is {channelInstance.StreamStatus.title}.");
				}
				else
				{
					sb.AppendLine($"{channelInstance.Channel} is currently not streaming any game.");
				}
			}
		}
	}
}
