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
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.B_AddEntry = new System.Windows.Forms.Button();
			this.B_Sort = new System.Windows.Forms.Button();
			this.B_RemoveEntry = new System.Windows.Forms.Button();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.B_SoundPlayBackSettings = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.B_Save = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 591);
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
			this.sndTreeView.Size = new System.Drawing.Size(804, 535);
			this.sndTreeView.TabIndex = 1;
			this.sndTreeView.DoubleClick += new System.EventHandler(this.SndTreeView_DoubleClick);
			this.sndTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SndTreeView_KeyDown);
			// 
			// iconList
			// 
			this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
			this.iconList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconList.Images.SetKeyName(0, "sound_player_icon_bundled.ico");
			this.iconList.Images.SetKeyName(1, "star.ico");
			this.iconList.Images.SetKeyName(2, "note.ico");
			this.iconList.Images.SetKeyName(3, "audio-volume-high-4.ico");
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 544);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(804, 44);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
			this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 2, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(798, 38);
			this.tableLayoutPanel4.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.Controls.Add(this.B_AddEntry, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.B_Sort, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.B_RemoveEntry, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(209, 32);
			this.tableLayoutPanel3.TabIndex = 2;
			// 
			// B_AddEntry
			// 
			this.B_AddEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_AddEntry.Location = new System.Drawing.Point(4, 4);
			this.B_AddEntry.Name = "B_AddEntry";
			this.B_AddEntry.Size = new System.Drawing.Size(62, 23);
			this.B_AddEntry.TabIndex = 0;
			this.B_AddEntry.Text = "Add";
			this.B_AddEntry.UseVisualStyleBackColor = true;
			this.B_AddEntry.Click += new System.EventHandler(this.B_AddEntry_Click);
			// 
			// B_Sort
			// 
			this.B_Sort.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Sort.Location = new System.Drawing.Point(142, 4);
			this.B_Sort.Name = "B_Sort";
			this.B_Sort.Size = new System.Drawing.Size(63, 23);
			this.B_Sort.TabIndex = 3;
			this.B_Sort.Text = "Sort";
			this.B_Sort.UseVisualStyleBackColor = true;
			this.B_Sort.Click += new System.EventHandler(this.B_Sort_Click);
			// 
			// B_RemoveEntry
			// 
			this.B_RemoveEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_RemoveEntry.Location = new System.Drawing.Point(73, 4);
			this.B_RemoveEntry.Name = "B_RemoveEntry";
			this.B_RemoveEntry.Size = new System.Drawing.Size(62, 23);
			this.B_RemoveEntry.TabIndex = 1;
			this.B_RemoveEntry.Text = "Remove";
			this.B_RemoveEntry.UseVisualStyleBackColor = true;
			this.B_RemoveEntry.Click += new System.EventHandler(this.B_RemoveEntry_Click);
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel5.Controls.Add(this.B_SoundPlayBackSettings, 0, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Left;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(218, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(168, 32);
			this.tableLayoutPanel5.TabIndex = 3;
			// 
			// B_SoundPlayBackSettings
			// 
			this.B_SoundPlayBackSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_SoundPlayBackSettings.Location = new System.Drawing.Point(8, 4);
			this.B_SoundPlayBackSettings.Name = "B_SoundPlayBackSettings";
			this.B_SoundPlayBackSettings.Size = new System.Drawing.Size(154, 23);
			this.B_SoundPlayBackSettings.TabIndex = 6;
			this.B_SoundPlayBackSettings.Text = "Sound Playback Settings";
			this.B_SoundPlayBackSettings.UseVisualStyleBackColor = true;
			this.B_SoundPlayBackSettings.Click += new System.EventHandler(this.B_SoundPlayBackSettings_Click);
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel6.ColumnCount = 2;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.B_Save, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Right;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(654, 3);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(141, 32);
			this.tableLayoutPanel6.TabIndex = 4;
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Cancel.Location = new System.Drawing.Point(74, 4);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(63, 23);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// B_Save
			// 
			this.B_Save.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Save.Location = new System.Drawing.Point(4, 4);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(63, 23);
			this.B_Save.TabIndex = 0;
			this.B_Save.Text = "Save";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// DB_Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(810, 591);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(726, 564);
			this.Name = "DB_Editor";
			this.Text = "Sound Database Editor";
			this.Load += new System.EventHandler(this.DB_Editor_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView sndTreeView;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button B_AddEntry;
        private System.Windows.Forms.Button B_Sort;
        private System.Windows.Forms.Button B_RemoveEntry;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button B_SoundPlayBackSettings;
    }
}