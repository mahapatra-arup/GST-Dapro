using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class FinancialYearTools
    {
        public static string _YearID = "", _YearName = "", _StartDate = "", _EndDate = "";
        public static DateTime _AccountDate;
        public static Dictionary<string, string> _DicFyear = new Dictionary<string, string>();
        private static string msg = "";
        public static void GetYear()
        {
            _DicFyear.Clear();
            string query = "select id,FinancialYearName from FinancialYear order by FinancialYearName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string YearName = item["FinancialYearName"].ToString();
                    _DicFyear.Add(id, YearName);
                }
            }
        }
        public static void Addyear(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicFyear.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicFyear, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        public static void GetFinancialYearDetails()
        {
            string query = "Select ID, FinancialYearName, convert(varchar(11),StartingDate,106) as StartingDate, " +
                           "convert(varchar(11),EndingDate,106) as EndingDate " +
                           "from FinancialYear where CurentFyear='1'";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);

            if (dt.IsValidDataTable())
            {
                _YearID = dt.Rows[0]["id"].ToString();
                _YearName = dt.Rows[0]["FinancialYearName"].ToString();
                _StartDate = dt.Rows[0]["StartingDate"].ToString();
                _EndDate = dt.Rows[0]["EndingDate"].ToString();
            }
        }
        public static bool ISAccountDateExist()
        {
            string query = "Select convert(varchar(11),BookStart,106) as BookStart " +
                           "from FinancialYear where CurentFyear='1'";

            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);

            if (o.ISValidObject())
            {
                if (DateTime.TryParse(o.ToString(), out _AccountDate))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
