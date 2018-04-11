using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace BasicTwitchSoundPlayer.SettingsForms
{
    public partial class ConnectionSettingsForm : Form
    {
        private MainForm _parent;
        private PrivateSettings _settingsRef;
        private HttpListener webListener;

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

        private void CloseHttpListener()
        {
            if(webListener != null && webListener.IsListening)
            {
                webListener.Stop();
            }
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            CloseHttpListener();
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            CloseHttpListener();
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

        private void B_GetLoginData_Click(object sender, EventArgs e)
        {
            webListener = new HttpListener();
            webListener.Prefixes.Add("http://127.0.0.1:43628/");
            webListener.Prefixes.Add("http://localhost:43628/");

            webListener.Start();
            Debug.WriteLine("WebListener started.");
            Process.Start("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token" +
                "&client_id=9z58zy6ak0ejk9lme6dy6nyugydaes" +
                "&redirect_uri=http://127.0.0.1:43628/resp.html" +
                "&scope=chat_login+channel_subscriptions");
            var context = webListener.GetContext();
            var response = context.Response;
            var responseText = new StreamReader(context.Request.InputStream).ReadToEnd();
            const string responseString = "Testing";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            Debug.WriteLine("Response written");
            CloseHttpListener();
        }
    }
}
