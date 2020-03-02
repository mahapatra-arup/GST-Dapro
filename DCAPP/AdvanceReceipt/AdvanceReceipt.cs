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
    public partial class AdvanceReceipt : Form
    {
        public event Action Onclose;
        string mcgstrate = "";
        string msgstrate = "";
        string migstrate = "";
        string mcessrate = "";
        string mitemid = "";
        string mQuery = "";
        List<string> mListquery = new List<string>();
        string mOrderid = "", mOrderNofromAdvReceipt = "", msg = "", mLedgerId = "", mReceiptNoForEdit = "";
        bool mIsIGST = false;
        long mSerialno = OtherSettingTools._AdvanceReceiptVoucherSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._AdvanceReceiptVoucherSerialStart);
        string mTransectionID = "";
        double mTotalPreviousvalue = 0d;
        public enum _FromWherere
        {
            Direct,
            Order
        }
        _FromWherere mformwhere;
        public AdvanceReceipt(string receiptno, string status, string notuse)
        {
            InitializeComponent();
            this.FitToVertical();
            mReceiptNoForEdit = receiptno;
            pnlGst.Hide();
            grbOrder.Show();
            GridDesign();
            cmbPaymentMethod.SelectedIndex = 0;
            AdvanceReceiptDataRetrive();
            ReadOnlyAllControl(status);
        }
        public AdvanceReceipt(_FromWherere formWhere, string id)
        {
            InitializeComponent();
            this.FitToVertical();
            mformwhere = formWhere;
            GenerateSlNo();
            pnlGst.Hide();
            grbOrder.Hide();
            GridDesign();
            cmbPaymentMethod.SelectedIndex = 0;
            if (mformwhere == _FromWherere.Order)
            {
                mOrderid = id;
                grbOrder.Show();
                GetOrderDetails();
            }
            else
            {
                mLedgerId = id;
                string customername;
                Customertools._DicCustomers.TryGetValue(mLedgerId, out customername);
                lblCustomername.Text = customername;
                GetCustomerAddressDetails(mLedgerId);
                GetCustomerShippedDetails(mLedgerId);
                pnlGst.Show();
            }
        }
        private void AdvanceReceiptDataRetrive()
        {
            string query = "Select * from AdvanceReceiptVoucher where ReceiptNo='" + mReceiptNoForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblreceiptNo.Text = mReceiptNoForEdit;
                mLedgerId = dt.Rows[0]["LedgerId"].ToString();
                string customername = "";
                Customertools._DicCustomers.TryGetValue(mLedgerId, out customername);
                mTransectionID = dt.Rows[0]["LastTransecetionID"].ToString();
                mTotalPreviousvalue = double.Parse(dt.Rows[0]["Total"].ToString());
                TransectionTools.GetPaymentDetailsId(mTransectionID);
                cmbPaymentMethod.Text = TransectionTools._PaymentMethod;
                cmbPaymentMethod_SelectedIndexChanged(null, null);
                cmbPaymentAccount.Text = TransectionTools._DRAccountTemplateName;
                cmbBank.Text = TransectionTools._BankName;
                txtChequeNo.Text = TransectionTools._ChequeNo;
                dtpDateCheque.Text = TransectionTools._ChequeDate;

                lblCustomername.Text = customername;
                dtpReceiptVoucharDate.Text = dt.Rows[0]["ReceiptDate"].ToString();
                string orderno = dt.Rows[0]["OrderNo"].ToString();
                lblOrderNo.Text = orderno;
                mOrderNofromAdvReceipt = orderno;
                string orderDate = dt.Rows[0]["OrderDate"].ToString();
                lblOrderate.Text = orderDate.ISNullOrWhiteSpace() ? "" : DateTime.Parse(orderDate).ToString("dd-MMM-yyyy");
                string itemId = dt.Rows[0]["ItemId"].ToString();
                string itemName = dt.Rows[0]["ItemName"].ToString();
                string comodityCode = dt.Rows[0]["ComodityCode"].ToString();
                string qty = dt.Rows[0]["Qty"].ToString();
                string unit = dt.Rows[0]["Unit"].ToString();
                string rate = dt.Rows[0]["Rate"].ToString();
                string taxValue = dt.Rows[0]["TaxValue"].ToString();
                mcgstrate = dt.Rows[0]["CGSTRate"].ToString();
                string cGSTAmount = dt.Rows[0]["CGSTAmount"].ToString();
                msgstrate = dt.Rows[0]["SGSTRate"].ToString();
                string sGSTAmount = dt.Rows[0]["SGSTAmount"].ToString();
                migstrate = dt.Rows[0]["IGSTRate"].ToString();
                string iGSTAmount = dt.Rows[0]["IGSTAmount"].ToString();
                mcessrate = dt.Rows[0]["CessRate"].ToString();
                string cessAmount = dt.Rows[0]["CessAmount"].ToString();
                string totalGst = dt.Rows[0]["TotalGst"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                string total = dt.Rows[0]["Total"].ToString();
                mTotalPreviousvalue = double.Parse(total);
                if (orderno.ISNullOrWhiteSpace())
                {
                    grbOrder.Hide();
                    pnlGst.Show();
                    double cgstrate = 0, sgstrate = 0;
                    double.TryParse(mcgstrate, out cgstrate);
                    double.TryParse(msgstrate, out sgstrate);
                    cmbGstRate.Text = !migstrate.ISNullOrWhiteSpace() ? migstrate : (cgstrate + sgstrate).ToString();
                }
                lblTotaltaxbleValue.Text = taxValue;
                lblTotalGstValue.Text = totalGst;
                lblTotalReceiptVoucherValue.Text = total;
                txtAdvanceAmount.Text = total;
                GetCustomerAddressDetails(mLedgerId);
                GetCustomerShippedDetails(mLedgerId);

                dgvItemList.Rows.Clear();
                dgvItemList.Rows.Add(1, itemId, itemName, comodityCode, qty, unit, rate, "", "", "", taxValue, mcgstrate, cGSTAmount, msgstrate, sGSTAmount, migstrate, iGSTAmount
                                     , mcessrate, cessAmount, total);
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
        private void GenerateSlNo()
        {
            string query = "Select max(SlNo) as slno from AdvanceReceiptVoucher ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                try
                {
                    mSerialno = (long.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblreceiptNo.Text = OtherSettingTools._AdvanceReceiptVoucherStart + mSerialno.ToString();
        }
        private void GetOrderDetails()
        {
            string query = "Select SalesOrderDetails.ItemName,SalesOrderDetails.ItemID,SalesOrderDetails.ComodityCode,SalesOrderDetails.Unit,SalesOrderDetails.qty,SalesOrderDetails.Rate,salesorder.CustomerOrderNo,salesorder.OrderDate,salesorder.LedgerID,salesorder.TotalAmount from salesorder" +
                " inner join SalesOrderDetails on SalesOrder.OrderId=SalesOrderDetails.orderid where SalesOrder.OrderId='" + mOrderid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                object orderno = dt.Rows[0]["CustomerOrderNo"];
                lblOrderNo.Text = orderno.ISValidObject() ? orderno.ToString() : "Not Assign";
                lblOrderate.Text = DateTime.Parse(dt.Rows[0]["OrderDate"].ToString()).ToString("dd-MMM-yyyy");
                mLedgerId = dt.Rows[0]["LedgerId"].ToString();
                lblOrdervalue.Text = dt.Rows[0]["TotalAmount"].ToString();
                string ledgerName = "";
                Customertools._DicCustomers.TryGetValue(mLedgerId, out ledgerName);
                lblCustomername.Text = ledgerName;
                GetCustomerAddressDetails(mLedgerId);
                GetCustomerShippedDetails(mLedgerId);

                string itemName = dt.Rows[0]["ItemName"].ToString();
                mitemid = dt.Rows[0]["ItemID"].ToString();
                string hsnCode = dt.Rows[0]["ComodityCode"].ToString();
                string unit = dt.Rows[0]["Unit"].ToString();
                string qty = dt.Rows[0]["qty"].ToString();
                string rate = dt.Rows[0]["Rate"].ToString();
                ItemTools.GetItemGSTRateIsiGst(mitemid, mIsIGST, out mcgstrate, out msgstrate, out migstrate, out mcessrate);
                dgvItemList.Rows.Add(1, mitemid, itemName, hsnCode, qty, unit, rate, "", "", "", "", mcgstrate, "", msgstrate, "", migstrate, ""
                                     , mcessrate, "", "");
            }
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
        private void GenerateGridForNonGSTType()
        {
            if (mOrderid.ISNullOrWhiteSpace() && mOrderNofromAdvReceipt.ISNullOrWhiteSpace())
            {
                dgvItemList.Columns["itemname"].Visible = false;
                dgvItemList.Columns["ParticularsHsnCode"].Visible = false;
                dgvItemList.Columns["Unit"].Visible = false;
                dgvItemList.Columns["QTY"].Visible = false;
                dgvItemList.Columns["RATE"].Visible = false;
                dgvItemList.Columns["TOTALAMOUNT"].Visible = false;
                dgvItemList.Columns["DISCOUNTAMOUNT"].Visible = false;
                dgvItemList.Columns["DISCOUNTRATE"].Visible = false;
            }
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

                }
                else
                {
                    dgvItemList.Columns["CGSTRATE"].Visible = true;
                    dgvItemList.Columns["CGSTAMOUNT"].Visible = true;
                    dgvItemList.Columns["SGSTRATE"].Visible = true;
                    dgvItemList.Columns["SGSTAMOUNT"].Visible = true;
                    dgvItemList.Columns["IGSTRATE"].Visible = false;
                    dgvItemList.Columns["IGSTAMOUNT"].Visible = false;
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

            }
        }
        private void CalculateAdvanceReceiptAmpount()
        {
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "", taxWithamount = "";
            double taxvalue = 0d, taxableamount = 0d, totalrate = 0d,
                   advanceamount = 0d, cessrate = 0d,
                   cgstrate = 0d, sgstrate = 0d,
                   igstrate = 0d;
            double.TryParse(mcgstrate, out cgstrate);
            double.TryParse(msgstrate, out sgstrate);
            double.TryParse(mcessrate, out cessrate);
            double.TryParse(migstrate, out igstrate);
            double.TryParse(txtAdvanceAmount.Text, out advanceamount);
            totalrate = cgstrate + sgstrate + cessrate + igstrate;
            try
            {
                taxvalue = ((advanceamount * totalrate) / (100 + totalrate));
                if (mIsIGST)
                {
                    igstAmount = taxvalue.toString();
                }
                else
                {
                    cgstAmount = (taxvalue / 2).toString();
                    sgstAmount = (taxvalue / 2).toString();
                }
            }
            catch (Exception) { }
            taxableamount = advanceamount - taxvalue;
            taxWithamount = (taxableamount + taxvalue).toString();

            if (!mOrderid.ISNullOrWhiteSpace())
            {
                ItemTools.GetItemGSTRateAndAmount(mitemid, mIsIGST, taxableamount, out mcgstrate, out cgstAmount, out msgstrate,
                                               out sgstAmount, out migstrate, out igstAmount, out mcessrate, out cessAmount, out taxWithamount);
            }
            dgvItemList.Rows[0].Cells["CGSTAMOUNT"].Value = cgstAmount.toRound();
            dgvItemList.Rows[0].Cells["CGSTRATE"].Value = mcgstrate;
            dgvItemList.Rows[0].Cells["SGSTAMOUNT"].Value = sgstAmount.toRound();
            dgvItemList.Rows[0].Cells["SGSTRATE"].Value = msgstrate;
            dgvItemList.Rows[0].Cells["IGSTAMOUNT"].Value = igstAmount.toRound();
            dgvItemList.Rows[0].Cells["IGSTRATE"].Value = migstrate;
            dgvItemList.Rows[0].Cells["CESSAMOUNT"].Value = cessAmount.toRound();
            dgvItemList.Rows[0].Cells["CESSRATE"].Value = mcessrate;
            dgvItemList.Rows[0].Cells["TAXABLEVALUE"].Value = taxableamount.toString();
            dgvItemList.Rows[0].Cells["TotalWithTax"].Value = taxWithamount.toRound();

            lblTotaltaxbleValue.Text = taxableamount.toString();
            lblTotalGstValue.Text = taxvalue.toString();
            lblTotalReceiptVoucherValue.Text = taxWithamount.toRound();
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
                    if (mformwhere == _FromWherere.Direct)
                    {
                        length = 2;
                        array[0] = "IGST";
                        array[1] = "CESS";

                        ary[0] = 15;
                        ary[1] = 17;
                    }
                    else
                    {
                        length = 3;
                        array[0] = "DISCOUNT";
                        array[1] = "IGST";
                        array[2] = "CESS";

                        ary[0] = 8;
                        ary[1] = 15;
                        ary[2] = 17;
                    }
                }
                else
                {
                    if (mformwhere == _FromWherere.Direct)
                    {
                        length = 3;
                        array[0] = "CGST";
                        array[1] = "SGST";
                        array[2] = "CESS";

                        ary[0] = 11;
                        ary[1] = 13;
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

            }
            else
            {
                if (mformwhere == _FromWherere.Direct)
                {
                    length = 0;
                }
                else
                {
                    length = 1;
                    array[0] = "DISCOUNT";
                    ary[0] = 8;
                }
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
        private void txtAdvanceAmount_Leave(object sender, EventArgs e)
        {
            if (!txtAdvanceAmount.Text.ISNullOrWhiteSpace())
            {
                if (mOrderid.ISNullOrWhiteSpace() && mOrderNofromAdvReceipt.ISNullOrWhiteSpace())
                {
                    dgvItemList.Rows.Clear();
                    dgvItemList.Rows.Add(1.ToString());
                }
                CalculateAdvanceReceiptAmpount();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValidEntry())
            {
                DataSave();
            }
        }
        private void DataSave()
        {
            #region Data 
            string transectionid = Guid.NewGuid().ToString();

            string receptno = lblreceiptNo.Text.GetDBFormatString();
            string receiptddate = dtpReceiptVoucharDate.Text.GetDBFormatString();
            string orderno = "NULL";
            string orderdate = "NULL";
            string rcm = "NO";
            #region Billing
            string billingname = lblNameBilling.Text.GetDBFormatString();
            string billingaddress = lblBillingAddress.Text.GetDBFormatString();
            string billingstate = lblStateNameBilling.Text.GetDBFormatString();
            string billingstatecode = lblStateCodeBilling.Text.GetDBFormatString();
            #endregion

            #region Shipping
            string shippingname = lblShippedTo.Text.GetDBFormatString();
            string shippingaddress = lblShippedAddress.Text.GetDBFormatString();
            string shippingstate = lblStateNameShipping.Text.GetDBFormatString();
            string shippingstatecode = lblStateCodeShipping.Text.GetDBFormatString();
            #endregion

            #region Item
            object itemidobj = dgvItemList.Rows[0].Cells["itemid"].Value;
            string itemid = itemidobj.ISValidObject() ? "" + itemidobj.ToString() + "" : "NULL";

            object itemnameobj = dgvItemList.Rows[0].Cells["itemname"].Value;
            string itemname = itemnameobj.ISValidObject() ? "'" + itemnameobj.ToString() + "'" : "NULL";

            object comoditicodeobj = dgvItemList.Rows[0].Cells["ParticularsHsnCode"].Value;
            string comoditicode = comoditicodeobj.ISValidObject() ? "'" + comoditicodeobj.ToString() + "'" : "NULL";

            object qtyobj = dgvItemList.Rows[0].Cells["QTY"].Value;
            string qty = qtyobj.ISValidObject() ? "" + qtyobj.ToString() + "" : "NULL";

            object unitobj = dgvItemList.Rows[0].Cells["UNIT"].Value;
            string unit = unitobj.ISValidObject() ? "'" + unitobj.ToString() + "'" : "NULL";

            object rateobj = dgvItemList.Rows[0].Cells["RATE"].Value;
            string rate = rateobj.ISValidObject() ? "" + rateobj.ToString() + "" : "NULL";

            object taxvalueobj = dgvItemList.Rows[0].Cells["TAXABLEVALUE"].Value;
            string taxvalue = taxvalueobj.ISValidObject() ? "" + taxvalueobj.ToString() + "" : "NULL";

            object cgstrateobj = dgvItemList.Rows[0].Cells["CGSTRATE"].Value;
            object cgstamountobj = dgvItemList.Rows[0].Cells["CGSTAMOUNT"].Value;
            string cgstrate = cgstrateobj.ISValidObject() ? "" + cgstrateobj.ToString() + "" : "NULL";
            string cgstamount = cgstamountobj.ISValidObject() ? "" + cgstamountobj.ToString() + "" : "NULL";

            object sgstrateobj = dgvItemList.Rows[0].Cells["SGSTRATE"].Value;
            object sgstamountobj = dgvItemList.Rows[0].Cells["SGSTAMOUNT"].Value;
            string sgstrate = sgstrateobj.ISValidObject() ? "" + sgstrateobj.ToString() + "" : "NULL";
            string sgstamount = sgstamountobj.ISValidObject() ? "" + sgstamountobj.ToString() + "" : "NULL";

            object igstrateobj = dgvItemList.Rows[0].Cells["IGSTRATE"].Value;
            object igstamountobj = dgvItemList.Rows[0].Cells["IGSTAMOUNT"].Value;
            string igstrate = igstrateobj.ISValidObject() ? "" + igstrateobj.ToString() + "" : "NULL";
            string igstamount = igstamountobj.ISValidObject() ? "" + igstamountobj.ToString() + "" : "NULL";

            object cessrateobj = dgvItemList.Rows[0].Cells["CESSRATE"].Value;
            object cessamountobj = dgvItemList.Rows[0].Cells["CESSAMOUNT"].Value;
            string cessrate = cessrateobj.ISValidObject() ? "" + cessrateobj.ToString() + "" : "NULL";
            string cessamount = cessamountobj.ISValidObject() ? "" + cessamountobj.ToString() + "" : "NULL";

            #endregion

            string totalgst = lblTotalGstValue.Text.GetDBFormatString();
            string totaladvancevalue = lblTotalReceiptVoucherValue.Text.GetDBFormatString();
            string description = txtDescription.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtDescription.Text.GetDBFormatString() + "'";
            string status = "Open";
            string lasttransectionid = transectionid;
            if (!mOrderid.ISNullOrWhiteSpace())
            {
                orderno = "'" + lblOrderNo.Text.GetDBFormatString() + "'";
                orderdate = "'" + lblOrderate.Text.GetDBFormatString() + "'";
            }
            string transectiontype = "Advance_Receipt";
            string drledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
            string crledgerid = mLedgerId;
            string mode = "'" + cmbPaymentMethod.Text + "'";
            string bankname = cmbPaymentMethod.Text == "Cheque" ? "'" + cmbBank.Text.GetDBFormatString() + "'" : "NULL";
            string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
            string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";

            #endregion
            #region Query
            if (mReceiptNoForEdit.ISNullOrWhiteSpace())
            {
                mQuery = "insert into AdvanceReceiptVoucher(SlNo,ReceiptNo," +
                        "ReceiptDate,OrderNo,OrderDate,ReverseCharge,LedgerId,BillingName," +
                        "BillingAddress,BillingState,BillingStateCode,ShippingName,ShippingAddress," +
                        "ShippingState,ShippingStateCode,ItemId,ItemName," +
                        "ComodityCode, Qty, Unit,RATE, TaxValue, CGSTRate, CGSTAmount," +
                        " SGSTRate, SGSTAmount, IGSTRate, IGSTAmount, CessRate,CessAmount,TotalGst," +
                        " Total,Description, Status,DueAmount,LastTransecetionID) values(" + mSerialno + ",'" + receptno + "','" + receiptddate + "'," + orderno + "," + orderdate
                        + ",'" + rcm + "','" + mLedgerId + "','" + billingname + "','" + billingaddress + "','" + billingstate + "','" + billingstatecode
                        + "','" + shippingname + "','" + shippingaddress + "','" + shippingstate + "','" + shippingstatecode
                        + "'," + itemid + "," + itemname + "," + comoditicode + "," + qty + "," + unit + "," + rate + "," + taxvalue
                        + "," + cgstrate + "," + cgstamount + "," + sgstrate + "," + sgstamount + "," + igstrate + "," + igstamount
                        + "," + cessrate + "," + cessamount + "," + totalgst + "," + totaladvancevalue + "," + description + ",'" + status + "'," + totaladvancevalue + ",'" + lasttransectionid + "')";
                mListquery.Add(mQuery);
                InsertOrUpdateTransection(transectionid, receiptddate, receptno, totaladvancevalue, drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate);
                mListquery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totaladvancevalue, out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mListquery.Add(mQuery);//AddCrQuery
                
                //UpdateCurrentBalance(drledgerid);
                //mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                //        "LedgerIdTo, Amount_Dr) values('" + transectionid + "','" +
                //        receiptddate + "','" + receptno + "','Advance_Receipt','" + DebitLedgerId + "','" +
                //        mLedgerId + "'," + totaladvancevalue + ")";
                //mListquery.Add(mQuery);

                //transectionid = Guid.NewGuid().ToString();
                //mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                //        "LedgerIdTo, Amount_Cr) values('" + transectionid + "','" +
                //        receiptddate + "','" + receptno + "','Advance_Receipt','" + mLedgerId  + "','" +
                //         DebitLedgerId + "'," + totaladvancevalue + ")";
                //mListquery.Add(mQuery);
                //InsertIntoTransectionMore(lasttransectionid);

            }
            else
            {
                mQuery = "Update AdvanceReceiptVoucher set ReceiptDate='" + receiptddate + "',BillingName='" + billingname + "',BillingAddress='" + billingaddress + "',BillingState='" + billingstate + "'," +
                   "BillingStateCode='" + billingstatecode + "',ShippingName='" + shippingname + "',ShippingAddress='" + shippingaddress + "',ShippingState='" + shippingstate + "',ShippingStateCode='" + shippingstatecode
                   + "',ItemId=" + itemid + ",ItemName=" + itemname + ",ComodityCode=" + comoditicode + ", Qty=" + qty + ", Unit=" + unit + ",RATE=" + rate + ", TaxValue=" + taxvalue
                   + ", CGSTRate=" + cgstrate + ", CGSTAmount=" + cgstamount + ",SGSTRate=" + sgstrate + ", SGSTAmount=" + sgstamount + ", IGSTRate=" + igstrate + ", IGSTAmount=" + igstamount + ", CessRate=" + cessrate
                   + ",CessAmount=" + cessamount + ",TotalGst=" + totalgst + ", Total=" + totaladvancevalue + ",Description=" + description + ",DueAmount=" + totaladvancevalue + " where ReceiptNo='" + mReceiptNoForEdit + "'";
                mListquery.Add(mQuery);
                InsertOrUpdateTransection(transectionid, receiptddate, receptno, totaladvancevalue, drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate);
                #region CurrentBalanceRestore

                mListquery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mTotalPreviousvalue.ToString("0.00"), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mListquery.Add(mQuery);

                #endregion
                #region CurrentBalanceUpdate

                mListquery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, totaladvancevalue, out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mListquery.Add(mQuery);

                #endregion


            }
            #endregion

            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mListquery, out msg))
            {
                MessageBox.Show("\"" + totaladvancevalue + "\" receipt successfully genarate..", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OtherSettingTools._IsAdvanceReceiptBillgenarate = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg);
            }
            #endregion
        }
        private void InsertOrUpdateTransection(string tranid, string receiptdate, string receiptno, string totalNotevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate)
        {
            if (mReceiptNoForEdit.ISNullOrWhiteSpace())
            {
                string transectionid = tranid;
                mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        receiptdate + "','" + receiptno + "','" + transectiontype + "','" + drledgerid + "','" +
                        crledgerid + "'," + totalNotevalue + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mListquery.Add(mQuery);
                transectionid = Guid.NewGuid().ToString();
                mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        receiptdate + "','" + receiptno + "','" + transectiontype + "','" + crledgerid + "','" +
                        drledgerid + "'," + totalNotevalue + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mListquery.Add(mQuery);
            }
            else
            {
                TransectionTools.GetTransectionId(receiptno, "Advance_Receipt");

                mQuery = "Update Transection Set Date='" + receiptdate + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalNotevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mListquery.Add(mQuery);

                mQuery = "Update Transection Set Date='" + receiptdate + "',LedgerIdFrom='" + crledgerid + "', " +
                        "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalNotevalue + ",Mode=" + Mode + ",bankname=" +
                        BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mListquery.Add(mQuery);
            }
        }
        private void InsertIntoTransectionMore(string transectionid)
        {
            string bankid = cmbBank.Text.ISNullOrWhiteSpace() ? "NULL" : ((KeyValuePair<string, string>)cmbBank.SelectedItem).Key.ToString();
            if (mReceiptNoForEdit.ISNullOrWhiteSpace())
            {
                mQuery = "insert into TransectionMore(TransectionID, Mode,BankID, ChequeNo, ChequeDate) values('" + transectionid
                       + "','" + cmbPaymentMethod.Text + "'," + bankid + ",'" + txtChequeNo.Text.GetDBFormatString() + "','" + dtpDateCheque.Text + "') ";
                mListquery.Add(mQuery);
            }
            else
            {
                mQuery = "update TransectionMore set Mode='" + cmbPaymentMethod.Text + "',BankID=" + bankid + ",ChequeNo='" + txtChequeNo.Text.GetDBFormatString() + "',ChequeDate='" + dtpDateCheque.Text + "' where TransectionID='" + mTransectionID + "' ";
                mListquery.Add(mQuery);
            }
        }
        private void UpdateCurrentBalance(string ledgerid)
        {
            if (!mReceiptNoForEdit.ISNullOrWhiteSpace())
            {
                RestoreCurrentBalance();
            }
            mQuery = "update LedgerStatus set CurrentBalance=(select (CurrentBalance+" + lblTotalReceiptVoucherValue.Text.GetDBFormatString() + ") as currentbal  from LedgerStatus where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "')  where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "' ";
            mListquery.Add(mQuery);
        }
        private void RestoreCurrentBalance()
        {
            mQuery = "update LedgerStatus set CurrentBalance=(select (CurrentBalance-" + mTotalPreviousvalue + ") as currentbal  from LedgerStatus where LedgerID='" + TransectionTools._DRAccountLedgerId + "' )  where LedgerID='" + TransectionTools._DRAccountLedgerId + "'";
            mListquery.Add(mQuery);
        }
        private bool isValidEntry()
        {
            if (cmbPaymentMethod.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment method for payment.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentMethod.Select();
                return false;
            }
            if (cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment account.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentAccount.Select();
                return false;
            }
            if (cmbPaymentMethod.Text == "Cheque")
            {
                if (cmbBank.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select Bank name.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbBank.Select();
                    return false;
                }
                if (txtChequeNo.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter cheqe No.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtChequeNo.Focus();
                    return false;
                }
            }
            if (mOrderid.ISNullOrWhiteSpace() && mOrderNofromAdvReceipt.ISNullOrWhiteSpace())
            {
                if (cmbGstRate.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select GST Percentage", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbGstRate.Focus();
                    return false;
                }
            }
            if (txtAdvanceAmount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter advance Amount.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAdvanceAmount.Focus();
                return false;
            }
            else if (double.Parse(txtAdvanceAmount.Text) <= 0)
            {
                MessageBox.Show("You  enter zero amount.", "Advance Receipt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAdvanceAmount.Focus();
                return false;
            }

            return true;
        }
        private void cmbGstRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            migstrate = ""; mcgstrate = ""; msgstrate = ""; mcessrate = "";
            double gst = 0;
            if (!cmbGstRate.Text.ISNullOrWhiteSpace())
            {
                double.TryParse(cmbGstRate.Text, out gst);
                if (mIsIGST)
                {
                    migstrate = gst.ToString();
                }
                else
                {
                    msgstrate = (gst / 2).ToString();
                    mcgstrate = (gst / 2).ToString();
                }
            }
            if (!txtAdvanceAmount.Text.ISNullOrWhiteSpace())
            {
                txtAdvanceAmount_Leave(txtAdvanceAmount, null);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else { e.Handled = true; }
        }
        private void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            txtChequeNo.ForeColor = Color.Red;
            if (txtChequeNo.Text.Length == 6)
            {
                txtChequeNo.ForeColor = Color.Black;
            }
        }
        private void AdvanceReceipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Onclose != null)
            {
                Onclose();
            }
        }
        private void txtAdvanceAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtAdvanceAmount.Text.Contains('.'))
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
        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string ledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                float balance = AccountHeadTools.GetCurrentBalance(ledgerid);
            }
        }
    }
}
