using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms
{
	public partial class UserOverrideAddForm : Form
	{
		public string Nickname { get; set; } = "";

		public UserOverrideAddForm()
		{
			InitializeComponent();
		}

		private void UserOverrideAddForm_Load(object sender, EventArgs e)
		{
			this.TB_Username.DataBindings.Add("Text", this, nameof(Nickname), false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			Nickname = Nickname.ToLower();
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
