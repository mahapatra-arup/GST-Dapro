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
    public partial class EstimateGenerator : Form
    {
        bool IsCallendarvisible = false;
        //private long mTotalQuantity;
        private int mDescriptionSlno = 1;
        private double mAvailabelQty = 0d,mTotalOrderAmount, mTotalAmount, mTotalDiscount, mTotalIGST, mTotalCESS,
            mTaxableAmount, mTotalWithTax,mTotalCGST,
                       mTotalSGST,mCurrentQty = 0d, mmaxqty1 = 0d, mTotalQuantity = 0, finalqty = 0,
            mmaxqty = 0d, mqty1, mrate1, mqty2, mrate2, mqty3, mrate3, mtempcurrentqty;
        //private long mTotalQuantity;
        List<string> batchList = new List<string>();
        private string mHighestUnit = "", mMiddleUnit = "", mLowestUnit = "",
           mHighestStockQty = "", mMiddleStockQty = "", mLowestQty = "", mHighestSalesRate = "",
           mMiddlesalesRate = "", mLowestSalesRate = "", mHighestMesureUnit = "", mLowestMesureUnit = "";


        private string  mUnit1, mStockSummaryID, munit2, munit3, mstockunit;

        private bool mIsIGST = true;
        string msg = "", mquery = "";
        string mestimateserialno = "";
        string mEstimateidForEdit = string.Empty;
        List<string> mlistquery = new List<string>();


        public event Action onclose;
        public EstimateGenerator()
        {
            InitializeComponent();
            cmbState.AddState();
            GenerateGridForNonGSTType();
            cmbItemName.AddItem();
            cmbItemName.SelectedIndex = -1;
            GetExtimateNo();
            GridDesign();
        }

        public EstimateGenerator(string estimateid, string validupto)
        {
            InitializeComponent();
            cmbState.AddState();
            cmbItemName.AddItem();
            cmbItemName.SelectedIndex = -1;
            GridDesign();
            mEstimateidForEdit = estimateid;
            DateTime Vaildupto = DateTime.Parse(validupto);
            DateTime currentdate = DateTime.Now.Date;
            if (Vaildupto.Date < currentdate)
            {
                ReadOnlyAllControl("Close");
            }
            ViewExistingDataMain();
            btnSave.Text = "Update";

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
        private void ViewExistingDataMain()
        {
            string query = "Select  * from Estimate where EstimateId='" + mEstimateidForEdit + "'";
            DataTable DT = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (DT.IsValidDataTable())
            {
                lblEstimateNo.Text= DT.Rows[0]["EstimateNo"].ToString();
                txtCustomerName.Text = DT.Rows[0]["PartyName"].ToString();
                txtAddress.Text = DT.Rows[0]["Address"].ToString();
                cmbState.Text = DT.Rows[0]["StateName"].ToString();
                txtContactNo.Text = DT.Rows[0]["ContactNo"].ToString();
                txtremark.Text = DT.Rows[0]["Description"].ToString();
                txttemplatename.Text= DT.Rows[0]["TemplateName"].ToString();
                dtmEstimatedate.Text = DateTime.Parse(DT.Rows[0]["Date"].ToString()).ToString("dd-MMM-yyyy");
                txtUptodateDate.Text = DateTime.Parse(DT.Rows[0]["ValidUpTo"].ToString()).ToString("dd-MMM-yyyy");

                ViewExistingDataDetails();
                GenerateTotal();
            }
        }
        private void ViewExistingDataDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select EstimateDetails.* from EstimateDetails " +
                           "inner join Estimate on EstimateDetails.EstimateId=Estimate.EstimateId " +
                           "where Estimate.EstimateId='" + mEstimateidForEdit + "'";
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
                    string stockSummaryID= item["StockSummaryId"].ToString();
                    object disRateStr = item["DiscountRate"];
                    string disRate = disRateStr.ISValidObject() ? double.Parse(disRateStr.ToString()).ToString("0.00") : "";
                    object disAmountStr = item["DiscountAmount"];
                    string disAmount = disAmountStr.ISValidObject() ? double.Parse(disAmountStr.ToString()).ToString("0.00") : "";

                    string taxAmountStr = item["TaxAmount"].ToString();
                    string taxAmount = taxAmountStr.ISValidObject() ? double.Parse(taxAmountStr.ToString()).ToString("0.00") : "";

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

                    dgvItemList.Rows.Add(mDescriptionSlno, itemID, stockSummaryID, itemName, comodityCode, qtyStr, unit,
                                         rate.ToString("0.00"), amount.ToString("0.00"), disRate, disAmount, taxAmount
                                         , "", "", "", "", igstRate, igstAmount
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
        private void GenerateGridForNonGSTType()
        {
            if (ORG_Tools._IsRegularGST)
            {
                dgvItemList.Columns["CESSRATE"].Visible = true;
                dgvItemList.Columns["CESSAMOUNT"].Visible = true;
                if (mIsIGST)
                {
                    dgvItemList.Columns["IGSTRATE"].Visible = true;
                    dgvItemList.Columns["IGSTAMOUNT"].Visible = true;
                }
            }
            else
            {
                dgvItemList.Columns["IGSTRATE"].Visible = false;
                dgvItemList.Columns["IGSTAMOUNT"].Visible = false;
                dgvItemList.Columns["CESSRATE"].Visible = false;
                dgvItemList.Columns["CESSAMOUNT"].Visible = false;

                lblTotalIGST.Text = "";
                lblTotalCESS.Text = "";
            }
        }
        private void GetExtimateNo()
        {
            long ExtimateNo = OtherSettingTools._EstimateSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._EstimateSerialStart);
            string query = "select max(SlNo) as estimateNo from Estimate";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                string id = obj.ToString();
                try
                {
                    ExtimateNo = int.Parse(id)+1;
                }
                catch (Exception)
                {
                }

            }
            mestimateserialno = ExtimateNo.ToString();
            lblEstimateNo.Text = OtherSettingTools._EstimateStart+ mestimateserialno;
        }
        private void DataSave()
        {
            #region data
            string estimateNo = lblEstimateNo.Text;
            string estimateid = Guid.NewGuid().ToString();
            string customername = txtCustomerName.Text.GetDBFormatString();
            string address = txtAddress.Text.GetDBFormatString();
            string contactno = txtContactNo.Text.GetDBFormatString();
            string statename = cmbState.Text.GetDBFormatString();
            string estimatedate = dtmEstimatedate.Text;
            string valitupto = DateTime.Parse(txtUptodateDate.Text).ToString("dd-MMM-yyyy");
            string remark = "NULL";
            string totalAmount = lblTotalOrderAmount.Text.GetDBFormatString();
            if (!txtremark.Text.ISNullOrWhiteSpace())
            {
                remark = "'" + txtremark.Text.GetDBFormatString() + "'";
            }
            #endregion
            mlistquery.Clear();
            if (!mEstimateidForEdit.ISNullOrWhiteSpace())
            {
                estimateid = mEstimateidForEdit;
                mquery = "update Estimate set PartyName='" + customername + "',Address='" + address + "',ContactNo='" + contactno + "',Date='" + estimatedate + "',ValidUpTo='" + valitupto + "'," +
                        "TotalAmount=" + totalAmount + ",Description=" + remark + ",StateName='"+statename+ "',TemplateName='"+txttemplatename.Text.GetDBFormatString()+"' where EstimateId='" + mEstimateidForEdit + "' ";
                mlistquery.Add(mquery);
                mquery = "delete EstimateDetails where EstimateId='" + mEstimateidForEdit + "'";
                mlistquery.Add(mquery);
            }
            else
            {
                mquery = "insert into Estimate(SlNo,EstimateId,EstimateNo,PartyName,Address,StateName,ContactNo," +
                        "Date,ValidUpTo,TotalAmount,Description,Status,TemplateName) values('" + mestimateserialno + "','"+estimateid+"','" + estimateNo + "'," +
                        "'" + customername + "','" + address + "','"+statename+"','" + contactno + "','" + estimatedate + "','" + valitupto + "'," + totalAmount + "," + remark + ",'Open','"+txttemplatename.Text.GetDBFormatString()+"')";
                mlistquery.Add(mquery);
            }
            SaveEstimateDetails(estimateid);
            if (SQLHelper.GetInstance().ExecuteTransection(mlistquery, out msg))
            {
                if (mEstimateidForEdit.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Your data is save successfully. ", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                OtherSettingTools._IsEstimateBillGenarate=true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Internal error ." + "\n" + msg, " Do not Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SaveEstimateDetails(string estimateid)
        {
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
                #region Set Query
                if (ORG_Tools._IsRegularGST)
                {
                    if (mIsIGST)
                    {
                        mquery = "Insert into EstimateDetails(EstimateId, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                                "Amount, DiscountRate, DiscountAmount, TaxAmount, " +
                                "IGSTRate, IGSTAmount, CessRate,CeassAmount,Total, DueQty,StockSummaryId)" +
                                "Values('" + estimateid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                                + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," + taxAmount + "," + igstRate + "," +
                                igstAmount + "," + cessRate + "," + cessAmount + "," + total + "," + quantity + ","+ stocksummaryid + ")";
                    }
                }
                else
                {
                    mquery = "Insert into EstimateDetails(EstimateId, ItemID, ComodityCode, ItemName, Qty, Unit, Rate, " +
                            "Amount, DiscountRate, DiscountAmount, TaxAmount, Total, DueQty,StockSummaryId)" +
                            "Values('" + estimateid + "'," + itemId + ",'" + hsnCode + "','" + itemName + "'," + quantity
                            + ",'" + unit + "'," + rate + "," + amount + "," + disRate + "," + disAmount + "," +
                            taxAmount + "," + total + "," + quantity + "," + stocksummaryid + ")";
                }
                mlistquery.Add(mquery);
                #endregion
            }
        }
        private bool IsvalidEntry()
        {
            if (txtCustomerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter a customer name.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerName.Focus();
                return false;
            }
            if (txtContactNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter \"" + txtCustomerName.Text + "'s\" contact no.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtContactNo.Focus();
                return false;
            }
            else
            {
                if (txtContactNo.Text.Length != 10)
                {

                    MessageBox.Show("Please enter \"" + txtCustomerName.Text + "'s\" valid contact no.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtContactNo.Focus();
                    return false;
                }
            }
            if (txttemplatename.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter TemplateName.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txttemplatename.Focus();
                return false;
            }
            if (txtAddress.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter customer address.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAddress.Focus();
                return false;
            }
            if (cmbState.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Select customer State.", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbState.Focus();
                return false;
            }
            if (txtUptodateDate.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter how many day's valid for this estimate", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUptodateDate.Focus();
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Select  atleast one item", "Estimate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Focus();
                return false;
            }
            
            return true;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
              DataSave();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCalander_Click(object sender, EventArgs e)
        {
            if (!IsCallendarvisible)
            {
                IsCallendarvisible = true;
                monthCalendar1.Visible = true;
                monthCalendar1.Focus();
            }
            else
            {
                IsCallendarvisible = false;
                monthCalendar1.Visible = false;
            }
            
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            txtUptodateDate.Text = monthCalendar1.SelectionStart.ToString("dd-MMM-yyyy");
            txtUptodateDate.Focus();
        }
        private void txtUptodateDate_Leave(object sender, EventArgs e)
        {
            if (!txtUptodateDate.Text.ISNullOrWhiteSpace())
            {
                try
                {
                    txtUptodateDate.Text = DateTime.Parse(txtUptodateDate.Text).ToString("dd-MMM-yyyy");
                    DateTime vailduptodate = DateTime.Parse(txtUptodateDate.Text);
                    if (dtmEstimatedate.Value.Date > vailduptodate.Date)
                    {
                        MessageBox.Show("Plese enter a larger date from Estimate Date", "Valid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtUptodateDate.Focus();
                        txtUptodateDate.Text = dtmEstimatedate.Text;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Plese enter a valid date", "Valid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtUptodateDate.Focus();
                }

            }
        }
        private void txtUptodateDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46 || e.KeyChar == 47 || e.KeyChar == 45)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            txtCustomerName.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtCustomerName.Text.ToLower());
            txttemplatename.Clear();
            if (!txtContactNo.Text.ISNullOrWhiteSpace() && !txtCustomerName.Text.ISNullOrWhiteSpace())
            {
                txttemplatename.Text = txtCustomerName.Text + " (" + txtContactNo.Text + ")";
            }
            else
            {
                txttemplatename.Text = txtCustomerName.Text;

            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtAddress.Text.ToLower());
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter Number only", "Number Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void EstimateGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose();
            }
        }

        private void txtUptodateDate_TextChanged(object sender, EventArgs e)
        {

        }
        private void pnlEstimate_Paint(object sender, PaintEventArgs e)
        {

        }
        private void GetItemBatchNo(string itemid)
        {
            cmbBatchNo.Items.Clear();
            string query = "Select BatchNo,id,itemid,qty1 from StockSummary where itemid='" + itemid + "' order by id asc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out query);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    int j = 0;

                    //double stock = item["qty1"].ToString().ISNullOrWhiteSpace() ? 0 : double.Parse(item["qty1"].ToString());
                    string idandbatch = item["id"].ToString() + item["itemid"].ToString();
                    foreach (var i in batchList)
                    {
                        if (i == idandbatch)
                        {
                            j = 1;
                            break;
                        }
                    }
                    if (j == 0)
                    {
                        //if (stock > 0)
                        //{
                        cmbBatchNo.Items.Add(item["BatchNo"].ToString());
                        cmbBatchNo.SelectedIndex = 0;
                        //}
                    }
                }
            }
            if (cmbBatchNo.Items.Count != 0)
            {
                cmbBatchNo.Enabled = true;
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
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

        private void dgvitemList_Paint(object sender, PaintEventArgs e)
        {
            #region Assign Array
            string[] array = new string[3];
            int[] ary = new int[3];
            int length = 0;
            if (ORG_Tools._IsRegularGST)
            {
                if (mIsIGST)
                {
                    length = 3;
                    array[0] = "DISCOUNT";
                    array[1] = "GST";
                    array[2] = "CESS";

                    ary[0] = 9;
                    ary[1] = 16;
                    ary[2] = 18;
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

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void txtQuantity_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Clear();
            if (!cmbUnit.Text.ISNullOrWhiteSpace())
            {
                GetSaleRate(cmbUnit.Text);
                CalculateAmount();
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
        private bool IsValidItemData()
        {
            if (txtCustomerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give customer template name.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerName.Select();
                return false;
            }
            if (txtContactNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give customer Contact No.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtContactNo.Select();
                return false;
            }
            if (txttemplatename.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give customer name.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txttemplatename.Select();
                return false;
            }
            if (cmbState.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give State name.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbState.Select();
                return false;
            }
            if (txtUptodateDate.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please give Validiti Date.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUptodateDate.Select();
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

        private void btnAdd_Click_1(object sender, EventArgs e)
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

                dgvItemList.Rows.Add(mDescriptionSlno, itemID, mStockSummaryID, itemName, hsnCode, qty, unit, rate.toString(), amount.toString(), discountRate,
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

            lblTotalIGST.Text = mTotalIGST.toString();
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
            txtRate.Clear();
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

        private void BtnClear_Click_1(object sender, EventArgs e)
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
            txtAmount.Clear();
        }


        private void btnAddstate_Click(object sender, EventArgs e)
        {
            State_master state = new State_master();
            state.onclose += State_onclose;
            state.ShowDialog();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            txtUptodateDate.Text = monthCalendar1.SelectionStart.ToString("dd-MMM-yyyy");
            txtUptodateDate.Focus();

        }

        private void monthCalendar1_Leave(object sender, EventArgs e)
        {
                monthCalendar1.Visible = false;
        }

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {
            txtContactNo.ForeColor = Color.Black;

            if (!txtContactNo.Text.ISNullOrWhiteSpace())
            {
                if (txtContactNo.Text.Length!=10 && txtContactNo.Text.Length != 12)
                {
                    txtContactNo.ForeColor = Color.Red;
                }
            }
        }


        private void cmbItemName_Leave_1(object sender, EventArgs e)
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

        private void cmbBatchNo_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void txtContactNo_Leave(object sender, EventArgs e)
        {
            txttemplatename.Clear();
            if (!txtContactNo.Text.ISNullOrWhiteSpace() && !txtCustomerName.Text.ISNullOrWhiteSpace())
            {
                txttemplatename.Text = txtCustomerName.Text + " (" + txtContactNo.Text + ")";
            }
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

        private void txtDiscountAmount_KeyPress_1(object sender, KeyPressEventArgs e)
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

        void State_onclose(string statename)
        {
            cmbState.AddState();
            cmbState.Text = statename;
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
    }
}
