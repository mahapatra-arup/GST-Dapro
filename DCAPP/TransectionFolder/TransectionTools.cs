using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAPRO
{ 
    public static class TransectionTools
    {
        private static string msg="";
        public static List<string> _mTransectionIdList = new List<string>();
        public static string _PaymentMethod = "";
        public static string _DRAccountTemplateName = "";
        public static string _CRAccountTemplateName = "";
        public static string _DRAccountLedgerId = "";
        public static string _CRAccountLedgerId = "";
        public static string _BankName = "";
        public static string _ChequeNo = "";
        public static string _ChequeDate = "";

      public static void GetTransectionId(string No,string TransectionType)
        {
            _mTransectionIdList.Clear();
            string query = "select TransectionID,SlNo FROM Transection where No='"+No+ "' and TransectionType='"+ TransectionType + "' order by slno asc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query,out msg);
            if (dt.IsValidDataTable())
            {
                _mTransectionIdList.Add(dt.Rows[0]["TransectionID"].ToString());
                _mTransectionIdList.Add(dt.Rows[1]["TransectionID"].ToString());
            }
        }
        public static void GetPaymentDetailsId(string transectionid)
        {
            string query = " select LedgerIdFrom,LedgerIdTo,Mode, ChequeNo,BankName, ChequeDate from transection where TransectionID='" + transectionid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _PaymentMethod = dt.Rows[0]["Mode"].ToString();
                _BankName = dt.Rows[0]["BankName"].ToString();
                _ChequeDate = dt.Rows[0]["ChequeDate"].ToString();
                _ChequeNo = dt.Rows[0]["ChequeNo"].ToString();
                _DRAccountLedgerId = dt.Rows[0]["LedgerIdFrom"].ToString();
                _CRAccountLedgerId = dt.Rows[0]["LedgerIdTo"].ToString();
                _DRAccountTemplateName = LedgerTools.GetTempleteName(_DRAccountLedgerId);
                _CRAccountTemplateName = LedgerTools.GetTempleteName(_CRAccountLedgerId);

            }
        }

    }
}
