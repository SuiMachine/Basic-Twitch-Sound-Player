using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    public partial class EditDialog_Right : Form
    {
        public Structs.TwitchRights RetPermission { get; set; }

        public EditDialog_Right(Structs.TwitchRights OldRight)
        {
            RetPermission = OldRight;
            InitializeComponent();
            CBox_Right.SelectedIndex = (int)RetPermission;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            RetPermission = (Structs.TwitchRights)CBox_Right.SelectedIndex;
            this.Close();
        }
    }
}
