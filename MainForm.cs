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
        Thread TwitchBotThread;
        PrivateSettings _programSettings;
        SoundBase soundDb;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadColors();
            _programSettings = new PrivateSettings();
            connectOnStartupToolStripMenuItem.Checked = _programSettings.Autostart;
            int valrr = Convert.ToInt32(100 * _programSettings.Volume);
            trackBar_Volume.Value = valrr;
            L_Volume.Text = trackBar_Volume.Value.ToString() + "%";
            soundDb = new SoundBase(Path.Combine("SoundDBs", "sounds.xml"), _programSettings);
            TwitchBot = new IRC.IRCBot(this, _programSettings, soundDb);
            if(_programSettings.Autostart)
            {
                TwitchBotThread = new Thread(new ThreadStart(TwitchBot.Run));
                TwitchBotThread.Start();
            }
        }

        private void LoadColors()
        {
            this.BackColor = Color.FromArgb(15, 15, 15);
            menuStrip1.BackColor = Color.FromArgb(15, 15, 15);
            menuStrip1.ForeColor = Color.WhiteSmoke;

            RB_Preview.BackColor = Color.FromArgb(30, 30, 30);
            RB_Preview.ForeColor = Color.WhiteSmoke;
            L_Volume.ForeColor = Color.WhiteSmoke;
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
                        RB_Preview.SelectionColor = Color.WhiteSmoke;
                        break;

                    case LineType.IrcCommand:
                        RB_Preview.SelectionColor = Color.LimeGreen;
                        break;

                    case LineType.ModCommand:
                        RB_Preview.SelectionColor = Color.Red;
                        break;


                    case LineType.SoundCommand:
                        RB_Preview.SelectionColor = Color.Purple;
                        break;

                    default:
                        RB_Preview.SelectionColor = Color.WhiteSmoke;
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
            TwitchBot.StopBot();
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
            TwitchBot.UpdateVolume();
        }



        #region FileTree_Events
        private void RunBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var CastedSender = (ToolStripMenuItem)sender;
            if(CastedSender.Checked)
            {
                TwitchBotThread = new Thread(new ThreadStart(TwitchBot.Run));
                TwitchBotThread.Start();
            }
            else
            {
                TwitchBot.StopBot();
                TwitchBotThread.Abort();
                TwitchBotThread = null;
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

                _programSettings.SaveSettings();
                TwitchBot.Reload();
            }
        }

        private void ColorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Color settings not implemented");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SoundTree_Events
        private void DatabaseEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoundDatabaseEditor.DB_Editor scf = new SoundDatabaseEditor.DB_Editor(soundDb.soundlist);
            DialogResult res = scf.ShowDialog();
            if (res == DialogResult.OK)
            {

            }
        }

        private void ImportFromBotnakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BasicTwitchSoundPlayer.Structs.SoundEntry> importedEntries = BotnakImporter.ImportFiles();
            soundDb.Marge(importedEntries);
        }
        #endregion

        #endregion
    }
}
