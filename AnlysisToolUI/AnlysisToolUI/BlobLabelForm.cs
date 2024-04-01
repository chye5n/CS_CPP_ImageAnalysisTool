using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AnlysisToolUI
{
    public partial class BlobLabelForm : Form
    {
        public BlobLabelForm(string[] strBlobInform)
        {
            InitializeComponent();
            listView.Columns.Add("", 30, HorizontalAlignment.Center);
            listView.Columns.Add("Avg", 70, HorizontalAlignment.Center);
            listView.Columns.Add("Min", 70, HorizontalAlignment.Center);
            listView.Columns.Add("Max", 70, HorizontalAlignment.Center);
            listView.Columns.Add("Length", 70, HorizontalAlignment.Center);
            listView.Columns.Add("LongSide", 70, HorizontalAlignment.Center);
            listView.Columns.Add("ShortSide", 70, HorizontalAlignment.Center);
            listView.Columns.Add("Angle", 70, HorizontalAlignment.Center);

            SetBlob(strBlobInform);
        }

        public void SetBlob(string[] strBlobInform)
        {
            listView.Items.Clear();
            for (int i = 0; i < strBlobInform.Length / 7; i++)
            {
                string[] strItem = { (i + 1).ToString(), strBlobInform[i * 7], strBlobInform[i * 7 + 1], strBlobInform[i * 7 + 2], strBlobInform[i * 7 + 3], strBlobInform[i * 7 + 4], strBlobInform[i * 7 + 5], strBlobInform[i * 7 + 6] };
                ListViewItem listViewItem = new ListViewItem(strItem);
                listView.Items.Add(listViewItem);
            }
        }

        private void BlobLabelForm_Load(object sender, EventArgs e)
        {
            Height = 38 + listView.Height + pictureBox.Height;
            if(pictureBox.Width > listView.Width) { Width = 16 + pictureBox.Width; }
        }
    }
}
