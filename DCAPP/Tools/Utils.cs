using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;

namespace DAPRO
{
    public static class Utils
    {
        public static DataTable _DtStudentList = new DataTable();
        // private static string msg = "";

        public static string GetDBFormatDate(DateTime date)
        {
            try
            {
                string dateStr = String.Empty;
                dateStr = date.ToString("dd-MMM-yyyy");
                return dateStr;
            }
            catch (Exception)
            {

            }
            return null;

        }

        public static bool isNimeric(string txt, char c)
        {
            if (c == '\b')
            {
                return true;
            }
            else if (char.IsWhiteSpace(c))
            {
                return false;
            }
            else
            {
                try
                {
                    double dbl = Double.Parse(txt);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public static List<string> GetListFromDataTbale(DataTable dataTable)
        {

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    string val = dr[0].ToString();
                    lst.Add(val);
                }
                return lst;
            }
            return null;
        }

        public static int RoundX(Double value)
        {

            string str = value.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = str.Split('.');
            string c = parts[1].Substring(0);
            int num;
            int val = 0;
            try
            {
                num = int.Parse(c);
                val = int.Parse(parts[0]);
                if (num >= 5)
                {
                    val += 1;
                }

            }
            catch (Exception)
            {
                val = 0;
            }

            return val;

        }
        public static string GetMonthMMM(this int month)
        {
            switch (month)
            {
                case 01:
                    return "Jan";
                case 02:
                    return "Feb";
                case 03:
                    return "Mar";
                case 04:
                    return "Apr";
                case 05:
                    return "May";
                case 06:
                    return "Jun";
                case 07:
                    return "Jul";
                case 08:
                    return "Aug";
                case 09:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    break;
            }
            return null;
        }
        public static int GetMonthInNumber(this string monthName)
        {
            switch (monthName)
            {
                case "January":
                    return 01;
                case "February":
                    return 02;
                case "March":
                    return 03;
                case "April":
                    return 04;
                case "May":
                    return 05;
                case "June":
                    return 06;
                case "July":
                    return 07;
                case "August":
                    return 08;
                case "September":
                    return 09;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    break;
            }
            return 13;
        }
    }

    public static class StringExtenssion
    {
        public static string GetDBFormatString(this string strVal)
        {
            if (!strVal.ISNullOrWhiteSpace())
            {
                strVal = strVal.Replace("'", "\'");
                strVal = strVal.Replace("\"", "\"");
            }

            return strVal;
        }

        public static bool ISNullOrWhiteSpace(this string strVal)
        {

            if (strVal == null || string.IsNullOrEmpty(strVal.Trim()))
            {
                return true;
            }

            return false;
        }

        public static string toString(this double val)
        {
            if (OtherSettingTools._DecemalePlace == 0)
            {
                return OtherSettingTools._IsThousandSeparate ? val.ToString("N0") : val.ToString("0");
            }
            else if (OtherSettingTools._DecemalePlace == 2)
            {
                return OtherSettingTools._IsThousandSeparate ? val.ToString("N2") : val.ToString("0.00");
            }
            else
            {
                return OtherSettingTools._IsThousandSeparate ? val.ToString("N4") : val.ToString("0.0000");
            }
        }

        public static string toRound(this string val)
        {
            double dbl = 0d;
            try
            {
                dbl = double.Parse(val);
            }
            catch (Exception)
            {
                return null;
            }
            if (OtherSettingTools._DecemalePlace == 0)
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N0") : dbl.ToString("0");
            }
            else if (OtherSettingTools._DecemalePlace == 2)
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N2") : dbl.ToString("0.00");
            }
            else
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N4") : dbl.ToString("0.0000");
            }
        }

        public static string toRound(this object val)
        {
            double dbl = 0d;
            try
            {
                dbl = double.Parse(val.ToString());
            }
            catch (Exception)
            {
                return null;
            }
            if (OtherSettingTools._DecemalePlace == 0)
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N0") : dbl.ToString("0");
            }
            else if (OtherSettingTools._DecemalePlace == 2)
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N2") : dbl.ToString("0.00");

            }
            else
            {
                return OtherSettingTools._IsThousandSeparate ? dbl.ToString("N4") : dbl.ToString("0.0000");
            }
        }

        public static double toRound(this double val)
        {
            return Math.Round(val, OtherSettingTools._DecemalePlace);
        }
    }

    public static class DataTableExtension
    {
        public static bool IsValidDataTable(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidList(this List<string> lst)
        {
            if (lst != null && lst.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool ISValidObject(this object obj)
        {
            if (obj != null && !obj.ToString().ISNullOrWhiteSpace())
            {
                return true;
            }
            return false;
        }
        public static bool ISValidNumericObject(this object obj)
        {
            if (obj != null)
            {
                switch (Type.GetTypeCode(obj.GetType()))
                {
                    case TypeCode.Byte:
                        return true;
                    case TypeCode.SByte:
                        return true;
                    case TypeCode.UInt16:
                        return true;
                    case TypeCode.UInt32:
                        return true;
                    case TypeCode.UInt64:
                        return true;
                    case TypeCode.Int16:
                        return true;
                    case TypeCode.Int32:
                        return true;
                    case TypeCode.Int64:
                        return true;
                    case TypeCode.Decimal:
                        return true;
                    case TypeCode.Double:
                        return true;
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }
    }

    public static class DictionaryExtention
    {
        public static bool IsNullOrEmpty<T, U>(this IDictionary<T, U> Dictionary)
        {
            return (Dictionary == null || Dictionary.Count < 1);
        }
    }

    public static class DateTimeExtention
    {
        public static bool IsValidDateTime(this DateTime dt)
        {
            if (dt != null)
            {
                DateTime temp;
                if (DateTime.TryParse(dt.ToString(), out temp))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidDate(this DateTime dt)
        {
            if (dt.IsValidDateTime())
            {
                if (dt < FinancialYearTools._AccountDate)
                {
                    MessageBox.Show("Date cannot be below of account starting date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else if (dt > DateTime.Parse(FinancialYearTools._EndDate))
                {
                    MessageBox.Show("Date cannot be above of financial year ending date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }
    }
}
