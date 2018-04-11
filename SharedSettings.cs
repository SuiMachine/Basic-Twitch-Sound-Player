using System.Drawing;
using System.IO;
using System.Xml;
using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.ConfigStructs;

namespace BasicTwitchSoundPlayer
{
    public class PrivateSettings
    {
        private XmlDocument xmlDoc;
        private XmlNode MasterNode;

        #region Properties
        public bool Autostart { get; set; }
        public float Volume { get; set; }
        public int Delay { get; set; }

        public Color ColorGenericLine { get; set; }
        public Color ColorSoundLine { get; set; }

        public string TwitchServer { get; set; }
        public string TwitchUsername { get; set; }
        public string TwitchPassword { get; set; }
        public string TwitchChannelToJoin { get; set; }
        #endregion

        public PrivateSettings()
        {
            //NOTE: Make sure everything is initialized first!
            Autostart = false;
            Volume = 0.5f;
            Delay = 15;
            ColorGenericLine = Color.WhiteSmoke;
            ColorSoundLine = Color.Yellow;

            TwitchServer = "";
            TwitchUsername = "";
            TwitchPassword = "";
            TwitchChannelToJoin = "";

            LoadSettings();
        }

        #region Load/Save
        private void LoadSettings()
        {
            xmlDoc = new XmlDocument();

            if (File.Exists(SettingsXMLMasterNames.CONFIGFILE))
            {
                xmlDoc.Load(SettingsXMLMasterNames.CONFIGFILE);
            }

            //I will have to come up with a cleaner way of doing this one day
            MasterNode = xmlDoc.Sui_GetNode(SettingsXMLMasterNames.MASTERNODE);
            
            //ProgramSettings
            var ProgramSettingsNode = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.NODENAME);
            Autostart = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Autostart).Sui_GetInnerText("false").ToBoolean(false);
            ColorGenericLine = ColorExtension.ParseColor(ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_GenericLineColor).Sui_GetInnerText(ColorGenericLine.ToArgb().ToString()), ColorGenericLine);
            ColorSoundLine = ColorExtension.ParseColor(ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_SoundLineColor).Sui_GetInnerText(ColorSoundLine.ToArgb().ToString()), ColorSoundLine);
            Volume = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Volume).Sui_GetInnerText("0.5").ToFloat(0.5f);
            Delay = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Delay).Sui_GetInnerText("15").ToInt(15);

            //TwitchSettings
            var TwitchSettings = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.NODENAME);
            TwitchServer = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Server).Sui_GetInnerText(TwitchServer);
            TwitchUsername = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Username).Sui_GetInnerText(TwitchUsername);
            TwitchPassword = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Password).Sui_GetInnerText(TwitchPassword);
            TwitchChannelToJoin = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_ChannelToJoin).Sui_GetInnerText(TwitchChannelToJoin);
        }

        public void SaveSettings()
        {
            //ProgramSettings
            var ProgramSettingsNode = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.NODENAME);
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Autostart).Sui_SetInnerText(Autostart.ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_GenericLineColor).Sui_SetInnerText(ColorGenericLine.ToArgb().ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_SoundLineColor).Sui_SetInnerText(ColorSoundLine.ToArgb().ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Volume).Sui_SetInnerText(Volume.ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Delay).Sui_SetInnerText(Delay.ToString());

            //TwitchSettings
            var TwitchSettings = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.NODENAME);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Server).Sui_SetInnerText(TwitchServer);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Username).Sui_SetInnerText(TwitchUsername);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Password).Sui_SetInnerText(TwitchPassword);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_ChannelToJoin).Sui_SetInnerText(TwitchChannelToJoin);

            xmlDoc.Save(SettingsXMLMasterNames.CONFIGFILE);
        }
        #endregion
    }
}
