using System;
using System.ComponentModel;
using System.Linq;

namespace BasicTwitchSoundPlayer.SoundDatabaseEditor.EditDialogues
{
    partial class SoundPlaybackSettingsDialog
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

        private void AddComboboxDataSources()
        {
            CBox_RedemptionLogic.DisplayMember = "Description";
            CBox_RedemptionLogic.ValueMember = "value";
            CBox_RedemptionLogic.DataSource = Enum.GetValues(typeof(BasicTwitchSoundPlayer.Structs.SoundRedemptionLogic)).Cast<Enum>().Select(value =>
            new
            {
                (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                value
            }).ToList();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.B_OK = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CB_OutputDevices = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TB_SoundRewardID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CBox_RedemptionLogic = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.B_CreateReward = new System.Windows.Forms.Button();
			this.B_VerifyReward = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 126);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 2;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Controls.Add(this.B_OK, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Right;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(287, 93);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(194, 30);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// B_OK
			// 
			this.B_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_OK.Location = new System.Drawing.Point(11, 3);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(75, 23);
			this.B_OK.TabIndex = 0;
			this.B_OK.Text = "OK";
			this.B_OK.UseVisualStyleBackColor = true;
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Cancel.Location = new System.Drawing.Point(108, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.B_VerifyReward);
			this.panel1.Controls.Add(this.B_CreateReward);
			this.panel1.Controls.Add(this.CB_OutputDevices);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.TB_SoundRewardID);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.CBox_RedemptionLogic);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(478, 84);
			this.panel1.TabIndex = 2;
			// 
			// CB_OutputDevices
			// 
			this.CB_OutputDevices.FormattingEnabled = true;
			this.CB_OutputDevices.Location = new System.Drawing.Point(86, 3);
			this.CB_OutputDevices.Name = "CB_OutputDevices";
			this.CB_OutputDevices.Size = new System.Drawing.Size(380, 21);
			this.CB_OutputDevices.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(130, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Sound Redemption Logic:";
			// 
			// TB_SoundRewardID
			// 
			this.TB_SoundRewardID.Location = new System.Drawing.Point(104, 57);
			this.TB_SoundRewardID.Name = "TB_SoundRewardID";
			this.TB_SoundRewardID.Size = new System.Drawing.Size(180, 20);
			this.TB_SoundRewardID.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Sound Reward ID:";
			// 
			// CBox_RedemptionLogic
			// 
			this.CBox_RedemptionLogic.FormattingEnabled = true;
			this.CBox_RedemptionLogic.Location = new System.Drawing.Point(139, 30);
			this.CBox_RedemptionLogic.Name = "CBox_RedemptionLogic";
			this.CBox_RedemptionLogic.Size = new System.Drawing.Size(327, 21);
			this.CBox_RedemptionLogic.TabIndex = 1;
			this.CBox_RedemptionLogic.SelectedIndexChanged += new System.EventHandler(this.CBox_RedemptionLogic_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Output device:";
			// 
			// B_CreateReward
			// 
			this.B_CreateReward.Location = new System.Drawing.Point(290, 55);
			this.B_CreateReward.Name = "B_CreateReward";
			this.B_CreateReward.Size = new System.Drawing.Size(85, 23);
			this.B_CreateReward.TabIndex = 0;
			this.B_CreateReward.Text = "Create reward";
			this.B_CreateReward.UseVisualStyleBackColor = true;
			this.B_CreateReward.Click += new System.EventHandler(this.B_CreateReward_Click);
			// 
			// B_VerifyReward
			// 
			this.B_VerifyReward.Location = new System.Drawing.Point(381, 55);
			this.B_VerifyReward.Name = "B_VerifyReward";
			this.B_VerifyReward.Size = new System.Drawing.Size(85, 23);
			this.B_VerifyReward.TabIndex = 1;
			this.B_VerifyReward.Text = "Verify reward";
			this.B_VerifyReward.UseVisualStyleBackColor = true;
			this.B_VerifyReward.Click += new System.EventHandler(this.B_VerifyReward_Click);
			// 
			// SoundPlaybackSettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 126);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SoundPlaybackSettingsDialog";
			this.Text = "Sound Playback Settings";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBox_RedemptionLogic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_SoundRewardID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox CB_OutputDevices;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button B_CreateReward;
		private System.Windows.Forms.Button B_VerifyReward;
	}
}