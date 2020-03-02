using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
   public static  class UnitTools
    {
        public static Dictionary<string, string> _DicUnit = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetUnit()
        {
            _DicUnit.Clear();
            string query = "select ID,UnitShortName from unit order by UnitShortName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["UnitShortName"].ToString();

                    _DicUnit.Add(id, name);
                }
            }
        }
        public static bool AddUnit(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicUnit.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicUnit, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
    }
}
