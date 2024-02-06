using System;
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
		HTML
	}
}
