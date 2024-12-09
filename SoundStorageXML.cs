using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer.SoundStorage
{
	class SoundStorageXML
	{
		private static readonly string varSounds = "Sounds";
		private static readonly string varDescription = "SoundDescription";

		public static List<SoundEntry> LoadSoundBase(string XmlPath)
		{
			List<SoundEntry> entries;
			if (!File.Exists(XmlPath))
			{
				entries = new List<SoundEntry>();
				SaveSoundBase(XmlPath, entries);
			}
			else
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SoundEntry>));
				FileStream fs = new FileStream(XmlPath, FileMode.Open);
				entries = (List<SoundEntry>)xmlSerializer.Deserialize(fs);
				fs.Dispose();
			}
			return entries;
		}

		public static void SaveSoundBase(string XmlPath, List<SoundEntry> entries)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SoundEntry>));
			FileStream fs = new FileStream(XmlPath, FileMode.OpenOrCreate);
			xmlSerializer.Serialize(fs, entries);
			fs.Dispose();
		}
	}
}
