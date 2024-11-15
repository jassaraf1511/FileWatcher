using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSB.FileProcessing.Services
{
    class DataTransformationRule
    {
        public static object DatatypeConversion(string entry, string entryType, Type targetType, string translationRule,out string errorMessage)
        {
            errorMessage = "";
            object obj=null;

            if (targetType.Equals(typeof(DateTime)))
            {

                obj = (object)convStringToDate(entry.Trim(), translationRule);
            }

            if (targetType.Equals(typeof(int)))
            {

                obj  = (object) convStringToInt(entry);
            }

           
            if (targetType.Equals(typeof(decimal)))
            {
                
                obj = (object)convStringTodecimal(entry);
            }
            if (targetType.Equals(typeof(string)))
            {

                obj = (object)(entry);
            }
            return obj;
        }

        private static int convStringToInt(string stringToConvert)
        {
            int intRersult;
            return int.TryParse(stringToConvert.Trim(), out intRersult) ? (int)intRersult : 0;
        }

        private static decimal convStringTodecimal(string stringToConvert)
        {
            decimal intRersult;
            return Decimal.TryParse(stringToConvert.Trim(), out intRersult) ? (decimal)intRersult : 0;
        }

        private static DateTime ? convStringToDate(string stringToConvert, string dateFormat)
        {
                        
            DateTime? transformedDate = null;
            DateTime date;
            if (stringToConvert == null || stringToConvert.Trim().Length<2)
            {
                return transformedDate;
            }


            switch (dateFormat.ToUpper().Trim())
            {
                case "YYMMDD":
                    var newDate = DateTime.TryParseExact(stringToConvert.Trim(), "yyMMdd",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out date);
                    transformedDate = date;

                    break;
                case "YYDDD":
                    transformedDate = fromJulianToDate(stringToConvert);
                    break;
                case "YDDD":
                    string calcJulianDate = convToJulian(stringToConvert);
                    transformedDate = fromJulianToDate(calcJulianDate);
                    break;
                default:
         
                    break;
            }

            
            return transformedDate;
        }

        static private DateTime ? fromJulianToDate(string julianDate)
        {

            if (julianDate == null) return null;
            int jDate = Convert.ToInt32(julianDate.Trim());
            int day = jDate % 1000;
            int year = (jDate - day) / 1000;
            year += 2000;
            var date1 = new DateTime(year, 1, 1);
            return date1.AddDays(day - 1);
        }
        static private string convToJulian(string yDDD)
        {
            if (yDDD == null) return null;
            try
            {
                string currentYear = DateTime.Now.ToString("yy");
                int lastYearDigit = int.Parse(currentYear.Substring(1, 1));
                int yDDDYear = int.Parse(yDDD.Substring(0, 1));
                int yDDDDay = int.Parse(yDDD.Substring(1, 3).Trim());
                int calcYear = yDDDYear - lastYearDigit;
                if (yDDDYear > lastYearDigit)
                {
                    calcYear = 10 - yDDDYear;
                }
                calcYear = int.Parse(currentYear) - calcYear;
                return  calcYear.ToString() + yDDDDay.ToString();
                
            }
            catch { return null; }


        }
    }
}
