using System;
using System.ComponentModel;
using System.Linq;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    partial class AddEditNewEntryDialog
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.TB_RewardID = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.TB_RewardName = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.B_CreateReward = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.B_OK = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.RB_Description = new System.Windows.Forms.RichTextBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.ListB_Files = new System.Windows.Forms.ListBox();
			this.contextMenu_File = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.Num_Cooldown = new System.Windows.Forms.NumericUpDown();
			this.Num_Points = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.Num_Volume = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.contextMenu_File.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_Cooldown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Points)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Volume)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 384);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.TB_RewardID, 1, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 315);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(478, 30);
			this.tableLayoutPanel4.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Reward ID:";
			// 
			// TB_RewardID
			// 
			this.TB_RewardID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_RewardID.Enabled = false;
			this.TB_RewardID.Location = new System.Drawing.Point(79, 4);
			this.TB_RewardID.Name = "TB_RewardID";
			this.TB_RewardID.Size = new System.Drawing.Size(395, 20);
			this.TB_RewardID.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.TB_RewardName, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(478, 27);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// TB_RewardName
			// 
			this.TB_RewardName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_RewardName.Location = new System.Drawing.Point(51, 4);
			this.TB_RewardName.Name = "TB_RewardName";
			this.TB_RewardName.Size = new System.Drawing.Size(423, 20);
			this.TB_RewardName.TabIndex = 1;
			this.TB_RewardName.TextChanged += new System.EventHandler(this.TB_Command_TextChanged);
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel5.Controls.Add(this.B_CreateReward, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.B_Cancel, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.B_OK, 1, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 351);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(478, 30);
			this.tableLayoutPanel5.TabIndex = 3;
			// 
			// B_CreateReward
			// 
			this.B_CreateReward.Dock = System.Windows.Forms.DockStyle.Left;
			this.B_CreateReward.Location = new System.Drawing.Point(3, 3);
			this.B_CreateReward.Name = "B_CreateReward";
			this.B_CreateReward.Size = new System.Drawing.Size(86, 24);
			this.B_CreateReward.TabIndex = 2;
			this.B_CreateReward.Text = "Create reward";
			this.B_CreateReward.UseVisualStyleBackColor = true;
			this.B_CreateReward.Click += new System.EventHandler(this.B_CreateReward_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Cancel.Location = new System.Drawing.Point(390, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// B_OK
			// 
			this.B_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_OK.Location = new System.Drawing.Point(290, 3);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(75, 23);
			this.B_OK.TabIndex = 0;
			this.B_OK.Text = "OK";
			this.B_OK.UseVisualStyleBackColor = true;
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel6.ColumnCount = 1;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.RB_Description, 0, 1);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 36);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(478, 82);
			this.tableLayoutPanel6.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Description (Prompt):";
			// 
			// RB_Description
			// 
			this.RB_Description.DetectUrls = false;
			this.RB_Description.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RB_Description.Location = new System.Drawing.Point(4, 28);
			this.RB_Description.Name = "RB_Description";
			this.RB_Description.Size = new System.Drawing.Size(470, 50);
			this.RB_Description.TabIndex = 2;
			this.RB_Description.Text = "";
			this.RB_Description.TextChanged += new System.EventHandler(this.RB_Description_TextChanged);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.ListB_Files, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 124);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(478, 153);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Files:";
			// 
			// ListB_Files
			// 
			this.ListB_Files.AllowDrop = true;
			this.ListB_Files.ContextMenuStrip = this.contextMenu_File;
			this.ListB_Files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListB_Files.FormattingEnabled = true;
			this.ListB_Files.Location = new System.Drawing.Point(4, 25);
			this.ListB_Files.Name = "ListB_Files";
			this.ListB_Files.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListB_Files.Size = new System.Drawing.Size(471, 124);
			this.ListB_Files.TabIndex = 1;
			this.ListB_Files.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListB_Files_DragDrop);
			this.ListB_Files.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListB_Files_DragEnter);
			// 
			// contextMenu_File
			// 
			this.contextMenu_File.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileToolStripMenuItem,
            this.removeFileToolStripMenuItem});
			this.contextMenu_File.Name = "contextMenu_File";
			this.contextMenu_File.Size = new System.Drawing.Size(150, 48);
			// 
			// addFileToolStripMenuItem
			// 
			this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
			this.addFileToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.addFileToolStripMenuItem.Text = "Add file(s)";
			this.addFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
			// 
			// removeFileToolStripMenuItem
			// 
			this.removeFileToolStripMenuItem.Name = "removeFileToolStripMenuItem";
			this.removeFileToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.removeFileToolStripMenuItem.Text = "Remove file(s)";
			this.removeFileToolStripMenuItem.Click += new System.EventHandler(this.RemoveFileToolStripMenuItem_Click);
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 6;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel7.Controls.Add(this.Num_Cooldown, 5, 0);
			this.tableLayoutPanel7.Controls.Add(this.Num_Points, 3, 0);
			this.tableLayoutPanel7.Controls.Add(this.label6, 2, 0);
			this.tableLayoutPanel7.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.Num_Volume, 1, 0);
			this.tableLayoutPanel7.Controls.Add(this.label7, 4, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 283);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(478, 26);
			this.tableLayoutPanel7.TabIndex = 6;
			// 
			// Num_Cooldown
			// 
			this.Num_Cooldown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Num_Cooldown.Location = new System.Drawing.Point(402, 3);
			this.Num_Cooldown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
			this.Num_Cooldown.Name = "Num_Cooldown";
			this.Num_Cooldown.Size = new System.Drawing.Size(73, 20);
			this.Num_Cooldown.TabIndex = 7;
			this.Num_Cooldown.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
			// 
			// Num_Points
			// 
			this.Num_Points.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Num_Points.Location = new System.Drawing.Point(246, 3);
			this.Num_Points.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.Num_Points.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
			this.Num_Points.Name = "Num_Points";
			this.Num_Points.Size = new System.Drawing.Size(71, 20);
			this.Num_Points.TabIndex = 5;
			this.Num_Points.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(134, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(103, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Channel points cost:";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Volume:";
			// 
			// Num_Volume
			// 
			this.Num_Volume.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Num_Volume.Location = new System.Drawing.Point(57, 3);
			this.Num_Volume.Name = "Num_Volume";
			this.Num_Volume.Size = new System.Drawing.Size(71, 20);
			this.Num_Volume.TabIndex = 3;
			this.Num_Volume.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(323, 6);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(57, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "Cooldown:";
			// 
			// AddEditNewEntryDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 384);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 423);
			this.Name = "AddEditNewEntryDialog";
			this.ShowIcon = false;
			this.Text = "Add new entry";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddEditNewEntryDialog_FormClosed);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.contextMenu_File.ResumeLayout(false);
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_Cooldown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Points)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Volume)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_RewardName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ListB_Files;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.ContextMenuStrip contextMenu_File;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox RB_Description;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button B_CreateReward;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown Num_Volume;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.NumericUpDown Num_Points;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown Num_Cooldown;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox TB_RewardID;
	}
}