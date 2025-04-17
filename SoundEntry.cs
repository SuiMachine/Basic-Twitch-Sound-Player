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
		[XmlAttribute]
		public float Volume;
		[XmlAttribute]
		public int AmountOfPoints;
		[XmlAttribute]
		public int Cooldown;
		[XmlArrayItem]
		public string[] Files;
		[XmlArrayItem]
		public string[] Tags;

		public SoundEntry()
		{
			RewardName = "";
			Description = "";
			Files = new string[0];
			Tags = new string[0];
			RewardID = "";
			Volume = 1f;
			AmountOfPoints = 500;
			Cooldown = 0;
		}

		public SoundEntry(string Command, string Description, string RewardID, string[] Files, string[] Tags, float Volume, int AmountOfPoints, int Cooldown)
		{
			this.RewardName = Command;
			this.Description = Description;
			this.RewardID = RewardID;
			this.Files = Files;
			this.Tags = Tags;
			this.Description = Description;
			this.Volume = Volume;
			this.AmountOfPoints = AmountOfPoints;

			if (Cooldown < 0)
				Cooldown = 0;
			this.Cooldown = Cooldown;
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

		public SoundEntry CreateCopy() => new SoundEntry()
		{
			RewardID = RewardID,
			RewardName = RewardName,
			AmountOfPoints = AmountOfPoints,
			Cooldown = Cooldown,
			Description = Description,
			Files = Files,
			Volume = Volume,
		};
	}
}
