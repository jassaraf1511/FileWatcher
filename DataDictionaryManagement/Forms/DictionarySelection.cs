using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace DataDictionaryManagement.Forms
{
    public partial class DictionarySelection : Form
    {
        private string dictionaryFile;
        private bool saveClick;
        private bool openClick;
        private bool cancelClicl;
        private string extension;
        public DictionarySelection(string option)
        {
            InitializeComponent();
            this.AutoSize = true;
            SaveClick = false; ;
            OpenClick = false; ;
            CancelClicl = false; ;
            btnOpen.Visible = false;
            btnSave.Visible = false;
            dictionaryFile = null;
            lblMessage.Text = "";

            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;

            if (option.StartsWith("O")) { btnOpen.Visible = true; }
            if (option.StartsWith("S")) { btnSave.Visible = true; }

        }

        public string DictionaryFile { get => dictionaryFile; set => dictionaryFile = value; }
        public bool SaveClick { get => saveClick; set => saveClick = value; }
        public bool OpenClick { get => openClick; set => openClick = value; }
        public bool CancelClicl { get => cancelClicl; set => cancelClicl = value; }
        public string Extension { get => extension; set => extension = value; }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Title = "data Dictionary File Selection - Select A CSV or XML File";
            openFileDialog1.FileName = "*.csv";

            openFileDialog1.Filter = "csv files (*.csv)|*.csv|Xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDataDictFile.Text = openFileDialog1.FileName;

            }
        }


        private void DictionarySelection_FormClosed(object sender, FormClosedEventArgs e)
        {
            
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SaveClick = false; ;
            OpenClick = false; ;
            CancelClicl = true;
            dictionaryFile = null;
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDataDictFile.Text))
            {
                lblMessage.Text = "No Data Dictionary Selected";
                return;
            }

            if (!File.Exists(txtDataDictFile.Text))
            {
                lblMessage.Text = "No Data Dictionary Selected";
                return;
            }

            FileInfo fi = new FileInfo(txtDataDictFile.Text);
            extension = fi.Extension;
            if (extension != ".csv" && extension != ".xml")
            {
                lblMessage.Text = "Invalid Data Dictionary Type";
                return;
            }
            dictionaryFile = txtDataDictFile.Text;
            OpenClick = true;
            this.Close();
        }



    }
}
