using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class SubAccountTools
    {
        public static Dictionary<string, string> _DicUnder = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetSubAccountForExpense()
        {
            _DicUnder.Clear();
            string query = "Select ID,AccountName from SubAccount " +
                           "where Nature in ('Expense','Assets','Liabilities') order by AccountName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["ID"].ToString();
                    string groupName = row["AccountName"].ToString();
                    _DicUnder.Add(id, groupName);
                }
            }
        }
        public static void AddSubAccountForExpense(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicUnder.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicUnder, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        public static string GetAccountParent(string account)
        {
            string query = "Select ParentAccount from SubAccount where AccountName='" + account + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        public static string GetAccountUnder(string account)
        {
            string query = "Select Nature from SubAccount where AccountName='" + account + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
    }
}
