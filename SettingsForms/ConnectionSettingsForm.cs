using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
    public partial class ConnectionSettingsForm : Form
    {
        private MainForm _parent;
        private PrivateSettings _settingsRef;

        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ChannelToJoin { get; set; }

        public ConnectionSettingsForm(MainForm _parent, PrivateSettings _settingsRef)
        {
            InitializeComponent();
            this._parent = _parent;
            this._settingsRef = _settingsRef;

            this.TB_Server.DataBindings.Add("Text", this, "Server", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_Username.DataBindings.Add("Text", this, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_Password.DataBindings.Add("Text", this, "Password", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_ChannelToJoin.DataBindings.Add("Text", this, "ChannelToJoin", false, DataSourceUpdateMode.OnPropertyChanged);

            this.Server = _settingsRef.TwitchServer;
            this.Username = _settingsRef.TwitchUsername;
            this.Password = _settingsRef.TwitchPassword;
            this.ChannelToJoin = _settingsRef.TwitchChannelToJoin;
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CB_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            var castedSender = (CheckBox)sender;
            if(castedSender.Checked)
            {
                DialogResult res = MessageBox.Show("Do you really want to display the password?\nDisplaying is not recommended when streaming, recording or sharing desktop!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    TB_Password.UseSystemPasswordChar = false;
                }
                else
                    castedSender.Checked = false;
            }
            else
            {
                TB_Password.UseSystemPasswordChar = true;
            }
        }
    }
}
