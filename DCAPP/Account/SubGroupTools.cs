using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class SubGroupTools
    {
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicUnder = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetIncomeExpenseGroup()
        {
            dictionary.Clear();
            string query = "Select SubGroupID,SubGroup from GroupSub where GroupID in (1,3) order by SubGroup";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["SubGroupID"].ToString();
                    string groupName = row["SubGroup"].ToString();
                    dictionary.Add(id, groupName);
                }
            }
        }
        public static void AddIncomeExpenseGroup(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!dictionary.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(dictionary, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }

        public static void GetUnder()
        {
            _DicUnder.Clear();
            string query = "Select GroupUnderID,GroupName from GroupUnder order by GroupName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["GroupUnderID"].ToString();
                    string groupName = row["GroupName"].ToString();
                    _DicUnder.Add(id, groupName);
                }
            }
        }
        public static void AddUnder(this ComboBox cmb)
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
    }
}
