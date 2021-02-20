using System;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.Speech.Synthesis;

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
        public string SpreadsheetID { get; set; }

        public ConnectionSettingsForm(MainForm _parent, PrivateSettings _settingsRef)
        {
            InitializeComponent();
            this._parent = _parent;
            this._settingsRef = _settingsRef;

            this.TB_Server.DataBindings.Add("Text", this, "Server", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_Username.DataBindings.Add("Text", this, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_Password.DataBindings.Add("Text", this, "Password", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_ChannelToJoin.DataBindings.Add("Text", this, "ChannelToJoin", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TB_GoogleSpreadsheetID.DataBindings.Add("Text", this, "SpreadsheetID", false, DataSourceUpdateMode.OnPropertyChanged);

            this.Server = _settingsRef.TwitchServer;
            this.Username = _settingsRef.TwitchUsername;
            this.Password = _settingsRef.TwitchPassword;
            this.ChannelToJoin = _settingsRef.TwitchChannelToJoin;
            this.SpreadsheetID = _settingsRef.GoogleSpreadsheetID;
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
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isElevated = true;

            if (!isElevated)
            {
                DialogResult result = MessageBox.Show("HttpListener generally requires administrator rights. You can try running it without them, but the program oftens fails. Are you sure you want to continue?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    isElevated = true;
            }

            if(isElevated)
            {
                webListener = new HttpListener();
                webListener.Prefixes.Add("http://127.0.0.1:43628/");
                webListener.Prefixes.Add("http://localhost:43628/");

                webListener.Start();
                Debug.WriteLine("WebListener started.");
                Process.Start("https://id.twitch.tv/oauth2/authorize?response_type=token" +
                    "&client_id=9z58zy6ak0ejk9lme6dy6nyugydaes" +
                    "&redirect_uri=http://127.0.0.1:43628/resp.html" +
                    "&scope=chat_login+channel_subscriptions+channel:read:subscriptions+channel:read:redemptions+channel:manage:redemptions");

                string pageContentRedirect = string.Join("\n", "<html>",
                        "<head>",
                        "<script>",
                        "window.onload = function() {",
                        "if(location.hash != null && location.hash.startsWith(\"#\"))",
                        "{",
                        "window.location.replace(\"http://127.0.0.1:43628/\" +  location.hash.replace(\'#\', \'&\'));",
                        "}",
                        "}",
                        "</script>",
                        "<title>Close it</title>",
                        "</head>",
                        "<body>You can probably close this page</body>",
                        "</html>");

                string requestUri = "";

                for (int i = 0; i < 2; i++)
                {
                    var context = webListener.GetContext();
                    var response = context.Response;
                    var responseText = new StreamReader(context.Request.InputStream).ReadToEnd();
                    var buffer = Encoding.UTF8.GetBytes(pageContentRedirect);
                    requestUri = context.Request.Url.ToString();
                    response.ContentLength64 = buffer.Length;
                    var output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                }
                webListener.Stop();
                string login = "";
                string scopes = "";

                if (requestUri.Contains("&"))
                {
                    var split = requestUri.Split('&');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i].StartsWith("access_token="))
                        {
                            login = split[i].Remove(0, "access_token=".Length);
                        }
                        else if (split[i].StartsWith("scope="))
                        {
                            scopes = split[i].Remove(0, "scope=".Length);
                        }
                    }
                }

                if (scopes != "" && login != "")
                {
                    TB_Password.Text = login;
                    MessageBox.Show("Sucessfully received new login data!\nClick Save button to save the new authorization key.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to obtain new login data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }
    }
}
