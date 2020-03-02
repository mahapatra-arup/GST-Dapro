using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class StateTool
    {
        public static Dictionary<string, string> _DicState = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetState()
        {
            _DicState.Clear();
            string query = "Select StateCode,StateName from State order by StateName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["StateCode"].ToString();
                    string name = item["StateName"].ToString();

                    _DicState.Add(id, name);
                }
            }
        }
        public static bool AddState(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicState.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicState, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
    }
}
