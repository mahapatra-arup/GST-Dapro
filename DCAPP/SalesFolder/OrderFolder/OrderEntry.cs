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
    public partial class OrderEntry : Form
    {
        private string msg = "";
        string mEstimateid = "";
        public event Action OnClose;
        private string mOrderIDForEdit = "", mUnitMoreID = "", mUnit1, mStockSummaryID, munit2, munit3, mstockunit;
        private List<string> mLstQuery = new List<string>();

        private double mTotalOrderAmount, mTotalAmount, mTotalDiscount, mTotalCGST,
                       mTotalSGST, mTotalIGST, mTotalCESS, mTaxableAmount, mTotalWithTax,
            mCurrentQty = 0d, mAvailabelQty = 0d, mmaxqty1 = 0d, mTotalQuantity = 0, finalqty = 0,
            mmaxqty = 0d, mqty1, mrate1, mqty2, mrate2, mqty3, mrate3, mtempcurrentqty;
        //private long mTotalQuantity;
        List<string> batchList = new List<string>();
        private string mHighestUnit = "", mMiddleUnit = "", mLowestUnit = "",
           mHighestStockQty = "", mMiddleStockQty = "", mLowestQty = "", mHighestSalesRate = "",
           mMiddlesalesRate = "", mLowestSalesRate = "", mHighestMesureUnit = "", mLowestMesureUnit = "";

        private bool mIsIGST = false;
        public OrderEntry()
        {
            InitializeComponent();
            this.FitToVertical();
            GenerateGridForNonGSTType();
            cmbCustomerName.AddCustomers();
            cmbItemName.Leave -= cmbItemName_Leave;
            cmbItemName.AddItem();
            cmbItemName.Leave += cmbItemName_Leave;
            GenerateSlNo();
            pnlshippinggst.Hide();
            pnlbillinggst.Hide();
            grbEstimateNo.Hide();
            GridDesign();
        }
        public OrderEntry(string estimateid, string partyiname, string estimateno)//from estimate
        {
            InitializeComponent();
            this.FitToVertical();
            grbEstimateNo.Show();
            GenerateGridForNonGSTType();
            cmbCustomerName.AddCustomers();
            cmbCustomerName.Text = partyiname;
            lblEstimateNo.Text = estimateno;
            string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
            GetCustomerAddressDetails(customerID);
            GetCustomerShippedDetails(customerID);
            cmbCustomerName.Enabled = false;
            btnCustomer.Enabled = false;
            mEstimateid = estimateid;
            RetriveEstimateData(mEstimateid);
            cmbItemName.AddItem();
            GenerateSlNo();


            GridDesign();
        }
        public OrderEntry(string orderID, string status)
        {
            InitializeComponent();
            mOrderIDForEdit = orderID;
            this.FitToVertical();
            grbEstimateNo.Hide();
            GenerateGridForNonGSTType();
            cmbCustomerName.AddCustomers();
            cmbItemName.AddItem();
            //cmbUnit.AddUnit();
            ViewExistingDataMain();
            ReadOnlyAllControl(status);
            cmbCustomerName.Enabled = false;
            btnCustomer.Enabled = false;
            GridDesign();
        }
        private void RetriveEstimateData(string estimateid)
        {
            string query = "Select * from EstimateDetails where EstimateId='" + estimateid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            foreach (DataRow item in dt.Rows)
            {
                string itemName = item["ItemName"].ToString();
                string itemID = item["ItemID"].ToString();
                string hsnCode = ItemTools.GetItemHSNCode(itemID);
                string unit = item["Unit"].ToString();
                string qtyStr = item["Qty"].ToString();
                int qty = (qtyStr.ISNullOrWhiteSpace() ? 0 : int.Parse(qtyStr));
                string rateStr = item["Rate"].ToString();
                double rate = double.Parse(rateStr);
                double discountRate = item["DiscountRate"].ToString().ISNullOrWhiteSpace() ? 0d : double.Parse(item["DiscountRate"].ToString());
                double discountAmount = 0d;
                double amount = rate * qty;
                string disAmountSrt = item["DiscountAmount"].ToString();
                discountAmount = disAmountSrt.ISNullOrWhiteSpace() ? 0d : double.Parse(disAmountSrt);
                string amountWithDiscount = item["TaxAmount"].ToString();
                double taxAmount = amountWithDiscount.ISNullOrWhiteSpace() ? 0d : double.Parse(amountWithDiscount);
                string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
                string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
                string totalWithTax = "";
                string stocksummaryid = item["StockSummaryId"].ToString();
                ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGST, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                                  out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

                dgvItemList.Rows.Add(mDescriptionSlno, itemID, stocksummaryid, itemName, hsnCode, qty, unit, rate, amount.ToString("0.00"), discountRate,
                                      discountAmount.ToString("0.00"), taxAmount.ToString("0.00"), cgstRate,
                                      cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);

                DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                btnCelCol.ToolTipText = "Delete";
                btnCelCol.Value = "Delete";
                btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                mDescriptionSlno++;

            }
            GenerateTotal();

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
        private void GenerateGridForNonGSTType()
        {
            if (ORG_Tools._IsRegularGST)
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
                    lblTotalCGST.Text = "---";
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

                lblTotalCGST.Text = "";
                lblTotalSGST.Text = "";
                lblTotalIGST.Text = "";
                lblTotalCESS.Text = "";
            }
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
        private void dgvitemList_Paint(object sender, PaintEventArgs e)
        {
            #region Assign Array
            string[] array = new string[4];
            int[] ary = new int[4];
            int length = 0;
            if (ORG_Tools._IsRegularGST)
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
            #endregion
            //string[] array = { "DISCOUNT", "CGST", "SGST", "IGST", "CESS" };//
            //int[] ary = { 8, 11, 13, 15, 17 };//
            for (int i = 0; i < length; i++)
            {
                //Column Location And Hight ANd Width;
                DataGridViewCell hc = dgvItemList.Columns[ary[i]].HeaderCell;
                Rectangle hcRct = dgvItemList.GetCellDisplayRectangle(hc.ColumnIndex, -1, true);

                //For column wise create Width means how many column are span;
                int multiHeaderWidth = dgvItemList.Columns[hc.ColumnIndex].Width + dgvItemList.Columns[hc.ColumnIndex + 1].Width;
                //Rectengle x,y location and Hight and Width Set;
                Rectangle headRct = new Rectangle(hcRct.Left, 2, multiHeaderWidth, dgvItemList.ColumnHeadersHeight / 3 + 5);

                SizeF sz = e.Graphics.MeasureString(array[i], dgvItemList.Font);
                int headerTop = Convert.ToInt32((headRct.Height / 2) - (sz.Height / 2)) + 3;
                //Rectengle clor;
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), headRct);
                ////border draw
                e.Graphics.DrawRectangle(Pens.LightGray, headRct);
                //For Text Design and location;
                e.Graphics.DrawString(array[i], new Font("Microsoft Sans Serif", 7f), Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
                dgvItemList.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7f);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.A))
            {
                btnNewSupplier_Click(btnCustomer, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.A))
            {
                if (!cmbCustomerName.Text.ISNullOrWhiteSpace())
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
                    MessageBox.Show("Select customer name.", "Customer Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Private Methods
        /// </summary>
        private void GenerateSlNo()
        {
            int slno = 1;
            string query = "Select max(SlNo) as slno from SalesOrder ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                try
                {
                    slno = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblSlNo.Text = slno.ToString();
        }
        private void ViewExistingDataMain()
        {
            string query = "Select SalesOrder.*,LadgerMain.TemplateName from SalesOrder inner join LadgerMain on SalesOrder.LedgerID=LadgerMain.LadgerID " +
                           "where OrderId='" + mOrderIDForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                cmbCustomerName.Text = dt.Rows[0]["TemplateName"].ToString();
                string ledgerID = dt.Rows[0]["LedgerID"].ToString();
                GetCustomerAddressDetails(ledgerID);
                txtOrderNo.Text = dt.Rows[0]["CustomerOrderNo"].ToString();
                dtpDate.Text = dt.Rows[0]["OrderDate"].ToString();
                string amountStr = dt.Rows[0]["TotalAmount"].ToString();
                mTotalOrderAmount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);
                lblTotalOrderAmount.Text = mTotalOrderAmount.ToString("0.00");
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                lblShippedTo.Text = dt.Rows[0]["ShippingTo"].ToString();
                lblShippedAddress.Text = dt.Rows[0]["ShippingAddress"].ToString();
                lblStateNameShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                lblStateCodeShipping.Text = dt.Rows[0]["ShippingStateCode"].ToString();
                txtTermsOfDelevery.Text = dt.Rows[0]["TermsofDelivery"].ToString();
                dtpdeliveryDate.Text = dt.Rows[0]["DeliveryDate"].ToString();
                ViewExistingDataDetails();
                GenerateTotal();
            }
        }
        private void ViewExistingDataDetails()
        {
            batchList.Clear();
            dgvItemList.Rows.Clear();
            string query = "Select SalesOrderDetails.* from SalesOrderDetails " +
                           "inner join SalesOrder on SalesOrderDetails.OrderId=SalesOrder.OrderId " +
                           "where SalesOrder.OrderId='" + mOrderIDForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string comodityCode = item["ComodityCode"].ToString();
                    string itemID = item["ItemId"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string qtyStr = item["Qty"].ToString();
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
                    string summaryid = item["StockSummaryId"].ToString();

                    string idandbatch = summaryid + itemID;
                    batchList.Add(idandbatch);
                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, summaryid, itemName, comodityCode, qtyStr, unit,
                                         rate.ToString("0.00"), amount.ToString("0.00"), disRate, disAmount, taxAmount
                                         , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                                         , cessrate, cessAmount, totalAmount);

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
        private bool IsValidSelection()
        {
            if (!dtpDate.Value.IsValidDate())
            {
                dtpDate.Focus();
                return false;
            }
            if (cmbCustomerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a customer.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Select();
                return false;
            }
            if (lblShippedTo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping name not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (lblShippedAddress.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping address not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (lblStateNameShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping state not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Please add at least one item.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
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
        private void ResetData()
        {
            mDescriptionSlno = 1;
            mTotalOrderAmount = 0F;
            GenerateSlNo();
            mOrderIDForEdit = "";
            dgvItemList.Rows.Clear();
            lblTotalOrderAmount.Text = "0.00";
            cmbItemName.SelectedIndex = -1;
            txtRate.Clear();
            txtAmount.Clear();
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            txtOrderNo.Clear();
            batchList.Clear();
            GenerateTotal();
        }
        private void ResetAddressData()
        {
            lblNameBilling.Text = "";
            lblBillingAddress.Text = "";
            lblStateNameBilling.Text = "";
            lblGSTIn.Text = "";
            lblStateNameBilling.Text = "";
            lblStateCodeBilling.Text = "";
            lblShippedTo.Text = "";
            lblShippedAddress.Text = "";
            lblShippedToGstin.Text = "";
            lblStateCodeShipping.Text = "";
            lblStateNameShipping.Text = "";
        }
        private int mDescriptionSlno = 1;
        private void DescriptionAdd()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string hsnCode = ItemTools.GetItemHSNCode(itemID);
            string narration = txtNarration.Text.GetDBFormatString();
            itemName = narration.ISNullOrWhiteSpace() ? itemName : itemName + "\n  " + narration;
            string unit = cmbUnit.Text;
            double qty = 0d;
            double.TryParse(txtQuantity.Text, out qty);
            string rateStr = txtRate.Text;
            double rate = double.Parse(rateStr);
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
            string StockId = mStockSummaryID;
            string SummaryIdAndItemid = mStockSummaryID + itemID;
            int temp = 0;
            foreach (var item in batchList)
            {
                if (item == SummaryIdAndItemid)
                {
                    temp = 1;
                }
            }
            if (temp == 0)
            {
                batchList.Add(SummaryIdAndItemid);
                ItemTools.GetItemGSTRateAndAmount(itemID, mIsIGST, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                                  out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

                dgvItemList.Rows.Add(mDescriptionSlno, itemID, mStockSummaryID, itemName, hsnCode, qty, unit,
                                     rate.toString(), amount.toString(), discountRate,
                                     discountAmount.toString(), taxAmount.toString(), cgstRate,
                                     cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);

                DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                btnCelCol.ToolTipText = "Delete";
                btnCelCol.Value = "Delete";
                btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                mDescriptionSlno++;
            }
            else
            {
                MessageBox.Show("item alredy added in the list.", "Item add", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
            }
        }
        private void OrderSave()
        {
            #region Data
            mLstQuery.Clear();
            string orderID = Guid.NewGuid().ToString();
            string ledgerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
            string customerOrderNo = txtOrderNo.Text;
            string orderDate = dtpDate.Text;
            string totOrderAmount = mTotalOrderAmount.ToString();
            string slNo = lblSlNo.Text;
            string description = txtDescription.Text.GetDBFormatString();
            string status = "Open";
            string isPurchaseOrder = "True";
            string termofdelivery = txtTermsOfDelevery.Text.GetDBFormatString();
            string delliverydate = dtpdeliveryDate.Text;
            /// Shipping Details
            string nameShipping = lblShippedTo.Text.GetDBFormatString();
            string addressShipping = lblShippedAddress.Text.GetDBFormatString();
            string shippingState = lblStateNameShipping.Text;
            string shippingstateCode = lblStateCodeShipping.Text;

            string estimateno = lblEstimateNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + lblEstimateNo.Text.GetDBFormatString() + "'";
            string query = "";
            #endregion

            #region Query
            if (mOrderIDForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into SalesOrder(OrderId, SlNo, CustomerOrderNo, LedgerID, OrderDate, TotalAmount, " +
                        "Description,  IsPurchaseOrder, Status, ShippingTo, ShippingAddress, " +
                        "ShippingState, ShippingStateCode,EstimateNo,TermsofDelivery,DeliveryDate)  " +
                        "Values('" + orderID + "'," + slNo + ",'" + customerOrderNo + "','" + ledgerID + "','" + orderDate
                        + "'," + totOrderAmount + ",'" + description + "','" + isPurchaseOrder + "','" + status + "','" +
                        nameShipping + "','" + addressShipping + "','" + shippingState + "','" + shippingstateCode + "'," + estimateno + ",'" + termofdelivery + "','" + delliverydate + "')";
                mLstQuery.Add(query);
            }
            else
            {
                query = "Update SalesOrder set LedgerID='" + ledgerID + "', CustomerOrderNo='" + customerOrderNo + "', OrderDate='" + orderDate + "', TotalAmount=" +
                        totOrderAmount + ",ShippingTo='" + nameShipping + "',ShippingAddress='" + addressShipping +
                        "',ShippingState='" + shippingState + "',TermsofDelivery='" + termofdelivery + "',DeliveryDate='" + delliverydate + "',EstimateNo=" + estimateno + ",ShippingStateCode='" + shippingstateCode +
                        "' where OrderId='" + mOrderIDForEdit + "'";
                mLstQuery.Add(query);
                query = "Delete from SalesOrderDetails where OrderId='" + mOrderIDForEdit + "'";
                mLstQuery.Add(query);
                orderID = mOrderIDForEdit;
            }
            #region SalesOrderDetails
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                #region Data
                string itemId = row.Cells["ItemId"].Value.ToString();
                var hsnCode = row.Cells["ParticularsHsnCode"].Value;
                string itemName = row.Cells["ItemName"].Value.ToString();
                string quantity = row.Cells["QTY"].Value.ToString();

                string rateStr = row.Cells["RATE"].Value.ToString();
                double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);

                string amountStr = row.Cells["TOTALAMOUNT"].Value.ToString();
                double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);

                string unit = row.Cells["UNIT"].Value.ToString();

                object disRateStr = row.Cells["DISCOUNTRATE"].Value;
                float disRate = !disRateStr.ISValidObject() ? 0f : float.Parse(disRateStr.ToString());
                object disAmountStr = row.Cells["DISCOUNTAMOUNT"].Value;
                double disAmount = !disAmountStr.ISValidObject() ? 0f : double.Parse(disAmountStr.ToString());

                object taxAmountStr = row.Cells["TAXABLEVALUE"].Value;
                double taxAmount = !taxAmountStr.ISValidObject() ? 0f : double.Parse(taxAmountStr.ToString());

                object cgstRateStr = row.Cells["CGSTRATE"].Value;
                float cgstRate = !cgstRateStr.ISValidObject() ? 0f : float.Parse(cgstRateStr.ToString());
                object cgstAmountStr = row.Cells["CGSTAMOUNT"].Value;
                double cgstAmount = !cgstAmountStr.ISValidObject() ? 0f : double.Parse(cgstAmountStr.ToString());

                object sgstRateStr = row.Cells["SGSTRATE"].Value;
                float sgstRate = !sgstRateStr.ISValidObject() ? 0f : float.Parse(sgstRateStr.ToString());
                object sgstAmountStr = row.Cells["SGSTAMOUNT"].Value;
                double sgstAmount = !sgstAmountStr.ISValidObject() ? 0f : double.Parse(sgstAmountStr.ToString());

                object igstRateStr = row.Cells["IGSTRATE"].Value;
                float igstRate = !igstRateStr.ISValidObject() ? 0f : float.Parse(igstRateStr.ToString());
                object igstAmountStr = row.Cells["IGSTAMOUNT"].Value;
                double igstAmount = !igstAmountStr.ISValidObject() ? 0f : double.Parse(igstAmountStr.ToString());

                object cessRateStr = row.Cells["CESSRATE"].Value;
                float cessRate = !cessRateStr.ISValidObject() ? 0f : float.Parse(cessRateStr.ToString());
                object cessAmountStr = row.Cells["CESSAMOUNT"].Value;
                double cessAmount = !cessAmountStr.ISValidObject() ? 0f : double.Parse(cessAmountStr.ToString());

                object totalStr = row.Cells["TotalWithTax"].Value;
                double total = !totalStr.ISValidObject() ? 0f : double.Parse(totalStr.ToString());
                object stocksummaryidstr = row.Cells["SotckSummaryId"].Value;
                string stocksummaryid = !stocksummaryidstr.ISValidObject() ? "NULL" : stocksummaryidstr.ToString();

                #endregion
                if (ORG_Tools._IsRegularGST)
                {
                    if (mIsIGST)
                    {
                        query = "Insert into SalesOrderDetails(OrderId, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, " +
                                "IGSTRate, IGSTAmount, CessRate,CeassAmount,Total, DueQty,StockSummaryId)" +
                                "Values('" + orderID + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," + igstRate + "," +
                                igstAmount + "," + cessRate + "," + cessAmount + "," + total + "," + quantity + "," + stocksummaryid + ")";
                    }
                    else
                    {
                        query = "Insert into SalesOrderDetails(OrderId, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, " +
                                "CessRate,CeassAmount, Total,DueQty,StockSummaryId)" +
                                "Values('" + orderID + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," +
                                cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," + cessRate + "," +
                                cessAmount + "," + total + "," + quantity + "," + stocksummaryid + ")";
                    }
                }
                else
                {
                    query = "Insert into SalesOrderDetails(OrderId, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                            "Amount, DiscountRate, DiscountAmount, TaxAmount, Total, DueQty,StockSummaryId)" +
                            "Values('" + orderID + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                            + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," +
                            taxAmount + "," + total + "," + quantity + "," + stocksummaryid + ")";
                }
                mLstQuery.Add(query);
                #endregion
            }
            #endregion

            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
            {

                if (mOrderIDForEdit.ISNullOrWhiteSpace() && mEstimateid.ISNullOrWhiteSpace())
                {
                    ResetData();
                    ResetAddressData();
                    cmbCustomerName.SelectedIndex = -1;
                    cmbCustomerName.Select();
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
            #endregion
        }
        /// <summary>
        /// Customer Details
        /// </summary>
        private void GetCustomerAddressDetails(string customerID)
        {
            string billingName = "", billingAddress = "", billingStateName = "", billingStateCOde = "", gstNo = "";
            LedgerTools.GetLadgerBillingDetails(customerID, out billingName, out billingAddress, out billingStateName, out billingStateCOde, out gstNo);

            lblNameBilling.Text = billingName;
            lblBillingAddress.Text = billingAddress;
            lblGSTIn.Text = gstNo;
            lblStateCodeBilling.Text = billingStateCOde;
            lblStateNameBilling.Text = billingStateName;

            mIsIGST = (ORG_Tools._StateCode == billingStateCOde) ? false : true;
            GenerateGridForNonGSTType();
        }
        private void GetCustomerShippedDetails(string customerID)
        {
            string billingName = "", billingAddress = "", billingStateName = "", billingStateCOde = "", gstNo = "";
            LedgerTools.GetLadgerShippingDetails(customerID, out billingName, out billingAddress, out billingStateName, out billingStateCOde, out gstNo);

            lblShippedTo.Text = billingName;
            lblShippedAddress.Text = billingAddress;
            lblShippedToGstin.Text = gstNo;

            lblStateCodeShipping.Text = billingStateCOde;
            lblStateNameShipping.Text = billingStateName;
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtQuantity.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
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
        private void GenerateTotal()
        {
            mDescriptionSlno = 1;
            mTotalOrderAmount = 0d; mTotalAmount = 0d; mTotalDiscount = 0d;
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

                mTotalQuantity += (qtyObj.ISValidObject()) ? double.Parse(qtyObj.ToString()) : 0;
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
            lblTotalDiscount.Text = mTotalDiscount.toString();
            lblTotalWithTax.Text = mTotalWithTax.toString();
            lblTotalOrderAmount.Text = mTotalOrderAmount.toString();
        }
        private void ItemDataClear()
        {
            cmbBatchNo.Items.Clear();
            txtQuantity.Clear();
            txtDiscountRate.Clear();
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
            txtAmount.Clear();
            txtNarration.Clear();
            lblAvlQty.Text = "";
            txtNarration.Clear();
            cmbItemName.Text = "";
            cmbItemName.Select();
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            batchList.Clear();
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            mTotalOrderAmount = 0f;
            lblTotalOrderAmount.Text = mTotalOrderAmount.ToString("0.00");

            cmbItemName.SelectedIndex = -1;
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            cmbUnit.SelectedIndex = -1;
            txtRate.Clear();
            lblTotQuantity.Text = "----";
            lblTotAmount.Text = "----";
            lblTotalWithTax.Text = "----";
            lblTotalOrderAmount.Text = "----";
            lblTotalIGST.Text = "----";
            lblTotalDiscount.Text = "----";
            lblTotalCESS.Text = "----";
            lblTaxableAmountTotal.Text = "----";
            lblTotalCGST.Text = "----";
            lblTotalSGST.Text = "----";
            txtAmount.Clear();
        }
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Customer, LedgerDetails._Type.showDialog);
            frm.OnClose += Frm_OnClose;
            frm.ShowDialog();
        }
        private void Frm_OnClose(string customer)
        {
            cmbCustomerName.AddCustomers();
            cmbCustomerName.Text = customer;
            cmbCustomerName_Leave(cmbCustomerName, null);
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
        private void btnUnitAdd_Click(object sender, EventArgs e)
        {
            UnitEntry unitentry = new UnitEntry();
            unitentry.onclose += Unitentry_onclose;
            unitentry.ShowDialog();
        }
        private void Unitentry_onclose(string obj)
        {
            cmbUnit.AddUnit();
            cmbUnit.Text = obj;
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
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
        private void ShowUnitDetails()
        {
            string query = "Select * from StockMoreUnit Where UnitMoreID='" +
                           mUnitMoreID + "' and Unit='" + cmbUnit.Text + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string unit = dt.Rows[0]["Unit"].ToString();
                string qty = dt.Rows[0]["Qty"].ToString();
                string unitOfQty = dt.Rows[0]["UnitOfQty"].ToString();
                lblUnitDescription.Text = "1 " + unit + " = " + qty + " " + unitOfQty;
            }
        }
        private void GetSaleRate(string unit)
        {
            if (unit == mHighestUnit)
            {
                txtRate.Text = mHighestSalesRate.toRound();
                lblAvlQty.Text = mHighestStockQty + " " + mHighestUnit;
                double.TryParse(mHighestStockQty, out mmaxqty);
                lblUnitDescription.Text = mHighestMesureUnit.ISNullOrWhiteSpace() ? "" : mLowestMesureUnit.ISNullOrWhiteSpace() ? "1 " + mHighestUnit + " = " + mHighestMesureUnit + " " + mMiddleUnit : "";
            }
            else if (unit == mMiddleUnit)
            {
                txtRate.Text = mMiddlesalesRate.toRound();
                lblAvlQty.Text = mMiddleStockQty + " " + mMiddleUnit;
                double.TryParse(mMiddleStockQty, out mmaxqty);
                lblUnitDescription.Text = mLowestMesureUnit.ISNullOrWhiteSpace() ? "" : "1 " + mMiddleUnit + " = " + mLowestMesureUnit + " " + mLowestUnit;
            }
            else
            {
                txtRate.Text = mLowestSalesRate.toRound();
                lblAvlQty.Text = mLowestQty + " " + mLowestUnit;
                double.TryParse(mLowestQty, out mmaxqty);
            }
        }
        private void pnlItem_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtDiscountRate_Leave_1(object sender, EventArgs e)
        {
            double rate = 0d, disRate = 0d, disAmount = 0d;
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text) > 0)
            {
                if (!txtDiscountRate.Text.ISNullOrWhiteSpace())
                {
                    double.TryParse(txtRate.Text, out rate);
                    double.TryParse(txtDiscountRate.Text, out disRate);
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
        private void OrderEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void BTNcANCLE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Discount Calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateAmount()
        {
            double rate = 0d;
            double qty = 0d;
            double disAmount = 0d;
            double amount = 0d;
            double totalDiscount = 0d;

            double.TryParse(txtRate.Text, out rate);
            double.TryParse(txtQuantity.Text, out qty);
            double.TryParse(txtDiscountAmount.Text, out disAmount);

            totalDiscount = qty * disAmount;
            amount = (qty * rate) - totalDiscount;

            txtAmount.Text = amount.toString();
        }
        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            double rate = 0d, disRate = 0d, disAmount = 0d;
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text) > 0)
            {
                if (!txtDiscountAmount.Text.ISNullOrWhiteSpace())
                {
                    double.TryParse(txtRate.Text, out rate);
                    double.TryParse(txtDiscountAmount.Text, out disAmount);
                    disRate = (disAmount / rate) * 100;
                    txtDiscountRate.Text = disRate.ToString("0.00");
                    txtDiscountAmount.Text = disAmount.ToString("0.00");
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
        private void txtDiscountRate_Leave(object sender, EventArgs e)
        {
            double rate = 0d, disRate = 0d, disAmount = 0d;
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text) > 0)
            {
                if (!txtDiscountRate.Text.ISNullOrWhiteSpace())
                {
                    double.TryParse(txtRate.Text, out rate);
                    double.TryParse(txtDiscountRate.Text, out disRate);
                    disAmount = rate * (disRate / 100);
                    txtDiscountAmount.Text = disAmount.ToString("0.00");
                    if (txtDiscountRate.Text.Contains('.'))
                    {
                        txtDiscountRate.Text = disRate.ToString("0.00");
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
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && txtQuantity.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
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
                    double.TryParse(txtDiscountRate.Text + e.KeyChar, out discountPer);
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
            btnUnitAdd.Visible = false;
            cmbBatchNo.Items.Clear();
            string query = "Select BatchNo from StockSummary " +
                           "where itemid='" + itemID + "' order by id asc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string batchNo = item["BatchNo"].ToString();
                    cmbBatchNo.Items.Add(batchNo);
                }
                cmbBatchNo.SelectedIndex = 0;
            }
            else
            {
                cmbUnit.AddUnit();
                btnUnitAdd.Visible = true;
            }
        }
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
            //CheckStockAvilabelity();
        }
        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbBatchNo.Text.ISNullOrWhiteSpace())
            {
                ShowBatchDetails();
            }
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
                double.TryParse(mHighestStockQty, out mAvailabelQty);
                lblAvlQty.Text = mAvailabelQty.ToString() + " " + mHighestUnit;
                if (mAvailabelQty < 0)
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

                //cmbUnit.Items.Clear();
            }

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
        private void cmbCustomerName_Leave(object sender, EventArgs e)
        {
            ResetAddressData();
            ResetData();
            pnlbillinggst.Hide();
            pnlshippinggst.Hide();
            int index = cmbCustomerName.FindStringExact(cmbCustomerName.Text);
            if (index >= 0)
            {
                pnlbillinggst.Show();
                pnlshippinggst.Show();
                cmbCustomerName.SelectedIndex = index;
                string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
                GetCustomerAddressDetails(customerID);
                GetCustomerShippedDetails(customerID);
            }
            else
            {
                cmbCustomerName.Text = "";
            }
        }
        private void cmbQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                if (MessageBox.Show("Are you sure?\nOrder amount Rs. " + mTotalOrderAmount, "Order Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OrderSave();
                }
            }
        }
        private void cmbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
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
    }
}
