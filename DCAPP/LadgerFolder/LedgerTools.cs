using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DAPRO
{
    public static class LedgerTools
    {
        public static Dictionary<string, string> _DicAllLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCustomer_SupplierLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicAccountLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCustomerLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicSupplierLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCashLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicBankLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCash_BankLedgers = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicOtherLedgers = new Dictionary<string, string>();
        public static string _CashLedgerId;

        static string msg = "";

        public static string LedgerNameById(string ledgerID)
        {
            string query = "Select TemplateName from LadgerMain where LadgerID='" + ledgerID + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        /// <summary>
        /// All Ledgers
        /// </summary>
        public static void GetAllLedgers()
        {
            _DicAllLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicAllLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDAllLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicAllLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicAllLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Party Ledgers
        /// </summary>
        public static void GetPartyLedgers()
        {
            _DicCustomer_SupplierLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category in('Customer','Supplier') order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicCustomer_SupplierLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void AddPartyLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCustomer_SupplierLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCustomer_SupplierLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Party Ledgers
        /// </summary>
        public static void GetAccountLedgers()
        {
            _DicAccountLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category in('Account') order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicAccountLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void AddAccountLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicAccountLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicAccountLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Customer Ledgers
        /// </summary>
        public static void GetCustomerLedgers()
        {
            _DicCustomerLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category='Customer' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicCustomerLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDCustomerLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCustomerLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCustomerLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Supplier Ledgers
        /// </summary>
        public static void GetSupplierLedgers()
        {
            _DicSupplierLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category='Supplier' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicSupplierLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDSupplierLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicSupplierLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicSupplierLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Cash Ledgers
        /// </summary>
        public static bool GetCashLedgers()
        {
            _DicCashLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category='Cash' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicCashLedgers.Add(ledgerID, ledgerName);
                }
                return true;
            }
            return false;
        }
        public static void ADDCashLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCashLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCashLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
            }
        }
        /// <summary>
        /// Bank Ledgers
        /// </summary>
        public static void GetBankLedgers()
        {
            _DicBankLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category='Bank' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicBankLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDBankLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicBankLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicBankLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Cash_Bank Ledgers
        /// </summary>
        public static void GetCash_BankLedgers()
        {
            _DicCash_BankLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category in ('Cash','Bank') order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicCash_BankLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDCash_BankLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCash_BankLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCash_BankLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Others Ledgers
        /// </summary>
        public static void GetOtherLedgers()
        {
            _DicOtherLedgers.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                           "where Category ='Others' order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicOtherLedgers.Add(ledgerID, ledgerName);
                }
            }
        }
        public static void ADDOtherLedgers(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicOtherLedgers.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicOtherLedgers, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Expense & Assets Ledgers
        /// </summary>
        public static Dictionary<string, string> _DicExpense_Assets = new Dictionary<string, string>();
        public static void GetAccountHeadForExpense()
        {
            _DicExpense_Assets.Clear();
            string query = "Select LadgerID,TemplateName from LadgerMain " +
                            "where Under in ('Cash','Bank') order by TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerName = row["TemplateName"].ToString();
                    string ledgerID = row["LadgerID"].ToString();
                    _DicExpense_Assets.Add(ledgerID, ledgerName);
                }
            }
        }
        public static bool AddAccountHeadForExpense(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicExpense_Assets.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicExpense_Assets, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
        /// <summary>
        /// End
        /// </summary>
        public static string GetTempleteName(string ledgerID)
        {
            string query = "Select TemplateName from LadgerMain where LadgerID='" + ledgerID + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        public static void GetReceivableAccountHeadID()
        {
            string query = "Select LadgerID from LadgerMain where LadgerName='CASH'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _CashLedgerId = o.ToString();
            }
        }
        public static void GetLadgerBillingDetails(string ladgerId, out string billingName, out string billingAddress, out string billingStateName, out string billingStateCOde, out string gstNo)
        {
            billingName = "";
            billingAddress = ""; billingStateName = ""; billingStateCOde = ""; gstNo = "";
            string query = "SELECT * FROM CustomerView where LadgerID='" + ladgerId + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                billingName = dt.Rows[0]["BillingName"].ToString();
                string address = dt.Rows[0]["BillingAddress"].ToString();
                string town = dt.Rows[0]["BillingTown"].ToString();
                string dist = dt.Rows[0]["BillingDist"].ToString();
                string pin = dt.Rows[0]["BillingPIN"].ToString();
                string state = dt.Rows[0]["BillingState"].ToString();
                string mobileNo = dt.Rows[0]["Mobile"].ToString();
                string cno = dt.Rows[0]["Phone"].ToString();
                billingStateName = state;
                billingStateCOde = !state.ISNullOrWhiteSpace() ? StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString() : "";
                mobileNo = mobileNo.ISNullOrWhiteSpace() ? cno : mobileNo;
                gstNo = dt.Rows[0]["GSTIN"].ToString();

                billingAddress = address;
                billingAddress = billingAddress + ((!town.ISNullOrWhiteSpace()) ? "\n" + town : "");
                billingAddress = billingAddress + ", \n" + dist + ", " + pin;
                billingAddress = billingAddress + ((!mobileNo.ISNullOrWhiteSpace()) ? "\n" + mobileNo : "");
            }
        }
        public static void GetLadgerShippingDetails(string ladgerId, out string billingName, out string billingAddress, out string billingStateName, out string billingStateCOde, out string gstNo)
        {
            billingName = "";
            billingAddress = ""; billingStateName = ""; billingStateCOde = ""; gstNo = "";
            string query = "SELECT * FROM CustomerView where LadgerID='" + ladgerId + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                billingName = dt.Rows[0]["ShippingName"].ToString();
                string address = dt.Rows[0]["ShippingAddress"].ToString();
                string town = dt.Rows[0]["ShippingTown"].ToString();
                string dist = dt.Rows[0]["ShippingDist"].ToString();
                string pin = dt.Rows[0]["ShippingPIN"].ToString();
                string state = dt.Rows[0]["ShippingState"].ToString();
                string cno = dt.Rows[0]["ShippingContactNo"].ToString();
                billingStateName = state;
                billingStateCOde = !state.ISNullOrWhiteSpace() ? StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString() : "";
                gstNo = dt.Rows[0]["GSTIN"].ToString();

                billingAddress = address;
                billingAddress = billingAddress + ((!town.ISNullOrWhiteSpace()) ? "\n" + town : "");
                billingAddress = billingAddress + ", \n" + dist + ", " + pin;
                billingAddress = billingAddress + ((!cno.ISNullOrWhiteSpace()) ? "\n" + cno : "");
            }
        }
        public static string GetBillingTerms(string ledgerID)
        {
            string query = "Select BillingTerms from Ledgers where LedgerID='" + ledgerID + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
    }
}
