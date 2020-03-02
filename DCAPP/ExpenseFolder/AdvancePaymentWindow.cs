using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AdvancePaymentWindow : Form
    {
        private string msg = "";
        public event Action OnClose;
        private string mTransectionIDForEdit = "";
        private List<string> mLstQuery = new List<string>();
        public AdvancePaymentWindow()
        {
            InitializeComponent();
            cmbSuppliersName.AddSuppliers();
            GenerateSlNo();
        }
        private void GenerateSlNo()
        {
            int slno = 1;
            string query = "Select max(SlNo) as slno from Transection where TransectionType='AdvancePayment'";
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool IsValidSelection()
        {
            if (cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select supplier's name.", "Advance Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSuppliersName.Select();
                return false;
            }
            if (cmbPaymentMethod.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment method.", "Advance Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentMethod.Select();
                return false;
            }
            if (cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment account.", "Advance Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentAccount.Select();
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
        private void BillSave()
        {
            #region Data
            mLstQuery.Clear();
            string transectionID = Guid.NewGuid().ToString();
            string ladgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
            string advancePaymentHeadID = AccountHeadTools._AdvancePaymentID;
            string billDate = dtpBillDate.Text;
            string transectionType = "AdvancePayment";
            string amount = txtAdvanceAmount.Text;
            string desctiption = txtBillingDescription.Text.GetDBFormatString();
            string slNo = lblSlNo.Text;
            bool isPaid = true;
            string status = "Not Adjust";
            bool isSplit = false;
            string query = "";

            #endregion

            #region Query
            if (mTransectionIDForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into Transection(TransectionID, LedgerID, AccountHeadIdTo, Date, TransectionType, Amount, " +
                        "Balance,Purpose, SlNo, Status, IsSplit) " +
                        "Values('" + transectionID + "','" + ladgerID + "','" + advancePaymentHeadID + "','" + billDate
                        + "','" + transectionType + "'," + amount + "," + amount + ",'" + desctiption + "'," + slNo +
                        ",'" + status + "','" + isSplit + "')";
                mLstQuery.Add(query);
            }
            else
            {
                query = "Update Transection LedgerID='" + ladgerID + "',  Date='" + billDate + "', Amount=" + amount + ", " +
                        "Purpose='" + desctiption + "', where TransectionID='" + mTransectionIDForEdit + "'";
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

            string transectionById = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();

            query = "Insert into TransectionDetails(TransectionID, AccountHeadFrom, AccountHeadTo, " +
                    "TransectionType, Description, Amount)" +
                    "Values('" + transectionID + "','" + advancePaymentHeadID + "','" + transectionById +
                    "','" + transectionType + "','" + desctiption + "'," + amount + ")";
            mLstQuery.Add(query);
            #endregion

            #region Execute
            if (MessageBox.Show("Are you sure you want to proceed to payment Rs. " + txtAdvanceAmount.Text + " ?", "Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
                {
                   // MessageBox.Show("Advance payment Rs. " + txtAdvanceAmount.Text + " saved.", "Supplier Advance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (mTransectionIDForEdit.ISNullOrWhiteSpace())
                    {
                        ResetData();
                        cmbSuppliersName.Select();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            #endregion
        }
        private void ResetData()
        {
            GenerateSlNo();
            mTransectionIDForEdit = "";
            cmbSuppliersName.SelectedIndex = -1;
            dtpBillDate.Value = DateTime.Now;
            txtBillingDescription.Clear();
            txtAdvanceAmount.Clear();
            cmbBank.SelectedIndex = -1;
            txtChequeNo.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                BillSave();
            }
        }
        private void AdvancePaymentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
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
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frmPayee = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog);
            frmPayee.OnClose += frmPayee_OnClose;
            frmPayee.ShowDialog();
        }
        void frmPayee_OnClose(string obj)
        {
            cmbSuppliersName.AddSuppliers();
            cmbSuppliersName.Text = obj;
        }
        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBalance.Text = "Rs. ";
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string acHeadID = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                float balance = 0f;// AccountHeadTools.GetCashBalance(acHeadID);
                lblBalance.Text = "Rs. " + balance.ToString("0.00");
            }
        }
    }
}
