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
    public partial class Invoice_Direct : Form
    {
        #region Decleartion
        private long mInvoiceSlNo = INVOICE_TOOLS._InvoiceStartFrom;
        private int c = 0, mDescriptionSlno = 1;
        private bool mIsIGST = false;
        public event Action OnClose;
        List<string> batchList = new List<string>();
        List<string> mlistQuery = new List<string>();
        DataTable mdt = new DataTable();
        DataTable mdtFroRestore = new DataTable();


        #region Double
        private double mtotalInvoiceAmount = 0d, mTotalAmount = 0d, mTotalDiscount = 0d, mTotalCGST, mTotalSGST,
                      mTotalIGST, mTotalCESS, mTaxableAmount, mTotalWithTax, mDueAmount = 0d, mmaxqty = 0d, mTotalQuantity = 0,
                       mAvailabelQty = 0d;
        //edit
        private double mtotalPreviosInvoiceAmount = 0d;
        #endregion Double

        #region String
        //for stock
        private string mStockSummaryID, mHighestUnit = "", mMiddleUnit = "", mLowestUnit = "", mHighestStockQty = "", mMiddleStockQty = "", mLowestQty = "", mHighestSalesRate = "",
                       mMiddlesalesRate = "", mLowestSalesRate = "", mHighestMesureUnit = "", mLowestMesureUnit = "";
        //Others
        private string msg = "", mquery = "", mStatus = "", mPreviousItemGSTType = "", mInvoiceType = "", morderslno = "", mCustomerBillingName = "",
                       mCustomerbillingaddress = "", mbillingGstinnumber = "", mbillingstatecode = "", mbillingstatename = "", mshippingname,
                       mshippingaddress, mShippinggstno, mshippingstatename, mshippingStatecode, mCustomerLadgerID = "";
        //for edit           
        private string mInvoiceNoForEdit = "", mPreviousCustomerID = "", mTransectionIDForEdit = "", mPreviosStateCode = "", mPreviousCustomername = "";
        #endregion String

        public enum _Invoicefrom
        {
            Order,
            Challan
        }
        _Invoicefrom minvoicefrom;

        #endregion Decleartion

        public Invoice_Direct()
        {
            InitializeComponent();
            this.FitToVertical();
            cmbCustomerName.AddCashCustomers();
            cmbCustomerName.Text = "CASH";
            cmbCustomerName_Leave(null, null);
            cmbItemName.SelectedIndexChanged -= cmbItemName_Leave;
            cmbItemName.AddItem();
            cmbItemName.SelectedIndexChanged += cmbItemName_Leave;

            cmbBillingTerms.AddBillingTerms();
            //cmbUnit1.AddUnit();
            GridDesign();
            GenerateInvoiceNo();
            
        }
        public Invoice_Direct(string slno, string customername)
        {
            InitializeComponent();
            this.FitToVertical();
            morderslno = slno;
            cmbCustomerName.AddCashCustomers();
            cmbCustomerName.Text = customername;
            cmbCustomerName_Leave(null, null);
            cmbCustomerName.Enabled = false;
            btnCustomer.Enabled = false;
            cmbItemName.SelectedIndexChanged -= cmbItemName_Leave;
            cmbItemName.AddItem();
            cmbItemName.SelectedIndexChanged += cmbItemName_Leave;

            cmbBillingTerms.AddBillingTerms();

            GridDesign();
            GenerateInvoiceNo();
        }

        /// <summary>
        /// edit
        /// </summary>
        /// <param name="invoiceno"></param>
        /// <param name="status"></param>
        /// <param name="unused"></param>
        public Invoice_Direct(string invoiceno, string status, string unused)
        {
            InitializeComponent();
            this.FitToVertical();
            mInvoiceNoForEdit = invoiceno;
            InitTable();

            cmbCustomerName.AddCashCustomers();
            cmbItemName.SelectedIndexChanged -= cmbItemName_Leave;
            cmbItemName.AddItem();
            cmbItemName.SelectedIndexChanged += cmbItemName_Leave;
            cmbBillingTerms.AddBillingTerms();
            //cmbUnit1.AddUnit();
            GridDesign();
            RetruveDataFromInvoice();
            ReadOnlyAllControl(status);
        }
        private void ReadOnlyAllControl(string status)
        {
            if (cmbCustomerName.Text != "CASH")
            {
                if (status == "Paid" || IsAdjustAmount() || status == "Cancel")
                {
                    foreach (Control item in this.Controls)
                    {
                        item.Enabled = false;
                    }
                }
            }
            else
            {
                if (status == "Cancel")
                {
                    foreach (Control item in this.Controls)
                    {
                        item.Enabled = false;
                    }
                }
            }
            btnCancel.Enabled = true;
        }
        private bool IsAdjustAmount()
        {
            string qurey = "select InVoiceNo from AdjustHistory where InVoiceNo='" + lblInvoiceNo.Text + "' ";
            object obj = SQLHelper.GetInstance().ExcuteScalar(qurey, out msg);
            if (obj.ISValidObject())
            {
                return true;
            }
            return false;
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
            //mdtFroRestore.Columns.Add("IsRightProduct", typeof(string));

        }
        private void RetruveDataFromInvoice()
        {
            string dueamount = "0.00";
            string query = "Select Invoice.*,Invoice.LedgerId,ladgerMain.templatename from Invoice" +
                "  inner join ladgerMain on LadgerMain.LadgerID=Invoice.LedgerId where InvoiceNo='" + mInvoiceNoForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                mPreviousCustomername = dt.Rows[0]["templatename"].ToString();
                cmbCustomerName.Text = mPreviousCustomername;
                mPreviousCustomerID = dt.Rows[0]["LedgerId"].ToString();
                mPreviosStateCode = dt.Rows[0]["BillingStateCode"].ToString();
                cmbCustomerName_Leave(null, null);

                txtBillingName.Text = dt.Rows[0]["BillingTo"].ToString();
                txtBillingAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
                cmbState.Text = dt.Rows[0]["BillingState"].ToString();

                dtpInvoiceDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
                cmbBillingTerms.Text = dt.Rows[0]["BillingTerms"].ToString();
                dtpDueDate.Text = dt.Rows[0]["DueDate"].ToString();
                txtBuyerOrderno.Text = dt.Rows[0]["BuyerOrderNo"].ToString();
                dtmBuyerOrderDate.Text = dt.Rows[0]["BuyerOrderDate"].ToString();

                txtPackingCharges.Text = dt.Rows[0]["PackingCharges"].ToString();
                txtfreightcharges.Text = dt.Rows[0]["FreightChargs"].ToString();
                txtOthersCharges.Text = dt.Rows[0]["OtherCharges"].ToString();
                txtDiscount.Text = dt.Rows[0]["OverallDiscount"].ToString();

                mInvoiceType = dt.Rows[0]["InvoiceType"].ToString();

                string Totalinvoiceamount = dt.Rows[0]["TotalInvoiceAmount"].ToString().Replace(",","");
                mtotalPreviosInvoiceAmount = Totalinvoiceamount.ISNullOrWhiteSpace() ? 0 : double.Parse(Totalinvoiceamount);

                if (cmbCustomerName.Text == "CASH")
                {
                    cmbCustomerName.Enabled = false;
                    btnCustomer.Enabled = false;
                    mTransectionIDForEdit = dt.Rows[0]["LastTransecetionID"].ToString();
                    TransectionTools.GetPaymentDetailsId(mTransectionIDForEdit);

                    chkPayment.Checked = true;
                    cmbPaymentMethod.Text = TransectionTools._PaymentMethod;
                    cmbPaymentAccount.Text = TransectionTools._CRAccountTemplateName;
                    txtChequeNo.Text = TransectionTools._ChequeNo;
                    cmbBank.Text = TransectionTools._BankName;
                    dtpDateCheque.Text = TransectionTools._ChequeDate;
                    cmbPaymentAccount_SelectedIndexChanged(null, null);
                    txtPaidAmount.Text = dt.Rows[0]["TotalInvoiceAmount"].ToString();
                    GetBillingDetails();

                }
                dueamount = dt.Rows[0]["DueAmount"].ToString();
            }
            RetrivedataInvoiceDetails();
            GenerateTotal();
            SumOfCharges();
            GenerateGridForNonGSTType();
            lblDueAmount.Text = dueamount;
            GetUnitDetails();
        }
        private void RetrivedataInvoiceDetails()
        {
            string query = "select * from InvoiceDetails where InvoiceNo='" + mInvoiceNoForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mDescriptionSlno = 1;
                batchList.Clear();
                dgvItemList.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    string itemId = item["ItemID"].ToString();
                    string hsnCode = item["HSNCode"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string quantity = item["Quantity"].ToString();
                    string unit = item["Unit"].ToString();
                    string rate = item["Rate"].ToString().Replace(",", ""); ;
                    string amount = item["Amount"].ToString();
                    string disRate = item["DiscountRate"].ToString().Replace(",", "");
                    string disAmount = item["DiscountAmount"].ToString().Replace(",", ""); ;
                    string taxAmount = item["TaxAmount"].ToString();
                    string CgstRate = item["CGSTRate"].ToString().Replace(",", "");
                    string CgstAmount = item["CGSTAmount"].ToString();
                    string sgstRate = item["SGSTRate"].ToString();
                    string sgstAmount = item["SGSTAmount"].ToString();
                    string igstRate = item["IGSTRate"].ToString();
                    string igstAmount = item["IGSTAmount"].ToString();
                    string cessRate = item["CessRate"].ToString();
                    string cessAmount = item["CeassAmount"].ToString();
                    string totalWithTax = item["Total"].ToString();
                    string stockSummaryid = item["StockSummaryId"].ToString();

                    //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                    mdtFroRestore.Rows.Add(stockSummaryid, itemId, quantity, unit, "", "", "", "", "", "");
                    batchList.Add(stockSummaryid + itemId);
                    dgvItemList.Rows.Add(mDescriptionSlno, itemId, stockSummaryid, itemName, hsnCode,
                                         quantity, unit, rate.toRound(), amount.toRound(), disRate,
                                         disAmount.toRound(), taxAmount.toRound(), CgstRate,
                                         CgstAmount.toRound(), sgstRate, sgstAmount.toRound(),
                                         igstRate, igstAmount.toRound(), cessRate, cessAmount.toRound(),
                                         totalWithTax.toRound());

                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                    dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
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
                string qtystr = item["Qty"].ToString().Replace(",","");
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

                    highrtmesurestr = highrtmesurestr.Replace(",", "");
                    lowestMesurestr = lowestMesurestr.Replace(",", "");
                    qtystr = qtystr.Replace(",", "");

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

        private void GridDesign()
        {
            dgvItemList.Columns["SlNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["ItemName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["ParticularsHsnCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["QTY"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["UNIT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["RATE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["TOTALAMOUNT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["TAXABLEVALUE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItemList.Columns["TotalWithTax"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void dgvItemList_Paint(object sender, PaintEventArgs e)
        {
            #region Assign Array
            string[] array = new string[4];
            int[] ary = new int[4];
            int length = 0;
            if (ORG_Tools._IsRegularGST)
            {
                if (mInvoiceType == "Regular")
                {
                    if (mIsIGST)
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
                DataGridViewCell hc = dgvItemList.Columns[ary[i]].HeaderCell;
                Rectangle hcRct = dgvItemList.GetCellDisplayRectangle(hc.ColumnIndex, -1, true);

                //For column wise create Width means how many column are span;
                int multiHeaderWidth = dgvItemList.Columns[hc.ColumnIndex].Width + dgvItemList.Columns[hc.ColumnIndex + 1].Width;
                //Rectengle x,y location and Hight and Width Set;
                Rectangle headRct = new Rectangle(hcRct.Left, 2, multiHeaderWidth, dgvItemList.ColumnHeadersHeight / 2);

                SizeF sz = e.Graphics.MeasureString(array[i], dgvItemList.Font);
                int headerTop = Convert.ToInt32((headRct.Height / 2) - (sz.Height / 2)) + 3;
                //Rectengle clor;
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), headRct);
                ////border draw
                e.Graphics.DrawRectangle(Pens.LightGray, headRct);
                //For Text Design and location;
                e.Graphics.DrawString(array[i], new Font("Microsoft Sans Serif", 8f), Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
                dgvItemList.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8f);
                dgvItemList.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7f);

                // e.Graphics.DrawString(array[i], dgvitemList.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
            }
        }
        private void GenerateGridForNonGSTType()
        {
            if (ORG_Tools._IsRegularGST)
            {
                if (mInvoiceType == "Regular")
                {
                    dgvItemList.Columns["CESSRATE"].Visible = true;
                    dgvItemList.Columns["CESSAMOUNT"].Visible = true;
                    if (mIsIGST)
                    {
                        dgvItemList.Columns["CGSTRATE"].Visible = false;
                        dgvItemList.Columns["CGSTAMOUNT"].Visible = false;
                        dgvItemList.Columns["SGSTRATE"].Visible = false;
                        dgvItemList.Columns["SGSTAMOUNT"].Visible = false;
                        dgvItemList.Columns["IGSTRATE"].Visible = true;
                        dgvItemList.Columns["IGSTAMOUNT"].Visible = true;

                        lblTotalIGST.BringToFront();
                        lblTotalCGST.Text = "";
                    }
                    else
                    {
                        dgvItemList.Columns["CGSTRATE"].Visible = true;
                        dgvItemList.Columns["CGSTAMOUNT"].Visible = true;
                        dgvItemList.Columns["SGSTRATE"].Visible = true;
                        dgvItemList.Columns["SGSTAMOUNT"].Visible = true;
                        dgvItemList.Columns["IGSTRATE"].Visible = false;
                        dgvItemList.Columns["IGSTAMOUNT"].Visible = false;
                        lblTotalSGST.BringToFront();
                        //lblTotalCGST.Text = "---";
                    }
                }
                else
                {
                    dgvItemList.Columns["CGSTRATE"].Visible = false;
                    dgvItemList.Columns["CGSTAMOUNT"].Visible = false;
                    dgvItemList.Columns["SGSTRATE"].Visible = false;
                    dgvItemList.Columns["SGSTAMOUNT"].Visible = false;
                    dgvItemList.Columns["IGSTRATE"].Visible = false;
                    dgvItemList.Columns["IGSTAMOUNT"].Visible = false;
                    dgvItemList.Columns["CESSRATE"].Visible = false;
                    dgvItemList.Columns["CESSAMOUNT"].Visible = false;
                    lblTotalSGST.Text = "";
                    lblTotalIGST.Text = "";
                    lblTotalCGST.Text = "";
                    lblTotalCESS.Text = "";
                }
            }
            else
            {
                dgvItemList.Columns["CGSTRATE"].Visible = false;
                dgvItemList.Columns["CGSTAMOUNT"].Visible = false;
                dgvItemList.Columns["SGSTRATE"].Visible = false;
                dgvItemList.Columns["SGSTAMOUNT"].Visible = false;
                dgvItemList.Columns["IGSTRATE"].Visible = false;
                dgvItemList.Columns["IGSTAMOUNT"].Visible = false;
                dgvItemList.Columns["CESSRATE"].Visible = false;
                dgvItemList.Columns["CESSAMOUNT"].Visible = false;
                dgvItemList.Columns["TAXABLEVALUE"].Visible = false;

                lblTotalCGST.Text = "";
                lblTotalSGST.Text = "";
                lblTotalIGST.Text = "";
                lblTotalCESS.Text = "";
            }
        }
        private void GenerateInvoiceNo()
        {
            string query = "Select max(SlNo) as slno from Invoice ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                try
                {
                    mInvoiceSlNo = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblInvoiceNo.Text = INVOICE_TOOLS._InvoiceString + mInvoiceSlNo.ToString();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.A))    ///Add New Customer
            {
                btnCustomer_Click(btnCustomer, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.A))  ///Edit Customer
            {
                #region Edit Customer
                if (!cmbCustomerName.Text.ISNullOrWhiteSpace())
                {
                    if (cmbCustomerName.Text != "CASH")
                    {
                        try
                        {
                            string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
                            LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Customer, LedgerDetails._Type.showDialog, customerID);
                            frm.OnClose += Frm_OnClose;
                            frm.ShowDialog();
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        MessageBox.Show("Cash customer not editable.", "Customer Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("Select customer name.", "Customer Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                }
                #endregion
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.I)) ///Add New Item
            {
                btnItemADD_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.I)) ///Edit Item
            {
                #region Edit Item
                if (!cmbItemName.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                        ItemEntry frm = new ItemEntry(itemID);
                        frm.onclose += item_onclose;
                        frm.ShowDialog();
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("Select item name.", "Item Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                }
                #endregion
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void GetDetails()
        {
            int index = cmbCustomerName.FindStringExact(cmbCustomerName.Text);
            if (index >= 0)
            {
                try
                {
                    cmbCustomerName.SelectedIndex = index;
                    string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
                    mCustomerLadgerID = customerID;

                    if (cmbCustomerName.Text != "CASH")
                    {
                        chkPayment.Visible = false;
                        chkPayment.Checked = false;
                        cmbBillingTerms.Enabled = true;
                        btnAddBillingTerms.Enabled = true;
                        dtpDueDate.Enabled = true;
                        cmbBillingTerms.Text = "ON RECEIPT";

                        pnlCashCustomerDetails.Visible = false;
                        GetBillingDetails(customerID);
                        GetCustomerShippedDetails(customerID);
                        cmbBillingTerms.Text = LedgerTools.GetBillingTerms(customerID);
                    }
                    else
                    {
                        pnlCashCustomerDetails.Visible = true;
                        cmbState.AddState();
                        cmbState.Text = ORG_Tools._State;
                        cmbBillingTerms.Enabled = false;
                        btnAddBillingTerms.Enabled = false;
                        dtpDueDate.Enabled = false;
                        cmbBillingTerms.Text = "ON RECEIPT";
                        GetBillingDetails();
                    }
                }
                catch (Exception) { }
            }
            else
            {
                cmbCustomerName.Text = "";
            }

        }
        private void cmbCustomerName_Leave(object sender, EventArgs e)
        {
            if (mInvoiceNoForEdit.ISNullOrWhiteSpace())
            {
                ResetData();
                GetDetails();
            }
            else
            {
                if (mPreviousCustomername != "CASH" && cmbCustomerName.Text == "CASH")
                {
                    MessageBox.Show("Sorry You Can't Set Cash Customer", "Customer Change", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbCustomerName.Text = mPreviousCustomername;
                    GetDetails();
                }
                else
                {
                    GetDetails();
                }
            }

        }
        private void GetBillingDetails()
        {
            mCustomerBillingName = txtBillingName.Text.ISNullOrWhiteSpace() ? cmbCustomerName.Text.GetDBFormatString() : txtBillingName.Text.GetDBFormatString();
            mCustomerbillingaddress = txtBillingAddress.Text.GetDBFormatString();
            mbillingstatecode = cmbState.Text.ISNullOrWhiteSpace() ? "" : ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
            mbillingstatename = cmbState.Text;

            mIsIGST = (ORG_Tools._StateCode == mbillingstatecode) ? false : true;
            GenerateGridForNonGSTType();
        }
        /// <summary>
        /// Billing and Shipping Details
        /// </summary>
        /// <param name="customerID"></param>
        private void Frm_OnClose(string customer)
        {
            cmbCustomerName.AddCashCustomers();
            cmbCustomerName.Text = customer;
            cmbCustomerName_Leave(cmbCustomerName, null);
        }
        private void ResetData()
        {
            mDescriptionSlno = 1;
            dgvItemList.Rows.Clear();
            cmbItemName.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            txtRate.Clear();
            txtAmount.Clear();
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            GenerateTotal();
        }
        private void GetBillingDetails(string customerID)
        {
            string billingName = "", billingAddress = "", billingStateName = "", billingStateCOde = "", gstNo = "";
            LedgerTools.GetLadgerBillingDetails(customerID, out billingName, out billingAddress, out billingStateName, out billingStateCOde, out gstNo);

            mCustomerBillingName = billingName;
            mCustomerbillingaddress = billingAddress;
            mbillingGstinnumber = gstNo;
            mbillingstatecode = billingStateCOde;
            mbillingstatename = billingStateName;

            mIsIGST = (ORG_Tools._StateCode == billingStateCOde) ? false : true;
            GenerateGridForNonGSTType();
        }
        private void GetCustomerShippedDetails(string customerID)
        {
            string shippingname = "", shippingaddress = "", shippingStateName = "", shippingStateCOde = "", gstNo = "";
            LedgerTools.GetLadgerShippingDetails(customerID, out shippingname, out shippingaddress, out shippingStateName, out shippingStateCOde, out gstNo);

            mshippingname = shippingname;
            mshippingaddress = shippingaddress;
            mShippinggstno = gstNo;
            mshippingStatecode = shippingStateCOde;
            mshippingstatename = shippingStateName;
        }
        ///
        /// Order details
        private string GetBuyerOrderDate(string slno)
        {
            string query = "Select OrderDate from salesorder Where slno='" + slno + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        private string GetBuyerOrderNo(string slno)
        {
            string query = "Select CustomerOrderNo from salesorder Where slno='" + slno + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }

        /// <summary>
        /// Item section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            if (!cmbItemName.Text.ISNullOrWhiteSpace())
            {
                int index = cmbItemName.FindStringExact(cmbItemName.Text);
                if (index >= 0)
                {
                    cmbItemName.SelectedIndex = index;
                    string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                    AddBatchNos(itemid);
                }
                else
                {
                    cmbItemName.Text = "";
                }
            }
        }
        private void AddBatchNos(string itemID)
        {
            cmbBatchNo.Items.Clear();
            string query = "Select BatchNo,HighestStockQty from StockSummary " +
                        "where itemid='" + itemID + "' order by id asc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double stock = 0d;

                    string hqty = item["HighestStockQty"].ToString();
                    hqty = hqty.Replace(",", "");
                    double.TryParse(hqty, out stock);
                    string batchNo = item["BatchNo"].ToString();
                    if (stock > 0)
                    {
                        cmbBatchNo.Items.Add(batchNo);
                    }
                }
                if (cmbBatchNo.Items.Count > 0)
                {
                    cmbBatchNo.SelectedIndex = 0;
                }
            }
        }
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Clear();
            if (!cmbUnit.Text.ISNullOrWhiteSpace())
            {
                GetSaleRate(cmbUnit.Text);
                CalculateAmount();
            }
        }
        private void CheckStockAvilabelity()
        {
            double qty = 0d;
            string qtystr = txtQuantity.Text.Replace(",","");;
            qtystr = qtystr.Replace(",", "");
            double.TryParse(qtystr, out qty);
            double saleQty = GetTotalQtyOfMinUnit(mStockSummaryID, cmbUnit.Text, qty);
            if (mAvailabelQty < saleQty)
            {
                MessageBox.Show("Stock not available to sale " + qty + " " + cmbUnit.Text, "Out of Stok", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantity.Clear();
                txtQuantity.Focus();
            }
        }
        private void ShowUnitDetails()
        {
            //string query = "Select * from StockMoreUnit Where UnitMoreID='" +
            //               mUnitMoreID + "' and Unit='" + cmbUnit.Text + "'";
            //DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            //if (dt.IsValidDataTable())
            //{
            //    string unit = dt.Rows[0]["Unit"].ToString();
            //    string qty = dt.Rows[0]["Qty"].ToString();
            //    string unitOfQty = dt.Rows[0]["UnitOfQty"].ToString();
            //    lblUnitDescription.Text = "1 " + unit + " = " + qty + " " + unitOfQty;
            //}
        }
        private void GetSaleRate(string unit)
        {
            if (unit == mHighestUnit)
            {
                txtRate.Text = mHighestSalesRate.toRound();
                lblAvlQty.Text = mHighestStockQty + " " + mHighestUnit;
                mHighestStockQty = mHighestStockQty.Replace(",", "");
                double.TryParse(mHighestStockQty, out mmaxqty);
                lblUnitDescription.Text = mHighestMesureUnit.ISNullOrWhiteSpace() ? "" : mLowestMesureUnit.ISNullOrWhiteSpace() ? "1 " + mHighestUnit + " = " + mHighestMesureUnit + " " + mMiddleUnit : "";
            }
            else if (unit == mMiddleUnit)
            {
                txtRate.Text = mMiddlesalesRate.toRound();
                lblAvlQty.Text = mMiddleStockQty + " " + mMiddleUnit;
                mMiddleStockQty = mMiddleStockQty.Replace(",", "");
                double.TryParse(mMiddleStockQty, out mmaxqty);
                lblUnitDescription.Text = mLowestMesureUnit.ISNullOrWhiteSpace() ? "" : "1 " + mMiddleUnit + " = " + mLowestMesureUnit + " " + mLowestUnit;
            }
            else
            {
                txtRate.Text = mLowestSalesRate.toRound();
                lblAvlQty.Text = mLowestQty + " " + mLowestUnit;
                mLowestQty = mLowestQty.Replace(",", "");
                double.TryParse(mLowestQty, out mmaxqty);
            }
        }
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
            //CheckStockAvilabelity();
        }
        //private void GetItemBatchNo(string itemid)
        //{
        //    cmbBatchNo.Items.Clear();
        //    string query = "Select BatchNo,id,itemid,qty1 from StockSummary where itemid='" + itemid + "' order by id asc";
        //    DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
        //    if (dt.IsValidDataTable())
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            int j = 0;

        //            double stock = item["qty1"].ToString().ISNullOrWhiteSpace() ? 0 : double.Parse(item["qty1"].ToString());
        //            string idandbatch = item["id"].ToString() + item["itemid"].ToString();
        //            foreach (var i in batchList)
        //            {
        //                if (i == idandbatch)
        //                {
        //                    j = 1;
        //                    break;
        //                }
        //            }
        //            if (j == 0)
        //            {
        //                if (stock > 0)
        //                {
        //                    cmbBatchNo.Items.Add(item["BatchNo"].ToString());
        //                    cmbBatchNo.SelectedIndex = 0;
        //                }
        //            }
        //        }
        //    }
        //    if (cmbBatchNo.Items.Count != 0)
        //    {
        //        cmbBatchNo.Enabled = true;
        //    }
        //}
        private void btnItemADD_Click(object sender, EventArgs e)
        {
            ItemEntry frmItemEntry = new ItemEntry();
            frmItemEntry.onclose += new Action<string>(item_onclose);
            frmItemEntry.ShowDialog();
        }
        void item_onclose(string itemname)
        {
            cmbItemName.AddItem();
            cmbItemName.Text = itemname;
        }
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtQuantity.Text.Contains('.'))
                {
                    e.Handled = true;
                }
                else
                {
                    double currentQty = 0d;
                    string qtystr = txtQuantity.Text.Replace(",","");;
                    qtystr = qtystr.Replace(",", "");
                    double.TryParse(qtystr + e.KeyChar, out currentQty);
                    if (currentQty > mmaxqty)
                    {
                        e.Handled = true;
                        toolTip1.ToolTipIcon = ToolTipIcon.Warning;
                        toolTip1.ToolTipTitle = "Insufficient Stock";
                        toolTip1.Show("Not enough stock available.", txtQuantity, 1400);
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            batchList.Clear();
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            mtotalInvoiceAmount = 0d;
            lblTotalInvoiceAmount.Text = mtotalInvoiceAmount.toString();

            cmbItemName.SelectedIndex = -1;
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            cmbUnit.SelectedIndex = -1;
            txtRate.Clear();
            lblTotQuantity.Text = "----";
            lblTotAmount.Text = "----";
            lblTotalWithTax.Text = "----";
            lblTotalInvoiceAmount.Text = "----";
            lblTotalIGST.Text = "----";
            lblTotalDiscount.Text = "----";
            lblTotalCESS.Text = "----";
            lblTaxableAmountTotal.Text = "----";
            lblTotalCGST.Text = "----";
            lblTotalSGST.Text = "----";
            txtAmount.Clear();
        }
        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemList.SelectedCells.Count > 0 && e.RowIndex != -1)
            {
                if (dgvItemList.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    batchList.RemoveAt(e.RowIndex);
                    GenerateTotal();
                }
            }
        }
        private void GenerateTaxAmounts(int rowindex, double amount, double taxAmount)
        {
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
            string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
            string totalWithTax = "";
            string itemID = dgvItemList.Rows[rowindex].Cells["ItemID"].Value.ToString();
            object rateObj = dgvItemList.Rows[rowindex].Cells["Rate"].Value;

            ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGST, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                            out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

            dgvItemList.Rows[rowindex].Cells["TotalAmount"].Value = amount.toString();
            dgvItemList.Rows[rowindex].Cells["TAXABLEVALUE"].Value = taxAmount.toString();
            dgvItemList.Rows[rowindex].Cells["CgstAmount"].Value = cgstAmount;
            dgvItemList.Rows[rowindex].Cells["SgstAmount"].Value = sgstAmount;
            dgvItemList.Rows[rowindex].Cells["CESSAmount"].Value = cessAmount;
            dgvItemList.Rows[rowindex].Cells["IgstAmount"].Value = igstAmount;
            dgvItemList.Rows[rowindex].Cells["TotalWithTax"].Value = totalWithTax;
        }
        private void RegenerateItemDiscountAndTax(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                object qtyObj = dgvItemList.Rows[rowIndex].Cells["Qty"].Value;
                object rateObj = dgvItemList.Rows[rowIndex].Cells["Rate"].Value;
                object disAmountObj = dgvItemList.Rows[rowIndex].Cells["DISCOUNTAMOUNT"].Value;
                int qty = 0;
                double rate = 0d;
                double disAmount = 0d;
                double amount = 0d, taxAmount = 0d;
                double totalDiscount = 0d;
                try
                {
                    qty = qtyObj.ISValidObject() ? int.Parse(qtyObj.ToString()) : 0;
                    rate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString().Replace(",", "")) : 0d;
                    disAmount = disAmountObj.ISValidObject() ? double.Parse(disAmountObj.ToString().Replace(",", "")) : 0d;
                }
                catch (Exception) { }
                totalDiscount = qty * disAmount;
                amount = qty * rate;
                taxAmount = (qty * rate) - totalDiscount;
                GenerateTaxAmounts(rowIndex, amount, taxAmount);
                GenerateTotal();
            }
        }
        private void SumOfCharges()
        {
            double totalamount = 0d, freightamount = 0d, packingamount = 0d, othersamount = 0d, invoiceamount = 0d, overallDiscount = 0d;
            string totalWithTaxstr = lblTotalWithTax.Text;
            string freightchargesstr = txtfreightcharges.Text;
            string OthersChargesstr = txtOthersCharges.Text;
            string packingChargesstr = txtPackingCharges.Text;
            string discountstr = txtDiscount.Text;

            totalWithTaxstr = totalWithTaxstr.Replace(",", "");
            freightchargesstr = freightchargesstr.Replace(",", "");
            OthersChargesstr = OthersChargesstr.Replace(",", "");
            packingChargesstr = packingChargesstr.Replace(",", "");
            discountstr = discountstr.Replace(",", "");

            double.TryParse(totalWithTaxstr, out totalamount);
            double.TryParse(freightchargesstr, out freightamount);
            double.TryParse(OthersChargesstr, out othersamount);
            double.TryParse(packingChargesstr, out packingamount);
            double.TryParse(discountstr, out overallDiscount);

            invoiceamount = (totalamount + freightamount + packingamount + othersamount) - overallDiscount;
            mtotalInvoiceAmount = invoiceamount;
            mtotalInvoiceAmount = Math.Round(mtotalInvoiceAmount);

            lblTotalInvoiceAmount.Text = mtotalInvoiceAmount.toString();
        }
        private void SalesOrderDetailsDataretrive(string slno)
        {
            string query2 = "Select * from SalesOrderDetails inner join SalesOrder on  SalesOrderDetails.orderid=SalesOrder.orderid Where slno = '" + slno + "'";
            DataTable DT2 = SQLHelper.GetInstance().ExcuteNonQuery(query2, out msg);
            if (DT2.IsValidDataTable())
            {
                foreach (DataRow item in DT2.Rows)
                {
                    string hsnCode = item["ComodityCode"].ToString();
                    string itemID = item["ItemId"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string qty = item["Qty"].ToString().Replace(",","");
                    string unit = item["Unit"].ToString();
                    string rateStr = item["Rate"].ToString().Replace(",", ""); ;
                    string amountStr = item["Amount"].ToString();

                    object disRateStr = item["DiscountRate"];
                    string discountRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString().Replace(",", "")).toString() : "";
                    object disAmountStr = item["DiscountAmount"];
                    string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString().Replace(",", "")).toString() : "";

                    string taxAmountStr = item["TaxAmount"].ToString();
                    string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString().Replace(",", "")).toString() : "";
                    double taxamount;
                    taxAmount = taxAmount.Replace(",", "");
                    double.TryParse(taxAmount, out taxamount);

                    double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr.Replace(",", ""));
                    double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr.Replace(",", ""));
                    string stocksummaryid = item["StockSummaryId"].ToString();


                    string currentItemGstType = "";
                    string currentInvoiceType = ItemTools.IsTaxBillByItem(itemID, out currentItemGstType);
                    mInvoiceType = currentInvoiceType;
                    mPreviousItemGSTType = currentItemGstType;
                    GenerateGridForNonGSTType();
                    GetBatchDetails(itemID, stocksummaryid, unit, qty, discountRate, itemName, hsnCode);
                    if (dgvItemList.RowCount <= 0)
                    {
                        morderslno = "";
                    }
                }
                if (dgvItemList.RowCount <= 0)
                {
                    morderslno = "";
                }
                else
                {
                    GenerateTotal();
                    SumOfCharges();
                }

            }
        }
        private void GetBatchDetails(string itemid, string summaryid, string unit, string orderqtystr, string discountRATE, string itemname, string hsncode)
        {
            string query = "select id,BatchNo,itemid,HighestStockQty,HighestRate,HighestUnit,MiddleUnit,MiddleStockQty," +
            "MiddleRate,LowestRate,LowestStockQty,LowestUnit,HighestMeasureQty,LowestMeasureQty from StockSummary where" +
            " ItemID='" + itemid + "' and (HighestUnit='" + unit + "'or MiddleUnit='" + unit + "' or LowestUnit='" + unit
            + "') and ID='" + summaryid + "'  union all  select id,BatchNo,itemid,HighestStockQty,HighestRate,HighestUnit," +
            "MiddleUnit,MiddleStockQty,MiddleRate,LowestRate,LowestStockQty,LowestUnit,HighestMeasureQty,LowestMeasureQty" +
            " from StockSummary where ItemID='" + itemid + "' and (HighestUnit='" + unit + "'or MiddleUnit='" + unit
            + "' or LowestUnit='" + unit + "') and ID<>'" + summaryid + "' ";

            mdt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (mdt.IsValidDataTable())
            {
                int temp = 0, temp2 = 0;
                double orderqty, stockqty;
                orderqtystr = orderqtystr.Replace(",", "");
                double.TryParse(orderqtystr, out orderqty);
                foreach (DataRow row in mdt.Rows)
                {
                    string batchno = row["BatchNo"].ToString();
                    string id = row["id"].ToString();

                    mHighestUnit = row["HighestUnit"].ToString();
                    mMiddleUnit = row["MiddleUnit"].ToString();
                    mLowestUnit = row["LowestUnit"].ToString();

                    mHighestStockQty = row["HighestStockQty"].ToString();
                    mMiddleStockQty = row["MiddleStockQty"].ToString();
                    mLowestQty = row["LowestStockQty"].ToString();

                    mHighestSalesRate = row["HighestRate"].ToString();
                    mMiddlesalesRate = row["MiddleRate"].ToString();
                    mLowestSalesRate = row["LowestRate"].ToString();

                    mHighestMesureUnit = row["HighestMeasureQty"].ToString();
                    mLowestMesureUnit = row["LowestMeasureQty"].ToString();

                    string stockqtystr = unit == mHighestUnit ? mHighestStockQty : unit == mMiddleUnit ? mMiddleStockQty : mLowestQty;
                    stockqtystr = stockqtystr.Replace(",", "");
                    double.TryParse(stockqtystr, out stockqty);
                    double useqtytemp = 0d;
                    if (orderqty > 0 && stockqty > 0)
                    {
                        if (orderqty <= stockqty)
                        {
                            useqtytemp = orderqty;
                            orderqty = 0;

                        }
                        else
                        {
                            temp = 1;
                            useqtytemp = orderqty - (orderqty - stockqty);
                            orderqty = orderqty - stockqty;
                        }
                        #region Addgrid

                        string ratestr = unit == mHighestUnit ? mHighestSalesRate : unit == mMiddleUnit ? mMiddlesalesRate : mLowestSalesRate;
                        double rate = 0d;
                        ratestr = ratestr.Replace(",", "");
                        double.TryParse(ratestr, out rate);
                        double disRATE = 0d, disAmount = 0d;
                        discountRATE = discountRATE.Replace(",", "");
                        double.TryParse(discountRATE, out disRATE);
                        disAmount = rate * (disRATE / 100);
                        double amount = rate * useqtytemp;
                        string taxamountstr = ((rate - disAmount) * useqtytemp).toString();
                        double taxamount = double.Parse(taxamountstr.Replace(",", ""));
                        string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
                        string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
                        string totalWithTax = "";
                        ItemTools.GetItemGSTRateAndAmount(itemid, mIsIGST, taxamount, out cgstRate, out cgstAmount, out sgstRate,
                                                            out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

                        dgvItemList.Rows.Add(mDescriptionSlno, itemid, id, itemname, hsncode, useqtytemp, unit, rate, amount.toString(), disRATE,
                                                disAmount, taxamount.toString(), cgstRate,
                                                cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);
                        batchList.Add(id + itemid);

                        DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                        btnCelCol.ToolTipText = "Delete";
                        btnCelCol.Value = "Delete";
                        btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                        //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                        dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                        mDescriptionSlno++;
                        #endregion
                        if (temp == 1 && temp2 == 0)
                        {
                            temp2 = 1;
                            if ((MessageBox.Show("Item " + itemname + " batch No " + batchno + ", out of stock.\nDo you want add another batch??", "Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.No)
                            {
                                return;
                            }
                        }

                    }
                }
                if (orderqty != 0)
                {
                    MessageBox.Show("Sorry!!!!" + itemname + " out of stock.\n" + orderqty + " " + unit + " Not added in the list.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void lblTotalWithTax_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }

        private void Invoice_Direct_Shown(object sender, EventArgs e)
        {
            if (!morderslno.ISNullOrWhiteSpace())
            {
                txtBuyerOrderno.Text = GetBuyerOrderNo(morderslno);
                dtmBuyerOrderDate.Text = GetBuyerOrderDate(morderslno); ;
                SalesOrderDetailsDataretrive(morderslno);
            }
        }
        private void CalculateDueAmount()
        {
            double dueamount = 0d, invoiceValue = 0d, paidamount = 0d;
            string totalInvoiceAmount = lblTotalInvoiceAmount.Text;
            string paidAmount = txtPaidAmount.Text;
            totalInvoiceAmount = totalInvoiceAmount.Replace(",", "");
            paidAmount = paidAmount.Replace(",", "");
            double.TryParse(totalInvoiceAmount, out invoiceValue);
            double.TryParse(paidAmount, out paidamount);
            dueamount = invoiceValue - paidamount;
            lblDueAmount.Text = dueamount.toString();
        }
        private void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateDueAmount();
        }
        private void label37_Click(object sender, EventArgs e)
        {

        }
        private void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            txtChequeNo.ForeColor = Color.Red;
            if (txtChequeNo.Text.Length == 6)
            {
                txtChequeNo.ForeColor = Color.Black;
            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
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
                cmbBank.AddBank();
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
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtBillingName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBalance.Text = "Rs. ";
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string ledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                double balance = AccountHeadTools.GetCurrentBalance(ledgerid);
                lblBalance.Text = "Rs. " + balance.toString();
            }
        }
        private void chkPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPayment.Checked)
            {
                pnlItem.Enabled = false;
                txtfreightcharges.Enabled = false;
                txtPackingCharges.Enabled = false;
                txtOthersCharges.Enabled = false;
                txtDiscount.Enabled = false;
                //grbPayment.Enabled = true;
                grbPayment.Visible = true;
                cmbPaymentMethod.SelectedIndex = 0;
                lblDueAmount.Text = "0.00";
                txtPaidAmount.Text = lblTotalInvoiceAmount.Text;
            }
            else
            {
                txtfreightcharges.Enabled = true;
                txtPackingCharges.Enabled = true;
                txtOthersCharges.Enabled = true;
                txtDiscount.Enabled = true;
                pnlItem.Enabled = true;
                // grbPayment.Enabled = false;
                txtPaidAmount.Text = "";
                grbPayment.Visible = false;
                lblDueAmount.Text = lblTotalInvoiceAmount.Text;

            }
        }
        private void lblTotalInvoiceAmount_TextChanged(object sender, EventArgs e)
        {
            double totalinvoice = 0d;
            string totalInvoiceAmount = lblTotalInvoiceAmount.Text;
            totalInvoiceAmount = totalInvoiceAmount.Replace(",", "");
            double.TryParse(totalInvoiceAmount, out totalinvoice);
            if (totalinvoice > 0 && cmbCustomerName.Text == "CASH")
            {
                lblDueAmount.Text = lblTotalInvoiceAmount.Text;
                //chkPayment.Checked = true;
                chkPayment.Visible = true;
            }
            else
            {
                chkPayment.Checked = false;
                chkPayment.Visible = false;
                lblDueAmount.Text = lblTotalInvoiceAmount.Text;
            }
        }
        private void lblTotalSGST_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalInvoiceAmount_Click(object sender, EventArgs e)
        {

        }
        private void txtfreightcharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtfreightcharges.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtfreightcharges_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void txtPackingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPackingCharges.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtPaidAmount.Text.Contains('.'))
                {
                    e.Handled = true;
                }
                else
                {
                    double totamount = 0d, givenamount = 0d;
                    string totalInvoiceAmount = lblTotalInvoiceAmount.Text;
                    string paidAmount = txtPaidAmount.Text;
                    totalInvoiceAmount = totalInvoiceAmount.Replace(",", "");
                    paidAmount = paidAmount.Replace(",", "");
                    double.TryParse(totalInvoiceAmount, out totamount);
                    double.TryParse(paidAmount + e.KeyChar, out givenamount);
                    if (givenamount <= totamount) { e.Handled = false; }
                    else { e.Handled = true; }
                }
            }
            else { e.Handled = true; }
        }
        private void txtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else { e.Handled = true; }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        private void txtOthersCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtOthersCharges.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtDiscount.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            LedgerDetails ledgerdetails = new LedgerDetails(LedgerDetails._LedgerCategory.Customer, LedgerDetails._Type.showDialog);
            ledgerdetails.OnClose += Ledgerdetails_OnClose;
            ledgerdetails.ShowDialog();
        }
        private void Ledgerdetails_OnClose(string templatename)
        {
            cmbCustomerName.AddCashCustomers();
            cmbCustomerName.Text = templatename;
            cmbCustomerName_Leave(cmbCustomerName, null);
        }

        private void txtfreightcharges_Leave(object sender, EventArgs e)
        {
            txtfreightcharges.Text = txtfreightcharges.Text.toRound();
        }
        private void txtPackingCharges_Leave(object sender, EventArgs e)
        {
            txtPackingCharges.Text = txtPackingCharges.Text.toRound();
        }

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtOthersCharges_Leave(object sender, EventArgs e)
        {
            txtOthersCharges.Text = txtOthersCharges.Text.toRound();
        }
        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            txtDiscount.Text = txtDiscount.Text.toRound();
        }

        private void txtBillingName_Leave(object sender, EventArgs e)
        {
            GetBillingDetails();
        }
        private void txtBillingAddress_Leave(object sender, EventArgs e)
        {
            GetBillingDetails();
        }
        private void cmbState_Leave(object sender, EventArgs e)
        {
            GetBillingDetails();
        }
        private void ShowBatchDetails()
        {
            if (cmbUnit.Items.Count > 0)
            {
                if (cmbUnit.DataSource != null)
                {
                    cmbUnit.DataSource = null;
                }
                else
                {
                    cmbUnit.Items.Clear();
                }
            }
            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string batchNo = cmbBatchNo.Text;
            string query = "Select * from StockSummary where itemid='" + itemid + "' and BatchNo='" + batchNo + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
            if (dt.IsValidDataTable())
            {
                mStockSummaryID = dt.Rows[0]["id"].ToString();

                mHighestUnit = dt.Rows[0]["HighestUnit"].ToString();
                mHighestSalesRate = dt.Rows[0]["HighestRate"].ToString();
                mHighestStockQty = dt.Rows[0]["HighestStockQty"].ToString();

                mMiddleUnit = dt.Rows[0]["MiddleUnit"].ToString();
                mMiddlesalesRate = dt.Rows[0]["MiddleRate"].ToString();
                mMiddleStockQty = dt.Rows[0]["MiddleStockQty"].ToString();

                mLowestUnit = dt.Rows[0]["LowestUnit"].ToString();
                mLowestQty = dt.Rows[0]["LowestStockQty"].ToString();
                mLowestSalesRate = dt.Rows[0]["LowestRate"].ToString();

                mHighestMesureUnit = dt.Rows[0]["HighestMeasureQty"].ToString();
                mLowestMesureUnit = dt.Rows[0]["LowestMeasureQty"].ToString();
                mHighestStockQty = mHighestStockQty.Replace(",", "");
                double.TryParse(mHighestStockQty, out mAvailabelQty);
                lblAvlQty.Text = mAvailabelQty.ToString() + " " + mHighestUnit;
                if (mAvailabelQty <= 0)
                {
                    MessageBox.Show("Item out of stock.", "Out Of Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnAdd.Enabled = false;
                }
                else
                {
                    // GetUnit(mUnitMoreID);
                    if (!mHighestUnit.ISNullOrWhiteSpace())
                    {
                        cmbUnit.Items.Add(mHighestUnit);
                        if (!mMiddleUnit.ISNullOrWhiteSpace())
                        {
                            cmbUnit.Items.Add(mMiddleUnit);
                            if (!mLowestUnit.ISNullOrWhiteSpace())
                            {
                                cmbUnit.Items.Add(mLowestUnit);
                            }
                        }
                    }
                    cmbUnit.Text = mHighestUnit;
                    btnAdd.Enabled = true;
                }
            }
        }
        private void GetUnit(string unitMoreID)
        {
            cmbUnit.Items.Clear();
            string query = "Select * from StockMoreUnit where UnitMoreID='" + unitMoreID + "' order by ID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    cmbUnit.Items.Add(item["Unit"].ToString());
                }
            }
        }
        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbBatchNo.Text.ISNullOrWhiteSpace())
            {
                ShowBatchDetails();
            }
        }

        private bool IsvalidEntry()
        {
            if (chkPayment.Checked)
            {
                if (double.Parse(lblDueAmount.Text.Replace(",", "")) != 0)
                {
                    MessageBox.Show("Please paid full invoice amount.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPaidAmount.Select();
                    return false;
                }
                if (cmbPaymentMethod.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select payment method for payment.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbPaymentMethod.Select();
                    return false;
                }
                if (cmbPaymentAccount.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select payment account.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbPaymentAccount.Select();
                    return false;
                }
                if (cmbPaymentMethod.Text == "Cheque")
                {
                    if (cmbBank.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Select Bank name.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbBank.Select();
                        return false;
                    }
                    if (txtChequeNo.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter cheqe No.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtChequeNo.Focus();
                        return false;
                    }
                    else if (txtChequeNo.Text.Length != 6)
                    {
                        MessageBox.Show("Enter valid cheqe No.", " Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtChequeNo.Focus();
                        return false;
                    }
                }
                if (txtPaidAmount.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter amount to paid.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPaidAmount.Focus();
                    return false;
                }
                else
                {
                    if (double.Parse(txtPaidAmount.Text.Replace(",", "")) <= 0)
                    {
                        MessageBox.Show("Zero amount can not paid.\n Please enter more than zero to paid.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtPaidAmount.Focus();
                        return false;
                    }
                }
            }
            if (!dtpInvoiceDate.Value.IsValidDate())
            {
                dtpInvoiceDate.Focus();
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Item not found to create invoice.", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (!mInvoiceNoForEdit.ISNullOrWhiteSpace() && mPreviosStateCode != mbillingstatecode)
            {
                MessageBox.Show("Sorry.you can't change customer. \n Cause :- previous customer state and present customer state are mismatch..", "Change customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                RetruveDataFromInvoice();
                return false;
            }
            return true;
        }
        private void Invoice_Direct_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            chkPayment.Checked = cmbCustomerName.Text == "CASH" ? true : false;
            if (IsvalidEntry())
            {
                if (mInvoiceNoForEdit.ISNullOrWhiteSpace() && (IsExsistAdvanceReceipt() || IsExsistCreditNote()))
                {
                    AdvancePaymentAndCreditNoteLIST adcrlist = new AdvancePaymentAndCreditNoteLIST(lblInvoiceNo.Text, lblTotalInvoiceAmount.Text, mCustomerLadgerID);
                    adcrlist.OnClose += Adcrlist_OnClose;
                    adcrlist.ShowDialog();
                }
                else
                {
                    if (cmbCustomerName.Text == "CASH" || !IsMoreThanPaid())
                    {
                        string totalInvoiceAmount = lblTotalInvoiceAmount.Text;
                        totalInvoiceAmount = totalInvoiceAmount.Replace(",", "");
                        double.TryParse(totalInvoiceAmount, out mDueAmount);
                        if (!mInvoiceNoForEdit.ISNullOrWhiteSpace() && cmbCustomerName.Text == "CASH")
                        {
                            mDueAmount = 0;
                        }
                        mStatus = mDueAmount > 0 ? "Due" : "Paid";
                        if (MessageBox.Show("Are you confirm?", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (InvoiceAlerts())
                            {
                                SaveInvoice();
                            }
                        }
                    }
                }
            }

        }
        private bool IsMoreThanPaid()
        {
            double totalinvoicevalue = 0d, paymentamount = 0d;
            string totalInvoiceAmount = lblTotalInvoiceAmount.Text;
            totalInvoiceAmount = totalInvoiceAmount.Replace(",", "");
            double.TryParse(totalInvoiceAmount, out totalinvoicevalue);

            string query = "select sum(Amount) as amount from ReceiptPaymentStatus where VoucherType='InVoice' and  VoucharNo='" + lblInvoiceNo.Text + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                string objstr = obj.ToString();
                objstr = objstr.Replace(",", "");
                double.TryParse(objstr, out paymentamount);
                if (totalinvoicevalue < paymentamount)
                {
                    MessageBox.Show("Sorry customer already paid the maximaum amount from the invoice value.", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            mDueAmount = totalinvoicevalue - paymentamount;
            return false;
        }

        private bool InvoiceAlerts()
        {
            if (ORG_Tools._IsCompositGST && mbillingstatename != ORG_Tools._State)
            {
                MessageBox.Show("You can't sale Interstate.\nBecause you are a Composite Dealer of " + ORG_Tools._State
                                    + " and your customer state is " + mbillingstatename + ".", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (mbillingGstinnumber.ISNullOrWhiteSpace() && mtotalInvoiceAmount > 10000)
            {
                string alertMsg = Customertools.CustomerAddressAndPANValidation(mCustomerLadgerID);
                if (!alertMsg.ISNullOrWhiteSpace())
                {
                    DialogResult dr = MessageBox.Show(alertMsg, "Alert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No)
                    {
                        string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
                        LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Customer, LedgerDetails._Type.showDialog, customerID);
                        frm.OnClose += Frm_OnClose;
                        frm.ShowDialog();
                        return false;
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
                return true;
            }
            if (mtotalInvoiceAmount > 50000)
            {
                MessageBox.Show("E-Way Bill No required. To continue without e-way bill no click OK.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void InvoiceCancel()
        {
            if (MessageBox.Show("Are Sure to cancel this Invoice?", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mlistQuery.Clear();
                string salesLedgerId = AccountHeadTools._SalesLedgerId;
                string invoiceNo = lblInvoiceNo.Text.GetDBFormatString();
                string invoiceDate = dtpInvoiceDate.Text;
                double totalInvoiceAmount = 0d;
                string totalInvoiceAmountstr = lblTotalInvoiceAmount.Text;
                totalInvoiceAmountstr = totalInvoiceAmountstr.Replace(",", "");
                double.TryParse(totalInvoiceAmountstr, out totalInvoiceAmount);

                string transectiontype = "Invoice";
                string drledgerid = mCustomerLadgerID;
                string crledgerid = salesLedgerId;

                mquery = "Update invoice set TotalInvoiceAmount='0',DueAmount='0', Status='Cancel' where invoiceNo='" + mInvoiceNoForEdit + "'";
                mlistQuery.Add(mquery);

                InsertOrUpdateTransection("", invoiceDate, invoiceNo, "0.00", drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL", "NULL");

                #region CurrentBalanceRestore

                mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(crledgerid, mPreviousCustomerID, mtotalPreviosInvoiceAmount.toString(), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistQuery.Add(mquery);
                #endregion
                if (cmbCustomerName.Text == "CASH")
                {
                    #region Restore Amount
                    mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mtotalPreviosInvoiceAmount.toString(), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(mquery);
                    #endregion
                    transectiontype = "Receipt_Payment";
                    drledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                    crledgerid = mCustomerLadgerID;
                    string receiptNo = GetReceiptNoUsingTransectionId();
                    string mode = "'" + cmbPaymentMethod.Text + "'";
                    string bankname = cmbPaymentMethod.Text == "Cheque" ? "'" + cmbBank.Text.GetDBFormatString() + "'" : "NULL";
                    string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
                    string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
                    string narration = "'" + invoiceNo + "'";

                    InsertOrUpdateTransection("", invoiceDate, receiptNo, "0.00", drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate, narration);
                    mquery = "update ReceiptPaymentStatus set Amount='0.00' where VoucherType='InVoice' and  VoucharNo='" + lblInvoiceNo.Text + "'";
                    mlistQuery.Add(mquery);
                }

                foreach (DataRow item in mdtFroRestore.Rows)
                {
                    string itemid = item["ItemId"].ToString();
                    string stockSummaryId = item["StockSummaryId"].ToString();
                    string quantity = item["Qty"].ToString().Replace(",","");
                    string unit = item["Unit"].ToString();
                    mlistQuery.Add(UpdateStockSummaryForRestoring(itemid, stockSummaryId, quantity, unit));
                }
                if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
                {
                    MessageBox.Show("Invoice Cancelled .", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invoice Cancelled Faild.\n" + msg, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void SaveInvoice()
        {
            #region data
            string salesLedgerId = AccountHeadTools._SalesLedgerId;

            string invoiceNo = lblInvoiceNo.Text.GetDBFormatString();
            string invoiceDate = dtpInvoiceDate.Text;
            string billingTerms = cmbBillingTerms.Text;
            string dueDate = dtpDueDate.Text;

            string billingTo = mCustomerBillingName;
            string billingAddress = mCustomerbillingaddress;
            string billingGSTIN = mbillingGstinnumber;
            string billingState = mbillingstatename;
            string billingStateCode = mbillingstatecode;
            string buyerOrderDate = "NULL";
            string buyerOrderNo = txtBuyerOrderno.Text.GetDBFormatString();
            if (!buyerOrderNo.ISNullOrWhiteSpace())
            {
                buyerOrderDate = "'" + dtmBuyerOrderDate.Text + "'";
            }
            string shippingTo = mshippingstatename;
            string shippingAddress = mshippingaddress;
            string shippingState = mshippingstatename;
            string shippingStateCode = mshippingStatecode;

            string totalQty = lblTotQuantity.Text.Replace(",","");
            string totalAmount = lblTotAmount.Text.Replace(",", "");
            string totalDiscount = lblTotalDiscount.Text.Replace(",", "");
            string totalTaxAmount = lblTaxableAmountTotal.Text.Replace(",", "");
            string totalCGST = lblTotalCGST.Text.Replace(",", "");
            string totalSGST = lblTotalSGST.Text.Replace(",", "");
            string totalIGST = lblTotalIGST.Text.Replace(",", "");
            string totalCess = lblTotalCESS.Text.Replace(",", "");
            string totalNet = lblTotalWithTax.Text.Replace(",", "");
            string totalInvoiceAmountstr = lblTotalInvoiceAmount.Text.Replace(",", "");
            string freightchargesstr = txtfreightcharges.Text.Replace(",", "");
            string packingChargesstr = txtPackingCharges.Text.Replace(",", "");
            string othersChargesstr = txtOthersCharges.Text.Replace(",", "");
            string discountstr = txtDiscount.Text.Replace(",", "");

            double totalInvoiceAmount = 0d, freightCharges = 0d, pachingCharges = 0d, otherCharges = 0d, overAllDiscount = 0d;

            //totalInvoiceAmountstr = totalInvoiceAmountstr.Replace(",", "");
            //freightchargesstr = freightchargesstr.Replace(",", "");
            //packingChargesstr = packingChargesstr.Replace(",", "");
            //othersChargesstr = othersChargesstr.Replace(",", "");
            //discountstr = discountstr.Replace(",", "");

            double.TryParse(totalInvoiceAmountstr, out totalInvoiceAmount);
            double.TryParse(freightchargesstr, out freightCharges);
            double.TryParse(packingChargesstr, out pachingCharges);
            double.TryParse(othersChargesstr, out otherCharges);
            double.TryParse(discountstr, out overAllDiscount);

            string note = "";
            string transectionid = Guid.NewGuid().ToString();
            string transectiontype = "Invoice";
            string drledgerid = mCustomerLadgerID;
            string crledgerid = salesLedgerId;
            //mDueAmount = mtotalInvoiceAmount;
            // mStatus = "Due";
            string rcm = (mbillingGstinnumber.ISNullOrWhiteSpace()) ? "YES" : "NO";
            #endregion

            #region Insert 
            if (mInvoiceNoForEdit.ISNullOrWhiteSpace())
            {
                mquery = "Insert into invoice( SlNo, InvoiceNo, InvoiceDate, LedgerId, BillingTerms, DueDate, " +
                               "BillingTo, BillingAddress, BillingGSTNO, BillingState, BillingStateCode, BuyerOrderNo, " +
                               "BuyerOrderDate, ShippingTo, " +
                               "ShippingAddress, ShippingState, ShippingStateCode, TotalQty, TotalAmount, TotalDiscount, " +
                               "TotalTaxAmount, TotalCGST, TotalSGST, TotalIGST, TotalCess, NetAmount, FreightChargs, " +
                               "PackingCharges, OtherCharges, OverAllDiscount, TotalInvoiceAmount,Note,RCM,InvoiceType,DueAmount,Status) " +
                               "values(" + mInvoiceSlNo + ",'" + invoiceNo + "','" + invoiceDate + "','" +
                               mCustomerLadgerID + "','" + billingTerms + "','" + dueDate + "','" + billingTo + "','" +
                               billingAddress + "','" + billingGSTIN + "','" + billingState + "','" + billingStateCode
                               + "','" + buyerOrderNo + "'," + buyerOrderDate + ",'" + shippingTo + "','" + shippingAddress
                               + "','" + shippingState + "','" + shippingStateCode + "'," + totalQty + "," + totalAmount
                               + "," + totalDiscount + "," + totalTaxAmount + "," + totalCGST + "," + totalSGST + "," + totalIGST + "," +
                               totalCess + "," + totalNet + "," + freightCharges + "," + pachingCharges + "," + otherCharges + "," +
                               overAllDiscount + "," + totalInvoiceAmount + ",'" + note + "','" + rcm + "','" + mInvoiceType + "'," + mDueAmount + ",'" + mStatus + "')";
                mlistQuery.Add(mquery);
                InsertOrUpdateTransection(transectionid, invoiceDate, invoiceNo, totalInvoiceAmount.toString().Replace(",",""), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL", "NULL");
                #region CurrentBalanceUpdate

                mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalInvoiceAmount.toString().Replace(",",""), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistQuery.Add(mquery);
                #endregion

                if (chkPayment.Checked)
                {
                    transectionid = Guid.NewGuid().ToString();
                    transectiontype = "Receipt_Payment";
                    drledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                    crledgerid = mCustomerLadgerID;
                    string receiptNo = GetReceiptNo();
                    string status = "Due";
                    string mode = "'" + cmbPaymentMethod.Text + "'";
                    string bankname = cmbPaymentMethod.Text == "Cheque" ? "'" + cmbBank.Text.GetDBFormatString() + "'" : "NULL";
                    string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
                    string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
                    string dueAmountstr = lblDueAmount.Text;
                    dueAmountstr = dueAmountstr.Replace(",", "");
                    if (double.Parse(dueAmountstr) <= 0)
                    {
                        status = "Paid";
                    }
                    string narration = "'" + invoiceNo + "'";

                    InsertOrUpdateTransection(transectionid, invoiceDate, receiptNo, txtPaidAmount.Text.Replace(",", ""), drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate, narration);

                    #region CurrentBalanceUpdate

                    mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalInvoiceAmount.toString().Replace(",","").Replace(",","").Replace(",",""), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(mquery);
                    #endregion
                    #region Insert ReceiptPaymentStatus
                    mquery = "insert into ReceiptPaymentStatus( LastTransectionId, VoucherType, VoucharNo, Amount) values('" + transectionid + "','InVoice','" + invoiceNo + "','" + totalAmount + "')";
                    mlistQuery.Add(mquery);
                    #endregion

                    UpdateInvoiceLastTransectionId(invoiceNo, transectionid, status, lblDueAmount.Text);
                }
            }
            #endregion insert

            #region Update
            else
            {
                mquery = "update invoice set LedgerId='" + mCustomerLadgerID + "', InvoiceDate='" + invoiceDate + "', BillingTerms='" + billingTerms + "', DueDate='" + dueDate + "', " +
                               "BillingTo='" + billingTo + "', BillingAddress='" + billingAddress + "', BillingGSTNO='" + billingGSTIN
                               + "', BillingState='" + billingState + "', BillingStateCode='" + billingStateCode + "', BuyerOrderNo='" + buyerOrderNo
                               + "',BuyerOrderDate=" + buyerOrderDate + ", ShippingTo='" + shippingTo + "',ShippingAddress='" + shippingAddress
                               + "', ShippingState='" + shippingState + "', ShippingStateCode='" + shippingStateCode + "', TotalQty=" + totalQty
                               + ", TotalAmount=" + totalAmount + ", TotalDiscount=" + totalDiscount + ",TotalTaxAmount=" + totalTaxAmount
                               + ", TotalCGST=" + totalCGST + ", TotalSGST=" + totalSGST + ", TotalIGST=" + totalIGST + ", TotalCess=" + totalCess
                               + ", NetAmount=" + totalNet + ", FreightChargs=" + freightCharges + ",PackingCharges=" + pachingCharges
                               + ", OtherCharges=" + otherCharges + ", OverAllDiscount=" + overAllDiscount + ", TotalInvoiceAmount=" + totalInvoiceAmount
                               + ",Note='" + note + "',RCM='" + rcm + "',InvoiceType='" + mInvoiceType + "',DueAmount=" + mDueAmount + ",Status='" + mStatus
                               + "' where invoiceno='" + mInvoiceNoForEdit + "'";
                mlistQuery.Add(mquery);
                mquery = "delete from InvoiceDetails where invoiceno='" + mInvoiceNoForEdit + "'";
                mlistQuery.Add(mquery);
                invoiceNo = mInvoiceNoForEdit;

                InsertOrUpdateTransection(transectionid, invoiceDate, invoiceNo, totalInvoiceAmount.toString().Replace(",",""), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL", "NULL");
                #region CurrentBalanceRestore

                mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(crledgerid, mPreviousCustomerID, mtotalPreviosInvoiceAmount.toString(), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistQuery.Add(mquery);
                #endregion
                #region CurrentBalanceUpdate

                mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalInvoiceAmount.toString().Replace(",",""), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistQuery.Add(mquery);
                #endregion
                if (chkPayment.Checked)
                {
                    #region Restore Amount
                    mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mtotalPreviosInvoiceAmount.toString(), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(mquery);
                    #endregion

                    transectionid = Guid.NewGuid().ToString();
                    transectiontype = "Receipt_Payment";
                    drledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                    crledgerid = mCustomerLadgerID;
                    string receiptNo = GetReceiptNoUsingTransectionId();
                    string status = "Due";
                    string mode = "'" + cmbPaymentMethod.Text + "'";
                    string bankname = cmbPaymentMethod.Text == "Cheque" ? "'" + cmbBank.Text.GetDBFormatString() + "'" : "NULL";
                    string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
                    string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
                    string dueAmountstr = lblDueAmount.Text;
                    dueAmountstr = dueAmountstr.Replace(",", "");
                    if (double.Parse(dueAmountstr) <= 0)
                    {
                        status = "Paid";
                    }
                    string narration = "'" + invoiceNo + "'";
                    InsertOrUpdateTransection(transectionid, invoiceDate, receiptNo, txtPaidAmount.Text.Replace(",",""), drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate, narration);

                    #region CurrentBalanceUpdate

                    mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totalInvoiceAmount.toString().Replace(",",""), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(mquery);
                    mquery = "update ReceiptPaymentStatus set Amount=" + totalInvoiceAmount + " where VoucherType='InVoice' and  VoucharNo='" + lblInvoiceNo.Text + "'";
                    mlistQuery.Add(mquery);
                    #endregion

                }
            }
            #endregion update
            SaveInvoiceDetails(invoiceNo);
            if (!mInvoiceNoForEdit.ISNullOrWhiteSpace())
            {
                foreach (DataRow item in mdtFroRestore.Rows)
                {
                    string itemid = item["ItemId"].ToString();
                    string stockSummaryId = item["StockSummaryId"].ToString();
                    string quantity = item["Qty"].ToString().Replace(",","");
                    string unit = item["Unit"].ToString();
                    mlistQuery.Add(UpdateStockSummaryForRestoring(itemid, stockSummaryId, quantity, unit));
                }
            }
            if (!morderslno.ISNullOrWhiteSpace())
            {
                mquery = "update SalesOrder set Status='Close' where slno='" + morderslno + "'";
                mlistQuery.Add(mquery);
            }

            if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
            {
                INVOICE_TOOLS._ISInvoiceCreate = true;
                if (MessageBox.Show("Invoice saved.\nDo you want to print now ?", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InvoiceReportViewer frmInvoiceReportViewer = new InvoiceReportViewer(InvoiceReportViewer._InvoiceCopy.Original, lblInvoiceNo.Text);
                    frmInvoiceReportViewer.OnClose += FrmInvoiceReportViewer_OnClose;
                    frmInvoiceReportViewer.ShowDialog();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
        private bool IsExsistAdvanceReceipt()
        {
            string query = "select * from AdvanceReceiptVoucher  where dueamount>0 and LedgerId='" + mCustomerLadgerID + "' ";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return true;
            }
            return false;
        }
        private bool IsExsistCreditNote()
        {
            string query = "Select * from CDRNote where dueamount>0 and DocumentType='C' and LedgerId='" + mCustomerLadgerID + "'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return true;
            }
            return false;
        }
        private void Adcrlist_OnClose(List<string> list, string dueamount)
        {
            mlistQuery.Clear();
            dueamount = dueamount.Replace(",", "");
            double.TryParse(dueamount, out mDueAmount);
            foreach (var item in list)
            {
                mlistQuery.Add(item);
            }
            mStatus = mDueAmount > 0 ? "Due" : "Paid";
            if (InvoiceAlerts())
            {
                SaveInvoice();
            }
        }
        private void UpdateInvoiceLastTransectionId(string invoiceNo, string transectionid, string status, string dueamount)
        {
            mquery = "Update Invoice set LastTransecetionID='" + transectionid + "',status='" +
                    status + "',dueamount='" + dueamount + "' where invoiceno='" + invoiceNo + "' ";
            mlistQuery.Add(mquery);
        }
        private string GetReceiptNo()
        {
            long receptno = 1;
            string query = "select MAX(no) from (select convert(bigint,No) as no from Transection where TransectionType='Receipt_Payment') as Transection";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            try
            {
                receptno = (long.Parse(obj.ToString())) + 1;
            }
            catch (Exception) { }

            return receptno.ToString(); ;
        }
        private string GetReceiptNoUsingTransectionId()
        {
            string query = "Select No as no from transection where TransectionType='Receipt_Payment' and Transectionid='" + mTransectionIDForEdit + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }

        private void InsertOrUpdateTransection(string tranid, string invoiceDate, string invoiceNo, string totalinvoicevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate, string narration)
        {
            string transectionid = tranid;
            if (mInvoiceNoForEdit.ISNullOrWhiteSpace())
            {
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                       "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                       invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + drledgerid + "','" +
                       crledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                       + "," + ChequeNo + "," + ChequeDate + "," + narration + ")";
                mlistQuery.Add(mquery);
                transectionid = Guid.NewGuid().ToString();
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                    "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                    invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + crledgerid + "','" +
                    drledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                    + "," + ChequeNo + "," + ChequeDate + "," + narration + ")";
                mlistQuery.Add(mquery);
            }
            else
            {
                TransectionTools.GetTransectionId(invoiceNo, transectiontype);
                mquery = "Update Transection Set Date='" + invoiceDate + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mlistQuery.Add(mquery);

                mquery = "Update Transection Set Date='" + invoiceDate + "',LedgerIdFrom='" + crledgerid + "', " +
                       "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mlistQuery.Add(mquery);

            }
        }
        private void FrmInvoiceReportViewer_OnClose()
        {
            this.Close();
        }
        private void FrmReceiptVoucher_OnClose()
        {
            this.Close();
        }
        /// <summary>
        /// Data save ,
        /// Stock Update
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="transectionId"></param>
        private void SaveInvoiceDetails(string invoiceNo)
        {
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemId = row.Cells["ItemId"].Value.ToString();
                object hsncodeobj = row.Cells["ParticularsHsnCode"].Value;
                string hsnCode = !hsncodeobj.ISValidObject() ? "NULL" : "'" + hsncodeobj.ToString() + "'";
                string itemName = row.Cells["ItemName"].Value.ToString();
                string quantity = row.Cells["QTY"].Value.ToString().Replace(",", ""); ;

                string rateStr = row.Cells["RATE"].Value.ToString().Replace(",","");
                double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);

                string amountStr = row.Cells["TOTALAMOUNT"].Value.ToString().Replace(",", "");
                double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);

                string unit = row.Cells["UNIT"].Value.ToString();

                object disRateStr = row.Cells["DISCOUNTRATE"].Value;
                string disRate = !disRateStr.ISValidObject() ? "NULL" : disRateStr.ToString().Replace(",", "");
                object disAmountStr = row.Cells["DISCOUNTAMOUNT"].Value;
                string disAmount = !disAmountStr.ISValidObject() ? "NULL" : disAmountStr.ToString().Replace(",", "");

                string taxAmountStr = row.Cells["TAXABLEVALUE"].Value.ToString().Replace(",", "");
                double taxAmount = taxAmountStr.ISNullOrWhiteSpace() ? 0f : double.Parse(taxAmountStr);

                object cgstRateStr = row.Cells["CGSTRATE"].Value;
                string cgstRate = !cgstRateStr.ISValidObject() ? "NULL" : cgstRateStr.ToString().Replace(",", "");
                object cgstAmountStr = row.Cells["CGSTAMOUNT"].Value.ToString().Replace(",", "");
                string cgstAmount = !cgstAmountStr.ISValidObject() ? "NULL" : cgstAmountStr.ToString().Replace(",", "");

                object sgstRateStr = row.Cells["SGSTRATE"].Value;
                string sgstRate = !sgstRateStr.ISValidObject() ? "NULL" : sgstRateStr.ToString().Replace(",", "");
                object sgstAmountStr = row.Cells["SGSTAMOUNT"].Value;
                string sgstAmount = !sgstAmountStr.ISValidObject() ? "NULL" : sgstAmountStr.ToString().Replace(",", "");

                object igstRateStr = row.Cells["IGSTRATE"].Value;
                string igstRate = !igstRateStr.ISValidObject() ? "NULL" : igstRateStr.ToString().Replace(",", "");
                object igstAmountStr = row.Cells["IGSTAMOUNT"].Value;
                string igstAmount = !igstAmountStr.ISValidObject() ? "NULL" : igstAmountStr.ToString().Replace(",", "");

                object cessRateStr = row.Cells["CESSRATE"].Value;
                string cessRate = !cessRateStr.ISValidObject() ? "NULL" : cessRateStr.ToString().Replace(",", "");
                object cessAmountStr = row.Cells["CESSAMOUNT"].Value;
                string cessAmount = !cessAmountStr.ISValidObject() ? "NULL" : cessAmountStr.ToString().Replace(",", "");

                object netAmountStr = row.Cells["TotalWithTax"].Value;
                string netAmount = !netAmountStr.ISValidObject() ? "NULL" : netAmountStr.ToString().Replace(",", "");

                object stocksummaryidstr = row.Cells["SotckSummaryId"].Value;
                string stocksummaryid = !stocksummaryidstr.ISValidObject() ? "NULL" : stocksummaryidstr.ToString();
                double qty;
                quantity = quantity.Replace(",", "");
                double.TryParse(quantity, out qty);

                string query = "Insert into InvoiceDetails(InvoiceNo, ItemID, HSNCode, ItemName, Quantity,Unit, Rate, Amount, " +
                                "DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, IGSTRate, " +
                                "IGSTAmount, CessRate, CeassAmount,Total,StockSummaryId) values('" + invoiceNo + "'," + itemId + "," + hsnCode + ",'" +
                                itemName + "'," + quantity + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount
                                + "," + taxAmount + "," + cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," +
                                igstRate + "," + igstAmount + "," + cessRate + "," + cessAmount + "," + netAmount + "," + stocksummaryid + ")";
                mlistQuery.Add(query);

                StockMentance(stocksummaryid, quantity, unit, itemId);

            }
        }
        private void StockMentance(string stocksummaryid, string quantity, string unit, string itemid)
        {
            if (IsPreviousItem(stocksummaryid))
            {
                //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

                int i = 0;
                foreach (DataRow item in mdtFroRestore.Rows)
                {
                    string summaryid = item["StockSummaryId"].ToString();
                    if (summaryid == stocksummaryid)
                    {
                        double hqty = 0d, mqty = 0d, lqty = 0d, finalqty = 0, currentqty = 0;
                        string hunit = item["hUnit"].ToString();
                        string hqtystr = item["hQty"].ToString();
                        string munit = item["mUnit"].ToString();
                        string mqtystr = item["mQty"].ToString();
                        string lunit = item["lUnit"].ToString();
                        string lqtystr = item["lQty"].ToString();

                        hqtystr = hqtystr.Replace(",", "");
                        mqtystr = mqtystr.Replace(",", "");
                        lqtystr = lqtystr.Replace(",", "");
                        quantity = quantity.Replace(",", "");

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

                    }
                    i++;
                }

            }

            else
            {
                mlistQuery.Add(UpdateStockSummary(itemid, stocksummaryid, quantity, unit));
            }
        }
        private bool IsPreviousItem(string stockSummaryId)
        {
            //StockSummaryId,ItemId,Qty,Unit,hUnit,hQty,mUnit,mQty,lUnit,lQty,IsRightProduct

            foreach (DataRow item in mdtFroRestore.Rows)
            {
                string summaryid = item["StockSummaryId"].ToString();
                //bool isgoodproduct = bool.Parse(item["IsRightProduct"].ToString());
                if (summaryid == stockSummaryId)
                {
                    return true;
                }
            }
            return false;
        }
        private string UpdateStockSummary(string itemID, string stockSummaryid, string currentqtystr, string currentunit)
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

                string highestStockQty = dt.Rows[0]["HighestStockQty"].ToString();
                string middleStockQty = dt.Rows[0]["MiddleStockQty"].ToString();
                string lowestStockQty = dt.Rows[0]["LowestStockQty"].ToString();
                string highestMeasureQty = dt.Rows[0]["HighestMeasureQty"].ToString();
                string lowestMeasureQty = dt.Rows[0]["LowestMeasureQty"].ToString();

                highestStockQty = highestStockQty.Replace(",", "");
                middleStockQty = middleStockQty.Replace(",", "");
                lowestStockQty = lowestStockQty.Replace(",", "");
                highestMeasureQty = highestMeasureQty.Replace(",", "");
                lowestMeasureQty = lowestMeasureQty.Replace(",", "");
                currentqtystr = currentqtystr.Replace(",", "");

                double.TryParse(highestStockQty, out higestpreviosqty);
                double.TryParse(middleStockQty, out midpreviousqty);
                double.TryParse(lowestStockQty, out lowestpreviousqty);
                double.TryParse(highestMeasureQty, out higestmesure);
                double.TryParse(lowestMeasureQty, out lowestmesure);

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
        private string UpdateStockSummaryForRestoring(string itemID, string stockSummaryid, string currentqtystr, string currentunit)
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

                string highestStockQty = dt.Rows[0]["HighestStockQty"].ToString();
                string middleStockQty = dt.Rows[0]["MiddleStockQty"].ToString();
                string lowestStockQty = dt.Rows[0]["LowestStockQty"].ToString();
                string highestMeasureQty = dt.Rows[0]["HighestMeasureQty"].ToString();
                string lowestMeasureQty = dt.Rows[0]["LowestMeasureQty"].ToString();

                highestStockQty = highestStockQty.Replace(",", "");
                middleStockQty = middleStockQty.Replace(",", "");
                lowestStockQty = lowestStockQty.Replace(",", "");
                highestMeasureQty = highestMeasureQty.Replace(",", "");
                lowestMeasureQty = lowestMeasureQty.Replace(",", "");
                currentqtystr = currentqtystr.Replace(",", "");

                double.TryParse(highestStockQty, out higestpreviosqty);
                double.TryParse(middleStockQty, out midpreviousqty);
                double.TryParse(lowestStockQty, out lowestpreviousqty);
                double.TryParse(highestMeasureQty, out higestmesure);
                double.TryParse(lowestMeasureQty, out lowestmesure);

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
        private double GetTotalQtyOfMinUnit(string stockSummeryID, string unit, double qty)
        {
            string query = "Select * from ( " +
                        "select StockMoreUnit.* from StockMoreUnit " +
                        "inner join StockSummary on StockSummary.UnitMoreID = StockMoreUnit.UnitMoreID " +
                        "where StockSummary.ID = " + stockSummeryID + ") as stockAllUnit " +
                        "where ID<= (select StockMoreUnit.ID from StockMoreUnit " +
                                "inner join StockSummary on StockSummary.UnitMoreID = StockMoreUnit.UnitMoreID " +
                                "where StockSummary.ID = " + stockSummeryID + " and StockMoreUnit.Unit = '" +
                                unit + "' ) order by id";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            double prevUnitQty = 0d, totQty = 0d;
            if (dt.IsValidDataTable())
            {
                if (dt.Rows.Count == 1)
                {
                    return qty;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    else if (i == 1)
                    {
                        string qtystr = dt.Rows[i]["Qty"].ToString().Replace(",", "");
                        qtystr = qtystr.Replace(",", "");
                        double.TryParse(qtystr, out prevUnitQty);
                        totQty = qty * prevUnitQty;
                    }
                    else
                    {
                        double currentQty = 0d;
                        string qtystr = dt.Rows[i]["Qty"].ToString().Replace(",", "") ;
                        qtystr = qtystr.Replace(",", "");
                        double.TryParse(qtystr, out currentQty);
                        totQty = qty * prevUnitQty * currentQty;
                        return totQty;
                    }
                }
            }
            return totQty;
        }
        private double GetCurrentQty(string stockSummeryID)
        {
            string query = "Select SalesQty from StockSummary where ID='" + stockSummeryID + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                try
                {
                    return double.Parse(o.ToString().Replace(",", ""));
                }
                catch (Exception) { }
            }
            return 0d;
        }

        /// <summary>
        /// END
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtDiscountRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }
                else
                {
                    double discountPer = 0d;
                    string discountRatestr = txtDiscountRate.Text;
                    discountRatestr = discountRatestr.Replace(",", "");
                    double.TryParse(discountRatestr + e.KeyChar, out discountPer);
                    if (discountPer > 100)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtDiscountRate_Leave(object sender, EventArgs e)
        {
            double rate = 0d, disRate = 0d, disAmount = 0d;
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text.Replace(",", "")) > 0)
            {
                if (!txtDiscountRate.Text.ISNullOrWhiteSpace())
                {
                    string ratestr = txtRate.Text;
                    string discountRatestr = txtDiscountRate.Text;
                    ratestr = ratestr.Replace(",", "");
                    discountRatestr = discountRatestr.Replace(",", "");

                    double.TryParse(ratestr, out rate);
                    double.TryParse(discountRatestr, out disRate);

                    disAmount = rate * (disRate / 100);
                    txtDiscountAmount.Text = disAmount.toString();
                    if (txtDiscountRate.Text.Contains('.'))
                    {
                        txtDiscountRate.Text = disRate.toString();
                    }
                    CalculateAmount();
                }
                else
                {
                    txtDiscountAmount.Text = "";
                }
            }
            else
            {
                txtDiscountRate.Text = "";
                txtDiscountAmount.Text = "";
            }
        }
        private void txtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtDiscountAmount.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            double rate = 0d, disRate = 0d, disAmount = 0d;
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text.Replace(",", "")) > 0)
            {
                if (!txtDiscountAmount.Text.ISNullOrWhiteSpace())
                {
                    string ratestr = txtRate.Text;
                    string discountAmountstr = txtDiscountAmount.Text;
                    ratestr = ratestr.Replace(",", "");
                    discountAmountstr = discountAmountstr.Replace(",", "");

                    double.TryParse(ratestr, out rate);
                    double.TryParse(discountAmountstr, out disAmount);
                    disRate = (disAmount / rate) * 100;
                    txtDiscountRate.Text = disRate.toString();
                    txtDiscountAmount.Text = disAmount.toString();
                    CalculateAmount();
                }

                else
                {
                    txtDiscountRate.Text = "";
                }
            }
            else
            {
                txtDiscountRate.Text = "";
                txtDiscountAmount.Text = "";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidItemData())
            {
                DescriptionAdd();
                GenerateTotal();
                ItemDataClear();
            }
        }
        private void DescriptionAdd()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string narration = txtNarration.Text.GetDBFormatString();
            itemName = narration.ISNullOrWhiteSpace() ? itemName : itemName + "\n  " + narration;
            string hsnCode = ItemTools.GetItemHSNCode(itemID);
            string unit = cmbUnit.Text;
            double qty = 0d;
            string qtystr = txtQuantity.Text.Replace(",","");
            qtystr = qtystr.Replace(",","");
            double.TryParse(qtystr, out qty);
            string rateStr = txtRate.Text.Replace(",","");;
            double rate = double.Parse(rateStr.Replace(",", ""));
            double discountRate = txtDiscountRate.Text.ISNullOrWhiteSpace() ? 0d : double.Parse(txtDiscountRate.Text.Replace(",", ""));
            double discountAmount = 0d;
            double amount = rate * qty;
            string disAmountSrt = txtDiscountAmount.Text;
            discountAmount = disAmountSrt.ISNullOrWhiteSpace() ? 0d : double.Parse(disAmountSrt.Replace(",", ""));
            string amountWithDiscount = txtAmount.Text;
            double taxAmount = amountWithDiscount.ISNullOrWhiteSpace() ? 0d : double.Parse(amountWithDiscount.Replace(",", ""));
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
            string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
            string totalWithTax = "";
            string StockId = mStockSummaryID;
            string Summaryanditemid = StockId + itemID;
            if (!IsDublicateBatch(Summaryanditemid))
            {
                ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGST, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                                    out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);
                if (ORG_Tools._IsRegularGST)
                {
                    string currentItemGstType = "";
                    string currentInvoiceType = ItemTools.IsTaxBillByItem(itemID, out currentItemGstType);
                    if (mDescriptionSlno == 1)
                    {
                        #region MyRegion
                        mInvoiceType = currentInvoiceType;
                        mPreviousItemGSTType = currentItemGstType;
                        ///Design Grid view
                        GenerateGridForNonGSTType();

                        dgvItemList.Rows.Add(mDescriptionSlno, itemID, StockId, itemName, hsnCode,
                                        qty, unit, rate.toString(), amount.toString(), discountRate,
                                        discountAmount.toString(), taxAmount.toString(), cgstRate,
                                        cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);
                        batchList.Add(Summaryanditemid);
                        DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                        btnCelCol.ToolTipText = "Delete";
                        btnCelCol.Value = "Delete";
                        btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                        dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                        mDescriptionSlno++;
                        #endregion
                    }
                    else if (currentInvoiceType != mInvoiceType)
                    {
                        MessageBox.Show("You can't sale both " + mPreviousItemGSTType + " and " + currentItemGstType
                                        + " rate in a single invoice.\nYou can sale those itmes multiple invoice.", "Access Denied", MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                    }
                    else
                    {
                        #region MyRegion
                        dgvItemList.Rows.Add(mDescriptionSlno, itemID, StockId, itemName, hsnCode, qty, unit,
                                             rate.toString(), amount.toString(), discountRate,
                                             discountAmount.toString(), taxAmount.toString(), cgstRate,
                                             cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);
                        batchList.Add(Summaryanditemid);
                        DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                        btnCelCol.ToolTipText = "Delete";
                        btnCelCol.Value = "Delete";
                        btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                        dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                        mDescriptionSlno++;
                        #endregion
                    }
                }
                else
                {
                    mInvoiceType = "Bill of Supply";
                    GenerateGridForNonGSTType();

                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, StockId, itemName, hsnCode, qty,
                                        unit, rate.toString(), amount.toString(), discountRate,
                                        discountAmount.toString(), taxAmount.toString(), "",
                                        "", "", "", "", "", "", "", taxAmount.toString());
                    batchList.Add(Summaryanditemid);
                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
                }
            }
            else
            {
                MessageBox.Show("You alredy added same batch with same item.", "item", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cmbBatchNo.Select();
            }

        }
        private bool IsDublicateBatch(string summaryanditemid)
        {
            foreach (var item in batchList)
            {
                if (item.ToString() == summaryanditemid)
                {
                    return true;
                }
            }
            return false;
        }
        private void ItemDataClear()
        {
            cmbBatchNo.Items.Clear();
            txtQuantity.Clear();
            cmbUnit.Items.Clear();
            txtDiscount.Clear();
            txtDiscountRate.Clear();
            txtAmount.Clear();
            txtNarration.Clear();
            lblAvlQty.Text = "";
            txtNarration.Clear();
            cmbItemName.Text = "";
            cmbItemName.Select();
        }
        private void CalculateAmount()
        {
            double rate = 0d;
            double qty = 0d;
            double disAmount = 0d;
            double amount = 0d;
            double totalDiscount = 0d;

            string ratestr = txtRate.Text;
            string quantitystr = txtQuantity.Text.Replace(",","");
            string discountAmountstr = txtDiscountAmount.Text;

            ratestr = ratestr.Replace(",", "");
            quantitystr = quantitystr.Replace(",", "");
            discountAmountstr = discountAmountstr.Replace(",", "");

            double.TryParse(ratestr, out rate);
            double.TryParse(quantitystr, out qty);
            double.TryParse(discountAmountstr, out disAmount);

            totalDiscount = qty * disAmount;
            amount = (qty * rate) - totalDiscount;

            txtAmount.Text = amount.toString();
        }
        private bool IsValidItemData()
        {
            if (cmbCustomerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give customer name.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Select();
                return false;
            }
            if (cmbItemName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select any item.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
                return false;
            }
            if (txtQuantity.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter quantity", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtQuantity.Select();
                return false;
            }
            if (cmbUnit.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select unit", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbUnit.Focus();
                return false;
            }
            if (txtRate.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter rate.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRate.Focus();
                return false;
            }
            return true;
        }
        private bool IsDuplicateItemSelect()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemId = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemExist = row.Cells["ItemName"].Value.ToString();
                if (itemName == itemExist)
                {
                    MessageBox.Show("Found duplicate item in below list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbItemName.Select();
                    return true;
                }
            }
            return false;
        }
        private void GenerateTotal()
        {
            mDescriptionSlno = 1;
            mTotalAmount = 0d; mTotalDiscount = 0d;
            mTotalCGST = 0d; mTotalSGST = 0d; mTotalIGST = 0d; mTotalCESS = 0d;
            mTaxableAmount = 0d; mTotalWithTax = 0d;
            mTotalQuantity = 0;
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                row.Cells["SlNo"].Value = mDescriptionSlno++;
                object qtyObj = row.Cells["QTY"].Value;
                object discountObj = row.Cells["DiscountAmount"].Value;
                object totalObj = row.Cells["TotalAmount"].Value;
                object totalTaxableobj = row.Cells["TaxableValue"].Value;
                object cgstObj = row.Cells["CGSTAmount"].Value;
                object sGstObj = row.Cells["SGSTAmount"].Value;
                object iGstObj = row.Cells["IGSTAmount"].Value;
                object cessObj = row.Cells["CessAmount"].Value;
                object totalWithTaxObj = row.Cells["TotalWithTax"].Value;

                mTotalQuantity += (qtyObj.ISValidObject()) ? double.Parse(qtyObj.ToString().Replace(",", "")) : 0;
                mTotalAmount += (totalObj.ISValidObject()) ? double.Parse(totalObj.ToString().Replace(",", "")) : 0;
                mTaxableAmount += (totalTaxableobj.ISValidObject()) ? double.Parse(totalTaxableobj.ToString().Replace(",", "")) : 0;
                mTotalDiscount += (discountObj.ISValidObject()) ? double.Parse(discountObj.ToString().Replace(",", "")) : 0;
                mTotalCGST += (cgstObj.ISValidObject()) ? double.Parse(cgstObj.ToString().Replace(",", "")) : 0;
                mTotalSGST += (sGstObj.ISValidObject()) ? double.Parse(sGstObj.ToString().Replace(",", "")) : 0;
                mTotalIGST += (iGstObj.ISValidObject()) ? double.Parse(iGstObj.ToString().Replace(",", "")) : 0;
                mTotalCESS += (cessObj.ISValidObject()) ? double.Parse(cessObj.ToString().Replace(",", "")) : 0;
                mTotalWithTax += (totalWithTaxObj.ISValidObject()) ? double.Parse(totalWithTaxObj.ToString().Replace(",", "")) : 0;
            }
            lblTotQuantity.Text = mTotalQuantity.ToString();
            lblTotAmount.Text = mTotalAmount.toString();
            lblTaxableAmountTotal.Text = mTaxableAmount.toString();

            lblTotalCGST.Text = mTotalCGST.toString();
            lblTotalIGST.Text = mTotalIGST.toString();
            lblTotalSGST.Text = mTotalSGST.toString();
            lblTotalCESS.Text = mTotalCESS.toString();
            lblTotalDiscount.Text = mTotalDiscount.toString();
            lblTotalWithTax.Text = mTotalWithTax.toString();
            lblTotalInvoiceAmount.Text = mtotalInvoiceAmount.toString();
        }

        /// <summary>
        /// grid design and Enabled controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
