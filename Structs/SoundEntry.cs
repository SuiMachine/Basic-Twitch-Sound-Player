﻿using System;
using System.Linq;

namespace BasicTwitchSoundPlayer.Structs
{
    public struct SoundEntry
    {
        string Command { get; set; }
        string[] File { get; set; }
        TwitchRights Requirement { get; set; }

        public SoundEntry(string Command, TwitchRights Requirement, string File)
        {
            //This is actually not used, but I wrote it, so whatever
            this.Command = Command;
            this.Requirement = Requirement;
            this.File = new string[1] { File };
        }

        public SoundEntry(string Command, TwitchRights Requirement, string[] Files)
        {
            this.Command = Command;
            this.Requirement = Requirement;
            this.File = Files;
        }

        public string GetCommand() { return Command; }
        public TwitchRights GetRequirement() { return Requirement; }
        public string[] GetAllFiles() { return File; }
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
            foreach(var importedFile in importedFiles)
            {
                if(!File.Any(x => x.ToLower() == importedFile.ToLower()))
                {
                    var tempFile = new string[File.Length + 1];
                    for(int i=0; i<File.Length; i++)
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