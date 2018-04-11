using System.Collections.Generic;
using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.Structs;
using System.IO;
using System.Xml;

namespace BasicTwitchSoundPlayer.SoundStorage
{
    class SoundStorageXML
    {
        private static readonly string varSounds = "Sounds";
        private static readonly string varRequirement = "Requirement";

        public static List<SoundEntry> LoadSoundBase(string XmlPath)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ROOTNODE = null;
            List<SoundEntry> entries = new List<SoundEntry>();

            if (!File.Exists(XmlPath))
            {
                ROOTNODE = doc.Sui_GetNode("Entries");
                SaveSoundBase(XmlPath, entries);
            }
            else
            {
                doc.Load(XmlPath);
                foreach (XmlNode sound in doc["Entries"])
                {
                    var newSoundEntry = doc.GetQuickSoundEntry(sound);
                    if (newSoundEntry.GetIsProperEntry())
                        entries.Add(newSoundEntry);
                }
            }
            return entries;
        }

        public static void SaveSoundBase(string XmlPath, List<SoundEntry> entries)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ROOTNODE = doc.Sui_GetNode("Entries");

            var parentDir = Directory.GetParent(XmlPath);
            if (!parentDir.Exists)
                parentDir.Create();

            foreach(var entry in entries)
            {
                if(entry.GetIsProperEntry())
                {
                    var ChildNode = ROOTNODE.Sui_GetNode(doc, entry.GetCommand());
                    ChildNode.Sui_SetAttributeValue(doc, varSounds, string.Join(";", entry.GetAllFiles()));
                    ChildNode.Sui_SetAttributeValue(doc, varRequirement, entry.GetRequirement().ToString());
                }
            }

            doc.Save(XmlPath);
        }
    }

    static class SoundEntriesExtansions
    {
        private static readonly string varSounds = "Sounds";
        private static readonly string varRequirement = "Requirement";

        public static SoundEntry GetQuickSoundEntry(this XmlDocument xmlDoc, XmlNode node)
        {
            try
            {
                string tmpCommand = node.Name;
                string[] tmpSounds = node.Sui_GetAttributeValue(xmlDoc, varSounds, "").Split(';');
                TwitchRights tmpRequirement = node.Sui_GetAttributeValue(xmlDoc, varRequirement, TwitchRights.Disabled.ToString()).ToTwitchRights();
                return new SoundEntry(tmpCommand, tmpRequirement, tmpSounds);
            }
            catch
            {
                return new SoundEntry();
            }
        }
    }
}
