using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Cfsb.Incoming.FedWires.Utils
{
    public static class PropertyExtensions
    {
        public static void InitializePropery<T>(this T clsObject) where T : class
        {
            PropertyInfo[] properties = clsObject.GetType().GetProperties();
            int i = 0;
            foreach (var info in properties)
            {
                // if a string and null, set to String.Empty
                i++;
                if (info.PropertyType == typeof(string) && info.GetValue(clsObject, null) == null)
                {
                    info.SetValue(clsObject, String.Empty, null);
                    // info.SetValue(clsObject, "TEST_"+i, null);
                }

            }
        }

        public static string SetCsvRecord<T>(this T clsObject, string separator = null) where T : class
        {
            PropertyInfo[] properties = clsObject.GetType().GetProperties();
            int i = 0;
            StringBuilder sbRecord = new StringBuilder();
            foreach (var info in properties)
            {

                // if a string and null, set to String.Empty
                //csvString += "\""+"Date Generated: \n" ; 

                if (info.PropertyType == typeof(string))
                {
                    i++;
                    string propValue;
                    if (info.GetValue(clsObject, null) != null)
                        propValue = (string)info.GetValue(clsObject);
                    else
                        propValue = "";
                    if (i > 1)
                    {
                        sbRecord.Append("\t" + '"' + propValue + '"');
                    }
                    else
                    {
                        sbRecord.Append('"' + propValue + '"');
                    }

                }
            }
            return sbRecord.ToString();
        }

        public static string SetCsRecordHeader<T>(this T clsObject, string separator = null) where T : class
        {
            PropertyInfo[] properties = clsObject.GetType().GetProperties();
            int i = 0;
            StringBuilder sbRecord = new StringBuilder();
            foreach (var info in properties)
            {

                // if a string and null, set to String.Empty

                if (info.PropertyType == typeof(string) && info.GetValue(clsObject, null) != null)
                {
                    i++;
                    string propName = (string)info.Name;
                    if (i > 1)
                    {
                        sbRecord.Append("," + '"' + propName + '"');
                    }
                    else
                    {
                        sbRecord.Append('"' + propName + '"');
                    }

                }
            }
            return sbRecord.ToString();
        }

        // Set Field Class prperty having same field Name with the Array
        public static void SetTableFieldWithArray<T>(this T clsObject, string fieldname, string[] strArray) where T : class
        {

            System.Collections.Generic.List<string> fieldList = new System.Collections.Generic.List<string>();
            if (clsObject == null) return;
            if (strArray == null) return;

            int count = 0;
            int i = 0;
            PropertyInfo[] properties = clsObject.GetType().GetProperties();

            try
            {
                foreach (var info in properties)
                {

                    if (info.Name.StartsWith(fieldname) && info.PropertyType == typeof(string))
                    {
                        fieldList.Add(info.Name);
                    }
                }
            }
            catch (Exception exProp)
            {
                Console.WriteLine(exProp.Message);
                Console.WriteLine(exProp.StackTrace);
                Console.WriteLine(exProp.Data);

            }
            if (fieldList.Count < 1) return;

            fieldList.Sort();
            count = fieldList.Count;
            try
            {
                foreach (string str in fieldList)
                {
                    clsObject.GetType().GetProperty(str).SetValue(clsObject, String.Empty);
                    if (strArray != null && strArray.Length > i && !String.IsNullOrEmpty(strArray[i]))
                    {
                        clsObject.GetType().GetProperty(str).SetValue(clsObject, strArray[i]);
                    }
                    i++;
                }
            }
            catch (Exception exProp)
            {
                Console.WriteLine(exProp.Message);
                Console.WriteLine(exProp.StackTrace);
                Console.WriteLine(exProp.Data);

            }
        }
    }
}
