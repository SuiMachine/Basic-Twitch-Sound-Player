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


		public List<SoundEntry> Sounds;
        char PrefixCharacter;

        public DB_Editor(List<SoundEntry> Sounds, char PrefixCharacter)
        {
            this.PrefixCharacter = PrefixCharacter;
            this.Sounds = Sounds;
            InitializeComponent();
        }

        private void DB_Editor_Load(object sender, EventArgs e)
        {
            foreach(var Sound in Sounds)
            {
                sndTreeView.Nodes.Add(Sound.ToTreeNode());
            }
        }

        private TreeNode GetRootSoundNode(TreeNode Node)
        {
            while(Node.Parent != null)
            {
                Node = Node.Parent;
            }
            return Node;
        }

        private void EditEntry()
        {
            if (sndTreeView.SelectedNode != null)
            {
                var SndNode = GetRootSoundNode(sndTreeView.SelectedNode);
                int index = sndTreeView.SelectedNode.Index;
                var dialForm = new EditDialogues.AddEditNewEntryDialog(SndNode.ToSoundEntry());
                DialogResult res = dialForm.ShowDialog();

                if(res == DialogResult.OK)
                {
                    sndTreeView.Nodes[index].Remove();
                    sndTreeView.Nodes.Insert(index, dialForm.ReturnSound.ToTreeNode());
                }
            }
        }

        private void RemoveEntry()
        {
            if(sndTreeView.SelectedNode != null)
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
            this.Sounds = TreeNodesToSoundsEntryList(sndTreeView);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private List<SoundEntry> TreeNodesToSoundsEntryList(TreeView tree)
        {
            this.Sounds = new List<SoundEntry>();
            for(int i=0; i<sndTreeView.Nodes.Count; i++)
            {
                var newSnd = sndTreeView.Nodes[i].ToSoundEntry();
                if (newSnd.GetIsProperEntry())
                    Sounds.Add(newSnd);
            }
            return Sounds;

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
            if(e.KeyCode == Keys.Delete)
            {
                if(sndTreeView.SelectedNode != null)
                {
                    var parent = GetRootSoundNode(sndTreeView.SelectedNode);

                    sndTreeView.Nodes.Remove(parent);
                }
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if(sndTreeView.SelectedNode != null)
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
                if(result == DialogResult.OK)
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
        public static SoundEntry ToSoundEntry(this TreeNode node)
        {
            if (node.Name == DB_Editor.NodeNameEntry)
            {
                string Command = node.Text;
                string[] Files = new string[node.Nodes[DB_Editor.NodeNameFiles].Nodes.Count];
                string Description = node.Nodes[DB_Editor.NodeDescription].Text;

                for (int i = 0; i < Files.Length; i++)
                {
                    Files[i] = node.Nodes[DB_Editor.NodeNameFiles].Nodes[i].Text;
                }

                return new SoundEntry(Command, Description, "", Files, 1f);
            }
            else
                return new SoundEntry();
        }

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
				Description.ImageIndex = 1;
				Description.SelectedImageIndex = 1;
                Description.StateImageIndex = 1;
				Description.Name = DB_Editor.NodeDescription;
				Description.Text = snd.Description;

				var FilesNode = newNode.Nodes.Add(DB_Editor.NodeNameFiles);
                FilesNode.ImageIndex = 2;
                FilesNode.SelectedImageIndex = 2;
                FilesNode.StateImageIndex = 2;
                FilesNode.Name = DB_Editor.NodeNameFiles;

				var VolumeNome = newNode.Nodes.Add(DB_Editor.NodeNameVolume);
				VolumeNome.ImageIndex = 3;
				VolumeNome.SelectedImageIndex = 3;
				VolumeNome.StateImageIndex = 3;
				VolumeNome.Name = DB_Editor.NodeNameVolume;
                VolumeNome.Text = snd.Volume.ToString("0%");

				foreach (var file in snd.Files)
                {
                    if(file.RemoveWhitespaces() != String.Empty)
                    {
                        var fNode = FilesNode.Nodes.Add(file);
                        fNode.ImageIndex = 2;
                        fNode.SelectedImageIndex = 2;
                        fNode.StateImageIndex = 2;
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
