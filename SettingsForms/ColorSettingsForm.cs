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


        public ColorSettingsForm(PrivateSettings _programSettings)
        {
            InitializeComponent();

            MenuStripBackground = _programSettings.Colors.MenuBarBackground;
            MenuStripText = _programSettings.Colors.GenericLine;
            MenuStripBackgroundSelected = _programSettings.Colors.MenuBarBackground;

            B_ColorMenuStripBackground.DataBindings.Add("BackColor", this, "MenuStripBackground");
            B_ColorMenuStriptTextColor.DataBindings.Add("BackColor", this, "MenuStripText");
            B_ColorMenuStripSelectedBackground.DataBindings.Add("BackColor", this, "MenuStripBackgroundSelected");
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
            UpdatePreviewColors();
        }

        private void UpdatePreviewColors()
        {
            menuStrip1.BackColor = MenuStripBackground;
            menuStrip1.ForeColor = MenuStripText;
        }
    }
}
