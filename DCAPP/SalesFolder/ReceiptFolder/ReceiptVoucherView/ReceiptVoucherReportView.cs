using DAPRO.ReceiptVoucherView;
using DAPRO.SalesFolder.ReceiptFolder.ReceiptVoucherView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class ReceiptVoucherReportView : Form
    {
        private string msg = "";
        string mTransectionId = string.Empty;
        DataSet _DT = new ReceiptVoucherView.DSReceiptVoucher();
        public ReceiptVoucherReportView(string transectionId)
        {
            InitializeComponent();
            mTransectionId = transectionId;
            PrintReceiptVoucherReport();
        }
        private void GetReceiptVoucherDetails()
        {
            //Clear Dataset table
            _DT.Tables["ReceiptVoucher"].Clear();

            string query = "SELECT     Transection.SlNo, Transection.TransectionID, convert(varchar(11),Transection.Date,106) as date, Transection.No, Transection.TransectionType, Transection.LedgerIdFrom, Transection.Amount_Dr, " +
            "Transection.Mode, Transection.BankName, Transection.ChequeNo,convert(varchar(11),Transection.ChequeDate,106) as ChequeDate, Transection.Narration,  CustomerView.LadgerName, CustomerView.Category, CustomerView.GSTRegistrationType, CustomerView.GSTIN,  " +
            "CustomerView.BillingName, CustomerView.BillingAddress, CustomerView.BillingTown, CustomerView.BillingDist, CustomerView.BillingState, CustomerView.BillingPIN " +
            "FROM         Transection INNER JOIN " +
            "CustomerView on CustomerView.LadgerID =Transection.LedgerIdTO " +
            "where Transection.TransectionID='" + mTransectionId + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string OrgName = ORG_Tools._OrganizationName;
                string OrgAddress = ORG_Tools._Address;
                string OrgGSTINNo = "GSTIN NO. :" + ORG_Tools._GSTin;
                string OrgEmail = ORG_Tools._Email;
                string OrgContectNo = ORG_Tools._ContactNo1;
                string CusmName = dt.Rows[0]["LadgerName"].ToString();
                string CusmAddress = dt.Rows[0]["BillingAddress"].ToString();
                string CusmGSTINNo = dt.Rows[0]["GSTIN"].ToString();
                string CusmNarration = dt.Rows[0]["Narration"].ToString();
                string CusmPaidMode = dt.Rows[0]["Mode"].ToString();
                string CusmChequeNo = dt.Rows[0]["ChequeNo"].ToString();
                string CusmChequeDate = dt.Rows[0]["ChequeDate"].ToString();
                string CusmChequeBank = dt.Rows[0]["BankName"].ToString();
                string ReceiptAmount = dt.Rows[0]["Amount_Dr"].ToString();
                string ReceiptDate = dt.Rows[0]["Date"].ToString();
                string VoucherNo = dt.Rows[0]["No"].ToString();
                _DT.Tables["ReceiptVoucher"].Rows.Add(OrgName, OrgAddress, OrgGSTINNo, OrgEmail, OrgContectNo, CusmName, CusmAddress,
                                     CusmGSTINNo, CusmNarration, CusmPaidMode, CusmChequeNo, CusmChequeDate, CusmChequeBank, ReceiptAmount, ReceiptDate, VoucherNo);
            }
        }
        private void PrintReceiptVoucherReport()
        {
            GetReceiptVoucherDetails();
            CrystalReportReceipVoucher crReceipVoucher = new CrystalReportReceipVoucher();
            crReceipVoucher.SetDataSource(_DT);
            crystalReportViewer1.ReportSource = crReceipVoucher;
        }
    }
}
