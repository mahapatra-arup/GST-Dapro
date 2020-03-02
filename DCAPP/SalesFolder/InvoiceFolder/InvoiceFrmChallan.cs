using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class InvoiceFrmChallan : Form
    {
        public enum _Invoicefrom
        {
            Order,
            Challan
        }
        private _Invoicefrom mInvoiceFrom;
        private string msg = "";
        public event Action OnClose;
        private long mInvoiceSlNo = 1;
        private bool mIsIGST = false;
        private string mInvoiceType = "";

        private string mChallanID = "";  /// <summary>        /// OrderID/ ChallanID        /// </summary>
        private string mCustomerLadgerID = "";
        List<string> mlistQuery = new List<string>();
        private int mDescriptionSlno = 1;
        private double mTotalInvoiceAmount, mTotalAmount, mTotalDiscount, mTotalCGST,
                     mTotalSGST, mTotalIGST, mTotalCESS, mTaxableAmount, mTotalWithTax;
        private long mTotalQuantity;
        public InvoiceFrmChallan(_Invoicefrom inveFrom, string chalanID)
        {
            InitializeComponent();
            this.FitToVertical();
            mInvoiceFrom = inveFrom;
            GridDesign();
            mChallanID = chalanID;
        }

        /// <summary>
        /// grid design and Enabled controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceFrmChallan_Shown(object sender, EventArgs e)
        {
            GenerateInvoiceNo();
            SetInvoiceType();
            cmbBillingTerms.AddBillingTerms();

            ShowORGDetails();

            GetDataFromOrder();
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
        ///
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
        private void SetInvoiceType()
        {
            if (ORG_Tools._GSTtype == "Regular")
            {
                mInvoiceType = "Regular";
            }
            else
            {
                mInvoiceType = "BillOfSupply";
            }
        }
        private void ShowORGDetails()
        {
            string orgName = "", orgAddress = "", orgLegalNos = "", orgStateCode = "", orgState = "";
            ORG_Tools.GetORGDetails(out orgName, out orgAddress, out orgLegalNos, out orgStateCode, out orgState);
            lblOrgNAme.Text = orgName;
            lblAddressORG.Text = orgAddress;
            lblORGLegalNos.Text = orgLegalNos;
            lblOrgStateCode.Text = orgStateCode;
            lblOrgState.Text = orgState;

            //lblOrgNAme.Text = ORG_Tools._OrganizationName;
            //lblAddressORG.Text = INVOICE_TOOLS._IsOrgAddress ? ORG_Tools._Address + " \n" : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgCityTown ? ORG_Tools._CityTown + ", " : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgDistrict ? ORG_Tools._Dist + " \n" : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgState ? ORG_Tools._State + ", " : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgPin ? ORG_Tools._PIN + " \n" : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgContactNo ? ORG_Tools._ContactNo1 + " \n" : "";
            //lblAddressORG.Text += INVOICE_TOOLS._IsOrgMailID ? ORG_Tools._Email + " " : "";

            //lblAddressORG.Text = lblAddressORG.Text + "\nState :" + ORG_Tools._StateCode + "-" + ORG_Tools._State;

            //lblORGLegalNos.Text = INVOICE_TOOLS._IsOrgGSTIN ? "GSTIN : " + ORG_Tools._GSTin + " \n" : "";
            //lblORGLegalNos.Text += INVOICE_TOOLS._IsOrgCIN ? "CIN    : " + ORG_Tools._CorporateNo + " \n" : "";
            //lblORGLegalNos.Text += INVOICE_TOOLS._IsOrgPAN ? "PAN    : " + ORG_Tools._PAN + " \n" : "";

            //lblOrgStateCode.Text = ORG_Tools._StateCode;
            //lblOrgState.Text = ORG_Tools._State;
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

            lblShippedToGstin.Text = gstNo;
            mIsIGST = (ORG_Tools._StateCode == billingStateCOde) ? false : true;
            GenerateGridForNonGSTType();
        }

        ///Billing Terms
        /// 
        private void BillingTermAndDueDate(string ledgerID)
        {
            string terms = "";
            int dueDays = BillingTermTools.GetBillingTermsAndDueDay(ledgerID, out terms);
            cmbBillingTerms.Text = terms;
            dtpDueDate.Value = dtpInvoiceDate.Value.Date.AddDays(dueDays);
        }

        /// <summary>
        ///Get Order details
        /// </summary>
        private void GetDataFromOrder()
        {
            string query = "Select Challan.*,LadgerMain.LadgerName from Challan " +
                           "inner join LadgerMain on Challan.LedgerID=LadgerMain.LadgerID " +
                           "where ChallanID='" + mChallanID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblChallanNo.Text = dt.Rows[0]["ChallanNo"].ToString();
                lblChallanDate.Text = DateTime.Parse(dt.Rows[0]["ChallanDate"].ToString()).ToString("dd-MMM-yyyy");
                lblBuyrOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
                lblBuyrOrderDate.Text = DateTime.Parse(dt.Rows[0]["OrderDate"].ToString()).ToString("dd-MMM-yyyy");
                lblDespatchThrough.Text = dt.Rows[0]["DespatchMode"].ToString();
                lblVehiclaNo.Text = dt.Rows[0]["VehicleNo"].ToString();

                lblNameBilling.Text = dt.Rows[0]["LadgerName"].ToString();
                mCustomerLadgerID = dt.Rows[0]["LedgerID"].ToString();

                GetCustomerAddressDetails(mCustomerLadgerID);
                BillingTermAndDueDate(mCustomerLadgerID);
                lblShippedTo.Text = dt.Rows[0]["ShippingTo"].ToString();
                lblShippedAddress.Text = dt.Rows[0]["ShippingAddress"].ToString();
                lblStateNameShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                lblStateCodeShipping.Text = dt.Rows[0]["ShippingStateCode"].ToString();

                string amountStr = dt.Rows[0]["TotalAmount"].ToString();
                mTotalInvoiceAmount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);
                lblTotalInvoiceAmount.Text = mTotalInvoiceAmount.ToString("0.00");

                string freightAmountStr = dt.Rows[0]["FreightCharges"].ToString();
                txtFreightCharges.Text = (freightAmountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(freightAmountStr)).ToString("0.00");

                string packingAmountStr = dt.Rows[0]["PackingCharges"].ToString();
                txtPackingCharges.Text = (packingAmountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(packingAmountStr)).ToString("0.00");

                string othersAmountStr = dt.Rows[0]["OtherCharges"].ToString();
                txtOtherCharges.Text = (othersAmountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(othersAmountStr)).ToString("0.00");

                GetChallanDetails();
                GenerateTotal();
            }
        }
        private void GetChallanDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select ChallanDetails.* from ChallanDetails " +
                           "inner join Challan on ChallanDetails.ChallanID=Challan.ChallanID " +
                           "where Challan.ChallanID='" + mChallanID + "'";
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

                    mDescriptionSlno++;
                }
            }
        }
        private void GenerateTotal()
        {
            mDescriptionSlno = 1;
            mTotalInvoiceAmount = 0d; mTotalAmount = 0d; mTotalDiscount = 0d;
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
            lblTotalDiscount.Text = mTotalDiscount.ToString("0.00");
            lblTaxableAmountTotal.Text = mTaxableAmount.ToString("0.00");

            lblTotalCGST.Text = mTotalCGST.ToString("0.00");
            lblTotalIGST.Text = mTotalIGST.ToString("0.00");
            lblTotalSGST.Text = mTotalSGST.ToString("0.00");
            lblTotalCESS.Text = mTotalCESS.ToString("0.00");
            lblTotalWithTax.Text = mTotalWithTax.ToString("0.00");
            double freight = 0d, packing = 0d, others = 0d, discount = 0d;
            double.TryParse(txtFreightCharges.Text, out freight);
            double.TryParse(txtPackingCharges.Text, out packing);
            double.TryParse(txtOtherCharges.Text, out others);
            double.TryParse(txtDiscount.Text, out discount);
            mTotalInvoiceAmount = (mTotalWithTax + freight + packing + others) - discount;
            lblTotalInvoiceAmount.Text = mTotalInvoiceAmount.ToString("0.00");
        }
        /// <summary>
        /// Invoice Save
        /// </summary>

        /// <summary>
        /// Billing Terms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBillingTerms_Click(object sender, EventArgs e)
        {
            BillingTermEntry frmBillingTermEntry = new BillingTermEntry();
            frmBillingTermEntry.OnClose += FrmBillingTermEntry_onClose;
            frmBillingTermEntry.ShowDialog();
        }
        private void txtFreightCharges_Leave(object sender, EventArgs e)
        {
            GenerateTotal();
        }
        private void InvoiceFrmChallan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }

        private void txtFreightCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtFreightCharges.Text.Contains('.'))
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

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtOtherCharges.Text.Contains('.'))
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

        private void FrmBillingTermEntry_onClose(string obj)
        {
            cmbBillingTerms.AddBillingTerms();
            cmbBillingTerms.Text = obj.ToString();
        }

        /// <summary>
        /// Valid Data
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (lblInvoiceNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Invoice not found.", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (lblInvoiceNo.Text.Length > 16)
            {
                MessageBox.Show("Total no of characters should not be more then 16.", "Invalid Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Item not found to create invoice.", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Data Save
        /// </summary>
        private void SaveInvoice()
        {
            mlistQuery.Clear();
            #region data
            string receivableheadid = AccountHeadTools._ReceivableAccountHeadId;
            string salesheadid = AccountHeadTools._SalesLedgerId;

            string invoiceNo = lblInvoiceNo.Text.GetDBFormatString();
            string invoiceDate = dtpInvoiceDate.Text;
            string billingTerms = cmbBillingTerms.Text;
            string dueDate = dtpDueDate.Text;

            string billingTo = lblNameBilling.Text;
            string billingAddress = lblBillingAddress.Text;
            string billingGSTIN = lblGSTIn.Text;
            string billingState = lblStateNameBilling.Text;
            string billingStateCode = lblStateCodeBilling.Text;

            string buyerOrderNo = lblBuyrOrderNo.Text;
            string buyerOrderDate = lblBuyrOrderDate.Text;

            string challanNo = lblChallanNo.Text;
            string challanDate = lblChallanDate.Text;
            string despatchThrough = lblDespatchThrough.Text;
            string vehiclaNo = lblVehiclaNo.Text;

            string shippingTo = lblShippedTo.Text;
            string shippingAddress = lblShippedAddress.Text;
            string shippingState = lblStateNameShipping.Text;
            string shippingStateCode = lblStateCodeShipping.Text;

            string totalQty = lblTotQuantity.Text;
            string totalAmount = lblTotAmount.Text;
            string totalDiscount = lblTotalDiscount.Text;
            string totalTaxAmount = lblTaxableAmountTotal.Text;
            string totalCGST = lblTotalCGST.Text;
            string totalSGST = lblTotalSGST.Text;
            string totalIGST = lblTotalIGST.Text;
            string totalCess = lblTotalCESS.Text;
            string totalNet = lblTotalWithTax.Text;

            double totalInvoiceAmount = 0d, freightCharges = 0d, pachingCharges = 0d, otherCharges = 0d, overAllDiscount = 0d;
            double.TryParse(lblTotalInvoiceAmount.Text, out totalInvoiceAmount);
            double.TryParse(txtFreightCharges.Text, out freightCharges);
            double.TryParse(txtPackingCharges.Text, out pachingCharges);
            double.TryParse(txtOtherCharges.Text, out otherCharges);
            double.TryParse(txtDiscount.Text, out overAllDiscount);
            string note = "";

            string rcm = (ORG_Tools._GSTtype == "Regular") ? "NO" : "YES";
            #endregion
            string query = "Insert into invoice( SlNo, InvoiceNo, InvoiceDate, LedgerId, BillingTerms, DueDate, " +
                           "BillingTo, BillingAddress, BillingGSTNO, BillingState, BillingStateCode, BuyerOrderNo, " +
                           "BuyerOrderDate, ChallanNo, ChallanDate, DispatchThrough, VehiclaNo, ShippingTo, " +
                           "ShippingAddress, ShippingState, ShippingStateCode, TotalQty, TotalAmount, TotalDiscount, " +
                           "TotalTaxAmount, TotalCGST, TotalSGST, TotalIGST, TotalCess, NetAmount, FreightChargs, " +
                           "PackingCharges, OtherCharges, OverAllDiscount, TotalInvoiceAmount,Note,RCM,InvoiceType) " +
                           "values(" + mInvoiceSlNo + ",'" + invoiceNo + "','" + invoiceDate + "','" +
                           mCustomerLadgerID + "','" + billingTerms + "','" + dueDate + "','" + billingTo + "','" +
                           billingAddress + "','" + billingGSTIN + "','" + billingState + "','" + billingStateCode
                           + "','" + buyerOrderNo + "','" + buyerOrderDate + "','" + challanNo + "','" + challanDate
                           + "','" + despatchThrough + "','" + vehiclaNo + "','" + shippingTo + "','" + shippingAddress
                           + "','" + shippingState + "','" + shippingStateCode + "'," + totalQty + "," + totalAmount
                           + "," + totalDiscount + "," + totalTaxAmount + "," + totalCGST + "," + totalSGST + "," + totalIGST + "," +
                           totalCess + "," + totalNet + "," + freightCharges + "," + pachingCharges + "," + otherCharges + "," +
                           overAllDiscount + "," + totalInvoiceAmount + ",'" + note + "','" + rcm + "','" + mInvoiceType + "')";
            mlistQuery.Add(query);

            string transectionid = Guid.NewGuid().ToString();

            query = "Insert into Transection(TransectionID,LedgerID,AccountHeadIdTo,Date,TransectionType,Amount, " +
                    "DueDate,VoucherNo,Balance,Status) values('" + transectionid + "','" + mCustomerLadgerID + "','" + receivableheadid + "','" + invoiceDate + "','Invoice'," + mTotalInvoiceAmount + ",'" + dueDate + "','" + invoiceNo + "'," + mTotalAmount + ",'Open')";
            mlistQuery.Add(query);
            SaveInvoiceDetails(invoiceNo, transectionid);
            switch (mInvoiceFrom)
            {
                case _Invoicefrom.Order:
                    break;
                case _Invoicefrom.Challan:
                    UpdateChallan();
                    break;
                default:
                    break;
            }
            if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
            {
                if (MessageBox.Show("Invoice saved.\nDo you want to print now ?", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InvoiceReportViewer frmInvoiceReportViewer = new InvoiceReportViewer(InvoiceReportViewer._InvoiceCopy.Original, lblInvoiceNo.Text);
                    frmInvoiceReportViewer.OnClose += FrmInvoiceReportViewer_OnClose;
                    frmInvoiceReportViewer.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
        private void SaveInvoiceDetails(string invoiceNo, string transectionId)
        {
            string receivableheadid = AccountHeadTools._ReceivableAccountHeadId;
            string salesheadid = AccountHeadTools._SalesLedgerId;
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemId = row.Cells["ItemId"].Value.ToString();
                string hsnCode = row.Cells["ParticularsHsnCode"].Value.ToString();
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

                string taxAmountStr = row.Cells["TAXABLEVALUE"].Value.ToString();
                double taxAmount = taxAmountStr.ISNullOrWhiteSpace() ? 0f : double.Parse(taxAmountStr);

                object cgstRateStr = row.Cells["CGSTRATE"].Value;
                string cgstRate = !cgstRateStr.ISValidObject() ? "NULL" : cgstRateStr.ToString();
                object cgstAmountStr = row.Cells["CGSTAMOUNT"].Value.ToString();
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

                object netAmountStr = row.Cells["TotalWithTax"].Value;
                string netAmount = !netAmountStr.ISValidObject() ? "NULL" : netAmountStr.ToString();

                string query = "Insert into InvoiceDetails(InvoiceNo, ItemID, HSNCode, ItemName, Quantity,Unit, Rate, Amount, " +
                               "DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, IGSTRate, " +
                               "IGSTAmount, CessRate, CeassAmount,Total) values('" + invoiceNo + "'," + itemId + ",'" + hsnCode + "','" +
                               itemName + "'," + quantity + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount
                               + "," + taxAmount + "," + cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," +
                               igstRate + "," + igstAmount + "," + cessRate + "," + cessAmount + "," + netAmount + ")";
                mlistQuery.Add(query);

                query = "Insert into TransectionDetails(TransectionID,AccountHeadFrom,AccountHeadTo,TransectionType,Quantity,Rate,Amount)  " +
                        "values('" + transectionId + "','" + salesheadid + "','" + receivableheadid + "','Invoice'," + quantity + "," + rate + "," + amount + ")";
                mlistQuery.Add(query);
            }
        }
        private void UpdateChallan()
        {
            string query = "Update Challan set Status='Close' where ChallanID='" + mChallanID + "'";
            mlistQuery.Add(query);
        }
        private void FrmInvoiceReportViewer_OnClose()
        {
            this.Close();
        }
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SaveInvoice();
            }
        }
    }
}
