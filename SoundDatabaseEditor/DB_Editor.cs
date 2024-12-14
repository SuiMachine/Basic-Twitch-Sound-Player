using BasicTwitchSoundPlayer.Extensions;
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

		public List<SoundEntry> SoundsCopy;
		char PrefixCharacter;

		public DB_Editor(List<SoundEntry> Sounds, char PrefixCharacter)
		{
			this.PrefixCharacter = PrefixCharacter;

			this.SoundsCopy = new List<SoundEntry>(Sounds.Count);
			foreach (SoundEntry SoundEntry in Sounds)
				this.SoundsCopy.Add(SoundEntry.CreateCopy());

			this.SoundsCopy = Sounds;
			InitializeComponent();
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
				var SndNode = GetRootSoundNode(sndTreeView.SelectedNode);
				sndTreeView.Nodes.Remove(SndNode);
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
					var parent = GetRootSoundNode(sndTreeView.SelectedNode);

					sndTreeView.Nodes.Remove(parent);
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
			sndTreeView.Sort();
		}

		private void B_SoundPlayBackSettings_Click(object sender, EventArgs e)
		{
			using (EditDialogues.SoundPlaybackSettingsDialog spsDialog = new EditDialogues.SoundPlaybackSettingsDialog())
			{
				var result = spsDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					var setings = PrivateSettings.GetInstance();
					setings.OutputDevice = spsDialog.SelectedDevice;
					setings.SaveSettings();
				}
			}
		}
	}

	#region Extensions
	static class EditorExtensions
	{
		public static TreeNode ToTreeNode(this SoundEntry snd)
		{
			if (snd.GetIsProperEntry())
			{
				var newNode = new TreeNode(snd.RewardName)
				{
					Name = DB_Editor.NodeNameEntry,
					ImageIndex = 0
				};

				var Description = newNode.Nodes.Add(DB_Editor.NodeDescription);
				Description.ImageIndex = 2;
				Description.SelectedImageIndex = 2;
				Description.StateImageIndex = 2;
				Description.Name = DB_Editor.NodeDescription;
				Description.Text = snd.Description;

				var FilesNode = newNode.Nodes.Add(DB_Editor.NodeNameFiles);
				FilesNode.ImageIndex = 1;
				FilesNode.SelectedImageIndex = 1;
				FilesNode.StateImageIndex = 1;
				FilesNode.Name = DB_Editor.NodeNameFiles;

				var VolumeNode = newNode.Nodes.Add(DB_Editor.NodeNameVolume);
				VolumeNode.ImageIndex = 3;
				VolumeNode.SelectedImageIndex = 3;
				VolumeNode.StateImageIndex = 3;
				VolumeNode.Name = DB_Editor.NodeNameVolume;
				VolumeNode.Text = snd.Volume.ToString("0%");

				var PointsNode = newNode.Nodes.Add(DB_Editor.NodeNamePoints);
				PointsNode.ImageIndex = 3;
				PointsNode.SelectedImageIndex = 3;
				PointsNode.StateImageIndex = 3;
				PointsNode.Name = DB_Editor.NodeNamePoints;
				PointsNode.Text = snd.AmountOfPoints.ToString();

				foreach (var file in snd.Files)
				{
					if (file.RemoveWhitespaces() != String.Empty)
					{
						var fNode = FilesNode.Nodes.Add(file);
						fNode.ImageIndex = 1;
						fNode.SelectedImageIndex = 1;
						fNode.StateImageIndex = 1;
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
