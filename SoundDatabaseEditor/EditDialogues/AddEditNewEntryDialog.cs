using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SoundStorage;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
	public partial class AddEditNewEntryDialog : Form
	{
		public SoundEntry ReturnSound { get; set; }
		public static AddEditNewEntryDialog Instance { get; private set; }

		public AddEditNewEntryDialog()
		{
			InitializeComponent();
			this.Text = "Add new entry";
		}

		public AddEditNewEntryDialog(SoundEntry Entry)
		{
			InitializeComponent();
			this.Text = "Entry editing";
			this.TB_RewardName.Text = Entry.RewardName;
			foreach (var sound in Entry.Files)
			{
				ListB_Files.Items.Add(sound);
			}
			this.RB_Description.Text = Entry.Description;
			this.TB_RewardID.Text = Entry.RewardID;
			this.Num_Points.Value = Entry.AmountOfPoints;
			this.Num_Cooldown.Value = Entry.Cooldown;
			this.Num_Volume.Value = (int)Math.Round(Entry.Volume * 100);
			this.RB_Tags.Lines = Entry.Tags;
			Verify();
			Instance = this;
		}

		private void AddEditNewEntryDialog_Load(object sender, EventArgs e)
		{
			Verify();
		}

		private void AddEditNewEntryDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Instance = null;
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			var listFile = ListB_Files.Items;
			string[] files = new string[listFile.Count];
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = listFile[i].ToString();
			}

			string[] listTags = RB_Tags.Lines.Select(x => x.SanitizeTags()).Where(x => x != "").ToArray();
			this.ReturnSound = new SoundEntry(TB_RewardName.Text, RB_Description.Text, TB_RewardID.Text, files, listTags, (float)Num_Volume.Value / 100f, (int)Num_Points.Value, (int)Num_Cooldown.Value);
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDial = new OpenFileDialog
			{
				Filter = SupportedFileFormats.Filter,
				FilterIndex = SupportedFileFormats.LastIndex,
				Multiselect = true
			};

			DialogResult res = fileDial.ShowDialog();
			if (res == DialogResult.OK)
			{
				this.ListB_Files.Items.AddRange(fileDial.FileNames);
			}
			Verify();
		}

		private void RemoveFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selectedItems = this.ListB_Files.SelectedItems;
			var selected = new string[selectedItems.Count];
			for (int i = 0; i < selected.Length; i++)
			{
				selected[i] = (string)selectedItems[i];
			}

			foreach (var item in selected)
			{
				this.ListB_Files.Items.Remove(item);
			}
			Verify();
		}

		private void TB_Command_TextChanged(object sender, EventArgs e)
		{
			Verify();
		}

		private void Verify()
		{
			if (TB_RewardName.Text == String.Empty)
			{
				B_OK.Enabled = false;
				return;
			}

			if (ListB_Files.Items.Count == 0)
			{
				B_OK.Enabled = false;
				return;
			}

			for (int i = 0; i < ListB_Files.Items.Count; i++)
			{
				if (ListB_Files.Items[i].ToString() == String.Empty)
				{
					B_OK.Enabled = false;
					return;
				}
			}

			B_OK.Enabled = true;
		}

		#region DragAndDropForListBox
		private void ListB_Files_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (files.All(x => SupportedFileFormats.IsAcceptableAudioFormat(x)))
					e.Effect = DragDropEffects.Copy;
				else
					e.Effect = DragDropEffects.None;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private void ListB_Files_DragDrop(object sender, DragEventArgs e)
		{
			string[] fToAdd = (string[])e.Data.GetData(DataFormats.FileDrop);
			ListB_Files.Items.AddRange(fToAdd);
			Verify();
		}
		#endregion

		private void RB_Description_TextChanged(object sender, EventArgs e)
		{
			Verify();
		}

		private async void B_CreateReward_Click(object sender, EventArgs e)
		{
			var mbResult = MessageBox.Show("Are you sure you want to create or update the reward?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (mbResult == DialogResult.No)
				return;

			var settings = PrivateSettings.GetInstance();
			var api = new SuiBot_TwitchSocket.API.HelixAPI(ChatBot.BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, null, settings.UserAuth);
			var validation = api.ValidateToken();

			if (validation != SuiBot_TwitchSocket.API.HelixAPI.ValidationResult.Successful)
			{
				MessageBox.Show("Failed to validate user token!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!await api.CreateRewardsCache())
			{
				MessageBox.Show("Failed to create reward cache!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var award = api.RewardsCache.FirstOrDefault(x => x.id == TB_RewardID.Text);
			if (award != null)
			{
				var update = await api.CreateOrUpdateReward(award.id, TB_RewardName.Text, RB_Description.Text, (int)Num_Points.Value, (int)Num_Cooldown.Value, true, false);
				if (update != null)
				{
					MessageBox.Show("Updated the reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					MessageBox.Show("Failed to update a reward", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				var reward = await api.CreateOrUpdateReward(null, TB_RewardName.Text, RB_Description.Text, (int)Num_Points.Value, (int)Num_Cooldown.Value, true, false);
				if (reward != null)
				{
					if (string.IsNullOrEmpty(TB_RewardID.Text))
						MessageBox.Show("Created a reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
					else
						MessageBox.Show("A reward was missing and was created - make sure this is OK", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.TB_RewardID.Text = reward.id;
				}
				else
					MessageBox.Show("Failed to create a reward", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void B_RemoveReward_Click(object sender, EventArgs e)
		{
			if (TB_RewardID.Text == "")
				return;

			var mbResult = MessageBox.Show("Are you sure you want to remove the reward?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (mbResult == DialogResult.No)
				return;

			var settings = PrivateSettings.GetInstance();
			var api = new SuiBot_TwitchSocket.API.HelixAPI(ChatBot.BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, null, settings.UserAuth);
			var validation = api.ValidateToken();

			if (validation != SuiBot_TwitchSocket.API.HelixAPI.ValidationResult.Successful)
			{
				MessageBox.Show("Failed to validate user token!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!await api.CreateRewardsCache())
			{
				MessageBox.Show("Failed to create reward cache!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var reward = api.RewardsCache.FirstOrDefault(x => x.id == TB_RewardID.Text);
			if (reward != null)
			{
				var delete = await api.DeleteCustomReward(reward);
				if (delete)
				{
					MessageBox.Show("Reward deleted successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
					TB_RewardID.Text = "";
				}
				else
				{
					MessageBox.Show("Failed to deleted award", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("Seems like the reward was already removed on Twitch!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				TB_RewardID.Text = "";
			}
		}
	}
}
