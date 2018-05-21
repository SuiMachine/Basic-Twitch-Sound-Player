using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS.Dialogs
{
    public partial class PressKeyDialog : Form
    {
        public Keys ReturnedKey = Keys.None;

        public PressKeyDialog()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            this.Select();
        }

        private void B_LeftMouse_Click(object sender, EventArgs e)
        {
            ReturnedKey = Keys.LButton;
            DialogResult = DialogResult.OK;

        }

        private void B_RightButton_Click(object sender, EventArgs e)
        {
            ReturnedKey = Keys.RButton;
            DialogResult = DialogResult.OK;

        }

        private void B_Middle_Click(object sender, EventArgs e)
        {
            ReturnedKey = Keys.MButton;
            DialogResult = DialogResult.OK;
        }

        private void B_Xbutton1_Click(object sender, EventArgs e)
        {
            ReturnedKey = Keys.XButton1;
            DialogResult = DialogResult.OK;
        }

        private void B_Xbutton2_Click(object sender, EventArgs e)
        {
            ReturnedKey = Keys.XButton2;
            DialogResult = DialogResult.OK;
        }

        private void PressKeyDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                ReturnedKey = e.KeyCode;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
