namespace BasicTwitchSoundPlayer.SettingsForms
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
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
			this.CB_ShowPasswordUser = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.CB_ShowBotAuth = new System.Windows.Forms.CheckBox();
			this.B_GetLoginData = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Num_PortUsed = new System.Windows.Forms.NumericUpDown();
			this.CB_Websocket = new System.Windows.Forms.CheckBox();
			this.CB_DebugMode = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.TB_UserAuth = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.TB_BotAuth = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_PortUsed)).BeginInit();
			this.tableLayoutPanel8.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 337F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 166);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.B_Save, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 125);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(612, 37);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// B_Save
			// 
			this.B_Save.Dock = System.Windows.Forms.DockStyle.Right;
			this.B_Save.Location = new System.Drawing.Point(228, 3);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(75, 31);
			this.B_Save.TabIndex = 0;
			this.B_Save.Text = "Save";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Left;
			this.B_Cancel.Location = new System.Drawing.Point(309, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 31);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(612, 116);
			this.panel1.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(612, 116);
			this.tableLayoutPanel3.TabIndex = 25;
			// 
			// tableLayoutPanel9
			// 
			this.tableLayoutPanel9.ColumnCount = 4;
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel9.Controls.Add(this.CB_ShowPasswordUser, 1, 0);
			this.tableLayoutPanel9.Controls.Add(this.button1, 2, 0);
			this.tableLayoutPanel9.Controls.Add(this.CB_ShowBotAuth, 3, 0);
			this.tableLayoutPanel9.Controls.Add(this.B_GetLoginData, 0, 0);
			this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 75);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 1;
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Size = new System.Drawing.Size(606, 38);
			this.tableLayoutPanel9.TabIndex = 5;
			// 
			// CB_ShowPasswordUser
			// 
			this.CB_ShowPasswordUser.AutoSize = true;
			this.CB_ShowPasswordUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_ShowPasswordUser.Location = new System.Drawing.Point(154, 3);
			this.CB_ShowPasswordUser.Name = "CB_ShowPasswordUser";
			this.CB_ShowPasswordUser.Size = new System.Drawing.Size(145, 32);
			this.CB_ShowPasswordUser.TabIndex = 10;
			this.CB_ShowPasswordUser.Text = "Show user auth";
			this.CB_ShowPasswordUser.UseVisualStyleBackColor = true;
			this.CB_ShowPasswordUser.CheckedChanged += new System.EventHandler(this.CB_ShowPassword_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point(305, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(145, 32);
			this.button1.TabIndex = 24;
			this.button1.Text = "Obtain auth (manual)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.B_GetLoginDataManual_Click);
			// 
			// CB_ShowBotAuth
			// 
			this.CB_ShowBotAuth.AutoSize = true;
			this.CB_ShowBotAuth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_ShowBotAuth.Location = new System.Drawing.Point(456, 3);
			this.CB_ShowBotAuth.Name = "CB_ShowBotAuth";
			this.CB_ShowBotAuth.Size = new System.Drawing.Size(147, 32);
			this.CB_ShowBotAuth.TabIndex = 22;
			this.CB_ShowBotAuth.Text = "Show bot auth";
			this.CB_ShowBotAuth.UseVisualStyleBackColor = true;
			this.CB_ShowBotAuth.CheckedChanged += new System.EventHandler(this.CB_ShowBotAuth_CheckedChanged);
			// 
			// B_GetLoginData
			// 
			this.B_GetLoginData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_GetLoginData.Location = new System.Drawing.Point(3, 3);
			this.B_GetLoginData.Name = "B_GetLoginData";
			this.B_GetLoginData.Size = new System.Drawing.Size(145, 32);
			this.B_GetLoginData.TabIndex = 11;
			this.B_GetLoginData.Text = "Obtain auth (webserver)";
			this.B_GetLoginData.UseVisualStyleBackColor = true;
			this.B_GetLoginData.Click += new System.EventHandler(this.B_GetLoginData_Click);
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 5;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
			this.tableLayoutPanel6.Controls.Add(this.label6, 2, 0);
			this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.Num_PortUsed, 3, 0);
			this.tableLayoutPanel6.Controls.Add(this.CB_Websocket, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.CB_DebugMode, 4, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(606, 30);
			this.tableLayoutPanel6.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(217, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95, 30);
			this.label6.TabIndex = 15;
			this.label6.Text = "Websocket port:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 30);
			this.label4.TabIndex = 23;
			this.label4.Text = "Websocket";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Num_PortUsed
			// 
			this.Num_PortUsed.Location = new System.Drawing.Point(318, 3);
			this.Num_PortUsed.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.Num_PortUsed.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
			this.Num_PortUsed.Name = "Num_PortUsed";
			this.Num_PortUsed.Size = new System.Drawing.Size(86, 20);
			this.Num_PortUsed.TabIndex = 17;
			this.Num_PortUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Num_PortUsed.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
			// 
			// CB_Websocket
			// 
			this.CB_Websocket.AutoSize = true;
			this.CB_Websocket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_Websocket.Location = new System.Drawing.Point(78, 3);
			this.CB_Websocket.Name = "CB_Websocket";
			this.CB_Websocket.Size = new System.Drawing.Size(133, 24);
			this.CB_Websocket.TabIndex = 13;
			this.CB_Websocket.Text = "Run websocket server";
			this.CB_Websocket.UseVisualStyleBackColor = true;
			// 
			// CB_DebugMode
			// 
			this.CB_DebugMode.AutoSize = true;
			this.CB_DebugMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_DebugMode.Location = new System.Drawing.Point(412, 3);
			this.CB_DebugMode.Name = "CB_DebugMode";
			this.CB_DebugMode.Size = new System.Drawing.Size(191, 24);
			this.CB_DebugMode.TabIndex = 12;
			this.CB_DebugMode.Text = "Debug mode";
			this.CB_DebugMode.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 4;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.79012F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.20988F));
			this.tableLayoutPanel8.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.TB_UserAuth, 1, 0);
			this.tableLayoutPanel8.Controls.Add(this.label8, 2, 0);
			this.tableLayoutPanel8.Controls.Add(this.TB_BotAuth, 3, 0);
			this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 39);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(606, 30);
			this.tableLayoutPanel8.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 30);
			this.label3.TabIndex = 3;
			this.label3.Text = "Owner auth:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_UserAuth
			// 
			this.TB_UserAuth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_UserAuth.Location = new System.Drawing.Point(99, 3);
			this.TB_UserAuth.Name = "TB_UserAuth";
			this.TB_UserAuth.Size = new System.Drawing.Size(235, 20);
			this.TB_UserAuth.TabIndex = 6;
			this.TB_UserAuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TB_UserAuth.UseSystemPasswordChar = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(340, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(78, 30);
			this.label8.TabIndex = 20;
			this.label8.Text = "Bot auth:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_BotAuth
			// 
			this.TB_BotAuth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_BotAuth.Location = new System.Drawing.Point(424, 3);
			this.TB_BotAuth.Name = "TB_BotAuth";
			this.TB_BotAuth.Size = new System.Drawing.Size(179, 20);
			this.TB_BotAuth.TabIndex = 21;
			this.TB_BotAuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TB_BotAuth.UseSystemPasswordChar = true;
			// 
			// ConnectionSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(621, 167);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionSettingsForm";
			this.Text = "Connection Settings";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_PortUsed)).EndInit();
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TB_UserAuth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CB_ShowPasswordUser;
        private System.Windows.Forms.Button B_GetLoginData;
		private System.Windows.Forms.CheckBox CB_DebugMode;
		private System.Windows.Forms.CheckBox CB_Websocket;
		private System.Windows.Forms.CheckBox CB_ShowBotAuth;
		private System.Windows.Forms.TextBox TB_BotAuth;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown Num_PortUsed;
	}
}