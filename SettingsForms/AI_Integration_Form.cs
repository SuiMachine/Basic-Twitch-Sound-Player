using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms;
using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
	public partial class AI_Integration_Form : Form
	{
		public AI_Integration_Form()
		{
			InitializeComponent();
		}

		private void AI_Integration_Form_Load(object sender, EventArgs e)
		{
			//No bindings - we do it manually so we don't have to copy data
			var config = AIConfig.GetInstance();

			TB_API_Key.Text = config.ApiKey;
			RB_InstructionStreamer.Text = config.Instruction_Streamer;
			RB_InstructionUser.Text = config.Instruction_User;

			TBar_StreamerHarassment.Value = (int)config.FilterSet_Streamer.Harassment;
			TBar_StreamerHate.Value = (int)config.FilterSet_Streamer.Hate;
			TBar_StreamerSexuallyExplicit.Value = (int)config.FilterSet_Streamer.Sexually_Explicit;
			TBar_StreamerDangerousContent.Value = (int)config.FilterSet_Streamer.Dangerous_Content;
			TBar_StreamerCivicIntegrity.Value = (int)config.FilterSet_Streamer.Civic_Integrity;
			Num_StreamerTokenLimit.Value = config.TokenLimit_Streamer;
			Num_StreamerTemperature.Value = (decimal)config.Temperature_Streamer;

			TBar_UserHarassment.Value = (int)config.FilterSet_User.Harassment;
			TBar_UserHate.Value = (int)config.FilterSet_User.Hate;
			TBar_UserSexuallyExplicit.Value = (int)config.FilterSet_User.Sexually_Explicit;
			TBar_UserDangerousContent.Value = (int)config.FilterSet_User.Dangerous_Content;
			TBar_UserCivicIntegrity.Value = (int)config.FilterSet_User.Civic_Integrity;
			Num_UserTokenLimit.Value = config.TokenLimit_User;
			Num_UserResponseTemperature.Value = (decimal)config.Temperature_User;
		}

		private async void B_CreateReward_Click(object sender, EventArgs e)
		{
			var config = AIConfig.GetInstance();

			var settings = PrivateSettings.GetInstance();
			IRC.KrakenConnections apiConnection = new IRC.KrakenConnections(settings.UserName);
			await apiConnection.GetBroadcasterIDAsync();
			if (apiConnection.CachedRewards == null)
				_ = await apiConnection.GetRewardsList();

			var result = await apiConnection.CreateOrUpdateReward("Ask AI", "Ask AI a question", 1_000, true, 5*60, config.TwitchAwardID);
			if (result != null)
			{
				config.TwitchAwardID = result.id;
				config.SaveSettings();
			}
		}

		private void B_Show_UserOverrides_Click(object sender, EventArgs e)
		{
			var f = new UserOverrideSelectionForm();
			f.ShowDialog();
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			var config = AIConfig.GetInstance();

			config.ApiKey = TB_API_Key.Text;
			config.Instruction_Streamer = RB_InstructionStreamer.Text;
			config.Instruction_User = RB_InstructionUser.Text;

			config.FilterSet_Streamer.Harassment = (Structs.Gemini.AISafetySettingsValues)TBar_StreamerHarassment.Value;
			config.FilterSet_Streamer.Hate = (Structs.Gemini.AISafetySettingsValues)TBar_StreamerHate.Value;
			config.FilterSet_Streamer.Sexually_Explicit = (Structs.Gemini.AISafetySettingsValues)TBar_StreamerSexuallyExplicit.Value;
			config.FilterSet_Streamer.Dangerous_Content = (Structs.Gemini.AISafetySettingsValues)TBar_StreamerDangerousContent.Value;
			config.FilterSet_Streamer.Civic_Integrity = (Structs.Gemini.AISafetySettingsValues)TBar_StreamerCivicIntegrity.Value;
			config.Temperature_Streamer = (float)Num_StreamerTemperature.Value;

			config.FilterSet_User.Harassment = (Structs.Gemini.AISafetySettingsValues)TBar_UserHarassment.Value;
			config.FilterSet_User.Hate = (Structs.Gemini.AISafetySettingsValues)TBar_UserHate.Value;
			config.FilterSet_User.Sexually_Explicit = (Structs.Gemini.AISafetySettingsValues)TBar_UserSexuallyExplicit.Value;
			config.FilterSet_User.Dangerous_Content = (Structs.Gemini.AISafetySettingsValues)TBar_UserDangerousContent.Value;
			config.FilterSet_User.Civic_Integrity = (Structs.Gemini.AISafetySettingsValues)TBar_UserCivicIntegrity.Value;
			config.Temperature_User = (float)Num_UserResponseTemperature.Value;

			config.TokenLimit_Streamer = (int)Num_StreamerTokenLimit.Value;
			config.TokenLimit_User = (int)Num_UserTokenLimit.Value;

			config.SaveSettings();
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to discard changes?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				this.Close();
			}
		}
	}
}
