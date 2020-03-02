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
    public partial class CreditNoteIssue : Form
    {
        public event Action OnClose;
        string mIdOrNo = "", mledgerid = "", msg = "", mBillID = "NULL", mDocumentytype, mNoteidforedit, mplaceofsupply, mgstinno, mquery = "";
        long mCreditNoteSlNo = 1, mTotalQuantity, mmaxqty;
        int mDescriptionSlno = 1;
        bool mIsIGst = false, mGstType = ORG_Tools._IsRegularGST;
        private double mTotalCrNoteAmount, mTotalAmount, mTotalDiscount, mTotalCGST,
                       mTotalSGST, mTotalPreviousAmount = 0d, mMaxAmount, mCurrentBalance = 0d, mTotalIGST, mtotalInvoiceAmount, mTotalCESS, mTaxableAmount, mTotalWithTax;
        List<string> mlistquery = new List<string>();
        List<string> mBatchList = new List<string>();
        string mTransectionID = "";
        DataTable mdtFroRestore = new DataTable();
        public enum _NoteType
        {
            Credit_Note,
            Debit_Note,
            Refund_Voucher
        }
        public _NoteType mNoteType;
        public CreditNoteIssue()
        {
            InitializeComponent();
            this.FitToVertical();
        }
        public CreditNoteIssue(_NoteType notetype, string NoOrid)
        {
            InitializeComponent();
            this.FitToVertical();
            mNoteType = notetype;
            mIdOrNo = NoOrid;
            GenerateNoteNo();
            cmbNoteType.Text = mNoteType.ToString();
            GenerateGridForNonGSTType();
            GridDesign();
            cmbUnit.AddUnit();
            btnAdd.Enabled = false;
            txtReason.Enabled = false;
            txtQuantity.Enabled = false;

        }
        public CreditNoteIssue(string noteid, string status, string unuse)
        {
            InitializeComponent();
            this.FitToVertical();
            mNoteidforedit = noteid;
            cmbUnit.AddUnit();
            InitTable();
            DataRetriveFromCreditNote();
            GridDesign();
            btnAdd.Enabled = false;
            txtReason.Enabled = false;
            txtQuantity.Enabled = false;
            ReadOnlyAllControl(status);
        }
        private void ReadOnlyAllControl(string status)
        {
            if (status == "Close")
            {
                foreach (Control item in this.Controls)
                {
                    item.Enabled = false;
                }
            }
            btnCancel.Enabled = true;
        }
        private void InitTable()
        {
            //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct
            mdtFroRestore.Columns.Add("StockSummaryId", typeof(string));
            mdtFroRestore.Columns.Add("ItemId", typeof(string));
            mdtFroRestore.Columns.Add("Qty", typeof(string));
            mdtFroRestore.Columns.Add("Unit", typeof(string));
            mdtFroRestore.Columns.Add("hUnit", typeof(string));
            mdtFroRestore.Columns.Add("hQty", typeof(string));
            mdtFroRestore.Columns.Add("mUnit", typeof(string));
            mdtFroRestore.Columns.Add("mQty", typeof(string));
            mdtFroRestore.Columns.Add("lUnit", typeof(string));
            mdtFroRestore.Columns.Add("lQty", typeof(string));
            mdtFroRestore.Columns.Add("IsRightProduct", typeof(string));

        }
        private void DataRetriveFromCreditNote()
        {
            string query = "Select NoteNo,LastTransecetionID,PurchaseBillId,DocumentType, "+
                           "InvoiceOrADRNo,AdvanceReciptNo,InvoiceOrADRDate,RefundDate,NoteValue, "+
                           "IsPreGstCrNote from CDRNote where NoteID='" + mNoteidforedit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblNoteNo.Text = dt.Rows[0]["NoteNo"].ToString();
                string invoiceno = dt.Rows[0]["InvoiceOrADRNo"].ToString();
                string invoicedatestr = dt.Rows[0]["InvoiceOrADRDate"].ToString();
                lblInvoiceDate.Text = invoicedatestr.ISNullOrWhiteSpace() ? "" : DateTime.Parse(invoicedatestr).ToString("dd-MMM-yyyy");
                dtmCrNoteDate.Text = dt.Rows[0]["RefundDate"].ToString();
                lblTotalCrNoteAmount.Text = dt.Rows[0]["NoteValue"].toRound();
                mTotalPreviousAmount = double.Parse(dt.Rows[0]["NoteValue"].ToString());
                string purchassbillid = dt.Rows[0]["PurchaseBillId"].ToString();
                string advanceReciptNo = dt.Rows[0]["AdvanceReciptNo"].ToString();
                chkPregst.Checked = dt.Rows[0]["IsPreGstCrNote"].ToString() == "Yes" ? true : false;
                string notetype = dt.Rows[0]["DocumentType"].ToString();
                mIdOrNo = notetype == "C" ? invoiceno : notetype == "D" ? purchassbillid : advanceReciptNo;
                cmbNoteType.Text = notetype == "C" ? "Credit_Note" : notetype == "D" ? "Debit_Note" : "Refund_Voucher";
                if (notetype == "R")
                {
                    mTransectionID = dt.Rows[0]["LastTransecetionID"].ToString();
                    TransectionTools.GetPaymentDetailsId(mTransectionID);
                    cmbPaymentMethod.Text = TransectionTools._PaymentMethod;
                    cmbPaymentAccount.Text = TransectionTools._CRAccountTemplateName;
                    txtChequeNo.Text = TransectionTools._ChequeNo;
                    dtpDateCheque.Text = TransectionTools._ChequeDate;
                    cmbPaymentAccount_SelectedIndexChanged(null, null);
                }
            }
            DataRetriveFromCrNoteDetails();
        }
        private void DataRetriveFromCrNoteDetails()
        {
            dgvItemListForCrNote.Rows.Clear();
            string query = "select * from CDRNoteDetails where NoteID='" + mNoteidforedit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mDescriptionSlno = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string comodityCode = item["HSNCode"].ToString();
                    string itemID = item["ItemID"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string qtyStr = item["Quantity"].ToString();
                    string unit = item["Unit"].ToString();
                    string rateStr = item["Rate"].toRound();
                    string amountStr = item["Amount"].toRound();

                    object disRateStr = item["DiscountRate"];
                    string disRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString()).ToString("0.00") : "";
                    object disAmountStr = item["DiscountAmount"];
                    string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString()).ToString("0.00") : "";

                    string taxAmountStr = item["TaxAmount"].ToString();
                    string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString()).ToString("0.00") : "";

                    object cgstRateStr = item["CGSTRate"];
                    string cgstRate = cgstRateStr.ISValidObject() ? double.Parse(cgstRateStr.ToString()).ToString("0.00") : "";
                    object cgstAmountStr = item["CGSTAmount"];
                    string cgstAmount = cgstAmountStr.ISValidObject() ? double.Parse(cgstAmountStr.ToString()).ToString("0.00") : "";

                    object sgstRateStr = item["SGSTRate"];
                    string sgstRate = sgstRateStr.ISValidObject() ? double.Parse(sgstRateStr.ToString()).ToString("0.00") : "";
                    object sgstAmountStr = item["SGSTAmount"];
                    string sgstAmount = sgstAmountStr.ISValidObject() ? double.Parse(sgstAmountStr.ToString()).ToString("0.00") : "";

                    object igstRateStr = item["IGSTRate"];
                    string igstRate = igstRateStr.ISValidObject() ? double.Parse(igstRateStr.ToString()).ToString("0.00") : "";
                    object igstAmountStr = item["IGSTAmount"];
                    string igstAmount = igstAmountStr.ISValidObject() ? double.Parse(igstAmountStr.ToString()).ToString("0.00") : "";

                    object cessrateStr = item["CessRate"];
                    string cessrate = cessrateStr.ISValidObject() ? double.Parse(cessrateStr.ToString()).ToString("0.00") : "";
                    object cessAmountStr = item["CeassAmount"];
                    string cessAmount = cessAmountStr.ISValidObject() ? double.Parse(cessAmountStr.ToString()).ToString("0.00") : "";

                    object totalAmountStr = item["Total"];
                    string totalAmount = totalAmountStr.ISValidObject() ? double.Parse(totalAmountStr.ToString()).ToString("0.00") : "";

                    double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);
                    double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);

                    object reasonstr = item["reason"];
                    string reason = reasonstr.ISValidObject() ? reasonstr.ToString() : "";

                    string StockSummaryId = item["StockSummaryID"].ToString();
                    string isRightProduct = item["IsRightProduct"].ToString();
                    string reasonType = item["ReasonType"].ToString();
                    //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                    mdtFroRestore.Rows.Add(StockSummaryId, itemID, qtyStr, unit, "", "", "", "", "", "", isRightProduct);
                    mBatchList.Add(itemID+StockSummaryId);
                    dgvItemListForCrNote.Rows.Add(mDescriptionSlno, StockSummaryId, itemID, itemName, comodityCode, qtyStr, unit,
                                         rate.toString(), amount.toString(), disRate, disAmount.toRound(), taxAmount.toRound()
                                         , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                                         , cessrate, cessAmount, totalAmount.toRound(), reasonType, reason, isRightProduct);
                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                    dgvItemListForCrNote.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
                }
                GenerateTotal();
                if (cmbNoteType.Text != "Refund_Voucher")
                {
                    GetUnitDetails();
                }
            }
        }
        private void GetUnitDetails()
        {
            //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

            int i = 0;
            foreach (DataRow item in mdtFroRestore.Rows)
            {
                string summaryid = item["StockSummaryId"].ToString();
                string itemid = item["ItemId"].ToString();
                string qtystr = item["Qty"].ToString();
                string unit = item["Unit"].ToString();

                string query = "Select  HighestUnit, MiddleUnit, LowestUnit,HighestMeasureQty, " +
                    "LowestMeasureQty from StockSummary where ItemID='" +
                    itemid + "' and Id=" + summaryid + "";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {

                    string highestUnit = dt.Rows[0]["HighestUnit"].ToString();
                    string middleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                    string lowestUnit = dt.Rows[0]["LowestUnit"].ToString();
                    string highrtmesurestr = dt.Rows[0]["HighestMeasureQty"].ToString();
                    string lowestMesurestr = dt.Rows[0]["LowestMeasureQty"].ToString();
                    double highrtmesure = 0d, lowestMesure = 0d, qty;
                    double.TryParse(highrtmesurestr, out highrtmesure);
                    double.TryParse(lowestMesurestr, out lowestMesure);
                    double.TryParse(qtystr, out qty);
                    if (highrtmesure == 0)
                    {
                        mdtFroRestore.Rows[i]["hQty"] = qty;
                        mdtFroRestore.Rows[i]["hUnit"] = highestUnit;
                    }
                    else
                    {
                        if (lowestMesure == 0)
                        {
                            if (unit == highestUnit)
                            {
                                mdtFroRestore.Rows[i]["hQty"] = qty;
                                mdtFroRestore.Rows[i]["hUnit"] = highestUnit;
                                mdtFroRestore.Rows[i]["mQty"] = qty * highrtmesure;
                                mdtFroRestore.Rows[i]["mUnit"] = middleUnit;
                            }
                            if (unit == middleUnit)
                            {
                                mdtFroRestore.Rows[i]["mQty"] = qty;
                                mdtFroRestore.Rows[i]["mUnit"] = middleUnit;
                                mdtFroRestore.Rows[i]["hQty"] = qty / highrtmesure;
                                mdtFroRestore.Rows[i]["hUnit"] = middleUnit;
                            }
                        }
                        else
                        {
                            if (unit == highestUnit)
                            {
                                mdtFroRestore.Rows[i]["hQty"] = qty;
                                mdtFroRestore.Rows[i]["hUnit"] = highestUnit;
                                mdtFroRestore.Rows[i]["mQty"] = qty * highrtmesure;
                                mdtFroRestore.Rows[i]["mUnit"] = middleUnit;
                                mdtFroRestore.Rows[i]["lQty"] = qty * highrtmesure * lowestMesure;
                                mdtFroRestore.Rows[i]["lUnit"] = lowestUnit;
                            }
                            if (unit == middleUnit)
                            {
                                mdtFroRestore.Rows[i]["mQty"] = qty;
                                mdtFroRestore.Rows[i]["mUnit"] = middleUnit;
                                mdtFroRestore.Rows[i]["hQty"] = qty / highrtmesure;
                                mdtFroRestore.Rows[i]["hUnit"] = middleUnit;
                                mdtFroRestore.Rows[i]["lQty"] = qty * lowestMesure;
                                mdtFroRestore.Rows[i]["lUnit"] = lowestUnit;
                            }
                            if (unit == lowestUnit)
                            {
                                mdtFroRestore.Rows[i]["lQty"] = qty;
                                mdtFroRestore.Rows[i]["lUnit"] = lowestUnit;
                                mdtFroRestore.Rows[i]["mQty"] = qty / lowestMesure;
                                mdtFroRestore.Rows[i]["mUnit"] = middleUnit;
                                mdtFroRestore.Rows[i]["hQty"] = qty / lowestMesure / highrtmesure;
                                mdtFroRestore.Rows[i]["hUnit"] = middleUnit;

                            }

                        }
                    }

                }
                i++;
            }
        }
        private void GenerateNoteNo()
        {
            string query = "";
            string NoteNoText = "";
            switch (mNoteType)
            {
                case _NoteType.Credit_Note:
                    query = "Select max(SlNo) as slno from CDRNote Where DocumentType = 'C' ";
                    NoteNoText = OtherSettingTools._CreditNoteStart;
                    mCreditNoteSlNo = OtherSettingTools._CreditNoteSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._CreditNoteSerialStart);

                    break;
                case _NoteType.Debit_Note:
                    query = "Select max(SlNo) as slno from CDRNote where DocumentType = 'D'";
                    NoteNoText = OtherSettingTools._DebitNoteStart;
                    mCreditNoteSlNo = OtherSettingTools._DebitNoteSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._DebitNoteSerialStart);
                    break;
                case _NoteType.Refund_Voucher:
                    query = "Select max(SlNo) as slno from CDRNote where DocumentType = 'R'";
                    NoteNoText = OtherSettingTools._RefundVoucherStart;
                    mCreditNoteSlNo = OtherSettingTools._RefundVoucherSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._RefundVoucherSerialStart);
                    break;
                default:
                    break;
            }
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                try
                {
                    mCreditNoteSlNo = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblNoteNo.Text = NoteNoText + mCreditNoteSlNo.ToString();
        }
        private void GetBillsAndSupplierDetails()
        {
            string query = "select ladgermain.ladgerid,ladgermain.GSTIN,ladgermain.GSTRegistrationType,PurchaseBill.InvoiceNo,PurchaseBill.TotalAmount,PurchaseBill.InvoiceDate,ledgers.State from PurchaseBill" +
                " inner join ladgermain on PurchaseBill.LedgerId=ladgermain.ladgerid" +
                "  inner join Ledgers on PurchaseBill.LedgerId=Ledgers.ledgerid Where  BillID='" + mIdOrNo + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mledgerid = dt.Rows[0]["ladgerid"].ToString();
                mtotalInvoiceAmount = double.Parse(dt.Rows[0]["TotalAmount"].ToString());
                lblInvoiceDate.Text = DateTime.Parse(dt.Rows[0]["InvoiceDate"].ToString()).ToString("dd-MMM-yyyy");
                lblInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                string placeofsupply = dt.Rows[0]["State"].ToString();
                string gstType = dt.Rows[0]["GSTRegistrationType"].ToString();

                mgstinno = dt.Rows[0]["GSTIN"].ToString();
                string statecode = StateTool._DicState.FirstOrDefault(x => x.Value == placeofsupply).Key.ToString();
                mplaceofsupply = statecode + "-" + placeofsupply;
                mIsIGst = (ORG_Tools._StateCode == statecode.ToString() ? false : true);
                mGstType = (gstType == "Regular" ? true : false);
            }

        }
        private void GetInVoiceAndCustomerBillingDetails()
        {
            string query = "select LedgerId,BillingGSTNO,InvoiceNo,TotalInvoiceAmount,InvoiceDate,BillingState,BillingStateCode from Invoice Where  InvoiceNo='" + mIdOrNo + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mledgerid = dt.Rows[0]["LedgerId"].ToString();
                mtotalInvoiceAmount = double.Parse(dt.Rows[0]["TotalInvoiceAmount"].ToString());
                lblInvoiceDate.Text = DateTime.Parse(dt.Rows[0]["InvoiceDate"].ToString()).ToString("dd-MMM-yyyy");
                lblInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                string placeofsupply = dt.Rows[0]["BillingState"].ToString();
                mgstinno = dt.Rows[0]["BillingGSTNO"].ToString();
                string statecode = dt.Rows[0]["BillingStateCode"].ToString();
                mplaceofsupply = statecode + "-" + placeofsupply;
                mIsIGst = (ORG_Tools._StateCode == statecode.ToString() ? false : true);
            }

        }
        private void GenerateGridForNonGSTType()
        {
            if (mGstType)
            {
                dgvItemListForCrNote.Columns["CESSRATECN"].Visible = true;
                dgvItemListForCrNote.Columns["CESSAMOUNTCN"].Visible = true;
                dgvItemlistfrominvoice.Columns["CESSRATE"].Visible = true;
                dgvItemlistfrominvoice.Columns["CESSAMOUNT"].Visible = true;
                if (mIsIGst)
                {
                    dgvItemlistfrominvoice.Columns["CGSTRATE"].Visible = false;
                    dgvItemlistfrominvoice.Columns["CGSTAMOUNT"].Visible = false;
                    dgvItemlistfrominvoice.Columns["SGSTRATE"].Visible = false;
                    dgvItemlistfrominvoice.Columns["SGSTAMOUNT"].Visible = false;
                    dgvItemlistfrominvoice.Columns["IGSTRATE"].Visible = true;
                    dgvItemlistfrominvoice.Columns["IGSTAMOUNT"].Visible = true;

                    dgvItemListForCrNote.Columns["CGSTRATECN"].Visible = false;
                    dgvItemListForCrNote.Columns["CGSTAMOUNTCN"].Visible = false;
                    dgvItemListForCrNote.Columns["SGSTRATECN"].Visible = false;
                    dgvItemListForCrNote.Columns["SGSTAMOUNTCN"].Visible = false;
                    dgvItemListForCrNote.Columns["IGSTRATECN"].Visible = true;
                    dgvItemListForCrNote.Columns["IGSTAMOUNTCN"].Visible = true;

                    lblTotalIGST.BringToFront();
                    lblTotalCGST.Text = "";
                }
                else
                {
                    dgvItemlistfrominvoice.Columns["CGSTRATE"].Visible = true;
                    dgvItemlistfrominvoice.Columns["CGSTAMOUNT"].Visible = true;
                    dgvItemlistfrominvoice.Columns["SGSTRATE"].Visible = true;
                    dgvItemlistfrominvoice.Columns["SGSTAMOUNT"].Visible = true;
                    dgvItemlistfrominvoice.Columns["IGSTRATE"].Visible = false;
                    dgvItemlistfrominvoice.Columns["IGSTAMOUNT"].Visible = false;

                    dgvItemListForCrNote.Columns["CGSTRATECN"].Visible = true;
                    dgvItemListForCrNote.Columns["CGSTAMOUNTCN"].Visible = true;
                    dgvItemListForCrNote.Columns["SGSTRATECN"].Visible = true;
                    dgvItemListForCrNote.Columns["SGSTAMOUNTCN"].Visible = true;
                    dgvItemListForCrNote.Columns["IGSTRATECN"].Visible = false;
                    dgvItemListForCrNote.Columns["IGSTAMOUNTCN"].Visible = false;
                    lblTotalSGST.BringToFront();
                    //lblTotalCGST.Text = "---";
                }
            }
            else
            {
                dgvItemListForCrNote.Columns["CGSTRATECn"].Visible = false;
                dgvItemListForCrNote.Columns["CGSTAMOUNTcn"].Visible = false;
                dgvItemListForCrNote.Columns["SGSTRATEcn"].Visible = false;
                dgvItemListForCrNote.Columns["SGSTAMOUNTcn"].Visible = false;
                dgvItemListForCrNote.Columns["IGSTRATEcn"].Visible = false;
                dgvItemListForCrNote.Columns["IGSTAMOUNTcn"].Visible = false;
                dgvItemListForCrNote.Columns["CESSRATEcn"].Visible = false;
                dgvItemListForCrNote.Columns["CESSAMOUNTcn"].Visible = false;
                dgvItemListForCrNote.Columns["TAXABLEVALUEcn"].Visible = false;

                dgvItemlistfrominvoice.Columns["CGSTRATE"].Visible = false;
                dgvItemlistfrominvoice.Columns["CGSTAMOUNT"].Visible = false;
                dgvItemlistfrominvoice.Columns["SGSTRATE"].Visible = false;
                dgvItemlistfrominvoice.Columns["SGSTAMOUNT"].Visible = false;
                dgvItemlistfrominvoice.Columns["IGSTRATE"].Visible = false;
                dgvItemlistfrominvoice.Columns["IGSTAMOUNT"].Visible = false;
                dgvItemlistfrominvoice.Columns["CESSRATE"].Visible = false;
                dgvItemlistfrominvoice.Columns["CESSAMOUNT"].Visible = false;
                dgvItemlistfrominvoice.Columns["TAXABLEVALUE"].Visible = false;

                lblTotalCGST.Text = "";
                lblTotalSGST.Text = "";
                lblTotalIGST.Text = "";
                lblTotalCESS.Text = "";
            }
        }
        private void dgvItemList_Paint(object sender, PaintEventArgs e)
        {

            #region Assign Array
            string[] array = new string[4];
            int[] ary = new int[4];
            int length = 0;
            if (mGstType)
            {
                if (mIsIGst)
                {
                    length = 3;
                    array[0] = "DISCOUNT";
                    array[1] = "IGST";
                    array[2] = "CESS";

                    ary[0] = 9;
                    ary[1] = 16;
                    ary[2] = 18;
                }
                else
                {
                    length = 4;
                    array[0] = "DISCOUNT";
                    array[1] = "CGST";
                    array[2] = "SGST";
                    array[3] = "CESS";

                    ary[0] = 9;
                    ary[1] = 12;
                    ary[2] = 14;
                    ary[3] = 18;

                }

            }
            else
            {
                length = 1;
                array[0] = "DISCOUNT";
                ary[0] = 9;

            }
            #endregion
            //string[] array = { "DISCOUNT", "CGST", "SGST", "IGST", "CESS" };//
            //int[] ary = { 8, 12, 14, 16, 18 };//
            for (int i = 0; i < length; i++)
            {

                //Column Location And Hight ANd Width;
                DataGridViewCell hc = dgvItemlistfrominvoice.Columns[ary[i]].HeaderCell;
                Rectangle hcRct = dgvItemlistfrominvoice.GetCellDisplayRectangle(hc.ColumnIndex, -1, true);

                //For column wise create Width means how many column are span;
                int multiHeaderWidth = dgvItemlistfrominvoice.Columns[hc.ColumnIndex].Width + dgvItemlistfrominvoice.Columns[hc.ColumnIndex + 1].Width;
                //Rectengle x,y location and Hight and Width Set;
                Rectangle headRct = new Rectangle(hcRct.Left, 2, multiHeaderWidth, dgvItemlistfrominvoice.ColumnHeadersHeight / 2);

                SizeF sz = e.Graphics.MeasureString(array[i], dgvItemlistfrominvoice.Font);
                int headerTop = Convert.ToInt32((headRct.Height / 2) - (sz.Height / 2)) + 3;
                //Rectengle clor;
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), headRct);
                ////border draw
                e.Graphics.DrawRectangle(Pens.LightGray, headRct);
                //For Text Design and location;
                e.Graphics.DrawString(array[i], new Font("Microsoft Sans Serif", 8f), Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
                dgvItemlistfrominvoice.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8f);
                dgvItemlistfrominvoice.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7f);

                // e.Graphics.DrawString(array[i], dgvitemList.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
            }
        }
        private void Dgvcrnote_Paint(object sender, PaintEventArgs e)
        {

            #region Assign Array
            string[] array = new string[4];
            int[] ary = new int[4];
            int length = 0;
            if (mGstType)
            {
                if (mIsIGst)
                {
                    length = 3;
                    array[0] = "DISCOUNT";
                    array[1] = "IGST";
                    array[2] = "CESS";

                    ary[0] = 9;
                    ary[1] = 16;
                    ary[2] = 18;
                }
                else
                {
                    length = 4;
                    array[0] = "DISCOUNT";
                    array[1] = "CGST";
                    array[2] = "SGST";
                    array[3] = "CESS";

                    ary[0] = 9;
                    ary[1] = 12;
                    ary[2] = 14;
                    ary[3] = 18;

                }

            }
            else
            {
                length = 1;
                array[0] = "DISCOUNT";
                ary[0] = 9;

            }
            #endregion
            //string[] array = { "DISCOUNT", "CGST", "SGST", "IGST", "CESS" };//
            //int[] ary = { 8, 12, 14, 16, 18 };//
            for (int i = 0; i < length; i++)
            {

                //Column Location And Hight ANd Width;
                DataGridViewCell hc = dgvItemListForCrNote.Columns[ary[i]].HeaderCell;
                Rectangle hcRct = dgvItemListForCrNote.GetCellDisplayRectangle(hc.ColumnIndex, -1, true);

                //For column wise create Width means how many column are span;
                int multiHeaderWidth = dgvItemListForCrNote.Columns[hc.ColumnIndex].Width + dgvItemListForCrNote.Columns[hc.ColumnIndex + 1].Width;
                //Rectengle x,y location and Hight and Width Set;
                Rectangle headRct = new Rectangle(hcRct.Left, 2, multiHeaderWidth, dgvItemListForCrNote.ColumnHeadersHeight / 2);

                SizeF sz = e.Graphics.MeasureString(array[i], dgvItemListForCrNote.Font);
                int headerTop = Convert.ToInt32((headRct.Height / 2) - (sz.Height / 2)) + 3;
                //Rectengle clor;
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), headRct);
                ////border draw
                e.Graphics.DrawRectangle(Pens.LightGray, headRct);
                //For Text Design and location;
                e.Graphics.DrawString(array[i], new Font("Microsoft Sans Serif", 8f), Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
                dgvItemListForCrNote.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8f);
                dgvItemListForCrNote.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7f);

                // e.Graphics.DrawString(array[i], dgvitemList.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
            }
        }
        private void GridDesign()
        {
            dgvItemlistfrominvoice.Columns["SlNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["ItemName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["ParticularsHsnCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["QTY"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["UNIT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["RATE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["TOTALAMOUNT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["TAXABLEVALUE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemlistfrominvoice.Columns["TotalWithTax"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvItemListForCrNote.Columns["SlNoCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["ItemNameCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["ParticularsHsnCodeCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["QTYCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["UNITCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["RATECN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["TOTALAMOUNTCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["TAXABLEVALUECN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["TotalWithTaxCN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["Reason"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["Reasontype"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemListForCrNote.Columns["btnDelete"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46)
            {
                if (txtAmount.Text.Length < 1 && e.KeyChar == '.')
                {
                    e.Handled = true;
                    txtAmount.Text = "0.";
                    txtAmount.SelectionStart = txtAmount.Text.Length;
                }
                else
                {
                    if (e.KeyChar == '.' && txtAmount.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        string currentamountstr = txtAmount.Text + e.KeyChar;
                        double currentamount;
                        double.TryParse(currentamountstr, out currentamount);
                        e.Handled = mMaxAmount > currentamount ? false : true;
                    }
                }
                if (e.KeyChar == 8)
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void CreditNoteIssue_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbPaymentMethod.Text == "Cash")
            {
                cmbPaymentAccount.ADDCashLedgers();
                pnlChequeDetails.Visible = false;
            }
            else if (cmbPaymentMethod.Text == "Cheque")
            {
                cmbPaymentAccount.ADDBankLedgers();
                pnlChequeDetails.Visible = true;
            }
            else
            {
                cmbPaymentAccount.ADDBankLedgers();
                pnlChequeDetails.Visible = false;
            }
            if (cmbPaymentAccount.Items.Count > 0)
            {
                cmbPaymentAccount.SelectedIndex = 0;
            }
        }
        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string ledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                float balance = AccountHeadTools.GetCurrentBalance(ledgerid);
                if (cmbPaymentAccount.Text == TransectionTools._CRAccountTemplateName)
                {
                }
                mCurrentBalance = balance;
            }
        }
        private void dgvItemListForCrNote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemListForCrNote.SelectedCells.Count > 0 && e.RowIndex >= 0)
            {
                if (dgvItemListForCrNote.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    dgvItemListForCrNote.Rows.RemoveAt(e.RowIndex);
                    mBatchList.RemoveAt(e.RowIndex);
                    GenerateTotal();
                }
            }
        }
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
        }
        private void AutoComplteSource()
        {
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            txtReason.AutoCompleteCustomSource = acsc;
            string query = "select Reason from CDRNoteDetails";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string reason = item["Reason"].ToString();
                    acsc.Add(reason);
                }
            }
        }

        private void cmbNoteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grbPayment.Visible = false;
            if (!cmbNoteType.Text.ISNullOrWhiteSpace())
            {
                if (cmbNoteType.Text == "Credit_Note")
                {
                    pnlBatch.Visible = false;
                    GetInVoiceAndCustomerBillingDetails();
                    InvoiceAndPurchaseDetailsDataRetrive(mIdOrNo);
                    mDocumentytype = "C";
                    AutoComplteSource();
                }
                else if (cmbNoteType.Text == "Debit_Note")
                {
                    GetBillsAndSupplierDetails();
                    InvoiceAndPurchaseDetailsDataRetrive(mIdOrNo);
                    mDocumentytype = "D";
                    mBillID = "'" + mIdOrNo + "'";
                    AutoComplteSource();

                }
                else
                {
                    pnlBatchandrightProduct.Visible = false;
                    grbPayment.Visible = true;
                    cmbPaymentMethod.SelectedIndex = 0;
                    AdvanceReceiptDataRetrive();
                    txtAmount.Cursor = Cursors.IBeam;
                    pnlinvoice.Hide();
                    mDocumentytype = "R";
                    AutoComplteSource();

                }
                GenerateGridForNonGSTType();
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            mBatchList.Clear();
            btnAdd.Enabled = false;
            txtReason.Enabled = false;
            txtQuantity.Enabled = false;
            txtAmount.Enabled = false;
            dgvItemListForCrNote.Rows.Clear();
            mDescriptionSlno = 1;
            mTotalCrNoteAmount = 0f;
            lblItemName.Text = "-----";
            txtReason.Text = "";
            txtQuantity.Text = "";
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            cmbUnit.SelectedIndex = -1;
            txtRate.Clear();
            lblTotQuantity.Text = "----";
            lblTotAmount.Text = "----";
            lblTotalWithTax.Text = "----";
            lblTotalCrNoteAmount.Text = "----";
            lblTotalIGST.Text = "----";
            lblTotalDiscount.Text = "----";
            lblTotalCESS.Text = "----";
            lblTaxableAmountTotal.Text = "----";
            lblTotalCGST.Text = "----";
            lblTotalSGST.Text = "----";
            txtAmount.Clear();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
                Datasave();
            }
        }
        private void Datasave()
        {
            mlistquery.Clear();
            string noteid = Guid.NewGuid().ToString();
            string transectionid = Guid.NewGuid().ToString();

            string noteno = lblNoteNo.Text.GetDBFormatString();
            string refunddate = dtmCrNoteDate.Text.GetDBFormatString();
            string invoiceno = "'" + lblInvoiceNo.Text.GetDBFormatString() + "'";
            string invoicedate = "'" + lblInvoiceDate.Text.GetDBFormatString() + "'";
            string documenttype = mDocumentytype;
            string pregst = "NO";
            string receiptno = "NULL";
            string lasttranscetionid = "NULL";
            if (cmbNoteType.Text == "Refund_Voucher")
            {
                invoiceno = "NULL";
                invoicedate = "NULL";
                receiptno = "'" + mIdOrNo + "'";
                lasttranscetionid = "'" + transectionid + "'";

            }
            if (chkPregst.Checked)
            {
                pregst = "Yes";
            }
            string totalnotevaluestr = lblTotalCrNoteAmount.Text.GetDBFormatString();


            if (mNoteidforedit.ISNullOrWhiteSpace())
            {
                mquery = "insert into CDRNote(NoteID,SlNo,NoteNo," +
                "GSTIN,InvoiceOrADRNo," +
                "InvoiceOrADRDate,RefundDate," +
                "DocumentType,PlaceOfSupply,NoteValue,IsPreGstCrNote,InvoiceValue,PurchaseBillId,LedgerId,AdvanceReciptNo,DueAmount,Status,LastTransecetionID) values('" + noteid + "'," + mCreditNoteSlNo + ",'" + noteno +
                "','" + mgstinno + "'," + invoiceno + "," + invoicedate
                + ",'" + refunddate + "','" + documenttype + "','" + mplaceofsupply + "'," + totalnotevaluestr + ",'" + pregst + "'," + mtotalInvoiceAmount + "," + mBillID + ",'" + mledgerid + "'," + receiptno + ", " + totalnotevaluestr + ",'Open'," + lasttranscetionid + ")";
                mlistquery.Add(mquery);
            }
            else
            {
                mquery = "update CDRNote set RefundDate='" + refunddate + "',NoteValue=" + totalnotevaluestr + ",IsPreGstCrNote='" + pregst + "',DueAmount='"+ totalnotevaluestr + "' where noteid='" + mNoteidforedit + "'";
                mlistquery.Add(mquery);
                mquery = "delete from CDRNoteDetails where noteid='" + mNoteidforedit + "'";
                mlistquery.Add(mquery);
                noteid = mNoteidforedit;
            }

            SaveCrNoteDetails(noteid);
            string drledgerid = "";
            string crledgerid = "";
            string transectiontype = "";
            if (cmbNoteType.Text == "Credit_Note")
            {
                transectiontype = "Credit_Note";
                drledgerid = AccountHeadTools._SalesReturnLedgerId;
                crledgerid = mledgerid;
                InsertOrUpdateTransection(transectionid, refunddate, noteno, totalnotevaluestr, drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
                #region UpdateLedgerStatus
                if (!mNoteidforedit.ISNullOrWhiteSpace())
                {
                    //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                    foreach (DataRow item in mdtFroRestore.Rows)
                    {
                        string itemid = item["ItemId"].ToString();
                        string stockSummaryId = item["StockSummaryId"].ToString();
                        string quantity = item["Qty"].ToString();
                        string unit = item["Unit"].ToString();
                        bool isright = bool.Parse(item["IsRightProduct"].ToString());
                        if (isright)
                        {
                            mlistquery.Add(UpdateStockSummaryForDebitNote(itemid, stockSummaryId, quantity, unit));
                        }
                        else
                        {
                            UpDateDamageProduct(itemid, stockSummaryId, quantity, unit);
                        }
                    }


                    #region CurrentBalanceRestore

                    mlistquery.Add(LedgerStatus.UpdateLedgerStatus(crledgerid, drledgerid, mTotalPreviousAmount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistquery.Add(mquery);
                    #endregion

                   
                }
                #region CurrentBalanceUpdate

                mlistquery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalnotevaluestr, out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistquery.Add(mquery);
                #endregion
                #endregion

            }
            else if (cmbNoteType.Text == "Debit_Note")
            {

                transectiontype = "Debit_Note";
                drledgerid = mledgerid;
                crledgerid = AccountHeadTools._PurchaseReturnLedgerId;
                InsertOrUpdateTransection(transectionid, refunddate, noteno, totalnotevaluestr, drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
                #region UpdateLedgerStatus
                if (!mNoteidforedit.ISNullOrWhiteSpace())
                {
                    foreach (DataRow item in mdtFroRestore.Rows)
                    {
                        string itemid = item["ItemId"].ToString();
                        string stockSummaryId = item["StockSummaryId"].ToString();
                        string quantity = item["Qty"].ToString();
                        string unit = item["Unit"].ToString();
                        bool isright = bool.Parse(item["IsRightProduct"].ToString());
                        if (isright)
                        {
                            mlistquery.Add(UpdateStockSummaryForCreditNote(itemid, stockSummaryId, quantity, unit));
                        }
                        else
                        {
                            UpDateDamageProduct(itemid, stockSummaryId, quantity, unit);
                        }
                    }
                    #region CurrentBalanceRestore

                    mlistquery.Add(LedgerStatus.UpdateLedgerStatus(crledgerid, drledgerid, mTotalPreviousAmount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistquery.Add(mquery);
                    #endregion
                }
                #region CurrentBalanceUpdate

                mlistquery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalnotevaluestr, out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistquery.Add(mquery);
                #endregion
                #endregion
            }
            else
            {
                transectiontype = "Refund_Voucher";
                string mode = "'" + cmbPaymentMethod.Text + "'";
                string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
                string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
                drledgerid = mledgerid;
                crledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                InsertOrUpdateTransection(transectionid, refunddate, noteno, totalnotevaluestr, drledgerid, crledgerid, transectiontype, mode, "NULL", checkno, checkdate);
                #region UpdateLedgerStatus
                if (!mNoteidforedit.ISNullOrWhiteSpace())
                {
                    #region CurrentBalanceRestore

                    mlistquery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mTotalPreviousAmount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistquery.Add(mquery);
                    #endregion
                }
                #region CurrentBalanceUpdate

                mlistquery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalnotevaluestr, out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistquery.Add(mquery);
                #endregion
                #endregion

                double totalnotevalu;
                double.TryParse(totalnotevaluestr, out totalnotevalu);
                mquery = "update AdvanceReceiptVoucher set DueAmount='" + (mMaxAmount - totalnotevalu) + "' where ReceiptNo='" + mIdOrNo + "'";
                mlistquery.Add(mquery);
            }
            if (SQLHelper.GetInstance().ExecuteTransection(mlistquery, out msg))
            {
                MessageBox.Show("Note successfully Genarate.", "Credit Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (cmbNoteType.Text == "Credit_Note") { OtherSettingTools._IsCreditNoteBillgenarate = true; }
                else if (cmbNoteType.Text == "Debit_Note") { OtherSettingTools._IsDebitNoteBillgenarate = true; }
                else { OtherSettingTools._IsRefundVoucharBillgenarate = true; }
                this.Close();
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStockSumaryId.Text = "";
            if (!cmbBatchNo.Text.ISNullOrWhiteSpace())
            {
                string query = "select id from stocksummary where batchno='" + cmbBatchNo.Text.GetDBFormatString() + "'";
                object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
                if (obj.ISValidObject())
                {
                    lblStockSumaryId.Text = obj.ToString();
                }
            }
        }

        private void InsertOrUpdateTransection(string tranid, string date, string no, string totalamount, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate)
        {
            string transectionid = tranid;
            if (mNoteidforedit.ISNullOrWhiteSpace())
            {
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        date + "','" + no + "','" + transectiontype + "','" + drledgerid + "','" +
                        crledgerid + "'," + totalamount + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mlistquery.Add(mquery);
                transectionid = Guid.NewGuid().ToString();
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        date + "','" + no + "','" + transectiontype + "','" + crledgerid + "','" +
                        drledgerid + "'," + totalamount + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mlistquery.Add(mquery);
            }
            else
            {
                TransectionTools.GetTransectionId(no, transectiontype);

                mquery = "Update Transection Set Date='" + date + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalamount + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mlistquery.Add(mquery);

                mquery = "Update Transection Set Date='" + date + "',LedgerIdFrom='" + crledgerid + "', " +
                       "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalamount + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mlistquery.Add(mquery);

            }

        }
        private void SaveCrNoteDetails(string noteid)
        {
            foreach (DataGridViewRow row in dgvItemListForCrNote.Rows)
            {
                object itemIdobj = row.Cells["ItemIdcn"].Value;
                string itemId = itemIdobj.ISValidObject() ? itemIdobj.ToString() : "NULL";

                object hsnCodeobj = row.Cells["ParticularsHsnCodecn"].Value;
                string hsnCode = hsnCodeobj.ISValidObject() ? "'" + hsnCodeobj.ToString() + "'" : "NULL";

                object itemNameobj = row.Cells["ItemNamecn"].Value;
                string itemName = itemNameobj.ISValidObject() ? "'" + itemNameobj.ToString() + "'" : "NULL";

                object stockSummaryIdobj = row.Cells["stockSummaryidcn"].Value;
                string stockSummaryId = stockSummaryIdobj.ISValidObject() ? stockSummaryIdobj.ToString() : "NULL";


                object quantityobj = row.Cells["QTYcn"].Value;
                string quantity = quantityobj.ISValidObject() ? quantityobj.ToString() : "NULL";


                string rateStr = row.Cells["RATEcn"].Value.ToString();
                double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);

                string amountStr = row.Cells["TOTALAMOUNTcn"].Value.ToString();
                double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);

                string unit = row.Cells["UNITcn"].Value.ToString();

                object disRateStr = row.Cells["DISCOUNTRATEcn"].Value;
                string disRate = !disRateStr.ISValidObject() ? "NULL" : disRateStr.ToString();
                object disAmountStr = row.Cells["DISCOUNTAMOUNTcn"].Value;
                string disAmount = !disAmountStr.ISValidObject() ? "NULL" : disAmountStr.ToString();

                string taxAmountStr = row.Cells["TAXABLEVALUEcn"].Value.ToString();
                double taxAmount = taxAmountStr.ISNullOrWhiteSpace() ? 0f : double.Parse(taxAmountStr);

                object cgstRateStr = row.Cells["CGSTRATEcn"].Value;
                string cgstRate = !cgstRateStr.ISValidObject() ? "NULL" : cgstRateStr.ToString();
                object cgstAmountStr = row.Cells["CGSTAMOUNTcn"].Value.ToString();
                string cgstAmount = !cgstAmountStr.ISValidObject() ? "NULL" : cgstAmountStr.ToString();

                object sgstRateStr = row.Cells["SGSTRATEcn"].Value;
                string sgstRate = !sgstRateStr.ISValidObject() ? "NULL" : sgstRateStr.ToString();
                object sgstAmountStr = row.Cells["SGSTAMOUNTcn"].Value;
                string sgstAmount = !sgstAmountStr.ISValidObject() ? "NULL" : sgstAmountStr.ToString();

                object igstRateStr = row.Cells["IGSTRATEcn"].Value;
                string igstRate = !igstRateStr.ISValidObject() ? "NULL" : igstRateStr.ToString();
                object igstAmountStr = row.Cells["IGSTAMOUNTcn"].Value;
                string igstAmount = !igstAmountStr.ISValidObject() ? "NULL" : igstAmountStr.ToString();

                object cessRateStr = row.Cells["CESSRATEcn"].Value;
                string cessRate = !cessRateStr.ISValidObject() ? "NULL" : cessRateStr.ToString();
                object cessAmountStr = row.Cells["CESSAMOUNTcn"].Value;
                string cessAmount = !cessAmountStr.ISValidObject() ? "NULL" : cessAmountStr.ToString();

                object netAmountStr = row.Cells["TotalWithTaxcn"].Value;
                string netAmount = !netAmountStr.ISValidObject() ? "NULL" : netAmountStr.ToString();

                object reasontypeobj = row.Cells["ReasonType"].Value;
                string reasontypestr = !reasontypeobj.ISValidObject() ? "NULL" : "'" + reasontypeobj.ToString() + "'";

                object reasonobj = row.Cells["Reason"].Value;
                string reasonstr = !reasonobj.ISValidObject() ? "NULL" : "'" + reasonobj.ToString() + "'";

                object Isrightproductobj = row.Cells["IsRightproduct"].Value;
                bool Isrightproductbool = !Isrightproductobj.ISValidObject() ? false : bool.Parse(Isrightproductobj.ToString());


                mquery = "Insert into CDRNoteDetails(NoteID, ItemID, HSNCode, ItemName, Quantity,Unit, Rate, Amount, " +
                               "DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, IGSTRate, " +
                               "IGSTAmount, CessRate, CeassAmount,Total,Reason,ReasonType,StockSummaryID,IsRightProduct) values('" + noteid + "'," + itemId + "," + hsnCode + "," +
                               itemName + "," + quantity + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount
                               + "," + taxAmount + "," + cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," +
                               igstRate + "," + igstAmount + "," + cessRate + "," + cessAmount + "," + netAmount + "," + reasonstr + "," + reasontypestr + "," + stockSummaryId + ",'" + Isrightproductbool + "')";
                mlistquery.Add(mquery);
                if (cmbNoteType.Text == "Credit_Note")
                {
                    if (IsPreviousItem(stockSummaryId, Isrightproductbool))
                    {
                        //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                        int i = 0;
                        foreach (DataRow item in mdtFroRestore.Rows)
                        {
                            double hqty = 0d, mqty = 0d, lqty = 0d, finalqty = 0, currentqty = 0;
                            string hunit = item["hUnit"].ToString();
                            string hqtystr = item["hQty"].ToString();
                            string munit = item["mUnit"].ToString();
                            string mqtystr = item["mQty"].ToString();
                            string lunit = item["lUnit"].ToString();
                            string lqtystr = item["lQty"].ToString();

                            double.TryParse(hqtystr, out hqty);
                            double.TryParse(mqtystr, out mqty);
                            double.TryParse(lqtystr, out lqty);
                            double.TryParse(quantity, out currentqty);
                            if (unit == hunit)
                            {
                                finalqty = hqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }
                            else if (unit == munit)
                            {
                                finalqty = mqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }
                            else if (unit == lunit)
                            {
                                finalqty = lqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }

                            i++;
                        }

                    }

                    else
                    {
                        if (Isrightproductbool)
                        {
                            mlistquery.Add(UpdateStockSummaryForCreditNote(itemId, stockSummaryId, quantity, unit));
                        }
                        else
                        {
                            InsertOrUpDateDamageProduct(itemId, stockSummaryId, quantity, unit, reasontypestr, reasonstr);
                        }
                    }
                }
                else if (cmbNoteType.Text == "Debit_Note")
                {


                    if (IsPreviousItem(stockSummaryId, Isrightproductbool))
                    {
                        //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                        int i = 0;
                        foreach (DataRow item in mdtFroRestore.Rows)
                        {
                            double hqty = 0d, mqty = 0d, lqty = 0d, finalqty = 0, currentqty = 0;
                            string hunit = item["hUnit"].ToString();
                            string hqtystr = item["hQty"].ToString();
                            string munit = item["mUnit"].ToString();
                            string mqtystr = item["mQty"].ToString();
                            string lunit = item["lUnit"].ToString();
                            string lqtystr = item["lQty"].ToString();

                            double.TryParse(hqtystr, out hqty);
                            double.TryParse(mqtystr, out mqty);
                            double.TryParse(lqtystr, out lqty);
                            double.TryParse(quantity, out currentqty);
                            if (unit == hunit)
                            {
                                finalqty = hqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }
                            else if (unit == munit)
                            {
                                finalqty = mqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }
                            else if (unit == lunit)
                            {
                                finalqty = lqty - currentqty;
                                mdtFroRestore.Rows[i]["Qty"] = finalqty.ToString();
                                mdtFroRestore.Rows[i]["unit"] = unit;
                            }

                            i++;
                        }

                    }
                    else
                    {

                        if (Isrightproductbool)
                        {
                            mlistquery.Add(UpdateStockSummaryForDebitNote(itemId, stockSummaryId, quantity, unit));
                        }
                        else
                        {
                            UpDateDamageProductforUpdate(itemId, stockSummaryId, quantity, unit);
                        } 
                    }
                }
                else
                {

                }
            }
        }

        private bool IsPreviousItem(string stockSummaryId, bool isRightproduct)
        {
            //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

            foreach (DataRow item in mdtFroRestore.Rows)
            {
                string summaryid = item["StockSummaryId"].ToString();
                bool isgoodproduct = bool.Parse(item["IsRightProduct"].ToString());
                if (summaryid == stockSummaryId && isgoodproduct == isRightproduct)
                {
                    return true;
                }
            }
            return false;
        }

        private void InsertOrUpDateDamageProduct(string itemId, string stockSummaryId, string quantity, string unit, string reasontype, string reason)
        {
            double higestDamageqty = 0d, midDamageqty = 0d, lowestDamageqty = 0d,
               higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string HighestUnit = "", MiddleUnit = "", LowestUnit = "",
                batchNo = "", MfgDate = "", ExpDate = "", HighestRate = "",
            HighestMRP = "", MiddleRate = "", MiddleMRP = "", LowestRate = "",
            LowestMRP = "", HighestMeasureQty = "", LowestMeasureQty = "", PurchaseQty = "",
            PurchaseRate = "", PurchaseUnit = "";
            if (!IsExsistBatch(stockSummaryId))
            {
                string query = "Select * from StockSummary where ItemID='" +
                       itemId + "' and id=" + stockSummaryId + "";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    batchNo = dt.Rows[0]["BatchNo"].ToString();
                    MfgDate = DateTime.Parse(dt.Rows[0]["MfgDate"].ToString()).ToString("dd-MMM-yyyy");
                    ExpDate = DateTime.Parse(dt.Rows[0]["ExpDate"].ToString()).ToString("dd-MMM-yyyy");
                    HighestUnit = dt.Rows[0]["HighestUnit"].ToString();
                    HighestRate = dt.Rows[0]["HighestRate"].ToString();
                    HighestMRP = dt.Rows[0]["HighestMRP"].ToString();
                    MiddleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                    // string MiddleDamageQty = dt.Rows[0]["MiddleStockQty"].ToString();
                    MiddleRate = dt.Rows[0]["MiddleRate"].ToString();
                    MiddleMRP = dt.Rows[0]["MiddleMRP"].ToString();
                    LowestUnit = dt.Rows[0]["LowestUnit"].ToString();
                    //string LowestDamageQty = dt.Rows[0]["LowestStockQty"].ToString();
                    LowestRate = dt.Rows[0]["LowestRate"].ToString();
                    LowestMRP = dt.Rows[0]["LowestMRP"].ToString();
                    HighestMeasureQty = dt.Rows[0]["HighestMeasureQty"].ToString();
                    LowestMeasureQty = dt.Rows[0]["LowestMeasureQty"].ToString();
                    PurchaseQty = dt.Rows[0]["PurchaseQty"].ToString();
                    PurchaseRate = dt.Rows[0]["PurchaseRate"].ToString();
                    PurchaseUnit = dt.Rows[0]["PurchaseUnit"].ToString();

                    double.TryParse(dt.Rows[0]["HighestStockQty"].ToString(), out higestDamageqty);
                    double.TryParse(dt.Rows[0]["MiddleStockQty"].ToString(), out midDamageqty);
                    double.TryParse(dt.Rows[0]["LowestStockQty"].ToString(), out lowestDamageqty);
                    double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                    double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                    double.TryParse(quantity, out currentqty);
                }
            }
            else
            {
                string query = "Select  HighestUnit, HighestDamageQty, MiddleUnit," +
                    " MiddleDamageQty, LowestUnit, LowestDamageQty,HighestMeasureQty, " +
                    "LowestMeasureQty from DamageProduct where ItemID='" +
                    itemId + "' and StockSummaryId=" + stockSummaryId + "";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {

                    HighestUnit = dt.Rows[0]["HighestUnit"].ToString();
                    MiddleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                    LowestUnit = dt.Rows[0]["LowestUnit"].ToString();

                    double.TryParse(dt.Rows[0]["HighestDamageQty"].ToString(), out higestDamageqty);
                    double.TryParse(dt.Rows[0]["MiddleDamageQty"].ToString(), out midDamageqty);
                    double.TryParse(dt.Rows[0]["LowestDamageQty"].ToString(), out lowestDamageqty);
                    double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                    double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                    double.TryParse(quantity, out currentqty);
                }
                #region Checking Which one Update

                if (higestmesure == 0)                             //If it is true than One  Unit r there that is Highest unit
                {
                    higestDamageqty = higestDamageqty + currentqty;
                }
                else
                {
                    if (lowestmesure == 0)                      //If it is true than Two Unit r there that r Highest and Middle Unit
                    {
                        if (unit == HighestUnit)        //if true than unit come highest unit so change higest and middle unit stock
                        {
                            higestDamageqty = higestDamageqty + currentqty;
                            midDamageqty = higestDamageqty * higestmesure;
                        }
                        else                                  //if false unit came to in middle unit so change higest and middle unit stock;
                        {
                            midDamageqty = midDamageqty + currentqty;
                            higestDamageqty = midDamageqty / higestmesure;
                        }

                    }
                    else
                    {
                        if (unit == HighestUnit)        //if true than unit come highest unit so change higest,middle and lowest unit stock
                        {
                            higestDamageqty = higestDamageqty + currentqty;
                            midDamageqty = higestDamageqty * higestmesure;
                            lowestDamageqty = midDamageqty * lowestmesure;
                        }
                        else if (unit == MiddleUnit)                                //if true  than unit came to in middle unit so change higest,middle and lowest unit stock
                        {
                            midDamageqty = midDamageqty + currentqty;
                            higestDamageqty = midDamageqty / higestmesure;
                            lowestDamageqty = midDamageqty * lowestmesure;
                        }
                        else                                                       //if false than unit must came to lowest unit higest,middle and lowest unit stock;
                        {
                            lowestDamageqty = lowestDamageqty + currentqty;
                            midDamageqty = lowestDamageqty / lowestmesure;
                            higestDamageqty = midDamageqty / higestmesure;
                        }
                    }

                }
                #endregion
            }
            if (!IsExsistBatch(stockSummaryId))
            {
                HighestUnit = HighestUnit.ISNullOrWhiteSpace() ? "NULL" : "'" + HighestUnit + "'";
                HighestRate = HighestRate.ISNullOrWhiteSpace() ? "NULL" : HighestRate;
                HighestMRP = HighestMRP.ISNullOrWhiteSpace() ? "NULL" : HighestMRP;
                MiddleUnit = MiddleUnit.ISNullOrWhiteSpace() ? "NULL" : "'" + MiddleUnit + "'";
                MiddleRate = MiddleRate.ISNullOrWhiteSpace() ? "NULL" : MiddleRate;
                MiddleMRP = MiddleMRP.ISNullOrWhiteSpace() ? "NULL" : MiddleMRP;
                LowestUnit = LowestUnit.ISNullOrWhiteSpace() ? "NULL" : "'" + LowestUnit + "'";
                LowestRate = LowestRate.ISNullOrWhiteSpace() ? "NULL" : LowestRate;
                LowestMRP = LowestMRP.ISNullOrWhiteSpace() ? "NULL" : LowestMRP;
                HighestMeasureQty = HighestMeasureQty.ISNullOrWhiteSpace() ? "NULL" : HighestMeasureQty;
                LowestMeasureQty = LowestMeasureQty.ISNullOrWhiteSpace() ? "NULL" : LowestMeasureQty;
                PurchaseQty = PurchaseQty.ISNullOrWhiteSpace() ? "NULL" : PurchaseQty;
                PurchaseRate = PurchaseRate.ISNullOrWhiteSpace() ? "NULL" : PurchaseRate;
                PurchaseUnit = PurchaseUnit.ISNullOrWhiteSpace() ? "NULL" : "'" + PurchaseUnit + "'";
                string query = "Insert into DamageProduct(StockSummaryId, BatchNo, ItemID," +
                    " DamageType, Reason, MfgDate, ExpDate, HighestUnit, HighestDamageQty, HighestRate," +
                    " HighestMRP, MiddleUnit, MiddleDamageQty, MiddleRate,MiddleMRP, LowestUnit," +
                    " LowestDamageQty, LowestRate, LowestMRP, HighestMeasureQty, LowestMeasureQty," +
                    " PurchaseQty, PurchaseRate, PurchaseUnit) values('" + stockSummaryId + "','" + batchNo + "'," +
                    itemId + "," + reasontype + "," + reason + ",'" + MfgDate + "','" + ExpDate
                    + "'," + HighestUnit + "," + higestDamageqty + "," + HighestRate + "," +
                     HighestMRP + "," + MiddleUnit + "," + midDamageqty + "," + MiddleRate + "," +
                     MiddleMRP + "," + LowestUnit + "," + lowestDamageqty + "," + LowestRate + "," + LowestMRP
                     + "," + HighestMeasureQty + "," + LowestMeasureQty + "," + PurchaseQty + "," + PurchaseRate + "," + PurchaseUnit + ")";
                mlistquery.Add(query);
            }
            else
            {
                string query = "Update DamageProduct set HighestDamageQty=" + higestDamageqty + ",MiddleDamageQty=" + midDamageqty + ",LowestDamageQty=" + lowestDamageqty + " where ItemID = '" +
                        itemId + "' and StockSummaryId=" + stockSummaryId + "";
                mlistquery.Add(query);
            }
        }
        private void UpDateDamageProduct(string itemId, string stockSummaryId, string quantity, string unit)
        {
            double higestDamageqty = 0d, midDamageqty = 0d, lowestDamageqty = 0d,
               higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string HighestUnit = "", MiddleUnit = "", LowestUnit = "";
            string query = "Select  HighestUnit, HighestDamageQty, MiddleUnit," +
            " MiddleDamageQty, LowestUnit, LowestDamageQty,HighestMeasureQty, " +
            "LowestMeasureQty from DamageProduct where ItemID='" +
            itemId + "' and StockSummaryId=" + stockSummaryId + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {

                HighestUnit = dt.Rows[0]["HighestUnit"].ToString();
                MiddleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                LowestUnit = dt.Rows[0]["LowestUnit"].ToString();

                double.TryParse(dt.Rows[0]["HighestDamageQty"].ToString(), out higestDamageqty);
                double.TryParse(dt.Rows[0]["MiddleDamageQty"].ToString(), out midDamageqty);
                double.TryParse(dt.Rows[0]["LowestDamageQty"].ToString(), out lowestDamageqty);
                double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                double.TryParse(quantity, out currentqty);
            }
            #region Checking Which one Update

            if (higestmesure == 0)                             //If it is true than One  Unit r there that is Highest unit
            {
                higestDamageqty = higestDamageqty - currentqty;
            }
            else
            {
                if (lowestmesure == 0)                      //If it is true than Two Unit r there that r Highest and Middle Unit
                {
                    if (unit == HighestUnit)        //if true than unit come highest unit so change higest and middle unit stock
                    {
                        higestDamageqty = higestDamageqty - currentqty;
                        midDamageqty = higestDamageqty * higestmesure;
                    }
                    else                                  //if false unit came to in middle unit so change higest and middle unit stock;
                    {
                        midDamageqty = midDamageqty - currentqty;
                        higestDamageqty = midDamageqty / higestmesure;
                    }

                }
                else
                {
                    if (unit == HighestUnit)        //if true than unit come highest unit so change higest,middle and lowest unit stock
                    {
                        higestDamageqty = higestDamageqty - currentqty;
                        midDamageqty = higestDamageqty * higestmesure;
                        lowestDamageqty = midDamageqty * lowestmesure;
                    }
                    else if (unit == MiddleUnit)                                //if true  than unit came to in middle unit so change higest,middle and lowest unit stock
                    {
                        midDamageqty = midDamageqty - currentqty;
                        higestDamageqty = midDamageqty / higestmesure;
                        lowestDamageqty = midDamageqty * lowestmesure;
                    }
                    else                                                       //if false than unit must came to lowest unit higest,middle and lowest unit stock;
                    {
                        lowestDamageqty = lowestDamageqty - currentqty;
                        midDamageqty = lowestDamageqty / lowestmesure;
                        higestDamageqty = midDamageqty / higestmesure;
                    }
                }

            }
            #endregion
            query = "Update DamageProduct set HighestDamageQty=" + higestDamageqty + ",MiddleDamageQty=" + midDamageqty + ",LowestDamageQty=" + lowestDamageqty + " where ItemID = '" +
                   itemId + "' and StockSummaryId=" + stockSummaryId + "";
            mlistquery.Add(query);
        }
        private void UpDateDamageProductforUpdate(string itemId, string stockSummaryId, string quantity, string unit)
        {
            double higestDamageqty = 0d, midDamageqty = 0d, lowestDamageqty = 0d,
               higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string HighestUnit = "", MiddleUnit = "", LowestUnit = "";
            string query = "Select  HighestUnit, HighestDamageQty, MiddleUnit," +
            " MiddleDamageQty, LowestUnit, LowestDamageQty,HighestMeasureQty, " +
            "LowestMeasureQty from DamageProduct where ItemID='" +
            itemId + "' and StockSummaryId=" + stockSummaryId + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {

                HighestUnit = dt.Rows[0]["HighestUnit"].ToString();
                MiddleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                LowestUnit = dt.Rows[0]["LowestUnit"].ToString();

                double.TryParse(dt.Rows[0]["HighestDamageQty"].ToString(), out higestDamageqty);
                double.TryParse(dt.Rows[0]["MiddleDamageQty"].ToString(), out midDamageqty);
                double.TryParse(dt.Rows[0]["LowestDamageQty"].ToString(), out lowestDamageqty);
                double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                double.TryParse(quantity, out currentqty);
            }
            #region Checking Which one Update

            if (higestmesure == 0)                             //If it is true than One  Unit r there that is Highest unit
            {
                higestDamageqty = higestDamageqty + currentqty;
            }
            else
            {
                if (lowestmesure == 0)                      //If it is true than Two Unit r there that r Highest and Middle Unit
                {
                    if (unit == HighestUnit)        //if true than unit come highest unit so change higest and middle unit stock
                    {
                        higestDamageqty = higestDamageqty + currentqty;
                        midDamageqty = higestDamageqty * higestmesure;
                    }
                    else                                  //if false unit came to in middle unit so change higest and middle unit stock;
                    {
                        midDamageqty = midDamageqty + currentqty;
                        higestDamageqty = midDamageqty / higestmesure;
                    }

                }
                else
                {
                    if (unit == HighestUnit)        //if true than unit come highest unit so change higest,middle and lowest unit stock
                    {
                        higestDamageqty = higestDamageqty + currentqty;
                        midDamageqty = higestDamageqty * higestmesure;
                        lowestDamageqty = midDamageqty * lowestmesure;
                    }
                    else if (unit == MiddleUnit)                                //if true  than unit came to in middle unit so change higest,middle and lowest unit stock
                    {
                        midDamageqty = midDamageqty + currentqty;
                        higestDamageqty = midDamageqty / higestmesure;
                        lowestDamageqty = midDamageqty * lowestmesure;
                    }
                    else                                                       //if false than unit must came to lowest unit higest,middle and lowest unit stock;
                    {
                        lowestDamageqty = lowestDamageqty + currentqty;
                        midDamageqty = lowestDamageqty / lowestmesure;
                        higestDamageqty = midDamageqty / higestmesure;
                    }
                }

            }
            #endregion
            query = "Update DamageProduct set HighestDamageQty=" + higestDamageqty + ",MiddleDamageQty=" + midDamageqty + ",LowestDamageQty=" + lowestDamageqty + " where ItemID = '" +
                   itemId + "' and StockSummaryId=" + stockSummaryId + "";
            mlistquery.Add(query);
        }

        private bool IsExsistBatch(string stocksummaryid)
        {
            string query = "Select * from DamageProduct where StockSummaryId='" + stocksummaryid + "'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return true;
            }
            return false;
        }

        private string UpdateStockSummaryForCreditNote(string itemID, string stockSummaryid, string currentqtystr, string currentunit)
        {
            string higestunit = "", midunt = "", loestunit = "", setqry = string.Empty;
            double higestpreviosqty = 0d, midpreviousqty = 0d, lowestpreviousqty = 0d,
                higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string query = "Select  HighestUnit, HighestStockQty, MiddleUnit," +
                " MiddleStockQty, LowestUnit, LowestStockQty,HighestMeasureQty, " +
                "LowestMeasureQty from StockSummary where ItemID='" +
                itemID + "' and id=" + stockSummaryid + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {

                higestunit = dt.Rows[0]["HighestUnit"].ToString();
                midunt = dt.Rows[0]["MiddleUnit"].ToString();
                loestunit = dt.Rows[0]["LowestUnit"].ToString();

                double.TryParse(dt.Rows[0]["HighestStockQty"].ToString(), out higestpreviosqty);
                double.TryParse(dt.Rows[0]["MiddleStockQty"].ToString(), out midpreviousqty);
                double.TryParse(dt.Rows[0]["LowestStockQty"].ToString(), out lowestpreviousqty);
                double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                double.TryParse(currentqtystr, out currentqty);
                #region Checking Which one Update

                if (higestmesure == 0)                             //If it is true than One  Unit r there that is Highest unit
                {
                    higestpreviosqty = higestpreviosqty + currentqty;
                }
                else
                {
                    if (lowestmesure == 0)                      //If it is true than Two Unit r there that r Highest and Middle Unit
                    {
                        if (currentunit == higestunit)        //if true than unit come highest unit so change higest and middle unit stock
                        {
                            higestpreviosqty = higestpreviosqty + currentqty;
                            midpreviousqty = higestpreviosqty * higestmesure;
                        }
                        else                                  //if false unit came to in middle unit so change higest and middle unit stock;
                        {
                            midpreviousqty = midpreviousqty + currentqty;
                            higestpreviosqty = midpreviousqty / higestmesure;
                        }

                    }
                    else
                    {
                        if (currentunit == higestunit)        //if true than unit come highest unit so change higest,middle and lowest unit stock
                        {
                            higestpreviosqty = higestpreviosqty + currentqty;
                            midpreviousqty = higestpreviosqty * higestmesure;
                            lowestpreviousqty = midpreviousqty * lowestmesure;
                        }
                        else if (currentunit == midunt)                                //if true  than unit came to in middle unit so change higest,middle and lowest unit stock
                        {
                            midpreviousqty = midpreviousqty + currentqty;
                            higestpreviosqty = midpreviousqty / higestmesure;
                            lowestpreviousqty = midpreviousqty * lowestmesure;
                        }
                        else                                                       //if false than unit must came to lowest unit higest,middle and lowest unit stock;
                        {
                            lowestpreviousqty = lowestpreviousqty + currentqty;
                            midpreviousqty = lowestpreviousqty / lowestmesure;
                            higestpreviosqty = midpreviousqty / higestmesure;
                        }
                    }

                }
                #endregion

                query = "Update StockSummary set HighestStockQty=" + higestpreviosqty + ",MiddleStockQty=" + midpreviousqty + ",LowestStockQty=" + lowestpreviousqty + " where ItemID = '" +
                        itemID + "' and id=" + stockSummaryid + "";
            }
            return query;
        }
        private string UpdateStockSummaryForDebitNote(string itemID, string stockSummaryid, string currentqtystr, string currentunit)
        {
            string higestunit = "", midunt = "", loestunit = "", setqry = string.Empty;
            double higestpreviosqty = 0d, midpreviousqty = 0d, lowestpreviousqty = 0d,
                higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string query = "Select  HighestUnit, HighestStockQty, MiddleUnit," +
                " MiddleStockQty, LowestUnit, LowestStockQty,HighestMeasureQty, " +
                "LowestMeasureQty from StockSummary where ItemID='" +
                itemID + "' and id=" + stockSummaryid + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {

                higestunit = dt.Rows[0]["HighestUnit"].ToString();
                midunt = dt.Rows[0]["MiddleUnit"].ToString();
                loestunit = dt.Rows[0]["LowestUnit"].ToString();

                double.TryParse(dt.Rows[0]["HighestStockQty"].ToString(), out higestpreviosqty);
                double.TryParse(dt.Rows[0]["MiddleStockQty"].ToString(), out midpreviousqty);
                double.TryParse(dt.Rows[0]["LowestStockQty"].ToString(), out lowestpreviousqty);
                double.TryParse(dt.Rows[0]["HighestMeasureQty"].ToString(), out higestmesure);
                double.TryParse(dt.Rows[0]["LowestMeasureQty"].ToString(), out lowestmesure);

                double.TryParse(currentqtystr, out currentqty);
                #region Checking Which one Update

                if (higestmesure == 0)                             //If it is true than One  Unit r there that is Highest unit
                {
                    higestpreviosqty = higestpreviosqty - currentqty;
                }
                else
                {
                    if (lowestmesure == 0)                      //If it is true than Two Unit r there that r Highest and Middle Unit
                    {
                        if (currentunit == higestunit)        //if true than unit come highest unit so change higest and middle unit stock
                        {
                            higestpreviosqty = higestpreviosqty - currentqty;
                            midpreviousqty = higestpreviosqty * higestmesure;
                        }
                        else                                  //if false unit came to in middle unit so change higest and middle unit stock;
                        {
                            midpreviousqty = midpreviousqty - currentqty;
                            higestpreviosqty = midpreviousqty / higestmesure;
                        }

                    }
                    else
                    {
                        if (currentunit == higestunit)        //if true than unit come highest unit so change higest,middle and lowest unit stock
                        {
                            higestpreviosqty = higestpreviosqty - currentqty;
                            midpreviousqty = higestpreviosqty * higestmesure;
                            lowestpreviousqty = midpreviousqty * lowestmesure;
                        }
                        else if (currentunit == midunt)                                //if true  than unit came to in middle unit so change higest,middle and lowest unit stock
                        {
                            midpreviousqty = midpreviousqty - currentqty;
                            higestpreviosqty = midpreviousqty / higestmesure;
                            lowestpreviousqty = midpreviousqty * lowestmesure;
                        }
                        else                                                       //if false than unit must came to lowest unit higest,middle and lowest unit stock;
                        {
                            lowestpreviousqty = lowestpreviousqty - currentqty;
                            midpreviousqty = lowestpreviousqty / lowestmesure;
                            higestpreviosqty = midpreviousqty / higestmesure;
                        }
                    }

                }
                #endregion

                query = "Update StockSummary set HighestStockQty=" + higestpreviosqty + ",MiddleStockQty=" + midpreviousqty + ",LowestStockQty=" + lowestpreviousqty + " where ItemID = '" +
                        itemID + "' and id=" + stockSummaryid + "";
            }
            return query;
        }

        private bool IsvalidEntry()
        {
            if (cmbNoteType.Text == "Refund_Voucher")
            {
                double amount;
                double.TryParse(lblTotalCrNoteAmount.Text, out amount);
                //if (double.Parse(lblBalance.Text) < amount)
                //{
                //    MessageBox.Show("Insufficent Balance for refund.\n Change account and try again..", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    cmbPaymentAccount.Focus();
                //    return false;
                //}
                if (cmbPaymentMethod.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select payment method for payment.", "Refund Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbPaymentMethod.Select();
                    return false;
                }
                if (cmbPaymentAccount.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select payment account.", "Refund Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbPaymentAccount.Select();
                    return false;
                }
                if (cmbPaymentMethod.Text == "Cheque")
                {
                    if (txtChequeNo.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter cheqe No.", "Refund Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtChequeNo.Focus();
                        return false;
                    }
                }

            }
            if (dgvItemListForCrNote.RowCount <= 0)
            {
                MessageBox.Show("No data found for saving Cr. Note.", "Item save", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dgvItemlistfrominvoice.Select();
                return false;
            }
            return true;
        }
        private void dgvItemlistfrominvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Enabled = true;
            txtReason.Enabled = true;
            txtQuantity.Enabled = true;
            lblItemName.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["ItemName"].Value.ToString();
            lblitemid.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["itemid"].Value.ToString();
            lblStockSumaryId.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["StockSummaryid"].Value.ToString();
            txtRate.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["RATE"].Value.toRound();
            cmbUnit.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["UNIT"].Value.ToString();
            if (cmbNoteType.Text == "Refund_Voucher")
            {
                txtAmount.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["TotalWithTax"].Value.toRound();
                txtAmount.Enabled = true;
            }
            else if (cmbNoteType.Text == "Credit_Note")
            {
                txtAmount.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["TAXABLEVALUE"].Value.toRound();
            }
            else
            {
                txtAmount.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["TAXABLEVALUE"].Value.toRound();
                GetBatchDetails();
            }

            txtDiscountRate.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["DISCOUNTRATE"].Value.ToString();
            txtDiscountAmount.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["DISCOUNTAMOUNT"].Value.toRound();
            txtQuantity.Text = dgvItemlistfrominvoice.SelectedRows[0].Cells["QTY"].Value.ToString();
            string maxqtystr = dgvItemlistfrominvoice.SelectedRows[0].Cells["QTY"].Value.ToString();
            string maxamount = txtAmount.Text;
            long.TryParse(maxqtystr, out mmaxqty);
            double.TryParse(maxamount, out mMaxAmount);
        }

        private void GetBatchDetails()
        {
            cmbBatchNo.Items.Clear();
            string query = "Aelect StockSummary.BatchNo from StockHistory " +
                           "inner join PurchaseBill on PurchaseBill.BillNo = StockHistory.PurchaseBillNo " +
                           "inner join PurchaseBillDetails on PurchaseBillDetails.Billid = PurchaseBill.BillID " +
                           "inner join StockSummary on StockSummary.BatchNo = StockHistory.BatchNo " +
                           "where PurchaseBill.BillID = '" + mIdOrNo + "' and StockSummary.ItemID = '" + lblitemid.Text.GetDBFormatString()
                           +"' and PurchaseBillDetails.Rate = '" + txtRate.Text.GetDBFormatString() 
                           + "' and PurchaseBillDetails.Unit = '" + cmbUnit.Text + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    cmbBatchNo.Items.Add(item["BatchNo"].ToString());
                }
                cmbBatchNo.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (!IsDuplicateItemSelect())
                {
                    DescriptionAdd();
                    GenerateTotal();
                    lblItemName.Text = "-----";
                    txtReason.Text = "";
                    txtQuantity.Text = "";
                    cmbUnit.SelectedIndex = -1;
                    txtRate.Clear();
                    txtAmount.Clear();
                    txtDiscountAmount.Clear();
                    txtDiscountRate.Clear();
                    DeleteRowFromItemList();

                }
            }
        }
        private void DeleteRowFromItemList()
        {
            if (dgvItemlistfrominvoice.SelectedRows.Count > 0)
            {
                dgvItemlistfrominvoice.Rows.RemoveAt(dgvItemlistfrominvoice.CurrentRow.Index);
            }
        }

        private bool IsValidData()
        {
            if (txtQuantity.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter quantity.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtQuantity.Focus();
                return false;
            }
            else
            {
                if (cmbNoteType.Text != "Refund_Voucher")
                {
                    if (int.Parse(txtQuantity.Text) <= 0)
                    {
                        MessageBox.Show("Enter valid quantity.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtQuantity.Focus();
                        return false;
                    }
                }
            }
            if (cmbReasonType.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select resaon type for return.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReason.Focus();
                return false;
            }
            if (txtReason.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter resaon for return.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReason.Focus();
                return false;
            }
            if (cmbNoteType.Text == "Debit_Note")
            {
                if (cmbBatchNo.Items.Count <= 0)
                {
                    MessageBox.Show("Sorry you can not refund it.\n Because this item not found in the list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbBatchNo.Focus();
                    return false;
                }
                if (cmbBatchNo.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select Batch No.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbBatchNo.Focus();
                    return false;
                }
                if (!chkRightProduct.Checked)
                {
                    if (!IsFoundInDamageProduct())
                    {
                        MessageBox.Show("Select Batch No. Not found in Damage list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbBatchNo.Focus();
                        return false;
                    }
                    if (!IsValidQtyforDamageList())
                    {
                        MessageBox.Show("This quantity not available in damage list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtQuantity.Focus();
                        return false;
                    }
                }
                if (!IsValidQtyforStockSummary())
                {
                    MessageBox.Show("This quantity not available in Stock list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtQuantity.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidQtyforDamageList()
        {
            string query = "Select * from DamageProduct where stockSummaryid='" + lblStockSumaryId.Text.GetDBFormatString() + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                double currentqty = 0d, hpreviousqty = 0d, mpreviousqty = 0d, lowpreviosqty = 0;
                string highestunit = dt.Rows[0]["HighestUnit"].ToString();
                string highestdamageqty = dt.Rows[0]["HighestDamageQty"].ToString();

                string middleunit = dt.Rows[0]["MiddleUnit"].ToString();
                string middleDamageQty = dt.Rows[0]["MiddleDamageQty"].ToString();

                string lowestunit = dt.Rows[0]["LowestUnit"].ToString();
                string lowestDamageQty = dt.Rows[0]["LowestDamageQty"].ToString();
                double.TryParse(txtQuantity.Text, out currentqty);
                double.TryParse(highestdamageqty, out hpreviousqty);
                double.TryParse(middleDamageQty, out mpreviousqty);
                double.TryParse(lowestDamageQty, out lowpreviosqty);

                if (cmbUnit.Text == highestunit)
                {
                    if (currentqty > hpreviousqty)
                    {
                        return false;
                    }
                }
                if (cmbUnit.Text == middleunit)
                {
                    if (currentqty > mpreviousqty)
                    {
                        return false;
                    }
                }
                if (cmbUnit.Text == lowestunit)
                {
                    if (currentqty > lowpreviosqty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool IsValidQtyforStockSummary()
        {
            string query = "Select * from StockSummary where id='" + lblStockSumaryId.Text.GetDBFormatString() + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                double currentqty = 0d, hpreviousqty = 0d, mpreviousqty = 0d, lowpreviosqty = 0;
                string highestunit = dt.Rows[0]["HighestUnit"].ToString();
                string highestStockqty = dt.Rows[0]["HighestStockQty"].ToString();

                string middleunit = dt.Rows[0]["MiddleUnit"].ToString();
                string middleStockQty = dt.Rows[0]["MiddleStockQty"].ToString();

                string lowestunit = dt.Rows[0]["LowestUnit"].ToString();
                string lowestStockQty = dt.Rows[0]["LowestStockQty"].ToString();
                double.TryParse(txtQuantity.Text, out currentqty);
                double.TryParse(highestStockqty, out hpreviousqty);
                double.TryParse(middleStockQty, out mpreviousqty);
                double.TryParse(lowestStockQty, out lowpreviosqty);

                if (cmbUnit.Text == highestunit)
                {
                    if (currentqty > hpreviousqty)
                    {
                        return false;
                    }
                }
                if (cmbUnit.Text == middleunit)
                {
                    if (currentqty > mpreviousqty)
                    {
                        return false;
                    }
                }
                if (cmbUnit.Text == lowestunit)
                {
                    if (currentqty > lowpreviosqty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsFoundInDamageProduct()
        {
            string query = "Select * from DamageProduct where stockSummaryid='" + lblStockSumaryId.Text.GetDBFormatString() + "'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return false;
            }
            return true;
        }

        private bool IsDuplicateItemSelect()
        {
            string itemidlbl = lblitemid.Text;
            string stocksummaryIdlbl = lblStockSumaryId.Text;
            foreach (var row in mBatchList)
            {
                string itemidstocksummaryId = row.ToString();
                if (itemidstocksummaryId == itemidlbl + stocksummaryIdlbl)
                {
                    MessageBox.Show("Found duplicate item in below list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dgvItemlistfrominvoice.Select();
                    return true;
                }
            }
            return false;
        }
        private void AdvanceReceiptDataRetrive()
        {
            string query = "select * from AdvanceReceiptVoucher where ReceiptNo='" + mIdOrNo + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mledgerid = dt.Rows[0]["LedgerId"].ToString();
                string itemName = dt.Rows[0]["ItemName"].ToString();
                string itemID = dt.Rows[0]["ItemId"].ToString();
                string hsnCode = dt.Rows[0]["ComodityCode"].ToString();
                string unit = dt.Rows[0]["Unit"].ToString();
                string qtyStr = dt.Rows[0]["Qty"].ToString();
                int qty = (qtyStr.ISNullOrWhiteSpace() ? 0 : int.Parse(qtyStr));
                string rateStr = dt.Rows[0]["rate"].ToString();
                double rate = 0d;
                double.TryParse(rateStr, out rate);
                double amount = rate * qty;
                string cgstRate = dt.Rows[0]["CGSTRate"].ToString();
                string cgstAmount = dt.Rows[0]["CGSTAmount"].ToString();
                string sgstRate = dt.Rows[0]["SGSTRate"].ToString();
                string sgstAmount = dt.Rows[0]["SGSTAmount"].ToString();
                string igstRate = dt.Rows[0]["IGSTRate"].ToString();
                string igstAmount = dt.Rows[0]["IGSTAmount"].ToString();
                string cessRate = dt.Rows[0]["CessRate"].ToString();
                string cessAmount = dt.Rows[0]["CessAmount"].ToString();
                string amountWithDiscount = dt.Rows[0]["TaxValue"].ToString();
                double taxAmount = amountWithDiscount.ISNullOrWhiteSpace() ? 0d : double.Parse(amountWithDiscount);
                string totalWithTax = dt.Rows[0]["DueAmount"].ToString();
                string reason = txtReason.Text.GetDBFormatString();

                if (!itemID.ISNullOrWhiteSpace())
                {
                    taxAmount = CalculateAdvanceReceiptAmpount(totalWithTax, cgstRate, sgstRate, cessRate, igstRate, itemID, "dgvItemlistfrominvoice");

                    ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGst, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                              out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);
                    dgvItemlistfrominvoice.Rows.Add(mDescriptionSlno, "", itemID, itemName, hsnCode, qty, unit, rate, amount.ToString("0.00"), "",
                                      "", taxAmount.ToString("0.00"), cgstRate,
                                      cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax, reason);
                }
                else
                {
                    dgvItemlistfrominvoice.Rows.Add(mDescriptionSlno, "", "", "", "", qty, "", "", "", "",
                                                          "", "", "",
                                                          "", "", "", "", "", "", "", "", reason);

                    CalculateAdvanceReceiptAmpount(totalWithTax, cgstRate, sgstRate, cessRate, igstRate, itemID, "dgvItemlistfrominvoice");
                }

            }

        }
        private void DescriptionAdd()
        {
            string itemName = lblItemName.Text;
            string stockSummaryid = lblStockSumaryId.Text;
            string itemID = itemName.ISNullOrWhiteSpace() ? "" : lblitemid.Text;
            string hsnCode = itemName.ISNullOrWhiteSpace() ? "" : ItemTools.GetItemHSNCode(itemID);
            string unit = cmbUnit.Text;
            string qtyStr = txtQuantity.Text;
            int qty = (qtyStr.ISNullOrWhiteSpace() ? 0 : int.Parse(qtyStr));
            string rateStr = txtRate.Text;
            //string rateStr = rateobj.ISValidNumericObjec txtRate.Text;
            double rate = 0d;
            double.TryParse(rateStr, out rate);
            double discountRate = txtDiscountRate.Text.ISNullOrWhiteSpace() ? 0d : double.Parse(txtDiscountRate.Text);
            double discountAmount = 0d;
            double amount = rate * qty;
            string disAmountSrt = txtDiscountAmount.Text;
            discountAmount = disAmountSrt.ISNullOrWhiteSpace() ? 0d : double.Parse(disAmountSrt);
            string amountWithDiscount = txtAmount.Text;
            double taxAmount = amountWithDiscount.ISNullOrWhiteSpace() ? 0d : double.Parse(amountWithDiscount);
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
            string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
            string totalWithTax = "";
            string reasonType = cmbReasonType.Text.GetDBFormatString();
            string reason = txtReason.Text.GetDBFormatString();
            bool isRightProuduct = chkRightProduct.Checked;
            double taxvalue = 0d, taxableamount = 0d, totalrate = 0d,
                advanceamount = 0d, cessrate = 0d,
                cgstrate = 0d, sgstrate = 0d,
                igstrate = 0d;


            object cgstRateobj = dgvItemlistfrominvoice.Rows[0].Cells["CGSTRATE"].Value;
            cgstRate = cgstRateobj.ISValidObject() ? cgstRateobj.ToString() : "";
            object sgstRateobj = dgvItemlistfrominvoice.Rows[0].Cells["SGSTRATE"].Value;
            sgstRate = sgstRateobj.ISValidObject() ? sgstRateobj.ToString() : "";
            object igstRateobj = dgvItemlistfrominvoice.Rows[0].Cells["IGSTRATE"].Value;
            igstRate = igstRateobj.ISValidObject() ? igstRateobj.ToString() : "";
            object cessRateobj = dgvItemlistfrominvoice.Rows[0].Cells["CESSRATE"].Value;
            cessRate = cessRateobj.ISValidObject() ? cessRateobj.ToString() : "";
            if (cmbNoteType.Text == "Refund_Voucher")
            {
                if (!itemID.ISNullOrWhiteSpace())
                {
                    taxAmount = CalculateAdvanceReceiptAmpount(txtAmount.Text, cgstRate, sgstRate, cessRate, igstRate, itemID, "dgvItemListForCrNote");

                    ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGst, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                              out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);
                    dgvItemListForCrNote.Rows.Add(mDescriptionSlno, stockSummaryid, itemID, itemName, hsnCode, qty, unit, rate, amount.ToString("0.00"), discountRate,
                                      discountAmount.ToString("0.00"), taxAmount.ToString("0.00"), cgstRate,
                                      cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax, reasonType, reason, isRightProuduct);
                    mBatchList.Add(itemID+ stockSummaryid);
                }
                else
                {
                    dgvItemListForCrNote.Rows.Add(mDescriptionSlno, "", "", "", "", qty, "", "", "", "",
                                                          "", "", "",
                                                          "", "", "", "", "", "", "", "", "", reason, "");

                    CalculateAdvanceReceiptAmpount(txtAmount.Text, cgstRate, sgstRate, cessRate, igstRate, itemID, "dgvItemListForCrNote");
                }
            }
            if (cmbNoteType.Text == "Credit_Note")
            {
                ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGst, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                              out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);
                dgvItemListForCrNote.Rows.Add(mDescriptionSlno, stockSummaryid, itemID, itemName, hsnCode, qty, unit, rate, amount.ToString("0.00"), discountRate,
                                  discountAmount.ToString("0.00"), taxAmount.ToString("0.00"), cgstRate,
                                  cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax, reasonType, reason, isRightProuduct);
                mBatchList.Add(itemID + stockSummaryid);

            }
            else if (cmbNoteType.Text == "Debit_Note")
            {
                ItemTools.GetItemGSTRateAndAmountForPurchase(itemID, mIsIGst, mGstType, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                              out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);
                dgvItemListForCrNote.Rows.Add(mDescriptionSlno, stockSummaryid, itemID, itemName, hsnCode, qty, unit, rate, amount.ToString("0.00"), discountRate,
                                  discountAmount.ToString("0.00"), taxAmount.ToString("0.00"), cgstRate,
                                  cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax, reasonType, reason, isRightProuduct);
                mBatchList.Add(itemID + stockSummaryid);
            }

            DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
            btnCelCol.ToolTipText = "Delete";
            btnCelCol.Value = "Delete";
            btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
            //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
            dgvItemListForCrNote.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
            mDescriptionSlno++;
        }
        private double CalculateAdvanceReceiptAmpount(string totalval, string mcgstrate, string msgstrate, string mcessrate, string migstrate, string itemid, string addgrid)
        {
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "", taxWithamount = "";
            double taxvalue = 0d, taxableamount = 0d, totalrate = 0d,
                   amount = 0d, cessrate = 0d,
                   cgstrate = 0d, sgstrate = 0d,
                   igstrate = 0d;
            double.TryParse(mcgstrate, out cgstrate);
            double.TryParse(msgstrate, out sgstrate);
            double.TryParse(mcessrate, out cessrate);
            double.TryParse(migstrate, out igstrate);
            double.TryParse(totalval, out amount);
            totalrate = cgstrate + sgstrate + cessrate + igstrate;
            try
            {
                taxvalue = ((amount * totalrate) / (100 + totalrate));
                if (mIsIGst)
                {
                    igstAmount = taxvalue.ToString("0.00");
                }
                else
                {
                    cgstAmount = (taxvalue / 2).ToString("0.00");
                    sgstAmount = (taxvalue / 2).ToString("0.00");
                }
            }
            catch (Exception) { }
            taxableamount = amount - taxvalue;
            taxWithamount = (taxableamount + taxvalue).ToString("0.00");

            if (itemid.ISNullOrWhiteSpace() && addgrid == "dgvItemListForCrNote")
            {
                dgvItemListForCrNote.Rows[0].Cells["CGSTAMOUNTCN"].Value = cgstAmount;
                dgvItemListForCrNote.Rows[0].Cells["CGSTRATECN"].Value = mcgstrate;
                dgvItemListForCrNote.Rows[0].Cells["SGSTAMOUNTCN"].Value = sgstAmount;
                dgvItemListForCrNote.Rows[0].Cells["SGSTRATECN"].Value = msgstrate;
                dgvItemListForCrNote.Rows[0].Cells["IGSTAMOUNTCN"].Value = igstAmount;
                dgvItemListForCrNote.Rows[0].Cells["IGSTRATECN"].Value = migstrate;
                dgvItemListForCrNote.Rows[0].Cells["CESSAMOUNTCN"].Value = cessAmount;
                dgvItemListForCrNote.Rows[0].Cells["CESSRATECN"].Value = mcessrate;
                dgvItemListForCrNote.Rows[0].Cells["TAXABLEVALUECN"].Value = taxableamount.ToString("0.00");
                dgvItemListForCrNote.Rows[0].Cells["TotalWithTaxCN"].Value = taxWithamount;
            }
            else if (itemid.ISNullOrWhiteSpace() && addgrid == "dgvItemlistfrominvoice")
            {
                dgvItemlistfrominvoice.Rows[0].Cells["CGSTAMOUNT"].Value = cgstAmount;
                dgvItemlistfrominvoice.Rows[0].Cells["CGSTRATE"].Value = mcgstrate;
                dgvItemlistfrominvoice.Rows[0].Cells["SGSTAMOUNT"].Value = sgstAmount;
                dgvItemlistfrominvoice.Rows[0].Cells["SGSTRATE"].Value = msgstrate;
                dgvItemlistfrominvoice.Rows[0].Cells["IGSTAMOUNT"].Value = igstAmount;
                dgvItemlistfrominvoice.Rows[0].Cells["IGSTRATE"].Value = migstrate;
                dgvItemlistfrominvoice.Rows[0].Cells["CESSAMOUNT"].Value = cessAmount;
                dgvItemlistfrominvoice.Rows[0].Cells["CESSRATE"].Value = mcessrate;
                dgvItemlistfrominvoice.Rows[0].Cells["TAXABLEVALUE"].Value = taxableamount.ToString("0.00");
                dgvItemlistfrominvoice.Rows[0].Cells["TotalWithTax"].Value = taxWithamount;


            }

            return taxableamount;
        }

        private void GenerateTotal()
        {
            mDescriptionSlno = 1;
            mTotalCrNoteAmount = 0d; mTotalAmount = 0d; mTotalDiscount = 0d;
            mTotalCGST = 0d; mTotalSGST = 0d; mTotalIGST = 0d; mTotalCESS = 0d;
            mTaxableAmount = 0d; mTotalWithTax = 0d;
            mTotalQuantity = 0;
            foreach (DataGridViewRow row in dgvItemListForCrNote.Rows)
            {
                row.Cells["SlNocn"].Value = mDescriptionSlno++;
                object qtyObj = row.Cells["QTYcn"].Value;
                object discountObj = row.Cells["DiscountAmountcn"].Value;
                object totalObj = row.Cells["TotalAmountcn"].Value;
                object totalTaxableobj = row.Cells["TaxableValuecn"].Value;
                object cgstObj = row.Cells["CGSTAmountcn"].Value;
                object sGstObj = row.Cells["SGSTAmountcn"].Value;
                object iGstObj = row.Cells["IGSTAmountcn"].Value;
                object cessObj = row.Cells["CessAmountcn"].Value;
                object totalWithTaxObj = row.Cells["TotalWithTaxcn"].Value;

                mTotalQuantity += (qtyObj.ISValidObject()) ? long.Parse(qtyObj.ToString()) : 0;
                mTotalAmount += (totalObj.ISValidObject()) ? double.Parse(totalObj.ToString()) : 0;
                mTaxableAmount += (totalTaxableobj.ISValidObject()) ? double.Parse(totalTaxableobj.ToString()) : 0;
                mTotalDiscount += (discountObj.ISValidObject()) ? double.Parse(discountObj.ToString()) : 0;
                mTotalCGST += (cgstObj.ISValidObject()) ? double.Parse(cgstObj.ToString()) : 0;
                mTotalSGST += (sGstObj.ISValidObject()) ? double.Parse(sGstObj.ToString()) : 0;
                mTotalIGST += (iGstObj.ISValidObject()) ? double.Parse(iGstObj.ToString()) : 0;
                mTotalCESS += (cessObj.ISValidObject()) ? double.Parse(cessObj.ToString()) : 0;
                mTotalWithTax += (totalWithTaxObj.ISValidObject()) ? double.Parse(totalWithTaxObj.ToString()) : 0;
            }
            lblTotQuantity.Text = mTotalQuantity.ToString();
            lblTotAmount.Text = mTotalAmount.toString();
            lblTaxableAmountTotal.Text = mTaxableAmount.toString();

            lblTotalCGST.Text = mTotalCGST.toString();
            lblTotalIGST.Text = mTotalIGST.toString();
            lblTotalSGST.Text = mTotalSGST.toString();
            lblTotalCESS.Text = mTotalCESS.toString();
            lblTotalWithTax.Text = mTotalWithTax.toString();
            lblTotalDiscount.Text = mTotalDiscount.toString();
            mTotalCrNoteAmount = mTotalWithTax;
            lblTotalCrNoteAmount.Text = mTotalCrNoteAmount.toString();
        }
        private void CalculateAmount()
        {
            int qty = 0;
            double rate = 0d;
            double disAmount = 0d;
            double amount = 0d;
            double totalDiscount = 0d;
            try
            {
                double.TryParse(txtRate.Text, out rate);
                int.TryParse(txtQuantity.Text, out qty);
                double.TryParse(txtDiscountAmount.Text, out disAmount);
            }
            catch (Exception) { }
            totalDiscount = qty * disAmount;
            amount = (qty * rate) - totalDiscount;

            txtAmount.Text = amount.ToString("0.00");
        }
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (cmbNoteType.Text != "Refund_Voucher")
            {
                CalculateAmount();
            }
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                string currentqtystr = txtQuantity.Text + e.KeyChar;
                long currentqty;
                long.TryParse(currentqtystr, out currentqty);
                if (currentqty <= mmaxqty)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;

                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void InvoiceAndPurchaseDetailsDataRetrive(string midOrNo)
        {
            string query = "";
            dgvItemlistfrominvoice.Rows.Clear();
            if (cmbNoteType.Text == "Credit_Note")
            {
                query = "select * from InvoiceDetails where invoiceno='" + midOrNo + "'";
            }
            else if (cmbNoteType.Text == "Debit_Note")
            {
                query = "select * from PurchaseBillDetails where Billid='" + midOrNo + "'";
            }
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                int i = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string stockSummaryId = cmbNoteType.Text == "Credit_Note" ? item["StockSummaryId"].ToString() : "";
                    string comodityCode = item["HSNCode"].ToString();
                    string itemID = item["ItemID"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string qtyStr = item["Quantity"].ToString();
                    string unit = item["Unit"].ToString();
                    string rateStr = item["Rate"].ToString();
                    string amountStr = item["Amount"].ToString();

                    object disRateStr = item["DiscountRate"];
                    string disRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString()).ToString("0.00") : "";
                    object disAmountStr = item["DiscountAmount"];
                    string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString()).ToString("0.00") : "";

                    string taxAmountStr = item["TaxAmount"].ToString();
                    string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString()).ToString("0.00") : "";

                    object cgstRateStr = item["CGSTRate"];
                    string cgstRate = cgstRateStr.ISValidObject() ? double.Parse(cgstRateStr.ToString()).ToString("0.00") : "";
                    object cgstAmountStr = item["CGSTAmount"];
                    string cgstAmount = cgstAmountStr.ISValidObject() ? double.Parse(cgstAmountStr.ToString()).ToString("0.00") : "";

                    object sgstRateStr = item["SGSTRate"];
                    string sgstRate = sgstRateStr.ISValidObject() ? double.Parse(sgstRateStr.ToString()).ToString("0.00") : "";
                    object sgstAmountStr = item["SGSTAmount"];
                    string sgstAmount = sgstAmountStr.ISValidObject() ? double.Parse(sgstAmountStr.ToString()).ToString("0.00") : "";

                    object igstRateStr = item["IGSTRate"];
                    string igstRate = igstRateStr.ISValidObject() ? double.Parse(igstRateStr.ToString()).ToString("0.00") : "";
                    object igstAmountStr = item["IGSTAmount"];
                    string igstAmount = igstAmountStr.ISValidObject() ? double.Parse(igstAmountStr.ToString()).ToString("0.00") : "";

                    object cessrateStr = item["CessRate"];
                    string cessrate = cessrateStr.ISValidObject() ? double.Parse(cessrateStr.ToString()).ToString("0.00") : "";
                    object cessAmountStr = item["CeassAmount"];
                    string cessAmount = cessAmountStr.ISValidObject() ? double.Parse(cessAmountStr.ToString()).ToString("0.00") : "";

                    object totalAmountStr = item["Total"];
                    string totalAmount = totalAmountStr.ISValidObject() ? double.Parse(totalAmountStr.ToString()).ToString("0.00") : "";

                    double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);
                    double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);

                    dgvItemlistfrominvoice.Rows.Add(i++, stockSummaryId, itemID, itemName, comodityCode, qtyStr, unit,
                                         rate.ToString("0.00"), amount.ToString("0.00"), disRate, disAmount, taxAmount
                                         , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                                         , cessrate, cessAmount, totalAmount);

                }
            }
        }
    }
}
