using BasicTwitchSoundPlayer.Structs;
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
    public partial class AddEditNewEntryDialog : Form
    {
        public SoundEntry ReturnSound { get; set; }

        public AddEditNewEntryDialog()
        {
            InitializeComponent();
            AddComboboxDataSources();
            this.Text = "Add new entry";
        }

        public AddEditNewEntryDialog(SoundEntry Entry)
        {
            InitializeComponent();
            AddComboboxDataSources();
            this.Text = "Entry editing";
            this.TB_Command.Text = Entry.GetCommand();
            this.CBox_RequiredRight.SelectedIndex = (int)Entry.GetRequirement();
            foreach(var sound in Entry.GetAllFiles())
            {
                ListB_Files.Items.Add(sound);
            }
            this.RB_Description.Text = Entry.GetDescription();
            Verify();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            var listFile = ListB_Files.Items;
            string[] files = new string[listFile.Count];
            for(int i=0; i<files.Length; i++)
            {
                files[i] = listFile[i].ToString();
            }

            this.ReturnSound = new SoundEntry(TB_Command.Text, (TwitchRightsEnum)CBox_RequiredRight.SelectedIndex, files, RB_Description.Text, DateTime.UtcNow);

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
            if(res == DialogResult.OK)
            {
                this.ListB_Files.Items.AddRange(fileDial.FileNames);
            }
            Verify();
        }

        private void RemoveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItems = this.ListB_Files.SelectedItems;
            var selected = new string[selectedItems.Count];
            for(int i=0; i<selected.Length; i++)
            {
                selected[i] = (string)selectedItems[i];
            }

            foreach(var item in selected)
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
            if(TB_Command.Text == String.Empty)
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
                if(ListB_Files.Items[i].ToString() == String.Empty)
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
    }
}
