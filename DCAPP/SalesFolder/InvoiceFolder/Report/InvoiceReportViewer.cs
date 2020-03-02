using DAPRO.SalesFolder.InvoiceFolder.Report;
using System;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class InvoiceReportViewer : Form
    {
        DataSet _DtSet = new DSChallenInvoice();
        private string msg = "";
        public event Action OnClose;
        public enum _InvoiceCopy
        {
            Original,
            Duplicate,
            Triplicate
        }
        public enum _InvoiceType
        {
            CGST_SGST_WO_Cess,
            CGST_SGST_W_Cess,
            IGST_WO_Cess,
            IGST_O_Cess,
            BillOfSupply
        }

        private _InvoiceCopy mInvoiceCopy;
        private string mInvoiceCopyText;
        private _InvoiceType mInvoiceType;
        private bool isCess = false;
        private string mInvoiceNo;

        public InvoiceReportViewer(_InvoiceCopy copy, string invoiceNo)
        {
            InitializeComponent();
            this.FitToVertical();
            mInvoiceNo = invoiceNo;
            mInvoiceCopy = copy;
            switch (mInvoiceCopy)
            {
                case _InvoiceCopy.Original:
                    mInvoiceCopyText = "Original for Recipient";
                    break;
                case _InvoiceCopy.Duplicate:
                    mInvoiceCopyText = "Duplicate for Transporter";
                    break;
                case _InvoiceCopy.Triplicate:
                    mInvoiceCopyText = "Triplicate for Supplier";
                    break;
            }
            PreparetoPrintInvoice();
        }
        private void PreparetoPrintInvoice()
        {
            GenerateRegularTaxInvoice();
            switch (mInvoiceType)
            {
                case _InvoiceType.CGST_SGST_WO_Cess:
                    CRInvoiceCS_WOCess frmCR = new CRInvoiceCS_WOCess();
                    frmCR.SetDataSource(_DtSet);
                    //***********************Set report footer Print At Bottom Of Page***********************
                    string sectionname = frmCR.ReportDefinition.Sections[frmCR.ReportDefinition.Sections.Count - 2].Name;
                    frmCR.ReportDefinition.Sections[sectionname].SectionFormat.EnablePrintAtBottomOfPage = INVOICE_TOOLS._IsFooterBottomOfthePage;
                    CRViewer.ReportSource = frmCR;
                    break;
                case _InvoiceType.CGST_SGST_W_Cess:
                    CRInvoiceSC_WCess frmCRInvoiceSC_WCess = new CRInvoiceSC_WCess();
                    frmCRInvoiceSC_WCess.SetDataSource(_DtSet);
                    //***********************Set report footer Print At Bottom Of Page***********************
                    string sectionname1 = frmCRInvoiceSC_WCess.ReportDefinition.Sections[frmCRInvoiceSC_WCess.ReportDefinition.Sections.Count - 2].Name;
                    frmCRInvoiceSC_WCess.ReportDefinition.Sections[sectionname1].SectionFormat.EnablePrintAtBottomOfPage = INVOICE_TOOLS._IsFooterBottomOfthePage;
                    CRViewer.ReportSource = frmCRInvoiceSC_WCess;
                    break;
                case _InvoiceType.IGST_WO_Cess:
                    TaxInvoiceIGST_WOCess frmTaxInvoiceIGST_WOCess = new TaxInvoiceIGST_WOCess();
                    frmTaxInvoiceIGST_WOCess.SetDataSource(_DtSet);
                    //***********************Set report footer Print At Bottom Of Page***********************
                    string sectionname2 = frmTaxInvoiceIGST_WOCess.ReportDefinition.Sections[frmTaxInvoiceIGST_WOCess.ReportDefinition.Sections.Count - 2].Name;
                    frmTaxInvoiceIGST_WOCess.ReportDefinition.Sections[sectionname2].SectionFormat.EnablePrintAtBottomOfPage = INVOICE_TOOLS._IsFooterBottomOfthePage;
                    CRViewer.ReportSource = frmTaxInvoiceIGST_WOCess;
                    break;
                case _InvoiceType.IGST_O_Cess:
                    break;
                case _InvoiceType.BillOfSupply:
                    InvoiceBillOfSupply frmInvoiceBillOfSupply = new InvoiceBillOfSupply();
                    frmInvoiceBillOfSupply.SetDataSource(_DtSet);
                    //***********************Set report footer Print At Bottom Of Page***********************
                    string sectionname3 = frmInvoiceBillOfSupply.ReportDefinition.Sections[frmInvoiceBillOfSupply.ReportDefinition.Sections.Count - 2].Name;
                    frmInvoiceBillOfSupply.ReportDefinition.Sections[sectionname3].SectionFormat.EnablePrintAtBottomOfPage = INVOICE_TOOLS._IsFooterBottomOfthePage;
                    CRViewer.ReportSource = frmInvoiceBillOfSupply;
                    break;
                default:
                    break;
            }
        }
        private void InvoiceReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void GenerateRegularTaxInvoice()
        {
            _DtSet.Tables["ItemDetails"].Rows.Clear();
            _DtSet.Tables["InvoiceDetailsFooter"].Rows.Clear();
            _DtSet.Tables["InvoiceDetailsAmounts"].Rows.Clear();
            _DtSet.Tables["InvoiceDetailsHeader"].Rows.Clear();

            string query = "SELECT  SlNo, InvoiceNo, convert(varchar(11),InvoiceDate,106) as InvoiceDate, " +
                           "LedgerId, BillingTerms, convert(varchar(11),DueDate,106) as DueDate, " +
                           "BillingTo, BillingAddress, BillingGSTNO, BillingState, BillingStateCode, BuyerOrderNo, " +
                           "convert(varchar(11),BuyerOrderDate,106) as BuyerOrderDate, ChallanNo, " +
                           "convert(varchar(11),ChallanDate,106) as ChallanDate, DispatchThrough, VehiclaNo, " +
                           "ShippingTo,ShippingAddress, ShippingState, ShippingStateCode,TotalQty, TotalAmount, " +
                           "TotalDiscount, TotalTaxAmount, TotalCGST, TotalSGST, TotalIGST, TotalCess, NetAmount, " +
                           "FreightChargs,PackingCharges, OtherCharges, OverAllDiscount,TotalInvoiceAmount, Note,RCM,InvoiceType from Invoice " +
                           "where InvoiceNo = '" + mInvoiceNo + "'";
            DataTable dt0 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt0.IsValidDataTable())
            {
                string orgName = "", orgAddress = "", orgLegalNos = "", orgStateCode = "", orgState = "";
                ORG_Tools.GetORGDetails(out orgName, out orgAddress, out orgLegalNos, out orgStateCode, out orgState);
                _DtSet.Tables["InvoiceDetailsHeader"].Rows.Add(orgName, orgAddress, orgStateCode, orgState, orgLegalNos,
                              ORG_Tools._LogoByte, dt0.Rows[0]["InvoiceNo"], dt0.Rows[0]["InvoiceDate"], dt0.Rows[0]["ChallanNo"],
                              dt0.Rows[0]["ChallanDate"], dt0.Rows[0]["BuyerOrderNo"], dt0.Rows[0]["BuyerOrderDate"],
                              dt0.Rows[0]["BillingTerms"], dt0.Rows[0]["DueDate"], dt0.Rows[0]["DispatchThrough"],
                              dt0.Rows[0]["VehiclaNo"], dt0.Rows[0]["BillingTo"], dt0.Rows[0]["BillingAddress"],
                              dt0.Rows[0]["BillingGSTNO"], dt0.Rows[0]["BillingStateCode"], dt0.Rows[0]["BillingState"],
                              dt0.Rows[0]["ShippingTo"], dt0.Rows[0]["ShippingAddress"], dt0.Rows[0]["ShippingStateCode"],
                              dt0.Rows[0]["ShippingState"], mInvoiceCopyText);
                //Billing and shipped details
                _DtSet.Tables["InvoiceDetailsAmounts"].Rows.Add(orgName, dt0.Rows[0]["TotalQty"], dt0.Rows[0]["TotalDiscount"],
                              dt0.Rows[0]["TotalAmount"], dt0.Rows[0]["TotalTaxAmount"], dt0.Rows[0]["TotalCGST"],
                              dt0.Rows[0]["TotalSGST"], dt0.Rows[0]["TotalIGST"], dt0.Rows[0]["TotalCess"],
                              dt0.Rows[0]["NetAmount"], dt0.Rows[0]["TotalInvoiceAmount"], dt0.Rows[0]["FreightChargs"], dt0.Rows[0]["PackingCharges"],
                              dt0.Rows[0]["OtherCharges"], dt0.Rows[0]["OverAllDiscount"], dt0.Rows[0]["RCM"]);

                ///Footer
                _DtSet.Tables["InvoiceDetailsFooter"].Rows.Add(orgName, INVOICE_TOOLS._DeclarationText, "",
                             INVOICE_TOOLS._OrgBankDetailsText, INVOICE_TOOLS._OrgTermsAndConditionText,
                             INVOICE_TOOLS._SignatureForText, INVOICE_TOOLS._SignatureAuthorityText);
                /////invoice item details
                query = "SELECT   * from InvoiceDetails where InvoiceNo='" + mInvoiceNo + "'";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        object cess = item["CeassAmount"];
                        _DtSet.Tables["ItemDetails"].Rows.Add(orgName, item["ItemName"], item["HSNCode"], item["Quantity"],
                                     item["Unit"], item["Rate"], item["Amount"], item["DiscountRate"], item["DiscountAmount"],
                                     item["TaxAmount"], item["CGSTRate"], item["CGSTAmount"], item["SGSTRate"], item["SGSTAmount"],
                                     item["IGSTRate"], item["IGSTAmount"], item["CessRate"], cess, item["Total"]);
                        if (cess.ISValidObject() && !isCess)
                        {
                            isCess = true;
                        }
                    }
                }
                ///Clarify Invoice type
                #region Invoice Type
                string invoiceType = dt0.Rows[0]["InvoiceType"].ToString();
                string placeOfsuppllyState = dt0.Rows[0]["BillingState"].ToString();
                if (invoiceType == "Regular")
                {
                    if (ORG_Tools._State == placeOfsuppllyState)
                    {
                        if (isCess)
                        {
                            mInvoiceType = _InvoiceType.CGST_SGST_W_Cess;
                        }
                        else
                        {
                            mInvoiceType = _InvoiceType.CGST_SGST_WO_Cess;
                        }
                    }
                    else
                    {
                        if (isCess)
                        {
                            mInvoiceType = _InvoiceType.IGST_O_Cess;
                        }
                        else
                        {
                            mInvoiceType = _InvoiceType.IGST_WO_Cess;
                        }
                    }
                }
                else
                {
                    mInvoiceType = _InvoiceType.BillOfSupply;
                }
                #endregion
                //Organization details
            }
        }
    }
}
