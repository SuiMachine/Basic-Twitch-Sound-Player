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
				sb.AppendLine($"The current date is {DateTime.Now.ToShortDateString()}.");
				sb.AppendLine($"The current local time is {DateTime.Now.ToShortTimeString()}.");
				sb.AppendLine($"The current UTC time {DateTime.UtcNow.ToShortTimeString()}.");
			}

			if (attachIsLive)
			{
				OldIRCClient irc = MainForm.Instance.TwitchBot.Irc;

				if (irc.KrakenConnection.IsLive)
				{

					sb.AppendLine($"{PrivateSettings.GetInstance().UserName} is now streaming {irc.KrakenConnection.GameTitle}.");
					sb.AppendLine($"The stream title is {irc.KrakenConnection.StreamTitle}.");
				}
				else
				{
					sb.AppendLine($"{PrivateSettings.GetInstance().UserName} is currently not streaming any game.");
				}
			}
		}
	}
}
