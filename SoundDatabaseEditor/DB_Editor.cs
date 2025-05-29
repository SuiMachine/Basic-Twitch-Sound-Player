using BasicTwitchSoundPlayer.Extensions;
using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SoundStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor
{
	public partial class DB_Editor : Form
	{
		public const string NodeNameEntry = "Entry";
		public const string NodeDescription = "Description";
		public const string NodeNameFiles = "Files";
		public const string NodeNameVolume = "Volume";
		public const string NodeNamePoints = "Points cost";

		public static DB_Editor Instance { get; private set; }
		public List<SoundEntry> SoundsCopy;

		public DB_Editor(List<SoundEntry> Sounds)
		{
			this.SoundsCopy = new List<SoundEntry>(Sounds.Count);
			foreach (SoundEntry SoundEntry in Sounds)
				this.SoundsCopy.Add(SoundEntry.CreateCopy());

			this.SoundsCopy = Sounds;
			InitializeComponent();
			Instance = this;
		}

		private void DB_Editor_FormClosed(object sender, FormClosedEventArgs e)
		{
			Instance = null;
		}

		private void DB_Editor_Load(object sender, EventArgs e)
		{
			foreach (var Sound in SoundsCopy)
			{
				sndTreeView.Nodes.Add(Sound.ToTreeNode());
			}
		}

		private TreeNode GetRootSoundNode(TreeNode Node)
		{
			while (Node.Parent != null)
			{
				Node = Node.Parent;
			}
			return Node;
		}

		private void EditEntry()
		{
			if (sndTreeView.SelectedNode != null)
			{
				TreeNode SndNode = GetRootSoundNode(sndTreeView.SelectedNode);
				int index = sndTreeView.SelectedNode.Index;
				EditDialogues.AddEditNewEntryDialog dialForm = new EditDialogues.AddEditNewEntryDialog(SoundsCopy[index]);
				DialogResult res = dialForm.ShowDialog();

				if (res == DialogResult.OK)
				{
					sndTreeView.Nodes[index].Remove();
					SoundsCopy[index] = dialForm.ReturnSound;
					sndTreeView.Nodes.Insert(index, dialForm.ReturnSound.ToTreeNode());
				}
			}
		}

		private void RemoveEntry()
		{
			if (sndTreeView.SelectedNode != null)
			{
				var id = sndTreeView.SelectedNode.Index;
				SoundsCopy.RemoveAt(id);
				sndTreeView.Nodes.RemoveAt(id);
			}
		}

		#region ButtonEvents
		private void SndTreeView_DoubleClick(object sender, EventArgs e)
		{
			var SelectedNode = sndTreeView.SelectedNode;
			if (SelectedNode != null)
			{
				var RootSndNode = GetRootSoundNode(SelectedNode);
				sndTreeView.SelectedNode = RootSndNode;
				EditEntry();
			}
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void B_AddEntry_Click(object sender, EventArgs e)
		{
			EditDialogues.AddEditNewEntryDialog newEntryDialog = new EditDialogues.AddEditNewEntryDialog();
			DialogResult res = newEntryDialog.ShowDialog();
			if (res == DialogResult.OK)
			{
				SoundsCopy.Add(newEntryDialog.ReturnSound);
				sndTreeView.Nodes.Add(newEntryDialog.ReturnSound.ToTreeNode());
			}
		}


		private void B_Edit_Click(object sender, EventArgs e)
		{
			EditEntry();
		}

		private void B_RemoveEntry_Click(object sender, EventArgs e)
		{
			RemoveEntry();
		}
		#endregion

		#region PressKeyEvents
		private void SndTreeView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (sndTreeView.SelectedNode != null)
				{
					RemoveEntry();
				}
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (sndTreeView.SelectedNode != null)
				{
					EditEntry();
				}
			}
		}
		#endregion

		private void B_Sort_Click(object sender, EventArgs e)
		{
			SoundsCopy = SoundsCopy.OrderBy(x => x.RewardName).ToList();
			sndTreeView.Nodes.Clear();
			foreach (var Sound in SoundsCopy)
			{
				sndTreeView.Nodes.Add(Sound.ToTreeNode());
			}
		}

		private void B_SoundPlayBackSettings_Click(object sender, EventArgs e)
		{
			using (EditDialogues.SoundPlaybackSettingsDialog spsDialog = new EditDialogues.SoundPlaybackSettingsDialog())
			{
				var result = spsDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					var settings = PrivateSettings.GetInstance();
					settings.OutputDevice = spsDialog.SelectedDevice;
					settings.SaveSettings();
				}
			}
		}

		private void B_VerifyUniversalReward_Click(object sender, EventArgs e)
		{
			Action content = new Action(async () =>
			{
				var settings = PrivateSettings.GetInstance();

				var api = new SuiBot_TwitchSocket.API.HelixAPI(ChatBot.BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, null, settings.UserAuth);
				var authentication = api.ValidateToken();
				if (authentication != SuiBot_TwitchSocket.API.HelixAPI.ValidationResult.Successful)
				{
					MessageBox.Show("API authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (!await api.CreateRewardsCache())
				{
					MessageBox.Show("Failed to get rewards.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var foundReward = api.RewardsCache.FirstOrDefault(x => x.id == settings.UniversalRewardID);
				if (foundReward != null)
				{
					MessageBox.Show("A reward already exists and wasn't updated", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					var result = await api.CreateOrUpdateReward(null, "Universal sound reward", "Redeem a sound using tag / phrase", 160, 0, true, true);
					if (result == null)
					{
						if (string.IsNullOrEmpty(settings.UniversalRewardID))
							MessageBox.Show("A reward was missing and was created - make sure this is OK", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
							MessageBox.Show("Created a reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
						settings.UniversalRewardID = result.id;
					}
				}

				DialogBoxes.ProgressDisplay.Instance?.InvokeClose();
			});

			DialogBoxes.ProgressDisplay progressForm = DialogBoxes.ProgressDisplay.CreateIfNeeded().SetupForm(this, "Creating/Verifying universal reward", content);
		}

		private void B_ExportTags_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Name | Description | Tags / Phrases (comma separated)");
			sb.AppendLine("");

			foreach (SoundEntry sound in SoundsCopy)
			{
				if (!string.IsNullOrEmpty(sound.RewardID))
					continue;

				if (sound.Tags.Length == 0)
					continue;

				var joinTags = string.Join(", ", sound.Tags);

				sb.AppendLine($"{sound.RewardName} | {sound.Description} |  {joinTags}");
			}

			System.IO.File.WriteAllText("Tags.txt", sb.ToString());
			System.Diagnostics.Process.Start("Tags.txt");
		}
	}

	#region Extensions
	static class EditorExtensions
	{
		enum TreeIcons
		{
			None = 0,
			RewardSound = 0,
			TagSound = 1,
			Files = 0,
			Points = 2,
			Description = 3,
			Volume = 4,
		}

		public static TreeNode ToTreeNode(this SoundEntry snd)
		{
			if (snd.GetIsProperEntry())
			{
				var mainIcon = !string.IsNullOrEmpty(snd.RewardID) ? (int)TreeIcons.RewardSound : (int)TreeIcons.TagSound;

				var newNode = new TreeNode(snd.RewardName)
				{
					Name = DB_Editor.NodeNameEntry,
					ImageIndex = mainIcon,
					SelectedImageIndex = mainIcon,
					StateImageIndex = mainIcon,
				};

				var Description = newNode.Nodes.Add(DB_Editor.NodeDescription);
				Description.ImageIndex = (int)TreeIcons.Description;
				Description.SelectedImageIndex = (int)TreeIcons.Description;
				Description.StateImageIndex = (int)TreeIcons.Description;
				Description.Name = DB_Editor.NodeDescription;
				Description.Text = snd.Description;

				var FilesNode = newNode.Nodes.Add(DB_Editor.NodeNameFiles);
				FilesNode.ImageIndex = (int)TreeIcons.Files;
				FilesNode.SelectedImageIndex = (int)TreeIcons.Files;
				FilesNode.StateImageIndex = (int)TreeIcons.Files;
				FilesNode.Name = DB_Editor.NodeNameFiles;

				var VolumeNode = newNode.Nodes.Add(DB_Editor.NodeNameVolume);
				VolumeNode.ImageIndex = (int)TreeIcons.Volume;
				VolumeNode.SelectedImageIndex = (int)TreeIcons.Volume;
				VolumeNode.StateImageIndex = (int)TreeIcons.Volume;
				VolumeNode.Name = DB_Editor.NodeNameVolume;
				VolumeNode.Text = snd.Volume.ToString("0%");

				var PointsNode = newNode.Nodes.Add(DB_Editor.NodeNamePoints);
				PointsNode.ImageIndex = (int)TreeIcons.Points;
				PointsNode.SelectedImageIndex = (int)TreeIcons.Points;
				PointsNode.StateImageIndex = (int)TreeIcons.Points;
				PointsNode.Name = DB_Editor.NodeNamePoints;
				PointsNode.Text = snd.AmountOfPoints.ToString();

				foreach (var file in snd.Files)
				{
					if (file.RemoveWhitespaces() != String.Empty)
					{
						var fNode = FilesNode.Nodes.Add(file);
						fNode.ImageIndex = mainIcon;
						fNode.SelectedImageIndex = mainIcon;
						fNode.StateImageIndex = mainIcon;
					}
				}

				return newNode;
			}
			else
				throw new Exception(snd.RewardName + " is an incorrect entry!");
		}
	}
	#endregion
}
