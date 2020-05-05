using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
    public partial class TTSSettingsForm : Form
    {
        public string VoiceSynthesizer { get; set; }
        public string CustomRewardID { get; set; }
        public Structs.TwitchRightsEnum RequiredRight { get; set; }
        public Structs.TTSLogic TTSLogic { get; set; }

        public Guid OutputDeviceGuid;

        public TTSSettingsForm(PrivateSettings settings)
        {
            this.VoiceSynthesizer = settings.VoiceSynthesizer;
            this.RequiredRight = settings.TTSRoleRequirement;
            this.CustomRewardID = settings.TTSRewardID;
            this.TTSLogic = settings.TTSLogic;
            InitializeComponent();
            this.AddComboboxDataSources();

            //Initialization stuff here!
            using (var synthesizer = new SpeechSynthesizer())
            {
                var voices = synthesizer.GetInstalledVoices();
                foreach (var voice in voices)
                {
                    CBox_VoiceSynthesizer.Items.Add(voice.VoiceInfo.Name);
                }
            }

            //bindings
            this.CBox_VoiceSynthesizer.DataBindings.Add("Text", this, "VoiceSynthesizer", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_CustomRewardID.DataBindings.Add("Text", this, "CustomRewardID", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CBox_RequiredRole.DataBindings.Add("SelectedValue", this, "RequiredRight", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CBox_TTSLogic.DataBindings.Add("SelectedValue", this, "TTSLogic", false, DataSourceUpdateMode.OnPropertyChanged);
            TTSExplanation_Tooltip.SetToolTip(linkExplainLogic, "There are 2 logics available.\n" +
                "1st - Require role & reward ID - means that the bot will check if correct reward ID was used and then it will check if the user has minimum specified role for reward (not recommended)\n" +
                "2nd - Use role for !tts - means that anyone who uses the reward will be able to use the TTS and additionally anyone with a specified role (or higher) will be able to use !tts command (recommended)");

        }

        private void B_Test_Click(object sender, EventArgs e)
        {
            using(var synth = new SpeechSynthesizer())
            {
                synth.SelectVoice(CBox_VoiceSynthesizer.Text);
                synth.Volume = 100;
                synth.Rate = -2;
                synth.Speak(TB_ExampleText.Text);
            }
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            if(CustomRewardID == "")
            {
                MessageBox.Show("No Custom Reward Provided for TTS. Setting it to legacy method.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.TTSLogic = Structs.TTSLogic.Restricted;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void B_UseLastRewardID_Click(object sender, EventArgs e)
        {
            this.CustomRewardID = Logger.LastRewardID;
        }
    }
}
