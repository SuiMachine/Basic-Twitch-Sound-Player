using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicTwitchSoundPlayer.Structs;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor
{
    public partial class DB_Editor : Form
    {
        private const string FilesNodeName = "Files";
        private const string RequirementsNodeName = "Requirement";

        public List<SoundEntry> Sounds;

        public DB_Editor(List<SoundEntry> Sounds)
        {
            this.Sounds = Sounds;
            InitializeComponent();
        }

        private void DB_Editor_Load(object sender, EventArgs e)
        {
            foreach(var Sound in Sounds)
            {
                var newNode = sndTreeView.Nodes.Add(Sound.GetCommand());
                var FilesNode = newNode.Nodes.Add(FilesNodeName);
                FilesNode.Name = FilesNodeName;
                foreach(var file in Sound.GetAllFiles())
                {
                    FilesNode.Nodes.Add(file);
                }
                var Requirement = newNode.Nodes.Add(RequirementsNodeName);
                Requirement.Name = RequirementsNodeName;
                Requirement.Nodes.Add(Sound.GetRequirement().ToString());
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

        private void SndTreeView_DoubleClick(object sender, EventArgs e)
        {
            var SelectedNode = sndTreeView.SelectedNode;
            if(SelectedNode.Parent != null && SelectedNode.Nodes.Count == 0)
            {
                if(SelectedNode.Parent.Name == FilesNodeName)
                {
                    var fileDialog = new OpenFileDialog
                    {
                        Filter = SupportedFileFormats.Filter
                    };
                    fileDialog.FilterIndex = SupportedFileFormats.LastIndex;

                    DialogResult res = fileDialog.ShowDialog();
                    if(res == DialogResult.OK)
                    {
                        SelectedNode.Text = fileDialog.FileName;
                    }
                }
                else if (SelectedNode.Parent.Name == RequirementsNodeName)
                {
                    var PermissionDialog = new EditDialogues.EditDialog_Right(SelectedNode.Text.ToTwitchRights());
                    DialogResult res = PermissionDialog.ShowDialog();
                    if(res == DialogResult.OK)
                    {
                        SelectedNode.Text = PermissionDialog.RetPermission.ToString();
                    }
                }
            }
        }

        private void SndTreeView_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void B_AddEntry_Click(object sender, EventArgs e)
        {
            EditDialogues.AddNewEntryDialog newEntryDialog = new EditDialogues.AddNewEntryDialog();
            newEntryDialog.ShowDialog();
        }

        private void B_RemoveEntry_Click(object sender, EventArgs e)
        {

        }

        private void SndTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }
    }
}
