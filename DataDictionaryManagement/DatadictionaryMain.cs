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
using DataDictionaryManagement.Forms;
namespace DataDictionaryManagement
{
    public partial class DatadictionaryMain : Form
    {
        private DataTable dataTable;
        private DataSet dataSet;
        private string dictionaryFile;
        private string fileExtention;
        private bool dictLoaded;
        private bool dictUpdated;
        private bool okExit;
        private int currentRow;
        private ContextMenuStrip mnu;
        private int col = -1;
        private int row = -1;
        private string currentFileName;

        public DatadictionaryMain()
        {
            InitializeComponent();
            // DataDictionaryView.Visible = false;
            //   this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.SizableToolWindow;//.None;
            //   this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //  dataSet.ReadXml(@"C:\CFSB\FedDataDictionary.xml");
            this.WindowState = FormWindowState.Maximized;
            DataDictionaryView.Visible = false;
            lblDataDictionaryFile.Visible = false;
            dictLoaded = false;
            dictUpdated = false;
            okExit = true;
            currentFileName = string.Empty;
            SaveAsDataDictionary.Enabled = false;
            SaveDataDictionary.Enabled = false;

            mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuInsert = new ToolStripMenuItem("Insert Row");
            ToolStripMenuItem mnuDeletete = new ToolStripMenuItem("Delete Row");
            ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Copy Row");
            ToolStripMenuItem mnuPaste = new ToolStripMenuItem("Paste Row");

            mnuInsert.Click += new EventHandler(Menu_Insert_Row_Click);
            mnuDeletete.Click += new EventHandler(Menu_Delete_Row_Click);
            mnuCopy.Click += new EventHandler(Menu_Copy_Row_Click);
            mnuPaste.Click += new EventHandler(Menu_Paste_Row_Click);
            mnu.BackColor = Color.Azure;


            mnu.Items.AddRange(new ToolStripItem[] { mnuInsert, mnuDeletete, mnuCopy, mnuPaste });
            DataDictionaryView.ContextMenuStrip = mnu;
            /*   string xmlfile = @"C:\CFSB\DotNetProject\Fund Automation\Data\FedWire_Message_Data_Dictionnary.xml";

               DataSet dataSet = new DataSet();
               dataSet.DataSetName = "DataDicyionary";

               DataTable dictTable = readCSV(@"C:\CFSB\DotNetProject\Fund Automation\Data\FedWire_Message_Data_Dictionnary.csv");

               DataDictionaryView.DataSource = dictTable;

               dataSet.Tables.Add(dictTable);


              // dictTable.WriteXml(xmlfile);
               using (StreamWriter fs = new StreamWriter(xmlfile)) // XML File Path
              {
                   dataSet.WriteXml(fs);

               }
            */



        }

        private void DataDictionaryView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            row = e.RowIndex;
            col = e.ColumnIndex;
        }

        private void DataDictionaryView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Int32 yourindexColumn = 1;
            if (e.RowIndex < 0 || e.ColumnIndex != yourindexColumn) return;

            DataGridViewCell cell = DataDictionaryView.Rows[e.RowIndex].Cells[yourindexColumn];

            String value = cell.Value == null ? string.Empty : cell.Value.ToString();

            if (!string.IsNullOrEmpty(value) && !value.StartsWith("0"))
            {
                DataDictionaryView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                // DataDictionaryView.Rows[e.RowIndex].ReadOnly = true;
            }

        }

        private void DataDictionaryView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentRow = e.RowIndex;
            row = e.RowIndex;
            col = e.ColumnIndex;
        }
        private DataTable readCSV(string filePath)
        {
            int i = 0;
            DataTable dt = new DataTable();
            dt.TableName = "DictionaryData";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string strLine = sr.ReadLine();

                string[] strArray = strLine.Split(',');


                foreach (string value in strArray)
                {
                    dt.Columns.Add(value.Trim());
                }
                DataRow dr = dt.NewRow();

                {


                    while (sr.Peek() >= 0)
                    {

                        strArray = strLine.Split(',');
                        try
                        {
                            strLine = sr.ReadLine();

                            if (i > 0)
                            {
                                dt.Rows.Add(strArray);
                            }
                        }
                        catch (Exception ex)
                        {
                            int j = strArray.Length;
                            System.Console.WriteLine(j);
                        }
                        i++;
                    }
                }

            }
            return dt;
        }



        private void LoadDataDictionary_Click(object sender, EventArgs e)
        {
            //  dictLoaded = false;
            //  dictUpdated = false;
            //DataDictionaryView.Visible = false;
            //lblDataDictionaryFile.Visible = false;
            checkBeforeExit();
            if (!okExit) return;

            DictionarySelection selectionForm = new DictionarySelection("Open");
            selectionForm.ShowDialog();

            if (string.IsNullOrEmpty(selectionForm.DictionaryFile) || !selectionForm.OpenClick)
            {
                return;
            }

            fileExtention = selectionForm.Extension;
            dictionaryFile = selectionForm.DictionaryFile;

            dataSet = new DataSet();
            dataTable = new DataTable();
            dataSet.DataSetName = "DataDicyionary";

            if (fileExtention.Contains("csv"))
            {
                dataTable = readCSV(dictionaryFile);
            }

            if (fileExtention.Contains("xml"))
            {
                dataSet.ReadXml(dictionaryFile);
                dataTable = dataSet.Tables["DictionaryData"];

            }

            if (dataTable.Rows.Count > 0 && containColumn(dataTable))
            {
                DataDictionaryView.DataSource = dataTable;
                DataDictionaryView.Visible = true;
                lblDataDictionaryFile.Visible = true;
                lblDataDictionaryFile.Text = "Data Dictionary File : " + selectionForm.DictionaryFile;
                SaveAsDataDictionary.Enabled = true;
                SaveDataDictionary.Enabled = true;
                currentFileName = selectionForm.DictionaryFile; ;
                dictLoaded = true;
            }

            dictUpdated = false;
        }

        private void DataDictionaryView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dictUpdated = true;
        }

        private void DataDictionaryView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dictUpdated = true;
        }





        private int deleteRelatedSubtag(string searchTagName, string searchMapProperty, string searchSubTagId)
        {
            int tagCount = 0;

            StringBuilder sbTags = new StringBuilder();
            string fieldName = "TagName";
            string fieldvalue = searchTagName;

            if (!searchSubTagId.StartsWith("0"))
            {
                fieldName = "MapProperty";
                fieldvalue = searchMapProperty;
            }
            bool eof = false;
            while (!eof)
            {


                try
                {
                    var results = from row in dataTable.AsEnumerable() where row.Field<string>(fieldName) == fieldvalue select row;
                    if (results.Count() > 0)
                    {


                        foreach (DataRow row in results)
                        {
                            tagCount++;

                            var tagName = row.Field<string>("TagName");
                            var tagId = row.Field<string>("TagId");
                            var subTagId = row.Field<string>("SubTagId");
                            sbTags.AppendLine(tagId + " - " + subTagId + " - " + tagName);

                            dataTable.Rows.Remove(row);
                            break;
                        }
                    }
                    else
                    {
                        eof = true;
                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }

            }




            if (tagCount > 0)
            {
                dataTable.AcceptChanges();
                MessageBox.Show(sbTags.ToString());
            }
            return tagCount;
        }

        private void DataDictionaryView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            currentRow = e.RowIndex;

            if (e.RowIndex < 0) return;

            if (e.Button != MouseButtons.Right) return;
            row = e.RowIndex;
            col = e.ColumnIndex;


            // string test = DataDictionaryView.Rows[currentRow].Cells[2].Value.ToString();

            //  MessageBox.Show(test);
        }

        private void Menu_Insert_Row_Click(Object sender, System.EventArgs e)
        {
            DataRow newBlankRow1 = dataTable.NewRow();
            dataTable.Rows.InsertAt(newBlankRow1, row);
            MessageBox.Show(DataDictionaryView.Rows[row].Cells[2].Value.ToString());
            dictUpdated = true;
        }
        private void Menu_Delete_Row_Click(Object sender, System.EventArgs e)
        {
            if (row < 0) return;
            string tagId = DataDictionaryView.Rows[row].Cells[0].Value.ToString();
            string subTagId = DataDictionaryView.Rows[row].Cells[1].Value.ToString();
            string tagname = DataDictionaryView.Rows[row].Cells[3].Value.ToString();
            string mapProperty = DataDictionaryView.Rows[row].Cells[8].Value.ToString();
            MessageBox.Show(tagId + " - " + subTagId + " - " + tagname + " - " + mapProperty);

            int count;



            if (int.Parse(subTagId) > 0)
            {
                count = deleteRelatedSubtag(tagname, mapProperty, subTagId);
                dictUpdated = true;
            }
        }
        private void Menu_Copy_Row_Click(Object sender, System.EventArgs e)
        {
            MessageBox.Show(DataDictionaryView.Rows[row].Cells[2].Value.ToString());
        }


        private void Menu_Paste_Row_Click(Object sender, System.EventArgs e)
        {
            if (row < 0) return;



            MessageBox.Show(DataDictionaryView.Rows[row].Cells[2].Value.ToString());
        }

        private void DataDictionary_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DatadictionaryMain_Load(object sender, EventArgs e)
        {

        }

        private void DatadictionaryMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (okExit) checkBeforeExit();
        }
        private void checkBeforeExit()
        {
            okExit = true;

            if (dataTable == null || dataTable.Rows.Count <= 0 || !DataDictionaryView.Visible) return;
            if (!dictUpdated) return;


            DialogResult res = MessageBox.Show("Data Dictionary Not Saved, Are you sure you want to cancel changes", "Data Dictionary Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                dataTable.Clear();
                DataDictionaryView.Visible = false;
                dictUpdated = false;
                MessageBox.Show("Data Dictionary Not Saved");
                return;
                //Some task…  
            }
            if (res == DialogResult.Cancel)
            {
                okExit = false;
            }
        }
        private void SaveDataDicionary_click(object sender, EventArgs e)
        {
            if (dataTable == null || dataTable.Rows.Count <= 0 || !DataDictionaryView.Visible) return;
            if (string.IsNullOrEmpty(currentFileName))
            {
                saveDataDictionaryAsnewFile();
                return;
            }

            if (fileExtention.Contains("csv")) toCSV(dataTable, currentFileName);
            if (fileExtention.Contains("xml")) toXml(dataTable, currentFileName);

        }

        private void SaveAsDataDictionary_Click(object sender, System.EventArgs e)
        {

            if (dataTable == null || dataTable.Rows.Count <= 0 || !DataDictionaryView.Visible) return;

            saveDataDictionaryAsnewFile();

        }
        /// <summary>
        /// Save datadictionary in new file
        /// </summary>
        /// <returns></returns>
        private bool saveDataDictionaryAsnewFile()
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV File|*.csv|Xml File|*.xml";
            saveFileDialog1.Title = "Save an Image File";

            saveFileDialog1.ShowDialog();
            if (string.IsNullOrEmpty(saveFileDialog1.FileName))
            {
                saveFileDialog1.Dispose();
                MessageBox.Show("No File Selected - Data Dictionary not saved");
                return false; ;
            }

            string fileToSave = saveFileDialog1.FileName;
            int indexFilter = saveFileDialog1.FilterIndex;

            saveFileDialog1.Dispose();


            MessageBox.Show(fileToSave);
            lblDataDictionaryFile.Text = "Data Dictionary File : " + fileToSave;
            currentFileName = fileToSave;
            // Saves the Image via a FileStream created by the OpenFile method.
            // System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
            // Saves the Image in the appropriate Format
            // File type selected in the dialog box.
            // NOTE that the FilterIndex property is one-based.
            switch (indexFilter)
            {

                case 1:
                    fileExtention = ".csv";
                    toCSV(dataTable, fileToSave);
                    break;

                case 2:
                    fileExtention = ".xml";
                    toXml(dataTable, fileToSave);
                    break;
            }

            return true;

        }
        private void toCSV(DataTable dtDataTable, string strFilePath)
        {
            backupFile(strFilePath);
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        /// <summary>
        /// backup the existing fiel
        /// </summary>
        /// <param name="dtDataTable"></param>
        /// <param name="fileName"></param>
        private void backupFile(string fileName)
        {
            string fileToBackup = fileName + "_Backup";

            if (File.Exists(fileToBackup))
            {
                File.Delete(fileToBackup);
            }
            if (File.Exists(fileName))
            {
                File.Copy(fileName, fileToBackup);
            }
        }
        private void toXml(DataTable dtDataTable, string fileName)
        {
            backupFile(fileName);
            DataSet xmlDataset = new DataSet();

            xmlDataset.DataSetName = "DataDicyionary";
            xmlDataset.Tables.Add(dtDataTable);

            using (StreamWriter fs = new StreamWriter(fileName, false)) // XML File Path
            {
                xmlDataset.WriteXml(fs);

            }



        }

        private void NewDataDictionary_Click(object sender, EventArgs e)
        {
            checkBeforeExit();

            if (!okExit) return;
            currentFileName = string.Empty;
            if (dataTable != null) dataTable.Clear(); else dataTable = new DataTable();

            dataTable.TableName = "DictionaryData";

            dataTable.Columns.Add("TagId", typeof(String));
            dataTable.Columns.Add("SubTagId", typeof(String));
            dataTable.Columns.Add("TagDescription", typeof(String));
            dataTable.Columns.Add("TagName", typeof(String));
            dataTable.Columns.Add("Required", typeof(String));
            dataTable.Columns.Add("TagValues", typeof(String));
            dataTable.Columns.Add("DependencyWithValues", typeof(String));
            dataTable.Columns.Add("TagPropertyType", typeof(String));
            dataTable.Columns.Add("MapProperty", typeof(String));
            dataTable.Columns.Add("RuleName", typeof(String));
            dataTable.Columns.Add("DataLength", typeof(String));
            dataTable.Columns.Add("StartPosition", typeof(String));
            dataTable.Columns.Add("EndPosition", typeof(String));

            DataDictionaryView.DataSource = dataTable;
            DataDictionaryView.Visible = true;
            lblDataDictionaryFile.Visible = true;
            lblDataDictionaryFile.Text = "New Data Dictionary ";
            SaveAsDataDictionary.Enabled = true;
            SaveDataDictionary.Enabled = true;
            currentFileName = string.Empty;
            fileExtention = string.Empty;
            dictLoaded = false;
            dictUpdated = false;

        }

        private bool containColumn(DataTable table)
        {
            DataColumnCollection columns = table.Columns;
            if (!columns.Contains("TagId") ||
                !columns.Contains("SubTagId") ||
                !columns.Contains("TagDescription") ||
                !columns.Contains("TagName") ||
                !columns.Contains("Required") ||
                !columns.Contains("TagValues") ||
                !columns.Contains("DependencyWithValues") ||
                !columns.Contains("TagPropertyType") ||
                !columns.Contains("MapProperty") ||
                !columns.Contains("RuleName") ||
                !columns.Contains("DataLength") ||
                !columns.Contains("StartPosition") ||
                !columns.Contains("EndPosition"))
            {
                return false;
            }
            return true;
        }
    }
}
