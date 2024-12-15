using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms.EditForm
{
	public partial class VoiceModAddForm : Form
	{
		private List<VoiceModHandling.VoiceInformation> Voices;
		public VoiceModHandling.VoiceInformation SelectedVoice { get; private set; }
		public string RewardName { get; set; }
		public string Description { get; set; }
		public int Price { get; set; } = 240;
		public int Duration { get; set; } = 30;
		public int Cooldown { get; set; } = 1;

		public VoiceModAddForm()
		{
			InitializeComponent();
			TB_Reward_Name.DataBindings.Add("Text", this, nameof(RewardName), false, DataSourceUpdateMode.OnPropertyChanged);
			RB_Description.DataBindings.Add("Text", this, nameof(Description), false, DataSourceUpdateMode.OnPropertyChanged);
			NumBox_Price.DataBindings.Add("Value", this, nameof(Price), false, DataSourceUpdateMode.OnPropertyChanged);
			NumBox_Duration.DataBindings.Add("Value", this, nameof(Duration), false, DataSourceUpdateMode.OnPropertyChanged);
			NumBox_Cooldown.DataBindings.Add("Value", this, nameof(Cooldown), false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void VoiceModAddForm_Load(object sender, EventArgs e)
		{
			Voices = VoiceModHandling.GetInstance().VoicesAvailable.Select(x => x.Value).ToList();
			ComboBox_Voice.Items.Clear();
			foreach(var element in Voices)
			{
				ComboBox_Voice.Items.Add(element.FriendlyName);
			}
			if (Voices.Count > 0)
			{
				ComboBox_Voice.SelectedIndex = 0;
				SelectedVoice = Voices[0];
			}
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ComboBox_Voice_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedVoice = Voices[ComboBox_Voice.SelectedIndex];
		}

		private void B_Generate_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to do that?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if(result == DialogResult.Yes)
			{
				RewardName = $"Set voice to \"{SelectedVoice.FriendlyName}\"";
				Description = $"Set the voice to \"{SelectedVoice.FriendlyName}\" for {Duration} seconds (powered by VoiceMod)";
				TB_Reward_Name.Text = RewardName;
				RB_Description.Text = Description;
			}
		}
	}
}
