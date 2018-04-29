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
        public Color MenuStripBackground { get; set; }
        public Color MenuStripText { get; set; }
        public Color MenuStripBackgroundSelected { get; set; }
        public Extensions.OverridenColorTable CustomColorTable { get; set; }


        public ColorSettingsForm(PrivateSettings _programSettings)
        {
            InitializeComponent();

            MenuStripBackground = _programSettings.Colors.MenuBarBackground;
            MenuStripText = _programSettings.Colors.GenericLine;
            MenuStripBackgroundSelected = _programSettings.Colors.MenuBarBackground;
            CustomColorTable = new Extensions.OverridenColorTable();

            B_ColorMenuStripBackground.DataBindings.Add("BackColor", this, "MenuStripBackground");
            B_ColorMenuStriptTextColor.DataBindings.Add("BackColor", this, "MenuStripText");
            B_ColorMenuStripSelectedBackground.DataBindings.Add("BackColor", this, "MenuStripBackgroundSelected");

            //Overriding renderers
            UpdatePreviewColors();

        }

        private void B_Color_Click(object sender, EventArgs e)
        {
            var watbuttton = (Button)sender;
            ColorDialog coldg = new ColorDialog
            {
                Color = watbuttton.BackColor
            };
            DialogResult res = coldg.ShowDialog();
            if(res == DialogResult.OK)
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
                ColorMenuItemSelected = MenuStripBackgroundSelected,
                ColorMenuBackground = MenuStripBackground,
                TextColor = MenuStripText
            };
            tableLayoutPanel3.BackColor = MenuStripBackground;
            tableLayoutPanel3.ForeColor = MenuStripText;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(CustomColorTable);
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
    }
}
