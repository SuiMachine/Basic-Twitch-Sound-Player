namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    partial class ExportDialog
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
			this.B_ExportToCSV = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// B_ExportToCSV
			// 
			this.B_ExportToCSV.Location = new System.Drawing.Point(12, 12);
			this.B_ExportToCSV.Name = "B_ExportToCSV";
			this.B_ExportToCSV.Size = new System.Drawing.Size(93, 23);
			this.B_ExportToCSV.TabIndex = 5;
			this.B_ExportToCSV.Text = "Export to HTML";
			this.B_ExportToCSV.UseVisualStyleBackColor = true;
			this.B_ExportToCSV.Click += new System.EventHandler(this.B_ExportToCSV_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Location = new System.Drawing.Point(111, 12);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(69, 23);
			this.B_Cancel.TabIndex = 7;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// ExportDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(187, 42);
			this.ControlBox = false;
			this.Controls.Add(this.B_Cancel);
			this.Controls.Add(this.B_ExportToCSV);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export to...";
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button B_ExportToCSV;
        private System.Windows.Forms.Button B_Cancel;
    }
}