using System;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
	public partial class SoundPlaybackSettingsDialog : Form
	{
		public Guid SelectedDevice { get; set; }

		public SoundPlaybackSettingsDialog()
		{
			var settings = PrivateSettings.GetInstance();

			this.SelectedDevice = settings.OutputDevice;

			InitializeComponent();

			//Initialization stuff and bindings
			this.FillInOutputDevices();
		}

		private void FillInOutputDevices()
		{
			bool deviceWasSelected = false;
			foreach (var dev in NAudio.Wave.DirectSoundOut.Devices)
			{
				CB_OutputDevices.Items.Add(dev.Description);
				if (dev.Guid == SelectedDevice && !deviceWasSelected)
				{
					CB_OutputDevices.SelectedIndex = CB_OutputDevices.Items.Count - 1;
					deviceWasSelected = true;
				}
			}

			if (!deviceWasSelected)
				CB_OutputDevices.SelectedIndex = 0;
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			foreach (var dev in NAudio.Wave.DirectSoundOut.Devices)
			{
				if (dev.Description == CB_OutputDevices.SelectedItem.ToString())
				{
					SelectedDevice = dev.Guid;
					break;
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
