using System;
using System.ComponentModel;
using System.Linq;

namespace BasicTwitchSoundPlayer.SettingsForms
{
    partial class TTSSettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_CustomRewardID = new System.Windows.Forms.TextBox();
            this.B_OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CBox_RequiredRole = new System.Windows.Forms.ComboBox();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CBox_TTSLogic = new System.Windows.Forms.ComboBox();
            this.CBox_VoiceSynthesizer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_ExampleText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.B_Test = new System.Windows.Forms.Button();
            this.linkExplainLogic = new System.Windows.Forms.LinkLabel();
            this.TTSExplanation_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.B_UseLastRewardID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Custom Reward ID:";
            // 
            // TB_CustomRewardID
            // 
            this.TB_CustomRewardID.Location = new System.Drawing.Point(112, 61);
            this.TB_CustomRewardID.Name = "TB_CustomRewardID";
            this.TB_CustomRewardID.Size = new System.Drawing.Size(240, 20);
            this.TB_CustomRewardID.TabIndex = 1;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(311, 114);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(75, 23);
            this.B_OK.TabIndex = 2;
            this.B_OK.Text = "OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Required role:";
            // 
            // CBox_RequiredRole
            // 
            this.CBox_RequiredRole.FormattingEnabled = true;
            this.CBox_RequiredRole.Location = new System.Drawing.Point(86, 87);
            this.CBox_RequiredRole.Name = "CBox_RequiredRole";
            this.CBox_RequiredRole.Size = new System.Drawing.Size(162, 21);
            this.CBox_RequiredRole.TabIndex = 4;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(392, 114);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 5;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Logic:";
            // 
            // CBox_TTSLogic
            // 
            this.CBox_TTSLogic.FormattingEnabled = true;
            this.CBox_TTSLogic.Items.AddRange(new object[] {
            "Require role && reward ID",
            "Use role for !tts"});
            this.CBox_TTSLogic.Location = new System.Drawing.Point(296, 87);
            this.CBox_TTSLogic.Name = "CBox_TTSLogic";
            this.CBox_TTSLogic.Size = new System.Drawing.Size(152, 21);
            this.CBox_TTSLogic.TabIndex = 7;
            // 
            // CBox_VoiceSynthesizer
            // 
            this.CBox_VoiceSynthesizer.FormattingEnabled = true;
            this.CBox_VoiceSynthesizer.Location = new System.Drawing.Point(99, 5);
            this.CBox_VoiceSynthesizer.Name = "CBox_VoiceSynthesizer";
            this.CBox_VoiceSynthesizer.Size = new System.Drawing.Size(376, 21);
            this.CBox_VoiceSynthesizer.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Voice synthesizer:";
            // 
            // TB_ExampleText
            // 
            this.TB_ExampleText.Location = new System.Drawing.Point(64, 32);
            this.TB_ExampleText.Name = "TB_ExampleText";
            this.TB_ExampleText.Size = new System.Drawing.Size(322, 20);
            this.TB_ExampleText.TabIndex = 10;
            this.TB_ExampleText.Text = "This is an example text used by Text to Speech. You can replace it with something" +
    " else to test the system.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Test text:";
            // 
            // B_Test
            // 
            this.B_Test.Location = new System.Drawing.Point(392, 30);
            this.B_Test.Name = "B_Test";
            this.B_Test.Size = new System.Drawing.Size(75, 23);
            this.B_Test.TabIndex = 12;
            this.B_Test.Text = "Test";
            this.B_Test.UseVisualStyleBackColor = true;
            this.B_Test.Click += new System.EventHandler(this.B_Test_Click);
            // 
            // linkExplainLogic
            // 
            this.linkExplainLogic.AutoSize = true;
            this.linkExplainLogic.Location = new System.Drawing.Point(454, 90);
            this.linkExplainLogic.Name = "linkExplainLogic";
            this.linkExplainLogic.Size = new System.Drawing.Size(13, 13);
            this.linkExplainLogic.TabIndex = 13;
            this.linkExplainLogic.TabStop = true;
            this.linkExplainLogic.Text = "?";
            // 
            // TTSExplanation_Tooltip
            // 
            this.TTSExplanation_Tooltip.ToolTipTitle = "TTS Logic Explanation";
            // 
            // B_UseLastRewardID
            // 
            this.B_UseLastRewardID.Location = new System.Drawing.Point(358, 59);
            this.B_UseLastRewardID.Name = "B_UseLastRewardID";
            this.B_UseLastRewardID.Size = new System.Drawing.Size(109, 23);
            this.B_UseLastRewardID.TabIndex = 14;
            this.B_UseLastRewardID.Text = "Use last Reward ID";
            this.B_UseLastRewardID.UseVisualStyleBackColor = true;
            this.B_UseLastRewardID.Click += new System.EventHandler(this.B_UseLastRewardID_Click);
            // 
            // TTSSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 145);
            this.Controls.Add(this.B_UseLastRewardID);
            this.Controls.Add(this.linkExplainLogic);
            this.Controls.Add(this.B_Test);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_ExampleText);
            this.Controls.Add(this.CBox_VoiceSynthesizer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CBox_TTSLogic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.CBox_RequiredRole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.TB_CustomRewardID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TTSSettingsForm";
            this.Text = "Text to Speech Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AddComboboxDataSources()
        {
            CBox_RequiredRole.DisplayMember = "Description";
            CBox_RequiredRole.ValueMember = "value";
            CBox_RequiredRole.DataSource = Enum.GetValues(typeof(BasicTwitchSoundPlayer.Structs.TwitchRightsEnum)).Cast<Enum>().Select(value =>
            new
            {
                (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                value
            }).ToList();

            CBox_TTSLogic.DisplayMember = "Description";
            CBox_TTSLogic.ValueMember = "value";
            CBox_TTSLogic.DataSource = Enum.GetValues(typeof(BasicTwitchSoundPlayer.Structs.TTSLogic)).Cast<Enum>().Select(value =>
            new
            {
                (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                value
            }).ToList();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_CustomRewardID;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBox_RequiredRole;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CBox_TTSLogic;
        private System.Windows.Forms.ComboBox CBox_VoiceSynthesizer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_ExampleText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button B_Test;
        private System.Windows.Forms.LinkLabel linkExplainLogic;
        private System.Windows.Forms.ToolTip TTSExplanation_Tooltip;
        private System.Windows.Forms.Button B_UseLastRewardID;
    }
}