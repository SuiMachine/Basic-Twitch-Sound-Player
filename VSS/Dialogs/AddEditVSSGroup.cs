using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS.Dialogs
{
	public partial class AddEditVSSGroup : Form
	{
		public Keys ReturnKey { get; set; }
		public string NameDesc { get; set; }

		public AddEditVSSGroup(bool isEdit = false)
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;

			if (isEdit)
			{
				this.Text = "Edit VSS Group Entry";
			}
			else
			{
				this.Text = "Add new VSS Group Entry";
				ReturnKey = Keys.None;
				NameDesc = "";
			}

			TB_Name.DataBindings.Add("Text", this, "NameDesc", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Key.DataBindings.Add("Text", this, "ReturnKey", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void B_SetKey_Click(object sender, EventArgs e)
		{
			Dialogs.PressKeyDialog dlg = new PressKeyDialog();
			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				this.ReturnKey = dlg.ReturnedKey;
				this.TB_Key.Text = ReturnKey.ToString();
			}
		}
	}
}
