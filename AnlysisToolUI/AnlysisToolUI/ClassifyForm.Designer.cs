namespace AnlysisToolUI
{
    partial class ClassifyForm
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.radio_avg = new System.Windows.Forms.RadioButton();
            this.radio_min = new System.Windows.Forms.RadioButton();
            this.radio_max = new System.Windows.Forms.RadioButton();
            this.radio_length = new System.Windows.Forms.RadioButton();
            this.radio_longside = new System.Windows.Forms.RadioButton();
            this.radio_shortside = new System.Windows.Forms.RadioButton();
            this.radio_angle = new System.Windows.Forms.RadioButton();
            this.txt_value1 = new System.Windows.Forms.TextBox();
            this.btn_set = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_value2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radio_avg);
            this.groupBox.Controls.Add(this.radio_min);
            this.groupBox.Controls.Add(this.radio_max);
            this.groupBox.Controls.Add(this.radio_length);
            this.groupBox.Controls.Add(this.radio_longside);
            this.groupBox.Controls.Add(this.radio_shortside);
            this.groupBox.Controls.Add(this.radio_angle);
            this.groupBox.Location = new System.Drawing.Point(12, 6);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(108, 182);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            // 
            // radio_avg
            // 
            this.radio_avg.AutoSize = true;
            this.radio_avg.Checked = true;
            this.radio_avg.Location = new System.Drawing.Point(17, 20);
            this.radio_avg.Name = "radio_avg";
            this.radio_avg.Size = new System.Drawing.Size(44, 16);
            this.radio_avg.TabIndex = 1;
            this.radio_avg.TabStop = true;
            this.radio_avg.Text = "Avg";
            this.radio_avg.UseVisualStyleBackColor = true;
            this.radio_avg.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_min
            // 
            this.radio_min.AutoSize = true;
            this.radio_min.Location = new System.Drawing.Point(17, 42);
            this.radio_min.Name = "radio_min";
            this.radio_min.Size = new System.Drawing.Size(44, 16);
            this.radio_min.TabIndex = 2;
            this.radio_min.Text = "Min";
            this.radio_min.UseVisualStyleBackColor = true;
            this.radio_min.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_max
            // 
            this.radio_max.AutoSize = true;
            this.radio_max.Location = new System.Drawing.Point(17, 64);
            this.radio_max.Name = "radio_max";
            this.radio_max.Size = new System.Drawing.Size(48, 16);
            this.radio_max.TabIndex = 3;
            this.radio_max.Text = "Max";
            this.radio_max.UseVisualStyleBackColor = true;
            this.radio_max.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_length
            // 
            this.radio_length.AutoSize = true;
            this.radio_length.Location = new System.Drawing.Point(17, 86);
            this.radio_length.Name = "radio_length";
            this.radio_length.Size = new System.Drawing.Size(61, 16);
            this.radio_length.TabIndex = 4;
            this.radio_length.Text = "Length";
            this.radio_length.UseVisualStyleBackColor = true;
            this.radio_length.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_longside
            // 
            this.radio_longside.AutoSize = true;
            this.radio_longside.Location = new System.Drawing.Point(17, 108);
            this.radio_longside.Name = "radio_longside";
            this.radio_longside.Size = new System.Drawing.Size(76, 16);
            this.radio_longside.TabIndex = 5;
            this.radio_longside.Text = "LongSide";
            this.radio_longside.UseVisualStyleBackColor = true;
            this.radio_longside.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_shortside
            // 
            this.radio_shortside.AutoSize = true;
            this.radio_shortside.Location = new System.Drawing.Point(17, 130);
            this.radio_shortside.Name = "radio_shortside";
            this.radio_shortside.Size = new System.Drawing.Size(77, 16);
            this.radio_shortside.TabIndex = 6;
            this.radio_shortside.Text = "ShortSide";
            this.radio_shortside.UseVisualStyleBackColor = true;
            this.radio_shortside.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radio_angle
            // 
            this.radio_angle.AutoSize = true;
            this.radio_angle.Location = new System.Drawing.Point(17, 152);
            this.radio_angle.Name = "radio_angle";
            this.radio_angle.Size = new System.Drawing.Size(55, 16);
            this.radio_angle.TabIndex = 7;
            this.radio_angle.Text = "Angle";
            this.radio_angle.UseVisualStyleBackColor = true;
            this.radio_angle.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // txt_value1
            // 
            this.txt_value1.Location = new System.Drawing.Point(130, 35);
            this.txt_value1.Name = "txt_value1";
            this.txt_value1.Size = new System.Drawing.Size(100, 21);
            this.txt_value1.TabIndex = 2;
            // 
            // btn_set
            // 
            this.btn_set.Location = new System.Drawing.Point(130, 124);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(100, 26);
            this.btn_set.TabIndex = 4;
            this.btn_set.Text = "SET";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Value1";
            // 
            // txt_value2
            // 
            this.txt_value2.Location = new System.Drawing.Point(130, 85);
            this.txt_value2.Name = "txt_value2";
            this.txt_value2.Size = new System.Drawing.Size(100, 21);
            this.txt_value2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value2";
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(130, 156);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(100, 26);
            this.btn_reset.TabIndex = 4;
            this.btn_reset.Text = "RESET";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // ClassifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 198);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_set);
            this.Controls.Add(this.txt_value2);
            this.Controls.Add(this.txt_value1);
            this.Controls.Add(this.groupBox);
            this.Name = "ClassifyForm";
            this.Text = "ClassifyForm";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton radio_avg;
        private System.Windows.Forms.RadioButton radio_min;
        private System.Windows.Forms.RadioButton radio_max;
        private System.Windows.Forms.RadioButton radio_length;
        private System.Windows.Forms.RadioButton radio_longside;
        private System.Windows.Forms.RadioButton radio_shortside;
        private System.Windows.Forms.RadioButton radio_angle;
        private System.Windows.Forms.TextBox txt_value1;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_value2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_reset;
    }
}