using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class StockEntry : Form
    {
        public event Action OnClose;
        private bool mIsSuccess = false, mIsPreviousBatch = false,mIsManualy=false;
        private string msg = "";
        private string mItemId = "";
        private List<string> mLstUnit = new List<string>();
        private DataTable mDtMoreUnitDetails;
        private string mUnitMoreID = "";
        private string mPurchaseQty = "0";

        private Dictionary<string, string> mDicBillNo = new Dictionary<string, string>();
        public StockEntry(string billNo)
        {
            InitializeComponent();
            this.FitToVertical();
            InitUnitMoreTable();
            MoreUnitClear();
            cmbHighestUnit.AddUnit();
            cmbItemName.AddItem();
            GetBillNo();
            lblPurchaseRateSales.Visible = true;
            txtPurchaseRateTosALES.Visible = true;
            if (!billNo.ISNullOrWhiteSpace())
            {
                string invoiceno = "";
                mDicBillNo.TryGetValue(billNo, out invoiceno);
                cmbBillID.Text = invoiceno;
            }
            if (OtherSettingTools._IsMrpPercent)
            {
                pnlSaleRatePer.Hide();
            }
        }
        public StockEntry()
        {
            InitializeComponent();
            this.FitToVertical();
            mIsManualy = true;
            InitUnitMoreTable();
            MoreUnitClear();
            cmbHighestUnit.AddUnit();
            MergeUnits();
            pnlPurchaseDetails.Visible = false;
            splitContainer1.SplitterDistance = 170;
            cmbItemName.AddItem();
            pnlItemDetails.Enabled = true;
            cmbItemName.Enabled = true;
            btnitemadd.Enabled = true;
            cmbItemName.Select();
            if (OtherSettingTools._IsMrpPercent)
            {
                pnlSaleRatePer.Hide();
            }
        }
        private void InitUnitMoreTable()
        {
            mDtMoreUnitDetails = new DataTable();
            mDtMoreUnitDetails.Columns.Add("UnitMoreID", typeof(string));
            mDtMoreUnitDetails.Columns.Add("Unit", typeof(string));
            mDtMoreUnitDetails.Columns.Add("SalesRate", typeof(string));
            mDtMoreUnitDetails.Columns.Add("Qty", typeof(string));
            mDtMoreUnitDetails.Columns.Add("UnitOfQty", typeof(string));
        }
        private void GetBillNo()
        {
            mDicBillNo.Clear();
            if (cmbBillID.Items.Count > 0)
            {
                if (cmbBillID.DataSource != null)
                {
                    cmbBillID.DataSource = null;
                }
                else
                {
                    cmbBillID.Items.Clear();
                }
            }
            string query = "Select BillNo,InvoiceNo from PurchaseBill where" +
                " BillNO not in (select PurchaseBillNO from StockHistory" +
                " where PurchaseBillNO <> 'NULL' ) and Status<>'Cancel'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string billNo = row["BillNo"].ToString();
                    string invoiceNo = "Bill- " + billNo + " // Invoice- " + row["InvoiceNo"].ToString();
                    mDicBillNo.Add(billNo, invoiceNo);
                }
                if (!mDicBillNo.IsNullOrEmpty())
                {
                    cmbBillID.DataSource = new BindingSource(mDicBillNo, null);
                    cmbBillID.DisplayMember = "Value";
                    cmbBillID.ValueMember = "Key";
                    cmbBillID.SelectedIndex = -1;
                }
            }
            if (cmbBillID.Items.Count > 0)
            {
                cmbBillID.SelectedIndex = 0;
            }

        }
        private void MergeUnits()
        {
            mLstUnit.Clear();
            if (!UnitTools._DicUnit.IsNullOrEmpty())
            {
                foreach (var unit in UnitTools._DicUnit)
                {
                    mLstUnit.Add(unit.Value.ToString());
                }
                mLstUnit.Remove(cmbHighestUnit.Text);
            }
        }
        private void cmbBillID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbBillID.Text.ISNullOrWhiteSpace())
            {
                dgvBillDetails.SelectionChanged -= dgvBillDetails_SelectionChanged;
                string billNo = ((KeyValuePair<string, string>)cmbBillID.SelectedItem).Key.ToString();
                GetBillDetails(billNo);
                dgvBillDetails.SelectionChanged += dgvBillDetails_SelectionChanged;
                if (dgvBillDetails.RowCount>0)
                {
                    dgvBillDetails.Rows[0].Selected = true;
                }
            }
        }
        private void GetBillDetails(string billNo)
        {
            dgvBillDetails.Rows.Clear();
            string query = "Select  PurchaseBillDetails.*,PurchaseBill.* from PurchaseBill " +
                           "inner join PurchaseBillDetails on PurchaseBillDetails.Billid = PurchaseBill.BillID " +
                           "where BillNo='" + billNo + "' ";
            DataTable DT = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (DT.IsValidDataTable())
            {
                foreach (DataRow item in DT.Rows)
                {
                    string id = item["ItemID"].ToString();
                    string ItemName = item["ItemName"].ToString();
                    string Qty = item["Quantity"].ToString();
                    string Rate = item["Rate"].toRound();
                    string Unit = item["Unit"].ToString();
                    string Amt = item["Amount"].toRound();
                    string dicountAmt = item["DiscountAmount"].toRound();
                    string CGST = item["CGSTAmount"].toRound();
                    string SGST = item["SGSTAmount"].toRound();
                    string IGST = item["IGSTAmount"].toRound();
                    string CESS = item["CeassAmount"].toRound();
                    string totAmt = item["Total"].toRound();
                    dgvBillDetails.Rows.Add(id, ItemName, Qty, Rate, Unit, Amt, dicountAmt, CGST, SGST, IGST, CESS, totAmt);
                }
                dgvBillDetails.ClearSelection();
            }
        }
        /// <summary>
        /// View item details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBillDetails_SelectionChanged(object sender, EventArgs e)
        {
            ItemDataClear();
            if (dgvBillDetails.SelectedRows.Count > 0)
            {
                pnlItemDetails.Enabled = true;
                mItemId = dgvBillDetails.SelectedRows[0].Cells["ItemIdPurchase"].Value.ToString();

                string itemName = dgvBillDetails.SelectedRows[0].Cells["ItemNamePurchase"].Value.ToString();
                string qty = dgvBillDetails.SelectedRows[0].Cells["QtyPurchase"].Value.ToString();
                string rate = dgvBillDetails.SelectedRows[0].Cells["RatePurchase"].Value.ToString();
                string unit = dgvBillDetails.SelectedRows[0].Cells["UnitPurchase"].Value.ToString();

                cmbItemName.Text = itemName;
                cmbItemName_Leave(null,null);
                txtHighestTotalStock.Text = qty;
                mPurchaseQty = qty;
                lblPurchaseUnit.Text = unit;
                txtPurchaseRate.Text = rate;
                lblPurchaseRatePerUnit.Text = "/ " + unit;
                txtBatchNo_TextChanged(null, null);
                cmbHighestUnit.Text = unit;
                txtBatchNo.Select();

            }
            else
            {
                pnlItemDetails.Enabled = false;
            }
        }
        private void ItemDataClear()
        {
            mItemId = "";
            mUnitMoreID = "";
            //cmbItemName.Text = "";
            txtBatchNo.Clear();
            txtBatchNo.AutoCompleteCustomSource.Clear();
            dtmMfgDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            dtmExpDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            ///txtMRP.Clear();
            numericUpDownExpDate.Value = 0;
            cmbTypeOfDate.SelectedIndex = -1;
            dtmExpDate.Enabled = true;

            txtPurchaseRate.Clear();
            mPurchaseQty = "0";
            lblPurchaseUnit.Text = "_____";
            txtPurchaseRateTosALES.Clear();

            cmbHighestUnit.SelectedIndex = -1;
            txtHighestSalesRateNoTax.Clear();
            txtHighestMRP.Clear();
            txtHighestTotalStock.Clear();

            chkMoreUnit.Checked = false;
            chkMoreUnit.Enabled = false;

            MoreUnitClear();
            pnlUnit.Enabled = false;

            pnlValidUpTo.Enabled = true;
            cmbHighestUnit.Enabled = false;
            txtHighestTotalStock.Enabled = false;
            txtHighestSalesRateNoTax.Enabled = false;
            txtHighestMRP.Enabled = false;

        }
        private List<string> AddBatchNos(string itemID)  ///Add batch nos in AutoCompleteCustomSource
        {
            List<string> lstBatchNos = new List<string>();
            txtBatchNo.AutoCompleteCustomSource.Clear();
            string query = "Select BatchNo from StockHistory where ItemId='" + itemID + "' order by ID desc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string batchNo = item["BatchNo"].ToString();
                    lstBatchNos.Add(batchNo);
                }
            }
            return lstBatchNos;
        }
        private void ShowBatchDetails()
        {
            if (cmbHighestUnit.Items.Count > 0)
            {
                if (cmbHighestUnit.DataSource != null)
                {
                    cmbHighestUnit.DataSource = null;
                }
                else
                {
                    cmbHighestUnit.Items.Clear();
                }
            }
            string batchNo = txtBatchNo.Text.GetDBFormatString();
            string query = "Select * from StockSummary where ItemID ='" +
                           mItemId + "' and BatchNo= '" + batchNo + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mIsPreviousBatch = true;
                txtBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
                dtmMfgDate.Text = dt.Rows[0]["MFGDate"].ToString();
                dtmExpDate.Text = dt.Rows[0]["EXPDate"].ToString();
                string highestunit = dt.Rows[0]["HighestUnit"].ToString();
                string higheststockqty = dt.Rows[0]["HighestStockQty"].ToString();
                string highestrateo = dt.Rows[0]["HighestRate"].toRound();
                string highestMRP = dt.Rows[0]["HighestMRP"].ToString();
                string middleunit = dt.Rows[0]["MiddleUnit"].ToString();
                string middlestockqty = dt.Rows[0]["MiddleStockQty"].ToString();
                string middlerateo = dt.Rows[0]["MiddleRate"].toRound();
                string middleMRP = dt.Rows[0]["MiddleMRP"].ToString();
                string lowestunit = dt.Rows[0]["LowestUnit"].ToString();
                string loweststockqty = dt.Rows[0]["LowestStockQty"].ToString();
                string lowestrate = dt.Rows[0]["LowestRate"].toRound();
                string lowestMRP = dt.Rows[0]["LowestMRP"].ToString();
                string highestMeasureQty = dt.Rows[0]["HighestMeasureQty"].ToString();
                string lowestMeasureQty = dt.Rows[0]["LowestMeasureQty"].ToString();

                if (!dt.Rows[0]["PurchaseRate"].ToString().ISNullOrWhiteSpace() && !dt.Rows[0]["PurchaseUnit"].ToString().ISNullOrWhiteSpace())
                {
                    txtHighestTotalStock.Text = dt.Rows[0]["PurchaseQty"].ToString();
                    lblPurchaseUnit.Text = dt.Rows[0]["PurchaseUnit"].ToString();
                    txtPurchaseRate.Text = dt.Rows[0]["PurchaseRate"].toRound();
                    lblPurchaseRatePerUnit.Text = "/ " + dt.Rows[0]["PurchaseUnit"].ToString();
                }
                else
                {
                    txtHighestTotalStock.Clear();
                    lblPurchaseUnit.Text = "_____";
                    txtPurchaseRate.Clear();
                    lblPurchaseRatePerUnit.Text = "_____";
                }
                //FOR PARTICULAR UNITS
                if (!highestunit.ISNullOrWhiteSpace())
                {
                    cmbHighestUnit.Items.Add(highestunit);
                    if (!middleunit.ISNullOrWhiteSpace())
                    {
                        cmbHighestUnit.Items.Add(middleunit);
                        if (!lowestunit.ISNullOrWhiteSpace())
                        {
                            cmbHighestUnit.Items.Add(lowestunit);
                        }
                    }
                }
                cmbHighestUnit.Text = highestunit;
                txtHighestMRP.Text = highestMRP;
                //txtHighestSalesRate.Text = highestrateo;
                if (!highestMeasureQty.ISNullOrWhiteSpace())
                {
                    chkMoreUnit.Checked = true;
                    cmbMiddleUnit.Text = middleunit;
                    txtHighestMesure.Text = highestMeasureQty;
                    txtMiddleMRP.Text = middleMRP;
                    txtMiddleSalesRate.Text = middlerateo.toRound();
                }
                else
                {
                    chkMoreUnit.Checked = false;
                }

                if (!lowestMeasureQty.ISNullOrWhiteSpace())
                {
                    btnAddUnitRow_Click(null, null);
                    cmbLowestUnit.Text = lowestunit;
                    txtLowestMesure.Text = lowestMeasureQty;
                    txtLowestMRP.Text = lowestMRP;
                    txtLowestSalesRate.Text = lowestrate.toRound();
                }

                pnlMoreUnit.Enabled = false;
                dtmMfgDate.Enabled = false;
                dtmExpDate.Enabled = false;
                //txtMRP.Enabled = false;
                pnlValidUpTo.Enabled = false;
                txtHighestSalesRateNoTax.Enabled = false;
                txtHighestMRP.Enabled = false;
                btnAddUnit.Enabled = false;
                pnlUnit.Enabled = false;
                txtPurchaseRateTosALES.Enabled = false;
                txtHighestSalesRateWithTax.Enabled = false;
                txtPurchaseRate.Enabled = false;
            }
            else
            {
                mIsPreviousBatch = false;
                cmbHighestUnit.AddUnit();
                
                if (!cmbBillID.Text.ISNullOrWhiteSpace())
                {
                    cmbHighestUnit.Text = dgvBillDetails.SelectedRows[0].Cells["UnitPurchase"].Value.ToString();
                }
                txtHighestSalesRateNoTax.Enabled = true;
                txtHighestMRP.Enabled = true;
                txtPurchaseRateTosALES.Enabled = true;
                txtHighestSalesRateWithTax.Enabled = true;
                txtPurchaseRate.Enabled = true;
                pnlMoreUnit.Enabled = true;
                chkMoreUnit.Checked = false;
                // mIsPreviousBatch = true;
                dtmMfgDate.Enabled = true;
                dtmExpDate.Enabled = true;
                //txtMRP.Enabled = true;
                pnlValidUpTo.Enabled = true;
                cmbHighestUnit.Enabled = true;
                txtHighestSalesRateNoTax.Enabled = true;
                chkMoreUnit.Enabled = false;
                btnAddUnit.Enabled = true;
                pnlUnit.Enabled = true;
                mUnitMoreID = "";
                MoreUnitClear();
            }
        }
        private void ShowUnitMoreDetails(string unitMoreID)
        {
            if (cmbHighestUnit.Items.Count > 0)
            {
                if (cmbHighestUnit.DataSource != null)
                {
                    cmbHighestUnit.DataSource = null;
                }
                else
                {
                    cmbHighestUnit.Items.Clear();
                }
            }
            string query = "Select * from StockMoreUnit " +
                           "where UnitMoreID ='" + unitMoreID + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                #region Show DAta
                cmbHighestUnit.SelectedIndexChanged -= cmbHighestUnit_SelectedIndexChanged;
                cmbMiddleUnit.SelectedIndexChanged -= cmbMiddleUnit_SelectedIndexChanged;
                cmbLowestUnit.SelectedIndexChanged -= cmbUnit3_SelectedIndexChanged;
                if (dt.Rows.Count == 1)
                {
                    string unit = dt.Rows[0]["Unit"].ToString();
                    double salerate = 0d, qty = 0d;
                    double.TryParse(dt.Rows[0]["SaleRate"].ToString().ToString(), out salerate);
                    double.TryParse(dt.Rows[0]["Qty"].ToString().ToString(), out qty);
                    string unitOfQty = dt.Rows[0]["UnitOfQty"].ToString();
                    if (mLstUnit.IsValidList())
                    {
                        cmbHighestUnit.Items.AddRange(mLstUnit.ToArray());
                        cmbHighestUnit.SelectedIndexChanged += cmbHighestUnit_SelectedIndexChanged;
                        cmbHighestUnit.Text = unit;
                    }
                    txtHighestTotalStock.Text = qty.ToString();
                    txtHighestSalesRateNoTax.Text = salerate.ToString();
                }
                if (dt.Rows.Count == 2)
                {
                    string unit = dt.Rows[0]["Unit"].ToString();
                    double salerate = 0d, qty = 0d;
                    double.TryParse(dt.Rows[0]["SaleRate"].ToString().ToString(), out salerate);
                    double.TryParse(dt.Rows[0]["Qty"].ToString().ToString(), out qty);
                    string unitOfQty = dt.Rows[0]["UnitOfQty"].ToString();
                    if (mLstUnit.IsValidList())
                    {
                        cmbMiddleUnit.Items.AddRange(mLstUnit.ToArray());
                        cmbMiddleUnit.SelectedIndexChanged += cmbMiddleUnit_SelectedIndexChanged;
                        cmbMiddleUnit.Text = unit;
                    }
                    txtHighestMesure.Text = qty.ToString();
                    txtMiddleMRP.Text = salerate.ToString();
                    lblHigestUnit2.Text = unitOfQty;
                }
                if (dt.Rows.Count == 3)
                {
                    pnlUnit2.Show();
                    string unit = dt.Rows[0]["Unit"].ToString();
                    double salerate = 0d, qty = 0d;
                    double.TryParse(dt.Rows[0]["SaleRate"].ToString(), out salerate);
                    double.TryParse(dt.Rows[0]["Qty"].ToString(), out qty);
                    string unitOfQty = dt.Rows[0]["UnitOfQty"].ToString();
                    if (mLstUnit.IsValidList())
                    {
                        cmbLowestUnit.Items.AddRange(mLstUnit.ToArray());
                        cmbLowestUnit.SelectedIndexChanged += cmbUnit3_SelectedIndexChanged;
                        cmbLowestUnit.Text = unit;
                    }
                    txtLowestMesure.Text = qty.ToString();
                    txtLowestMRP.Text = salerate.ToString();
                    lblMiddleUnit2.Text = unitOfQty;
                }
                else
                {

                }
                #endregion
            }
        }
        private void txtBatchNo_Leave(object sender, EventArgs e)
        {
            if (txtBatchNo.Text.ISNullOrWhiteSpace())
            {
                cmbHighestUnit.SelectedIndex = -1;
                txtHighestTotalStock.Clear();
                txtHighestSalesRateNoTax.Clear();
                txtHighestSalesRateWithTax.Clear();
                txtPurchaseRateTosALES.Clear();
                txtHighestMRP.Clear();
                cmbHighestUnit.Enabled = false;
                txtHighestTotalStock.Enabled = false;
                txtHighestSalesRateNoTax.Enabled = false;
                txtHighestMRP.Enabled = false;
                txtPurchaseRateTosALES.Enabled = false;
                txtHighestSalesRateWithTax.Enabled = false;
            }
            else
            {
                txtHighestTotalStock.Enabled = true;
                ShowBatchDetails();
            }
        }

        /// <summary>
        /// Add Item details into below list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidItemDetails())
            {
                // AddIntoUnitMoreList();    //Add unit more first
                AddItemInToList();
                cmbItemName.Text = "";
                ItemDataClear();
                mIsPreviousBatch = false;
                if (!mIsManualy)
                {
                    DeleteRowFromItemList();
                }
                cmbBillID.Enabled = false;

            }
        }
        private bool IsValidItemDetails()
        {

            if (cmbItemName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select item name.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
                return false;
            }
            if (txtBatchNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter batch no.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtBatchNo.Select();
                return false;
            }
            if (OtherSettingTools._IsMrpPercent)
            {
                if (txtHighestMRP.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter MRP.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHighestMRP.Select();
                    return false;
                }
            }
            if (cmbHighestUnit.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select unit.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbHighestUnit.Focus(); return false;
            }
            if (txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter rate.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtHighestSalesRateNoTax.Focus(); return false;
            }
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter quantity.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtHighestTotalStock.Focus(); return false;
            }
            if (!cmbMiddleUnit.Text.ISNullOrWhiteSpace())
            {
                if (txtHighestMesure.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter Mesure of Highest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHighestMesure.Select();
                    return false;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    if (txtMiddleMRP.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter MRP of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtMiddleMRP.Select();
                        return false;
                    }
                }
                if (txtMiddleSalesRate.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter sales rate of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtMiddleSalesRate.Select();
                    return false;
                }
            }
            if (!cmbLowestUnit.Text.ISNullOrWhiteSpace())
            {
                if (txtLowestMesure.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter Mesure of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtLowestMesure.Select();
                    return false;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    if (txtLowestMRP.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter MRP of Lowest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtLowestMRP.Select();
                        return false;
                    }
                }
                if (txtLowestSalesRate.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter sales rate of Lowest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtLowestSalesRate.Select();
                    return false;
                }
            }
            return true;
        }
        private void AddIntoUnitMoreList()
        {
            if (mUnitMoreID.ISNullOrWhiteSpace())
            {
                mUnitMoreID = Guid.NewGuid().ToString();
                mDtMoreUnitDetails.Rows.Add(mUnitMoreID, cmbHighestUnit.Text, txtHighestSalesRateNoTax.Text, txtHighestTotalStock.Text, "");
                if (!cmbMiddleUnit.Text.ISNullOrWhiteSpace() &&
                    !txtHighestMesure.Text.ISNullOrWhiteSpace() &&
                    !txtMiddleMRP.Text.ISNullOrWhiteSpace())
                {
                    string unit = cmbMiddleUnit.Text;
                    string rate = txtMiddleMRP.Text;
                    string qty = txtHighestMesure.Text;
                    string unitOfQty = unit;
                    mDtMoreUnitDetails.Rows.Add(mUnitMoreID, unit, rate, qty, unitOfQty);
                }
                if (!cmbLowestUnit.Text.ISNullOrWhiteSpace() &&
                    !txtLowestMesure.Text.ISNullOrWhiteSpace() &&
                    !txtLowestMRP.Text.ISNullOrWhiteSpace())
                {
                    string unit = cmbLowestUnit.Text;
                    string rate = txtLowestMRP.Text;
                    string qty = txtLowestMesure.Text;
                    string unitOfQty = unit;
                    mDtMoreUnitDetails.Rows.Add(mUnitMoreID, unit, rate, qty, unitOfQty);
                }
            }
        }
        private void AddItemInToList()
        {
            string iteamName = cmbItemName.Text.GetDBFormatString();
            string batchNo = txtBatchNo.Text.GetDBFormatString();
            string mfgDate = dtmMfgDate.Text.GetDBFormatString();
            string expDate = dtmExpDate.Text.GetDBFormatString();

            string highesttunit = cmbHighestUnit.Text.GetDBFormatString();
            string highestStockQty = txtHighestTotalStock.Text.GetDBFormatString();
            string highestrate = txtHighestSalesRateNoTax.Text.GetDBFormatString();
            string highestMrp = txtHighestMRP.Text.GetDBFormatString();

            string midunit = cmbMiddleUnit.Text.GetDBFormatString();
            string middlestockqty = midunit.ISNullOrWhiteSpace() ? "" : lblMiddleTotalStock.Text.GetDBFormatString();
            string middlerate = midunit.ISNullOrWhiteSpace() ? "" : txtMiddleSalesRate.Text.GetDBFormatString();
            string middlemrp = midunit.ISNullOrWhiteSpace() ? "" : txtMiddleMRP.Text.GetDBFormatString();

            string lowestunit = cmbLowestUnit.Text.GetDBFormatString();
            string lowestTotalStock = lowestunit.ISNullOrWhiteSpace() ? "" : lblLowestTotalStock.Text.GetDBFormatString();
            string lowestrate = lowestunit.ISNullOrWhiteSpace() ? "" : txtLowestSalesRate.Text.GetDBFormatString();
            string lowestmrp = lowestunit.ISNullOrWhiteSpace() ? "" : txtLowestMRP.Text.GetDBFormatString();

            string higestMesureQty = midunit.ISNullOrWhiteSpace() ? "" : txtHighestMesure.Text.GetDBFormatString();
            string lowestMesureQty = lowestunit.ISNullOrWhiteSpace() ? "" : txtLowestMesure.Text.GetDBFormatString();

            // string mrp = txtMRP.Text.GetDBFormatString();
            string purchaseqty = txtHighestTotalStock.Text.ISNullOrWhiteSpace() ? "" : txtHighestTotalStock.Text;
            string purchaseRate = txtPurchaseRate.Text.ISNullOrWhiteSpace() ? "" : txtPurchaseRate.Text;
            string purchaseUnit = lblPurchaseUnit.Text.ISNullOrWhiteSpace() ? "" : lblPurchaseUnit.Text.GetDBFormatString();

            if (mIsPreviousBatch)
            {
                dgvStockUpdate.Rows.Add(mItemId, iteamName, batchNo, mfgDate, expDate, highesttunit,
                                  highestStockQty, highestrate, highestMrp);

            }
            else
            {
                dgvStockUpdate.Rows.Add(mItemId, iteamName, batchNo, mfgDate, expDate, highesttunit,
                                  highestStockQty, highestrate, highestMrp, midunit, middlestockqty,
                                  middlerate, middlemrp, lowestunit, lowestTotalStock, lowestrate,
                                  lowestmrp, higestMesureQty, lowestMesureQty, purchaseqty, purchaseRate, purchaseUnit);

            }

        }
        private void DeleteRowFromItemList()
        {
            if (dgvBillDetails.SelectedRows.Count > 0)
            {
                double purchhaseqty = 0d, stockqty = 0d, remningstock = 0d;
                double.TryParse(mPurchaseQty, out purchhaseqty);
                double.TryParse(txtHighestTotalStock.Text, out stockqty);
                remningstock = purchhaseqty - stockqty;
                if (remningstock <= 0)
                {
                    dgvBillDetails.Rows.RemoveAt(dgvBillDetails.CurrentRow.Index);
                }
                else
                {
                    dgvBillDetails.SelectedRows[0].Cells["QtyPurchase"].Value = remningstock.ToString();
                }
                if (dgvBillDetails.SelectedRows.Count > 0)
                {
                    dgvBillDetails.ClearSelection();
                    dgvBillDetails.Rows[dgvBillDetails.CurrentRow.Index].Selected = true;
                    //dgvBillDetails_CellClick(null, null);
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAll();
        }
        private void ResetAll()
        {
            if (dgvStockUpdate.Rows.Count > 0)
            {
                ItemDataClear();
                cmbItemName.Text = "";
                mDtMoreUnitDetails.Rows.Clear();
                dgvStockUpdate.Rows.Clear();
                dgvBillDetails.Rows.Clear();
                cmbBillID.SelectedIndex = -1;
                cmbBillID.Select();
                cmbBillID.Enabled = true;

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mIsManualy || dgvBillDetails.Rows.Count <= 0 )
            {
                if (dgvStockUpdate.Rows.Count > 0)
                {
                    SaveData();
                }
            }
            else
            {
                MessageBox.Show("Add all item bellow in the list the list", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void SaveData()
        {
            List<string> lstQuery = new List<string>();
            string query = "";
            //StockUnitMoreInsertQuery(ref lstQuery);
            foreach (DataGridViewRow row in dgvStockUpdate.Rows)
            {
                #region Data Assign
                string purchaseBilno = cmbBillID.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + ((KeyValuePair<string, string>)cmbBillID.SelectedItem).Key.ToString() + "'";

                object itemIDobj = row.Cells["ItemIDStock"].Value;
                string itemID = itemIDobj.ISValidObject() ? itemIDobj.ToString() : "NULL";

                object batchNoobj = row.Cells["BatchNo"].Value;
                string batchNo = batchNoobj.ISValidObject() ? "'" + batchNoobj.ToString() + "'" : "NULL";

                object mfgDate = row.Cells["MfgDate"].Value;
                string mfgdt = mfgDate.ISValidObject() ? "'" + mfgDate.ToString() + "'" : "NULL";

                object expDate = row.Cells["EXPDate"].Value;
                string expdt = expDate.ISValidObject() ? "'" + expDate.ToString() + "'" : "NULL";

                object highestunitobj = row.Cells["HighestUnit"].Value;
                string highestunit = highestunitobj.ISValidObject() ? "'" + highestunitobj.ToString() + "'" : "NULL";

                object higheststockqtyobj = row.Cells["HighestStockQty"].Value;
                string higheststockqty = higheststockqtyobj.ISValidObject() ? higheststockqtyobj.ToString() : "NULL";

                object highestrateobj = row.Cells["HighestRate"].Value;
                string highestrateo = highestrateobj.ISValidObject() ? highestrateobj.ToString() : "NULL";

                object highestMRPobj = row.Cells["HighestMRP"].Value;
                string highestMRP = highestMRPobj.ISValidObject() ? highestMRPobj.ToString() : "NULL";

                object middleunitobj = row.Cells["MiddleUnit"].Value;
                string middleunit = middleunitobj.ISValidObject() ? "'" + middleunitobj.ToString() + "'" : "NULL";

                object middlestockqtyobj = row.Cells["MiddleStockQty"].Value;
                string middlestockqty = middlestockqtyobj.ISValidObject() ? middlestockqtyobj.ToString() : "NULL";

                object middlerateobj = row.Cells["MiddleRate"].Value;
                string middlerateo = middlerateobj.ISValidObject() ? middlerateobj.ToString() : "NULL";

                object middleMRPobj = row.Cells["MiddleMRP"].Value;
                string middleMRP = middleMRPobj.ISValidObject() ? middleMRPobj.ToString() : "NULL";

                object lowestunitobj = row.Cells["LowestUnit"].Value;
                string lowestunit = lowestunitobj.ISValidObject() ? "'" + lowestunitobj.ToString() + "'" : "NULL";

                object loweststockqtyobj = row.Cells["LowestStockQty"].Value;
                string loweststockqty = loweststockqtyobj.ISValidObject() ? loweststockqtyobj.ToString() : "NULL";

                object lowestrateobj = row.Cells["LowestRate"].Value;
                string lowestrate = lowestrateobj.ISValidObject() ? lowestrateobj.ToString() : "NULL";

                object lowestMRPobj = row.Cells["LowestMRP"].Value;
                string lowestMRP = lowestMRPobj.ISValidObject() ? lowestMRPobj.ToString() : "NULL";

                object highestMeasureQtyobj = row.Cells["HighestMeasureQty"].Value;
                string highestMeasureQty = highestMeasureQtyobj.ISValidObject() ? highestMeasureQtyobj.ToString() : "NULL";

                object lowestMeasureQtyobj = row.Cells["LowestMeasureQty"].Value;
                string lowestMeasureQty = lowestMeasureQtyobj.ISValidObject() ? lowestMeasureQtyobj.ToString() : "NULL";

                object purchaseQtyobj = row.Cells["PurchaseQty"].Value;
                string purchaseQty = purchaseQtyobj.ISValidObject() ? purchaseQtyobj.ToString() : "NULL";

                object purchaseRateobj = row.Cells["PurchaseRate"].Value;
                string purchaseRate = purchaseRateobj.ISValidObject() ? purchaseRateobj.ToString() : "NULL";

                object purchaseUnitobj = row.Cells["PurchaseUnit"].Value;
                string purchaseUnit = purchaseUnitobj.ISValidObject() ? "'" + purchaseUnitobj.ToString() + "'" : "NULL";

                #endregion

                query = "Insert into StockHistory( PurchaseBillNo, ItemID, BatchNo, MfgDate, ExpDate," +
                    " HighestUnit, HighestStockQty, HighestRate, HighestMRP, MiddleUnit, MiddleStockQty," +
                    " MiddleRate, MiddleMRP, LowestUnit,LowestStockQty, LowestRate, LowestMRP, HighestMeasureQty," +
                    " LowestMeasureQty, PurchaseQty, PurchaseRate, PurchaseUnit) " +
                        "Values(" + purchaseBilno + "," + itemID + "," + batchNo + "," + mfgdt + "," +
                        expdt + "," + highestunit + "," + higheststockqty + "," + highestrateo + "," +
                        highestMRP + "," + middleunit + "," + middlestockqty + "," + middlerateo + "," +
                        middleMRP + "," + lowestunit + "," + loweststockqty + "," + lowestrate + "," + lowestMRP
                        + "," + highestMeasureQty + "," + lowestMeasureQty + "," + purchaseQty + "," + purchaseRate + "," + purchaseUnit + ")";
                lstQuery.Add(query);

                if (!UpdateStockSummary(itemID, batchNo, higheststockqty, highestunit, ref lstQuery))
                {
                    query = "Insert into StockSummary ( ItemID, BatchNo, MfgDate, ExpDate, HighestUnit, HighestStockQty," +
                        " HighestRate, HighestMRP, MiddleUnit, MiddleStockQty, MiddleRate, MiddleMRP, LowestUnit, LowestStockQty," +
                        "LowestRate, LowestMRP, HighestMeasureQty, LowestMeasureQty, PurchaseQty, PurchaseRate, PurchaseUnit) values(" +
                         itemID + "," + batchNo + "," + mfgdt + "," + expdt + "," + highestunit + "," + higheststockqty + "," + highestrateo + "," +
                        highestMRP + "," + middleunit + "," + middlestockqty + "," + middlerateo + "," +
                        middleMRP + "," + lowestunit + "," + loweststockqty + "," + lowestrate + "," + lowestMRP
                        + "," + highestMeasureQty + "," + lowestMeasureQty + "," + purchaseQty + "," + purchaseRate + "," + purchaseUnit + ")";

                    lstQuery.Add(query);

                }
            }
            if (SQLHelper.GetInstance().ExecuteTransection(lstQuery, out msg))
            {
                mIsSuccess = true;
                MessageBox.Show("Stock Updated.", "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAll();
                if (!mIsManualy)
                {
                    GetBillNo();
                }
                KillCalculatorRunning();
            }
            else
            {
                MessageBox.Show("Error while stock updating.\n" + msg, "Stock Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void StockUnitMoreInsertQuery(ref List<string> lstQuery)
        {
            foreach (DataRow item in mDtMoreUnitDetails.Rows)
            {
                string unitMoreID = item["UnitMoreID"].ToString();
                string unit = item["Unit"].ToString();
                string salesRate = item["SalesRate"].ToString();
                string qty = item["Qty"].ToString();
                string unitOfQty = item["UnitOfQty"].ToString();
                string query = "Insert into StockMoreUnit( UnitMoreID, Unit, SaleRate, Qty, UnitOfQty) " +
                               "Values('" + unitMoreID + "','" + unit + "'," + salesRate + "," + qty + ",'" + unitOfQty + "')";
                lstQuery.Add(query);
            }
        }
        private bool UpdateStockSummary(string itemID, string batchno, string currentqtystr, string currentunit, ref List<string> lstQuery)
        {
            string higestunit = "", midunt = "", loestunit = "", setqry = string.Empty;
            double higestpreviosqty = 0d, midpreviousqty = 0d, lowestpreviousqty = 0d,
                higestmesure = 0d, lowestmesure = 0d, currentqty = 0d;
            string query = "Select  HighestUnit, HighestStockQty, MiddleUnit," +
                " MiddleStockQty, LowestUnit, LowestStockQty,HighestMeasureQty, " +
                "LowestMeasureQty from StockSummary where ItemID='" +
                itemID + "' and BatchNo=" + batchno + "";
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
                        itemID + "' and BatchNo = " + batchno + "";

                lstQuery.Add(query);
                return true;
            }
            return false;
        }

        /// <summary>
        /// All Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtmMfgDate_ValueChanged(object sender, EventArgs e)
        {
            if (!cmbTypeOfDate.Text.ISNullOrWhiteSpace())
            {
                string ExpValue = numericUpDownExpDate.Value.ToString();
                string ExpTyp = cmbTypeOfDate.Text;
                CalculateExpDate(ExpValue, ExpTyp);
            }
        }
        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                //if (e.KeyChar == '.' && txtMRP.Text.Contains('.'))
                //{
                //    e.Handled = true;
                //}

            }
            else
            {
                e.Handled = true;
            }
        }
        private void numericUpDownExpDate_ValueChanged(object sender, EventArgs e)
        {
            if (!cmbTypeOfDate.Text.ISNullOrWhiteSpace())
            {
                string ExpValue = numericUpDownExpDate.Value.ToString();
                string ExpTyp = cmbTypeOfDate.Text;
                CalculateExpDate(ExpValue, ExpTyp);
            }
        }
        private void cmbTypeOfDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbTypeOfDate.Text.ISNullOrWhiteSpace())
            {
                string ExpValue = numericUpDownExpDate.Value.ToString();
                string ExpTyp = cmbTypeOfDate.Text;
                CalculateExpDate(ExpValue, ExpTyp);
            }

        }
        private void txtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void CalculateExpDate(string valueExp, string type)
        {
            dtmExpDate.ResetText();
            DateTime expDate = dtmExpDate.Value;
            DateTime mfgDate = dtmMfgDate.Value;
            if (type == "D")
            {
                dtmExpDate.ResetText();
                expDate = mfgDate.AddDays(int.Parse(valueExp));
                dtmExpDate.Text = expDate.ToString();
            }
            if (type == "M")
            {
                expDate = mfgDate.AddMonths(int.Parse(valueExp));
                dtmExpDate.Text = expDate.ToString();
            }
            if (type == "Y")
            {
                expDate = mfgDate.AddYears(int.Parse(valueExp));
                dtmExpDate.Text = expDate.ToString();
            }
        }
        private void StockEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess)
            {
                OnClose();
            }
        }
        private void chkMoreUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMoreUnit.Checked)
            {
                MergeUnits();
                pnlUnit.Enabled = true;
                pnlUnit1.Enabled = true;
                btnAddUnitRow.Enabl();
                btnUnitReset.Enabl();
                cmbMiddleUnit.Items.Clear();
                cmbMiddleUnit.Items.AddRange(mLstUnit.ToArray());

                lblHighMesure.Text = cmbHighestUnit.Text + " Mesure";

                if (!mIsPreviousBatch)
                {
                    pnlUnitSmall.Enabled = false;
                }
            }
            else
            {
                MoreUnitClear();
            }


        }
        private void MoreUnitClear()
        {
            btnAddUnitRow.Disabl();
            btnUnitReset.Disabl();
            pnlUnit.Enabled = false;
            pnlUnitSmall.Enabled = true;


            cmbMiddleUnit.SelectedIndex = -1;
            txtHighestMesure.Clear();
            lblHigestUnit2.Text = "/";
            txtMiddleMRP.Clear();
            lblMiddleUnit1.Text = "/";

            cmbLowestUnit.SelectedIndex = -1;
            txtLowestMesure.Clear();
            lblMiddleUnit2.Text = "/";
            txtLowestMRP.Clear();
            lblLowestUnit1.Text = "/";
            pnlUnit2.Hide();
        }
        private void btnAddUnitRow_Click(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtMiddleMRP.Text.ISNullOrWhiteSpace() : false;
            if (!cmbMiddleUnit.Text.ISNullOrWhiteSpace() &&
                !txtHighestMesure.Text.ISNullOrWhiteSpace() &&
                !IsMrpBlank)
            {
                mLstUnit.Remove(cmbMiddleUnit.Text);
                pnlUnit2.Visible = true;
                cmbLowestUnit.Items.AddRange(mLstUnit.ToArray());
                lblLowMeSuer.Text = cmbMiddleUnit.Text + " Mesure";
                pnlUnit1.Enabled = false;
                btnAddUnitRow.Disabl();
            }
            else
            {
                MessageBox.Show("Please fill up unit details.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbMiddleUnit.Select();
            }
        }

        /// <summary>
        /// Add Unit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            UnitEntry unitentry = new UnitEntry();
            unitentry.onclose += Unitentry_onclose;
            unitentry.ShowDialog();
        }
        private void Unitentry_onclose(string obj)
        {
            DefaultClass df = new DefaultClass();
            cmbHighestUnit.AddUnit();
            cmbHighestUnit.Text = obj.ToString();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            KillCalculatorRunning();
            this.Close();
        }
        private void cmbUnit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMiddleUnit2.Text = cmbMiddleUnit.Text + "/ " + cmbLowestUnit.Text;
            lblLowMeSuer.Text = "Qty / " + cmbLowestUnit.Text;
            lblLowestUnit1.Text = "/ " + cmbLowestUnit.Text;
        }
        private void btnUnitReset_Click(object sender, EventArgs e)
        {
            chkMoreUnit.Checked = false;
            chkMoreUnit.Enabled = false;
        }
        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            mIsPreviousBatch = false;
            ItemDataClear();
            //ResetAll();
            txtBatchNo.AutoCompleteCustomSource.Clear();
            int index = cmbItemName.FindStringExact(cmbItemName.Text);
            if (index >= 0)
            {
                try
                {
                    cmbItemName.SelectedIndex = index;
                    mItemId = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                    txtBatchNo.AutoCompleteCustomSource.AddRange(AddBatchNos(mItemId).ToArray());
                }
                catch (Exception) { cmbItemName.Text = ""; }
            }
            else
            {
                cmbItemName.Text = "";
            }
        }
        private void StockEntry_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess)
            {
                KillCalculatorRunning();
                OnClose();
            }
        }
        private void KillCalculatorRunning()
        {
            Process[] name = Process.GetProcessesByName("Calculator");
            if (name.Length == 1)
            {
                name[0].Kill();
            }
        }
        private void btnCalculator_Click(object sender, EventArgs e)
        {
            KillCalculatorRunning();
            Process calculator = Process.Start("calc");

        }
        private void txtTotalQty_Leave(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Enabled = false;
            }
            else
            {
                chkMoreUnit.Enabled = true;
            }
        }
        private void txtSalesRate_Leave(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
                if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
                {
                    chkMoreUnit.Enabled = false;
                }
                else
                {
                    chkMoreUnit.Enabled = true;
                }
            }
            else
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    txtHighestMRP_TextChanged(null, null);
                }
                else
                {
                    txtPurchaseRate_TextChanged(null, null);
                }
            }
        }
        private void btnitemadd_Click(object sender, EventArgs e)
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
        private void cmbHighestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHighestUnit.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Checked = false;
                //chkMoreUnit.Enabled = false;
                lblHighestUnit1.Text = "/ ";
                lblHighestUnit11.Text = "/ ";
                lblHighestUnit111.Text = "/ ";
                lblPurchaseUnit.Text = "/";
                lblPurchaseRatePerUnit.Text = "/";
                MoreUnitClear();
            }
            else
            {
                //chkMoreUnit.Enabled = true;
                lblHighestUnit1.Text = "/ " + cmbHighestUnit.Text;
                lblHighestUnit11.Text = "/ " + cmbHighestUnit.Text;
                lblHighestUnit111.Text = "/ " + cmbHighestUnit.Text;
                lblPurchaseUnit.Text = "/ " + cmbHighestUnit.Text;
                lblPurchaseRatePerUnit.Text = "/ " + cmbHighestUnit.Text;
                if (mIsPreviousBatch)
                {
                    GetRateDetails();

                }
            }
        }
        private void GetRateDetails()
        {
            string itemid = !cmbItemName.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString() : "";
            string batchno = txtBatchNo.Text.GetDBFormatString();
            string unit = cmbHighestUnit.Text.GetDBFormatString();
            string query = "select StockSummary.highestRate,MiddleRate,highestunit,lowestunit,middleunit,LowestRate," +
                "HighestMRp,MiddleMrp,lowestMrp from StockSummary where ItemID='" + itemid + "' " +
                "and batchno='" + batchno + "' and ( highestunit='" + unit + "' or middleunit='" + unit + "' or lowestunit='" + unit + "' )  ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                if (dt.Rows[0]["highestunit"].ToString() == unit)
                {
                    txtHighestMRP.Text = dt.Rows[0]["HighestMRp"].ToString();
                    txtHighestSalesRateNoTax.Text = dt.Rows[0]["highestRate"].ToString();
                }
                else if (dt.Rows[0]["middleunit"].ToString() == unit)
                {
                    txtHighestMRP.Text = dt.Rows[0]["MiddleMrp"].ToString();
                    txtHighestSalesRateNoTax.Text = dt.Rows[0]["MiddleRate"].ToString();
                }
                else
                {
                    txtHighestMRP.Text = dt.Rows[0]["lowestMrp"].ToString();
                    txtHighestSalesRateNoTax.Text = dt.Rows[0]["LowestRate"].ToString();
                }
            }
        }
        private void txtLowestMRP_TextChanged(object sender, EventArgs e)
        {
            txtLowestSalesRate.Text = txtLowestMRP.Text;
        }
        private void cmbMiddleUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHigestUnit2.Text = cmbMiddleUnit.Text + " / " + cmbHighestUnit.Text;
            lblMiddleUnit1.Text = lblMiddleUnit11.Text = " / " + cmbMiddleUnit.Text;
            lblmidunit.Text = cmbMiddleUnit.Text;
        }
        private void cmbLowestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMiddleUnit2.Text = cmbLowestUnit.Text + " / " + cmbMiddleUnit.Text;
            lblLowestUnit1.Text = lblLowestUnit11.Text = " / " + cmbLowestUnit.Text;
            lblLowunit.Text = cmbLowestUnit.Text;
        }
        private void txtMiddleMRP_TextChanged(object sender, EventArgs e)
        {
            txtMiddleSalesRate.Text = txtMiddleMRP.Text;
        }
        private void txtHighestMesure_TextChanged(object sender, EventArgs e)
        {
            string totalmidstock = "0";
            txtMiddleMRP.Text = CalculateRate(txtHighestMRP.Text, txtHighestMesure.Text, txtHighestTotalStock.Text, out totalmidstock);
            txtMiddleSalesRate.Text = CalculateRate(txtHighestSalesRateNoTax.Text, txtHighestMesure.Text, txtHighestTotalStock.Text, out totalmidstock);

            lblMiddleTotalStock.Text = totalmidstock;
        }
        private void txtLowestMesure_TextChanged(object sender, EventArgs e)
        {
            string totalloweststock = "0";
            txtLowestMRP.Text = CalculateRate(txtMiddleMRP.Text, txtLowestMesure.Text, lblMiddleTotalStock.Text, out totalloweststock);
            txtLowestSalesRate.Text = CalculateRate(txtMiddleSalesRate.Text, txtLowestMesure.Text, lblMiddleTotalStock.Text, out totalloweststock);
            lblLowestTotalStock.Text = totalloweststock;
        }
        private void txtBatchNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtHighestTotalStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestTotalStock.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtHighestMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestSalesRateWithTax.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtMiddleSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtMiddleSalesRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtMiddleMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtMiddleMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtHighestMesure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestMesure.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtLowestMesure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestMesure.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtLowestMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLowestSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestSalesRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private string CalculateSalesRate()
        {
            double purchaseOrMrpAmount = 0d, salesRate = 0d, rateAmount = 0d, rate = 0d;
            if (OtherSettingTools._IsPurchasePercent)
            {
                double.TryParse(txtPurchaseRate.Text, out purchaseOrMrpAmount);
            }
            else ///Sales Rate==MRP
            {
                double.TryParse(txtHighestMRP.Text, out purchaseOrMrpAmount);
            }
            double.TryParse(txtPurchaseRateTosALES.Text, out rate);
            rateAmount = (rate / 100) * purchaseOrMrpAmount;
            salesRate = rateAmount + purchaseOrMrpAmount;
            return salesRate.ToString("0.00");
        }
        private string CalculateRate(string amount)
        {
            double purchaseOrMrpAmount = 0d, salesamount = 0d, rateAmount = 0d, rate = 0d;
            if (OtherSettingTools._IsPurchasePercent)
            {
                double.TryParse(txtPurchaseRate.Text, out purchaseOrMrpAmount);
            }
            else
            {
                double.TryParse(txtHighestMRP.Text, out purchaseOrMrpAmount);
            }
            double.TryParse(amount, out salesamount);
            rateAmount = salesamount - purchaseOrMrpAmount;
            rate = (100 / purchaseOrMrpAmount) * rateAmount;
            return rate.ToString("0.00");
        }
        private void txtPurchaseRateTosALES_Leave(object sender, EventArgs e)
        {
            if (!txtPurchaseRate.Text.ISNullOrWhiteSpace())
            {
                //txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(CalculateSalesRate()).toRound();
                txtHighestSalesRateNoTax.Text = CalculateSalesRate();
                //txtHighestSalesRateWithTax.Text = CalculateSalesRate();
            }
            else
            {
                CalculateSalesRateNoTaxFromMrp(txtHighestMRP.Text);
            }
        }
        private void dgvBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txtHighestMRP_TextChanged(object sender, EventArgs e)
        {
            if (OtherSettingTools._IsMrpPercent)
            {
                if (!txtHighestMRP.Text.ISNullOrWhiteSpace())
                {
                    if (ORG_Tools._IsRegularGST)
                    {
                        string amount = txtHighestMRP.Text;
                        string highestSalesRateNoTax= CalculateSalesRateNoTaxFromMrp(amount);
                        txtHighestSalesRateNoTax.Text = highestSalesRateNoTax.toRound();
                        txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(highestSalesRateNoTax);
                    }
                    else
                    {
                        txtHighestSalesRateNoTax.Text = txtHighestMRP.Text;
                    }

                }
                else
                {
                    txtHighestSalesRateNoTax.Text = txtHighestSalesRateWithTax.Text = "";
                }
            }
        }
        private string CalculateSalesRateNoTaxFromMrp(string amount)
        {
            double mrp = 0d, slalesRate = 0d, totalgst = 0d, igstd = 0d, cessd = 0d;

            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            object sgst = "", cgst, igst = "", cess = "";
            ItemTools.GetItemGSTRate(itemid, out cgst, out sgst, out igst, out cess);

            double.TryParse(amount, out mrp);
            double.TryParse(igst.ISValidObject() ? igst.ToString() : "0", out igstd);
            double.TryParse(cess.ISValidObject() ? cess.ToString() : "0", out cessd);

            totalgst = igstd + cessd;
            slalesRate = (mrp * 100) / (100 + totalgst);
            //txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(slalesRate.ToString());
            return slalesRate.ToString();
        }
        private void txtHighestSalesRateNoTax_TextChanged(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    string amount = txtHighestSalesRateNoTax.Text;
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(amount).toRound();

                }
                else
                {
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(txtHighestSalesRateNoTax.Text);
                    txtPurchaseRateTosALES.Text = CalculateRate(txtHighestSalesRateNoTax.Text);
                }
            }
            

        }
        private string CalculateSalesRateWithTaxFromMrp(string amount)
        {
            double amountd = 0d, slalesRate = 0d, totalgst = 0d, igstd = 0d, cessd = 0d;

            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            object sgst = "", cgst, igst = "", cess = "";
            ItemTools.GetItemGSTRate(itemid, out cgst, out sgst, out igst, out cess);

            double.TryParse(amount, out amountd);
            double.TryParse(igst.ISValidObject() ? igst.ToString() : "0", out igstd);
            double.TryParse(cess.ISValidObject() ? cess.ToString() : "0", out cessd);

            totalgst = igstd + cessd;
            slalesRate = amountd + (amountd * (totalgst / 100));
            return slalesRate.toString();
        }
        private void txtHighestSalesRateWithTax_Leave(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
                if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
                {
                    chkMoreUnit.Enabled = false;
                }
                else
                {
                    chkMoreUnit.Enabled = true;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    string amount = txtHighestSalesRateWithTax.Text;
                    string highestSalesRateNoTax = CalculateSalesRateNoTaxFromMrp(amount);
                    txtHighestSalesRateNoTax.Text = highestSalesRateNoTax.toRound();
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(highestSalesRateNoTax);

                    //txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(txtHighestSalesRateWithTax.Text).toRound();
                }
                else
                {
                    txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(txtHighestSalesRateWithTax.Text).toRound();
                    txtPurchaseRateTosALES.Text = CalculateRate(txtHighestSalesRateNoTax.Text);
                    txtPurchaseRateTosALES_Leave(null, null);
                }
            }
            else
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    txtHighestMRP_TextChanged(null, null);
                }
                else
                {
                    txtHighestSalesRateNoTax_TextChanged(null, null);
                }
            }
        }
        private void grb_Enter(object sender, EventArgs e)
        {

        }
        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            if (ORG_Tools._IsRegularGST)
            {
                if (!txtPurchaseRate.Text.ISNullOrWhiteSpace())
                {
                    if (OtherSettingTools._IsPurchasePercent)
                    {
                        txtPurchaseRateTosALES_Leave(null,null);
                    }
                }
                else
                {
                    txtHighestSalesRateNoTax.Text = "";
                    txtHighestSalesRateWithTax.Text = "";
                    txtPurchaseRateTosALES.Text = "";

                }
            }
        }
        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPurchaseRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPurchaseRateTosALES_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPurchaseRateTosALES.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestSalesRateNoTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestSalesRateNoTax.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestMRP_Leave(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Enabled = false;
            }
            else
            {
                chkMoreUnit.Enabled = true;
            }
        }

        private string CalculateRate(string highrateperqtystr, string lowestMesureqtystr, string totalhigherstockstr, out string lowerTotalStockstr)
        {
            double rate = 0d, highrateperqty = 0d, lowestMesureqty = 0d, totalhigherstock = 0d, lowerTotalStock = 0d;
            double.TryParse(highrateperqtystr, out highrateperqty);
            double.TryParse(lowestMesureqtystr, out lowestMesureqty);
            double.TryParse(totalhigherstockstr, out totalhigherstock);

            rate = lowestMesureqty == 0 ? 0 : (highrateperqty / lowestMesureqty);

            lowerTotalStock = totalhigherstock * lowestMesureqty;
            lowerTotalStockstr = lowerTotalStock.ToString();
            string ratestr = rate == 0 ? "" : rate.ToString();
            return ratestr;
        }
    }
}

