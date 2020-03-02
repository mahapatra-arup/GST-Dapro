using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class ExpenseEntry : Form
    {
        private string msg = "";
        public event Action OnClose;
        private string mTransectionIdForEdit = "";
        private List<string> mLstQuery = new List<string>();

        private double mTotalPaidAmount = 0f;
        public ExpenseEntry()
        {
            InitializeComponent();
            this.FitOnDown();
            GenerateSlNo();
            cmbPayeeName.AddSuppliers();
            //cmbAccountHead.AddAccountHeadForExpense();
        }
        private void GenerateSlNo()
        {
            int slno = 1;
            string query = "Select max(SlNo) as slno from Transection where TransectionType='Expense'";
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
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.Text == "Cash")
            {
                cmbPaymentAccount.ADDCashAccount();
                pnlChequeDetails.Visible = false;
            }
            else if (cmbPaymentMethod.Text == "Cheque")
            {
                cmbPaymentAccount.ADDBankAccountHead();
                cmbBank.AddBank();
                pnlChequeDetails.Visible = true;
            }
            else
            {
                cmbPaymentAccount.ADDBankAccountHead();
                pnlChequeDetails.Visible = false;
            }
        }
        private bool IsValidSelection()
        {
            if (cmbPayeeName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a supplier.", "Expense Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPayeeName.Select();
                return false;
            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Please enter at least one item.", "Expense Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAccountHead.Select();
                return false;
            }
            if (cmbPaymentMethod.Text == "Cheque")
            {
                if (cmbBank.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select bank name.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbBank.Select();
                    return false;
                }
                if (txtChequeNo.Text.ISNullOrWhiteSpace() || txtChequeNo.Text.Length < 6)
                {
                    MessageBox.Show("Enter a valid cheque no.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtChequeNo.Select();
                    return false;
                }
            }
            return true;
        }
        private void ExpenseSave()
        {
            mLstQuery.Clear();
            string transectionID = Guid.NewGuid().ToString();
            string ladgerID = ((KeyValuePair<string, string>)cmbPayeeName.SelectedItem).Key.ToString();
            string transectionById = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
            string paymentDate = dtpDate.Text;
            string transectionType = "Expense";
            string transectionMode = cmbPaymentMethod.Text;
            string paidAmountStr = mTotalPaidAmount.ToString();
            string balance = "0";
            string description = "";
            string status = "Paid";
            bool isSplit = dgvItemList.Rows.Count > 0 ? true : false;
            string slNo = lblSlNo.Text;
            string query = "";

            if (mTransectionIdForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into Transection(TransectionID, LedgerID, AccountHeadIdTo, Date, TransectionType, Amount, " +
                        "Balance,Purpose, SlNo, Status, IsSplit) " +
                        "Values('" + transectionID + "','" + ladgerID + "','" + transectionById + "','" + paymentDate
                        + "','" + transectionType + "'," + mTotalPaidAmount + "," + balance + ",'" + description + "'," + slNo +
                        ",'" + status + "','" + isSplit + "')";
                mLstQuery.Add(query);
            }
            else
            {
                query = "Update Transection LedgerID='" + ladgerID + "', AccountHeadIdTo='" + transectionById + "', Date='" + paymentDate
                        + "', TransectionType'" + transectionType + "', Amount" + mTotalPaidAmount + ",Balance=" + balance
                        + ",Purpose='" + description + "', SlNo=" + slNo + ", Status='" + status + "', IsSplit='" + isSplit
                        + "' where Transection='" + mTransectionIdForEdit + "'";
                mLstQuery.Add(query);
            }
            if (cmbPaymentMethod.Text == "Cheque")
            {
                string bankID = ((KeyValuePair<string, string>)cmbBank.SelectedItem).Key.ToString();
                string chequeNo = txtChequeNo.Text;
                string issueDate = dtpDateCheque.Text;
                query = "Update Transection set BankID=" + bankID + ",ChequeNo=" + chequeNo + ",IssueDate='" +
                        issueDate + "' where TransectionID='" + transectionID + "'";
                mLstQuery.Add(query);
            }
            TransectionMore(transectionID, transectionById, transectionType);
            if (MessageBox.Show("Are you sure you want to proceed to payment Rs. " + mTotalPaidAmount + " ?", "Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
                {
                    MessageBox.Show("Expense complete Rs. " + mTotalPaidAmount + "", "Expense Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (mTransectionIdForEdit.ISNullOrWhiteSpace())
                    {
                        ResetData();
                        cmbPayeeName.Select();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
        private void TransectionMore(string transectionID, string payHeadID, string transectionType)
        {
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string accountHeadFrom = row.Cells["AccountHeadID"].Value.ToString();
                string amountStr = row.Cells["Amount"].Value.ToString();
                double amount = !amountStr.ISNullOrWhiteSpace() ? double.Parse(amountStr) : 0f;

                string query = "Insert into TransectionDetails(TransectionID, AccountHeadFrom, AccountHeadTo, TransectionType, Amount) " +
                               "Values('" + transectionID + "','" + accountHeadFrom + "','" + payHeadID + "','" +
                               transectionType + "'," + amount + ")";
                mLstQuery.Add(query);
            }
        }
        private void ResetData()
        {
            mTransectionIdForEdit = "";
            mTotalPaidAmount = 0f;
            cmbPayeeName.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            txtMamoNumber.Clear();
            txtPaymentDescription.Clear();
            txtAmount.Clear();
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;
            cmbBank.SelectedIndex = -1;
            txtChequeNo.Clear();
            lblTotPaymentAmt.Text = "PAYMENT AMOUNT Rs. " + mTotalPaidAmount.ToString("0.00");
        }

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
                    MessageBox.Show("Found duplicate descrioption. Desctiption already added.", "Expense Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            string amount = double.Parse(txtAmount.Text).toString();

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
            double totAmt = 0f;
            mDescriptionSlno = 1;
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                double amount = double.Parse(row.Cells["Amount"].Value.ToString());
                totAmt += amount;
                row.Cells["SlNo"].Value = mDescriptionSlno++;
            }
            mTotalPaidAmount = totAmt;
            lblTotPaymentAmt.Text = "PAYMENT AMOUNT Rs. " + mTotalPaidAmount.toString();
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
            mTotalPaidAmount = 0f;
            lblTotPaymentAmt.Text = "PAYMENT AMOUNT Rs. " + mTotalPaidAmount.ToString("0.00");

            cmbAccountHead.SelectedIndex = -1;
            txtItemDescription.Clear();
            txtAmount.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                ExpenseSave();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frmPayee = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog);
            frmPayee.OnClose += frmPayee_OnClose;
            frmPayee.ShowDialog();
        }
        void frmPayee_OnClose(string obj)
        {
            cmbPayeeName.AddSuppliers();
            cmbPayeeName.Text = obj;
        }
        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBalance.Text = "Rs. ";
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string acHeadID = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                double balance = 0f;// AccountHeadTools.GetCashBalance(acHeadID);
                lblBalance.Text = "Rs. " + balance.toString();
            }
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
        private void btnAccountHeadAdd_Click(object sender, EventArgs e)
        {

            AddAccountLedger frmAddOthersAcHead = new AddAccountLedger();
            frmAddOthersAcHead.OnClose += new Action<string>(frmAddOthersAcHead_OnClose);
            frmAddOthersAcHead.Show();
        }
        void frmAddOthersAcHead_OnClose(string obj)
        {
            //cmbAccountHead.AddAccountHeadForExpense();
            cmbAccountHead.Text = obj;
        }

        private void ExpenseEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
    }
}
