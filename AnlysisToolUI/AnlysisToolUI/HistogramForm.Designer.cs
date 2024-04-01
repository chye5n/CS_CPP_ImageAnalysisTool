namespace AnlysisToolUI
{
    partial class HistogramForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lb_count = new System.Windows.Forms.Label();
            this.lb_mean = new System.Windows.Forms.Label();
            this.lb_stddev = new System.Windows.Forms.Label();
            this.lb_min = new System.Windows.Forms.Label();
            this.lb_max = new System.Windows.Forms.Label();
            this.lb_mode = new System.Windows.Forms.Label();
            this.btn_rgb = new System.Windows.Forms.Button();
            this.btn_live = new System.Windows.Forms.Button();
            this.btn_list = new System.Windows.Forms.Button();
            this.lb_value = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.Blue;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(491, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.PrePaint += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs>(this.chart1_PrePaint);
            // 
            // lb_count
            // 
            this.lb_count.AutoSize = true;
            this.lb_count.Location = new System.Drawing.Point(12, 321);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(52, 12);
            this.lb_count.TabIndex = 1;
            this.lb_count.Text = "lb_count";
            // 
            // lb_mean
            // 
            this.lb_mean.AutoSize = true;
            this.lb_mean.Location = new System.Drawing.Point(12, 345);
            this.lb_mean.Name = "lb_mean";
            this.lb_mean.Size = new System.Drawing.Size(53, 12);
            this.lb_mean.TabIndex = 1;
            this.lb_mean.Text = "lb_mean";
            // 
            // lb_stddev
            // 
            this.lb_stddev.AutoSize = true;
            this.lb_stddev.Location = new System.Drawing.Point(12, 368);
            this.lb_stddev.Name = "lb_stddev";
            this.lb_stddev.Size = new System.Drawing.Size(58, 12);
            this.lb_stddev.TabIndex = 1;
            this.lb_stddev.Text = "lb_stddev";
            // 
            // lb_min
            // 
            this.lb_min.AutoSize = true;
            this.lb_min.Location = new System.Drawing.Point(196, 321);
            this.lb_min.Name = "lb_min";
            this.lb_min.Size = new System.Drawing.Size(42, 12);
            this.lb_min.TabIndex = 1;
            this.lb_min.Text = "lb_min";
            // 
            // lb_max
            // 
            this.lb_max.AutoSize = true;
            this.lb_max.Location = new System.Drawing.Point(196, 345);
            this.lb_max.Name = "lb_max";
            this.lb_max.Size = new System.Drawing.Size(46, 12);
            this.lb_max.TabIndex = 1;
            this.lb_max.Text = "lb_max";
            // 
            // lb_mode
            // 
            this.lb_mode.AutoSize = true;
            this.lb_mode.Location = new System.Drawing.Point(196, 368);
            this.lb_mode.Name = "lb_mode";
            this.lb_mode.Size = new System.Drawing.Size(53, 12);
            this.lb_mode.TabIndex = 1;
            this.lb_mode.Text = "lb_mode";
            // 
            // btn_rgb
            // 
            this.btn_rgb.Location = new System.Drawing.Point(403, 370);
            this.btn_rgb.Name = "btn_rgb";
            this.btn_rgb.Size = new System.Drawing.Size(75, 23);
            this.btn_rgb.TabIndex = 2;
            this.btn_rgb.Text = "RGB";
            this.btn_rgb.UseVisualStyleBackColor = true;
            this.btn_rgb.Click += new System.EventHandler(this.btn_rgb_Click);
            // 
            // btn_live
            // 
            this.btn_live.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_live.Location = new System.Drawing.Point(403, 312);
            this.btn_live.Name = "btn_live";
            this.btn_live.Size = new System.Drawing.Size(75, 23);
            this.btn_live.TabIndex = 2;
            this.btn_live.Text = "Live";
            this.btn_live.UseVisualStyleBackColor = true;
            this.btn_live.Click += new System.EventHandler(this.btn_live_Click);
            // 
            // btn_list
            // 
            this.btn_list.Location = new System.Drawing.Point(403, 341);
            this.btn_list.Name = "btn_list";
            this.btn_list.Size = new System.Drawing.Size(75, 23);
            this.btn_list.TabIndex = 2;
            this.btn_list.Text = "List";
            this.btn_list.UseVisualStyleBackColor = true;
            this.btn_list.Click += new System.EventHandler(this.btn_list_Click);
            // 
            // lb_value
            // 
            this.lb_value.AutoSize = true;
            this.lb_value.Location = new System.Drawing.Point(314, 317);
            this.lb_value.Name = "lb_value";
            this.lb_value.Size = new System.Drawing.Size(51, 12);
            this.lb_value.TabIndex = 3;
            this.lb_value.Text = "lb_value";
            // 
            // HistogramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 405);
            this.Controls.Add(this.lb_value);
            this.Controls.Add(this.btn_live);
            this.Controls.Add(this.btn_list);
            this.Controls.Add(this.btn_rgb);
            this.Controls.Add(this.lb_mode);
            this.Controls.Add(this.lb_max);
            this.Controls.Add(this.lb_min);
            this.Controls.Add(this.lb_stddev);
            this.Controls.Add(this.lb_mean);
            this.Controls.Add(this.lb_count);
            this.Controls.Add(this.chart1);
            this.Name = "HistogramForm";
            this.Text = "HistogramForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistogramForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.Label lb_mean;
        private System.Windows.Forms.Label lb_stddev;
        private System.Windows.Forms.Label lb_min;
        private System.Windows.Forms.Label lb_max;
        private System.Windows.Forms.Label lb_mode;
        private System.Windows.Forms.Button btn_rgb;
        private System.Windows.Forms.Button btn_live;
        private System.Windows.Forms.Button btn_list;
        private System.Windows.Forms.Label lb_value;
    }
}