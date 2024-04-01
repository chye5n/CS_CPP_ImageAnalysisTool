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
    public partial class ListForm : Form
    {
        public ListForm(string[] strGrayValues, string[] strBlueValues, string[] strGreenValues, string[] strRedValues)
        {
            InitializeComponent();
            listView.Columns.Add("Value", 50, HorizontalAlignment.Center);
            listView.Columns.Add("Gray", 50, HorizontalAlignment.Center);
            listView.Columns.Add("Blue", 50, HorizontalAlignment.Center);
            listView.Columns.Add("Green", 50, HorizontalAlignment.Center);
            listView.Columns.Add("Red", 50, HorizontalAlignment.Center);

            SetList(strGrayValues, strBlueValues, strGreenValues, strRedValues);
        }

        public void SetList(string[] strGrayValues, string[] strBlueValues, string[] strGreenValues, string[] strRedValues)
        {
            //listView.Items.Clear();
            if(listView.Items.Count == 0) 
            {
                for (int i = 0; i < strGrayValues.Length; i++)
                {
                    string[] strItem = { i.ToString(), strGrayValues[i], strBlueValues[i], strGreenValues[i], strRedValues[i] };
                    ListViewItem listViewItem = new ListViewItem(strItem);
                    listView.Items.Add(listViewItem);
                }
            }
            else
            {
                for (int i = 0; i < strGrayValues.Length; i++)
                {
                    ListViewItem item = listView.Items[i];
                    item.SubItems[1].Text = strGrayValues[i];
                    item.SubItems[2].Text = strBlueValues[i];
                    item.SubItems[3].Text = strGreenValues[i];
                    item.SubItems[4].Text = strRedValues[i];
                }
                listView.Refresh();
            }
        }
    }
}
