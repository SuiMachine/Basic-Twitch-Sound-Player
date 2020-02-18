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

        public SoundPlaybackSettingsDialog(PrivateSettings programSettings)
        {
            this.SoundRewardID = programSettings.SoundRewardID;
            this.SoundLogic = programSettings.SoundRedemptionLogic;
            InitializeComponent();

            //Initialization stuff and bindings
            this.AddComboboxDataSources();
            this.TB_SoundRewardID.DataBindings.Add("Text", this, "SoundRewardID", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CBox_RedemptionLogic.DataBindings.Add("SelectedValue", this, "SoundLogic", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            if(SoundRewardID == "")
            {
                MessageBox.Show("No reward ID was provided, setting the redemption method to Legacy.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SoundLogic = Structs.SoundRedemptionLogic.Legacy;
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
