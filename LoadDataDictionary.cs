using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using CFSB.FileProcessing.DataEntities;

namespace CFSB.FileProcessing.Services
{
    public class LoadDataDictionary
    {

        public static Dictionary<string, XsdDataDictionaryProperty> DataDictionaryList;
        private int ruleId;

        public LoadDataDictionary(string dataDictionaryDoc, bool fromFile)
        {
            parseXmlDoc(dataDictionaryDoc, fromFile);
        }


        /// <summary>
        /// Parse DataDictionary from File or from loaded string from datatbase
        /// </summary>
        /// <param name="dataDictionaryDoc"></param>
        private void parseXmlDoc(string dataDictionaryDoc, bool fromFile)
        {
            XDocument doc = new XDocument();

            if (!fromFile)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(dataDictionaryDoc);
                MemoryStream stream = new MemoryStream(byteArray);
                doc = XDocument.Load(stream);
            }
            else
            {
                doc = XDocument.Load(File.OpenRead(dataDictionaryDoc));
            }

            IEnumerable<XsdDataDictionaryProperty> xsddataDict = new List<XsdDataDictionaryProperty>();

            DataDictionaryList = new Dictionary<string, XsdDataDictionaryProperty>();

            var dictionaryRecords = from dictionaryItem in doc.Descendants("DataDictionaryProduction_x0024_")
                                    select dictionaryItem;

            foreach (var record in dictionaryRecords)
            {
                XsdDataDictionaryProperty dataProperty = new XsdDataDictionaryProperty();
                dataProperty.RuleProperties = new DataRuleProperties();

                dataProperty.Field = (string)record.Element("Field");
                dataProperty.Description = (string)record.Element("Description");
                dataProperty.SrcType = (string)record.Element("SrcType");
                dataProperty.CobolType = (string)record.Element("CobolType");
                dataProperty.SrcStart = convStringToInt((string)record.Element("SrcStart"));
                dataProperty.SrcEnd = convStringToInt((string)record.Element("SrcEnd"));
                dataProperty.SrcLen = convStringToInt((string)record.Element("SrcLen"));
                dataProperty.TrgStart = convStringToInt((string)record.Element("TrgStart"));
                dataProperty.TrgEnd = convStringToInt((string)record.Element("TrgEnd"));
                dataProperty.TrgLen = convStringToInt((string)record.Element("TrgLen"));
                dataProperty.TrgCalcEnd = convStringToInt((string)record.Element("TrgCalcEnd"));
                dataProperty.TruncateLeft = convStringToInt((string)record.Element("TruncateLeft"));
                dataProperty.RedefinedField = (string)record.Element("RedefinedField");

                dataProperty.SQLTargetName = (string)record.Element("SQLTargetName");
                dataProperty.DataSQLType = (string)record.Element("DataSQLType");
                dataProperty.TargetName = (string)record.Element("TargetName");
                dataProperty.DataType = (string)record.Element("DataType");
                dataProperty.ClassName = (string)record.Element("ClassName");
                dataProperty.TransalationRule = (string)record.Element("TransalationRule");
                dataProperty.TransactionDataName = (string)record.Element("TransactionDataName");
                dataProperty.TransactionDataSQLType = (string)record.Element("TransactionDataSQLType");
                dataProperty.TransactionDataType = (string)record.Element("TransactionDataType");
                dataProperty.TransactionTransalationRule = (string)record.Element("TransactionTransalationRule");
                dataProperty.MessageTypeInclude = (string)record.Element("MessageTypeInclude");
                dataProperty.MessageTypeExclude = (string)record.Element("MessageTypeExclude");
                string messageToInclude = dataProperty.MessageTypeInclude != null ? dataProperty.MessageTypeInclude.ToUpper().Trim() : "";
                string messageToExclude = dataProperty.MessageTypeExclude != null ? dataProperty.MessageTypeExclude.ToUpper().Trim() : "";
                if (dataProperty.Field != null)
                {
                    dataProperty.RuleProperties = loadRules(messageToInclude, messageToExclude);
                    DataDictionaryList.Add(dataProperty.Field, dataProperty);
                }
            }
            
        }
        private int convStringToInt(string stringToConvert)
        {
            int intRersult;
            return int.TryParse(stringToConvert, out intRersult) ? (int)intRersult : 0;
        }

        /// <summary>
        /// Load Validation Rules into data dictionary for each Attribute
        /// Temporary Rules are defined in the XML Data Dictionary after will create a specific rule engine layer
        /// </summary>
        /// <param name="messageTypeInclude"></param>
        /// <param name="messageTypeExclude"></param>
        /// <returns></returns>
        private DataRuleProperties loadRules(string messageTypeInclude, string messageTypeExclude)
        {
            DataRuleProperties ruleProperties = new DataRuleProperties();
            ruleId++;
            ruleProperties.isApplyForAll = false;
            ruleProperties.IsToExclude = false;
            ruleProperties.IsToInclude = false;
            
            if (messageTypeInclude == null && messageTypeExclude == null)
            {
                return ruleProperties;
            }

            ruleProperties.RuleId = ruleId;
            if (messageTypeInclude.Equals("ALL")) ruleProperties.isApplyForAll = true;

            if (messageTypeExclude != null) ruleProperties.IsToExclude = true;
            if (messageTypeInclude != null) ruleProperties.IsToInclude = true;
            if (messageTypeInclude != null && !messageTypeInclude.Equals("ALL"))
            {
                ruleProperties.IncludeListValue = loadValuesList(messageTypeInclude);
            }
            if (messageTypeExclude != null && !messageTypeExclude.Equals("ALL"))
            {
                ruleProperties.ExcludeListValue = loadValuesList(messageTypeExclude);
            }
            return ruleProperties;
        }

        private static string[] loadValuesList(string ruleList)
        {
            string[] valueList = ruleList.Split('/');
            return valueList;
        }

    }
}
