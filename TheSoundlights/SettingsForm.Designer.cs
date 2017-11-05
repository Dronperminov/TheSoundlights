namespace TheSoundlights {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.topMostCheckBox = new System.Windows.Forms.CheckBox();
            this.spectrumCheckBox = new System.Windows.Forms.CheckBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.backColorBox = new System.Windows.Forms.TextBox();
            this.foreColorBox = new System.Windows.Forms.TextBox();
            this.colColorBox = new System.Windows.Forms.TextBox();
            this.backColorLabel = new System.Windows.Forms.Label();
            this.foreColorLabel = new System.Windows.Forms.Label();
            this.colColorLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusBackLabel = new System.Windows.Forms.Label();
            this.statusForeColorBox = new System.Windows.Forms.TextBox();
            this.statusBackColorBox = new System.Windows.Forms.TextBox();
            this.changeThemeBtn = new System.Windows.Forms.Button();
            this.channelsLabel = new System.Windows.Forms.Label();
            this.channelsCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // topMostCheckBox
            // 
            this.topMostCheckBox.AutoSize = true;
            this.topMostCheckBox.Location = new System.Drawing.Point(12, 37);
            this.topMostCheckBox.Name = "topMostCheckBox";
            this.topMostCheckBox.Size = new System.Drawing.Size(116, 17);
            this.topMostCheckBox.TabIndex = 0;
            this.topMostCheckBox.Text = "Поверх всех окон";
            this.topMostCheckBox.UseVisualStyleBackColor = true;
            // 
            // spectrumCheckBox
            // 
            this.spectrumCheckBox.AutoSize = true;
            this.spectrumCheckBox.Location = new System.Drawing.Point(12, 60);
            this.spectrumCheckBox.Name = "spectrumCheckBox";
            this.spectrumCheckBox.Size = new System.Drawing.Size(127, 17);
            this.spectrumCheckBox.TabIndex = 1;
            this.spectrumCheckBox.Text = "Показывать спектр";
            this.spectrumCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(157, 153);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(76, 23);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // backColorBox
            // 
            this.backColorBox.Location = new System.Drawing.Point(312, 10);
            this.backColorBox.MaxLength = 7;
            this.backColorBox.Name = "backColorBox";
            this.backColorBox.Size = new System.Drawing.Size(50, 20);
            this.backColorBox.TabIndex = 3;
            // 
            // foreColorBox
            // 
            this.foreColorBox.Location = new System.Drawing.Point(312, 36);
            this.foreColorBox.MaxLength = 7;
            this.foreColorBox.Name = "foreColorBox";
            this.foreColorBox.Size = new System.Drawing.Size(50, 20);
            this.foreColorBox.TabIndex = 4;
            // 
            // colColorBox
            // 
            this.colColorBox.Location = new System.Drawing.Point(312, 62);
            this.colColorBox.MaxLength = 7;
            this.colColorBox.Name = "colColorBox";
            this.colColorBox.Size = new System.Drawing.Size(50, 20);
            this.colColorBox.TabIndex = 5;
            // 
            // backColorLabel
            // 
            this.backColorLabel.AutoSize = true;
            this.backColorLabel.Location = new System.Drawing.Point(252, 12);
            this.backColorLabel.Name = "backColorLabel";
            this.backColorLabel.Size = new System.Drawing.Size(61, 13);
            this.backColorLabel.TabIndex = 6;
            this.backColorLabel.Text = "Цвет фона";
            // 
            // foreColorLabel
            // 
            this.foreColorLabel.AutoSize = true;
            this.foreColorLabel.Location = new System.Drawing.Point(244, 38);
            this.foreColorLabel.Name = "foreColorLabel";
            this.foreColorLabel.Size = new System.Drawing.Size(69, 13);
            this.foreColorLabel.TabIndex = 7;
            this.foreColorLabel.Text = "Цвет текста";
            // 
            // colColorLabel
            // 
            this.colColorLabel.AutoSize = true;
            this.colColorLabel.Location = new System.Drawing.Point(243, 64);
            this.colColorLabel.Name = "colColorLabel";
            this.colColorLabel.Size = new System.Drawing.Size(70, 13);
            this.colColorLabel.TabIndex = 8;
            this.colColorLabel.Text = "Цвет границ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Цвет текста стат.строки";
            // 
            // statusBackLabel
            // 
            this.statusBackLabel.AutoSize = true;
            this.statusBackLabel.Location = new System.Drawing.Point(189, 90);
            this.statusBackLabel.Name = "statusBackLabel";
            this.statusBackLabel.Size = new System.Drawing.Size(124, 13);
            this.statusBackLabel.TabIndex = 11;
            this.statusBackLabel.Text = "Цвет фона стат.строки";
            // 
            // statusForeColorBox
            // 
            this.statusForeColorBox.Location = new System.Drawing.Point(312, 113);
            this.statusForeColorBox.MaxLength = 7;
            this.statusForeColorBox.Name = "statusForeColorBox";
            this.statusForeColorBox.Size = new System.Drawing.Size(50, 20);
            this.statusForeColorBox.TabIndex = 10;
            // 
            // statusBackColorBox
            // 
            this.statusBackColorBox.Location = new System.Drawing.Point(312, 88);
            this.statusBackColorBox.MaxLength = 7;
            this.statusBackColorBox.Name = "statusBackColorBox";
            this.statusBackColorBox.Size = new System.Drawing.Size(50, 20);
            this.statusBackColorBox.TabIndex = 9;
            // 
            // changeThemeBtn
            // 
            this.changeThemeBtn.Location = new System.Drawing.Point(12, 110);
            this.changeThemeBtn.Name = "changeThemeBtn";
            this.changeThemeBtn.Size = new System.Drawing.Size(90, 23);
            this.changeThemeBtn.TabIndex = 13;
            this.changeThemeBtn.Text = "Выбрать тему";
            this.changeThemeBtn.UseVisualStyleBackColor = true;
            this.changeThemeBtn.Click += new System.EventHandler(this.changeThemeBtn_Click);
            // 
            // channelsLabel
            // 
            this.channelsLabel.AutoSize = true;
            this.channelsLabel.Location = new System.Drawing.Point(12, 13);
            this.channelsLabel.Name = "channelsLabel";
            this.channelsLabel.Size = new System.Drawing.Size(84, 13);
            this.channelsLabel.TabIndex = 14;
            this.channelsLabel.Text = "Число каналов";
            // 
            // channelsCombo
            // 
            this.channelsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelsCombo.FormattingEnabled = true;
            this.channelsCombo.Items.AddRange(new object[] {
            "3",
            "4"});
            this.channelsCombo.Location = new System.Drawing.Point(96, 10);
            this.channelsCombo.Name = "channelsCombo";
            this.channelsCombo.Size = new System.Drawing.Size(32, 21);
            this.channelsCombo.TabIndex = 15;
            this.channelsCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 188);
            this.Controls.Add(this.channelsCombo);
            this.Controls.Add(this.channelsLabel);
            this.Controls.Add(this.changeThemeBtn);
            this.Controls.Add(this.statusForeColorBox);
            this.Controls.Add(this.statusBackColorBox);
            this.Controls.Add(this.colColorBox);
            this.Controls.Add(this.backColorBox);
            this.Controls.Add(this.foreColorBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusBackLabel);
            this.Controls.Add(this.colColorLabel);
            this.Controls.Add(this.foreColorLabel);
            this.Controls.Add(this.backColorLabel);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.spectrumCheckBox);
            this.Controls.Add(this.topMostCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 227);
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox topMostCheckBox;
        private System.Windows.Forms.CheckBox spectrumCheckBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox backColorBox;
        private System.Windows.Forms.TextBox foreColorBox;
        private System.Windows.Forms.TextBox colColorBox;
        private System.Windows.Forms.Label backColorLabel;
        private System.Windows.Forms.Label foreColorLabel;
        private System.Windows.Forms.Label colColorLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusBackLabel;
        private System.Windows.Forms.TextBox statusForeColorBox;
        private System.Windows.Forms.TextBox statusBackColorBox;
        private System.Windows.Forms.Button changeThemeBtn;
        private System.Windows.Forms.Label channelsLabel;
        private System.Windows.Forms.ComboBox channelsCombo;
    }
}