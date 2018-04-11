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
    public partial class AddNewEntryDialog : Form
    {
        public AddNewEntryDialog()
        {
            InitializeComponent();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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
        }
    }
}
