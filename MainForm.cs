using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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
        public delegate void SetPreviewTextDelegate(string text, LineType type);       //used to safely handle the IRC output from bot class
        public delegate void SetVolumeSlider(int valuee);       //used to safely change the slider position

        IRC.IRCBot TwitchBot;
        private char PrefixCharacter = '-';
        Thread TwitchBotThread;
        PrivateSettings _programSettings;
        SoundBase soundDb;
        VSS.VSS_Entry_Group VSSdb;
        VSS_PreviewWindow VSSPreview;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _programSettings = PrivateSettings.LoadSettings();
            UpdateColors();
            connectOnStartupToolStripMenuItem.Checked = _programSettings.Autostart;
            int valrr = Convert.ToInt32(100 * _programSettings.Volume);
            trackBar_Volume.Value = valrr;
            L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
            soundDb = new SoundBase(Path.Combine("SoundDBs", "sounds.xml"), _programSettings);
            VSSdb = SoundStorage.VSSStorageXML.LoadVSSBase(Path.Combine("SoundDBs", "VSS.xml"));

            if (_programSettings.Autostart)
            {
                StartBot();
            }
        }

        private void StartBot()
        {
            TwitchBot = new IRC.IRCBot(this, _programSettings, soundDb, PrefixCharacter);
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
                RB_Preview.AppendText(text + "\n");
                RB_Preview.Select(RB_Preview.Text.Length - text.Length -1, text.Length);

                switch(type)
                {
                    case LineType.Generic:
                        RB_Preview.SelectionColor = _programSettings.Colors.LineColorGeneric;
                        break;

                    case LineType.IrcCommand:
                        RB_Preview.SelectionColor = _programSettings.Colors.LineColorIrcCommand;
                        break;

                    case LineType.ModCommand:
                        RB_Preview.SelectionColor = _programSettings.Colors.LineColorModeration;
                        break;


                    case LineType.SoundCommand:
                        RB_Preview.SelectionColor = _programSettings.Colors.LineColorSoundPlayback;
                        break;

                    default:
                        RB_Preview.SelectionColor = _programSettings.Colors.LineColorGeneric;
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
            if(TwitchBot != null)
                TwitchBot.StopBot();
            trayIcon.Visible = false;
            _programSettings.SaveSettings();
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
            _programSettings.Volume = trackBar_Volume.Value / 100f;
            if(TwitchBot != null)
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
            if(CastedSender.Checked)
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
            _programSettings.Autostart = CastedSender.Checked;
            _programSettings.SaveSettings();
        }

        private void ConnectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForms.ConnectionSettingsForm form = new SettingsForms.ConnectionSettingsForm(this, _programSettings);
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                _programSettings.TwitchServer = form.Server;
                _programSettings.TwitchUsername = form.Username;
                _programSettings.TwitchPassword = form.Password;
                _programSettings.TwitchChannelToJoin = form.ChannelToJoin;
                _programSettings.GoogleSpreadsheetID = form.SpreadsheetID;

                _programSettings.SaveSettings();
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
            SettingsForms.ColorSettingsForm csf = new SettingsForms.ColorSettingsForm(_programSettings);
            DialogResult res = csf.ShowDialog();
            if(res == DialogResult.OK)
            {
                _programSettings.Colors.FormBackground = csf.FormBackground;
                _programSettings.Colors.FormTextColor = csf.FormTextColor;
                _programSettings.Colors.MenuStripBarBackground = csf.MenuStripBarBackground;
                _programSettings.Colors.MenuStripBarText = csf.MenuStripBarText;
                _programSettings.Colors.MenuStripBackground = csf.MenuStripBackground;
                _programSettings.Colors.MenuStripText = csf.MenuStripText;
                _programSettings.Colors.MenuStripBackgroundSelected = csf.MenuStripBackgroundSelected;

                _programSettings.Colors.LineColorBackground = csf.LineColorBackground;
                _programSettings.Colors.LineColorGeneric = csf.LineColorGeneric;
                _programSettings.Colors.LineColorIrcCommand = csf.LineColorIrcCommand;
                _programSettings.Colors.LineColorModeration = csf.LineColorModeration;
                _programSettings.Colors.LineColorSoundPlayback = csf.LineColorSoundPlayback;
                _programSettings.SaveSettings();
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
            SoundDatabaseEditor.DB_Editor scf = new SoundDatabaseEditor.DB_Editor(this, soundDb.soundlist, PrefixCharacter, _programSettings);
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
            var CustomColorTable = new Extensions.OverridenColorTable()
            {
                UseSystemColors = false,
                ColorMenuBorder = Color.Black,
                ColorMenuBarBackground = _programSettings.Colors.MenuStripBarBackground,
                ColorMenuItemSelected = _programSettings.Colors.MenuStripBackgroundSelected,
                ColorMenuBackground = _programSettings.Colors.MenuStripBackground,

                TextColor = _programSettings.Colors.MenuStripText
            };

            this.BackColor = _programSettings.Colors.FormBackground;
            this.ForeColor = _programSettings.Colors.FormTextColor;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(CustomColorTable);
            menuStrip1.ForeColor = _programSettings.Colors.MenuStripBarText;
            ReColorChildren(menuStrip1);

            RB_Preview.BackColor = _programSettings.Colors.LineColorBackground;
            RB_Preview.Clear();
        }

        private void ReColorChildren(MenuStrip menuStrip1)
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[1].GetType() == typeof(ToolStripMenuItem))
                {
                    var TempCast = (ToolStripMenuItem)menuStrip1.Items[i];
                    foreach (ToolStripItem child in TempCast.DropDownItems)
                    {
                        child.BackColor = _programSettings.Colors.MenuStripBackground;
                        child.ForeColor = _programSettings.Colors.MenuStripText;
                    }
                }
            }
        }
        #endregion

        private void VSSEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VSS.VSS_BindingsEditor scf = new VSS.VSS_BindingsEditor(VSSdb);
            DialogResult res = scf.ShowDialog();
            if (res == DialogResult.OK)
            {
                SoundStorage.VSSStorageXML.SaveVSSBase(Path.Combine("SoundDBs", "VSS.xml"), scf.VSS_RootEntry);
            }
        }

        private void EnableVSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VSSPreview == null || VSSPreview.IsDisposed)
            {
                VSSPreview = new VSS_PreviewWindow(_programSettings, VSSdb);
                VSSPreview.Show();
            }
            else
                VSSPreview.Show();
        }

        private void tTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForms.TTSSettingsForm ttsSettings = new SettingsForms.TTSSettingsForm(this, _programSettings);
            var result = ttsSettings.ShowDialog();
            if(result == DialogResult.OK)
            {
                this._programSettings.TTSRoleRequirement = ttsSettings.RequiredRight;
                this._programSettings.TTSRewardID = ttsSettings.CustomRewardID;
                this._programSettings.VoiceSynthesizer = ttsSettings.VoiceSynthesizer;
                this._programSettings.TTSLogic = ttsSettings.TTSLogic;
                this._programSettings.SaveSettings();
            }
        }
    }
}
