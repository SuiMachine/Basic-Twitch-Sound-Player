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
			Verify();
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

			this.ReturnSound = new SoundEntry(TB_RewardName.Text, RB_Description.Text, TB_RewardID.Text, files, (float)Num_Volume.Value / 100f, (int)Num_Points.Value, (int)Num_Cooldown.Value);
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
			if (string.IsNullOrEmpty(TB_RewardID.Text))
			{
				var settings = PrivateSettings.GetInstance();
				IRC.KrakenConnections apiConnection = new IRC.KrakenConnections(settings.TwitchUsername, settings.TwitchPassword);
				await apiConnection.GetBroadcasterIDAsync();
				if (string.IsNullOrEmpty(apiConnection.BroadcasterID))
					return;

				await apiConnection.GetRewardsList();

				var reward = await apiConnection.CreateOrUpdateReward(new SoundEntry(TB_RewardName.Text, RB_Description.Text, TB_RewardID.Text, new string[] { }, 1f, (int)Num_Points.Value, (int)Num_Cooldown.Value));
				if (reward != null)
				{
					this.TB_RewardID.Text = reward.id;
				}
			}
		}
	}
}
