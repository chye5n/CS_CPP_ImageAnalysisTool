using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AnlysisToolUI
{
    public partial class Form1 : Form
    {
        private List<ImageForm> ImageFormList = new List<ImageForm>();
        private string selectTool = "Rect";
        public string ActivateForm = "";
        ThresholdForm thresholdForm;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
            if (sender == toolStripRect) { lb_inform.Text = "Rectangle selection"; }
        }

        private void toolStripMagnify_MouseEnter(object sender, EventArgs e)
        {
            if (sender == toolStripMagnify) { lb_inform.Text = "Magnifying glass (마우스 좌측 버튼: 확대\"+\", 우측 버튼: \"-\")"; }
        }

        private void toolStripScroll_MouseEnter(object sender, EventArgs e)
        {
            if (sender == toolStripScroll) { lb_inform.Text = "Scrolling tool (마우스로 이미지 이동)"; }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0 && File.Exists(files[0]))
            {
                OpenForm(files[0]);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            lb_inform.Text = "<<Drag and Drop>>";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)    //OpenFileDialog를 사용해서 이미지 열기
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OpenForm(openFileDialog1.FileName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is ImageForm)
                {
                    form.Close();
                    return;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ImageForm imgform in ImageFormList)
            {
                System.Drawing.Image img = imgform.GetImage();
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:";
                saveFileDialog1.Title = imgform.Text;
                saveFileDialog1.FileName = imgform.Text;
                saveFileDialog1.Filter = "Bitmap File(*.bmp)|*.bmp |JPEG File(*.jpg)|*.jpg |PNG File(*.png)|*.png";
                
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) { img.Save(saveFileDialog1.FileName); }
                return;
            }

        }

        private void OpenForm(string fileName)  //이미지 폼 열기
        {
            ImageForm imageForm = new ImageForm(this, fileName, selectTool);
            ImageFormList.Add(imageForm);
            imageForm.Show();
        }

        private void toolStripRect_Click(object sender, EventArgs e)
        {
            toolStripRect.Checked = true;
            toolStripMagnify.Checked = false;
            toolStripScroll.Checked = false;
            selectTool = toolStripRect.Text;
            ChangeItme();
        }

        private void toolStripMagnify_Click(object sender, EventArgs e)
        {
            toolStripRect.Checked = false;
            toolStripMagnify.Checked = true;
            toolStripScroll.Checked = false;
            selectTool = toolStripMagnify.Text;
            ChangeItme();
        }

        private void toolStripScroll_Click(object sender, EventArgs e)
        {
            toolStripRect.Checked = false;
            toolStripMagnify.Checked = false;
            toolStripScroll.Checked = true;
            selectTool = toolStripScroll.Text;
            ChangeItme();
        }

        private void ChangeItme()
        {
            foreach (ImageForm imgform in ImageFormList)
            {
                imgform.SetMenuItem(selectTool);
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ImageForm imgform in ImageFormList)
            {
                if(imgform.Text == ActivateForm)
                {
                    imgform.IsHistogram = true;
                    imgform.Histogram();
                }
            }
        }

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenthresholdForm();
        }

        public void OpenthresholdForm()
        {//ImageFormList가 null이 아니고, 1채널 이미지인 경우 nValues의 이미지의 값을 가져와서 사용
            int[] nValues = new int[256];
            thresholdForm = new ThresholdForm(this, nValues);
            thresholdForm.Show();

            if (ImageFormList.Count > 0) { thresholdImage(); }            
        }

        public void thresholdImage()
        {
            foreach (ImageForm imgform in ImageFormList)
            {
                if (imgform.Text == ActivateForm)
                {
                    if(imgform.GetnChannels() != 1) { return; }
                    int[] nValues = imgform.thresholdChart();
                    thresholdForm.AddChart(nValues);
                    thresholdForm.GetImageForm(imgform);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.T) { OpenthresholdForm(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
