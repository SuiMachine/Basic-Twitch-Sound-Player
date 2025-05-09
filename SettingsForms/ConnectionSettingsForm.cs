﻿using BasicTwitchSoundPlayer.Structs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
	public partial class ConnectionSettingsForm : Form
	{
		private MainForm _parent;
		private HttpListener webListener;

		public string Server { get; set; }
		public string Username { get; set; }
		public string UserAuth { get; set; }
		public string BotName { get; set; }
		public string BotAuth { get; set; }

		public bool DebugMode { get; set; }
		public int WebsocketPort { get; set; }

		public bool RunWebsocket { get; set; }

		public ConnectionSettingsForm(MainForm _parent)
		{
			InitializeComponent();

			var settings = PrivateSettings.GetInstance();

			this._parent = _parent;

			this.TB_Server.DataBindings.Add("Text", this, nameof(Server), false, DataSourceUpdateMode.OnPropertyChanged);

			this.TB_Username.DataBindings.Add("Text", this, nameof(Username), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_UserAuth.DataBindings.Add("Text", this, nameof(UserAuth), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_BotName.DataBindings.Add("Text", this, nameof(BotName), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_BotAuth.DataBindings.Add("Text", this, nameof(BotAuth), false, DataSourceUpdateMode.OnPropertyChanged);
			this.CB_DebugMode.DataBindings.Add("Checked", this, nameof(DebugMode), false, DataSourceUpdateMode.OnPropertyChanged);
			this.Num_PortUsed.DataBindings.Add("Value", this, nameof(WebsocketPort), false, DataSourceUpdateMode.OnPropertyChanged);
			this.CB_Websocket.DataBindings.Add("Checked", this, nameof(RunWebsocket), false, DataSourceUpdateMode.OnPropertyChanged);

			this.Server = settings.TwitchServer;
			this.Username = settings.UserName;
			this.UserAuth = settings.UserAuth;
			this.BotName = settings.BotUsername;
			this.BotAuth = settings.BotAuth;

			this.DebugMode = settings.Debug_mode;
			this.WebsocketPort = settings.WebSocketsServerPort;
			this.RunWebsocket = settings.RunWebSocketsServer;
		}

		private void CloseHttpListener()
		{
			if (webListener != null && webListener.IsListening)
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
			if (castedSender.Checked)
			{
				DialogResult res = MessageBox.Show("Do you really want to display the password?\nDisplaying is not recommended when streaming, recording or sharing desktop!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (res == DialogResult.Yes)
					TB_UserAuth.UseSystemPasswordChar = false;
				else
					castedSender.Checked = false;
			}
			else
			{
				TB_UserAuth.UseSystemPasswordChar = true;
			}
		}

		private void CB_ShowBotAuth_CheckedChanged(object sender, EventArgs e)
		{
			var castedSender = (CheckBox)sender;
			if (castedSender.Checked)
			{
				DialogResult res = MessageBox.Show("Do you really want to display the password?\nDisplaying is not recommended when streaming, recording or sharing desktop!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (res == DialogResult.Yes)
					TB_BotAuth.UseSystemPasswordChar = false;
				else
					castedSender.Checked = false;
			}
			else
			{
				TB_BotAuth.UseSystemPasswordChar = true;
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

			if (isElevated)
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
					TB_UserAuth.Text = login;
					MessageBox.Show("Successfully received new login data!\nClick Save button to save the new authorization key.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Failed to obtain new login data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void B_GetLoginDataManual_Click(object sender, EventArgs e)
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

			if (isElevated)
			{
				webListener = new HttpListener();
				webListener.Prefixes.Add("http://127.0.0.1:43628/");
				webListener.Prefixes.Add("http://localhost:43628/");

				webListener.Start();
				Debug.WriteLine("WebListener started.");

				var urlToRun = "https://id.twitch.tv/oauth2/authorize?response_type=token" +
					"&client_id=9z58zy6ak0ejk9lme6dy6nyugydaes" +
					"&redirect_uri=http://127.0.0.1:43628/resp.html" +
					"&scope=chat_login+channel_subscriptions+channel:read:subscriptions+channel:read:redemptions+channel:manage:redemptions";
				Clipboard.SetText(urlToRun);
				MessageBox.Show("An url copied to your clipboard - paste it in your browser to set up bot auth", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
					TB_BotAuth.Text = login;
					MessageBox.Show("Successfully received new login data!\nClick Save button to save the new authorization key.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Failed to obtain new login data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
