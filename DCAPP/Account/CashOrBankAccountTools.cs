using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class CashOrBankAccountTools
    {
        public static string _CashAcHeadName = "";
        public static string _CashAcHeadID = "";
        public static string _AMCAccountHaedIDTo = "";
        public static string _InstallAccountHaedIDTo = "";
        public static string _SuportAccountHaedIDTo = "";
        public static string _TraningAccountHaedIDTo = "";
        public static string _serviceAccountHaedIDTo = "";

        public static Dictionary<string, string> _DicBankAccountHead = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCashAccountHead = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetBankAcHead()
        {
            _DicBankAccountHead.Clear();
            string query = "Select AccountHeadID,AccountHeadName from AccountHead " +
                          "Inner join GroupSubSub on AccountHead.GroupSubSubID=GroupSubSub.GroupSubSubID " +
                          " where GroupName='BANK_ACCOUNT'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string accountHeadID = row["AccountHeadID"].ToString();
                    string accountHeadName = row["AccountHeadName"].ToString();
                    _DicBankAccountHead.Add(accountHeadID, accountHeadName);
                }
            }
        }
        public static void ADDBankAccountHead(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicBankAccountHead.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicBankAccountHead, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        public static void GetCashAccount()
        {
            _DicCashAccountHead.Clear();
            string query = "Select AccountHeadID,AccountHeadName from AccountHead " +
                          "Inner join GroupSubSub on AccountHead.GroupSubSubID=GroupSubSub.GroupSubSubID " +
                          " where GroupName='CASH_IN_HAND'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    _CashAcHeadID = row["AccountHeadID"].ToString();
                    _CashAcHeadName = row["AccountHeadName"].ToString();
                    _DicCashAccountHead.Add(_CashAcHeadID, _CashAcHeadName);
                }
            }
        }
        public static void ADDCashAccount(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCashAccountHead.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCashAccountHead, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
            }
        }
        public static float OpeningBalance(string AcHeadId)
        {
            string query = "Select OpeningAmount from AccountOpeningBalance where FinYearID=" + FinancialYearTools._YearID + " " +
                           " and AccountHeadID='" + AcHeadId + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return float.Parse(obj.ToString());
            }
            return 0f;
        }
    }
}

