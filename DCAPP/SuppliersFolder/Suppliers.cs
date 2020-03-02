using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class Supplier
    {
       static string msg = "";
        public static Dictionary<string, string> _DicSuppliers = new Dictionary<string, string>();
        public static void GetSuppliers()
        {
            _DicSuppliers.Clear();
            string query = "SELECT LadgerID, TemplateName FROM LadgerMain where Category='Supplier' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query,out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string ID = item["LadgerID"].ToString();
                    string Name = item["TemplateName"].ToString();
                    _DicSuppliers.Add(ID, Name);
                }
            }
        }
        public static bool AddSuppliers(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicSuppliers.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicSuppliers, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
    }
}
