using BasicTwitchSoundPlayer.IRC;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static BasicTwitchSoundPlayer.IRC.KrakenConnections;

namespace BasicTwitchSoundPlayer
{
	public enum LineType
	{
		Generic,
		IrcCommand,
		ModCommand,
		SoundCommand,
		VoiceMod,
		WebSocket,
		GeminiAI
	}

	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }

		public delegate void SetPreviewTextDelegate(string text, LineType type);       //used to safely handle the IRC output from bot class
		public delegate void SetVolumeSlider(int value);       //used to safely change the slider position

		public IRCBot TwitchBot { get; private set; }
		private char PrefixCharacter = '-';
		Thread TwitchBotThread;
		SoundDB soundDb;
		GeminiAI AI;
		public static TwitchSocket TwitchSocket { get; private set; }
		WebSocketsListener webSockets;

		public MainForm()
		{
			Instance = this;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var settings = PrivateSettings.GetInstance();
			webSockets = new WebSocketsListener();
			TwitchSocket = new TwitchSocket();
			AI = new GeminiAI();
			UpdateColors();
			connectOnStartupToolStripMenuItem.Checked = settings.Autostart;
			int valrr = Convert.ToInt32(100 * settings.Volume);
			trackBar_Volume.Value = valrr;
			L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
			soundDb = new SoundDB();

			if (settings.Autostart)
			{
				StartBot();
			}

			if (settings.RunWebSocketsServer)
				webSockets.Start();
		}

		private void StartBot()
		{
			TwitchBot = new IRC.IRCBot(soundDb, PrefixCharacter);
			TwitchBotThread = new Thread(new ThreadStart(TwitchBot.Run));
			TwitchBotThread.Start();
			TwitchSocket.OnChannelPointsRedeem += OnRedeemUpdatedReceived;
			if (AI.IsConfigured())
			{
				AI.Register();
			}
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
					case LineType.WebSocket:
						RB_Preview.SelectionColor = settings.Colors.LineColorWebSocket;
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

		public void SetVolume(int value)
		{
			if (this.trackBar_Volume.InvokeRequired)
			{
				SetVolumeSlider d = new SetVolumeSlider(SetVolume);
				this.Invoke(d, new object[] { value });
			}
			else
			{
				trackBar_Volume.Value = value;
				L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
			}
		}

		public int GetVolume() => trackBar_Volume.Value;

		private void PerformShutdownTasks()
		{
			if (TwitchBot != null)
				TwitchBot.StopBot();
			trayIcon.Visible = false;
			var settings = PrivateSettings.GetInstance();
			settings.SaveSettings();
			this.webSockets.Stop();

			System.Environment.Exit(0);
		}

		private void OnRedeemUpdatedReceived(ChannelPointRedeemRequest redeem)
		{
#if DEBUG
			//Debug.WriteLine($"Received reward status {rewardId}, redeeem ID {redeemID} - {status}");
#endif
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
				settings.UserName = form.Username;
				settings.UserAuth = form.UserAuth;
				settings.BotUsername = form.BotName;
				settings.BotAuth = form.BotAuth;
				settings.Debug_mode = form.DebugMode;
				settings.WebSocketsServerPort = form.WebsocketPort;
				settings.RunWebSocketsServer = form.RunWebsocket;
				settings.SaveSettings();
				ReloadBot();
			}
		}

		private void ReloadBot()
		{
			webSockets.Stop();
			if (PrivateSettings.GetInstance().RunWebSocketsServer)
				webSockets.Start();

			if (TwitchBot != null && TwitchBot.BotRunning)
				StopBot();

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

			if (TwitchSocket != null)
				TwitchSocket.OnChannelPointsRedeem -= OnRedeemUpdatedReceived;
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
			this.webSockets.Stop();
			this.Close();
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

		private void DatabaseEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SoundDatabaseEditor.DB_Editor scf = new SoundDatabaseEditor.DB_Editor(soundDb.SoundList, PrefixCharacter);
			DialogResult res = scf.ShowDialog();
			if (res == DialogResult.OK)
			{
				soundDb.SoundList = scf.SoundsCopy;
				soundDb.Save();
			}
		}

		private void VoiceModSettings_Click(object sender, EventArgs e)
		{
			SettingsForms.VoiceModIntegrationForm form = new SettingsForms.VoiceModIntegrationForm();
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
			{

			}
		}

		private void GeminiAIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingsForms.AI_Integration_Form ai_form = new SettingsForms.AI_Integration_Form();
			var result = ai_form.ShowDialog();
			if (result == DialogResult.OK)
			{

			}
		}
	}
}
