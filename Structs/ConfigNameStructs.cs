namespace BasicTwitchSoundPlayer.ConfigStructs
{
    public static class SettingsXMLMasterNames
    {
        //Config file and MasterNode
        public const string CONFIGFILE = "Config.xml";
        public const string MASTERNODE = "Configuration";

        public static SettingsXMLProgram ProgramSetting = new SettingsXMLProgram();
        public static SettingsXMLTwitchNames TwitchSettings = new SettingsXMLTwitchNames();
        public static SettingsXMLColors Colors = new SettingsXMLColors();
    }

    public class SettingsXMLProgram
    {
        public string NODENAME = "ProgramSettings";

        //Program Settings
        public string Var_Autostart = "Autostart";
        public string Var_Volume = "Volume";
        public string Var_Delay = "Delay";
    }

    public class SettingsXMLColors
    {
        public string NODENAME = "ColorSettings";

        public string Var_FormBackground = "FormBackground";
        public string Var_FormTextColor = "FormTextColor";


        public string Var_MenuStripBarBackground = "MenuStripBarBackground";
        public string Var_MenuStripBarText = "MenuStripBarText";

        public string Var_MenuStripBackground = "MenuStripBackground";
        public string Var_MenuStripText = "MenuStripText";
        public string Var_MenuStripBackgroundSelected = "MenuStripBackgroundSelected";

        public string Var_LineColorBackground = "LineColorBackground";

        public string Var_LineColorGeneric = "LineColorGeneric";

        public string Var_LineColorIrcCommand = "LineColorIrcCommand";
        public string Var_LineColorModeration = "LineColorModeration";
        public string Var_LineColorSoundPlayback = "LineColorSoundPlayback";
    }

    public class SettingsXMLTwitchNames
    {
        public string NODENAME = "TwitchSettings";
        public string Var_Twitch_Server = "TwitchServer";
        public string Var_Twitch_Username = "TwitchUsername";
        public string Var_Twitch_Password = "TwitchPassword";
        public string Var_Twitch_ChannelToJoin = "ChannelToJoin";
    }
}
