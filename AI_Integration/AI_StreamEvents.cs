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
			RB_RaidInstructions.Text = config.Instruction_Raid;

			CB_AdsStarted.Checked = config.AdsBeginNotify;
			CB_AdsFinished.Checked = config.AdsFinishNotify;
			CB_Ads_PreRollsNowActive.Checked = config.AdsPrerollsActiveNotify;
			CB_RaidNotification.Checked = config.RaidNotify;
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			var aiConfig = AIConfig.GetInstance();

			var config = aiConfig.Events;

			config.Instruction_AdsBegin = RB_AdsStarted_Instruction.Text.Trim();
			config.Instruction_AdsFinished = RB_AdsFinished_Instruction.Text.Trim();
			config.Instruction_NotifyPrerolls = RB_Ads_PreRollsNowActive.Text.Trim();
			config.Instruction_Raid = RB_RaidInstructions.Text.Trim();
			config.AdsBeginNotify = CB_AdsStarted.Checked;
			config.AdsFinishNotify = CB_AdsFinished.Checked;
			config.AdsPrerollsActiveNotify = CB_Ads_PreRollsNowActive.Checked;
			config.RaidNotify = CB_RaidNotification.Checked;

			this.DialogResult = DialogResult.OK;
			aiConfig.SaveSettings();
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var aiConfig = AIConfig.GetInstance();

			var config = aiConfig.Events;

			var helixUser = MainForm.Instance?.TwitchBot.HelixAPI_User;

			MainForm.Instance?.TwitchBot.TwitchSocket_ChannelRaid(new SuiBot_TwitchSocket.API.EventSub.ES_ChannelRaid()
			{
				from_broadcaster_user_id = "89089368",
				from_broadcaster_user_login = "hibike7",
				from_broadcaster_user_name = "HiBike7",
				to_broadcaster_user_id = helixUser.BotUserId,
				to_broadcaster_user_login = helixUser.BotLoginName,
				to_broadcaster_user_name = helixUser.BotLoginName,
				viewers = 9
			});
		}
	}
}
