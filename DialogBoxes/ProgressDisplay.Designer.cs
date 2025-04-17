namespace BasicTwitchSoundPlayer.DialogBoxes
{
	partial class ProgressDisplay
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
			this.pb_Progress = new System.Windows.Forms.ProgressBar();
			this.L_Progress_Text = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pb_Progress
			// 
			this.pb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pb_Progress.Location = new System.Drawing.Point(12, 35);
			this.pb_Progress.Name = "pb_Progress";
			this.pb_Progress.Size = new System.Drawing.Size(444, 23);
			this.pb_Progress.TabIndex = 0;
			// 
			// L_Progress_Text
			// 
			this.L_Progress_Text.Location = new System.Drawing.Point(12, 9);
			this.L_Progress_Text.Name = "L_Progress_Text";
			this.L_Progress_Text.Size = new System.Drawing.Size(444, 23);
			this.L_Progress_Text.TabIndex = 1;
			this.L_Progress_Text.Text = "Please wait...";
			this.L_Progress_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgressDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(468, 68);
			this.ControlBox = false;
			this.Controls.Add(this.L_Progress_Text);
			this.Controls.Add(this.pb_Progress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressDisplay";
			this.Text = "Progress Display";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProgressDisplay_FormClosed);
			this.Load += new System.EventHandler(this.ProgressDisplay_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar pb_Progress;
		private System.Windows.Forms.Label L_Progress_Text;
	}
}