﻿using BasicTwitchSoundPlayer.SoundStorage;
using BasicTwitchSoundPlayer.Structs;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BasicTwitchSoundPlayer
{
	public class SoundBase
	{
		private readonly Random m_RNG;
		public List<SoundEntry> SoundList;
		private List<NSoundPlayer> m_SoundPlayerStack;
		private Dictionary<string, DateTime> m_UserDB;
		private int m_Delay;
		private readonly string m_SoundBaseFile;


		#region ConstructorRelated
		public SoundBase()
		{
			m_UserDB = new Dictionary<string, DateTime>();
			m_SoundPlayerStack = new List<NSoundPlayer>();
			m_RNG = new Random();
			this.m_Delay = PrivateSettings.GetInstance().Delay;
			this.m_SoundBaseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "Sounds.xml");
			SoundList = SoundStorageXML.LoadSoundBase(m_SoundBaseFile);
		}
		#endregion

		#region changePropertyFunctions
		internal void StopAllSounds()
		{
			foreach (NSoundPlayer player in m_SoundPlayerStack)
			{
				if (player != null)
					player.Dispose();
			}
			m_SoundPlayerStack.Clear();
		}

		internal void ChangeVolume(float volume)
		{
			for (int i = 0; i < m_SoundPlayerStack.Count; i++)
			{
				if (!m_SoundPlayerStack[i].DonePlaying())
				{
					m_SoundPlayerStack[i].SetVolume(volume);
				}
			}
		}
		#endregion

		public bool PlaySoundIfExists(string user, string cmd, TwitchRightsEnum userLevel, bool IgnoreCooldowns = false)
		{
			if (user == null)
				return false;

			//Iterate through existing sound players
			for (int i = m_SoundPlayerStack.Count - 1; i >= 0; i--)
			{
				//Dispose of the ones which finished playing
				if (m_SoundPlayerStack[i].DonePlaying())
				{
					m_SoundPlayerStack[i].Dispose();
					m_SoundPlayerStack.RemoveAt(i);
				}
			}

			//Check if our db has a user and if not add him
			if (!m_UserDB.ContainsKey(user))
			{
				m_UserDB.Add(user, DateTime.MinValue);
			}


			//check user cooldown
			if (m_UserDB[user] + TimeSpan.FromSeconds(m_Delay) < DateTime.Now || IgnoreCooldowns)
			{
				//iterate between all files in a sound list
				for (int i = 0; i < SoundList.Count; i++)
				{
					//if sound is found
					if (SoundList[i].GetRewardName() == cmd)
					{
						string filename = SoundList[i].GetFile(m_RNG);
						for (int j = 0; j < m_SoundPlayerStack.Count; j++)
						{
							//Just so we don't play the same sounds at the same time
							if (filename == m_SoundPlayerStack[j].fullFileName)
								return false;
						}
						//Sound is found, is not played allocate a new player, start playing it, write down when user started playing a sound so he's under cooldown

						PrivateSettings programSettings = PrivateSettings.GetInstance();
						NSoundPlayer player = new NSoundPlayer(programSettings.OutputDevice, SoundList[i].GetFile(m_RNG), programSettings.Volume);
						TimeSpan length = player.GetTimeLenght();
						m_SoundPlayerStack.Add(player);
						m_UserDB[user] = DateTime.Now + length;

						return true;
					}
				}
			}
			Debug.WriteLine("User " + user + " has to wait " + (DateTime.Now - (m_UserDB[user] + TimeSpan.FromSeconds(m_Delay))).TotalSeconds + " seconds.");
			return false;
		}

		internal void Close()
		{
			for (int i = 0; i < m_SoundPlayerStack.Count; i++)
			{
				m_SoundPlayerStack[i].Dispose();
			}
		}

		internal void Save()
		{
			SoundStorageXML.SaveSoundBase(m_SoundBaseFile, SoundList);
		}
	}

	enum PlayerFormat
	{
		Generic,
		Vorbis
	}

	class NSoundPlayer : IDisposable
	{
		private bool disposed = false;
		private readonly PlayerFormat format;
		AudioFileReader GenericFileReader;
		NAudio.Vorbis.VorbisWaveReader VorbisFileReader;
		IWavePlayer directWaveOut;
		public string fullFileName;
		private readonly string fileName;

		public NSoundPlayer(Guid SoundDevice, string soundFile, float volume)
		{
			fullFileName = soundFile;
			fileName = soundFile.Split('\\').Last();
			if (File.Exists(soundFile))
			{
				if (soundFile.EndsWith("ogg"))
				{
					format = PlayerFormat.Vorbis;
				}

				switch (format)
				{
					case PlayerFormat.Vorbis:
						VorbisFileReader = new NAudio.Vorbis.VorbisWaveReader(soundFile);
						directWaveOut = new DirectSoundOut(SoundDevice, 120);
						directWaveOut.Init(VorbisFileReader);
						directWaveOut.Volume = volume;
						directWaveOut.Play();
						break;
					default:
						GenericFileReader = new AudioFileReader(soundFile);
						directWaveOut = new DirectSoundOut(SoundDevice, 120);
						directWaveOut.Init(GenericFileReader);
						GenericFileReader.Volume = volume;
						directWaveOut.Play();
						break;
				}

			}
		}

		public TimeSpan GetTimeLenght()
		{
			if (format == PlayerFormat.Generic)
			{
				if (GenericFileReader != null)
					return GenericFileReader.TotalTime;
				else
					return TimeSpan.Zero;
			}
			else if (format == PlayerFormat.Vorbis)
			{
				if (VorbisFileReader != null)
					return VorbisFileReader.TotalTime;
				else
					return TimeSpan.Zero;
			}
			else
				return TimeSpan.Zero;
		}

		public bool DonePlaying()
		{
			switch (format)
			{
				case PlayerFormat.Vorbis:
					if (VorbisFileReader.CurrentTime >= VorbisFileReader.TotalTime)
						return true;
					else
						return false;
				default:
					if (GenericFileReader.CurrentTime >= GenericFileReader.TotalTime)
						return true;
					else
						return false;
			}


		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (directWaveOut != null)
						directWaveOut.Stop();

					if (GenericFileReader != null)
						GenericFileReader.Dispose();

					if (VorbisFileReader != null)
						VorbisFileReader.Dispose();

					if (directWaveOut != null)
						directWaveOut.Dispose();
					Debug.WriteLine("[DISPOSE] Disposed of player for " + fileName + ".");
				}
			}
			GenericFileReader = null;
			VorbisFileReader = null;
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
			if (format == PlayerFormat.Generic && GenericFileReader != null)
				GenericFileReader.Volume = volume;
			else if (format == PlayerFormat.Vorbis && VorbisFileReader != null)
				directWaveOut.Volume = volume;
		}
	}
}
