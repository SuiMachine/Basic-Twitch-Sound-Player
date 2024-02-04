using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer
{
	public partial class VSS_PreviewWindow : Form
	{
		VSS.VSS_Entry_Group VSSdb { get; set; }
		VSS.VSS_Entry_Group CurrentNode { get; set; }
		private VSS_Preview preview;
		private IWavePlayer directWaveOut;
		private Gma.System.MouseKeyHook.IKeyboardMouseEvents m_GlobalHook;

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		public VSS_PreviewWindow(VSS.VSS_Entry_Group VSSdb)
		{
			InitializeComponent();
			m_GlobalHook = Gma.System.MouseKeyHook.Hook.GlobalEvents();
			directWaveOut = new DirectSoundOut(60);
			this.VSSdb = new VSS.VSS_Entry_Group("ROOTNODE", Keys.NoName);
			this.VSSdb.Nodes.Add(VSSdb);
			this.CurrentNode = VSSdb;
			UpdateColors();
			preview = new VSS_Preview(pictureBoxVSSPreview, Color.Black, Color.Yellow, 24, Color.White, 18, Color.WhiteSmoke, 4);
			SetToRoot();
			m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
		}

		private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
		{
			foreach (var child in CurrentNode.Nodes)
			{
				if (((VSS.VSS_Entry)child).Hotkey == e.KeyCode && !e.Handled)
				{
					e.SuppressKeyPress = true;
					e.Handled = true;
					if (child.GetType() == typeof(VSS.VSS_Entry_Group))
					{
						var cast = (VSS.VSS_Entry_Group)child;
						this.preview.Clear();
						CurrentNode = cast;
						AddDisplayElement(CurrentNode);
					}
					else
					{
						this.preview.Clear();
						PlaySound(((VSS.VSS_Entry_Sound)child).Filepath);
						CurrentNode = VSSdb;
						AddDisplayElement(CurrentNode);
					}
				}
			}
		}

		private void PlaySound(string filepath)
		{
			if (File.Exists(filepath))
			{
				Task.Factory.StartNew(() =>
				{
					if (filepath.EndsWith("ogg"))
					{
						NAudio.Vorbis.VorbisWaveReader VorbisFileReader = new NAudio.Vorbis.VorbisWaveReader(filepath);
						directWaveOut.Init(VorbisFileReader);
					}
					else
					{
						AudioFileReader audioFileReader = new AudioFileReader(filepath);
						directWaveOut.Init(audioFileReader);
					}

					directWaveOut.Play();
				});
			}
		}

		private void UpdateColors()
		{
			var settings = PrivateSettings.GetInstance();
			this.BackColor = settings.Colors.FormBackground;
			this.ForeColor = settings.Colors.FormTextColor;
		}

		private void AddDisplayElement(VSS.VSS_Entry_Group NodeToDrawFrom)
		{
			foreach (var child in NodeToDrawFrom.Nodes)
			{
				var cast = (VSS.VSS_Entry)child;
				this.preview.AddElement(cast.Hotkey, cast.Description);
			}
			preview.UpdateView();
		}

		private void VSS_PreviewWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_GlobalHook.KeyDown -= M_GlobalHook_KeyDown;

			m_GlobalHook.Dispose();
		}

		private void ResetTimer_Tick(object sender, EventArgs e)
		{
			SetToRoot();
		}

		private void SetToRoot()
		{
			CurrentNode = VSSdb;
			AddDisplayElement(CurrentNode);
		}

		private void PictureBoxVSSPreview_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void CloseVSSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}

	public struct VSS_Preview
	{
		private PictureBox PictureBoxVSSPreview { get; set; }
		private Image Img { get; set; }
		private Graphics GBuffer { get; set; }
		private float LastElementTop;

		private Color BackgroundColor { get; set; }
		private Color KeyColor { get; set; }
		public float KeySize { get; }
		private Color DescriptionColor { get; set; }
		public float DescriptionSize { get; }
		private Color GenericTextColor { get; set; }
		public float BetweenEntriesGap { get; }


		public VSS_Preview(PictureBox PictureBoxVSSPreview, Color BackgroundColor, Color KeyColor, float KeySize, Color DescriptionColor, float DescriptionSize, Color GenericTextColor, float BetweenEntriesGap)
		{
			this.PictureBoxVSSPreview = PictureBoxVSSPreview;
			this.Img = new Bitmap(PictureBoxVSSPreview.Width, PictureBoxVSSPreview.Height);
			GBuffer = Graphics.FromImage(Img);
			LastElementTop = 0;

			this.BackgroundColor = BackgroundColor;
			this.KeyColor = KeyColor;
			this.KeySize = KeySize;
			this.DescriptionColor = DescriptionColor;
			this.DescriptionSize = DescriptionSize;
			this.GenericTextColor = GenericTextColor;
			this.BetweenEntriesGap = BetweenEntriesGap;
			Clear();
		}

		public void AddElement(Keys key, string Description)
		{
			Font FontKeyText = new Font("Arial", KeySize, FontStyle.Bold);
			Font FontDescriptionText = new Font("Arial", DescriptionSize, FontStyle.Regular);

			GBuffer.DrawString("[", FontKeyText, new SolidBrush(GenericTextColor), 0, LastElementTop);
			GBuffer.DrawString(key.ToString(), FontKeyText, new SolidBrush(KeyColor), 16, LastElementTop);
			GBuffer.DrawString("]", FontKeyText, new SolidBrush(GenericTextColor), 32, LastElementTop);
			GBuffer.DrawString(Description, FontDescriptionText, new SolidBrush(GenericTextColor), 48, LastElementTop);

			LastElementTop += (KeySize + BetweenEntriesGap);
		}

		public void Clear()
		{
			GBuffer.Clear(BackgroundColor);
			LastElementTop = 0;
			UpdateView();
		}

		public void UpdateView()
		{
			PictureBoxVSSPreview.Image = Img;
		}
	}
}
