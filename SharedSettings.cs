using System.Drawing;
using System.IO;
using System.Xml;
using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.ConfigStructs;

namespace BasicTwitchSoundPlayer
{
    public class ColorStruct
    {
        //Form background
        public Color FormBackground { get; set; }
        public Color FormTextColor { get; set; }

        //MenuStripBar
        public Color MenuStripBarBackground { get; set; }
        public Color MenuStripBarText { get; set; }

        //MenuStripColors
        public Color MenuStripBackground { get; set; }
        public Color MenuStripText { get; set; }
        public Color MenuStripBackgroundSelected { get; set; }

        //LineColors
        public Color LineColorBackground { get; set; }
        public Color LineColorGeneric { get; set; }
        public Color LineColorIrcCommand { get; set; }
        public Color LineColorModeration { get; set; }
        public Color LineColorSoundPlayback { get; set; }

        public ColorStruct()
        {
            FormBackground = Color.WhiteSmoke;
            FormTextColor = Color.Black;

            MenuStripBarBackground = Color.WhiteSmoke;
            MenuStripBarText = Color.Black;

            MenuStripBackground = Color.WhiteSmoke;
            MenuStripText = Color.Black;
            MenuStripBackgroundSelected = Color.SkyBlue;

            LineColorBackground = Color.GhostWhite;
            LineColorGeneric = Color.Black;
            LineColorIrcCommand = Color.DarkGreen;
            LineColorModeration = Color.DarkBlue;
            LineColorSoundPlayback = Color.DarkOrange;
        }
    }

    public class PrivateSettings
    {
        private XmlDocument xmlDoc;
        private XmlNode MasterNode;

        #region Properties
        public ColorStruct Colors { get; set; }

        public bool Autostart { get; set; }
        public float Volume { get; set; }
        public int Delay { get; set; }
        public bool AllowUsersToUseSubSounds { get; set; }

        public string TwitchServer { get; set; }
        public string TwitchUsername { get; set; }
        public string TwitchPassword { get; set; }
        public string TwitchChannelToJoin { get; set; }
        public string GoogleSpreadsheetID { get; set; }
        #endregion

        public PrivateSettings()
        {
            //NOTE: Make sure everything is initialized first!
            Autostart = false;
            AllowUsersToUseSubSounds = false;
            Volume = 0.5f;
            Delay = 15;
            this.Colors = new ColorStruct();

            TwitchServer = "";
            TwitchUsername = "";
            TwitchPassword = "";
            TwitchChannelToJoin = "";
            GoogleSpreadsheetID = "";

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

            //Colors
            var ColorSettingsNode = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.NODENAME);

            Colors.FormBackground = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_FormBackground).Sui_GetInnerText(Colors.FormBackground.ToArgb().ToString()), Colors.FormBackground);
            Colors.FormTextColor = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_FormTextColor).Sui_GetInnerText(Colors.FormTextColor.ToArgb().ToString()), Colors.FormTextColor);

            Colors.MenuStripBarBackground = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBarBackground).Sui_GetInnerText(Colors.MenuStripBarBackground.ToArgb().ToString()), Colors.MenuStripBarBackground);
            Colors.MenuStripBarText = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBarText).Sui_GetInnerText(Colors.MenuStripBarText.ToArgb().ToString()), Colors.MenuStripBarText);

            Colors.MenuStripBackground = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBackground).Sui_GetInnerText(Colors.MenuStripBackground.ToArgb().ToString()), Colors.MenuStripBackground);
            Colors.MenuStripText = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripText).Sui_GetInnerText(Colors.MenuStripText.ToArgb().ToString()), Colors.MenuStripText);
            Colors.MenuStripBackgroundSelected = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBackgroundSelected).Sui_GetInnerText(Colors.MenuStripBackgroundSelected.ToArgb().ToString()), Colors.MenuStripBackgroundSelected);

            Colors.LineColorBackground = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorBackground).Sui_GetInnerText(Colors.LineColorBackground.ToArgb().ToString()), Colors.LineColorBackground);
            Colors.LineColorGeneric = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorGeneric).Sui_GetInnerText(Colors.LineColorGeneric.ToArgb().ToString()), Colors.LineColorGeneric);
            Colors.LineColorIrcCommand = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorIrcCommand).Sui_GetInnerText(Colors.LineColorIrcCommand.ToArgb().ToString()), Colors.LineColorIrcCommand);
            Colors.LineColorModeration = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorModeration).Sui_GetInnerText(Colors.LineColorModeration.ToArgb().ToString()), Colors.LineColorModeration);
            Colors.LineColorSoundPlayback = ColorExtension.ParseColor(ColorSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorSoundPlayback).Sui_GetInnerText(Colors.LineColorSoundPlayback.ToArgb().ToString()), Colors.LineColorSoundPlayback);

            //Other
            Volume = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Volume).Sui_GetInnerText("0.5").ToFloat(0.5f);
            Delay = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Delay).Sui_GetInnerText("60").ToInt(15);
            AllowUsersToUseSubSounds = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_AllowUsersToUseSubscriberSounds).Sui_GetInnerText("false").ToBoolean(false);

            //TwitchSettings
            var TwitchSettings = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.NODENAME);
            TwitchServer = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Server).Sui_GetInnerText(TwitchServer);
            TwitchUsername = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Username).Sui_GetInnerText(TwitchUsername);
            TwitchPassword = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Password).Sui_GetInnerText(TwitchPassword);
            TwitchChannelToJoin = TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_ChannelToJoin).Sui_GetInnerText(TwitchChannelToJoin);

            //GoogleSheets
            GoogleSpreadsheetID = ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_GoogleSpreadsheetId).Sui_GetInnerText(GoogleSpreadsheetID);
        }

        public void SaveSettings()
        {
            //ProgramSettings
            var ProgramSettingsNode = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.NODENAME);
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Autostart).Sui_SetInnerText(Autostart.ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Volume).Sui_SetInnerText(Volume.ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_Delay).Sui_SetInnerText(Delay.ToString());
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_AllowUsersToUseSubscriberSounds).Sui_SetInnerText(AllowUsersToUseSubSounds.ToString());

            //Colors
            var ColorsNode = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.NODENAME);
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_FormBackground).Sui_SetInnerText(Colors.FormBackground.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_FormTextColor).Sui_SetInnerText(Colors.FormTextColor.ToArgb().ToString());

            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBarBackground).Sui_SetInnerText(Colors.MenuStripBarBackground.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBarText).Sui_SetInnerText(Colors.MenuStripBarText.ToArgb().ToString());

            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBackground).Sui_SetInnerText(Colors.MenuStripBackground.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripText).Sui_SetInnerText(Colors.MenuStripText.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_MenuStripBackgroundSelected).Sui_SetInnerText(Colors.MenuStripBackgroundSelected.ToArgb().ToString());

            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorBackground).Sui_SetInnerText(Colors.LineColorBackground.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorGeneric).Sui_SetInnerText(Colors.LineColorGeneric.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorIrcCommand).Sui_SetInnerText(Colors.LineColorIrcCommand.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorModeration).Sui_SetInnerText(Colors.LineColorModeration.ToArgb().ToString());
            ColorsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.Colors.Var_LineColorSoundPlayback).Sui_SetInnerText(Colors.LineColorSoundPlayback.ToArgb().ToString());


            //TwitchSettings
            var TwitchSettings = MasterNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.NODENAME);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Server).Sui_SetInnerText(TwitchServer);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Username).Sui_SetInnerText(TwitchUsername);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_Password).Sui_SetInnerText(TwitchPassword);
            TwitchSettings.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.TwitchSettings.Var_Twitch_ChannelToJoin).Sui_SetInnerText(TwitchChannelToJoin);

            //GoogleSheets
            ProgramSettingsNode.Sui_GetNode(xmlDoc, SettingsXMLMasterNames.ProgramSetting.Var_GoogleSpreadsheetId).Sui_SetInnerText(GoogleSpreadsheetID);
            xmlDoc.Save(SettingsXMLMasterNames.CONFIGFILE);
        }
        #endregion
    }
}
