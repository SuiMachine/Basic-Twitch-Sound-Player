using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NAudio.Wave;
using System.Diagnostics;
using BasicTwitchSoundPlayer.SoundStorage;
using BasicTwitchSoundPlayer.Structs;

namespace BasicTwitchSoundPlayer
{
    public class SoundBase
    {
        private Random rng;
        public List<SoundEntry> soundlist;
        private List<NSoundPlayer> SoundPlayererStack;
        private PrivateSettings programSettings;
        private Dictionary<string, DateTime> userDB;
        private int delay;
        private string SoundBaseFile;

        #region ConstructorRelated
        public SoundBase(string importPath, PrivateSettings programSettings)
        {
            this.programSettings = programSettings;
            userDB = new Dictionary<string, DateTime>();
            SoundPlayererStack = new List<NSoundPlayer>();
            rng = new Random();
            this.delay = programSettings.Delay;
            this.SoundBaseFile = importPath;
            soundlist = SoundStorageXML.LoadSoundBase(importPath);
        }
        #endregion

        #region changePropertyFunctions
        internal void ChangeVolumeIRC(IRC.OldIRCClient ircBot, string poopedString, MainForm formReference)
        {
            if (!poopedString.Contains(" "))
            {
                ircBot.SendChatMessage("The Volume is: " + Convert.ToInt32(programSettings.Volume * 100).ToString() + "%");
            }
            else
            {
                string[] splice = poopedString.Split(new char[] { ' ' }, 2);
                if(float.TryParse(splice[1], out float volume))
                {
                    volume = volume / 100f;
                    if(volume < 1.0f && volume >= 0.01f)
                    {
                        for(int i = 0; i < SoundPlayererStack.Count; i++)
                        {
                            if(!SoundPlayererStack[i].DonePlaying())
                            {
                                SoundPlayererStack[i].SetVolume(volume);
                            }
                        }
                        formReference.ThreadSafeMoveSlider((int)(volume * 100));
                        ircBot.SendChatMessage("Updated volume to: " + Convert.ToInt32(volume * 100).ToString() + "%");
                    }
                }
            }
        }

        internal void Stopallsounds()
        {
            foreach(NSoundPlayer player in SoundPlayererStack)
            {
                if(player != null)
                    player.Dispose();
            }
            SoundPlayererStack.Clear();
        }

        internal void RemoveSound(IRC.OldIRCClient irc, string text)
        {
            text = text.Split(' ').Last();
            if (text.StartsWith("!"))
                text = text.Remove(0, 1);

            for(int i=0; i<soundlist.Count; i++)
            {
                if(soundlist[i].GetCommand() == text)
                {
                    soundlist.RemoveAt(i);
                    SoundStorageXML.SaveSoundBase(SoundBaseFile, soundlist);
                    irc.SendChatMessage("Removed sound entry #" + i.ToString() + ".");
                    return;
                }
            }
            irc.SendChatMessage("Nothing found.");
        }

        internal void ChangeDelay(IRC.OldIRCClient ircBot, string poopedString)
        {
            if (!poopedString.Contains(" "))
            {
                ircBot.SendChatMessage("The delay is: " + delay.ToString() + " second(s).");
            }
            else
            {
                string[] splice = poopedString.Split(new char[] { ' ' }, 2);
                if(int.TryParse(splice[1], out int delay))
                {
                    if(delay >= 0 && delay < 1800)
                    {
                        this.delay = delay;
                        programSettings.Delay = delay;
                        ircBot.SendChatMessage("Updated delay to: " + delay.ToString() + " second(s).");
                    }
                }
            }
        }

        internal void ChangeVolume(float volume)
        {
            for (int i = 0; i < SoundPlayererStack.Count; i++)
            {
                if (!SoundPlayererStack[i].DonePlaying())
                {
                    SoundPlayererStack[i].SetVolume(volume);
                }
            }
        }
        #endregion

        public bool PlaySoundIfExists(string user, string cmd, TwitchRights userLevel)
        {
            for(int i=SoundPlayererStack.Count-1; i>=0; i--)
            {
                //First, cleanup
                if (SoundPlayererStack[i].DonePlaying())
                {
                    SoundPlayererStack[i].Dispose();
                    SoundPlayererStack.RemoveAt(i);
                }
            }

            if(!userDB.ContainsKey(user))
            {
                userDB.Add(user, DateTime.MinValue);
            }

            if(userDB[user]+TimeSpan.FromSeconds(delay) < DateTime.Now)
            {
                for (int i = 0; i < soundlist.Count; i++)
                {
                    if (soundlist[i].GetCommand() == cmd)
                    {
                        if (userLevel >= soundlist[i].GetRequirement() && soundlist[i].GetRequirement() != TwitchRights.Disabled)
                        {
                            string filename = soundlist[i].GetFile(rng);
                            for(int j=0; j<SoundPlayererStack.Count; j++)
                            {
                                //Just so we don't play the same sounds at the same time
                                if (filename == SoundPlayererStack[i].fullFileName)
                                    return false;
                            }
                            //Sound is found, is not played allocate a new player, start playing it, write down when user started playing a sound so he's under cooldown
                            SoundPlayererStack.Add(new NSoundPlayer(soundlist[i].GetFile(rng), programSettings.Volume));

                            //If the user is SuperMod - we don't use delay
                            //if(!(req == 4))
                                userDB[user] = DateTime.Now;

                            return true;
                        }
                        return false;
                    }
                }
            }
            Debug.WriteLine("User " + user + " has to wait " + (DateTime.Now - (userDB[user] + TimeSpan.FromSeconds(delay))).TotalSeconds + " seconds.");
            return false;
        }

        internal void Close()
        {
            for(int i=0; i<SoundPlayererStack.Count; i++)
            {
                SoundPlayererStack[i].Dispose();
            }
        }

        internal void Marge(List<SoundEntry> importedEntries)
        {
            foreach(var entry in importedEntries)
            {
                if(soundlist.Any(x => x.GetCommand() == entry.GetCommand()))
                {
                    soundlist.First(x => x.GetCommand() == entry.GetCommand()).AddFiles(entry.GetAllFiles());
                }
                else
                {
                    soundlist.Add(entry);
                }
                SoundStorageXML.SaveSoundBase(SoundBaseFile, soundlist);
            }
        }

        internal void Save()
        {
            SoundStorageXML.SaveSoundBase(SoundBaseFile, soundlist);
        }
    }

    class NSoundPlayer :IDisposable
    {
        private bool disposed;
        AudioFileReader audioFileReader;
        IWavePlayer directWaveOut;
        public string fullFileName;
        string fileName;

        public NSoundPlayer(string soundFile, float volume)
        {
            fullFileName = soundFile;
            fileName = soundFile.Split('\\').Last();
            if(File.Exists(soundFile))
            {
                audioFileReader = new AudioFileReader(soundFile);
                directWaveOut = new DirectSoundOut();
                directWaveOut.Init(audioFileReader);
                audioFileReader.Volume = volume;
                directWaveOut.Play();
            }
        }

        public bool DonePlaying()
        {
            if (audioFileReader.CurrentTime >= audioFileReader.TotalTime)
                return true;

            else
                return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if(directWaveOut != null)
                        directWaveOut.Stop();

                    if (audioFileReader != null)
                        audioFileReader.Dispose();

                    if (directWaveOut != null)
                        directWaveOut.Dispose();
                    Debug.WriteLine("[DISPOSE] Disposed of player for " + fileName + ".");
                }
            }
            audioFileReader = null;
            directWaveOut = null;
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void SetVolume(float volume)
        {
            audioFileReader.Volume = volume;
        }
    }
}
