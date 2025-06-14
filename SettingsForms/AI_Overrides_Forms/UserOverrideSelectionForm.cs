using SSC.Structs.Gemini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SSC.SettingsForms.AI_Overrides_Forms
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
				var userName = Path.GetFileNameWithoutExtension(xmlFile);

				var read = GeminiCharacterOverride.GetOverride(xmlFile);
				if (read == null)
				{
					File.Delete(xmlFile);
					continue;
				}

				Overrides.Add(userName, read);
				this.List_Nicknames.Items.Add(read.Username);
			}
		}

		private void List_Nicknames_Add_Click(object sender, EventArgs e)
		{
			var l = new UserOverrideAddForm();
			var result = l.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				var strippedNickname = l.Nickname.Trim().Trim('@');

				if (string.IsNullOrEmpty(strippedNickname))
					return;

				if (Overrides.ContainsKey(strippedNickname))
				{
					MessageBox.Show("Nickname like this already exists in overrides!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var text = AIConfig.GetInstance().GetInstruction(strippedNickname, false, false, false);
				var overrideContent = new GeminiCharacterOverride
				{
					SystemInstruction = text.parts.FirstOrDefault().text,
					Path = GeminiCharacterOverride.GetOverridePath(strippedNickname)
				};
				overrideContent.Save();

				Overrides.Add(l.Nickname, overrideContent);
				this.List_Nicknames.Items.Add(l.Nickname);
			}
		}

		private void B_Close_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void List_Nicknames_EditElement(object sender, EventArgs e)
		{
			if (this.List_Nicknames.SelectedItem == null)
				return;

			string selectedItem = (string)this.List_Nicknames.SelectedItem;
			if (string.IsNullOrEmpty(selectedItem))
				return;

			if (!Overrides.TryGetValue(selectedItem, out GeminiCharacterOverride val))
				return;

			UserOverrideEdit f = new UserOverrideEdit(val);
			var result = f.ShowDialog();
			if (result == DialogResult.OK)
			{
				f.UserData.Save();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.List_Nicknames.SelectedItem == null)
				return;

			string selectedItem = (string)this.List_Nicknames.SelectedItem;
			if (string.IsNullOrEmpty(selectedItem))
				return;

			if (Overrides.TryGetValue(selectedItem, out GeminiCharacterOverride val))
			{
				if (val.Path != null)
				{
					if (MessageBox.Show($"Are you sure you want to delete entry for: {val.Username}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						File.Delete(val.Path);
						Overrides.Remove(selectedItem);

						this.List_Nicknames.Items.Remove(selectedItem);
					}
				}
			}
		}
	}
}
