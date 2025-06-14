using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSC.DialogBoxes
{
	public partial class ProgressDisplay : Form
	{
		public static ProgressDisplay Instance { get; private set; }
		private Action TaskToPerform;

		private ProgressDisplay()
		{
			InitializeComponent();
		}

		private void ProgressDisplay_Load(object sender, EventArgs e)
		{
			Task.Run(TaskToPerform);
		}

		public static ProgressDisplay CreateIfNeeded()
		{
			if (Instance == null)
				Instance = new ProgressDisplay();
			return Instance;
		}

		public ProgressDisplay SetupForm(Form parent, string header, Action content)
		{
			this.Text = header;
			this.TaskToPerform = content;
			this.L_Progress_Text.Text = "Please wait...";
			this.ShowDialog(parent);
			return this;
		}

		public void SetProgressText(string text)
		{
			if (L_Progress_Text.InvokeRequired)
			{
				L_Progress_Text.Invoke(new Action(() =>
				{
					SetProgressText(text);
				}));
				return;
			}

			L_Progress_Text.Text = text;
		}

		private void ProgressDisplay_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!Instance.IsDisposed)
				Instance.Dispose();

			Instance = null;
		}

		public void InvokeClose()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() =>
				{
					this.Close();
				}));
			}
			else
				this.Close();
		}
	}
}
