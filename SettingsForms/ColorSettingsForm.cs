using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SettingsForms
{
	public partial class ColorSettingsForm : Form
	{
		//Form background
		public Color FormBackground { get; set; }
		public Color FormTextColor { get; set; }

		//MenuStripBar
		public Color MenuStripBarBackground { get; set; }
		public Color MenuStripBarText { get; set; }

		//MenuStripColors
		public Color MenuStripBackground { get; set; }
		public Color MenuStripText { get; set; }
		public Color MenuStripBackgroundSelected { get; set; }

		//LineColors
		public Color LineColorBackground { get; set; }
		public Color LineColorGeneric { get; set; }
		public Color LineColorIrcCommand { get; set; }
		public Color LineColorModeration { get; set; }
		public Color LineColorSoundPlayback { get; set; }

		public Extensions.OverridenColorTable CustomColorTable { get; set; }


		public ColorSettingsForm(PrivateSettings _programSettings)
		{
			InitializeComponent();

			FormBackground = _programSettings.Colors.FormBackground;
			FormTextColor = _programSettings.Colors.FormTextColor;

			MenuStripBarBackground = _programSettings.Colors.MenuStripBarBackground;
			MenuStripBarText = _programSettings.Colors.MenuStripBarText;

			MenuStripBackground = _programSettings.Colors.MenuStripBackground;
			MenuStripText = _programSettings.Colors.MenuStripText;
			MenuStripBackgroundSelected = _programSettings.Colors.MenuStripBackgroundSelected;

			LineColorBackground = _programSettings.Colors.LineColorBackground;
			LineColorGeneric = _programSettings.Colors.LineColorGeneric;
			LineColorIrcCommand = _programSettings.Colors.LineColorIrcCommand;
			LineColorModeration = _programSettings.Colors.LineColorModeration;
			LineColorSoundPlayback = _programSettings.Colors.LineColorSoundPlayback;

			CustomColorTable = new Extensions.OverridenColorTable();

			//Form Generic
			B_ColorFormBackground.DataBindings.Add("BackColor", this, "FormBackground");
			B_ColorFormText.DataBindings.Add("BackColor", this, "FormTextColor");

			//MenuStripBar
			B_ColorMenuBarBackground.DataBindings.Add("BackColor", this, "MenuStripBarBackground");
			B_ColorMenuBarText.DataBindings.Add("BackColor", this, "MenuStripBarText");

			//MenuStripElements
			B_ColorMenuStripBackground.DataBindings.Add("BackColor", this, "MenuStripBackground");
			B_ColorMenuStriptTextColor.DataBindings.Add("BackColor", this, "MenuStripText");
			B_ColorMenuStripSelectedBackground.DataBindings.Add("BackColor", this, "MenuStripBackgroundSelected");

			//Lines
			B_ColorLinesBackground.DataBindings.Add("BackColor", this, "LineColorBackground");
			B_ColorLinesGeneric.DataBindings.Add("BackColor", this, "LineColorGeneric");
			B_ColorLinesIrcCommand.DataBindings.Add("BackColor", this, "LineColorIrcCommand");
			B_ColorLinesModeration.DataBindings.Add("BackColor", this, "LineColorModeration");
			B_ColorLinesSoundPlayback.DataBindings.Add("BackColor", this, "LineColorSoundPlayback");

			//Overriding renderers
			UpdatePreviewColors();

			CB_Preset.SelectedIndex = 0;
		}

		private void B_Color_Click(object sender, EventArgs e)
		{
			var watbuttton = (Button)sender;
			ColorDialog coldg = new ColorDialog
			{
				Color = watbuttton.BackColor
			};
			DialogResult res = coldg.ShowDialog();
			if (res == DialogResult.OK)
			{
				watbuttton.BackColor = coldg.Color;
			}
		}

		private void UpdatePreviewColors()
		{
			CustomColorTable = new Extensions.OverridenColorTable()
			{
				UseSystemColors = false,
				ColorMenuBorder = Color.Black,
				ColorMenuBarBackground = MenuStripBarBackground,
				ColorMenuItemSelected = MenuStripBackgroundSelected,
				ColorMenuBackground = MenuStripBackground,

				TextColor = MenuStripText
			};

			tableLayoutPanel3.BackColor = FormBackground;
			tableLayoutPanel3.ForeColor = FormTextColor;
			menuStrip1.Renderer = new ToolStripProfessionalRenderer(CustomColorTable);
			menuStrip1.ForeColor = MenuStripBarText;
			ReColorChildren(menuStrip1);

			RB_Preview.BackColor = LineColorBackground;
			RB_Preview.Clear();
			AppendChatLine("This is the generic line, which is usually any chat message", LineType.Generic);
			AppendChatLine("This is the irc command line, which is usually received when receiving information like connection state change / verification / moderation rights", LineType.IrcCommand);
			AppendChatLine("This is the moderation line, which is when a moderator uses a mod only command (rare one)", LineType.ModCommand);
			AppendChatLine("This is a sound playback line, which is when a user uses a sound", LineType.SoundCommand);
		}

		private void ReColorChildren(MenuStrip menuStrip1)
		{
			for (int i = 0; i < menuStrip1.Items.Count; i++)
			{
				if (menuStrip1.Items[1].GetType() == typeof(ToolStripMenuItem))
				{
					var TempCast = (ToolStripMenuItem)menuStrip1.Items[i];
					foreach (ToolStripItem child in TempCast.DropDownItems)
					{
						child.BackColor = MenuStripBackground;
						child.ForeColor = MenuStripText;
					}
				}
			}
		}

		private void AppendChatLine(string text, LineType linetype)
		{
			RB_Preview.AppendText(text + "\n");
			RB_Preview.Select(RB_Preview.Text.Length - text.Length - 1, text.Length);
			switch (linetype)
			{
				case LineType.IrcCommand:
					RB_Preview.SelectionColor = LineColorIrcCommand;
					break;
				case LineType.ModCommand:
					RB_Preview.SelectionColor = LineColorModeration;
					break;
				case LineType.SoundCommand:
					RB_Preview.SelectionColor = LineColorSoundPlayback;
					break;
				default:
					RB_Preview.SelectionColor = LineColorGeneric;
					break;
			}
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void B_OK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_RefreshDisplay_Click(object sender, EventArgs e)
		{
			UpdatePreviewColors();
		}

		private void B_SetPreset_Click(object sender, EventArgs e)
		{
			switch (CB_Preset.SelectedIndex)
			{
				case 1:
					FormBackground = Color.FromArgb(30, 30, 30);
					FormTextColor = Color.WhiteSmoke;

					MenuStripBarBackground = Color.FromArgb(20, 20, 20);
					MenuStripBarText = Color.WhiteSmoke;

					MenuStripBackground = Color.FromArgb(15, 15, 15);
					MenuStripText = Color.WhiteSmoke;
					MenuStripBackgroundSelected = Color.SkyBlue;

					LineColorBackground = Color.FromArgb(40, 40, 40);
					LineColorGeneric = Color.White;
					LineColorIrcCommand = Color.Green;
					LineColorModeration = Color.CadetBlue;
					LineColorSoundPlayback = Color.Yellow;
					break;

				case 2:
					FormBackground = Color.Pink;
					FormTextColor = Color.Black;

					MenuStripBarBackground = Color.LightPink;
					MenuStripBarText = Color.Black;

					MenuStripBackground = Color.Pink;
					MenuStripText = Color.Black;
					MenuStripBackgroundSelected = Color.DeepPink;

					LineColorBackground = Color.GhostWhite;
					LineColorGeneric = Color.Black;
					LineColorIrcCommand = Color.DarkGreen;
					LineColorModeration = Color.DarkBlue;
					LineColorSoundPlayback = Color.DarkOrange;
					break;
				default:
					FormBackground = Color.WhiteSmoke;
					FormTextColor = Color.Black;

					MenuStripBarBackground = Color.WhiteSmoke;
					MenuStripBarText = Color.Black;

					MenuStripBackground = Color.WhiteSmoke;
					MenuStripText = Color.Black;
					MenuStripBackgroundSelected = Color.SkyBlue;

					LineColorBackground = Color.GhostWhite;
					LineColorGeneric = Color.Black;
					LineColorIrcCommand = Color.DarkGreen;
					LineColorModeration = Color.DarkBlue;
					LineColorSoundPlayback = Color.DarkOrange;
					break;
			}
			UpdatePreviewColors();
		}
	}
}