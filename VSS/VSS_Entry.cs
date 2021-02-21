using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS
{
	public class VSS_Entry : TreeNode
	{
		private enum Icons
		{
			Root,
			Category,
			Sound
		}

		public Keys Hotkey { get; set; }
		public bool HasParent => Parent != null;
		public string Description { get; set; }

		protected VSS_Entry()
		{
			this.Hotkey = Keys.None;
			this.Description = "";
			this.UpdateNodeDisplay();
		}

		public void UpdateNodeDisplay()
		{
			this.Text = string.Format("[{0}] {1}", Hotkey, Description);
		}
	}

	//Inhereted Variants
	public class VSS_Entry_Sound : VSS_Entry
	{
		public string Filepath { get; set; }

		public VSS_Entry_Sound(string Description, Keys Hotkey, string Filepath)
		{
			this.Description = Description;
			this.Hotkey = Hotkey;
			this.Filepath = Filepath;
			this.ImageIndex = 2;
			this.SelectedImageIndex = 2;
			this.StateImageIndex = 2;
			this.UpdateNodeDisplay();
		}
	}

	public class VSS_Entry_Group : VSS_Entry
	{
		public VSS_Entry_Group(string Description, Keys Hotkey)
		{
			this.Description = Description;
			this.Hotkey = Hotkey;
			this.ImageIndex = 1;
			this.SelectedImageIndex = 1;
			this.StateImageIndex = 1;
			this.UpdateNodeDisplay();
		}

		internal VSS_Entry AddChild(VSS_Entry VSS_Child)
		{
			this.Nodes.Add(VSS_Child);
			return VSS_Child;
		}
	}
}