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
    public partial class ExportDialog : Form
    {
        public ExportType ExportType { get; set; }

        public ExportDialog()
        {
            InitializeComponent();
            ExportType = ExportType.None;
        }

        private void B_ExportToSpreadsheet_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.ExportType = ExportType.GoogleDoc;
            this.Close();
        }

        private void B_ExportToCSV_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.ExportType = ExportType.HTML;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public enum ExportType
    {
        None,
        HTML,
        GoogleDoc
    }
}
