using System;
using System.Linq;

namespace BasicTwitchSoundPlayer.Structs
{
	public struct SoundEntry
	{
		string Command { get; set; }
		string[] File { get; set; }
		TwitchRightsEnum Requirement { get; set; }
		string Description { get; set; }
		DateTime DateAdded { get; set; }


		public SoundEntry(string Command, TwitchRightsEnum Requirement, string File, DateTime DateAdded)
		{
			//This is actually not used, but I wrote it, so whatever
			this.Command = Command;
			this.Requirement = Requirement;
			this.File = new string[1] { File };
			Description = "";
			this.DateAdded = DateAdded;
		}

		public SoundEntry(string Command, TwitchRightsEnum Requirement, string[] Files, string Description, DateTime DateAdded)
		{
			this.Command = Command;
			this.Requirement = Requirement;
			this.File = Files;
			this.Description = Description;
			this.DateAdded = DateAdded;
		}

		public string GetCommand() { return Command; }
		public TwitchRightsEnum GetRequirement() { return Requirement; }
		public string[] GetAllFiles() { return File; }
		public string GetDescription() { return Description; }
		public DateTime GetDateAdded() { return DateAdded; }

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

		public bool GetIsProperEntry() { return (Command != null && Command != "") && (File != null && File.Length > 0); }
	}
}
