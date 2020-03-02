using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class AccountHeadTools
    {
        public static List<string> _LstCatagory = new List<string>();
        private static string msg = "";
        public static string _SalesReturnLedgerId;
        public static string _SalesLedgerId;
        public static string _PurchaseReturnLedgerId;
        public static string _PurchaseLedgerId;

        public static string _AccountsPayableID;
        public static string _AdvancePaymentID;
        public static string _InventoryAccountHeadId;
        public static string _ReceivableAccountHeadId;
        public static void SetCategory()
        {
            _LstCatagory.Add("None");
            _LstCatagory.Add("Supplier");
            _LstCatagory.Add("Customer");
            _LstCatagory.Add("Employee");
        }
        public static void AddAccountHeadCategory(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                cmbBox.Items.Clear();
            }
            if (_LstCatagory.IsValidList())
            {
                cmbBox.Items.AddRange(_LstCatagory.ToArray());
            }
        }
        public static float GetCurrentBalance(string ledgerid)
        {
            float currentbalance = 0f;
            string query = "Select CurrentBalance from LedgerStatus where LedgerID='" + ledgerid + "' and FinYearID='"+FinancialYearTools._YearID+"'" ;
                object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            try
            {
                currentbalance = float.Parse(obj.ToString());
            }
            catch (Exception) { }
            return currentbalance;
        }
        public static float GetBankBalance(string bankHeadID)
        {
            float dr = 0f, cr = 0f;
            string query = "Select SUM(Amount) from TransectionDetailsView where AccountHeadIdTo='" + bankHeadID 
                           + "' and TransectionType in('Income','Receipt','AdvanceReceipt')";
            object oDr = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            query = "Select SUM(Amount) from TransectionDetailsView where AccountHeadIdTo='" + bankHeadID 
                    + "' and TransectionType in('Expense','Payment','BillPayment','AdvancePayment')";
            object oCr = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            try
            {
                dr = float.Parse(oDr.ToString());
            }
            catch (Exception) { }
            try
            {
                cr = float.Parse(oCr.ToString());
            }
            catch (Exception) { }
            return dr - cr;
        }
        public static void GetAccountsPayableHeadID()
        {
            string query = "Select AccountHeadID from AccountHead where AccountHeadName='ACCOUNTS PAYABLE'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _AccountsPayableID = o.ToString();
            }
        }
        public static void GetAdvancePaymentID()
        {
            string query = "Select AccountHeadID from AccountHead where AccountHeadName='ADVANCE PAYMENT'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _AdvancePaymentID = o.ToString();
            }
        }
        public static float GetOpeningAmount(string accountHead)
        {
            string query = "Select OpeningAmount from AccountOpeningBalance where FinYearID=" + 
                           FinancialYearTools._YearID + " and AccountHeadID='" + accountHead + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query,out msg);
            if (o.ISValidObject())
            {
                try
                {
                    return float.Parse(o.ToString());
                }
                catch (Exception)
                {
                }
            }
            return 0f;
        }

        public static void GetSalesLedgerID()
        {
            string query = "Select LadgerID from LadgerMain where Category='SALES'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _SalesLedgerId = o.ToString();
            }
        }
        public static void GetPurchaseLedgerID()
        {
            string query = "Select LadgerID from LadgerMain where Category='Purchase'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _PurchaseLedgerId = o.ToString();
            }
        }
        public static   void GetSalesReturnLedgerID()
        {
            string query = "Select LadgerID from LadgerMain where Category='Sales_Return'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _SalesReturnLedgerId = o.ToString();
            }
        }
        public static void GetPurchaseReturnLedgerID()
        {
            string query = "Select LadgerID from LadgerMain where Category='Purchase_Return'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _PurchaseReturnLedgerId = o.ToString();
            }
        }


        public static void GetInventoryAccountHeadID()
        {
            string query = "Select AccountHeadID from AccountHead where HeadType='INVENTORY'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _InventoryAccountHeadId = o.ToString();
            }
        }
        public static void GetReceivableAccountHeadID()
        {
            string query = "Select AccountHeadID from AccountHead where HeadType='ACCOUNTS_RECEIVABLE'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                _ReceivableAccountHeadId = o.ToString();
            }
        }

    }
}

