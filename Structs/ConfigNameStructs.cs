namespace BasicTwitchSoundPlayer.ConfigStructs
{
    public static class SettingsXMLMasterNames
    {
        //Config file and MasterNode
        public const string CONFIGFILE = "Config.xml";
        public const string MASTERNODE = "Configuration";

        public static SettingsXMLProgram ProgramSetting = new SettingsXMLProgram();
        public static SettingsXMLTwitchNames TwitchSettings = new SettingsXMLTwitchNames();
    }

    public class SettingsXMLProgram
    {
        public string NODENAME = "ProgramSettings";

        //Program Settings
        public string Var_Autostart = "Autostart";
        public string Var_Volume = "Volume";
        public string Var_Delay = "Delay";
        public string Var_GenericLineColor = "ColorGenericLine";
        public string Var_SoundLineColor = "ColorSoundLine";
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
