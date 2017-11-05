namespace TheSoundlights {
    partial class MainForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.scanBtn = new TheSoundlights.MainForm.MyButton();
            this.connectBtn = new TheSoundlights.MainForm.MyButton();
            this.deviceCombo = new TheSoundlights.MainForm.MyComboBox();
            this.barsCheckBox = new TheSoundlights.MainForm.MyCheckBox();
            this.fadeCheckBox = new TheSoundlights.MainForm.MyCheckBox();
            this.lightCheckBox = new TheSoundlights.MainForm.MyCheckBox();
            this.bar1 = new TheSoundlights.MainForm.MyTrackBar();
            this.bar2 = new TheSoundlights.MainForm.MyTrackBar();
            this.bar4 = new TheSoundlights.MainForm.MyTrackBar();
            this.bar3 = new TheSoundlights.MainForm.MyTrackBar();
            this.status = new System.Windows.Forms.StatusStrip();
            this.sLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.spectrumChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spectrumChart)).BeginInit();
            this.SuspendLayout();
            // 
            // scanBtn
            // 
            this.scanBtn.Location = new System.Drawing.Point(10, 9);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(94, 23);
            this.scanBtn.TabIndex = 0;
            this.scanBtn.TabStop = false;
            this.scanBtn.Text = "Сканировать";
            this.scanBtn.UseVisualStyleBackColor = true;
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // connectBtn
            // 
            this.connectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectBtn.Enabled = false;
            this.connectBtn.Location = new System.Drawing.Point(274, 9);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(94, 23);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.TabStop = false;
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // deviceCombo
            // 
            this.deviceCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceCombo.Enabled = false;
            this.deviceCombo.FormattingEnabled = true;
            this.deviceCombo.Location = new System.Drawing.Point(110, 10);
            this.deviceCombo.Name = "deviceCombo";
            this.deviceCombo.Size = new System.Drawing.Size(159, 21);
            this.deviceCombo.TabIndex = 1;
            this.deviceCombo.TabStop = false;
            this.deviceCombo.SelectedIndexChanged += new System.EventHandler(this.deviceCombo_SelectedIndexChanged);
            // 
            // barsCheckBox
            // 
            this.barsCheckBox.AutoSize = true;
            this.barsCheckBox.Location = new System.Drawing.Point(10, 115);
            this.barsCheckBox.Name = "barsCheckBox";
            this.barsCheckBox.Size = new System.Drawing.Size(92, 17);
            this.barsCheckBox.TabIndex = 8;
            this.barsCheckBox.TabStop = false;
            this.barsCheckBox.Text = "Common level";
            this.barsCheckBox.UseVisualStyleBackColor = true;
            this.barsCheckBox.Visible = false;
            // 
            // fadeCheckBox
            // 
            this.fadeCheckBox.AutoSize = true;
            this.fadeCheckBox.Checked = true;
            this.fadeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fadeCheckBox.Location = new System.Drawing.Point(10, 132);
            this.fadeCheckBox.Name = "fadeCheckBox";
            this.fadeCheckBox.Size = new System.Drawing.Size(58, 17);
            this.fadeCheckBox.TabIndex = 9;
            this.fadeCheckBox.TabStop = false;
            this.fadeCheckBox.Text = "Fading";
            this.fadeCheckBox.UseVisualStyleBackColor = true;
            this.fadeCheckBox.Visible = false;
            // 
            // lightCheckBox
            // 
            this.lightCheckBox.AutoSize = true;
            this.lightCheckBox.Location = new System.Drawing.Point(10, 149);
            this.lightCheckBox.Name = "lightCheckBox";
            this.lightCheckBox.Size = new System.Drawing.Size(79, 17);
            this.lightCheckBox.TabIndex = 10;
            this.lightCheckBox.TabStop = false;
            this.lightCheckBox.Text = "Simple light";
            this.lightCheckBox.UseVisualStyleBackColor = true;
            this.lightCheckBox.Visible = false;
            this.lightCheckBox.CheckedChanged += new System.EventHandler(this.lightCheckBox_CheckedChanged);
            // 
            // bar1
            // 
            this.bar1.AutoSize = false;
            this.bar1.Location = new System.Drawing.Point(7, 32);
            this.bar1.Maximum = 5;
            this.bar1.Minimum = -5;
            this.bar1.Name = "bar1";
            this.bar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.bar1.Size = new System.Drawing.Size(23, 87);
            this.bar1.TabIndex = 4;
            this.bar1.TabStop = false;
            this.bar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // bar2
            // 
            this.bar2.AutoSize = false;
            this.bar2.Location = new System.Drawing.Point(32, 32);
            this.bar2.Maximum = 5;
            this.bar2.Minimum = -5;
            this.bar2.Name = "bar2";
            this.bar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.bar2.Size = new System.Drawing.Size(23, 87);
            this.bar2.TabIndex = 5;
            this.bar2.TabStop = false;
            this.bar2.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // bar4
            // 
            this.bar4.AutoSize = false;
            this.bar4.Location = new System.Drawing.Point(82, 32);
            this.bar4.Maximum = 5;
            this.bar4.Minimum = -5;
            this.bar4.Name = "bar4";
            this.bar4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.bar4.Size = new System.Drawing.Size(23, 87);
            this.bar4.TabIndex = 7;
            this.bar4.TabStop = false;
            this.bar4.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // bar3
            // 
            this.bar3.AutoSize = false;
            this.bar3.Location = new System.Drawing.Point(57, 32);
            this.bar3.Maximum = 5;
            this.bar3.Minimum = -5;
            this.bar3.Name = "bar3";
            this.bar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.bar3.Size = new System.Drawing.Size(23, 87);
            this.bar3.TabIndex = 6;
            this.bar3.TabStop = false;
            this.bar3.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sLabel,
            this.sLabel2,
            this.sLabel3});
            this.status.Location = new System.Drawing.Point(0, 166);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(374, 22);
            this.status.SizingGrip = false;
            this.status.TabIndex = 7;
            // 
            // sLabel
            // 
            this.sLabel.Name = "sLabel";
            this.sLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // sLabel2
            // 
            this.sLabel2.Name = "sLabel2";
            this.sLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // sLabel3
            // 
            this.sLabel3.Name = "sLabel3";
            this.sLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // spectrumChart
            // 
            this.spectrumChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spectrumChart.BackColor = System.Drawing.Color.Transparent;
            this.spectrumChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.spectrumChart.ChartAreas.Add(chartArea1);
            this.spectrumChart.Location = new System.Drawing.Point(111, 38);
            this.spectrumChart.Name = "spectrumChart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "FFT";
            series1.YValuesPerPoint = 4;
            this.spectrumChart.Series.Add(series1);
            this.spectrumChart.Size = new System.Drawing.Size(257, 125);
            this.spectrumChart.TabIndex = 11;
            this.spectrumChart.TabStop = false;
            this.spectrumChart.Text = "chart2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 188);
            this.Controls.Add(this.barsCheckBox);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.bar4);
            this.Controls.Add(this.bar3);
            this.Controls.Add(this.bar2);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.spectrumChart);
            this.Controls.Add(this.lightCheckBox);
            this.Controls.Add(this.fadeCheckBox);
            this.Controls.Add(this.status);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.deviceCombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(390, 227);
            this.Name = "MainForm";
            this.Text = "The Soundlights";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spectrumChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.StatusStrip status;
        public System.Windows.Forms.ToolStripStatusLabel sLabel;
        public System.Windows.Forms.DataVisualization.Charting.Chart spectrumChart;
        public System.Windows.Forms.ToolStripStatusLabel sLabel2;
        public System.Windows.Forms.ToolStripStatusLabel sLabel3;
        public MyComboBox deviceCombo;
        public MyButton scanBtn;
        public MyButton connectBtn;
        public MyCheckBox fadeCheckBox;
        public MyTrackBar bar1;
        public MyTrackBar bar2;
        public MyTrackBar bar4;
        public MyTrackBar bar3;
        public MyCheckBox barsCheckBox;
        public MyCheckBox lightCheckBox;
    }
}

