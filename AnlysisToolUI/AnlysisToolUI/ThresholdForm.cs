using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnlysisToolUI
{
    public partial class ThresholdForm : Form
    {
        Form1 toolForm;
        ImageForm imageForm;
        double dAvg;
        int dDefalut = 128;
        int dOtsu;
        int classify = 0;
        int lowValue;
        int highValue;
        Pen pen = new Pen(Color.Red, 2);
        Pen Overpen = new Pen(Color.Green, 2);
        Rectangle rectangle;
        Rectangle Overrectangle;
        Bitmap bmpOutData;

        public ThresholdForm(Form1 toolForm, int[] values)
        {
            InitializeComponent();

            AddChart(values);
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[0].Color = Color.Blue;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;

            maxGVScroll.Value = dDefalut;
            txt_minGVscroll.Text = minGVScroll.Value.ToString();
            txt_maxGVscroll.Text = maxGVScroll.Value.ToString();
            SetRect();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            this.toolForm = toolForm;
        }

        public void AddChart(int[] values)
        {
            int nCnt = 0, nSum = 0;
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 256; i++)
            {
                nCnt += values[i];
                nSum += (i * values[i]);
                chart1.Series[0].Points.AddXY(i, values[i]);
            }
            dAvg = (double)nSum / nCnt;
        }

        public void GetImageForm(ImageForm imgForm)
        {
            imageForm = imgForm;
            dOtsu = imageForm.thresholdOtsu();
            SetRect();
            SetImage();
        }

        private void SetImage()
        {
            if (imageForm == null) { return; }
            if(comboBox2.SelectedIndex == 0) { pen.Color = Color.Red; imageForm.thresholdRed(minGVScroll.Value, maxGVScroll.Value); }
            else if(comboBox2.SelectedIndex == 1) { pen.Color = Color.Black; imageForm.thresholdBW(minGVScroll.Value, maxGVScroll.Value); }
            else if(comboBox2.SelectedIndex == 2) {  pen.Color = Color.Blue; imageForm.thresholdOverUnder(minGVScroll.Value, maxGVScroll.Value); }
        }

        private void SetRect()
        {
            if (comboBox2.SelectedIndex == 2)
            {
                rectangle = new Rectangle(11, 2, (int)(minGVScroll.Value * 1.23), chart1.Height - 4);
                Overrectangle = new Rectangle(11 + (int)(maxGVScroll.Value * 1.23), 2, (int)((255 - maxGVScroll.Value) * 1.23), chart1.Height - 4);
            }
            else { rectangle = new Rectangle(11 + (int)(minGVScroll.Value * 1.23), 2, (int)((maxGVScroll.Value - minGVScroll.Value) * 1.23), chart1.Height - 4); }
        }

        private void minGVScroll_Scroll(object sender, ScrollEventArgs e)
        {
            txt_minGVscroll.Text = minGVScroll.Value.ToString();
            if (minGVScroll.Value >= maxGVScroll.Value) { maxGVScroll.Value = minGVScroll.Value; txt_maxGVscroll.Text = maxGVScroll.Value.ToString(); }
            SetRect();
            
            chart1.Invalidate();
            SetImage();
        }

        private void maxGVScroll_Scroll(object sender, ScrollEventArgs e)
        {
            txt_maxGVscroll.Text = maxGVScroll.Value.ToString();
            if (minGVScroll.Value >= maxGVScroll.Value) { minGVScroll.Value = maxGVScroll.Value; txt_minGVscroll.Text = minGVScroll.Value.ToString(); }
            SetRect();

            chart1.Invalidate();
            SetImage();
        }

        private void btn_auto_Click(object sender, EventArgs e)
        {
            Value();
            SetRect();
            SetImage();
            chart1.Invalidate();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            toolForm.thresholdImage();
            SetImage();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_minGVscroll.Text = string.Empty;
            txt_maxGVscroll.Text = string.Empty;
            rectangle.Width = 0;
            chart1.Invalidate();

            imageForm.ResetImage();
        }

        private void btn_label_Click(object sender, EventArgs e)
        {
            BlobLabelForm blobLabelForm = new BlobLabelForm(BlobValue());
            blobLabelForm.pictureBox.Image = bmpOutData;
            blobLabelForm.Show();
        }

        private void chart1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, rectangle);
            if(comboBox2.SelectedIndex == 2) { e.Graphics.DrawRectangle(Overpen, Overrectangle); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Value();
            SetImage();
            chart1.Invalidate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRect();
            SetImage();
            chart1.Invalidate();
        }

        private void ScrollValue(int minGV, int maxGV)
        {
            minGVScroll.Value = minGV;
            txt_minGVscroll.Text = minGVScroll.Value.ToString();

            maxGVScroll.Value = maxGV;
            txt_maxGVscroll.Text = maxGVScroll.Value.ToString();
        }

        private void Value()
        {
            if (comboBox1.SelectedIndex == 0) { ScrollValue(0, dDefalut); }
            else if (comboBox1.SelectedIndex == 1) { ScrollValue(0, (int)dAvg); }
            else if (comboBox1.SelectedIndex == 2) { ScrollValue(0, dOtsu); }
        }

        private string[] BlobValue()
        {
            int[] nBlobInform;
            nBlobInform = imageForm.Blob(minGVScroll.Value, maxGVScroll.Value, classify, lowValue, highValue, out bmpOutData);
            return Array.ConvertAll(nBlobInform, n => n.ToString());
        }

        private void txt_minGVscroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Enter 키를 눌렀을 때
            {
                if (txt_minGVscroll.Text == "") { txt_minGVscroll.Text = "0"; }
                if (Convert.ToInt32(txt_minGVscroll.Text) >= 256) { txt_minGVscroll.Text = "255"; }
                minGVScroll.Value = Convert.ToInt32(txt_minGVscroll.Text);
                if (minGVScroll.Value > maxGVScroll.Value)
                {
                    maxGVScroll.Value = minGVScroll.Value;
                    txt_maxGVscroll.Text = maxGVScroll.Value.ToString();
                }
                SetRect();
                SetImage();
                chart1.Invalidate();
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) { e.Handled = true; }
        }

        private void txt_maxGVscroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Enter 키를 눌렀을 때
            {
                if (txt_minGVscroll.Text == "") { txt_minGVscroll.Text = "0"; }
                if (Convert.ToInt32(txt_maxGVscroll.Text) >= 256) { txt_maxGVscroll.Text = "255"; }
                maxGVScroll.Value = Convert.ToInt32(txt_maxGVscroll.Text);
                if (maxGVScroll.Value < minGVScroll.Value)
                {
                    minGVScroll.Value = maxGVScroll.Value;
                    txt_minGVscroll.Text = minGVScroll.Value.ToString();
                }
                SetRect();
                SetImage();
                chart1.Invalidate();
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) { e.Handled = true; }
        }

        private void btn_blobset_Click(object sender, EventArgs e)
        {
            ClassifyForm classifyForm = new ClassifyForm(this);
            classifyForm.Show();
        }

        public void SetClassify(int classify, int Value1, int Value2)
        {
            this.classify = classify;
            lowValue = Value1;
            highValue = Value2;
        }
    }
}
