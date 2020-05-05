using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    public partial class SoundPlaybackSettingsDialog : Form
    {
        public Structs.SoundRedemptionLogic SoundLogic { get; set; }
        public string SoundRewardID { get; set; }
        public Guid SelectedDevice { get; set; }

        public SoundPlaybackSettingsDialog(PrivateSettings programSettings)
        {
            this.SoundRewardID = programSettings.SoundRewardID;
            this.SoundLogic = programSettings.SoundRedemptionLogic;
            this.SelectedDevice = programSettings.OutputDevice;
            InitializeComponent();

            //Initialization stuff and bindings
            this.AddComboboxDataSources();
            this.FillInOutputDevices();
            this.TB_SoundRewardID.DataBindings.Add("Text", this, "SoundRewardID", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CBox_RedemptionLogic.DataBindings.Add("SelectedValue", this, "SoundLogic", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void FillInOutputDevices()
        {
            bool deviceWasSelected = false;
            foreach (var dev in NAudio.Wave.DirectSoundOut.Devices)
            {
                CB_OutputDevices.Items.Add(dev.Description);
                if (dev.Guid == SelectedDevice && !deviceWasSelected)
                {
                    CB_OutputDevices.SelectedIndex = CB_OutputDevices.Items.Count - 1;
                    deviceWasSelected = true;
                }
            }

            if (!deviceWasSelected)
                CB_OutputDevices.SelectedIndex = 0;
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            if(SoundRewardID == "")
            {
                MessageBox.Show("No reward ID was provided, setting the redemption method to Legacy.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SoundLogic = Structs.SoundRedemptionLogic.Legacy;
            }

            foreach (var dev in NAudio.Wave.DirectSoundOut.Devices)
            {
                if (dev.Description == CB_OutputDevices.SelectedItem.ToString())
                {
                    SelectedDevice = dev.Guid;
                    break;
                }
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
            this.TB_SoundRewardID.Text = Logger.LastRewardID;
        }

        private void CBox_RedemptionLogic_SelectedIndexChanged(object sender, EventArgs e)
        {
            var enableOrDisable = SoundLogic == Structs.SoundRedemptionLogic.ChannelPoints;
            TB_SoundRewardID.Enabled = enableOrDisable;
            B_UseLastRewardID.Enabled = enableOrDisable;
        }
    }
}
