using System;

namespace BasicTwitchSoundPlayer.Structs
{
    public enum TwitchRights
    {
        Disabled,
        Public,
        TrustedSub,
        Mod,
        Admin,
    }

    static class TwitchRightsExtensions
    {
        public static TwitchRights ToTwitchRights(this int Number)
        {
            Number += 1;
            try
            {
                TwitchRights right = (TwitchRights)Number;
                return right;
            }
            catch
            {
                return TwitchRights.Disabled;
            }
        }

        public static TwitchRights ToTwitchRights(this string Text)
        {
            if (Enum.TryParse(Text, out TwitchRights enumeratedResult))
            {
                return enumeratedResult;
            }
            else
                return TwitchRights.Disabled;
        }
    }
}
