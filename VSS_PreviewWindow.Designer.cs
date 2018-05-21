namespace BasicTwitchSoundPlayer
{
    partial class VSS_PreviewWindow
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
            this.ResetTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxVSSPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVSSPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // ResetTimer
            // 
            this.ResetTimer.Interval = 2500;
            this.ResetTimer.Tick += new System.EventHandler(this.ResetTimer_Tick);
            // 
            // pictureBoxVSSPreview
            // 
            this.pictureBoxVSSPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxVSSPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVSSPreview.Name = "pictureBoxVSSPreview";
            this.pictureBoxVSSPreview.Size = new System.Drawing.Size(280, 377);
            this.pictureBoxVSSPreview.TabIndex = 0;
            this.pictureBoxVSSPreview.TabStop = false;
            // 
            // VSS_PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 377);
            this.Controls.Add(this.pictureBoxVSSPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VSS_PreviewWindow";
            this.Text = "VSS Preview Window";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VSS_PreviewWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVSSPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ResetTimer;
        private System.Windows.Forms.PictureBox pictureBoxVSSPreview;
    }
}