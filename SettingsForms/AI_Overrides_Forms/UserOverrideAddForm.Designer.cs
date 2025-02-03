namespace BasicTwitchSoundPlayer.SettingsForms.AI_Overrides_Forms
{
	partial class UserOverrideAddForm
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
			this.TB_Username = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.B_OK = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TB_Username
			// 
			this.TB_Username.Location = new System.Drawing.Point(76, 12);
			this.TB_Username.Name = "TB_Username";
			this.TB_Username.Size = new System.Drawing.Size(213, 20);
			this.TB_Username.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Username:";
			// 
			// B_OK
			// 
			this.B_OK.Location = new System.Drawing.Point(133, 38);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(75, 23);
			this.B_OK.TabIndex = 2;
			this.B_OK.Text = "OK";
			this.B_OK.UseVisualStyleBackColor = true;
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Location = new System.Drawing.Point(214, 38);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 3;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// UserOverrideAddForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(301, 70);
			this.Controls.Add(this.B_Cancel);
			this.Controls.Add(this.B_OK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TB_Username);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserOverrideAddForm";
			this.Text = "Add user override";
			this.Load += new System.EventHandler(this.UserOverrideAddForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TB_Username;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button B_OK;
		private System.Windows.Forms.Button B_Cancel;
	}
}