using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAPRO
{
    public static class QryInvoice
    {
        public static string msg = string.Empty;
        public static void GetOrganizationDetails(ref DataTable dt)
        {
            string query = "SELECT * FROM OrganizationDetails  " +
            "Inner join State on State.Id=OrganizationDetails.StateID";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
        }
        public static void GetLadgerShippingDetails(ref DataTable dt, string ladgerId)
        {
            string query = "SELECT Challan.*,State.StateCode  FROM Challan " +
            "Inner join State on State.StateName=Challan.ShippingState " +
            "where Challan.LedgerID='" + ladgerId + "'";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
        }
        public static void GetLadgerBillingDetails( string ladgerId,out string billingName,out string billingAddress,out string billingStateName,out string billingStateCOde,out string gstNo)
        {
            billingName = "";
            billingAddress = "";billingStateName = "";billingStateCOde = "";gstNo = "";
            string query = "SELECT Customers.*,LadgerMain.LadgerName,LadgerMain.GSTIN FROM LadgerMain " +
                           "inner join Ledgers on LadgerMain.LadgerID=Ledgers.LedgerID " +
                           "inner join Customers on LadgerMain.LadgerID=Customers.LedgerID " +
                           "where Customers.LedgerID='" + ladgerId + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                billingName = dt.Rows[0]["BillingName"].ToString();
                string address = dt.Rows[0]["BillingAddress"].ToString();
                string town = dt.Rows[0]["BillingTown"].ToString();
                string dist = dt.Rows[0]["BillingDist"].ToString();
                string pin = dt.Rows[0]["BillingPIN"].ToString();
                string state = dt.Rows[0]["BillingState"].ToString();
                billingStateName =state;
                billingStateCOde = StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString();
               
                gstNo = dt.Rows[0]["GSTIN"].ToString();

                billingAddress = address + ", \n" + town;
                billingAddress = billingAddress + ", \n" + dist + ", " + pin;
            }
        }
        public static void GetItemDetails(ref DataTable dt, string _ChallenId)
        {
            string query = "Select * from ChallanDettails Where Challan.ChallanID='" + _ChallenId + "'";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
        }
    }
}
