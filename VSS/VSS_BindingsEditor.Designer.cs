namespace BasicTwitchSoundPlayer.VSS
{
    partial class VSS_BindingsEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VSS_BindingsEditor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.VSS_Tree = new System.Windows.Forms.TreeView();
            this.contexMenuVSS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VSS_Icons = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_OK = new System.Windows.Forms.Button();
            this.TB_Key = new System.Windows.Forms.TextBox();
            this.B_SetKey = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.contexMenuVSS.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.VSS_Tree, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // VSS_Tree
            // 
            this.VSS_Tree.ContextMenuStrip = this.contexMenuVSS;
            this.VSS_Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VSS_Tree.ImageIndex = 1;
            this.VSS_Tree.ImageList = this.VSS_Icons;
            this.VSS_Tree.Location = new System.Drawing.Point(3, 3);
            this.VSS_Tree.Name = "VSS_Tree";
            this.VSS_Tree.SelectedImageIndex = 0;
            this.VSS_Tree.Size = new System.Drawing.Size(794, 404);
            this.VSS_Tree.TabIndex = 0;
            this.VSS_Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.VSS_Tree_AfterSelect);
            this.VSS_Tree.DoubleClick += new System.EventHandler(this.VSS_Tree_DoubleClick);
            this.VSS_Tree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VSS_Tree_KeyUp);
            // 
            // contexMenuVSS
            // 
            this.contexMenuVSS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.addSoundToolStripMenuItem,
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.contexMenuVSS.Name = "contexMenuVSS";
            this.contexMenuVSS.Size = new System.Drawing.Size(148, 114);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addToolStripMenuItem.Text = "Add Category";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // addSoundToolStripMenuItem
            // 
            this.addSoundToolStripMenuItem.Name = "addSoundToolStripMenuItem";
            this.addSoundToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addSoundToolStripMenuItem.Text = "Add Sound";
            this.addSoundToolStripMenuItem.Click += new System.EventHandler(this.AddSoundToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            // 
            // VSS_Icons
            // 
            this.VSS_Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("VSS_Icons.ImageStream")));
            this.VSS_Icons.TransparentColor = System.Drawing.Color.Transparent;
            this.VSS_Icons.Images.SetKeyName(0, "sound_player_icon_bundled.ico");
            this.VSS_Icons.Images.SetKeyName(1, "folder-2.ico");
            this.VSS_Icons.Images.SetKeyName(2, "audio-volume-high-4.ico");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_OK, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.TB_Key, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_SetKey, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 413);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Cancel.Location = new System.Drawing.Point(709, 5);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 0;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_OK
            // 
            this.B_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_OK.Location = new System.Drawing.Point(615, 5);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(75, 23);
            this.B_OK.TabIndex = 1;
            this.B_OK.Text = "OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // TB_Key
            // 
            this.TB_Key.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TB_Key.Location = new System.Drawing.Point(3, 7);
            this.TB_Key.Name = "TB_Key";
            this.TB_Key.ReadOnly = true;
            this.TB_Key.ShortcutsEnabled = false;
            this.TB_Key.Size = new System.Drawing.Size(117, 20);
            this.TB_Key.TabIndex = 2;
            this.TB_Key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Key.WordWrap = false;
            // 
            // B_SetKey
            // 
            this.B_SetKey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_SetKey.Location = new System.Drawing.Point(145, 5);
            this.B_SetKey.Name = "B_SetKey";
            this.B_SetKey.Size = new System.Drawing.Size(75, 23);
            this.B_SetKey.TabIndex = 3;
            this.B_SetKey.Text = "Set key";
            this.B_SetKey.UseVisualStyleBackColor = true;
            this.B_SetKey.Click += new System.EventHandler(this.B_SetKey_Click);
            // 
            // VSS_BindingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VSS_BindingsEditor";
            this.Text = "VSS Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VSS_BindingsEditor_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contexMenuVSS.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView VSS_Tree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.ImageList VSS_Icons;
        private System.Windows.Forms.TextBox TB_Key;
        private System.Windows.Forms.Button B_SetKey;
        private System.Windows.Forms.ContextMenuStrip contexMenuVSS;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}