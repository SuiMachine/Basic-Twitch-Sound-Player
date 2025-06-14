using System;
using System.Windows.Forms;
using static SuiBotAI.Components.Other.Gemini.GeminiSafetySettingsCategory;

namespace SSC.SettingsForms.AI_Overrides_Forms
{
	public partial class UserOverrideEdit : Form
	{
		public Structs.Gemini.GeminiCharacterOverride UserData { get; private set; }

		public UserOverrideEdit(Structs.Gemini.GeminiCharacterOverride userData)
		{
			UserData = userData;
			InitializeComponent();
		}

		private void UserOverrideEdit_Load(object sender, EventArgs e)
		{
			RB_Instructions.Text = UserData.SystemInstruction;
			TBar_Harassment.Value = (int)UserData.HARM_CATEGORY_HARASSMENT;
			TBar_Hate.Value = (int)UserData.HARM_CATEGORY_HATE_SPEECH;
			TBar_Sexually_Explicit.Value = (int)UserData.HARM_CATEGORY_SEXUALLY_EXPLICIT;
			TBar_DangerousContent.Value = (int)UserData.HARM_CATEGORY_DANGEROUS_CONTENT;
			TBar_CivicIntegrity.Value = (int)UserData.HARM_CATEGORY_CIVIC_INTEGRITY;
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			UserData.SystemInstruction = RB_Instructions.Text;
			UserData.HARM_CATEGORY_HARASSMENT = (AISafetySettingsValues)TBar_Harassment.Value;
			UserData.HARM_CATEGORY_HATE_SPEECH = (AISafetySettingsValues)TBar_Hate.Value;
			UserData.HARM_CATEGORY_SEXUALLY_EXPLICIT = (AISafetySettingsValues)TBar_Sexually_Explicit.Value;
			UserData.HARM_CATEGORY_DANGEROUS_CONTENT = (AISafetySettingsValues)TBar_DangerousContent.Value;
			UserData.HARM_CATEGORY_CIVIC_INTEGRITY = (AISafetySettingsValues)TBar_CivicIntegrity.Value;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
