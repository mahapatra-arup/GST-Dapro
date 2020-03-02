using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class EmployeeDesignationTools
    {
        public static Dictionary<string, string> _DicDesignation = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetDesignation()
        {
            _DicDesignation.Clear();
            string query = "select id,DesignationName from EmployeeDesignation order by DesignationName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string desig = item["DesignationName"].ToString();
                    _DicDesignation.Add(id, desig);
                }
            }
        }

        public static bool AddDesignation(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                try
                {
                    (cmb.DataSource as BindingSource).Clear();
                }
                catch (Exception)
                {

                    
                }
            }
            if (!_DicDesignation.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicDesignation, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
              cmb.SelectedIndex = -1;
                return true;
            }
            return false;
        }
       
    }
}
