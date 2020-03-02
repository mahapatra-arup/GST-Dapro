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
    public partial class ChallanEntry : Form
    {
        int mOrderQuantity = 0;
        private double mOrderRate = 0d;

        private string msg = "";
        public event Action OnClose;
        private string mChallanidForEdit = "";
        private List<string> mLstQuery = new List<string>();
        private double mtotalchallanAmount = 0f;
        private int mDescriptionSlno = 1;
        string mOrderID = "";
        string morderIdfromchallan = "";
        string mQuery = "";
        long mSerialNo = OtherSettingTools._ChallanSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._ChallanSerialStart);
        private bool mIsIGST = false;// Supply State
        DataTable mdt = new DataTable();//for restor

        private double mTotalOrderAmount, mTotalAmount, mTotalDiscount, mTotalCGST,
                     mTotalSGST, mTotalIGST, mTotalCESS, mTaxableAmount, mTotalWithTax;
        private long mTotalQuantity;

        /// <summary>
        /// 
        /// </summary>
        public ChallanEntry()
        {
            InitializeComponent();
            this.FitToVertical();
            cmbCustomerName.SelectedIndexChanged -= new EventHandler(cmbCustomerName_SelectedIndexChanged);
            cmbCustomerName.AddCustomers();
            cmbCustomerName.SelectedIndexChanged += new EventHandler(cmbCustomerName_SelectedIndexChanged);

            GenerateSlNo();
        }
        public ChallanEntry(string chalanid)
        {
            InitializeComponent();
            this.FitToVertical();
            GridDesign();
            cmbCustomerName.AddCustomers();
            mChallanidForEdit = chalanid;
            DataTableColumnCreate();
            ViewExistingDataFromChallan();
        }
        public ChallanEntry(string orderid, string orderno)
        {
            InitializeComponent();
            this.FitOnDown();
            GridDesign();
            cmbCustomerName.AddCustomers();

            GenerateSlNo();
            lblOrderNo.Text = orderno;
            mOrderID = orderid;
            cmbCustomerName.Enabled = false;
            GetDataFromOrder();
        }

        private void ViewExistingDataFromChallan()
        {
            string query = "Select Challan.*,LadgerMain.LadgerName,LadgerMain.TemplateName from Challan inner join LadgerMain on Challan.LedgerID=LadgerMain.LadgerID " +
                           "where ChallanID='" + mChallanidForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                morderIdfromchallan = dt.Rows[0]["OrderId"].ToString();
                lblSlNo.Text = dt.Rows[0]["ChallanNo"].ToString();
                cmbCustomerName.Text = dt.Rows[0]["TemplateName"].ToString();
                string ledgerID = dt.Rows[0]["LedgerID"].ToString();
                GetCustomerAddressDetails(ledgerID);
                lblOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
                DateTime orderdate;
                 DateTime.TryParse((dt.Rows[0]["OrderDate"].ToString()).ToString(),out orderdate);
                lblOrderDate.Text = orderdate.ToString("dd-MMM-yyyy");
                dtpChallanDate.Text = dt.Rows[0]["ChallanDate"].ToString();
                cmbDespatchMode.Text = dt.Rows[0]["DespatchMode"].ToString();
                txtVehicleNo.Text = dt.Rows[0]["VehicleNo"].ToString();
                txtfreightcharges.Text = dt.Rows[0]["FreightCharges"].ToString();
                txtPackingCharges.Text = dt.Rows[0]["PackingCharges"].ToString();
                txtOthersCharges.Text = dt.Rows[0]["OtherCharges"].ToString();
                string amountStr = dt.Rows[0]["TotalAmount"].ToString();
                mTotalOrderAmount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);
                lblTotalChallanAmount.Text = mTotalOrderAmount.ToString("0.00");
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                lblShippedTo.Text = dt.Rows[0]["ShippingTo"].ToString();
                lblShippedAddress.Text = dt.Rows[0]["ShippingAddress"].ToString();
                lblStateNameShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                lblStateCodeShipping.Text = dt.Rows[0]["ShippingStateCode"].ToString();
                ViewExistingDataFromChallanDetails();
                GenerateTotal();
            }
        }
        private void ViewExistingDataFromChallanDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select ChallanDetails.* from ChallanDetails " +
                           "inner join Challan on ChallanDetails.ChallanID=Challan.ChallanID " +
                           "where Challan.ChallanID='" + mChallanidForEdit + "'";
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

                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, comodityCode, qtyStr, unit,
                                         rate.ToString("0.00"), amount.ToString("0.00"), disRate, disAmount, taxAmount
                                         , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                                         , cessrate, cessAmount, totalAmount);

                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                    mdt.Rows.Add(morderIdfromchallan, itemID, qtyStr);
                    dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
                }
            }
        }
        private void DataTableColumnCreate()
        {
            mdt.Columns.Add("orderid", typeof(string));
            mdt.Columns.Add("itemid", typeof(string));
            mdt.Columns.Add("SupplyQty", typeof(string));
            //mdt.Columns.Add("DueQty", typeof(string));
        }

        /// <summary>
        /// Grid Design and Enabled
        /// </summary>
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

                    ary[0] = 8;
                    ary[1] = 15;
                    ary[2] = 17;
                }
                else
                {
                    length = 4;
                    array[0] = "DISCOUNT";
                    array[1] = "CGST";
                    array[2] = "SGST";
                    array[3] = "CESS";

                    ary[0] = 8;
                    ary[1] = 11;
                    ary[2] = 13;
                    ary[3] = 17;

                }

            }
            else
            {
                length = 1;
                array[0] = "DISCOUNT";
                ary[0] = 8;

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
        /// <summary>
        /// END
        /// </summary>

        ///Get Order details
        private void GetDataFromOrder()
        {
            string query = "Select SalesOrder.*,LadgerMain.LadgerName,LadgerMain.TemplateName from SalesOrder inner join LadgerMain on SalesOrder.LedgerID=LadgerMain.LadgerID " +
                           "where OrderId='" + mOrderID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
               // lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                cmbCustomerName.Text = dt.Rows[0]["TemplateName"].ToString();
                string ledgerID = dt.Rows[0]["LedgerID"].ToString();
                GetCustomerAddressDetails(ledgerID);
                lblOrderNo.Text = dt.Rows[0]["CustomerOrderNo"].ToString();
                lblOrderDate.Text = DateTime.Parse(dt.Rows[0]["OrderDate"].ToString()).ToString("dd-MMM-yyyy");
                string amountStr = dt.Rows[0]["TotalAmount"].ToString();
                mtotalchallanAmount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);
                //lblTotalAmount.Text = mtotalchallanAmount.ToString("0.00");
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                lblShippedTo.Text = dt.Rows[0]["ShippingTo"].ToString();
                lblShippedAddress.Text = dt.Rows[0]["ShippingAddress"].ToString();
                lblStateNameShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                lblStateCodeShipping.Text = dt.Rows[0]["ShippingStateCode"].ToString();
                GetOrderDetails();
                GenerateTotal();
            }
        }
        private void GetOrderDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select SalesOrderDetails.* from SalesOrderDetails " +
                           "inner join SalesOrder on SalesOrderDetails.OrderId=SalesOrder.OrderId " +
                           "where SalesOrder.OrderId='" + mOrderID + "'";
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
                    //if (int.Parse(qtyStr)>0)
                    //{
                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, comodityCode, qtyStr, unit,
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
                    //}

                }
            }
        }

        /// <summary>
        /// Customer address Details
        /// </summary>
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAddressData();
            if (!cmbCustomerName.Text.ISNullOrWhiteSpace())
            {
                string customerID = ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString();
                GetCustomerAddressDetails(customerID);
                GetCustomerShippedDetails(customerID);
            }
            else
            {
                cmbCustomerName.Text = "";
            }
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
        //
        private void GenerateSlNo()
        {
            //int slno = 1;
            string query = "Select max(SlNo) as slno from Challan ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                try
                {
                    mSerialNo = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblSlNo.Text = OtherSettingTools._ChallanStart + mSerialNo.ToString();
        }
        private void RestorsalesOrderDetails()
        {
            foreach (DataRow item in mdt.Rows)
            {
                string orderid = item["orderid"].ToString();
                string itemid = item["itemid"].ToString();
                string duequantitystr = item["SupplyQty"].ToString();
                int duquantity = int.Parse(duequantitystr);
                string query = "update SalesOrderDetails set DueQty=(select (DueQty+" + duquantity + ") as duerate from SalesOrderDetails where OrderId='" + orderid + "' and ItemID='" + itemid + "') where OrderId='" + orderid + "' and ItemID='" + itemid + "'";
                SQLHelper.GetInstance().ExcuteQuery(query, out msg);
            }
        }
        private bool IsValidSelection()
        {
            if (cmbCustomerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a customer.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Select();
                return false;
            }
            if (cmbDespatchMode.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a despatch mode.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbDespatchMode.Select();
                return false;
            }
            if (lblShippedTo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping name not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Focus();
                return false;
            }
            if (lblShippedAddress.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping address not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Focus();
                return false;
            }
            if (lblStateNameBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Shipping state not found.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCustomerName.Select();
                return false;
            }
            return true;
        }

        //private int GetitemCurrentQuantity(string itemid)
        //{
        //    string query = "Select CurrentQuantity from item where ID='" + itemid + "'";
        //    object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
        //    if (obj .ISValidObject())
        //    {
        //        return int.Parse(obj.ToString());
        //    }
        //    return 0;
        //}
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
            lblTotAmount.Text = mTotalAmount.ToString("0.00");
            lblTaxableAmountTotal.Text = mTaxableAmount.ToString("0.00");

            lblTotalCGST.Text = mTotalCGST.ToString("0.00");
            lblTotalIGST.Text = mTotalIGST.ToString("0.00");
            lblTotalSGST.Text = mTotalSGST.ToString("0.00");
            lblTotalCESS.Text = mTotalCESS.ToString("0.00");
            lblTotalWithTax.Text = mTotalWithTax.ToString("0.00");
            mtotalchallanAmount = mTotalWithTax;
            //lblTotalAmount.Text = mTotalOrderAmount.ToString("0.00");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                ChallanSave();
            }
        }
        private void StockManaged()
        {
            //string query = "se";
        }
        private void ChallanSave()
        {
            #region Data
            mLstQuery.Clear();
            string challanid = Guid.NewGuid().ToString();
            string slNo = mSerialNo.ToString();
            string challanno = lblSlNo.Text.GetDBFormatString();
            string challanDate = dtpChallanDate.Text;
            string ledgerID = !cmbCustomerName.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbCustomerName.SelectedItem).Key.ToString() : "NULL";
            string despatchmode = cmbDespatchMode.Text.GetDBFormatString();
            string totalChalanAmount = mtotalchallanAmount.ToString();
            string description = "NULL";
            string freightcharge = txtfreightcharges.Text.ISNullOrWhiteSpace()?"NULL":""+ txtfreightcharges.Text.GetDBFormatString()+"";
            string packingcharge = txtPackingCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtPackingCharges.Text.GetDBFormatString() + "";
            string otherscharges = txtOthersCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtOthersCharges.Text.GetDBFormatString() + "";

            /// Shipping Details
            /// 
            string shippingName = lblShippedTo.Text.GetDBFormatString();
            string shippingAddress = lblShippedAddress.Text.GetDBFormatString();
            string shippingState = lblStateNameShipping.Text;
            string shippingstateCode = lblStateCodeShipping.Text;
            string orderno = "NULL";
            string orderid = "NULL";
            string vechilno = txtVehicleNo.Text.GetDBFormatString();
            string orderdate = lblOrderDate.Text;
            if (!mOrderID.ISNullOrWhiteSpace())
            {
                orderid = "'" + mOrderID + "'";
            }
            if (!lblOrderNo.Text.ISNullOrWhiteSpace())
            {
                orderno = "'" + lblOrderNo.Text.GetDBFormatString() + "'";
            }

            if (!txtDescription.Text.ISNullOrWhiteSpace())
            {
                description = "'" + txtDescription.Text.GetDBFormatString() + "'";
            }
            #endregion

            #region Query
            if (mChallanidForEdit.ISNullOrWhiteSpace())
            {
                mQuery = "Insert into Challan(SlNo, ChallanID, ChallanNo, OrderNo, OrderId, OrderDate, " +
                        "ChallanDate,  LedgerID, DespatchMode,FreightCharges,PackingCharges,OtherCharges" +
                        ",TotalAmount,Description,ShippingTo,ShippingAddress,ShippingState,ShippingStateCode,VehicleNo,Status) " +
                        "Values(" + slNo + ",'" + challanid + "','" + challanno + "'," + orderno + "," + orderid + ",'" + orderdate + "','" + challanDate + "','" + ledgerID + "','" + despatchmode + "'," +
                        "" + freightcharge + "," + packingcharge + "," + otherscharges + "," + totalChalanAmount + "," +
                        "" + description + ",'" + shippingName + "','" + shippingAddress + "','" + shippingState + "','" + shippingstateCode + "','" + vechilno + "','Open')";
                mLstQuery.Add(mQuery);
            }
            else
            {
                challanid = mChallanidForEdit;
                mQuery = "Update Challan set LedgerID='" + ledgerID + "',ChallanDate='" + challanDate + "',FreightCharges='" + freightcharge + "',PackingCharges='" + packingcharge + "',OtherCharges='" + otherscharges + "',DespatchMode='" +
                         despatchmode + "',TotalAmount='" + totalChalanAmount + "',Description=" + description
                         + ",ShippingTo='" + shippingName + "',ShippingAddress='" + shippingAddress
                         + "',ShippingState='" + shippingState + "',ShippingStateCode='" + shippingstateCode
                         + "',VehicleNo='" + vechilno + "' where ChallanID='" + mChallanidForEdit + "'";
                mLstQuery.Add(mQuery);
                RestorsalesOrderDetails();
                mQuery = "Delete from ChallanDetails where ChallanID='" + mChallanidForEdit + "'";
                mLstQuery.Add(mQuery);
            }
            ChallanDetailsAndSalesorderDetailsDatasave(challanid);

            #endregion

            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
            {
                UpdateSalesOrderStatus();
                MessageBox.Show("Challan Rs. " + mtotalchallanAmount + " saved.", "Challan Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (mChallanidForEdit.ISNullOrWhiteSpace() && mOrderID.ISNullOrWhiteSpace())
                {
                    cmbCustomerName.SelectedIndex = -1;
                    cmbCustomerName.Select();
                    GenerateSlNo();
                }
                else
                {
                    this.Close();
                }
                OtherSettingTools._IsChallanBillgenarate=true;
            }
            else
            {
                MessageBox.Show("Internal Problem \n" + msg, "save");
            }
            #endregion
        }
        private void UpdateSalesOrderStatus()
        {
            string id = morderIdfromchallan;
            if (!mOrderID.ISNullOrWhiteSpace())
            {
                id = mOrderID;
            }
            if (IsSalesOrderStatusClose())
            {
                mQuery = "Update SalesOrder set Status='Close' where  OrderId='" + id + "'";
            }
            else
            {
                mQuery = "Update SalesOrder set Status='Open' where  OrderId='" + id + "'";
            }
            SQLHelper.GetInstance().ExcuteQuery(mQuery, out msg);

        }
        private void ChallanDetailsAndSalesorderDetailsDatasave(string challanid)
        {
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
                string disRate = !disRateStr.ISValidObject() ? "NULL" : disRateStr.ToString();
                object disAmountStr = row.Cells["DISCOUNTAMOUNT"].Value;
                string disAmount = !disAmountStr.ISValidObject() ? "NULL" : disAmountStr.ToString();

                object taxAmountStr = row.Cells["TAXABLEVALUE"].Value;
                double taxAmount = !taxAmountStr.ISValidObject() ? 0d : double.Parse(taxAmountStr.ToString());

                object cgstRateStr = row.Cells["CGSTRATE"].Value;
                string cgstRate = !cgstRateStr.ISValidObject() ? "NULL" : cgstRateStr.ToString();
                object cgstAmountStr = row.Cells["CGSTAMOUNT"].Value;
                string cgstAmount = !cgstAmountStr.ISValidObject() ? "NULL" : cgstAmountStr.ToString();

                object sgstRateStr = row.Cells["SGSTRATE"].Value;
                string sgstRate = !sgstRateStr.ISValidObject() ? "NULL" : sgstRateStr.ToString();
                object sgstAmountStr = row.Cells["SGSTAMOUNT"].Value;
                string sgstAmount = !sgstAmountStr.ISValidObject() ? "NULL" : sgstAmountStr.ToString();

                object igstRateStr = row.Cells["IGSTRATE"].Value;
                string igstRate = !igstRateStr.ISValidObject() ? "NULL" : igstRateStr.ToString();
                object igstAmountStr = row.Cells["IGSTAMOUNT"].Value;
                string igstAmount = !igstAmountStr.ISValidObject() ? "NULL" : igstAmountStr.ToString();

                object cessRateStr = row.Cells["CESSRATE"].Value;
                string cessRate = !cessRateStr.ISValidObject() ? "NULL" : cessRateStr.ToString();
                object cessAmountStr = row.Cells["CESSAMOUNT"].Value;
                string cessAmount = !cessAmountStr.ISValidObject() ? "NULL" : cessAmountStr.ToString();

                object totalStr = row.Cells["TotalWithTax"].Value;
                double total = !totalStr.ISValidObject() ? 0f : double.Parse(totalStr.ToString());

                string dueqtystr = GetSalesOrderDueQuantity(itemId);
                int dueqty = 0, supplyqty = 0;
                int.TryParse(quantity, out supplyqty);
                int.TryParse(dueqtystr, out dueqty);
                dueqty = dueqty - supplyqty;

                #endregion
                #region INSERT
                if (ORG_Tools._IsRegularGST)
                {
                    if (mIsIGST)
                    {
                        mQuery = "Insert into ChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, " +
                                "IGSTRate, IGSTAmount, CessRate,CeassAmount,Total)" +
                                "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," + igstRate + "," +
                                igstAmount + "," + cessRate + "," + cessAmount + "," + total + ")";
                    }
                    else
                    {
                        mQuery = "Insert into ChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, " +
                                "CessRate,CeassAmount, Total)" +
                                "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," +
                                cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," + cessRate + "," +
                                cessAmount + "," + total + ")";
                    }
                }
                else
                {
                    mQuery = "Insert into ChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                            "Amount, DiscountRate, DiscountAmount, TaxAmount, Total)" +
                            "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                            + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," +
                            taxAmount + "," + total + ")";
                }
                mLstQuery.Add(mQuery);
                if (!mOrderID.ISNullOrWhiteSpace() || !mChallanidForEdit.ISNullOrWhiteSpace())
                {
                    string id = morderIdfromchallan;
                    if (!mOrderID.ISNullOrWhiteSpace())
                    {
                        id = mOrderID;
                    }
                    mQuery = "update SalesOrderDetails set SupplyQty=" + quantity + ",DueQty=" + dueqty + " where OrderId='" + id + "' and ItemID='" + itemId + "'";
                    mLstQuery.Add(mQuery);
                }
                #endregion
            }
            #endregion

            #region uNUSED
            //    foreach (DataGridViewRow row in dgvItemList.Rows)
            //{

            //    string itemID = row.Cells["ItemID"].Value.ToString();
            //    string comoditycode = row.Cells["HSN"].Value.ToString();
            //    string itemName = row.Cells["ItemName"].Value.ToString();
            //    string supplyquantityqtystr = row.Cells["Quantity"].Value.ToString();
            //    string duequantityqtystr = GetSalesOrderDueQuantity(itemID);
            //    string unit = row.Cells["Unit"].Value.ToString();
            //    string rate = row.Cells["rate"].Value.ToString();
            //    string amount = row.Cells["Amount"].Value.ToString();

            //    int supplyquantityqty = int.Parse(supplyquantityqtystr);
            //    int duequantity = int.Parse(duequantityqtystr);
            //    mQuery = "Insert into ChallanDettails(ChallanID, ItemId, Qty, Unit, Rate, Amount,ItemName,ComodityCode,OrderQuantity)" +
            //           "Values('" + challanid + "'," + itemID + "," + supplyquantityqty +
            //           ",'" + unit + "'," + rate + "," + amount + ",'" + itemName + "','" + comoditycode + "'," + duequantity + ")";
            //    mLstQuery.Add(mQuery);

            //    if (!mOrderID.ISNullOrWhiteSpace() || !mChallanidForEdit.ISNullOrWhiteSpace())
            //    {
            //        duequantity = duequantity - supplyquantityqty;
            //        mQuery = "update SalesOrderDetails set SupplyQty=" + supplyquantityqty + ",DueQty=" + duequantity + " where OrderId='" + mOrderID + "' and ItemID='" + itemID + "'";
            //        mLstQuery.Add(mQuery);
            //    }
            //}
            #endregion
        }
        private string GetSalesOrderDueQuantity(string itemID)
        {
            string id = morderIdfromchallan;
            if (!mOrderID.ISNullOrWhiteSpace())
            {
                id = mOrderID;
            }
            string query = "Select DueQty from SalesOrderDetails where OrderId='" + id + "' and Itemid='" + itemID + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        private bool IsSalesOrderStatusClose()
        {
            string id = morderIdfromchallan;
            if (!mOrderID.ISNullOrWhiteSpace())
            {
                id = mOrderID;
            }
            string query = "Select DueQty from SalesOrderDetails where OrderId='" + id + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string dueqtystr = item["DueQty"].ToString();
                    int dueqty = int.Parse(dueqtystr);
                    if (dueqty != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void BTNcANCLE_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY" ||
                dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "RATE" ||
                dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "DISCOUNTAMOUNT" ||
                dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "DISCOUNTRATE")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY")
                    {
                        tb.KeyPress += new KeyPressEventHandler(ItemQuantity_KeyPress);
                    }
                    else
                    {
                        tb.KeyPress += new KeyPressEventHandler(ItemQuantity_KeyPress);
                    }
                }
            }
        }
        void ItemRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b' && e.KeyChar != '.') //allow the backspace and dot key
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        void ItemQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace and dot key
                {
                    e.Handled = true;
                }
            }
            else
            {

            }
        }
        private void SumOfCharges()
        {
            double totalamount = 0d, freightamount = 0d, packingamount = 0d, othersamount = 0d, challanamount = 0d;
            double.TryParse(lblTotalWithTax.Text, out totalamount);
            double.TryParse(txtfreightcharges.Text, out freightamount);
            double.TryParse(txtOthersCharges.Text, out othersamount);
            double.TryParse(txtPackingCharges.Text, out packingamount);
            challanamount = totalamount + freightamount + packingamount + othersamount;
            mtotalchallanAmount = challanamount;
            lblTotalChallanAmount.Text = challanamount.ToString("0.00");

        }
        private void txtfreightcharges_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void txtPackingCharges_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void txtOthersCharges_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void lblTotalAmount_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void lblTotalWithTax_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
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

        private void ChallanEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void Tb_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            if (str != null && !str.ISNullOrWhiteSpace())
            {
                string itemId = dgvItemList.CurrentRow.Cells["ItemId"].Value.ToString();
                // int availQty = GetitemCurrentQuantity(itemId);
                int currentQty = str.ISNullOrWhiteSpace() ? 0 : int.Parse(str);
                string orderQtyStr = dgvItemList.CurrentRow.Cells["OrderedQuantity"].Value.ToString();
                int orderQty = orderQtyStr.ISNullOrWhiteSpace() ? 0 : int.Parse(orderQtyStr);
                string ratestr = dgvItemList.CurrentRow.Cells["Rate"].Value.ToString();
                //string quantitystr = dgvItemList.CurrentRow.Cells["Quantity"].Value.ToString();
                if (orderQty >= currentQty)
                {
                    double rate = ratestr.ISNullOrWhiteSpace() ? 0d : double.Parse(ratestr);


                    double total = rate * currentQty;
                    dgvItemList.CurrentRow.Cells["Amount"].Value = total.ToString("0.00");
                }
            }
            //GenarateTotalForGridEdit();
        }
        private bool IsExsistStock(string itemid, int quantity)
        {
            //int currentquantity = GetitemCurrentQuantity(itemid);
            //if (currentquantity < quantity)
            //{
            //    return false;
            //}

            return true;
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
                    rate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString()) : 0d;
                    disAmount = disAmountObj.ISValidObject() ? double.Parse(disAmountObj.ToString()) : 0d;
                }
                catch (Exception) { }
                totalDiscount = qty * disAmount;
                amount = qty * rate;
                taxAmount = (qty * rate) - totalDiscount;
                GenerateTaxAmounts(rowIndex, amount, taxAmount);
                GenerateTotal();
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

            dgvItemList.Rows[rowindex].Cells["TotalAmount"].Value = amount.ToString("0.00");
            dgvItemList.Rows[rowindex].Cells["TAXABLEVALUE"].Value = taxAmount.ToString("0.00");
            dgvItemList.Rows[rowindex].Cells["CgstAmount"].Value = cgstAmount;
            dgvItemList.Rows[rowindex].Cells["SgstAmount"].Value = sgstAmount;
            dgvItemList.Rows[rowindex].Cells["IgstAmount"].Value = igstAmount;
            dgvItemList.Rows[rowindex].Cells["TotalWithTax"].Value = totalWithTax;
        }

        /// <summary>
        /// DGV Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvItemList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "QTY")
            {
                object quantityObj = dgvItemList.Rows[e.RowIndex].Cells["Qty"].Value;
                mOrderQuantity = quantityObj.ISValidObject() ? int.Parse(quantityObj.ToString()) : 0;
            }
            if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "RATE")
            {
                object rateObj = dgvItemList.Rows[e.RowIndex].Cells["RATE"].Value;
                mOrderRate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString()) : 0d;
            }
        }
        private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "QTY")  // IF quantity Chnage
            {
                object quantityObj = dgvItemList.Rows[e.RowIndex].Cells["Qty"].Value;
                if (!quantityObj.ISValidObject())
                {
                    dgvItemList.Rows[e.RowIndex].Cells["Qty"].Value = mOrderQuantity;
                }
                int rowIndex = dgvItemList.CurrentCell.RowIndex;
                RegenerateItemDiscountAndTax(rowIndex);
            }
            else if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "RATE")  // IF rate Chnage
            {
                object rateObj = dgvItemList.Rows[e.RowIndex].Cells["RATE"].Value;
                if (!rateObj.ISValidObject())
                {
                    dgvItemList.Rows[e.RowIndex].Cells["Rate"].Value = mOrderRate;
                }
                int rowIndex = dgvItemList.CurrentCell.RowIndex;
                RegenerateItemDiscountAndTax(rowIndex);
            }
            else if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "DISCOUNTRATE")  // IF discount rate Chnage
            {
                object disRateObj = dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTRATE"].Value;
                object rateObj = dgvItemList.Rows[e.RowIndex].Cells["RATE"].Value;
                double rate = 0d, disRate = 0d, disAmount = 0d;
                if (disRateObj.ISValidObject())
                {
                    rate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString()) : 0d;
                    disRate = disRateObj.ISValidObject() ? double.Parse(disRateObj.ToString()) : 0d;
                    disAmount = rate * (disRate / 100);
                }
                dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTAMOUNT"].Value = disAmount.ToString("0.00");
                int rowIndex = dgvItemList.CurrentCell.RowIndex;
                RegenerateItemDiscountAndTax(rowIndex);
            }
            else if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "DISCOUNTAMOUNT")  // IF discount amount Chnage
            {
                object disAmountObj = dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTAMOUNT"].Value;
                object rateObj = dgvItemList.Rows[e.RowIndex].Cells["RATE"].Value.ToString();
                double rate = 0d, disRate = 0d, disAmount = 0d;
                if (disAmountObj.ISValidObject())
                {
                    rate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString()) : 0d;
                    disAmount = disAmountObj.ISValidObject() ? double.Parse(disAmountObj.ToString()) : 0d;
                    disRate = (disAmount / rate) * 100;
                }
                dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTRATE"].Value = disRate.ToString("0.00");
                int rowIndex = dgvItemList.CurrentCell.RowIndex;
                RegenerateItemDiscountAndTax(rowIndex);
            }
        }
        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemList.SelectedCells.Count > 0 && e.RowIndex != -1)
            {
                if (dgvItemList.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    GenerateTotal();
                }
            }

        }
    }
}
