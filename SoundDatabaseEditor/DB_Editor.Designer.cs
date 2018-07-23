namespace BasicTwitchSoundPlayer.SoundDatabaseEditor
{
    partial class DB_Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DB_Editor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sndTreeView = new System.Windows.Forms.TreeView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.B_ExportToCSV = new System.Windows.Forms.Button();
            this.B_RemoveEntry = new System.Windows.Forms.Button();
            this.B_AddEntry = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Sort = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.sndTreeView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 526);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sndTreeView
            // 
            this.sndTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sndTreeView.ImageIndex = 0;
            this.sndTreeView.ImageList = this.iconList;
            this.sndTreeView.Location = new System.Drawing.Point(3, 3);
            this.sndTreeView.Name = "sndTreeView";
            this.sndTreeView.SelectedImageIndex = 0;
            this.sndTreeView.Size = new System.Drawing.Size(771, 472);
            this.sndTreeView.TabIndex = 1;
            this.sndTreeView.DoubleClick += new System.EventHandler(this.SndTreeView_DoubleClick);
            this.sndTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SndTreeView_KeyDown);
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "sound_player_icon_bundled.ico");
            this.iconList.Images.SetKeyName(1, "audio-volume-high-4.ico");
            this.iconList.Images.SetKeyName(2, "star.ico");
            this.iconList.Images.SetKeyName(3, "note.ico");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 481);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(771, 42);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.B_Sort);
            this.panel2.Controls.Add(this.B_ExportToCSV);
            this.panel2.Controls.Add(this.B_RemoveEntry);
            this.panel2.Controls.Add(this.B_AddEntry);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 33);
            this.panel2.TabIndex = 1;
            // 
            // B_ExportToCSV
            // 
            this.B_ExportToCSV.Location = new System.Drawing.Point(246, 5);
            this.B_ExportToCSV.Name = "B_ExportToCSV";
            this.B_ExportToCSV.Size = new System.Drawing.Size(93, 23);
            this.B_ExportToCSV.TabIndex = 2;
            this.B_ExportToCSV.Text = "Export to HTML";
            this.B_ExportToCSV.UseVisualStyleBackColor = true;
            this.B_ExportToCSV.Click += new System.EventHandler(this.B_ExportToHTML_Click);
            // 
            // B_RemoveEntry
            // 
            this.B_RemoveEntry.Location = new System.Drawing.Point(87, 5);
            this.B_RemoveEntry.Name = "B_RemoveEntry";
            this.B_RemoveEntry.Size = new System.Drawing.Size(75, 23);
            this.B_RemoveEntry.TabIndex = 1;
            this.B_RemoveEntry.Text = "Remove";
            this.B_RemoveEntry.UseVisualStyleBackColor = true;
            this.B_RemoveEntry.Click += new System.EventHandler(this.B_RemoveEntry_Click);
            // 
            // B_AddEntry
            // 
            this.B_AddEntry.Location = new System.Drawing.Point(6, 5);
            this.B_AddEntry.Name = "B_AddEntry";
            this.B_AddEntry.Size = new System.Drawing.Size(75, 23);
            this.B_AddEntry.TabIndex = 0;
            this.B_AddEntry.Text = "Add";
            this.B_AddEntry.UseVisualStyleBackColor = true;
            this.B_AddEntry.Click += new System.EventHandler(this.B_AddEntry_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel1.Controls.Add(this.B_Cancel);
            this.panel1.Controls.Add(this.B_Save);
            this.panel1.Location = new System.Drawing.Point(595, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 33);
            this.panel1.TabIndex = 0;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(87, 5);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 1;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(6, 5);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 0;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Sort
            // 
            this.B_Sort.Location = new System.Drawing.Point(168, 5);
            this.B_Sort.Name = "B_Sort";
            this.B_Sort.Size = new System.Drawing.Size(72, 23);
            this.B_Sort.TabIndex = 3;
            this.B_Sort.Text = "Sort";
            this.B_Sort.UseVisualStyleBackColor = true;
            this.B_Sort.Click += new System.EventHandler(this.B_Sort_Click);
            // 
            // DB_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 526);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(546, 260);
            this.Name = "DB_Editor";
            this.Text = "Sound Database Editor";
            this.Load += new System.EventHandler(this.DB_Editor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView sndTreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button B_AddEntry;
        private System.Windows.Forms.Button B_RemoveEntry;
        private System.Windows.Forms.Button B_ExportToCSV;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.Button B_Sort;
    }
}