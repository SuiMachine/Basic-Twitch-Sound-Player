using System;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer.SoundStorage
{
	[Serializable]
	public class SoundEntry
	{
		[XmlAttribute]
		public string RewardName;
		[XmlAttribute]
		public string RewardID;
		[XmlAttribute]
		public string Description;
		[XmlArrayItem]
		public string[] Files;
		[XmlAttribute]
		public float Volume;

		public SoundEntry()
		{
			RewardName = "";
			Description = "";
			Files = new string[0];
			RewardID = "";
			Volume = 1f;
		}

		public SoundEntry(string Command, string Description, string RewardID, string[] Files, float Volume)
		{
			this.RewardName = Command;
			this.Description = Description;
			this.RewardID = RewardID;
			this.Files = Files;
			this.Description = Description;
			this.Volume = Volume;
		}

		public string GetFile(Random rng)
		{
			if (Files.Length > 1)
			{
				return Files[rng.Next(0, Files.Length)];
			}
			return Files[0];
		}

		public void AddFiles(string[] importedFiles)
		{
			foreach (var importedFile in importedFiles)
			{
				if (!Files.Any(x => x.ToLower() == importedFile.ToLower()))
				{
					var tempFile = new string[Files.Length + 1];
					for (int i = 0; i < Files.Length; i++)
					{
						tempFile[i] = Files[i];
					}
					tempFile[tempFile.Length - 1] = importedFile;
					Files = tempFile;
				}
			}
		}

		public bool GetIsProperEntry() { return (RewardName != null && RewardName != "") && (Files != null && Files.Length > 0); }
	}
}
