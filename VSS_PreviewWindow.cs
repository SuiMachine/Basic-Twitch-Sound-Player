using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
    public partial class VSS_PreviewWindow : Form
    {
        PrivateSettings _programSettings { get; set; }
        VSS.VSS_Entry_Group VSSdb { get; set; }
        LiveSplit.Model.Input.LowLevelKeyboardHook lowLevelHook;
        VSS.VSS_Entry_Group CurrentNode { get; set; }
        private Task PollKeys;
        private VSS_Preview preview;
        private NAudio.Wave.WaveOut directWaveOut;

        public VSS_PreviewWindow(PrivateSettings _programSettings, VSS.VSS_Entry_Group VSSdb)
        {
            InitializeComponent();
            lowLevelHook = new LiveSplit.Model.Input.LowLevelKeyboardHook();
            this._programSettings = _programSettings;
            this.VSSdb = new VSS.VSS_Entry_Group("ROOTNODE", Keys.NoName);
            this.VSSdb.Nodes.Add(VSSdb);
            this.CurrentNode = VSSdb;
            UpdateColors();
            preview = new VSS_Preview(pictureBoxVSSPreview, Color.Black, Color.Yellow, 24, Color.White, 18, Color.WhiteSmoke, 4);
            UnregisterAllHotkeys();
            SetToRoot();
            PollKeys = Task.Factory.StartNew(HookPoll);
            lowLevelHook.KeyPressed += Hook_KeyPressed;
        }

        private void HookPoll()
        {
            while (true)
            {
                Thread.Sleep(25);
                try
                {
                    if (lowLevelHook != null)
                        lowLevelHook.Poll();
                }
                catch { }
            }
        }

        private void RegisterHotkey(Keys key)
        {
            Debug.WriteLine("Registered " + key);
            lowLevelHook.RegisterHotKey(key);
        }

        public void UnregisterAllHotkeys()
        {
            Debug.WriteLine("Unregistered All Hotkeys");
            lowLevelHook.UnregisterAllHotkeys();
        }

        private void Hook_KeyPressed(object sender, KeyEventArgs e)
        {
            foreach(var child in CurrentNode.Nodes)
            {
                if(((VSS.VSS_Entry)child).Hotkey == e.KeyCode && !e.Handled)
                {
                    e.Handled = true;
                    if (child.GetType() == typeof(VSS.VSS_Entry_Group))
                    {
                        var cast = (VSS.VSS_Entry_Group)child;
                        this.preview.Clear();
                        this.UnregisterAllHotkeys();
                        CurrentNode = cast;
                        AddDisplayElement(CurrentNode);
                    }
                    else
                    {
                        this.preview.Clear();
                        this.UnregisterAllHotkeys();
                        PlaySound(((VSS.VSS_Entry_Sound)child).Filepath);
                        CurrentNode = VSSdb;
                        AddDisplayElement(CurrentNode);
                    }
                }
            }
        }

        private void PlaySound(string filepath)
        {

            if (File.Exists(filepath))
            {
                Task.Factory.StartNew(() =>
                {
                    AudioFileReader audioFileReader = new AudioFileReader(filepath);
                    directWaveOut.Init(audioFileReader);
                    directWaveOut.Play();
                });
            }
        }

        private void UpdateColors()
        {
            this.BackColor = _programSettings.Colors.FormBackground;
            this.ForeColor = _programSettings.Colors.FormTextColor;
        }

        private void AddDisplayElement(VSS.VSS_Entry_Group NodeToDrawFrom)
        {
            foreach(var child in NodeToDrawFrom.Nodes)
            {
                var cast = (VSS.VSS_Entry)child;
                this.preview.AddElement(cast.Hotkey, cast.Description);
                RegisterHotkey(cast.Hotkey);
            }
            preview.UpdateView();
        }

        private void VSS_PreviewWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterAllHotkeys();
        }

        private void ResetTimer_Tick(object sender, EventArgs e)
        {
            lowLevelHook.UnregisterAllHotkeys();
            SetToRoot();
        }

        private void SetToRoot()
        {
            CurrentNode = VSSdb;
            AddDisplayElement(CurrentNode);
        }
    }

    public struct VSS_Preview
    {
        private PictureBox PictureBoxVSSPreview { get; set; }
        private Image Img { get; set; }
        private Graphics GBuffer { get; set; }
        private float LastElementTop;

        private Color BackgroundColor { get; set; }
        private Color KeyColor { get; set; }
        public float KeySize { get; }
        private Color DescriptionColor { get; set; }
        public float DescriptionSize { get; }
        private Color GenericTextColor { get; set; }
        public float BetweenEntriesGap { get; }


        public VSS_Preview(PictureBox PictureBoxVSSPreview, Color BackgroundColor, Color KeyColor, float KeySize, Color DescriptionColor, float DescriptionSize, Color GenericTextColor, float BetweenEntriesGap)
        {
            this.PictureBoxVSSPreview = PictureBoxVSSPreview;
            this.Img = new Bitmap(PictureBoxVSSPreview.Width, PictureBoxVSSPreview.Height);
            GBuffer = Graphics.FromImage(Img);
            LastElementTop = 0;

            this.BackgroundColor = BackgroundColor;
            this.KeyColor = KeyColor;
            this.KeySize = KeySize;
            this.DescriptionColor = DescriptionColor;
            this.DescriptionSize = DescriptionSize;
            this.GenericTextColor = GenericTextColor;
            this.BetweenEntriesGap = BetweenEntriesGap;
            Clear();
        }

        public void AddElement(Keys key, string Description)
        {
            Font FontKeyText = new Font("Arial", KeySize, FontStyle.Bold);
            Font FontDescriptionText = new Font("Arial", DescriptionSize, FontStyle.Regular);

            GBuffer.DrawString("[", FontKeyText, new SolidBrush(GenericTextColor), 0, LastElementTop);
            GBuffer.DrawString(key.ToString(), FontKeyText, new SolidBrush(KeyColor), 16, LastElementTop);
            GBuffer.DrawString("]", FontKeyText, new SolidBrush(GenericTextColor), 32, LastElementTop);
            GBuffer.DrawString(Description, FontDescriptionText, new SolidBrush(GenericTextColor), 48, LastElementTop);

            LastElementTop += (KeySize + BetweenEntriesGap);
        }

        public void Clear()
        {
            GBuffer.Clear(BackgroundColor);
            LastElementTop = 0;
            UpdateView();
        }

        public void UpdateView()
        {
            PictureBoxVSSPreview.Image = Img;
        }
    }
}
