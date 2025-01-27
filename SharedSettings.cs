using BasicTwitchSoundPlayer.Interfaces;
using BasicTwitchSoundPlayer.Structs;
using BasicTwitchSoundPlayer.Structs.Gemini;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BasicTwitchSoundPlayer
{
	[Serializable]
	public class ColorStruct
	{
		//Form background
		[XmlElement]
		public ColorWrapper FormBackground { get; set; }
		[XmlElement]
		public ColorWrapper FormTextColor { get; set; }

		//MenuStripBar
		[XmlElement]
		public ColorWrapper MenuStripBarBackground { get; set; }
		[XmlElement]
		public ColorWrapper MenuStripBarText { get; set; }

		//MenuStripColors
		[XmlElement]
		public ColorWrapper MenuStripBackground { get; set; }
		[XmlElement]
		public ColorWrapper MenuStripText { get; set; }
		[XmlElement]
		public ColorWrapper MenuStripBackgroundSelected { get; set; }

		//LineColors
		[XmlElement]
		public ColorWrapper LineColorBackground { get; set; }
		[XmlElement]
		public ColorWrapper LineColorGeneric { get; set; }
		[XmlElement]
		public ColorWrapper LineColorIrcCommand { get; set; }
		[XmlElement]
		public ColorWrapper LineColorModeration { get; set; }
		[XmlElement]
		public ColorWrapper LineColorSoundPlayback { get; set; }
		[XmlElement]
		public ColorWrapper LineColorWebSocket { get; set; }

		public ColorStruct()
		{
			FormBackground = Color.WhiteSmoke;
			FormTextColor = Color.Black;

			MenuStripBarBackground = Color.WhiteSmoke;
			MenuStripBarText = Color.Black;

			MenuStripBackground = Color.WhiteSmoke;
			MenuStripText = Color.Black;
			MenuStripBackgroundSelected = Color.SkyBlue;

			LineColorBackground = Color.GhostWhite;
			LineColorGeneric = Color.Black;
			LineColorIrcCommand = Color.DarkGreen;
			LineColorModeration = Color.DarkBlue;
			LineColorSoundPlayback = Color.DarkOrange;
			LineColorWebSocket = Color.DarkMagenta;
		}
	}

	[Serializable]
	public class PrivateSettings
	{
		private static string GetConfigPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "Config.xml");
		private static PrivateSettings m_Instance;
		public static PrivateSettings GetInstance()
		{
			if (m_Instance == null)
				m_Instance = LoadSettings();

			return m_Instance;
		}

		#region Properties
		[XmlElement]
		public ColorStruct Colors { get; set; }

		[XmlElement]
		public bool Debug_mode { get; set; }
		[XmlElement]
		public bool Autostart { get; set; }
		[XmlElement]
		public float Volume { get; set; }
		[XmlElement]
		public int Delay { get; set; }
		[XmlElement]
		public Guid OutputDevice { get; set; }
		[XmlElement]
		public string TwitchServer { get; set; }
		[XmlElement]
		public string UserName { get; set; }
		[XmlElement]
		public string UserAuth { get; set; }
		[XmlElement]
		public string BotUsername { get; set; }
		[XmlElement]
		public string BotAuth { get; set; }
		[XmlElement]
		public bool RunWebSocketsServer { get; set; }
		[XmlElement]
		public int WebSocketsServerPort { get; set; }
		#endregion

		public PrivateSettings()
		{
			//NOTE: Make sure everything is initialized first!
			Debug_mode = false;
			Autostart = false;
			Volume = 0.5f;
			Delay = 15;
			this.Colors = new ColorStruct();

			TwitchServer = "irc.twitch.tv";
			UserName = "";
			UserAuth = "";
			BotUsername = "";
			BotAuth = "";
			RunWebSocketsServer = false;
			WebSocketsServerPort = 8005;
		}

		#region Load/Save
		private static PrivateSettings LoadSettings()
		{
			var path = GetConfigPath();
			if (File.Exists(path))
			{
				PrivateSettings obj;
				XmlSerializer serializer = new XmlSerializer(typeof(PrivateSettings));
				FileStream fs = new FileStream(path, FileMode.Open);
				obj = (PrivateSettings)serializer.Deserialize(fs);
				fs.Close();
				return obj;
			}
			else
				return new PrivateSettings();
		}

		public void SaveSettings() => XML_Utils.Save(GetConfigPath(), this);
		#endregion
	}

	[Serializable]
	public class VoiceModConfig
	{
		private const string CONFIGFILE = "VoiceModConfig.xml";
		private static VoiceModConfig m_Instance;
		public static VoiceModConfig GetInstance()
		{
			if (m_Instance == null)
				m_Instance = LoadSettings();

			return m_Instance;
		}

		private static VoiceModConfig LoadSettings()
		{
			if (File.Exists(CONFIGFILE))
			{
				VoiceModConfig obj;
				XmlSerializer serializer = new XmlSerializer(typeof(VoiceModConfig));
				FileStream fs = new FileStream(CONFIGFILE, FileMode.Open);
				obj = (VoiceModConfig)serializer.Deserialize(fs);
				fs.Close();
				return obj;
			}
			else
				return new VoiceModConfig();
		}

		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(VoiceModConfig));
			StreamWriter fw = new StreamWriter(CONFIGFILE);
			serializer.Serialize(fw, this);
			fw.Close();
		}

		public string APIKey { get; set; }
		public string AdressPort { get; set; }

		public List<VoiceModReward> Rewards { get; set; } = new List<VoiceModReward>();
		private Dictionary<string, VoiceModReward> IDToReward;

		public VoiceModReward GetReward(string rewardID)
		{
			if (IDToReward == null)
			{
				IDToReward = new Dictionary<string, VoiceModReward>();
				foreach (var reward in Rewards)
				{
					if (string.IsNullOrEmpty(reward.RewardID))
						continue;
					if (IDToReward.ContainsKey(reward.RewardID))
						continue;

					IDToReward.Add(reward.RewardID, reward);
				}
			}

			if (IDToReward.TryGetValue(rewardID, out var foundReward))
				return foundReward;
			else
				return null;
		}

		public VoiceModConfig()
		{
			APIKey = "";
			AdressPort = "ws://localhost:59129/v1";
		}

		[Serializable]
		public class VoiceModReward : IVoiceModeRewardBindingInterface
		{
			[XmlAttribute]
			public string VoiceModFriendlyName { get; set; }
			[XmlAttribute]
			public string RewardTitle { get; set; }
			[XmlAttribute]
			public string RewardID { get; set; }
			[XmlAttribute]
			public int RewardCost { get; set; }
			[XmlAttribute]
			public int RewardCooldown { get; set; }
			[XmlAttribute]
			public int RewardDuration { get; set; }
			[XmlAttribute]
			public bool Enabled { get; set; }
			[XmlText]
			public string RewardDescription { get; set; }
			[XmlIgnore]
			public bool IsSetup = false;

			public VoiceModReward()
			{
				VoiceModFriendlyName = "";
				RewardTitle = "";
				RewardID = "";
				RewardCost = 240;
				RewardDuration = 30;
				RewardCooldown = 1;
				Enabled = true;
				RewardDescription = "";
			}

			public object Clone()
			{
				return new VoiceModReward()
				{
					VoiceModFriendlyName = this.VoiceModFriendlyName,
					RewardTitle = this.RewardTitle,
					RewardID = this.RewardID,
					RewardCost = this.RewardCost,
					RewardCooldown = this.RewardCooldown,
					RewardDuration = this.RewardDuration,
					Enabled = this.Enabled,
					RewardDescription = this.RewardDescription
				};
			}
		}
	}

	[Serializable]
	public class AIConfig
	{
		[Serializable]
		public class FilterSet
		{
			public AISafetySettingsValues Harassment { get; set; } = AISafetySettingsValues.BlockFew;
			public AISafetySettingsValues Hate { get; set; } = AISafetySettingsValues.BlockFew;
			public AISafetySettingsValues Sexually_Explicit { get; set; } = AISafetySettingsValues.BlockFew;
			public AISafetySettingsValues Dangerous_Content { get; set; } = AISafetySettingsValues.BlockFew;
			public AISafetySettingsValues Civic_Integrity { get; set; } = AISafetySettingsValues.BlockMost;
		}

		private static string GetConfigPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "AI_Config.xml");
		public static string GetAIHistory(string username) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BasicTwitchSoundPlayer", "AI_History", username + ".xml" );

		private static AIConfig m_Instance;
		public static AIConfig GetInstance()
		{
			if (m_Instance == null)
				m_Instance = LoadSettings();

			return m_Instance;
		}

		public string ApiKey { get; set; } = "";
		public string Instruction_Streamer { get; set; } = "The responses are always 200-550 characters long.";
		public int TokenLimit_Streamer { get; set; } = 1_048_576 - 8096 - 512;
		public FilterSet FilterSet_Streamer = new FilterSet()
		{
			Harassment = AISafetySettingsValues.BlockNone,
			Hate = AISafetySettingsValues.BlockNone,
			Sexually_Explicit = AISafetySettingsValues.BlockNone,
			Dangerous_Content = AISafetySettingsValues.BlockNone,
			Civic_Integrity = AISafetySettingsValues.BlockMost,
		};
		public string Instruction_User { get; set; } = "The user is {0}\nThe responses are always 200-450 characters long.";
		public int TokenLimit_User { get; set; } = 8096;
		public FilterSet FilterSet_User = new FilterSet()
		{
			Harassment = AISafetySettingsValues.BlockSome,
			Hate = AISafetySettingsValues.BlockSome,
			Sexually_Explicit = AISafetySettingsValues.BlockSome,
			Dangerous_Content = AISafetySettingsValues.BlockSome,
			Civic_Integrity = AISafetySettingsValues.BlockMost,
		};

		public string Model { get; set; } = "models/gemini-2.0-flash-exp";
		public string TwitchAwardID { get; set; } = "";

		private static AIConfig LoadSettings() => XML_Utils.Load(GetConfigPath(), new AIConfig());

		public void SaveSettings() => XML_Utils.Save(GetConfigPath(), this);
	}
}
