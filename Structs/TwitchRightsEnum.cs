using System;
using System.ComponentModel;

namespace BasicTwitchSoundPlayer.Structs
{
	public enum TwitchRightsEnum
	{
		[Description("Disabled")]
		Disabled,
		[Description("Public")]
		Public,
		[Description("Subscribers / Trusted")]
		TrustedSub,
		[Description("Moderators")]
		Mod,
		[Description("Administrator")]
		Admin,
	}

	public enum TTSLogic
	{
		[Description("Require role & reward ID")]
		Restricted,
		[Description("Use role for !tts")]
		RewardIDAndCommand
	}

	public enum SoundRedemptionLogic
	{
		[Description("Legacy (command and user cooldowns)")]
		Legacy,
		[Description("Twitch channel points")]
		ChannelPoints
	}

	public enum MessageType
	{
		Normal,
		TTSReward,
		SoundReward
	}

	static class TwitchRightsExtensions
	{
		public static TwitchRightsEnum ToTwitchRights(this int Number)
		{
			Number += 1;
			try
			{
				TwitchRightsEnum right = (TwitchRightsEnum)Number;
				return right;
			}
			catch
			{
				return TwitchRightsEnum.Disabled;
			}
		}

		public static TwitchRightsEnum ToTwitchRights(this string Text)
		{
			if (Enum.TryParse(Text, out TwitchRightsEnum enumeratedResult))
			{
				return enumeratedResult;
			}
			else
				return TwitchRightsEnum.Disabled;
		}
	}
}
