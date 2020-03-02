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
    public partial class PurchaseChallanEntry : Form
    {
        #region Decleration
        public event Action OnClose;
        private long   mTotalQuantity;
        private double mOrderRate = 0d, mtotalchallanAmount = 0f,
                       mTotalOrderAmount, mTotalAmount, mTotalDiscount,
                       mTotalCGST, mTotalSGST, mTotalIGST, mTotalCESS,
                       mTaxableAmount, mTotalWithTax;
        private int    mOrderQuantity = 0,Ischallanissu = 0,mDescriptionSlno = 1;
        private bool   isigst = false,isregular = false;
        private string msg = "", mGsttype = "", mOrderidFromChallan = "", status = "Close",mfixedorderqty = "",
                       mChallanidForEdit = "",mOrderID = "",mQuery = "";

        private List<string> mLstQuery = new List<string>();
        DataTable mdt = new DataTable();//for restor
        #endregion
        public PurchaseChallanEntry()
        {
            InitializeComponent();
            this.FitToVertical();
            GenerateSlNo();

        }
        public PurchaseChallanEntry(string chalanid, string status)
        {
            InitializeComponent();
            this.FitToVertical();
            mChallanidForEdit = chalanid;
            DataTableColumnCreate();
            ViewExistingDataFromPurchaseChallan();
            ReadOnlyAllControl(status);
            GenerateGridForNonGSTType();

        }
        public PurchaseChallanEntry(string orderid)
        {
            InitializeComponent();
            this.FitToVertical();
            GenerateSlNo();
            mOrderID = orderid;
            PurchaseOrderDataRetrive();
            GridDesign();
            GenerateGridForNonGSTType();
        }
        #region Order Entry
        private void GenerateSlNo()
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
            lblSlNo.Text = slno.ToString();
        }
        private void PurchaseOrderDataRetrive()
        {
            string query = "Select LadgerMain.TemplateName,GSTRegistrationType,PurchaseOrder.PurchaseOrderNo, ledgers.State from LadgerMain" +
                " inner join ledgers on ledgers.ledgerid=LadgerMain.LadgerID" +
                " inner join PurchaseOrder  on PurchaseOrder.LedgerID=LadgerMain.LadgerID  where OrderId='" + mOrderID + "' ";
            DataTable DT = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (DT.IsValidDataTable())
            {
                lblOrderNo.Text = DT.Rows[0]["PurchaseOrderNo"].ToString();
                lblSuppliername.Text = DT.Rows[0]["TemplateName"].ToString();
                string state = DT.Rows[0]["State"].ToString();

                string statecode = StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString();
                mGsttype = DT.Rows[0]["GSTRegistrationType"].ToString();

                if (ORG_Tools._StateCode != statecode)
                {
                    isigst = true;
                }
                PurchaseoOrderDetailsDataretrive();
            }
        }
        private void PurchaseoOrderDetailsDataretrive()
        {
            string query2 = "Select * from PurchaseOrderDetails  Where OrderId = '" + mOrderID + "'";
            DataTable DT2 = SQLHelper.GetInstance().ExcuteNonQuery(query2, out msg);
            if (DT2.IsValidDataTable())
            {
                int i = 0;
                foreach (DataRow item in DT2.Rows)
                {
                    string itemName = item["ItemName"].ToString();
                    string itemID = item["ItemID"].ToString();
                    string hsnCode = item["ComodityCode"].ToString();
                    string unit = item["Unit"].ToString();
                    string dueqtystr = item["DueQty"].ToString();
                    mfixedorderqty = item["Qty"].ToString();
                    string cgstrate = "";
                    string sgstrate = "";
                    string igstrate = "";
                    string cessrate = "";

                    int dueqty = 0;
                    int.TryParse(dueqtystr, out dueqty);
                    if (mGsttype == "Regular")
                    {
                        isregular = true;
                    }
                        ItemTools.GetItemGSTRateIsiGst(itemID, isigst, out cgstrate, out sgstrate, out igstrate, out cessrate);

                    if (dueqty != 0)
                    {
                        dgvItemList.Rows.Add(mDescriptionSlno, itemID, itemName, hsnCode, dueqtystr, unit, "", "", "", "", "", cgstrate, "", sgstrate, "", igstrate, ""
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
        /// Grid Design and Column Enabled
        /// </summary>
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
            if (mGsttype == "Regular")
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
            if (mGsttype == "Regular")
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
                int currentquantity = 0, dueqty = 0;
                double rate = 0d;
                int.TryParse((str + e.KeyChar), out currentquantity);
                double.TryParse((str + e.KeyChar), out rate);
                string itemid = dgvItemList.CurrentRow.Cells["itemid"].Value.ToString();
                string dueqtystr = GetPurchaseOrderDueQuantity(itemid);
                int.TryParse(dueqtystr, out dueqty);
                if (dgvItemList.Columns[dgvItemList.CurrentCell.ColumnIndex].Name == "QTY")
                {
                    if (currentquantity > dueqty || currentquantity <= 0)
                        e.Handled = true;
                }
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
                id = mOrderidFromChallan;
            }
            string query = "Select DueQty,Qty from PurchaseOrderDetails where OrderId='" + id + "' and Itemid='" + itemID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string dueqrty = dt.Rows[0]["DueQty"].ToString();
                mfixedorderqty = dt.Rows[0]["Qty"].ToString();
                return dueqrty;
            }
            return null;
        }
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

            ItemTools.GetItemGSTRateAndAmountForPurchase(itemID, isigst, isregular, taxAmount, out cgstRate, out cgstAmount, out sgstRate,
                                             out sgstAmount, out igstRate, out igstAmount, out cessRate, out cessAmount, out totalWithTax);

            dgvItemList.Rows[rowindex].Cells["TotalAmount"].Value = amount.ToString("0.00");
            dgvItemList.Rows[rowindex].Cells["TAXABLEVALUE"].Value = taxAmount.ToString("0.00");
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
            lblTotalChallanAmount.Text = mtotalchallanAmount.ToString("0.00");
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
        private void lblTotalWithTax_TextChanged(object sender, EventArgs e)
        {
            SumOfCharges();
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



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                if (!IsDuplicateChallan())
                {
                    ChallanSave();

                }
            }
        }
        private bool IsValidSelection()
        {

            if (txtChallanno.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter supplier's challan no.", "Challan Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtChallanno.Focus();
                return false;
            }
            if (cmbDespatchThrough.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select despatch mode.", "Challan Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbDespatchThrough.Select();
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show(" At list add one item in the list.", "Challan Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
        private bool IsDuplicateChallan()
        {
            string id = mOrderID;
            string query = "select SupplyChallanNo from PurchaseChallan where orderid='" + id + "'and SupplyChallanNo='" + txtChallanno.Text.GetDBFormatString() + "'";
            if (!mChallanidForEdit.ISNullOrWhiteSpace())
            {
                id = mOrderidFromChallan;
                query = "select SupplyChallanNo from PurchaseChallan where SupplyChallanNo='" + txtChallanno.Text.GetDBFormatString() + "' and orderid<>'" + id + "'";

            }
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                MessageBox.Show("Entered challan no. alredy done.\n Try Another Challlan.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtChallanno.Focus();
                return true;
            }
            return false;
        }
        private void ChallanSave()
        {
            #region Data
            mLstQuery.Clear();
            string challanid = Guid.NewGuid().ToString();
            string slNo = lblSlNo.Text.GetDBFormatString();
            string challanno = txtChallanno.Text.GetDBFormatString();
            string orderno = "NULL";
            string orderid = "NULL";
            string challanDate = dtpChallanDate.Text;
            string ledgerID = Supplier._DicSuppliers.FirstOrDefault(x => x.Value == lblSuppliername.Text).Key.ToString();
            string totalChalanAmount = mtotalchallanAmount.ToString();
            string description = "NULL";
            string despatchthrough = cmbDespatchThrough.Text;
            string despatchdate = dtmdespatchdate.Text;
            string vehicleNo = txtVehicleNo.Text.GetDBFormatString();
            string freightcharge = txtfreightcharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtfreightcharges.Text.GetDBFormatString() + "'";
            string packingcharge = txtPackingCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtPackingCharges.Text.GetDBFormatString() + "'";
            string othercharge = txtOthersCharges.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtOthersCharges.Text.GetDBFormatString() + "'";

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
                CloseEditableStatus(ledgerID);
                mQuery = "Insert into PurchaseChallan(ChallanID, SlNo, SupplyChallanNo, OrderNo, OrderId, Date, " +
                        "LedgerID,  TotalAmount, Description,Status,DespatchThrough,DespatchDate,EditabilStatus,FreightCharges,PackingCharges,OtherCharges,VehicleNo) Values('" + challanid + "'," + slNo + ",'" + challanno + "'," + orderno + "," + orderid + ",'" + challanDate + "','" + ledgerID + "'," +
                        "" + totalChalanAmount + "," + description + ",'" + status + "','" + despatchthrough + "','" + despatchdate + "','Open'," + freightcharge + "," + packingcharge + "," + othercharge + ",'" + vehicleNo + "')";
                mLstQuery.Add(mQuery);
            }
            else
            {
                orderid = mOrderidFromChallan;
                challanid = mChallanidForEdit;
                mQuery = "Update PurchaseChallan set LedgerID='" + ledgerID + "',Date='" + challanDate + "',Description=" + description + "," +
                    "TotalAmount=" + totalChalanAmount + ",DespatchThrough='" + despatchthrough + "',DespatchDate='" + despatchdate + "', SupplyChallanNo='" + challanno + "',FreightCharges=" + freightcharge + ",PackingCharges=" + packingcharge + ",OtherCharges=" + othercharge + ",VehicleNo='" + vehicleNo + "' where ChallanID='" + mChallanidForEdit + "'";
                mLstQuery.Add(mQuery);
                RestorPurchaseOrderDetails();
                mQuery = "Delete from PurchaseChallanDetails where ChallanID='" + mChallanidForEdit + "'";
                mLstQuery.Add(mQuery);
            }
            ChallanDetailsAndPurchasesorderDetailsDatasave(challanid);
            //ChallanDetailsAndPurchaseorderDetailsDatasaveAndUpdate(challanid);

            #endregion

            #region Execute
            if (Ischallanissu == 1)
            {

                if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
                {
                    UpdatePurchaseOrderStatus();
                    MessageBox.Show("Order Rs. " + mtotalchallanAmount + " saved.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (mChallanidForEdit.ISNullOrWhiteSpace() && mOrderID.ISNullOrWhiteSpace())
                    {
                        ResetData();
                        lblSuppliername.Text = "xxxxx";
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Internully Problem \n" + msg, "Challan save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (!mChallanidForEdit.ISNullOrWhiteSpace())
                {
                    DestroyPurchaseOrderDetails();
                }
                MessageBox.Show("No recored for Saving.\nEnter quantity and rate", "save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }
        private void CloseEditableStatus(string ledgerid)
        {
            string id = mOrderID;
            if (!mChallanidForEdit.ISNullOrWhiteSpace())
            {
                id = mOrderidFromChallan;
            }
            string query = "Update PurchaseChallan set EditabilStatus='Close' Where OrderId='" + id + "' and LedgerID='" + ledgerid + "' ";
            mLstQuery.Add(query);
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
                int dueqty = 0, supplyqty = 0;
                int.TryParse(quantity, out supplyqty);
                int.TryParse(dueqtystr, out dueqty);
                dueqty = dueqty - supplyqty;

                #endregion
                #region INSERT
                if (amountStr != "")
                {
                    Ischallanissu = 1;
                    if (isregular)
                    {
                        if (isigst)
                        {
                            mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                    "Amount, DiscountRate, DiscountAmount, TaxAmount, " +
                                    "IGSTRate, IGSTAmount, CessRate,CeassAmount,Total,OrderQuantity)" +
                                    "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                    + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," + igstRate + "," +
                                    igstAmount + "," + cessRate + "," + cessAmount + "," + total + "," + mfixedorderqty + ")";
                        }
                        else
                        {
                            mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                    "Amount, DiscountRate, DiscountAmount, TaxAmount, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, " +
                                    "CessRate,CeassAmount, Total,OrderQuantity)" +
                                    "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                    + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," +
                                    cgstRate + "," + cgstAmount + "," + sgstRate + "," + sgstAmount + "," + cessRate + "," +
                                    cessAmount + "," + total + "," + mfixedorderqty + ")";
                        }
                    }
                    else
                    {
                        mQuery = "Insert into PurchaseChallanDetails(ChallanID, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, Total,OrderQuantity)" +
                                "Values('" + challanid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," +
                                taxAmount + "," + total + "," + mfixedorderqty + ")";
                    }
                    mLstQuery.Add(mQuery);
                    string id = mOrderID;
                    if (!mChallanidForEdit.ISNullOrWhiteSpace())
                    {
                        id = mOrderidFromChallan;
                    }
                        mQuery = "update PurchaseOrderDetails set ReceivedQty=" + supplyqty + ",DueQty=" + dueqty + " where OrderId='" + id + "' and ItemID='" + itemId + "'";
                        mLstQuery.Add(mQuery);
                   
                }
                #endregion
            }
            #endregion

        }
        private void UpdatePurchaseOrderStatus()
        {
            mLstQuery.Clear();
            string id = mOrderID;
            if (!mChallanidForEdit.ISNullOrWhiteSpace())
            {
                id = mOrderidFromChallan;
            }
            if (IsPurchaseOrderStatusClose())
            {
                mQuery = "Update PurchaseOrder set Status='Close',StatusForchallan='Close' where  OrderId='" + id + "'";
                mLstQuery.Add(mQuery);
                mQuery = "Update PurchaseChallan set Status='Open' where  OrderId='" + id + "'";
                mLstQuery.Add(mQuery);
            }
            else
            {
                mQuery = "Update PurchaseOrder set Status='Open',StatusForchallan='Open' where  OrderId='" + id + "'";
                mLstQuery.Add(mQuery);
                mQuery = "Update PurchaseChallan set Status='Close' where  OrderId='" + id + "'";
                mLstQuery.Add(mQuery);
            }
            SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg);


        }
        private bool IsPurchaseOrderStatusClose()
        {
            string id = mOrderID;
            if (!mChallanidForEdit.ISNullOrWhiteSpace())
            {
                id = mOrderidFromChallan;
            }
            string query = "Select DueQty from PurchaseOrderDetails where OrderId='" + id + "'";
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
        private void ResetData()
        {
            mChallanidForEdit = "";
            dgvItemList.Rows.Clear();
            lblTotalChallanAmount.Text = "0.00";
        }
        #endregion

        /// <summary>
        /// Edit Purchase Challan
        /// </summary>
        /// <returns></returns>
        private void DataTableColumnCreate()
        {
            mdt.Columns.Add("orderid", typeof(string));
            mdt.Columns.Add("itemid", typeof(string));
            mdt.Columns.Add("SupplyQty", typeof(string));
        }
        private void ViewExistingDataFromPurchaseChallan()
        {
            string query = "Select LadgerMain.TemplateName,GSTRegistrationType, ledgers.State,PurchaseChallan.*,CONVERT(varchar(11),date,106) as Challandate from LadgerMain" +
                " inner join ledgers on LadgerMain.ladgerid=ledgers.ledgerid " +
                " inner join PurchaseChallan  on PurchaseChallan.LedgerID=LadgerMain.LadgerID  where ChallanID='" + mChallanidForEdit + "' ";
            DataTable DT = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (DT.IsValidDataTable())
            {
                txtVehicleNo.Text= DT.Rows[0]["VehicleNo"].ToString();
                txtfreightcharges.Text = DT.Rows[0]["FreightCharges"].ToString();
                txtPackingCharges.Text= DT.Rows[0]["PackingCharges"].ToString();
                txtOthersCharges.Text = DT.Rows[0]["OtherCharges"].ToString();
                
                string orderid = DT.Rows[0]["OrderId"].ToString();
                lblOrderNo.Text = DT.Rows[0]["OrderNo"].ToString();
                lblSlNo.Text = DT.Rows[0]["SlNo"].ToString();
                txtChallanno.Text = DT.Rows[0]["SupplyChallanNo"].ToString();
                cmbDespatchThrough.Text = DT.Rows[0]["DespatchThrough"].ToString();
                dtmdespatchdate.Text = DT.Rows[0]["DespatchDate"].ToString();
                mGsttype = DT.Rows[0]["GSTRegistrationType"].ToString();
                string state = DT.Rows[0]["State"].ToString();
                lblSuppliername.Text = DT.Rows[0]["TemplateName"].ToString();
                dtpChallanDate.Text = DT.Rows[0]["Challandate"].ToString();
                double totamount = double.Parse(DT.Rows[0]["TotalAmount"].ToString());
                lblTotalChallanAmount.Text = totamount.ToString("0.00");
                mtotalchallanAmount = totamount;
                mOrderidFromChallan = orderid;

                string statecode = StateTool._DicState.FirstOrDefault(x => x.Value == state).Key.ToString();
                if (ORG_Tools._StateCode != statecode)
                {
                    isigst = true;
                }
                ViewExistingDataFromChallanDetails();
                GenerateTotal();

            }
        }
        private void ViewExistingDataFromChallanDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select * from PurchaseChallanDetails  Where ChallanID = '" + mChallanidForEdit + "'";
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
                    mdt.Rows.Add(mOrderidFromChallan, itemID, qtyStr);
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
                }
            }
        }
        private void ReadOnlyAllControl(string status)
        {
            if (!IsEditableModetrue())
            {
                foreach (Control item in this.Controls)
                {
                    item.Enabled = false;
                }
            }
            BTNcANCLE.Enabled = true;
        }
        private bool IsEditableModetrue()
        {
            string query = "Select EditabilStatus from PurchaseChallan Where ChallanID='" + mChallanidForEdit + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                if (obj.ToString() == "Close")
                {
                    return false;
                }
            }
            return true;
        }
        private void RestorPurchaseOrderDetails()
        {
            foreach (DataRow item in mdt.Rows)
            {
                string orderid = item["orderid"].ToString();
                string itemid = item["itemid"].ToString();
                string supplyQty = item["SupplyQty"].ToString();
                int duquantity = int.Parse(supplyQty);
                string query = "update PurchaseOrderDetails set DueQty=(select (DueQty+" + duquantity + ") as duerate from PurchaseOrderDetails where OrderId='" + orderid + "' and ItemID='" + itemid + "') where OrderId='" + orderid + "' and ItemID='" + itemid + "'";
                SQLHelper.GetInstance().ExcuteQuery(query, out msg);
            }
        }
        private void DestroyPurchaseOrderDetails()
        {
            foreach (DataRow item in mdt.Rows)
            {
                string orderid = item["orderid"].ToString();
                string itemid = item["itemid"].ToString();
                string supplyQty = item["SupplyQty"].ToString();
                int duquantity = int.Parse(supplyQty);
                string query = "update PurchaseOrderDetails set DueQty=(select (DueQty-" + duquantity + ") as duerate from PurchaseOrderDetails where OrderId='" + orderid + "' and ItemID='" + itemid + "') where OrderId='" + orderid + "' and ItemID='" + itemid + "'";
                SQLHelper.GetInstance().ExcuteQuery(query, out msg);
            }
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChallanEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
    }
}
