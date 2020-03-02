using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class PurchaseBillEntry : Form
    {
        #region Declearation
        public event Action OnClose;
        private bool mIsSuccess = false, isigst = false, isregular = false,mPreviousIsIgst=false;
        private long mSlNo = 1;
        private double mOrderRate = 0d, mtotalInvoiceAmount = 0d, mtotalPreviosInvoiceAmount = 0d,
                       mTotalOrderAmount, mTotalAmount, mTotalDiscount,
                       mTotalCGST, mTotalSGST, mTotalIGST, mTotalCESS,
                       mTaxableAmount, mTotalWithTax, mTotalQuantity = 0, mdueamount = 0d, mOrderQuantity = 0;
        private int IsBillissu = 0, mDescriptionSlno = 1,mTemp=0;
        private string mQuery = "", mFixedOrderQty = "", msg = "", mbilltype = "",
                       mChallanID = "", mOrderID = "", mOrderIdFromChallan = "",
                       mSupplierLadgerID = "", mGstType, mIsRcm = "No", mPurchaseBillIdforEdit = "",
                       mPurchaseOrderDate = "", mPurchaseOrderNo = "", mChallanDate, mpreviousLedgerid = "",mpreviousGsttype="",mPreviousLedgername="";
        List<string> mlistQuery = new List<string>();
        public enum _CameFrom
        {
            Order,
            Challan,
            ExpenseBill,
            PurchaseBill,
        }
        public _CameFrom mCamefrom;

        #endregion

        public PurchaseBillEntry(_CameFrom camefrm)
        {
            InitializeComponent();
            this.FitToVertical();
            mCamefrom = camefrm;
            if (mCamefrom == _CameFrom.PurchaseBill)
            {
                mbilltype = "Purchase_Bill";
            }
            else if (mCamefrom == _CameFrom.ExpenseBill)
            {
                mbilltype = "Expense_Bill";
            }
            GridDesign();
            cmbPaymentTerms.SelectedIndexChanged -= cmbPaymentTerms_SelectedIndexChanged;
            cmbPaymentTerms.AddBillingTerms();
            cmbPaymentTerms.SelectedIndexChanged += cmbPaymentTerms_SelectedIndexChanged;
            cmbSupplierName.AddSuppliers();
            cmbItemName.AddItem();
            cmbUnit.AddUnit();
            GenerateSerialNo();
            GetOrderDetails();
            GenerateGridForNonGSTType();
        }
        public PurchaseBillEntry(string Purchasebilid, string status)
        {
            InitializeComponent();
            this.FitToVertical();
            mPurchaseBillIdforEdit = Purchasebilid;
            
            if (mCamefrom == _CameFrom.PurchaseBill)
            {
                mbilltype = "Purchase_Bill";
            }
            GridDesign();
            cmbPaymentTerms.SelectedIndexChanged -= cmbPaymentTerms_SelectedIndexChanged;
            cmbPaymentTerms.AddBillingTerms();
            cmbPaymentTerms.SelectedIndexChanged += cmbPaymentTerms_SelectedIndexChanged;
            cmbSupplierName.AddSuppliers();
            cmbItemName.AddItem();
            cmbUnit.AddUnit();
            GenerateGridForNonGSTType();
            RetruveDataFromPurchaseBill();
            ReadOnlyAllControl(status);
        }
        public PurchaseBillEntry(_CameFrom camefrm, string id, string orderidfromchallan)
        {
            InitializeComponent();
            this.FitToVertical();
            mbilltype = "Purchase_Bill";
            GridDesign();
            cmbPaymentTerms.SelectedIndexChanged -= cmbPaymentTerms_SelectedIndexChanged;
            cmbPaymentTerms.AddBillingTerms();
            cmbPaymentTerms.SelectedIndexChanged += cmbPaymentTerms_SelectedIndexChanged;
            GenerateSerialNo();

            dgvItemList.Location = new Point(6, 266);
            dgvItemList.Height = 212;
            mCamefrom = camefrm;
            switch (mCamefrom)
            {
                case _CameFrom.Order:
                    mOrderID = id;
                    GetOrderDetails();
                    GetSupplierDetails();
                    OrderItemDetails();
                    break;
                case _CameFrom.Challan:
                    dgvItemList.ReadOnly = true;
                    mChallanID = id;
                    mOrderIdFromChallan = orderidfromchallan;
                    GetOrderDetails();
                    GetSupplierDetails();
                    OrderItemDetails();
                    PurchaseChallanDataRetrive();
                    break;
                default:
                    break;
            }
            GenerateGridForNonGSTType();
        }

        private void ReadOnlyAllControl(string status)
        {
            if (status == "Paid" || IsStockEntry() || status == "Cancel")
            {
                foreach (Control item in this.Controls)
                {
                    item.Enabled = false;
                }
            }
            btnCancel.Enabled = true;
        }
        public void CancelBill(string status)
        {
            if (status == "Paid" || !IsStockEntry() || status == "Cancel")
            {
                if (MessageBox.Show("Are Sure to cancel this bill?", "Purchase Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mlistQuery.Clear();
                    #region data

                    string transectionid = Guid.NewGuid().ToString();
                    string billno = lblNo.Text.GetDBFormatString();

                    string purchaseLedgerId = AccountHeadTools._PurchaseLedgerId;
                    string invoiceDate = dtpEntryDate.Text;

                    string transectiontype = "Purchase_Bill";
                    string drledgerid = purchaseLedgerId;
                    string crledgerid = mSupplierLadgerID;
                    mTotalAmount = double.Parse(lblTotalChallanAmount.Text);
                    #endregion

                    mQuery = "Update PurchaseBill set TotalAmount='0',DueAmount='0', Status='Cancel' where billid='" + mPurchaseBillIdforEdit + "'";
                    mlistQuery.Add(mQuery);

                    //InsertOrUpdateTransection(transectionid, invoiceDate, billno, mTotalAmount.ToString("0.00"), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
                    InsertOrUpdateTransection(transectionid, invoiceDate, billno, "0", drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");

                    #region CurrentBalancerestore
                    //mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, mTotalAmount.ToString("0.00"), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(mpreviousLedgerid, drledgerid, mtotalPreviosInvoiceAmount.ToString(), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mlistQuery.Add(mQuery);
                    #endregion

                    if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
                    {
                        MessageBox.Show("Purchase Bill Canceled.", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error-> " + msg, "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            else
            {
                if (status == "Paid")
                {
                    MessageBox.Show("Sorry!! bill was already paid,So,you can't Cancel This Bill", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (IsStockEntry())
                {
                    MessageBox.Show("Sorry!! bill was already done in stock,So,you can't Cancel This Bill", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private bool IsStockEntry()
        {
            string qurey = "select PurchaseBillNo from StockHistory where PurchaseBillNo='" + lblNo.Text + "' ";
            object obj = SQLHelper.GetInstance().ExcuteScalar(qurey, out msg);
            if (obj.ISValidObject())
            {
                return true;
            }
            return false;
        }
        private void RetruveDataFromPurchaseBill()
        {
            mTemp = 1;
            string query = "Select PurchaseBill.*,purchasebill.LedgerId,ladgerMain.templatename from PurchaseBill" +
                "  inner join ladgerMain on LadgerMain.LadgerID=purchasebill.LedgerId where billid='" + mPurchaseBillIdforEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblNo.Text = dt.Rows[0]["BillNo"].ToString();
                mPreviousLedgername = dt.Rows[0]["templatename"].ToString();
                cmbSupplierName.Text = mPreviousLedgername;
                mpreviousLedgerid = dt.Rows[0]["LedgerId"].ToString();
                cmbSupplierName_Leave(null, null);
                dtpEntryDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
                txtInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                dtpBillingDate.Text = dt.Rows[0]["BillEntryDate"].ToString();
                txtOrderNo.Text = dt.Rows[0]["PurchaseOrderNo"].ToString();
                dtpOrderDate.Text = dt.Rows[0]["PurchaseOrderDate"].ToString();
                cmbDespatchThrough.Text = dt.Rows[0]["DispatchThrough"].ToString();
                txtVehicleNo.Text = dt.Rows[0]["VehicleNo"].ToString();
                cmbPaymentTerms.Text = dt.Rows[0]["TermsOfPayment"].ToString();
                dtpDueDate.Text = dt.Rows[0]["DueDate"].ToString();

                txtremarks.Text = dt.Rows[0]["Note"].ToString();
                txtPackingCharges.Text = dt.Rows[0]["PackingCharges"].ToString();
                txtfreightcharges.Text = dt.Rows[0]["FreightCharges"].ToString();
                txtOthersCharges.Text = dt.Rows[0]["OthersCharges"].ToString();
                txtDiscount.Text = dt.Rows[0]["OverallDiscount"].ToString();
                string Totalinvoiceamount = dt.Rows[0]["TotalAmount"].ToString();
                mtotalPreviosInvoiceAmount = Totalinvoiceamount.ISNullOrWhiteSpace() ? 0 : double.Parse(Totalinvoiceamount);
            }
            RetrivePurchaseBillDetails();
            GenerateTotal();
            SumOfCharges();
        }

        private void RetrivePurchaseBillDetails()
        {
            string query = "select * from purchasebilldetails where billid='" + mPurchaseBillIdforEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string itemId = item["ItemID"].ToString();
                    string hsnCode = item["HSNCode"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string quantity = item["Quantity"].ToString();
                    string unit = item["Unit"].ToString();
                    string rate = item["Rate"].ToString();
                    string amount = item["Amount"].ToString();
                    string disRate = item["DiscountRate"].ToString();
                    string disAmount = item["DiscountAmount"].ToString();
                    string taxAmount = item["TaxAmount"].ToString();
                    string CgstRate = item["CGSTRate"].ToString();
                    string CgstAmount = item["CGSTAmount"].ToString();
                    string sgstRate = item["SGSTRate"].ToString();
                    string sgstAmount = item["SGSTAmount"].ToString();
                    string igstRate = item["IGSTRate"].ToString();
                    string igstAmount = item["IGSTAmount"].ToString();
                    string cessRate = item["CessRate"].ToString();
                    string cessAmount = item["CeassAmount"].ToString();
                    string totalWithTax = item["Total"].ToString();

                    dgvItemList.Rows.Add(mDescriptionSlno, itemId, itemName, hsnCode, quantity, unit,
                                         rate.toRound(), amount.toRound(), disRate,
                                         disAmount.toRound(), taxAmount.toRound(), CgstRate,
                                         CgstAmount.toRound(), sgstRate, sgstAmount.toRound(), igstRate,
                                         igstAmount.toRound(), cessRate, cessAmount.toRound(),
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
        private void GenerateSerialNo()
        {
            string query = "Select max(SlNo) as slno from PurchaseBill ";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                try
                {
                    mSlNo = (int.Parse(obj.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblNo.Text = mSlNo.ToString();
        }

        private void GetSupplierDetails()
        {
            string query = "SELECT Ledgers.*,LadgerMain.GSTIN,GSTRegistrationType FROM LadgerMain " +
                           "inner join Ledgers on LadgerMain.LadgerID=Ledgers.LedgerID " +
                           "where LedgerID='" + mSupplierLadgerID + "' and Category='Supplier'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string supplierName = dt.Rows[0]["LedgerName"].ToString();
                string address = dt.Rows[0]["Address"].ToString();
                string town = dt.Rows[0]["City_Town"].ToString();
                string dist = dt.Rows[0]["Dist"].ToString();
                string state = dt.Rows[0]["State"].ToString();
                string statecode = StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString();

                string pin = dt.Rows[0]["PinCode"].ToString();
                string gsttype = dt.Rows[0]["GSTRegistrationType"].ToString();
                string gstin = dt.Rows[0]["GSTIN"].ToString();
                string bilingtermname = "";
                int day = BillingTermTools.GetBillingTermsAndDueDay(mSupplierLadgerID, out bilingtermname);
                cmbPaymentTerms.Text = bilingtermname;
                CalculateDueDate();
                mGstType = gsttype;
                if (mTemp==1)
                {
                    mpreviousGsttype = gsttype;
                }
                if (mGstType == "Regular")
                {
                    isregular = true;
                }
                if (ORG_Tools._StateCode != statecode)
                {
                    isigst = true;
                    if (mTemp == 1)
                    {
                        mPreviousIsIgst = true;
                    }
                }

                if (mGstType == "Unregister")
                {
                    mIsRcm = "Yes";
                }
                mTemp++;
            }
        }
        private void GetOrderDetails()
        {
            string query = "SELECT PurchaseOrderNo,convert(varchar(11),OrderDate,106) as OrderDate " +
                           ",LedgerID FROM PurchaseOrder where OrderId = '" + mOrderID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mSupplierLadgerID = dt.Rows[0]["LedgerID"].ToString();
                cmbSupplierName.Text = LedgerTools.LedgerNameById(mSupplierLadgerID);
                dtpOrderDate.Text = dt.Rows[0]["OrderDate"].ToString();
                txtOrderNo.Text = dt.Rows[0]["PurchaseOrderNo"].ToString();

                cmbSupplierName.Enabled = false;


            }
        }
        private void OrderItemDetails()
        {
            string query = "Select * from PurchaseOrderDetails  Where OrderId = '" + mOrderID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["ItemName"].ToString();
                    string itemID = item["ItemID"].ToString();
                    string hsnCode = item["ComodityCode"].ToString();
                    string unit = item["Unit"].ToString();
                    string dueqtystr = item["DueQty"].ToString();
                    mFixedOrderQty = item["Qty"].ToString();
                    string cgstrate = "";
                    string sgstrate = "";
                    string igstrate = "";
                    string cessrate = "";

                    double dueqty = 0;
                    double.TryParse(dueqtystr, out dueqty);
                    if (mGstType == "Regular")
                    {
                        isregular = true;
                    }
                    ItemTools.GetItemGSTRateIsiGst(itemID, isigst, out cgstrate, out sgstrate, out igstrate, out cessrate);
                    if (dueqty != 0)
                    {
                        dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, hsnCode, dueqtystr,
                                            unit, "", "", "", "", "", cgstrate, "", sgstrate, "", igstrate, ""
                                            , cessrate, "", "");
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
        }
        /// <summary>
        /// Grid view Design
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (OtherSettingTools._DecemalePlace == 0)
            {
                dgvItemList.Columns["RATE"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["TOTALAMOUNT"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["DiscountRate"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["DiscountAmount"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["TAXABLEVALUE"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["CGSTRate"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["CGSTAmount"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["SGSTRate"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["SGSTAmount"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["IGSTRate"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["IGSTAmount"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["CessRate"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["CESSAmount"].DefaultCellStyle.Format = "00";
                dgvItemList.Columns["TotalWithTax"].DefaultCellStyle.Format = "00";
            }
            else if (OtherSettingTools._DecemalePlace == 2)
            {
                dgvItemList.Columns["RATE"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["TOTALAMOUNT"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["DiscountRate"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["DiscountAmount"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["TAXABLEVALUE"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["CGSTRate"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["CGSTAmount"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["SGSTRate"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["SGSTAmount"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["IGSTRate"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["IGSTAmount"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["CessRate"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["CESSAmount"].DefaultCellStyle.Format = "0.00";
                dgvItemList.Columns["TotalWithTax"].DefaultCellStyle.Format = "0.00";
            }
            else
            {
                dgvItemList.Columns["RATE"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["TOTALAMOUNT"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["DiscountRate"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["DiscountAmount"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["TAXABLEVALUE"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["CGSTRate"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["CGSTAmount"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["SGSTRate"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["SGSTAmount"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["IGSTRate"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["IGSTAmount"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["CessRate"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["CESSAmount"].DefaultCellStyle.Format = "0.00##";
                dgvItemList.Columns["TotalWithTax"].DefaultCellStyle.Format = "0.00##";
            }
        }
        private void dgvitemList_Paint(object sender, PaintEventArgs e)
        {
            #region Assign Array
            string[] array = new string[4];
            int[] ary = new int[4];
            int length = 0;
            if (mGstType == "Regular")
            {
                if (isigst)
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
            if (mGstType == "Regular")
            {
                dgvItemList.Columns["CESSRATE"].Visible = true;
                dgvItemList.Columns["CESSAMOUNT"].Visible = true;
                if (isigst)
                {
                    dgvItemList.Columns["CGSTRATE"].Visible = false;
                    dgvItemList.Columns["CGSTAMOUNT"].Visible = false;
                    dgvItemList.Columns["SGSTRATE"].Visible = false;
                    dgvItemList.Columns["SGSTAMOUNT"].Visible = false;
                    dgvItemList.Columns["IGSTRATE"].Visible = true;
                    dgvItemList.Columns["IGSTAMOUNT"].Visible = true;
                    dgvItemList.Columns["TAXABLEVALUE"].Visible = true;


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
                    dgvItemList.Columns["TAXABLEVALUE"].Visible = true;

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
                dgvItemList.Columns["TAXABLEVALUE"].Visible = false;

                lblTotalCGST.Text = "";
                lblTotalSGST.Text = "";
                lblTotalIGST.Text = "";
                lblTotalCESS.Text = "";
            }
        }
        private void dgvitemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY" || dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "RATE" ||
                dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "DISCOUNTAMOUNT" || dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "DISCOUNTRATE")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress += new KeyPressEventHandler(ItemQuantity_KeyPress);
                }
            }
        }
        void ItemQuantity_KeyPress(object sender, KeyPressEventArgs e)
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
                if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY" && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }

            }
            else
            {
                double currentquantity = 0, dueqty = 0;
                double rate = 0d;
                double.TryParse((str + e.KeyChar), out currentquantity);
                double.TryParse((str + e.KeyChar), out rate);
                string itemid = dgvItemList.CurrentRow.Cells["itemid"].Value.ToString();
                string dueqtystr = GetPurchaseOrderDueQuantity(itemid);
                double.TryParse(dueqtystr, out dueqty);
                //if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY")
                //{
                //    if (currentquantity > dueqty || currentquantity <= 0)
                //        e.Handled = true;
                //}
                if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name != "QTY")
                {
                    if (rate <= 0)
                        e.Handled = true;
                }
            }
        }
        private string GetPurchaseOrderDueQuantity(string itemID)
        {
            string id = mOrderID;
            if (mOrderID.ISNullOrWhiteSpace())
            {
                id = mOrderIdFromChallan;
            }
            string query = "Select DueQty,Qty from PurchaseOrderDetails where OrderId='" + id + "' and Itemid='" + itemID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string dueqrty = dt.Rows[0]["DueQty"].ToString();
                mFixedOrderQty = dt.Rows[0]["Qty"].ToString();
                return dueqrty;
            }
            return null;
        }
        private void dgvItemList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && dgvItemList.Columns[e.ColumnIndex].Name == "QTY")
            {
                object quantityObj = dgvItemList.Rows[e.RowIndex].Cells["Qty"].Value;
                mOrderQuantity = quantityObj.ISValidObject() ? double.Parse(quantityObj.ToString()) : 0;
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
                //dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTAMOUNT"].Value = disAmount.ToString("0.00");
                dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTAMOUNT"].Value = disAmount.ToString();
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
                //dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTRATE"].Value = disRate.ToString("0.00");
                dgvItemList.Rows[e.RowIndex].Cells["DISCOUNTRATE"].Value = disRate.ToString();
                int rowIndex = dgvItemList.CurrentCell.RowIndex;
                RegenerateItemDiscountAndTax(rowIndex);
            }
        }
        private void RegenerateItemDiscountAndTax(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                object qtyObj = dgvItemList.Rows[rowIndex].Cells["Qty"].Value;
                object rateObj = dgvItemList.Rows[rowIndex].Cells["Rate"].Value;
                object disAmountObj = dgvItemList.Rows[rowIndex].Cells["DISCOUNTAMOUNT"].Value;
                double qty = 0;
                double rate = 0d;
                double disAmount = 0d;
                double amount = 0d, taxAmount = 0d;
                double totalDiscount = 0d;
                try
                {
                    qty = qtyObj.ISValidObject() ? double.Parse(qtyObj.ToString()) : 0;
                    rate = rateObj.ISValidObject() ? double.Parse(rateObj.ToString()) : 0d;
                    disAmount = disAmountObj.ISValidObject() ? double.Parse(disAmountObj.ToString()) : 0d;
                }
                catch (Exception) { }
                totalDiscount = qty * disAmount;
                amount = qty * rate;
                taxAmount = (qty * rate) - totalDiscount;
                GenerateTaxAmounts(rowIndex, amount, taxAmount);
                GenerateTotal();
                SumOfCharges();
            }
        }
        private void GenerateTaxAmounts(int rowindex, double amount, double taxAmount)
        {
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
            string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
            string totalWithTax = "";
            string itemID = dgvItemList.Rows[rowindex].Cells["ItemID"].Value.ToString();
            object rateObj = dgvItemList.Rows[rowindex].Cells["Rate"].Value;

            ItemTools.GetItemGSTRateAndAmountForPurchase(itemID, isigst, isregular, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                             out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

            // dgvItemList.Rows[rowindex].Cells["TotalAmount"].Value = amount.ToString("0.00");
            // dgvItemList.Rows[rowindex].Cells["TAXABLEVALUE"].Value = taxAmount.ToString("0.00");
            dgvItemList.Rows[rowindex].Cells["TotalAmount"].Value = amount.ToString();
            dgvItemList.Rows[rowindex].Cells["TAXABLEVALUE"].Value = taxAmount.ToString();
            dgvItemList.Rows[rowindex].Cells["CgstAmount"].Value = cgstAmount;
            dgvItemList.Rows[rowindex].Cells["SgstAmount"].Value = sgstAmount;
            dgvItemList.Rows[rowindex].Cells["CESSAmount"].Value = cessAmount;
            dgvItemList.Rows[rowindex].Cells["IgstAmount"].Value = igstAmount;
            dgvItemList.Rows[rowindex].Cells["TotalWithTax"].Value = totalWithTax;
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
            lblTotalCGST.Text = mTotalCGST.toString();
            lblTotalIGST.Text = mTotalIGST.toString();
            lblTotalSGST.Text = mTotalSGST.toString();
            lblTotalCESS.Text = mTotalCESS.toString();
            lblTotalWithTax.Text = mTotalWithTax.toString();
            mtotalInvoiceAmount = mTotalWithTax;
            mtotalInvoiceAmount = Math.Round(mtotalInvoiceAmount);
            lblTotalChallanAmount.Text = mtotalInvoiceAmount.toString();
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
        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
            int index = cmbSupplierName.FindStringExact(cmbSupplierName.Text);
            if (index >= 0)
            {
                cmbSupplierName.SelectedIndex = index;
                mSupplierLadgerID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
                GetSupplierDetails();
                GenerateGridForNonGSTType();
            }
            else
            {
                cmbSupplierName.Text = "";
            }
        }
        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtRate.Clear();
            //txtQuantity.Text = "";
            //txtDiscountRate.Clear();
            //txtDiscountAmount.Clear();
            //txtAmount.Clear();
            //if (!cmbItemName.Text.ISNullOrWhiteSpace())
            //{
            //    string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();

            //    txtRate.Text = ItemTools.GetItemSalesRate(itemid).ToString("0.00");
            //    cmbUnit.Text = ItemTools.GetUnitName(itemid);
            //}
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
            cmbUnit.AddUnit();
            cmbItemName.Text = itemname;
            cmbItemName_Leave(null, null);
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
        private void CalculateAmount()
        {
            double qty = 0;
            double rate = 0d;
            double disAmount = 0d;
            double amount = 0d;
            double totalDiscount = 0d;
            try
            {
                double.TryParse(txtRate.Text, out rate);
                double.TryParse(txtQuantity.Text, out qty);
                double.TryParse(txtDiscountAmount.Text, out disAmount);
            }
            catch (Exception) { }
            totalDiscount = qty * disAmount;
            amount = (qty * rate) - totalDiscount;

            txtAmount.Text = amount.toString();
        }
        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtDiscountRate.Text.Contains('.'))
                {
                    e.Handled = true;
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
                        txtDiscountRate.Text = disRate.ToString();
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
            if (!txtRate.Text.ISNullOrWhiteSpace() && double.Parse(txtRate.Text) > 0)
            {
                if (!txtDiscountAmount.Text.ISNullOrWhiteSpace())
                {
                    double.TryParse(txtRate.Text, out rate);
                    double.TryParse(txtDiscountAmount.Text, out disAmount);
                    disRate = (disAmount / rate) * 100;
                    txtDiscountRate.Text = disRate.ToString();
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
        private bool IsValidData()
        {
            if (cmbSupplierName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select supplier.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSupplierName.Select();
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
                MessageBox.Show("Enter quantity.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtQuantity.Focus();
                return false;
            }
            else
            {
                if (double.Parse(txtQuantity.Text) <= 0)
                {
                    MessageBox.Show("Enter valid quantity.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtQuantity.Focus();
                    return false;
                }
            }
            if (txtRate.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter rate.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRate.Focus();
                return false;
            }
            return true;
        }
        private void DescriptionAdd()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string hsnCode = ItemTools.GetItemHSNCode(itemID);
            string unit = cmbUnit.Text;
            string qtyStr = txtQuantity.Text;
            double qty = (qtyStr.ISNullOrWhiteSpace() ? 0 : double.Parse(qtyStr));
            string rateStr = txtRate.Text;
            double rate = double.Parse(rateStr);
            double discountRate = txtDiscountRate.Text.ISNullOrWhiteSpace() ? 0d : double.Parse(txtDiscountRate.Text);
            double discountAmount = 0d;
            double amount = (rate * qty).toRound();
            string disAmountSrt = txtDiscountAmount.Text;
            discountAmount = (disAmountSrt.ISNullOrWhiteSpace() ? 0d : double.Parse(disAmountSrt)).toRound();
            string amountWithDiscount = txtAmount.Text;
            double taxAmount = (amountWithDiscount.ISNullOrWhiteSpace() ? 0d : double.Parse(amountWithDiscount)).toRound();
            string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
            string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
            string totalWithTax = "";
            if (mGstType == "Regular")
            {
                isregular = true;
            }
            ItemTools.GetItemGSTRateAndAmountForPurchase(itemID, isigst, isregular, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                          out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

            dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, hsnCode, qty, unit, rate.toString(), amount.toString(), discountRate,
                                  discountAmount.toString(), taxAmount.toString(), cgstRate,
                                  cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount, cessRate, cessAmount, totalWithTax);

            DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
            btnCelCol.ToolTipText = "Delete";
            btnCelCol.Value = "Delete";
            btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
            dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
            mDescriptionSlno++;
        }
        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            txtRate.Clear();
            txtQuantity.Text = "";
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            txtAmount.Clear();
            cmbUnit.SelectedIndex = -1;
            int index = cmbItemName.FindStringExact(cmbItemName.Text);
            if (index >= 0)
            {
                cmbItemName.SelectedIndex = index;
                string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                //txtRate.Text = ItemTools.GetItemSalesRate(itemid).ToString("0.00");
                txtRate.Text = ItemTools.GetItemSalesRate(itemid).ToString();
                cmbUnit.Text = ItemTools.GetUnitName(itemid);
            }
            else
            {
                cmbItemName.Text = "";
            }
        }
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog);
            frm.OnClose += Frm_OnClose;
            frm.ShowDialog();
        }
        private void Frm_OnClose(string supplier)
        {
            cmbSupplierName.AddSuppliers();
            cmbSupplierName.Text = supplier;
            cmbSupplierName_Leave(cmbSupplierName, null);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.C))
            {
                btnNewSupplier_Click(btnNewSupplier, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.C))
            {
                if (!cmbSupplierName.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        string supplierID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
                        LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog, supplierID);
                        frm.OnClose += Frm_OnClose;
                        frm.ShowDialog();
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("Select supplier name.", "Supplier Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                //if (!IsDuplicateItemSelect())
                //{
                DescriptionAdd();
                GenerateTotal();
                cmbItemName.SelectedIndex = -1;
                txtQuantity.Text = "";
                cmbUnit.SelectedIndex = -1;
                txtRate.Clear();
                txtAmount.Clear();
                txtDiscountAmount.Clear();
                txtDiscountRate.Clear();
                cmbItemName.Select();
                //}
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            mTotalOrderAmount = 0f;
            lblTotalChallanAmount.Text = mTotalOrderAmount.toString();
            //lblTotalChallanAmount.Text = mTotalOrderAmount.ToString("0.00");

            cmbItemName.SelectedIndex = -1;
            txtQuantity.Text = "";
            txtDiscountRate.Clear();
            txtDiscountAmount.Clear();
            cmbUnit.SelectedIndex = -1;
            txtRate.Clear();
            lblTotQuantity.Text = "----";
            lblTotAmount.Text = "----";
            lblTotalWithTax.Text = "----";
            lblTotalChallanAmount.Text = "----";
            lblTotalIGST.Text = "----";
            lblTotalDiscount.Text = "----";
            lblTotalCESS.Text = "----";
            lblTaxableAmountTotal.Text = "----";
            lblTotalCGST.Text = "----";
            lblTotalSGST.Text = "----";
            txtAmount.Clear();
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
        private void dtpBillingDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateDueDate();
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
        private void cmbSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void lblTotalWithTax_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void SumOfCharges()
        {
            double totalamount = 0d, freightamount = 0d, packingamount = 0d, othersamount = 0d, invoiceamount = 0d, overallDiscount = 0d;
            double.TryParse(lblTotalWithTax.Text, out totalamount);
            double.TryParse(txtfreightcharges.Text, out freightamount);
            double.TryParse(txtOthersCharges.Text, out othersamount);
            double.TryParse(txtPackingCharges.Text, out packingamount);
            double.TryParse(txtDiscount.Text, out overallDiscount);
            invoiceamount = (totalamount + freightamount + packingamount + othersamount) - overallDiscount;
            mtotalInvoiceAmount = invoiceamount;
            mtotalInvoiceAmount = Math.Round(mtotalInvoiceAmount);
            lblTotalChallanAmount.Text = mtotalInvoiceAmount.toString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
                if (!IsDuplicate())
                {
                    if (!IsMoreThanPaid())
                    {
                        if (MessageBox.Show("Are you sure you want to save ?", "Purchase Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SaveBill();
                        }
                    }
                }
            }
        }
        private bool IsMoreThanPaid()
        {
            double totalinvoicevalue = 0d, paymentamount = 0d;
            double.TryParse(lblTotalChallanAmount.Text, out totalinvoicevalue);

            string query = "select sum(Amount) as amount from PaymentHistory where Type='Purchase Bill' and  VchNo='" + lblNo.Text + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                double.TryParse(obj.ToString(), out paymentamount);
                if (totalinvoicevalue < paymentamount)
                {
                    MessageBox.Show("Sorry are already paid the maximaum amount from the invoice value.", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            mdueamount = totalinvoicevalue - paymentamount;
            return false;
        }
        private bool IsvalidEntry()
        {
            if (cmbSupplierName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Enter supplier name.", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSupplierName.Focus();
                return false;
            }
            if (!dtpEntryDate.Value.IsValidDate())
            {
                dtpEntryDate.Focus();
                return false;
            }
            if (txtInvoiceNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Enter Invoice no.", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtInvoiceNo.Focus();
                return false;
            }
            if (cmbPaymentTerms.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Select Payment term.", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentTerms.Select();
                return false;
            }
            if (!mPurchaseBillIdforEdit.ISNullOrWhiteSpace())
            {
                if (mGstType != mpreviousGsttype)
                {
                    MessageBox.Show("Sorry..previous Supplier Gst type and present Supplier Gst type mismatch.\n present :- '" + mGstType + "' and Previous :- '" + mpreviousGsttype + "'", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbSupplierName.Text = mPreviousLedgername;
                    cmbSupplierName_Leave(null,null);
                    return false;
                }
                if (isigst != mPreviousIsIgst)
                {
                    MessageBox.Show("Sorry..previous Supplier state and present Supplier state mismatch.", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbSupplierName.Text = mPreviousLedgername;
                    cmbSupplierName_Leave(null, null);
                    return false;
                } 
            }
            return true;
        }
        private bool IsDuplicate()
        {
            string query = "";
            if (mPurchaseBillIdforEdit.ISNullOrWhiteSpace())
            {
                query = "select InvoiceNo from PurchaseBill where InvoiceNo='" + txtInvoiceNo.Text.GetDBFormatString() + "' and LedgerId='" + mSupplierLadgerID + "' and status!='Cancel' ";
            }
            else
            {
                query = "select InvoiceNo from PurchaseBill where InvoiceNo='" + txtInvoiceNo.Text.GetDBFormatString() + "' and LedgerId='" + mSupplierLadgerID + "' and billid<>'" + mPurchaseBillIdforEdit + "' and  status!='Cancel'";
            }
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                MessageBox.Show("This Invoice's bill alredy done ", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtInvoiceNo.Focus();
                return true;
            }
            return false;
        }
        private void SaveBill()
        {
            mlistQuery.Clear();
            #region data
            string billid = Guid.NewGuid().ToString();
            string transectionid = Guid.NewGuid().ToString();
            string billno = lblNo.Text.GetDBFormatString();
            string invoiceNo = "NULL";
            string purchaseLedgerId = AccountHeadTools._PurchaseLedgerId;
            string invoiceDate = dtpEntryDate.Text;
            string dueDate = dtpDueDate.Text;

            string buyerOrderNo = txtOrderNo.Text.GetDBFormatString();
            string buyerOrderDate = dtpOrderDate.Text;
            string challanNo = "NULL";
            string challanDate = mChallanDate;
            string despatchThrough = cmbDespatchThrough.Text.GetDBFormatString();
            string note = "NULL";
            string supplierRefNo = "NULL";
            string billingDate = dtpBillingDate.Text;
            string termsOfPayment = cmbPaymentTerms.Text;
            string vehicleNo = txtVehicleNo.Text.GetDBFormatString();
            string freightcharge = txtfreightcharges.Text.ISNullOrWhiteSpace() ? "NULL" : txtfreightcharges.Text.GetDBFormatString();
            string packcharges = txtPackingCharges.Text.ISNullOrWhiteSpace() ? "NULL" : txtPackingCharges.Text.GetDBFormatString();
            string othercharges = txtOthersCharges.Text.ISNullOrWhiteSpace() ? "NULL" : txtOthersCharges.Text.GetDBFormatString();
            string overalldiscount = txtDiscount.Text.ISNullOrWhiteSpace() ? "NULL" : txtDiscount.Text.GetDBFormatString();

            if (!txtremarks.Text.ISNullOrWhiteSpace())
            {
                note = "'" + txtremarks.Text.GetDBFormatString() + "'";
            }
            if (!txtInvoiceNo.Text.ISNullOrWhiteSpace())
            {
                invoiceNo = "'" + txtInvoiceNo.Text.GetDBFormatString() + "'";
            }
            string transectiontype = "Purchase_Bill";
            string drledgerid = purchaseLedgerId;
            string crledgerid = mSupplierLadgerID;
            mTotalAmount = double.Parse(lblTotalChallanAmount.Text);
            #endregion
            if (mPurchaseBillIdforEdit.ISNullOrWhiteSpace())
            {
                mQuery = "Insert into PurchaseBill(SlNo, BillID, BillNo, InvoiceNo, InvoiceDate,BillEntryDate, LedgerId, " +
                        "DueDate, PurchaseOrderNo, PurchaseOrderDate, ChallanNo, ChallanDate, DispatchThrough, " +
                        "TotalAmount, Note, SupplierRefNo, TermsOfPayment, RCM, VehicleNo, FreightCharges, " +
                        "PackingCharges, OthersCharges, OverallDiscount, DueAmount, Status,Billtype) " +
                        "values(" + mSlNo + ",'" + billid + "','" + billno + "'," + invoiceNo + ",'" +
                        invoiceDate + "','" + billingDate + "','" + mSupplierLadgerID + "','" + dueDate + "','" +
                        buyerOrderNo + "','" + buyerOrderDate + "'," + challanNo + ",'" + challanDate + "','" +
                        despatchThrough + "'," + mTotalAmount + "," + note + "," + supplierRefNo + ",'" +
                        termsOfPayment + "','" + mIsRcm + "','" + vehicleNo + "'," + freightcharge + "," +
                        packcharges + "," + othercharges + "," + overalldiscount + "," + mdueamount + ",'Due','" + mbilltype + "')";
                mlistQuery.Add(mQuery);
                //InsertOrUpdateTransection(transectionid, invoiceDate, billno, mTotalAmount.ToString("0.00"), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
                InsertOrUpdateTransection(transectionid, invoiceDate, billno, mTotalAmount.ToString(), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
            }
            else
            {
                string status = mdueamount == 0 ? "Paid" : "Due";
                mQuery = "Update PurchaseBill set  InvoiceNo=" + invoiceNo + ",InvoiceDate='" + invoiceDate + "',BillEntryDate='" + billingDate + "', LedgerId='" + mSupplierLadgerID + "', " +
                        "DueDate='" + dueDate + "', PurchaseOrderNo='" + buyerOrderNo + "', PurchaseOrderDate='" + buyerOrderDate + "', ChallanNo=" + challanNo + ", ChallanDate='" + challanDate + "', DispatchThrough='" + despatchThrough + "', " +
                        "TotalAmount=" + mTotalAmount + ", Note=" + note + ", SupplierRefNo=" + supplierRefNo + ", TermsOfPayment='" + termsOfPayment + "', RCM='" + mIsRcm + "', VehicleNo='" + vehicleNo + "', FreightCharges=" + freightcharge + ", " +
                        "PackingCharges=" + packcharges + ", OthersCharges=" + othercharges + ", OverallDiscount=" + overalldiscount + ", DueAmount=" + mdueamount + ", Status='" + status + "' where billid='" + mPurchaseBillIdforEdit + "'";
                mlistQuery.Add(mQuery);
                mQuery = "delete from PurchaseBillDetails where billid='" + mPurchaseBillIdforEdit + "' ";
                mlistQuery.Add(mQuery);
                billid = mPurchaseBillIdforEdit;

                //InsertOrUpdateTransection(transectionid, invoiceDate, billno, mTotalAmount.ToString("0.00"), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");
                InsertOrUpdateTransection(transectionid, invoiceDate, billno, mtotalInvoiceAmount.ToString(), drledgerid, crledgerid, transectiontype, "NULL", "NULL", "NULL", "NULL");

                #region CurrentBalancerestore
                mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(mpreviousLedgerid, drledgerid, mtotalPreviosInvoiceAmount.ToString(), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mlistQuery.Add(mQuery);
                #endregion
            }
            #region CurrentBalanceUpdate

            //mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, mTotalAmount.ToString("0.00"), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
            mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, mTotalAmount.ToString(), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
            mlistQuery.Add(mQuery);
            #endregion

            SaveBillDetails(billid);
            if (IsBillissu == 1)
            {
                if (!mOrderID.ISNullOrWhiteSpace())
                {
                    ChallanSave();
                }
                if (!mOrderID.ISNullOrWhiteSpace() || !mChallanID.ISNullOrWhiteSpace())
                {
                    UpdatePurchaseOrderStatus();
                }
                if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
                {
                    mIsSuccess = true;
                    if (MessageBox.Show("Data Saved. Do you want to update stock now ?", "Purchase Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        StockEntry frmStockEntry = new StockEntry(billno);
                        //frmStockEntry.OnClose += FrmStockEntry_OnClose;
                        frmStockEntry.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg, "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Item not found in below list.\n Give Quantity or Rate ", "Purchase Bill", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void FrmStockEntry_OnClose()
        {
            this.Close();
        }
        private void InsertOrUpdateTransection(string tranid, string billDate, string billNo, string totalinvoicevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate)
        {
            string transectionid = tranid;
            if (mPurchaseBillIdforEdit.ISNullOrWhiteSpace())
            {
                mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                            "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                            billDate + "','" + billNo + "','" + transectiontype + "','" + drledgerid + "','" +
                            crledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                            + "," + ChequeNo + "," + ChequeDate + ")";
                mlistQuery.Add(mQuery);
                transectionid = Guid.NewGuid().ToString();
                mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        billDate + "','" + billNo + "','" + transectiontype + "','" + crledgerid + "','" +
                        drledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mlistQuery.Add(mQuery);
            }

            else
            {
                TransectionTools.GetTransectionId(billNo, transectiontype);

                mQuery = "Update Transection Set Date='" + billDate + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mlistQuery.Add(mQuery);

                mQuery = "Update Transection Set Date='" + billDate + "',LedgerIdFrom='" + crledgerid + "', " +
                       "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mlistQuery.Add(mQuery);

            }

        }
        private void SaveBillDetails(string billid)
        {
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemId = row.Cells["ItemId"].Value.ToString();
                object hsnCodeobj = row.Cells["ParticularsHsnCode"].Value;
                string hsnCode = hsnCodeobj.ISValidObject() ? row.Cells["ParticularsHsnCode"].Value.ToString() : "";
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

                object netAmountStr = row.Cells["TotalWithTax"].Value;
                string netAmount = !netAmountStr.ISValidObject() ? "NULL" : netAmountStr.ToString();
                if (amountStr != "")
                {
                    IsBillissu = 1;
                    string query = "Insert into PurchaseBillDetails(Billid, ItemID, HSNCode, ItemName, Quantity,Unit, Rate, Amount, " +
                               "DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, IGSTRate, " +
                               "IGSTAmount, CessRate, CeassAmount,Total) values('" + billid + "'," + itemId + ",'" + hsnCode + "','" +
                               itemName + "'," + quantity + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount
                               + "," + taxAmount + "," + cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," +
                               igstRate + "," + igstAmount + "," + cessRate + "," + cessAmount + "," + netAmount + ")";
                    mlistQuery.Add(query);
                }
                else
                {
                    IsBillissu = 0;
                    break;
                }
            }
        }
        private void ChallanSave()
        {
            //#region Data
            //string challanid = Guid.NewGuid().ToString();
            //string slNo = GenerateChallanSlNo();
            //string challanno = txtChallanNo.Text.GetDBFormatString();
            //string orderno = mPurchaseOrderNo;
            //string orderid = mOrderID;
            //string challanDate = dtpBillingDate.Text;
            //string ledgerID = mSupplierLadgerID;
            //string discountamount = txtDiscount.Text.GetDBFormatString();
            //double disamount = 0d;
            //double.TryParse(discountamount, out disamount);
            //string totalChalanAmount = (mtotalInvoiceAmount + disamount).ToString();
            //string description = "NULL";
            //string despatchthrough = cmbDespatchThrough.Text;
            //string despatchdate = dtpEntryDate.Text;
            //string vehicleNo = txtVehicleNo.Text.GetDBFormatString();
            //string freightcharge = txtfreightcharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtfreightcharges.Text.GetDBFormatString() + "'";
            //string packingcharge = txtPackingCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtPackingCharges.Text.GetDBFormatString() + "'";
            //string othercharge = txtOthersCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtOthersCharges.Text.GetDBFormatString() + "'";

            //if (!txtremarks.Text.ISNullOrWhiteSpace())
            //{
            //    description = "'" + txtremarks.Text.GetDBFormatString() + "'";
            //}
            //#endregion

            //#region Query
            //mQuery = "Insert into PurchaseChallan(ChallanID, SlNo, SupplyChallanNo, OrderNo, OrderId, Date, " +
            //        "LedgerID,  TotalAmount, Description,Status,DespatchThrough,DespatchDate,EditabilStatus,FreightCharges,PackingCharges,OtherCharges,VehicleNo) Values('" + challanid + "'," + slNo + ",'" + challanno + "','" + orderno + "','" + orderid + "','" + challanDate + "','" + ledgerID + "'," +
            //        "" + totalChalanAmount + "," + description + ",'Close','" + despatchthrough + "','" + despatchdate + "','Close'," + freightcharge + "," + packingcharge + "," + othercharge + ",'" + vehicleNo + "')";
            //mlistQuery.Add(mQuery);
            //ChallanDetailsAndPurchasesorderDetailsDatasave(challanid);

            //#endregion
        }
        private string GenerateChallanSlNo()
        {
            int slno = 1;
            string query = "Select max(SlNo) as slno from PurchaseChallan ";
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
            return slno.ToString();
        }
        private void ChallanDetailsAndPurchasesorderDetailsDatasave(string challanid)
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

                string dueqtystr = GetPurchaseOrderDueQuantity(itemId);
                double dueqty = 0, supplyqty = 0;
                double.TryParse(quantity, out supplyqty);
                double.TryParse(dueqtystr, out dueqty);
                dueqty = dueqty - supplyqty;

                #endregion
                #region INSERT
                if (!amountStr.ISNullOrWhiteSpace())
                {
                    if (isregular)
                    {
                        if (isigst)
                        {
                            mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                    "Amount, DiscountRate, DiscountAmount, TaxAmount, " +
                                    "IGSTRate, IGSTAmount, CessRate,CeassAmount,Total,OrderQuantity)" +
                                    "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                    + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," + igstRate + "," +
                                    igstAmount + "," + cessRate + "," + cessAmount + "," + total + "," + mFixedOrderQty + ")";
                        }
                        else
                        {
                            mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                    "Amount, DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, " +
                                    "CessRate,CeassAmount, Total,OrderQuantity)" +
                                    "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                    + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," +
                                    cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," + cessRate + "," +
                                    cessAmount + "," + total + "," + mFixedOrderQty + ")";
                        }
                    }
                    else
                    {
                        mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, Total,OrderQuantity)" +
                                "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," +
                                taxAmount + "," + total + "," + mFixedOrderQty + ")";
                    }
                    mlistQuery.Add(mQuery);
                    mQuery = "update PurchaseOrderDetails set ReceivedQty=" + supplyqty + ",DueQty=" + dueqty + " where OrderId='" + mOrderID + "' and ItemID='" + itemId + "'";
                    mlistQuery.Add(mQuery);
                }
                #endregion
            }
            #endregion

        }
        private void UpdatePurchaseOrderStatus()
        {
            string id = "";
            if (!mOrderID.ISNullOrWhiteSpace())
            {
                id = mOrderID;
            }
            else if (!mChallanID.ISNullOrWhiteSpace())
            {
                id = mOrderIdFromChallan;
            }
            mQuery = "Update PurchaseOrder set Status='Close',StatusForchallan='Close' where  OrderId='" + id + "'";
            mlistQuery.Add(mQuery);
            mQuery = "Update PurchaseChallan set Status='Close',EditabilStatus='Close' where  OrderId='" + id + "'";
            mlistQuery.Add(mQuery);
        }
        private void PurchaseChallanDataRetrive()
        {
            string query = "select SUM(FreightCharges) as FreightCharges,SUM(TotalAmount) as TotalAmount,sum(PackingCharges) as PackingCharges,SUM(OtherCharges) as OtherCharges  from PurchaseChallan where OrderId='" + mOrderIdFromChallan + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                object frightchrg = dt.Rows[0]["FreightCharges"];
                object packingcharge = dt.Rows[0]["PackingCharges"];
                object otherscharge = dt.Rows[0]["OtherCharges"];
                object totalamount = dt.Rows[0]["TotalAmount"];
                txtfreightcharges.Text = frightchrg.ISValidObject() ? frightchrg.ToString() : "";
                txtPackingCharges.Text = packingcharge.ISValidObject() ? packingcharge.ToString() : "";
                txtOthersCharges.Text = otherscharge.ISValidObject() ? otherscharge.ToString() : "";
                lblTotalChallanAmount.Text = totalamount.ISValidObject() ? totalamount.ToString() : "";
            }

            PurchaseChallanDetailsDataRetrive();
        }
        private void PurchaseChallanDetailsDataRetrive()
        {

            dgvItemList.Rows.Clear();
            string query = "Select PurchaseChallanDetails.DiscountAmount,DiscountRate,ItemName,ItemId,ComodityCode,Unit,Rate,sum(Qty) as Qty,SUM(TaxAmount) as TaxAmount," +
                " sum(Amount)as Amount from PurchaseChallanDetails INNER JOIN PurchaseChallan on PurchaseChallanDetails.ChallanID=PurchaseChallan.ChallanID Where OrderId = '" + mOrderIdFromChallan +
                "' group by ItemName,ItemId,ComodityCode,Unit,Rate,DiscountRate,DiscountAmount";
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
                    // string disRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString()).ToString("0.00") : "";
                    string disRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString()).ToString() : "";
                    object disAmountStr = item["DiscountAmount"];
                    // string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString()).ToString("0.00") : "";
                    string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString()).ToString() : "";

                    string taxAmountStr = item["TaxAmount"].ToString();
                    // string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString()).ToString("0.00") : "";
                    string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString()).ToString() : "";
                    double taxamount;
                    double.TryParse(taxAmount, out taxamount);

                    double rate = rateStr.ISNullOrWhiteSpace() ? 0d : double.Parse(rateStr);
                    double amount = amountStr.ISNullOrWhiteSpace() ? 0d : double.Parse(amountStr);
                    string cgstAmount = "", sgstAmount = "", igstAmount = "", cessAmount = "";
                    string cgstRate = "", sgstRate = "", igstRate = "", cessRate = "";
                    string totalWithTax = "";

                    if (mGstType == "Regular")
                    {
                        isregular = true;
                    }
                    ItemTools.GetItemGSTRateAndAmountForPurchase(itemID, isigst, isregular, taxamount, out cgstRate, out cgstAmount, out sgstRate,
                                             out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

                    //dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, comodityCode, qtyStr, unit,
                    //                     rate.ToString("0.00"), amount.ToString("0.00"), disRate, disAmount, taxAmount
                    //                     , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                    //                     , cessRate, cessAmount, totalWithTax);

                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, comodityCode, qtyStr, unit,
                                         rate.ToString(), amount.ToString(), disRate, disAmount, taxAmount
                                         , cgstRate, cgstAmount, sgstRate, sgstAmount, igstRate, igstAmount
                                         , cessRate, cessAmount, totalWithTax);

                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                    dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
                }
                GenerateTotal();
                SumOfCharges();
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
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PurchaseBillEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess)
            {
                OnClose();
            }
        }
        private void btnBillinterm_Click(object sender, EventArgs e)
        {
            BillingTermEntry billingtermentry = new BillingTermEntry();
            billingtermentry.OnClose += Billingtermentry_onClose;
            billingtermentry.ShowDialog();
        }
        private void Billingtermentry_onClose(string billingtermname)
        {
            cmbPaymentTerms.AddBillingTerms();
            cmbPaymentTerms.Text = billingtermname;
        }
        private void CalculateDueDate()
        {
            if (!cmbPaymentTerms.Text.ISNullOrWhiteSpace())
            {
                try
                {
                    string billingid = ((KeyValuePair<string, string>)cmbPaymentTerms.SelectedItem).Key.ToString();
                    string billingdays = GetBillingtermDays(billingid);
                    int days = 0;
                    int.TryParse(billingdays, out days);
                    DateTime invoicdate = dtpBillingDate.Value;
                    DateTime duedate = invoicdate.AddDays(+days);
                    dtpDueDate.Value = duedate;
                }
                catch (Exception) { }
            }
        }
        private void cmbPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateDueDate();

        }
        private string GetBillingtermDays(string billingid)
        {
            string query = "select Days from BillingTerms where id='" + billingid + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        private void dtmInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateDueDate();
        }
    }
}
