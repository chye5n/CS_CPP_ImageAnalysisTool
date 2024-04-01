using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnlysisToolUI
{
    public partial class ClassifyForm : Form
    {
        ThresholdForm thresholdForm;
        int classify = 1;
        public ClassifyForm(ThresholdForm thresholdForm)
        {
            InitializeComponent();
            txt_value1.Text = "0";
            txt_value2.Text = "255";
            this.thresholdForm = thresholdForm;
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            if(radio_avg.Checked || radio_min.Checked || radio_max.Checked) { txt_value1.Text = "0"; txt_value2.Text = "255"; }
            else if(radio_length.Checked || radio_longside.Checked || radio_shortside.Checked) { txt_value1.Text = "0"; txt_value2.Text = string.Empty; }
            else if (radio_angle.Checked) { txt_value1.Text = "-45"; txt_value2.Text = "45"; }

            CheckClassify();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            int lowValue, highValue;
            if (txt_value1.Text == "") 
            {
                if (radio_angle.Checked) {lowValue = -45; }
                else { lowValue = 0; }
            }
            else { lowValue = Convert.ToInt32(txt_value1.Text); }
            if(txt_value2.Text == "") 
            {
                if (radio_avg.Checked || radio_min.Checked || radio_max.Checked) { highValue = 255; }
                else if (radio_length.Checked || radio_longside.Checked || radio_shortside.Checked) { highValue = -1; }
                else { highValue = 45; }
            }
            else { highValue = Convert.ToInt32(txt_value2.Text); }

            if((highValue != -1) && (lowValue > highValue)) { (highValue, lowValue) = (lowValue, highValue); }

            thresholdForm.SetClassify(classify, lowValue, highValue);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            radio_avg.Checked = true;
            thresholdForm.SetClassify(0, 0, 0);
        }

        private void CheckClassify()
        {
            if (radio_avg.Checked) { classify = 1; }
            else if (radio_min.Checked) { classify = 2; }
            else if (radio_max.Checked) { classify = 3; }
            else if (radio_length.Checked) { classify = 4; }
            else if (radio_longside.Checked) { classify = 5; }
            else if (radio_shortside.Checked) { classify = 6; }
            else if (radio_angle.Checked) { classify = 7; }
        }
    }
}
