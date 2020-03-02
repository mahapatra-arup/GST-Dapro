using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class BillEntry : Form
    {
        private string msg = "";
        private string mquery = "";

        public event Action OnClose;
        private string mSupplierLadgerID = "";
        private string mGstType, mIsRcm = "No";
        private string mExpenceIDForEdit = "";
        private List<string> mLstQuery = new List<string>();
        private float mtotalBillingAmount = 0f;
        public BillEntry()
        {
            InitializeComponent();
            this.FitOnDown();
            cmbSupplierName.AddSuppliers();
            cmbAccountHead.ADDOtherLedgers();
            GenerateSlNo();
        }
        private void GenerateSlNo()
        {
            int slno = 1;
            string query = "Select max(SlNo) as slno from Expense ";
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
        private bool IsValidSelection()
        {
            if (cmbSupplierName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a supplier.", "Bill Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSupplierName.Select();
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Please add at least one item.", "Bill Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAccountHead.Select();
                return false;
            }
            return true;
        }
        private void BillSave()
        {
            #region Data
            mLstQuery.Clear();
            string transectionID = Guid.NewGuid().ToString();
            string ladgerID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
            string billNo = txtBillNo.Text.GetDBFormatString();
            string billDate = dtpBillDate.Text;
            string transectionType = "Expense Bill";
            string totAmount = mtotalBillingAmount.ToString();
            string billDesctiption = txtBillingDescription.Text.GetDBFormatString();
            string slNo = lblSlNo.Text;
            string dueDate = dtpDueDate.Text;
            string memoNo = txtMamoNumber.Text.GetDBFormatString();
            string voucherNo = txtBillNo.Text.GetDBFormatString();
            string status = "Open";
            bool isSplit = (dgvItemList.Rows.Count > 1) ? true : false;
            #endregion
            #region Query
            if (mExpenceIDForEdit.ISNullOrWhiteSpace())
            {
                mquery = "Insert into Expense(SlNo, BillNo, BillingDate, LedgerId, DueDate, MemoNo, " +
                        "Description, TotalAmount, DueAmount, Status, RCM, LastTransectionID) " +
                        "Values(" + slNo + ",'" + billNo + "','" + billDate + "','" + ladgerID
                        + "','" + dueDate + "','" + memoNo + "','" + billDesctiption + "'," + totAmount
                        + "," + totAmount + ",'" + status + "','" + mIsRcm + "','" + transectionID + "')";
                mLstQuery.Add(mquery);
            }
            else
            {
                mquery = "Update Transection LedgerID='" + ladgerID + "',  Date='" + billDate + "', Amount=" + totAmount + ", " +
                        "Purpose='" + billDesctiption + "', DueDate='" + dueDate + "', MamoNo='" + memoNo + "', IsSplit='" +
                        isSplit + "',VoucherNo='" + voucherNo + "' where TransectionID='" + mExpenceIDForEdit + "'";
                mLstQuery.Add(mquery);
            }
            #endregion
            ///Expense Details
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                transectionID = Guid.NewGuid().ToString();
                string description = row.Cells["Description"].Value.ToString().GetDBFormatString();
                string accountHeadID = row.Cells["AccountHeadId"].Value.ToString();
                string amount = row.Cells["Amount"].Value.ToString();
                mquery = "Insert into ExpenseDetails(SlNo, LedgerID, Description, Amount, TransectionID)" +
                        "Values(" + slNo + ",'" + ladgerID + "','" + description + "'," + amount
                        + ",'" + transectionID + "')";
                mLstQuery.Add(mquery);
                string draccount = accountHeadID;
                string craccount = ladgerID;
                string transectiontype = "Expense_Bill";
                InsertOrUpdateTransection(transectionID, billDate, billNo, amount, draccount, craccount, transectiontype, "NULL", "NULL", "NULL", "NULL");

                #region CurrentBalanceUpdate

                //mlistQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, mTotalAmount.ToString("0.00"), out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mLstQuery.Add(LedgerStatus.UpdateLedgerStatus(draccount, craccount, amount, out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                mLstQuery.Add(mquery);
                #endregion

            }

            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
            {
                MessageBox.Show("Bill Rs. " + mtotalBillingAmount + " saved.", "Bill Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (mExpenceIDForEdit.ISNullOrWhiteSpace())
                {
                    ResetData();
                    cmbSupplierName.SelectedIndex = -1;
                    cmbSupplierName.Select();
                }
                else
                {
                    this.Close();
                }
            }
            #endregion
        }
        private void InsertOrUpdateTransection(string tranid, string billDate, string billNo, string totalinvoicevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate)
        {
            string transectionid = tranid;
            if (mExpenceIDForEdit.ISNullOrWhiteSpace())
            {
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                            "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                            billDate + "','" + billNo + "','" + transectiontype + "','" + drledgerid + "','" +
                            crledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                            + "," + ChequeNo + "," + ChequeDate + ")";
                mLstQuery.Add(mquery);
                transectionid = Guid.NewGuid().ToString();
                mquery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                        billDate + "','" + billNo + "','" + transectiontype + "','" + crledgerid + "','" +
                        drledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                        + "," + ChequeNo + "," + ChequeDate + ")";
                mLstQuery.Add(mquery);
            }

            else
            {
                TransectionTools.GetTransectionId(billNo, transectiontype);

                mquery = "Update Transection Set Date='" + billDate + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mLstQuery.Add(mquery);

                mquery = "Update Transection Set Date='" + billDate + "',LedgerIdFrom='" + crledgerid + "', " +
                       "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalinvoicevalue + ",Mode=" + Mode
                        + ",bankname=" + BankName + ",ChequeNo=" + ChequeNo + ",ChequeDate=" + ChequeDate + " where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mLstQuery.Add(mquery);

            }

        }

        private void ResetData()
        {
            mtotalBillingAmount = 0F;
            GenerateSlNo();
            //mExpenceIDForEdit = "";
            dtpBillDate.Value = DateTime.Now;
            dtpDueDate.Value = DateTime.Now;
            txtBillNo.Clear();
            txtMamoNumber.Clear();
            txtBillingDescription.Clear();
            txtAmount.Clear();
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            lblTotPaymentAmt.Text = "BILLING AMOUNT Rs. " + mtotalBillingAmount.ToString("0.00");
            cmbAccountHead.SelectedIndex = -1;
        }
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog);
            frm.OnClose += Frm_OnClose;
            frm.ShowDialog();
        }
        private void Frm_OnClose(string supplierName)
        {
            cmbSupplierName.AddSuppliers();
            cmbSupplierName.Text = supplierName;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                if (MessageBox.Show("Are you sure you want to save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BillSave();
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///Description add
        /// 
        #region Description Details

        private int mDescriptionSlno = 1;
        private bool IsValidDescription()
        {
            if (cmbAccountHead.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select any account head.", "Description", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAccountHead.Select();
                return false;
            }
            if (txtItemDescription.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter description.", "Description", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtItemDescription.Focus();
                return false;
            }
            if (txtAmount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter amount.", "Description", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAmount.Focus();
                return false;
            }
            return true;
        }
        private bool IsDuplicateDescription()
        {
            string description = txtItemDescription.Text;
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string descriptionExist = row.Cells["Description"].Value.ToString();
                if (description == descriptionExist)
                {
                    MessageBox.Show("Found duplicate descrioption. Desctiption already added.", "Description", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtItemDescription.Select();
                    return true;
                }
            }
            return false;
        }
        private void DescriptionAdd()
        {
            string accountID = ((KeyValuePair<string, string>)cmbAccountHead.SelectedItem).Key.ToString();
            string accountHeadName = ((KeyValuePair<string, string>)cmbAccountHead.SelectedItem).Value.ToString();
            string desciption = txtItemDescription.Text.GetDBFormatString();
            string amount = float.Parse(txtAmount.Text).ToString("0.00");

            dgvItemList.Rows.Add(mDescriptionSlno, accountID, accountHeadName, desciption, amount);
            DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
            btnCelCol.ToolTipText = "Delete";
            btnCelCol.Value = "Delete";
            btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
            //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
            dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnCol"] = btnCelCol;
            mDescriptionSlno++;
        }
        private void GenerateTotal()
        {
            mDescriptionSlno = 1;
            float totAmt = 0f;
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                float amount = float.Parse(row.Cells["Amount"].Value.ToString());
                totAmt += amount;
                row.Cells["SlNo"].Value = mDescriptionSlno++;
            }
            mtotalBillingAmount = totAmt;
            lblTotPaymentAmt.Text = "BILLING AMOUNT Rs. " + mtotalBillingAmount.ToString("0.00");
        }
        private void btnBillAdd_Click(object sender, EventArgs e)
        {
            if (IsValidDescription())
            {
                if (!IsDuplicateDescription())
                {
                    DescriptionAdd();
                    GenerateTotal();
                    txtItemDescription.Clear();
                    txtAmount.Clear();
                    txtItemDescription.Focus();
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            mtotalBillingAmount = 0f;
            lblTotPaymentAmt.Text = "PAYMENT AMOUNT Rs. " + mtotalBillingAmount.ToString("0.00");

            cmbAccountHead.SelectedIndex = -1;
            txtItemDescription.Clear();
            txtAmount.Clear();
        }
        #endregion
        private void BillEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) || e.KeyChar == '\b' || e.KeyChar == '.')
            {
                if (e.KeyChar == '.')
                {
                    char value = e.KeyChar;
                    string text = txtAmount.Text + value;
                    try
                    {
                        double.Parse(text);
                        e.Handled = false;
                    }
                    catch (Exception)
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
        private void cmbSuppliersName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetData();
        }
        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemList.SelectedCells.Count > 0)
            {
                if (dgvItemList.Columns[e.ColumnIndex].Name == "btnCol")
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    GenerateTotal();
                }
            }
        }
        private void dgvItemList_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemList.SelectedCells.Count > 0)
            {
                if (dgvItemList.Columns[e.ColumnIndex].Name == "btnCol")
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    GenerateTotal();
                }
            }
        }
        private void btnAddAccountHead_Click(object sender, EventArgs e)
        {
            AddAccountLedger frmAddOthersAcHead = new AddAccountLedger();
            frmAddOthersAcHead.OnClose += new Action<string>(frmAddOthersAcHead_OnClose);
            frmAddOthersAcHead.Show();
        }
        void frmAddOthersAcHead_OnClose(string obj)
        {
            cmbAccountHead.ADDOtherLedgers();
            cmbAccountHead.Text = obj;
        }

        private void cmbSuppliersName_Leave(object sender, EventArgs e)
        {
            int index = cmbSupplierName.FindStringExact(cmbSupplierName.Text);
            if (index >= 0)
            {
                cmbSupplierName.SelectedIndex = index;
                mSupplierLadgerID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
                GetSupplierDetails();
            }
            else
            {
                cmbSupplierName.Text = "";
            }
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
                mGstType = gsttype;
                if (mGstType == "Unregister")
                {
                    mIsRcm = "Yes";
                }
            }
        }
    }
}
