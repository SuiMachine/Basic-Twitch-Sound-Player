namespace BasicTwitchSoundPlayer.SettingsForms.EditForm
{
	partial class VoiceModAddForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceModAddForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.B_Add = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.RB_Description = new System.Windows.Forms.RichTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.CB_Enabled = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.NumBox_Cooldown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.NumBox_Duration = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.NumBox_Price = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.B_Generate = new System.Windows.Forms.Button();
			this.TB_Reward_Name = new System.Windows.Forms.TextBox();
			this.ComboBox_Voice = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Cooldown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Duration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Price)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(513, 247);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Add, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 214);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(507, 30);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// B_Cancel
			// 
			this.B_Cancel.Location = new System.Drawing.Point(426, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(75, 23);
			this.B_Cancel.TabIndex = 0;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// B_Add
			// 
			this.B_Add.Location = new System.Drawing.Point(342, 3);
			this.B_Add.Name = "B_Add";
			this.B_Add.Size = new System.Drawing.Size(75, 23);
			this.B_Add.TabIndex = 1;
			this.B_Add.Text = "Add";
			this.B_Add.UseVisualStyleBackColor = true;
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.RB_Description);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.CB_Enabled);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.NumBox_Cooldown);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.NumBox_Duration);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.NumBox_Price);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.B_Generate);
			this.panel1.Controls.Add(this.TB_Reward_Name);
			this.panel1.Controls.Add(this.ComboBox_Voice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(507, 205);
			this.panel1.TabIndex = 1;
			// 
			// RB_Description
			// 
			this.RB_Description.Location = new System.Drawing.Point(0, 100);
			this.RB_Description.Name = "RB_Description";
			this.RB_Description.Size = new System.Drawing.Size(501, 96);
			this.RB_Description.TabIndex = 21;
			this.RB_Description.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 84);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 20;
			this.label7.Text = "Description";
			// 
			// CB_Enabled
			// 
			this.CB_Enabled.AutoSize = true;
			this.CB_Enabled.Location = new System.Drawing.Point(482, 63);
			this.CB_Enabled.Name = "CB_Enabled";
			this.CB_Enabled.Size = new System.Drawing.Size(15, 14);
			this.CB_Enabled.TabIndex = 19;
			this.CB_Enabled.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(427, 63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Enabled:";
			// 
			// NumBox_Cooldown
			// 
			this.NumBox_Cooldown.Location = new System.Drawing.Point(374, 61);
			this.NumBox_Cooldown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.NumBox_Cooldown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumBox_Cooldown.Name = "NumBox_Cooldown";
			this.NumBox_Cooldown.Size = new System.Drawing.Size(47, 20);
			this.NumBox_Cooldown.TabIndex = 17;
			this.NumBox_Cooldown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(266, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(102, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Cooldown (minutes):";
			// 
			// NumBox_Duration
			// 
			this.NumBox_Duration.Location = new System.Drawing.Point(213, 61);
			this.NumBox_Duration.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.NumBox_Duration.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.NumBox_Duration.Name = "NumBox_Duration";
			this.NumBox_Duration.Size = new System.Drawing.Size(47, 20);
			this.NumBox_Duration.TabIndex = 15;
			this.NumBox_Duration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(108, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Duration (seconds):";
			// 
			// NumBox_Price
			// 
			this.NumBox_Price.Location = new System.Drawing.Point(49, 61);
			this.NumBox_Price.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.NumBox_Price.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumBox_Price.Name = "NumBox_Price";
			this.NumBox_Price.Size = new System.Drawing.Size(53, 20);
			this.NumBox_Price.TabIndex = 13;
			this.NumBox_Price.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Price:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Reward name:";
			// 
			// B_Generate
			// 
			this.B_Generate.Location = new System.Drawing.Point(370, 7);
			this.B_Generate.Name = "B_Generate";
			this.B_Generate.Size = new System.Drawing.Size(127, 23);
			this.B_Generate.TabIndex = 3;
			this.B_Generate.Text = "Generate description";
			this.B_Generate.UseVisualStyleBackColor = true;
			this.B_Generate.Click += new System.EventHandler(this.B_Generate_Click);
			// 
			// TB_Reward_Name
			// 
			this.TB_Reward_Name.Location = new System.Drawing.Point(91, 36);
			this.TB_Reward_Name.Name = "TB_Reward_Name";
			this.TB_Reward_Name.Size = new System.Drawing.Size(406, 20);
			this.TB_Reward_Name.TabIndex = 2;
			// 
			// ComboBox_Voice
			// 
			this.ComboBox_Voice.FormattingEnabled = true;
			this.ComboBox_Voice.Location = new System.Drawing.Point(102, 9);
			this.ComboBox_Voice.Name = "ComboBox_Voice";
			this.ComboBox_Voice.Size = new System.Drawing.Size(262, 21);
			this.ComboBox_Voice.TabIndex = 1;
			this.ComboBox_Voice.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Voice_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select the voice:";
			// 
			// VoiceModAddForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 247);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VoiceModAddForm";
			this.Text = "Add voice reward";
			this.Load += new System.EventHandler(this.VoiceModAddForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Cooldown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Duration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumBox_Price)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.Button B_Add;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox ComboBox_Voice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button B_Generate;
		private System.Windows.Forms.TextBox TB_Reward_Name;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox CB_Enabled;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown NumBox_Cooldown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown NumBox_Duration;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown NumBox_Price;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RichTextBox RB_Description;
	}
}