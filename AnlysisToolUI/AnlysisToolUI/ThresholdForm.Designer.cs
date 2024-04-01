namespace AnlysisToolUI
{
    partial class ThresholdForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.minGVScroll = new System.Windows.Forms.HScrollBar();
            this.maxGVScroll = new System.Windows.Forms.HScrollBar();
            this.btn_auto = new System.Windows.Forms.Button();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.txt_minGVscroll = new System.Windows.Forms.TextBox();
            this.txt_maxGVscroll = new System.Windows.Forms.TextBox();
            this.btn_label = new System.Windows.Forms.Button();
            this.btn_blobset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea5.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series5.BorderColor = System.Drawing.Color.Blue;
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(344, 64);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Paint += new System.Windows.Forms.PaintEventHandler(this.chart1_Paint);
            // 
            // minGVScroll
            // 
            this.minGVScroll.LargeChange = 1;
            this.minGVScroll.Location = new System.Drawing.Point(12, 105);
            this.minGVScroll.Maximum = 255;
            this.minGVScroll.Name = "minGVScroll";
            this.minGVScroll.Size = new System.Drawing.Size(285, 30);
            this.minGVScroll.TabIndex = 1;
            this.minGVScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.minGVScroll_Scroll);
            // 
            // maxGVScroll
            // 
            this.maxGVScroll.LargeChange = 1;
            this.maxGVScroll.Location = new System.Drawing.Point(12, 143);
            this.maxGVScroll.Maximum = 255;
            this.maxGVScroll.Name = "maxGVScroll";
            this.maxGVScroll.Size = new System.Drawing.Size(285, 30);
            this.maxGVScroll.TabIndex = 1;
            this.maxGVScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.maxGVScroll_Scroll);
            // 
            // btn_auto
            // 
            this.btn_auto.Location = new System.Drawing.Point(26, 215);
            this.btn_auto.Name = "btn_auto";
            this.btn_auto.Size = new System.Drawing.Size(75, 23);
            this.btn_auto.TabIndex = 2;
            this.btn_auto.Text = "Auto";
            this.btn_auto.UseVisualStyleBackColor = true;
            this.btn_auto.Click += new System.EventHandler(this.btn_auto_Click);
            // 
            // btn_apply
            // 
            this.btn_apply.Location = new System.Drawing.Point(107, 215);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(75, 23);
            this.btn_apply.TabIndex = 2;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(188, 215);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 2;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Defalut",
            "Mean",
            "Otsu"});
            this.comboBox1.Location = new System.Drawing.Point(31, 189);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Red",
            "B&W",
            "Over/Under"});
            this.comboBox2.Location = new System.Drawing.Point(152, 189);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(104, 20);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txt_minGVscroll
            // 
            this.txt_minGVscroll.Font = new System.Drawing.Font("굴림", 10F);
            this.txt_minGVscroll.Location = new System.Drawing.Point(301, 109);
            this.txt_minGVscroll.Name = "txt_minGVscroll";
            this.txt_minGVscroll.Size = new System.Drawing.Size(56, 23);
            this.txt_minGVscroll.TabIndex = 4;
            this.txt_minGVscroll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_minGVscroll_KeyPress);
            // 
            // txt_maxGVscroll
            // 
            this.txt_maxGVscroll.Font = new System.Drawing.Font("굴림", 10F);
            this.txt_maxGVscroll.Location = new System.Drawing.Point(301, 148);
            this.txt_maxGVscroll.Name = "txt_maxGVscroll";
            this.txt_maxGVscroll.Size = new System.Drawing.Size(56, 23);
            this.txt_maxGVscroll.TabIndex = 4;
            this.txt_maxGVscroll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_maxGVscroll_KeyPress);
            // 
            // btn_label
            // 
            this.btn_label.Location = new System.Drawing.Point(277, 189);
            this.btn_label.Name = "btn_label";
            this.btn_label.Size = new System.Drawing.Size(80, 20);
            this.btn_label.TabIndex = 2;
            this.btn_label.Text = "Label";
            this.btn_label.UseVisualStyleBackColor = true;
            this.btn_label.Click += new System.EventHandler(this.btn_label_Click);
            // 
            // btn_blobset
            // 
            this.btn_blobset.Location = new System.Drawing.Point(277, 215);
            this.btn_blobset.Name = "btn_blobset";
            this.btn_blobset.Size = new System.Drawing.Size(80, 20);
            this.btn_blobset.TabIndex = 2;
            this.btn_blobset.Text = "Blob Set";
            this.btn_blobset.UseVisualStyleBackColor = true;
            this.btn_blobset.Click += new System.EventHandler(this.btn_blobset_Click);
            // 
            // ThresholdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 250);
            this.Controls.Add(this.txt_maxGVscroll);
            this.Controls.Add(this.txt_minGVscroll);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_blobset);
            this.Controls.Add(this.btn_label);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.btn_auto);
            this.Controls.Add(this.maxGVScroll);
            this.Controls.Add(this.minGVScroll);
            this.Controls.Add(this.chart1);
            this.Name = "ThresholdForm";
            this.Text = "Threshold";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.HScrollBar minGVScroll;
        private System.Windows.Forms.HScrollBar maxGVScroll;
        private System.Windows.Forms.Button btn_auto;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox txt_minGVscroll;
        private System.Windows.Forms.TextBox txt_maxGVscroll;
        private System.Windows.Forms.Button btn_label;
        private System.Windows.Forms.Button btn_blobset;
    }
}