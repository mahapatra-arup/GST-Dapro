using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class ComodityCodeTools
    {
        public static Dictionary<string, string> _DicHsn = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicSac = new Dictionary<string, string>();
        private static string msg = "";
        public static void GetHSNCode()
        {
            _DicHsn.Clear();
            string query = "Select id,Code,ComodityName from ComodityCode where Type='Goods' order by Code";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["Code"].ToString();
                    string code = item["ComodityName"].ToString();
                    _DicHsn.Add(id, code);
                }
            }
        }
        public static void GetSACCode()
        {
            _DicSac.Clear();
            string query = "Select id,Code,ComodityName from ComodityCode where Type='Service' order by Code";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["Code"].ToString();
                    string code = item["ComodityName"].ToString();
                    _DicSac.Add(id, code);
                }
            }
        }
        public static string GetComodityName(string Code)
        {
            string query = "Select ComodityName from ComodityCode where code='" + Code + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        public static bool AddComodityCode(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicHsn.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicHsn, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
        public static bool AddComodityName(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicHsn.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicHsn, null);
                cmb.DisplayMember = "Key";
                cmb.ValueMember = "Value";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
    }
}
