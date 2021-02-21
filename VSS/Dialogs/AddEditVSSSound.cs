using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS.Dialogs
{
	public partial class AddEditVSSSound : Form
	{
		public string NameDesc { get; set; }
		public Keys Hotkey { get; set; }
		public string FilePath { get; set; }

		IWavePlayer directWaveOut;

		public AddEditVSSSound(bool IsEdit = false)
		{
			InitializeComponent();
			directWaveOut = new DirectSoundOut(100);

			if (IsEdit)
				this.Text = "Edit VSS Sound Entry";
			else
				this.Text = "Add new VSS Sound Entry";

			TB_Name.DataBindings.Add("Text", this, "NameDesc", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Key.DataBindings.Add("Text", this, "Hotkey", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_SoundPath.DataBindings.Add("Text", this, "FilePath", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void B_Browse_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDial = new OpenFileDialog
			{
				Filter = SoundDatabaseEditor.SupportedFileFormats.Filter,
				FilterIndex = SoundDatabaseEditor.SupportedFileFormats.LastIndex,
			};
			DialogResult res = fileDial.ShowDialog();
			if (res == DialogResult.OK)
			{
				this.FilePath = fileDial.FileName;
				this.TB_SoundPath.Text = this.FilePath;
			}
		}

		private void B_SetKey_Click(object sender, EventArgs e)
		{
			Dialogs.PressKeyDialog dlg = new PressKeyDialog();
			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				this.Hotkey = dlg.ReturnedKey;
				this.TB_Key.Text = Hotkey.ToString();
			}
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

		private void B_Test_Click(object sender, EventArgs e)
		{
			if (File.Exists(TB_SoundPath.Text))
			{
				Task.Factory.StartNew(() =>
				{
					if (TB_SoundPath.Text.ToLower().EndsWith("ogg"))
					{
						var VorbisStream = new NAudio.Vorbis.VorbisWaveReader(TB_SoundPath.Text);
						directWaveOut.Init(VorbisStream);
						directWaveOut.Play();
					}
					else
					{
						AudioFileReader audioFileReader = new AudioFileReader(TB_SoundPath.Text);
						directWaveOut.Init(audioFileReader);
						directWaveOut.Play();
					}


				});
			}
		}

		private void AddEditVSSSound_FormClosing(object sender, FormClosingEventArgs e)
		{
			directWaveOut.Dispose();
		}
	}
}
