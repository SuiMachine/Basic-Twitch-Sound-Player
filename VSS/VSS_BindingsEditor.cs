using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTwitchSoundPlayer.VSS
{
    public partial class VSS_BindingsEditor : Form
    {
        public VSS_Entry_Group VSS_RootEntry { get; set; }

        public VSS_BindingsEditor(VSS_Entry_Group VSS_RootNode)
        {
            InitializeComponent();
            this.VSS_RootEntry = VSS_RootNode;
            LoadExampleNodes();
            LoadVSSNodes();
        }

        private void LoadExampleNodes()
        {
            VSS_RootEntry = new VSS_Entry_Group("VSS Root", Keys.V);
            var Category1 = VSS_RootEntry.AddChild(new VSS_Entry_Group("Category 1", Keys.Q));
            ((VSS_Entry_Group)Category1).AddChild(new VSS_Entry_Sound("Sound 1", Keys.Q, "1"));
            ((VSS_Entry_Group)Category1).AddChild(new VSS_Entry_Sound("Sound 2", Keys.W, "2"));
            ((VSS_Entry_Group)Category1).AddChild(new VSS_Entry_Sound("Sound 3", Keys.E, "3"));
            var Category2 = VSS_RootEntry.AddChild(new VSS_Entry_Group("Category 2", Keys.W));
            ((VSS_Entry_Group)Category2).AddChild(new VSS_Entry_Sound("Sound 1", Keys.Q, "1"));
            ((VSS_Entry_Group)Category2).AddChild(new VSS_Entry_Sound("Sound 2", Keys.W, "2"));
            var Category3 = VSS_RootEntry.AddChild(new VSS_Entry_Group("Category 3", Keys.E));
            var subcategory = ((VSS_Entry_Group)Category3).AddChild(new VSS_Entry_Group("Sound 1", Keys.Q));
            ((VSS_Entry_Group)subcategory).AddChild(new VSS_Entry_Sound("Sound 1", Keys.Q, "1"));
            ((VSS_Entry_Group)subcategory).AddChild(new VSS_Entry_Sound("Sound 2", Keys.W, "2"));
            var Category4 = VSS_RootEntry.AddChild(new VSS_Entry_Group("Category 4", Keys.R));
            ((VSS_Entry_Group)Category4).AddChild(new VSS_Entry_Sound("Sound 1", Keys.Q, "1"));
            ((VSS_Entry_Group)Category4).AddChild(new VSS_Entry_Sound("Sound 2", Keys.W, "2"));
            ((VSS_Entry_Group)Category4).AddChild(new VSS_Entry_Sound("Sound 3", Keys.E, "3"));
            ((VSS_Entry_Group)Category4).AddChild(new VSS_Entry_Sound("Sound 4", Keys.R, "4"));
        }

        private void LoadVSSNodes()
        {
            var tree = this.VSS_Tree.Nodes;
            tree.Clear();
            var rootNode = VSS_Entry.ConvertVSSEntryToTreeNode(VSS_RootEntry);
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            rootNode.StateImageIndex = 0;
            tree.Add(rootNode);
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
    }
}
