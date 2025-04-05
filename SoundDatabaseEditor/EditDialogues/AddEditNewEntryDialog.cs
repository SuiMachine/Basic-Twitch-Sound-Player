using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SoundStorage;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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

			string[] listTags = RB_Tags.Lines.Select(x => SanitizeTag(x)).Where(x => x != "").ToArray();
			this.ReturnSound = new SoundEntry(TB_RewardName.Text, RB_Description.Text, TB_RewardID.Text, files, listTags, (float)Num_Volume.Value / 100f, (int)Num_Points.Value, (int)Num_Cooldown.Value);
			this.Close();
		}

		private string SanitizeTag(string tag)
		{
			tag = Regex.Replace(tag, "[^a-zA-Z0-9|\\s]", "");
			while(tag.Contains("  "))
				tag = tag.Replace("  ", " ");
			return tag.Trim();
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
			var settings = PrivateSettings.GetInstance();
			KrakenConnections apiConnection = new KrakenConnections(settings.UserName);
			await apiConnection.GetBroadcasterIDAsync();
			if (string.IsNullOrEmpty(apiConnection.BroadcasterID))
				return;

			await apiConnection.GetRewardsList();

			KrakenConnections.ChannelReward reward = await apiConnection.CreateOrUpdateReward(new SoundEntry(TB_RewardName.Text, RB_Description.Text, TB_RewardID.Text, new string[] { }, new string[] { }, 1f, (int)Num_Points.Value, (int)Num_Cooldown.Value));

			if (reward != null)
			{
				if (string.IsNullOrEmpty(TB_RewardID.Text))
				{
					this.TB_RewardID.Text = reward.id;
					MessageBox.Show("Created a reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else if (TB_RewardID.Text != reward.id)
				{
					this.TB_RewardID.Text = reward.id;
					MessageBox.Show("A reward was missing and was created - make sure this is OK", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					MessageBox.Show("A reward was updated!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
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
			KrakenConnections apiConnection = new KrakenConnections(settings.UserName);
			await apiConnection.GetBroadcasterIDAsync();
			if (string.IsNullOrEmpty(apiConnection.BroadcasterID))
				return;

			var rewards = await apiConnection.GetRewardsList();

			KrakenConnections.ChannelReward reward = rewards.FirstOrDefault(x => x.id == TB_RewardID.Text);
			if (reward != null)
			{
				var result = await apiConnection.DeleteCustomReward(reward);
				if(result)
				{
					MessageBox.Show("Reward deleted successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
					TB_RewardID.Text = "";
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
