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

		private PrivateSettings SettingsReference;
		private MainForm mainFormReference;

		public TTSSettingsForm(MainForm mainFormReference, PrivateSettings settings)
		{
			this.VoiceSynthesizer = settings.VoiceSynthesizer;
			this.RequiredRight = settings.TTSRoleRequirement;
			this.CustomRewardID = settings.TTSRewardID;
			this.TTSLogic = settings.TTSLogic;
			this.SettingsReference = settings;
			this.mainFormReference = mainFormReference;
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
		}

		private void B_Test_Click(object sender, EventArgs e)
		{
			using (var synth = new SpeechSynthesizer())
			{
				synth.SelectVoice(CBox_VoiceSynthesizer.Text);
				synth.Volume = 100;
				synth.Rate = -2;
				synth.Speak(TB_ExampleText.Text);
			}
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			if (CustomRewardID == "")
			{
				MessageBox.Show("No Custom Reward Provided for TTS. Setting it to legacy method.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.TTSLogic = Structs.TTSLogic.RewardIDAndCommand;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void linkExplainLogic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/SuiMachine/Basic-Twitch-Sound-Player/wiki/Text-to-speech");
		}

		private async void B_VerifyPointsResponse_Click(object sender, EventArgs e)
		{
			IRC.KrakenConnections apiConnection = new IRC.KrakenConnections(SettingsReference.TwitchUsername, SettingsReference.TwitchPassword);

			await apiConnection.GetBroadcasterIDAsync();
			await apiConnection.VerifyChannelRewardsAsync(mainFormReference, null, TB_CustomRewardID.Text);
			MessageBox.Show("Results should be displayed in main chat window (Sorry, that was an afterthought)", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void CBox_TTSLogic_SelectedIndexChanged(object sender, EventArgs e)
		{
			var value = (Structs.TTSLogic)CBox_TTSLogic.SelectedValue;

			if (value == Structs.TTSLogic.Restricted)
			{
				B_VerifyPointsResponse.Enabled = true;
				B_CreateReward.Enabled = true;
			}
			else
			{
				B_VerifyPointsResponse.Enabled = false;
				B_CreateReward.Enabled = false;
			}
		}

		private async void B_CreateReward_Click(object sender, EventArgs e)
		{
			var dialogResult = MessageBox.Show("Are you sure you want to create a new Reward?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{
				IRC.KrakenConnections apiConnection = new IRC.KrakenConnections(SettingsReference.TwitchUsername, SettingsReference.TwitchPassword);
				var broadcasterTask = apiConnection.GetBroadcasterIDAsync();
				var result = await apiConnection.CreateRewardAsync(IRC.KrakenConnections.RewardType.TTS);
				if (result != null)
				{
					TB_CustomRewardID.Text = result.id;
					MessageBox.Show("Successfully created a new reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}
	}
}
