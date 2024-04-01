using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics; 

namespace AnlysisToolUI
{
    public partial class HistogramForm : Form
    {
        ImageForm imgForm;
        ListForm listForm;
        private bool IsLive = false;
        private int rgbData = 0;
        public string[] strGrayValues;
        public string[] strRedValues;
        public string[] strGreenValues;
        public string[] strBlueValues;

        public HistogramForm(int[] nValues, ImageForm imageForm, int nChannel)
        {
            InitializeComponent();

            AddChart(nValues);
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[0].Color = Color.Blue;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;

            imgForm = imageForm;
            this.Text = "Histogram of " + imgForm.Text;
            imgForm.IsHistogram = false;
            lb_value.Text = string.Format("Value: 0\nCount: 0");

            chart1.Series[0].Color = Color.Black; 
            chart1.Series[0].BorderColor = Color.Black;

            if (nChannel == 1) { btn_rgb.Visible = false; }
        }

        public void AddChart(int[] nValues)
        {
            int nCnt = 0, nSum = 0;
            int maxidx = 0, minidx = -1;
            chart1.Series[0].Points.Clear();
            for(int i = 0; i < nValues.Length; i++) 
            {
                nCnt += nValues[i];
                nSum += (i * nValues[i]);
                chart1.Series[0].Points.AddXY(i, nValues[i]);

                if (nValues[i] != 0)
                {
                    if(minidx == -1) { minidx = i; }
                    maxidx = i;
                }
            }
            double dAvg = (double)nSum / nCnt;
            double nAvgSum = 0;
            for (int i = 0; i < 256; i++) 
            {
                if (nValues[i] != 0) { nAvgSum += (i - dAvg) * (i - dAvg) * nValues[i]; }
            }
            double dstdDev = Math.Sqrt(nAvgSum / nCnt);
            int maxValue = nValues.Max();
            int maxIndex = nValues.ToList().IndexOf(maxValue);

            lb_count.Text = "Count: " + nCnt.ToString();
            lb_mean.Text = "Mean: " + dAvg.ToString("0.000");
            lb_stddev.Text = "StdDev: " + dstdDev.ToString("0.000");
            lb_min.Text = "Min: " + minidx.ToString();
            lb_max.Text = "Max: " + maxidx.ToString();
            lb_mode.Text = "Mode: " + maxIndex + "(" + nValues.Max() + ")";
            chart1.Invalidate();
        }

        private void HistogramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            imgForm.CloseHistogramForm();
        }

        private void btn_rgb_Click(object sender, EventArgs e)
        {
            if(rgbData == 3) { rgbData = -1; }
            rgbData++;
            switch(rgbData)
            {
                case 0: btn_rgb.ForeColor = Color.Black; chart1.Series[0].Color = Color.Black; chart1.Series[0].BorderColor = Color.Black; break;
                case 1: btn_rgb.ForeColor = Color.Blue; chart1.Series[0].Color = Color.Blue; chart1.Series[0].BorderColor = Color.Blue; break;
                case 2: btn_rgb.ForeColor = Color.Green; chart1.Series[0].Color = Color.Green; chart1.Series[0].BorderColor = Color.Green; break;
                case 3: btn_rgb.ForeColor = Color.Red; chart1.Series[0].Color = Color.Red; chart1.Series[0].BorderColor = Color.Red; break;
            }
            imgForm.rgbData = rgbData;
            imgForm.Histogram();
        }

        private void btn_live_Click(object sender, EventArgs e)
        {
            if(IsLive)
            {
                IsLive = false;
                imgForm.IsHistogram = false;
                btn_live.ForeColor = Color.Black;
            }
            else 
            {
                IsLive = true;
                imgForm.IsHistogram = true;
                btn_live.ForeColor = Color.Red;
                imgForm.Histogram();
            }
        }

        public void GetListData()
        {
            //imgForm.HistogramArray();
            //if (listForm != null) { listForm.SetList(strGrayValues, strBlueValues, strGreenValues, strRedValues); }
            imgForm.Invoke((MethodInvoker)delegate {
                imgForm.HistogramArray();
            });

            if (listForm != null)
            {
                listForm.Invoke((MethodInvoker)delegate {
                    listForm.SetList(strGrayValues, strBlueValues, strGreenValues, strRedValues);
                });
            }
        }

        private void btn_list_Click(object sender, EventArgs e)
        {
            GetListData();

            listForm = new ListForm(strGrayValues, strBlueValues, strGreenValues, strRedValues);
            listForm.Show();
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X >= 490) { return; }
            int xValue = (int)chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

            // X 값이 차트 데이터의 범위를 벗어날 경우 예외 처리
            if (xValue < 0 || xValue >= chart1.Series[0].Points.Count) { return; }

            int yValue = (int)chart1.Series[0].Points[xValue].YValues[0];
            lb_value.Text = string.Format($"Value: {xValue}\nCount: {yValue}");
        }

        private void chart1_PrePaint(object sender, ChartPaintEventArgs e)
        {
            chart1.MouseMove += chart1_MouseMove;
        }
    }
}
