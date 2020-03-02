using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class CashCustomersTools
    {
       static string msg = "";
        public static Dictionary<string, string> _DicCashCustomer = new Dictionary<string, string>();
        public static void GetCashCustomers()
        {
            _DicCashCustomer.Clear();
            string query = "SELECT LadgerID, TemplateName FROM LadgerMain where Category='Customer' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query,out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string ID = item["LadgerID"].ToString();
                    string Name = item["TemplateName"].ToString();
                    _DicCashCustomer.Add(ID, Name);
                }
            }
        }
        public static bool AddCashCustomers(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicCashCustomer.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicCashCustomer, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
    }
}
