﻿namespace BasicTwitchSoundPlayer.SettingsForms
{
    partial class ConnectionSettingsForm
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
			this.B_Save = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.B_GetLoginData = new System.Windows.Forms.Button();
			this.CB_ShowPassword = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.TB_Username = new System.Windows.Forms.TextBox();
			this.TB_ChannelToJoin = new System.Windows.Forms.TextBox();
			this.TB_Password = new System.Windows.Forms.TextBox();
			this.TB_Port = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.TB_Server = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.TB_GoogleSpreadsheetID = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.TB_VoiceMod_AdressPort = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.TB_VoiceModApiKey = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.CB_VoiceModRedemptionLogic = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.B_CreateVoiceModReward = new System.Windows.Forms.Button();
			this.TB_VoiceModRewardID = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 369);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.B_Save, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 329);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(362, 40);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// B_Save
			// 
			this.B_Save.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Save.Location = new System.Drawing.Point(53, 8);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(75, 23);
			this.B_Save.TabIndex = 0;
			this.B_Save.Text = "Save";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Cancel.Location = new System.Drawing.Point(234, 8);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.B_GetLoginData);
			this.panel1.Controls.Add(this.CB_ShowPassword);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.TB_Username);
			this.panel1.Controls.Add(this.TB_ChannelToJoin);
			this.panel1.Controls.Add(this.TB_Password);
			this.panel1.Controls.Add(this.TB_Port);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.TB_Server);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(362, 165);
			this.panel1.TabIndex = 1;
			// 
			// B_GetLoginData
			// 
			this.B_GetLoginData.Location = new System.Drawing.Point(221, 136);
			this.B_GetLoginData.Name = "B_GetLoginData";
			this.B_GetLoginData.Size = new System.Drawing.Size(132, 23);
			this.B_GetLoginData.TabIndex = 11;
			this.B_GetLoginData.Text = "Obtain New Login Data";
			this.B_GetLoginData.UseVisualStyleBackColor = true;
			this.B_GetLoginData.Click += new System.EventHandler(this.B_GetLoginData_Click);
			// 
			// CB_ShowPassword
			// 
			this.CB_ShowPassword.AutoSize = true;
			this.CB_ShowPassword.Location = new System.Drawing.Point(93, 113);
			this.CB_ShowPassword.Name = "CB_ShowPassword";
			this.CB_ShowPassword.Size = new System.Drawing.Size(102, 17);
			this.CB_ShowPassword.TabIndex = 10;
			this.CB_ShowPassword.Text = "Show Password";
			this.CB_ShowPassword.UseVisualStyleBackColor = true;
			this.CB_ShowPassword.CheckedChanged += new System.EventHandler(this.CB_ShowPassword_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Port:";
			// 
			// TB_Username
			// 
			this.TB_Username.Location = new System.Drawing.Point(93, 61);
			this.TB_Username.Name = "TB_Username";
			this.TB_Username.Size = new System.Drawing.Size(260, 20);
			this.TB_Username.TabIndex = 8;
			this.TB_Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TB_ChannelToJoin
			// 
			this.TB_ChannelToJoin.Location = new System.Drawing.Point(93, 136);
			this.TB_ChannelToJoin.Name = "TB_ChannelToJoin";
			this.TB_ChannelToJoin.Size = new System.Drawing.Size(122, 20);
			this.TB_ChannelToJoin.TabIndex = 7;
			this.TB_ChannelToJoin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TB_Password
			// 
			this.TB_Password.Enabled = false;
			this.TB_Password.Location = new System.Drawing.Point(93, 87);
			this.TB_Password.Name = "TB_Password";
			this.TB_Password.Size = new System.Drawing.Size(260, 20);
			this.TB_Password.TabIndex = 6;
			this.TB_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TB_Password.UseSystemPasswordChar = true;
			// 
			// TB_Port
			// 
			this.TB_Port.Enabled = false;
			this.TB_Port.Location = new System.Drawing.Point(93, 35);
			this.TB_Port.Name = "TB_Port";
			this.TB_Port.Size = new System.Drawing.Size(260, 20);
			this.TB_Port.TabIndex = 5;
			this.TB_Port.Text = "6667";
			this.TB_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 139);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Channel to join:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Password:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Username:";
			// 
			// TB_Server
			// 
			this.TB_Server.Location = new System.Drawing.Point(93, 9);
			this.TB_Server.Name = "TB_Server";
			this.TB_Server.Size = new System.Drawing.Size(260, 20);
			this.TB_Server.TabIndex = 1;
			this.TB_Server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.TB_GoogleSpreadsheetID);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 174);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(362, 31);
			this.panel2.TabIndex = 2;
			// 
			// TB_GoogleSpreadsheetID
			// 
			this.TB_GoogleSpreadsheetID.Location = new System.Drawing.Point(101, 3);
			this.TB_GoogleSpreadsheetID.Name = "TB_GoogleSpreadsheetID";
			this.TB_GoogleSpreadsheetID.Size = new System.Drawing.Size(252, 20);
			this.TB_GoogleSpreadsheetID.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Google Sheet ID";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.TB_VoiceModRewardID);
			this.panel3.Controls.Add(this.B_CreateVoiceModReward);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Controls.Add(this.CB_VoiceModRedemptionLogic);
			this.panel3.Controls.Add(this.TB_VoiceMod_AdressPort);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.TB_VoiceModApiKey);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 211);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(362, 112);
			this.panel3.TabIndex = 3;
			// 
			// TB_VoiceMod_AdressPort
			// 
			this.TB_VoiceMod_AdressPort.Location = new System.Drawing.Point(101, 29);
			this.TB_VoiceMod_AdressPort.Name = "TB_VoiceMod_AdressPort";
			this.TB_VoiceMod_AdressPort.Size = new System.Drawing.Size(252, 20);
			this.TB_VoiceMod_AdressPort.TabIndex = 6;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 32);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(69, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "Adress / Port";
			// 
			// TB_VoiceModApiKey
			// 
			this.TB_VoiceModApiKey.Location = new System.Drawing.Point(101, 3);
			this.TB_VoiceModApiKey.Name = "TB_VoiceModApiKey";
			this.TB_VoiceModApiKey.PasswordChar = '*';
			this.TB_VoiceModApiKey.Size = new System.Drawing.Size(252, 20);
			this.TB_VoiceModApiKey.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 6);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Voice Mod Key";
			// 
			// CB_VoiceModRedemptionLogic
			// 
			this.CB_VoiceModRedemptionLogic.FormattingEnabled = true;
			this.CB_VoiceModRedemptionLogic.Location = new System.Drawing.Point(101, 55);
			this.CB_VoiceModRedemptionLogic.Name = "CB_VoiceModRedemptionLogic";
			this.CB_VoiceModRedemptionLogic.Size = new System.Drawing.Size(252, 21);
			this.CB_VoiceModRedemptionLogic.TabIndex = 7;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 58);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(92, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "Redemption logic:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(9, 86);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(61, 13);
			this.label10.TabIndex = 9;
			this.label10.Text = "Reward ID:";
			// 
			// B_CreateVoiceModReward
			// 
			this.B_CreateVoiceModReward.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_CreateVoiceModReward.Location = new System.Drawing.Point(278, 81);
			this.B_CreateVoiceModReward.Name = "B_CreateVoiceModReward";
			this.B_CreateVoiceModReward.Size = new System.Drawing.Size(75, 23);
			this.B_CreateVoiceModReward.TabIndex = 10;
			this.B_CreateVoiceModReward.Text = "Create";
			this.B_CreateVoiceModReward.UseVisualStyleBackColor = true;
			this.B_CreateVoiceModReward.Click += new System.EventHandler(this.B_CreateVoiceModReward_Click);
			// 
			// TB_VoiceModRewardID
			// 
			this.TB_VoiceModRewardID.Location = new System.Drawing.Point(76, 82);
			this.TB_VoiceModRewardID.Name = "TB_VoiceModRewardID";
			this.TB_VoiceModRewardID.PasswordChar = '*';
			this.TB_VoiceModRewardID.Size = new System.Drawing.Size(190, 20);
			this.TB_VoiceModRewardID.TabIndex = 11;
			// 
			// ConnectionSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(368, 369);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionSettingsForm";
			this.Text = "Connection Settings";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TB_Server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_ChannelToJoin;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_Username;
        private System.Windows.Forms.CheckBox CB_ShowPassword;
        private System.Windows.Forms.Button B_GetLoginData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TB_GoogleSpreadsheetID;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox TB_VoiceModApiKey;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.TextBox TB_VoiceMod_AdressPort;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox TB_VoiceModRewardID;
		private System.Windows.Forms.Button B_CreateVoiceModReward;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox CB_VoiceModRedemptionLogic;
	}
}