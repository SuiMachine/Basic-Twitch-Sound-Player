using System;
using System.Linq;

namespace BasicTwitchSoundPlayer.Structs
{
	//This struct is butt ugly and I hate I wrote something like this many years ago... and manually serialized and deserialized
	public struct SoundEntry
	{
		string RewardName { get; set; }
		string Description { get; set; }
		string[] File { get; set; }

		public SoundEntry(string Command, string Description, string[] Files)
		{
			this.RewardName = Command;
			this.Description = Description;
			this.File = Files;
			this.Description = Description;
		}

		public string GetRewardName() { return RewardName; }
		public string[] GetAllFiles() { return File; }
		public string GetDescription() { return Description; }

		public string GetFile(Random rng)
		{
			if (File.Length > 1)
			{
				return File[rng.Next(0, File.Length)];
			}
			return File[0];
		}

		public void AddFiles(string[] importedFiles)
		{
			foreach (var importedFile in importedFiles)
			{
				if (!File.Any(x => x.ToLower() == importedFile.ToLower()))
				{
					var tempFile = new string[File.Length + 1];
					for (int i = 0; i < File.Length; i++)
					{
						tempFile[i] = File[i];
					}
					tempFile[tempFile.Length - 1] = importedFile;
					File = tempFile;
				}
			}
		}

		public bool GetIsProperEntry() { return (RewardName != null && RewardName != "") && (File != null && File.Length > 0); }
	}
}
