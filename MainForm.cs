using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
	public enum LineType
	{
		Generic,
		IrcCommand,
		ModCommand,
		SoundCommand
	}

	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }

		public delegate void SetPreviewTextDelegate(string text, LineType type);       //used to safely handle the IRC output from bot class
		public delegate void SetVolumeSlider(int valuee);       //used to safely change the slider position

		public IRC.IRCBot TwitchBot { get; private set; }
		private char PrefixCharacter = '-';
		Thread TwitchBotThread;
		SoundBase soundDb;

		public MainForm()
		{
			Instance = this;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var settings = PrivateSettings.GetInstance();
			UpdateColors();
			connectOnStartupToolStripMenuItem.Checked = settings.Autostart;
			int valrr = Convert.ToInt32(100 * settings.Volume);
			trackBar_Volume.Value = valrr;
			L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
			soundDb = new SoundBase(Path.Combine("SoundDBs", "sounds.xml"), settings);

			if (settings.Autostart)
			{
				StartBot();
			}
		}

		private void StartBot()
		{
			TwitchBot = new IRC.IRCBot(soundDb, PrefixCharacter);
			TwitchBotThread = new Thread(new ThreadStart(TwitchBot.Run));
			TwitchBotThread.Start();
		}

		#region ThreadSafeFunctions
		private void AddPreviewText(string text, LineType type)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.RB_Preview.InvokeRequired)
			{
				SetPreviewTextDelegate d = new SetPreviewTextDelegate(AddPreviewText);
				this.Invoke(d, new object[] { text, type });
			}
			else
			{
				var settings = PrivateSettings.GetInstance();
				RB_Preview.AppendText(text + "\n");
				RB_Preview.Select(RB_Preview.Text.Length - text.Length - 1, text.Length);

				switch (type)
				{
					case LineType.Generic:
						RB_Preview.SelectionColor = settings.Colors.LineColorGeneric;
						break;

					case LineType.IrcCommand:
						RB_Preview.SelectionColor = settings.Colors.LineColorIrcCommand;
						break;

					case LineType.ModCommand:
						RB_Preview.SelectionColor = settings.Colors.LineColorModeration;
						break;


					case LineType.SoundCommand:
						RB_Preview.SelectionColor = settings.Colors.LineColorSoundPlayback;
						break;

					default:
						RB_Preview.SelectionColor = settings.Colors.LineColorGeneric;
						break;

				}
			}
		}

		public void ThreadSafeAddPreviewText(string text, LineType type)
		{
			this.AddPreviewText(text, type);
		}

		private void MoveSlider(int value)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.trackBar_Volume.InvokeRequired)
			{
				SetVolumeSlider d = new SetVolumeSlider(MoveSlider);
				this.Invoke(d, new object[] { value });
			}
			else
			{
				trackBar_Volume.Value = value;
				L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
			}
		}

		public void ThreadSafeMoveSlider(int value)
		{
			this.MoveSlider(value);
		}


		private void PerformShutdownTasks()
		{
			if (TwitchBot != null)
				TwitchBot.StopBot();
			trayIcon.Visible = false;
			var settings = PrivateSettings.GetInstance();

			settings.SaveSettings();
			System.Environment.Exit(0);
		}
		#endregion

		#region EventHandlers
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			PerformShutdownTasks();
		}

		private void RB_Preview_TextChanged(object sender, EventArgs e)
		{
			RB_Preview.SelectionStart = RB_Preview.Text.Length;
			RB_Preview.ScrollToCaret();
		}

		private void TrackBar_Volume_Scroll(object sender, EventArgs e)
		{
			L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
			var settings = PrivateSettings.GetInstance();

			settings.Volume = trackBar_Volume.Value / 100f;
			if (TwitchBot != null)
				TwitchBot.UpdateVolume();
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				trayIcon.Visible = true;
				this.Hide();
			}
			else if (this.WindowState == FormWindowState.Normal)
			{
				trayIcon.Visible = false;
				this.Show();
			}
		}

		#region FileTree_Events
		private void RunBotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var CastedSender = (ToolStripMenuItem)sender;
			if (CastedSender.Checked)
			{
				StartBot();
			}
			else
			{
				StopBot();
			}
		}

		private void ConnectOnStartupToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			var CastedSender = (ToolStripMenuItem)sender;
			var settings = PrivateSettings.GetInstance();

			settings.Autostart = CastedSender.Checked;
			settings.SaveSettings();
		}

		private void ConnectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsForms.ConnectionSettingsForm form = new SettingsForms.ConnectionSettingsForm(this);

			var settings = PrivateSettings.GetInstance();

			DialogResult res = form.ShowDialog();
			if (res == DialogResult.OK)
			{
				settings.TwitchServer = form.Server;
				settings.TwitchUsername = form.Username;
				settings.TwitchPassword = form.Password;
				settings.TwitchChannelToJoin = form.ChannelToJoin;
				settings.Debug_mode = form.DebugMode;
				settings.SaveSettings();
				ReloadBot();
			}
		}

		private void ReloadBot()
		{
			if (TwitchBot != null && TwitchBot.BotRunning)
				StopBot();

			//TODO: Add update to SoundBase for VoiceSynthesizer
			StartBot();
		}

		private void StopBot()
		{
			if (TwitchBot != null && TwitchBot.BotRunning)
			{
				TwitchBot.StopBot();
			}

			TwitchBot = null;
			if (TwitchBotThread != null)
			{
				TwitchBotThread.Interrupt();
				TwitchBotThread.Abort();
			}

			TwitchBotThread = null;
		}

		private void ColorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsForms.ColorSettingsForm csf = new SettingsForms.ColorSettingsForm();
			DialogResult res = csf.ShowDialog();
			if (res == DialogResult.OK)
			{
				var settings = PrivateSettings.GetInstance();
				settings.Colors.FormBackground = csf.FormBackground;
				settings.Colors.FormTextColor = csf.FormTextColor;
				settings.Colors.MenuStripBarBackground = csf.MenuStripBarBackground;
				settings.Colors.MenuStripBarText = csf.MenuStripBarText;
				settings.Colors.MenuStripBackground = csf.MenuStripBackground;
				settings.Colors.MenuStripText = csf.MenuStripText;
				settings.Colors.MenuStripBackgroundSelected = csf.MenuStripBackgroundSelected;

				settings.Colors.LineColorBackground = csf.LineColorBackground;
				settings.Colors.LineColorGeneric = csf.LineColorGeneric;
				settings.Colors.LineColorIrcCommand = csf.LineColorIrcCommand;
				settings.Colors.LineColorModeration = csf.LineColorModeration;
				settings.Colors.LineColorSoundPlayback = csf.LineColorSoundPlayback;
				settings.SaveSettings();
				UpdateColors();
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.StopBot();
			this.Close();
		}
		#endregion

		#region SoundTree_Events
		private void DatabaseEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SoundDatabaseEditor.DB_Editor scf = new SoundDatabaseEditor.DB_Editor(soundDb.soundlist, PrefixCharacter);
			DialogResult res = scf.ShowDialog();
			if (res == DialogResult.OK)
			{
				soundDb.soundlist = scf.Sounds;
				soundDb.Save();
			}
		}

		private void ImportFromBotnakToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<BasicTwitchSoundPlayer.Structs.SoundEntry> importedEntries = BotnakImporter.ImportFiles();
			soundDb.Marge(importedEntries);
		}
		#endregion

		#region TrayIcon_Events
		private void ShowProgramToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}
		#endregion
		#endregion

		#region ColorThemeOverrideFunctions
		private void UpdateColors()
		{
			var settings = PrivateSettings.GetInstance();

			var CustomColorTable = new Extensions.OverridenColorTable()
			{
				UseSystemColors = false,
				ColorMenuBorder = Color.Black,
				ColorMenuBarBackground = settings.Colors.MenuStripBarBackground,
				ColorMenuItemSelected = settings.Colors.MenuStripBackgroundSelected,
				ColorMenuBackground = settings.Colors.MenuStripBackground,

				TextColor = settings.Colors.MenuStripText
			};

			this.BackColor = settings.Colors.FormBackground;
			this.ForeColor = settings.Colors.FormTextColor;
			menuStrip1.Renderer = new ToolStripProfessionalRenderer(CustomColorTable);
			menuStrip1.ForeColor = settings.Colors.MenuStripBarText;
			ReColorChildren(menuStrip1);

			RB_Preview.BackColor = settings.Colors.LineColorBackground;
			RB_Preview.Clear();
		}

		private void ReColorChildren(MenuStrip menuStrip1)
		{
			var settings = PrivateSettings.GetInstance();


			for (int i = 0; i < menuStrip1.Items.Count; i++)
			{
				if (menuStrip1.Items[1].GetType() == typeof(ToolStripMenuItem))
				{
					var TempCast = (ToolStripMenuItem)menuStrip1.Items[i];
					foreach (ToolStripItem child in TempCast.DropDownItems)
					{
						child.BackColor = settings.Colors.MenuStripBackground;
						child.ForeColor = settings.Colors.MenuStripText;
					}
				}
			}
		}
		#endregion

		private void tTSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsForms.TTSSettingsForm ttsSettings = new SettingsForms.TTSSettingsForm(this);
			var result = ttsSettings.ShowDialog();
			if (result == DialogResult.OK)
			{
				var settings = PrivateSettings.GetInstance();
				settings.TTSRoleRequirement = ttsSettings.RequiredRight;
				settings.TTSRewardID = ttsSettings.CustomRewardID;
				settings.VoiceSynthesizer = ttsSettings.VoiceSynthesizer;
				settings.TTSLogic = ttsSettings.TTSLogic;
				settings.SaveSettings();
			}
		}

		private void voiceModIntegrationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsForms.VoiceModIntegrationForm form = new SettingsForms.VoiceModIntegrationForm();
			var result = form.ShowDialog();
			if(result == DialogResult.OK)
			{
				
			}
		}
	}
}
