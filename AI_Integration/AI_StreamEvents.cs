using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.AI_Integration
{
	public partial class AI_StreamEvents : Form
	{
		public AI_StreamEvents()
		{
			InitializeComponent();
		}

		private void AI_StreamEvents_Load(object sender, EventArgs e)
		{
			var config = AIConfig.GetInstance().Events;
			RB_AdsStarted_Instruction.Text = config.Instruction_AdsBegin;
			RB_AdsFinished_Instruction.Text = config.Instruction_AdsFinished;
			RB_Ads_PreRollsNowActive.Text = config.Instruction_NotifyPrerolls;
			CB_AdsStarted.Checked = config.AdsBeginNotify;
			CB_AdsFinished.Checked = config.AdsFinishNotify;
			CB_Ads_PreRollsNowActive.Checked = config.AdsPrerollsActiveNotify;
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			var aiConfig = AIConfig.GetInstance();

			var config = aiConfig.Events;

			config.Instruction_AdsBegin = RB_AdsStarted_Instruction.Text.Trim();
			config.Instruction_AdsFinished = RB_AdsFinished_Instruction.Text.Trim();
			config.Instruction_NotifyPrerolls = RB_Ads_PreRollsNowActive.Text.Trim();
			config.AdsBeginNotify = CB_AdsStarted.Checked;
			config.AdsFinishNotify = CB_AdsFinished.Checked;
			config.AdsPrerollsActiveNotify = CB_Ads_PreRollsNowActive.Checked;

			this.DialogResult = DialogResult.OK;
			aiConfig.SaveSettings();
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		SuiBot_TwitchSocket.API.EventSub.ES_AdBreakBeginNotification testAdd;
		private void B_Test_AdsStarted_Click(object sender, EventArgs e)
		{
			var helixUser = MainForm.Instance.TwitchBot.HelixAPI_User;

			testAdd = new SuiBot_TwitchSocket.API.EventSub.ES_AdBreakBeginNotification()
			{
				requester_user_id = helixUser.BotUserId,
				requester_user_login = helixUser.BotLoginName,
				requester_user_name = helixUser.BotLoginName,
				broadcaster_user_id = helixUser.BotUserId,
				broadcaster_user_login = helixUser.BotLoginName,
				broadcaster_user_name = helixUser.BotLoginName,
				is_automatic = false,
				started_at = DateTime.UtcNow,
				duration_seconds = 30
			};

			MainForm.Instance.TwitchBot?.TwitchSocket_AdBreakBegin(testAdd);
		}

		private void B_Test_AdsFinished_Click(object sender, EventArgs e)
		{
			var helixUser = MainForm.Instance.TwitchBot.HelixAPI_User;

			if(testAdd == null)
			{
				var tmp = new SuiBot_TwitchSocket.API.EventSub.ES_AdBreakBeginNotification()
				{
					requester_user_id = helixUser.BotUserId,
					requester_user_login = helixUser.BotLoginName,
					requester_user_name = helixUser.BotLoginName,
					broadcaster_user_id = helixUser.BotUserId,
					broadcaster_user_login = helixUser.BotLoginName,
					broadcaster_user_name = helixUser.BotLoginName,
					is_automatic = false,
					started_at = DateTime.UtcNow,
					duration_seconds = 30
				};
				MainForm.Instance.TwitchBot?.TwitchSocket_AdBreakFinished(tmp);
			}
			else
			{
				MainForm.Instance.TwitchBot?.TwitchSocket_AdBreakFinished(testAdd);
			}
		}
	}
}
