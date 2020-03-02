using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class BillingTermTools
    {
        public static Dictionary<string, string> _DicBillingTerms = new Dictionary<string, string>();
        private static string msg = "";
        public static void GetBillingTerms()
        {
            _DicBillingTerms.Clear();
            string query = "Select ID,TermsName from BillingTerms order by ID";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["TermsName"].ToString();

                    _DicBillingTerms.Add(id, name);
                }
            }
        }
        public static bool AddBillingTerms(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicBillingTerms.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicBillingTerms, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
        public static int GetBillingTermsAndDueDay(string ledgerID, out string terms)
        {
            terms = "";
            string query = "Select BillingTerms.TermsName,BillingTerms.Days from Ledgers " +
                           "inner join BillingTerms on Ledgers.BillingTerms=BillingTerms.TermsName " +
                           "where Ledgers.LedgerID='" + ledgerID + "'";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string dayStr = dt.Rows[0]["Days"].ToString();
                terms = dt.Rows[0]["TermsName"].ToString();
                return dayStr.ISNullOrWhiteSpace() ? 0 : int.Parse(dayStr);
            }
            return 0;
        }
    }
}
