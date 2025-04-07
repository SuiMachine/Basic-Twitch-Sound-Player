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
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.TB_UserAuth = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.TB_BotAuth = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Port = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.Num_PortUsed = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.TB_Server = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.CB_Websocket = new System.Windows.Forms.CheckBox();
			this.CB_DebugMode = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.TB_Username = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.TB_BotName = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
			this.label9 = new System.Windows.Forms.Label();
			this.TB_PastebinAPIKey = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_PortUsed)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tableLayoutPanel10.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 227F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 232F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 314);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.B_Save, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 272);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(566, 37);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// B_Save
			// 
			this.B_Save.Dock = System.Windows.Forms.DockStyle.Right;
			this.B_Save.Location = new System.Drawing.Point(205, 3);
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
			this.B_Cancel.Location = new System.Drawing.Point(286, 3);
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
			this.panel1.Size = new System.Drawing.Size(566, 221);
			this.panel1.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 0, 3);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 6;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(566, 221);
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
			this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 183);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 1;
			this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel9.Size = new System.Drawing.Size(560, 35);
			this.tableLayoutPanel9.TabIndex = 5;
			// 
			// CB_ShowPasswordUser
			// 
			this.CB_ShowPasswordUser.AutoSize = true;
			this.CB_ShowPasswordUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_ShowPasswordUser.Location = new System.Drawing.Point(143, 3);
			this.CB_ShowPasswordUser.Name = "CB_ShowPasswordUser";
			this.CB_ShowPasswordUser.Size = new System.Drawing.Size(134, 29);
			this.CB_ShowPasswordUser.TabIndex = 10;
			this.CB_ShowPasswordUser.Text = "Show user auth";
			this.CB_ShowPasswordUser.UseVisualStyleBackColor = true;
			this.CB_ShowPasswordUser.CheckedChanged += new System.EventHandler(this.CB_ShowPassword_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point(283, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(134, 29);
			this.button1.TabIndex = 24;
			this.button1.Text = "Obtain auth (manual)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.B_GetLoginDataManual_Click);
			// 
			// CB_ShowBotAuth
			// 
			this.CB_ShowBotAuth.AutoSize = true;
			this.CB_ShowBotAuth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_ShowBotAuth.Location = new System.Drawing.Point(423, 3);
			this.CB_ShowBotAuth.Name = "CB_ShowBotAuth";
			this.CB_ShowBotAuth.Size = new System.Drawing.Size(134, 29);
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
			this.B_GetLoginData.Size = new System.Drawing.Size(134, 29);
			this.B_GetLoginData.TabIndex = 11;
			this.B_GetLoginData.Text = "Obtain auth (webserver)";
			this.B_GetLoginData.UseVisualStyleBackColor = true;
			this.B_GetLoginData.Click += new System.EventHandler(this.B_GetLoginData_Click);
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
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 147);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(560, 30);
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
			this.TB_UserAuth.Enabled = false;
			this.TB_UserAuth.Location = new System.Drawing.Point(99, 3);
			this.TB_UserAuth.Name = "TB_UserAuth";
			this.TB_UserAuth.Size = new System.Drawing.Size(209, 20);
			this.TB_UserAuth.TabIndex = 6;
			this.TB_UserAuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TB_UserAuth.UseSystemPasswordChar = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(314, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(78, 30);
			this.label8.TabIndex = 20;
			this.label8.Text = "Bot auth:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_BotAuth
			// 
			this.TB_BotAuth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_BotAuth.Enabled = false;
			this.TB_BotAuth.Location = new System.Drawing.Point(398, 3);
			this.TB_BotAuth.Name = "TB_BotAuth";
			this.TB_BotAuth.Size = new System.Drawing.Size(159, 20);
			this.TB_BotAuth.TabIndex = 21;
			this.TB_BotAuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TB_BotAuth.UseSystemPasswordChar = true;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 4;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 357F));
			this.tableLayoutPanel5.Controls.Add(this.TB_Port, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.label6, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.Num_PortUsed, 3, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 39);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(560, 30);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// TB_Port
			// 
			this.TB_Port.Enabled = false;
			this.TB_Port.Location = new System.Drawing.Point(56, 3);
			this.TB_Port.Name = "TB_Port";
			this.TB_Port.Size = new System.Drawing.Size(50, 20);
			this.TB_Port.TabIndex = 5;
			this.TB_Port.Text = "6667";
			this.TB_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Left;
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 30);
			this.label5.TabIndex = 9;
			this.label5.Text = "Port:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Left;
			this.label6.Location = new System.Drawing.Point(112, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 30);
			this.label6.TabIndex = 15;
			this.label6.Text = "Websocket port:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Num_PortUsed
			// 
			this.Num_PortUsed.Location = new System.Drawing.Point(206, 3);
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
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.TB_Server, 1, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(560, 30);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_Server
			// 
			this.TB_Server.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Server.Location = new System.Drawing.Point(56, 3);
			this.TB_Server.Name = "TB_Server";
			this.TB_Server.Size = new System.Drawing.Size(501, 20);
			this.TB_Server.TabIndex = 1;
			this.TB_Server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 3;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 346F));
			this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.CB_Websocket, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.CB_DebugMode, 2, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 75);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(560, 30);
			this.tableLayoutPanel6.TabIndex = 2;
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
			this.CB_DebugMode.Location = new System.Drawing.Point(217, 3);
			this.CB_DebugMode.Name = "CB_DebugMode";
			this.CB_DebugMode.Size = new System.Drawing.Size(340, 24);
			this.CB_DebugMode.TabIndex = 12;
			this.CB_DebugMode.Text = "Debug mode";
			this.CB_DebugMode.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 4;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.65635F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.34365F));
			this.tableLayoutPanel7.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.TB_Username, 1, 0);
			this.tableLayoutPanel7.Controls.Add(this.label7, 2, 0);
			this.tableLayoutPanel7.Controls.Add(this.TB_BotName, 3, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 111);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(560, 30);
			this.tableLayoutPanel7.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 30);
			this.label2.TabIndex = 2;
			this.label2.Text = "Owner username:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_Username
			// 
			this.TB_Username.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Username.Location = new System.Drawing.Point(100, 3);
			this.TB_Username.Name = "TB_Username";
			this.TB_Username.Size = new System.Drawing.Size(207, 20);
			this.TB_Username.TabIndex = 8;
			this.TB_Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(313, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 30);
			this.label7.TabIndex = 18;
			this.label7.Text = "Bot username:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_BotName
			// 
			this.TB_BotName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_BotName.Location = new System.Drawing.Point(399, 3);
			this.TB_BotName.Name = "TB_BotName";
			this.TB_BotName.Size = new System.Drawing.Size(158, 20);
			this.TB_BotName.TabIndex = 19;
			this.TB_BotName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tableLayoutPanel10
			// 
			this.tableLayoutPanel10.ColumnCount = 2;
			this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 416F));
			this.tableLayoutPanel10.Controls.Add(this.label9, 0, 0);
			this.tableLayoutPanel10.Controls.Add(this.TB_PastebinAPIKey, 1, 0);
			this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 230);
			this.tableLayoutPanel10.Name = "tableLayoutPanel10";
			this.tableLayoutPanel10.RowCount = 1;
			this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel10.Size = new System.Drawing.Size(566, 36);
			this.tableLayoutPanel10.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label9.Location = new System.Drawing.Point(3, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(144, 36);
			this.label9.TabIndex = 0;
			this.label9.Text = "Pastebin API Key (optional):";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_PastebinAPIKey
			// 
			this.TB_PastebinAPIKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.TB_PastebinAPIKey.Location = new System.Drawing.Point(153, 8);
			this.TB_PastebinAPIKey.Name = "TB_PastebinAPIKey";
			this.TB_PastebinAPIKey.PasswordChar = '*';
			this.TB_PastebinAPIKey.Size = new System.Drawing.Size(410, 20);
			this.TB_PastebinAPIKey.TabIndex = 1;
			// 
			// ConnectionSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 314);
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
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_PortUsed)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.tableLayoutPanel10.ResumeLayout(false);
			this.tableLayoutPanel10.PerformLayout();
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
        private System.Windows.Forms.TextBox TB_UserAuth;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_Username;
        private System.Windows.Forms.CheckBox CB_ShowPasswordUser;
        private System.Windows.Forms.Button B_GetLoginData;
		private System.Windows.Forms.CheckBox CB_DebugMode;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox CB_Websocket;
		private System.Windows.Forms.NumericUpDown Num_PortUsed;
		private System.Windows.Forms.CheckBox CB_ShowBotAuth;
		private System.Windows.Forms.TextBox TB_BotAuth;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox TB_BotName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox TB_PastebinAPIKey;
	}
}