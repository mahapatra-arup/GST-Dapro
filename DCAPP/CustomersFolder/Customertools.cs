using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class Customertools
    {
        static string msg = "";
        public static Dictionary<string, string> _DicCustomers = new Dictionary<string, string>();
        public static void GetCustomers()
        {
            _DicCustomers.Clear();
            string query = "SELECT LadgerID, TemplateName FROM LadgerMain where LadgerName<>'Cash' and Category='Customer' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string ID = item["LadgerID"].ToString();
                    string Name = item["TemplateName"].ToString();
                    _DicCustomers.Add(ID, Name);
                }
            }
        }
        public static bool AddCustomers(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicCustomers.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicCustomers, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
        public static string CustomerAddressAndPANValidation(string ledgerID)
        {
            string alertMsg = "";
            string query = "Select Address,State,PAN from Ledgers where LedgerID='" + ledgerID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                if (dt.Rows[0]["Address"].ToString().ISNullOrWhiteSpace())
                {
                    alertMsg = "Customer Address";
                }
                if (dt.Rows[0]["State"].ToString().ISNullOrWhiteSpace())
                {
                    alertMsg += ", state";
                }
                if (dt.Rows[0]["PAN"].ToString().ISNullOrWhiteSpace())
                {
                    alertMsg += ", PAN No. not found. Do you want to proceed anyway?";
                }
            }
            return alertMsg;
        }
    }
}
