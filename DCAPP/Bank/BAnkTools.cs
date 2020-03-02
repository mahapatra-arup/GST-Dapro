using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class BAnkTools
    {
        public static Dictionary<string, string> _DicBank = new Dictionary<string, string>();
        private static string msg = "";
        public static void GetBank()
        {
            _DicBank.Clear();
            string query = "select id,BankName from Bank order by BankName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string desig = item["BankName"].ToString();
                    _DicBank.Add(id, desig);
                }
            }
        }
        public static void AddBank(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicBank.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicBank, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
    }
}
