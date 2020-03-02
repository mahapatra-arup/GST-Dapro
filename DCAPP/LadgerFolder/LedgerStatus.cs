using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPRO
{
    static public class LedgerStatus
    {
        static string msg = "";
        public static string UpdateLedgerStatus(string drledgerid, string crledgerid, string amount, out string crquery2)
        {
            string  drquery1 = "";
           
                drquery1 = "Update LedgerStatus set CurrentBalance= " +
                         "(select (CurrentBalance+" + amount + ") as currentbal  from LedgerStatus " +
                         "where LedgerID='" + drledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "')  " +
                         "where LedgerID='" + drledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "' ";
          
                crquery2 = "Update LedgerStatus set CurrentBalance= " +
                         "(select (CurrentBalance-" + amount + ") as currentbal  from LedgerStatus " +
                         "where LedgerID='" + crledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "')  " +
                         "where LedgerID='" + crledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "' ";
           
            return drquery1;
        }

        /// <summary>
        /// Non Cash Ledgers
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double GetLedgerOpeningBalanceByDate(string ledgerId, string date)
        {
            double opening = GetLedgerOpeningBalanceByFinYear(ledgerId);
            double totOpening = 0d;
            string query = "Select (sum (Amount_Dr)-sum(Amount_Cr)) as balance from Transection " +
                           "where LedgerIdFrom='" + ledgerId + "' " +
                           "and Date between '" + FinancialYearTools._StartDate + "' and '" + date + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out totOpening);
            }
            return (totOpening + opening);
        }
        public static double GetLedgerOpeningBalanceByFinYear(string ledgerId)
        {
            double amount = 0d;
            string query = "Select OpeningBalance from LedgerStatus " +
                          "where LedgerID='" + ledgerId + "' and FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            return amount;
        }
    }
}
