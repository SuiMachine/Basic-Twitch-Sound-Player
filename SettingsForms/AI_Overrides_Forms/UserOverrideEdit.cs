using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms
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
			UserData.HARM_CATEGORY_HARASSMENT = (Structs.Gemini.AISafetySettingsValues)TBar_Harassment.Value;
			UserData.HARM_CATEGORY_HATE_SPEECH = (Structs.Gemini.AISafetySettingsValues)TBar_Hate.Value;
			UserData.HARM_CATEGORY_SEXUALLY_EXPLICIT = (Structs.Gemini.AISafetySettingsValues)TBar_Sexually_Explicit.Value;
			UserData.HARM_CATEGORY_DANGEROUS_CONTENT = (Structs.Gemini.AISafetySettingsValues)TBar_DangerousContent.Value;
			UserData.HARM_CATEGORY_CIVIC_INTEGRITY = (Structs.Gemini.AISafetySettingsValues)TBar_CivicIntegrity.Value;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
