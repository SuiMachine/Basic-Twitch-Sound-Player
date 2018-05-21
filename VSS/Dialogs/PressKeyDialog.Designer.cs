namespace BasicTwitchSoundPlayer.VSS.Dialogs
{
    partial class PressKeyDialog
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.B_RightButton = new System.Windows.Forms.Button();
            this.B_LeftMouse = new System.Windows.Forms.Button();
            this.B_Middle = new System.Windows.Forms.Button();
            this.B_Xbutton1 = new System.Windows.Forms.Button();
            this.B_Xbutton2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(567, 131);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.B_RightButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_LeftMouse, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_Middle, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_Xbutton1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.B_Xbutton2, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 90);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(555, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // B_RightButton
            // 
            this.B_RightButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_RightButton.Location = new System.Drawing.Point(119, 6);
            this.B_RightButton.Name = "B_RightButton";
            this.B_RightButton.Size = new System.Drawing.Size(111, 23);
            this.B_RightButton.TabIndex = 2;
            this.B_RightButton.Text = "Right Mouse Button";
            this.B_RightButton.UseVisualStyleBackColor = true;
            this.B_RightButton.Click += new System.EventHandler(this.B_RightButton_Click);
            // 
            // B_LeftMouse
            // 
            this.B_LeftMouse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_LeftMouse.Location = new System.Drawing.Point(5, 6);
            this.B_LeftMouse.Name = "B_LeftMouse";
            this.B_LeftMouse.Size = new System.Drawing.Size(106, 23);
            this.B_LeftMouse.TabIndex = 1;
            this.B_LeftMouse.Text = "Left Mouse Button";
            this.B_LeftMouse.UseVisualStyleBackColor = true;
            this.B_LeftMouse.Click += new System.EventHandler(this.B_LeftMouse_Click);
            // 
            // B_Middle
            // 
            this.B_Middle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Middle.Location = new System.Drawing.Point(236, 6);
            this.B_Middle.Name = "B_Middle";
            this.B_Middle.Size = new System.Drawing.Size(115, 23);
            this.B_Middle.TabIndex = 3;
            this.B_Middle.Text = "Middle Mouse Button";
            this.B_Middle.UseVisualStyleBackColor = true;
            this.B_Middle.Click += new System.EventHandler(this.B_Middle_Click);
            // 
            // B_Xbutton1
            // 
            this.B_Xbutton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Xbutton1.Location = new System.Drawing.Point(357, 6);
            this.B_Xbutton1.Name = "B_Xbutton1";
            this.B_Xbutton1.Size = new System.Drawing.Size(95, 23);
            this.B_Xbutton1.TabIndex = 4;
            this.B_Xbutton1.Text = "Mouse XButton1";
            this.B_Xbutton1.UseVisualStyleBackColor = true;
            this.B_Xbutton1.Click += new System.EventHandler(this.B_Xbutton1_Click);
            // 
            // B_Xbutton2
            // 
            this.B_Xbutton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Xbutton2.Location = new System.Drawing.Point(458, 6);
            this.B_Xbutton2.Name = "B_Xbutton2";
            this.B_Xbutton2.Size = new System.Drawing.Size(94, 23);
            this.B_Xbutton2.TabIndex = 5;
            this.B_Xbutton2.Text = "Mouse XButton2";
            this.B_Xbutton2.UseVisualStyleBackColor = true;
            this.B_Xbutton2.Click += new System.EventHandler(this.B_Xbutton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(555, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "Press any keyboard key or select one of the Mouse Buttons by pressing a button be" +
    "low.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PressKeyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 131);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PressKeyDialog";
            this.Text = "Press Key Dialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressKeyDialog_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button B_Xbutton1;
        private System.Windows.Forms.Button B_Middle;
        private System.Windows.Forms.Button B_RightButton;
        private System.Windows.Forms.Button B_LeftMouse;
        private System.Windows.Forms.Button B_Xbutton2;
        private System.Windows.Forms.Label label1;
    }
}