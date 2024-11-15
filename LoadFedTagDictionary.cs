using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
                      
namespace Cfsb.Incoming.FedWires.Services
{
    public class LoadFedTagDictionary
    {

        public static Dictionary<string, string> DataDictionaryList;
        private string dataDictionaryFile;
        public LoadFedTagDictionary(string dataDictionaryFile)
        {
            this.dataDictionaryFile = dataDictionaryFile;
        }
        /// <summary>
        /// Read Csv File and load the Data Dictionary
        /// </summary>
        public int loadTagsToDictionary()
        {
            DataDictionaryList = new Dictionary<string, string>();
            String tagRecord;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(dataDictionaryFile);
                //Read the first line of text
                tagRecord = sr.ReadLine();

                while (tagRecord != null)
                {


                    tagRecord = sr.ReadLine();
                    if (!string.IsNullOrEmpty(tagRecord))
                    {
                        string[] tagArray = tagRecord.Split(',');
                        if (tagArray != null && tagArray.Length >= 3)
                        {
                            DataDictionaryList.Add(tagArray[1], tagArray[2]);
                        }
                    }
                }
                //close the file
                sr.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message + e.StackTrace.ToString());
            }
            finally
            {

            }
            return DataDictionaryList.Count();
        }
    }
}
