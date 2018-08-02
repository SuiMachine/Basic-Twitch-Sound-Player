using BasicTwitchSoundPlayer.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
    class BotnakImporter
    {
        public static List<SoundEntry> ImportFiles()
        {
            List<SoundEntry> soundEntries = new List<SoundEntry>();
            DialogResult res = MessageBox.Show("Do you really want to import sounds from Botnak?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No)
                return soundEntries;

            string botnakDir = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Botnak", "sounds.txt");

            if (!File.Exists(botnakDir))
                return soundEntries;

            var lines = File.ReadAllLines(botnakDir);

            foreach (var line in lines)
            {
                if (line.Count(x => x == ',') >= 2)
                {
                    string[] helper = line.Split(',');
                    string command = helper[0];
                    TwitchRightsEnum requirement = RequirementToInt(helper[1]).ToTwitchRights();
                    string[] files = new string[helper.Length - 2];
                    for (int i = 0; i < helper.Length - 2; i++)
                    {
                        files[i] = helper[i + 2];
                    }
                    soundEntries.Add(new SoundEntry(command, requirement, files, ""));
                }
            }
            return soundEntries;
        }


        private static int RequirementToInt(string requirement)
        {
            if (int.TryParse(requirement, out int val))
            {
                if (val >= (int)TwitchRightsEnum.Public && val <= (int)TwitchRightsEnum.Admin)
                {
                    return val;
                }
                return 0;
            }
            return 0;
        }
    }

}
