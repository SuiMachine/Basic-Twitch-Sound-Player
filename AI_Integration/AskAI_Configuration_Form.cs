using BasicTwitchSoundPlayer.IRC;
using BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms;
using SuiBot_TwitchSocket.API;
using System;
using System.Linq;
using System.Windows.Forms;
using static SuiBotAI.Components.Other.Gemini.GeminiSafetySettingsCategory;

namespace BasicTwitchSoundPlayer.AI_Integration
{
	public partial class AskAI_Configuration_Form : Form
	{
		public AskAI_Configuration_Form()
		{
			InitializeComponent();
		}

		private void AI_Integration_Form_Load(object sender, EventArgs e)
		{
			//No bindings - we do it manually so we don't have to copy data
			var config = AIConfig.GetInstance();

			TB_Username.Text = config.TwitchUsername;
			TB_API_Key.Text = config.ApiKey;
			RB_CharacterDefinition.Text = config.Instruction_Character;
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
			var aiConfig = AIConfig.GetInstance();

			var api = new HelixAPI(ChatBot.BASIC_TWITCH_SOUND_PLAYER_CLIENT_ID, null, PrivateSettings.GetInstance().UserAuth);
			var validation = api.ValidateToken();
			if (validation != HelixAPI.ValidationResult.Successful)
			{
				MessageBox.Show("Failed to validate user token!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!await api.CreateRewardsCache())
			{
				MessageBox.Show("Failed to create reward cache!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var award = api.RewardsCache.FirstOrDefault(x => x.id == aiConfig.TwitchAwardID);
			if (award != null)
			{
				MessageBox.Show("The reward already exists!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				var reward = await api.CreateOrUpdateReward(null, "Ask AI", "Ask AI a question", 1_000, 5 * 60, true, true);
				if (reward != null)
				{
					MessageBox.Show("Created a reward!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
					aiConfig.TwitchAwardID = reward.id;
					aiConfig.SaveSettings();
				}
				else
					MessageBox.Show("Failed to create a reward", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			config.Instruction_Character = RB_CharacterDefinition.Text;
			config.Instruction_Streamer = RB_InstructionStreamer.Text;
			config.Instruction_User = RB_InstructionUser.Text;

			config.FilterSet_Streamer.Harassment = (AISafetySettingsValues)TBar_StreamerHarassment.Value;
			config.FilterSet_Streamer.Hate = (AISafetySettingsValues)TBar_StreamerHate.Value;
			config.FilterSet_Streamer.Sexually_Explicit = (AISafetySettingsValues)TBar_StreamerSexuallyExplicit.Value;
			config.FilterSet_Streamer.Dangerous_Content = (AISafetySettingsValues)TBar_StreamerDangerousContent.Value;
			config.FilterSet_Streamer.Civic_Integrity = (AISafetySettingsValues)TBar_StreamerCivicIntegrity.Value;
			config.Temperature_Streamer = (float)Num_StreamerTemperature.Value;

			config.FilterSet_User.Harassment = (AISafetySettingsValues)TBar_UserHarassment.Value;
			config.FilterSet_User.Hate = (AISafetySettingsValues)TBar_UserHate.Value;
			config.FilterSet_User.Sexually_Explicit = (AISafetySettingsValues)TBar_UserSexuallyExplicit.Value;
			config.FilterSet_User.Dangerous_Content = (AISafetySettingsValues)TBar_UserDangerousContent.Value;
			config.FilterSet_User.Civic_Integrity = (AISafetySettingsValues)TBar_UserCivicIntegrity.Value;
			config.Temperature_User = (float)Num_UserResponseTemperature.Value;

			config.TokenLimit_Streamer = (int)Num_StreamerTokenLimit.Value;
			config.TokenLimit_User = (int)Num_UserTokenLimit.Value;
			config.TwitchUsername = TB_Username.Text.Trim();

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
