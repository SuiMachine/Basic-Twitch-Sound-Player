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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sndTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.B_RemoveEntry = new System.Windows.Forms.Button();
            this.B_AddEntry = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 484);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sndTreeView
            // 
            this.sndTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sndTreeView.Location = new System.Drawing.Point(3, 3);
            this.sndTreeView.Name = "sndTreeView";
            this.sndTreeView.Size = new System.Drawing.Size(794, 430);
            this.sndTreeView.TabIndex = 1;
            this.sndTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SndTreeView_AfterSelect);
            this.sndTreeView.DoubleClick += new System.EventHandler(this.SndTreeView_DoubleClick);
            this.sndTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SndTreeView_MouseClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 439);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 42);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.B_RemoveEntry);
            this.panel2.Controls.Add(this.B_AddEntry);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 33);
            this.panel2.TabIndex = 1;
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
            this.panel1.Location = new System.Drawing.Point(618, 4);
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
            // DB_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.tableLayoutPanel1);
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
    }
}