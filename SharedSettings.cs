using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BasicTwitchSoundPlayer.Extensions;

namespace BasicTwitchSoundPlayer
{
    [Serializable]
    public class ColorStruct
    {
        //Form background
        [XmlElement]
        public ColorWrapper FormBackground { get; set; }
        [XmlElement]
        public ColorWrapper FormTextColor { get; set; }

        //MenuStripBar
        [XmlElement]
        public ColorWrapper MenuStripBarBackground { get; set; }
        [XmlElement]
        public ColorWrapper MenuStripBarText { get; set; }

        //MenuStripColors
        [XmlElement]
        public ColorWrapper MenuStripBackground { get; set; }
        [XmlElement]
        public ColorWrapper MenuStripText { get; set; }
        [XmlElement]
        public ColorWrapper MenuStripBackgroundSelected { get; set; }

        //LineColors
        [XmlElement]
        public ColorWrapper LineColorBackground { get; set; }
        [XmlElement]
        public ColorWrapper LineColorGeneric { get; set; }
        [XmlElement]
        public ColorWrapper LineColorIrcCommand { get; set; }
        [XmlElement]
        public ColorWrapper LineColorModeration { get; set; }
        [XmlElement]
        public ColorWrapper LineColorSoundPlayback { get; set; }

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

    [Serializable]
    public class PrivateSettings
    {
        public const string CONFIGFILE = "Config.xml";

        #region Properties
        [XmlElement]
        public ColorStruct Colors { get; set; }

        [XmlElement]
        public bool Autostart { get; set; }
        [XmlElement]
        public float Volume { get; set; }
        [XmlElement]
        public int Delay { get; set; }
        [XmlElement]
        public bool AllowUsersToUseSubSounds { get; set; }

        [XmlElement]
        public string TwitchServer { get; set; }
        [XmlElement]
        public string TwitchUsername { get; set; }
        [XmlElement]
        public string TwitchPassword { get; set; }
        [XmlElement]
        public string TwitchChannelToJoin { get; set; }
        [XmlElement]
        public string GoogleSpreadsheetID { get; set; }
        [XmlElement]
        public string VoiceSynthesizer { get; set; }
        #endregion

        public PrivateSettings()
        {
            //NOTE: Make sure everything is initialized first!
            Autostart = false;
            AllowUsersToUseSubSounds = false;
            Volume = 0.5f;
            Delay = 15;
            this.Colors = new ColorStruct();

            TwitchServer = "irc.twitch.tv";
            TwitchUsername = "";
            TwitchPassword = "";
            TwitchChannelToJoin = "";
            GoogleSpreadsheetID = "";
            VoiceSynthesizer = "";
        }

        #region Load/Save
        public static PrivateSettings LoadSettings()
        {
            if (File.Exists(CONFIGFILE))
            {
                PrivateSettings obj;
                XmlSerializer serializer = new XmlSerializer(typeof(PrivateSettings));
                FileStream fs = new FileStream(CONFIGFILE, FileMode.Open); //Extension is NOT *.xml on purpose so that in case of streaming monitor, it's not tied to normal text editors, as it contains authy token (password).
                obj = (PrivateSettings)serializer.Deserialize(fs);
                fs.Close();
                return obj;
            }
            else
                return new PrivateSettings();
        }

        public void SaveSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PrivateSettings));
            StreamWriter fw = new StreamWriter(CONFIGFILE);
            serializer.Serialize(fw, this);
            fw.Close();
        }
        #endregion
    }
}
