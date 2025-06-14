namespace SSC.SettingsForms.AI_Overrides_Forms
{
	partial class UserOverrideSelectionForm
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
			this.List_Nicknames = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Close = new System.Windows.Forms.Button();
			this.B_Add = new System.Windows.Forms.Button();
			this.contextMenuStuff = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.contextMenuStuff.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.List_Nicknames, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(638, 450);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// List_Nicknames
			// 
			this.List_Nicknames.ContextMenuStrip = this.contextMenuStuff;
			this.List_Nicknames.Dock = System.Windows.Forms.DockStyle.Fill;
			this.List_Nicknames.FormattingEnabled = true;
			this.List_Nicknames.Location = new System.Drawing.Point(3, 3);
			this.List_Nicknames.Name = "List_Nicknames";
			this.List_Nicknames.Size = new System.Drawing.Size(632, 409);
			this.List_Nicknames.TabIndex = 0;
			this.List_Nicknames.DoubleClick += new System.EventHandler(this.List_Nicknames_EditElement);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
			this.tableLayoutPanel2.Controls.Add(this.B_Close, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Add, 0, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(444, 418);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(191, 29);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// B_Close
			// 
			this.B_Close.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Close.Location = new System.Drawing.Point(113, 4);
			this.B_Close.Name = "B_Close";
			this.B_Close.Size = new System.Drawing.Size(75, 21);
			this.B_Close.TabIndex = 1;
			this.B_Close.Text = "Close";
			this.B_Close.UseVisualStyleBackColor = true;
			this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
			// 
			// B_Add
			// 
			this.B_Add.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Add.Location = new System.Drawing.Point(29, 3);
			this.B_Add.Name = "B_Add";
			this.B_Add.Size = new System.Drawing.Size(75, 23);
			this.B_Add.TabIndex = 0;
			this.B_Add.Text = "Add";
			this.B_Add.UseVisualStyleBackColor = true;
			this.B_Add.Click += new System.EventHandler(this.List_Nicknames_Add_Click);
			// 
			// contextMenuStuff
			// 
			this.contextMenuStuff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.addToolStripMenuItem,
			this.deleteToolStripMenuItem,
			this.cancelToolStripMenuItem});
			this.contextMenuStuff.Name = "contextMenuStuff";
			this.contextMenuStuff.Size = new System.Drawing.Size(181, 92);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.addToolStripMenuItem.Text = "Add";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.List_Nicknames_Add_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// cancelToolStripMenuItem
			// 
			this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
			this.cancelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cancelToolStripMenuItem.Text = "Cancel";
			// 
			// UserOverrideSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(638, 450);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "UserOverrideSelectionForm";
			this.Text = "AI Overrides";
			this.Load += new System.EventHandler(this.UserOverrideSelectionForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.contextMenuStuff.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ListBox List_Nicknames;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button B_Close;
		private System.Windows.Forms.Button B_Add;
		private System.Windows.Forms.ContextMenuStrip contextMenuStuff;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
	}
}