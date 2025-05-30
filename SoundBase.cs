using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SoundStorage;
using NAudio.Wave;
using SuiBot_TwitchSocket.API.EventSub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static SuiBot_TwitchSocket.API.EventSub.ES_ChannelPoints;

namespace BasicTwitchSoundPlayer
{
	public class SoundDB
	{
		private readonly Random m_RNG;
		public List<SoundEntry> SoundList;
		public Dictionary<string, SoundEntry> RewardsToSound = new Dictionary<string, SoundEntry>();
		public Dictionary<string, SoundEntry> UniversalRewards = new Dictionary<string, SoundEntry>();
		private List<NSoundPlayer> m_SoundPlayerStack;
		private Dictionary<string, DateTime> m_UserDB;
		private int m_Delay;
		private readonly string m_SoundBaseFile;

		#region ConstructorRelated
		public SoundDB()
		{
			m_UserDB = new Dictionary<string, DateTime>();
			m_SoundPlayerStack = new List<NSoundPlayer>();
			m_RNG = new Random();
			this.m_Delay = PrivateSettings.GetInstance().Delay;
			this.m_SoundBaseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BasicTwitchSoundPlayer", "Sounds.xml");
			SoundList = SoundStorageXML.LoadSoundBase(m_SoundBaseFile);
			RebuildDictionary();
		}

		public void Register()
		{
			MainForm.Instance.TwitchEvents.OnChannelPointsRedeem += PlaySoundIfExists;
		}

		public void RebuildDictionary()
		{
			RewardsToSound.Clear();
			UniversalRewards.Clear();
			foreach (SoundEntry entry in SoundList)
			{
				if (string.IsNullOrEmpty(entry.RewardID))
				{
					if (entry.Tags.Length > 0)
					{
						foreach (string tag in entry.Tags)
						{
							//Check for collision first
							var lower_case_tag = tag.ToLower();
							if (UniversalRewards.TryGetValue(lower_case_tag, out var collidingReward))
								MainForm.Instance.ThreadSafeAddPreviewText($"Couldn't add universal reward - {entry.RewardName} and {collidingReward.RewardName} are calling with each other due to tag \"{lower_case_tag}\"", LineType.SoundCommand);
							else
								UniversalRewards.Add(lower_case_tag, entry);
						}
					}
				}
				else if (RewardsToSound.ContainsKey(entry.RewardID))
				{
					MainForm.Instance.ThreadSafeAddPreviewText($"Sound entry {entry.RewardName} already present in dictionary!", LineType.SoundCommand);
					continue;
				}
				else
					RewardsToSound.Add(entry.RewardID, entry);
			}
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

		public void SetDelay(int delay)
		{
			this.m_Delay = delay;
			PrivateSettings.GetInstance().Delay = delay;
		}

		public void PlaySoundIfExists(ES_ChannelPoints.ES_ChannelPointRedeemRequest redeem)
		{
			if (redeem.state != RedemptionStates.UNFULFILLED)
				return;

			if (redeem.user_id == null)
				return;

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
			if (!m_UserDB.ContainsKey(redeem.user_id))
			{
				m_UserDB.Add(redeem.user_id, DateTime.MinValue);
			}

			if (!string.IsNullOrEmpty(PrivateSettings.GetInstance().UniversalRewardID) && redeem.reward.id == PrivateSettings.GetInstance().UniversalRewardID)
			{
				if (m_UserDB[redeem.user_id] + TimeSpan.FromSeconds(m_Delay) < DateTime.Now)
				{
					if (UniversalRewards.TryGetValue(redeem.user_input.SanitizeTags().ToLower(), out SoundEntry universal_sound))
					{
						//Sound is found, is not played allocate a new player, start playing it, write down when user started playing a sound so he's under cooldown
						PrivateSettings programSettings = PrivateSettings.GetInstance();
						NSoundPlayer player = new NSoundPlayer(programSettings.OutputDevice, universal_sound.GetFile(m_RNG), programSettings.Volume * universal_sound.Volume);
						TimeSpan length = player.GetTimeLength() + TimeSpan.FromSeconds(1);
						m_SoundPlayerStack.Add(player);
						var additionalDelay = universal_sound.Cooldown - m_Delay;
						if (additionalDelay < 0)
							additionalDelay = 0;

						m_UserDB[redeem.user_id] = DateTime.Now + length + TimeSpan.FromSeconds(additionalDelay);
						MainForm.Instance.TwitchBot.HelixAPI_User.UpdateRedemptionStatus(redeem, RedemptionStates.FULFILLED);
					}
					else
						MainForm.Instance.TwitchBot.HelixAPI_User.UpdateRedemptionStatus(redeem, RedemptionStates.CANCELED);
				}
				else
					MainForm.Instance.TwitchBot.HelixAPI_User.UpdateRedemptionStatus(redeem, RedemptionStates.CANCELED);
			}
			else if (RewardsToSound.TryGetValue(redeem.reward.id, out SoundEntry sound))
			{
				//check user cooldown
				if (m_UserDB[redeem.user_id] + TimeSpan.FromSeconds(m_Delay) < DateTime.Now)
				{
					//Sound is found, is not played allocate a new player, start playing it, write down when user started playing a sound so he's under cooldown
					PrivateSettings programSettings = PrivateSettings.GetInstance();
					NSoundPlayer player = new NSoundPlayer(programSettings.OutputDevice, sound.GetFile(m_RNG), programSettings.Volume * sound.Volume);
					TimeSpan length = player.GetTimeLength() + TimeSpan.FromSeconds(1);
					m_SoundPlayerStack.Add(player);
					m_UserDB[redeem.user_id] = DateTime.Now + length;

					MainForm.Instance.TwitchBot.HelixAPI_User.UpdateRedemptionStatus(redeem, RedemptionStates.FULFILLED);
				}
				else
				{
					MainForm.Instance.TwitchBot.HelixAPI_User.UpdateRedemptionStatus(redeem, RedemptionStates.CANCELED);
				}
			}
		}

		public void Close()
		{
			for (int i = 0; i < m_SoundPlayerStack.Count; i++)
			{
				m_SoundPlayerStack[i].Dispose();
			}

			MainForm.Instance.TwitchEvents.OnChannelPointsRedeem -= PlaySoundIfExists;
		}

		public void Save()
		{
			SoundStorageXML.SaveSoundBase(m_SoundBaseFile, SoundList);
			RebuildDictionary();
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

		public TimeSpan GetTimeLength()
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
					if (VorbisFileReader == null)
						return true;
					else if (VorbisFileReader.CurrentTime >= VorbisFileReader.TotalTime)
						return true;
					else
						return false;
				default:
					if (GenericFileReader == null)
						return true;
					else if (GenericFileReader.CurrentTime >= GenericFileReader.TotalTime)
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
					directWaveOut?.Stop();
					GenericFileReader?.Dispose();
					VorbisFileReader?.Dispose();
					directWaveOut?.Dispose();
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
