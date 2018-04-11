namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    partial class EditDialog_Right
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
            this.CBox_Right = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CBox_Right
            // 
            this.CBox_Right.FormattingEnabled = true;
            this.CBox_Right.Items.AddRange(new object[] {
            "Disabled",
            "Public",
            "Subscriber / Trusted",
            "Moderator",
            "Administrator"});
            this.CBox_Right.Location = new System.Drawing.Point(123, 12);
            this.CBox_Right.Name = "CBox_Right";
            this.CBox_Right.Size = new System.Drawing.Size(196, 21);
            this.CBox_Right.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Required permission:";
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(60, 43);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(75, 23);
            this.B_OK.TabIndex = 2;
            this.B_OK.Text = "OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(141, 43);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // EditDialog_Right
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 78);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBox_Right);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDialog_Right";
            this.Text = "Select Permission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBox_Right;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
    }
}