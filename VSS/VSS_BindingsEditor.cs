using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS
{
	public partial class VSS_BindingsEditor : Form
	{
		public VSS_Entry_Group VSS_RootEntry { get; set; }

		public VSS_BindingsEditor(VSS_Entry_Group VSS_RootNode)
		{
			InitializeComponent();
			this.VSS_Tree.Nodes.Clear();
			this.VSS_RootEntry = VSS_RootNode;
			this.VSS_RootEntry.Name = "ROOTNODE";
			this.VSS_RootEntry.ImageIndex = 0;
			this.VSS_RootEntry.SelectedImageIndex = 0;
			this.VSS_RootEntry.StateImageIndex = 0;
			this.VSS_Tree.Nodes.Add(VSS_RootEntry);
			TB_Key.Text = VSS_RootEntry.Hotkey.ToString();
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void B_SetKey_Click(object sender, EventArgs e)
		{
			Dialogs.PressKeyDialog pressKeyForm = new Dialogs.PressKeyDialog()
			{
				StartPosition = FormStartPosition.Manual,
				Top = this.Top + 20,
				Left = this.Left + 20
			};
			var res = pressKeyForm.ShowDialog();

			if (res == DialogResult.OK)
			{
				VSS_RootEntry.Hotkey = pressKeyForm.ReturnedKey;
			}
		}

		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var AddNewVSSDialog = new Dialogs.AddEditVSSGroup();
			var parentNode = (VSS_Entry_Group)VSS_Tree.SelectedNode;
			var res = AddNewVSSDialog.ShowDialog();
			if (res == DialogResult.OK)
			{
				parentNode.AddChild(new VSS_Entry_Group(AddNewVSSDialog.NameDesc, AddNewVSSDialog.ReturnKey));
			}
		}

		private void EditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditNode();
		}

		private void EditNode()
		{
			var selectedNode = VSS_Tree.SelectedNode;
			if (selectedNode != VSS_RootEntry)
			{
				if (selectedNode.GetType() == typeof(VSS_Entry_Group))
				{
					var nodeCast = (VSS_Entry_Group)selectedNode;

					var EditVSSDialog = new Dialogs.AddEditVSSGroup(true)
					{
						ReturnKey = nodeCast.Hotkey,
						NameDesc = nodeCast.Description
					};
					var res = EditVSSDialog.ShowDialog();
					if (res == DialogResult.OK)
					{
						nodeCast.Hotkey = EditVSSDialog.ReturnKey;
						nodeCast.Description = EditVSSDialog.NameDesc;
						nodeCast.UpdateNodeDisplay();
					}
				}
				else
				{
					var nodeCast = (VSS_Entry_Sound)selectedNode;

					var EditVSSDialog = new Dialogs.AddEditVSSSound(true)
					{
						Hotkey = nodeCast.Hotkey,
						NameDesc = nodeCast.Description,
						FilePath = nodeCast.Filepath
					};
					var res = EditVSSDialog.ShowDialog();
					if (res == DialogResult.OK)
					{
						nodeCast.Hotkey = EditVSSDialog.Hotkey;
						nodeCast.Description = EditVSSDialog.NameDesc;
						nodeCast.Filepath = EditVSSDialog.FilePath;
						nodeCast.UpdateNodeDisplay();
					}
				}
			}
		}

		private void AddSoundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var AddNewVSSDialog = new Dialogs.AddEditVSSSound();
			var parentNode = VSS_Tree.SelectedNode;
			var res = AddNewVSSDialog.ShowDialog();
			if (res == DialogResult.OK)
			{
				parentNode.Nodes.Add(new VSS.VSS_Entry_Sound(AddNewVSSDialog.NameDesc, AddNewVSSDialog.Hotkey, AddNewVSSDialog.FilePath));
			}
		}

		private void VSS_Tree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (VSS_Tree.SelectedNode.GetType() == typeof(VSS_Entry_Sound))
			{
				contexMenuVSS.Items[0].Enabled = false;
				contexMenuVSS.Items[1].Enabled = false;
			}
			else
			{
				contexMenuVSS.Items[0].Enabled = true;
				contexMenuVSS.Items[1].Enabled = true;
			}

			if (VSS_Tree.SelectedNode == VSS_RootEntry)
			{
				contexMenuVSS.Items[3].Enabled = false;
			}
			else
			{
				contexMenuVSS.Items[3].Enabled = true;
			}
		}

		private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selectedNode = VSS_Tree.SelectedNode;
			if (selectedNode != null)
				selectedNode.Remove();
		}

		private void VSS_Tree_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				var selectedNode = VSS_Tree.SelectedNode;
				if (selectedNode != null && selectedNode.Name != "ROOTNODE")
					selectedNode.Remove();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				var selectedNode = VSS_Tree.SelectedNode;
				if (selectedNode != null)
					EditToolStripMenuItem_Click(sender, null);
			}
		}

		private void VSS_Tree_DoubleClick(object sender, EventArgs e)
		{
			EditNode();
		}

		private void VSS_BindingsEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Probably need disposing
		}
	}
}
