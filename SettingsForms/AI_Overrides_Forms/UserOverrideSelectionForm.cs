using BasicTwitchSoundPlayer.Structs.Gemini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms
{
	public partial class UserOverrideSelectionForm : Form
	{
		Dictionary<string, GeminiCharacterOverride> Overrides = new Dictionary<string, GeminiCharacterOverride>();

		public UserOverrideSelectionForm()
		{
			InitializeComponent();
		}

		private void UserOverrideSelectionForm_Load(object sender, EventArgs e)
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BasicTwitchSoundPlayer", "AI_Overrides");
			if (!Directory.Exists(path))
				return;

			var xmlFiles = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);
			foreach (var xmlFile in xmlFiles)
			{
				var fileName = Path.GetFileName(xmlFile);

				var read = XML_Utils.Load<GeminiCharacterOverride>(xmlFile, null);
				if (read == null)
				{
					File.Delete(xmlFile);
					continue;
				}

				Overrides.Add(fileName, read);
			}
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			var l = new UserOverrideAddForm();
			var result = l.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				if (string.IsNullOrEmpty(l.Nickname))
					return;

				if(Overrides.ContainsKey(l.Nickname))
				{
					MessageBox.Show("Nickname like this already exists in overrides!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var text = AIConfig.GetInstance().GetInstruction(l.Nickname, false, null, null, null);
				var overrideContent = new GeminiCharacterOverride();
				overrideContent.SystemInstruction = text.parts.FirstOrDefault().text;

				Overrides.Add(l.Nickname, overrideContent);
				this.List_Nicknames.Items.Add(l.Nickname);
			}
		}

		private void B_Close_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
