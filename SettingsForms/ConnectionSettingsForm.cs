using SSC.Chat;
using SuiBot_TwitchSocket.API;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SSC.SettingsForms
{
	public partial class ConnectionSettingsForm : Form
	{
		private readonly string[] SCOPES = new string[]
		{
			"channel:bot",
			"channel:read:ads",
			"channel:read:goals",
			"channel:read:guest_star",
			"channel:read:polls",
			"channel:manage:polls",
			"channel:read:predictions",
			"channel:manage:predictions",
			"channel:read:redemptions",
			"channel:manage:redemptions",
			"channel:read:subscriptions",
			"channel:read:goals",
			"channel:moderate",
			"moderation:read",
			"moderator:manage:announcements",
			"moderator:manage:automod",
			"moderator:read:banned_users",
			"moderator:manage:banned_users",
			"moderator:read:chat_messages",
			"moderator:manage:chat_messages",
			"moderator:manage:chat_settings",
			"moderator:read:chatters",
			"moderator:read:followers",
			"moderator:read:guest_star",
			"moderator:read:moderators",
			"moderator:manage:shoutouts",
			"moderator:read:suspicious_users",
			"moderator:read:vips",
			"moderator:manage:warnings",
			"user:bot",
			"user:read:chat",
			"user:read:emotes",
			"user:read:subscriptions",
			"user:write:chat",
		};

		private MainForm _parent;
		public string UserAuth { get; set; }
		public string BotAuth { get; set; }

		public bool DebugMode { get; set; }
		public int WebsocketPort { get; set; }
		public bool RunWebsocket { get; set; }
		public string MixItUp_WebookURL { get; set; }


		public ConnectionSettingsForm(MainForm _parent)
		{
			InitializeComponent();

			var settings = PrivateSettings.GetInstance();

			this._parent = _parent;

			this.TB_UserAuth.DataBindings.Add("Text", this, nameof(UserAuth), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_BotAuth.DataBindings.Add("Text", this, nameof(BotAuth), false, DataSourceUpdateMode.OnPropertyChanged);
			this.CB_DebugMode.DataBindings.Add("Checked", this, nameof(DebugMode), false, DataSourceUpdateMode.OnPropertyChanged);
			this.Num_PortUsed.DataBindings.Add("Value", this, nameof(WebsocketPort), false, DataSourceUpdateMode.OnPropertyChanged);
			this.CB_Websocket.DataBindings.Add("Checked", this, nameof(RunWebsocket), false, DataSourceUpdateMode.OnPropertyChanged);
			this.TB_MixItUpWebhook.DataBindings.Add("Text", this, nameof(MixItUp_WebookURL), false, DataSourceUpdateMode.OnPropertyChanged);

			this.UserAuth = settings.UserAuth;
			this.BotAuth = settings.BotAuth;

			this.DebugMode = settings.Debug_mode;
			this.WebsocketPort = settings.WebSocketsServerPort;
			this.RunWebsocket = settings.RunWebSocketsServer;
			this.MixItUp_WebookURL = settings.MixItUpWebookURL;
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
			var url = HelixAPI.GenerateAuthenticationURL(ChatBot.SSC_CLIENT_ID, "https://suimachine.github.io/twitchauthy/", SCOPES);
			Process.Start(url);
		}

		private void B_GetLoginDataManual_Click(object sender, EventArgs e)
		{
			var url = HelixAPI.GenerateAuthenticationURL(ChatBot.SSC_CLIENT_ID, "https://suimachine.github.io/twitchauthy/", SCOPES);
			Clipboard.SetText(url);
			MessageBox.Show("An url copied to your clipboard - paste it in your browser to set up bot auth", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
