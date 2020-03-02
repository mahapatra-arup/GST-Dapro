using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AddAccountLedger : Form
    {
        string msg = "";
        public event Action<string> OnClose;
        private bool mIsSuccess = false;
        string mLedgerIdForUpdate = "";
        public AddAccountLedger()
        {
            InitializeComponent();
            cmbSubAccount.AddSubAccountForExpense();
        }
        public AddAccountLedger(string acId)
        {
            InitializeComponent();
            mLedgerIdForUpdate = acId;
            cmbSubAccount.AddIncomeExpenseGroup();
            ShowData();
        }
        private bool IsValidData()
        {
            if (txtAcName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter account name", "Account Head", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAcName.Focus();
                return false;
            }
            if (cmbSubAccount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select account type.", "Account Head", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSubAccount.Select();
                return false;
            }
            return true;
        }
        private bool IsValidAccountName()
        {
            string acName = txtAcName.Text.GetDBFormatString();
            string query = "";
            if (mLedgerIdForUpdate.ISNullOrWhiteSpace())
            {
                query = "Select LadgerID from LadgerMain where TemplateName='" + acName + "'";
            }
            else
            {
                query = "Select TemplateName from LadgerMain where" +
                        " LadgerID <>" + mLedgerIdForUpdate + " and " +
                        " TemplateName='" + acName + "'";
            }
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                MessageBox.Show("This account already exist.", "Account Head", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAcName.Focus();
                return false;
            }
            return true;
        }
        private void DataSave()
        {
            string ledgerID = Guid.NewGuid().ToString();
            string subAccount = cmbSubAccount.Text.ToString();
            string templeteName = txtAcName.Text.GetDBFormatString();
            string parentAccount = SubAccountTools.GetAccountParent(subAccount);
            string parentUnder = SubAccountTools.GetAccountUnder(subAccount);
            parentUnder = parentUnder.ISNullOrWhiteSpace() ? "NULL" : "'" + parentUnder + "'";
            double amountBalance = 0d;
            double.TryParse(txtOpeningBalance.Text, out amountBalance);

            List<string> lstQuery = new List<string>();
            string query = "";
            if (mLedgerIdForUpdate.ISNullOrWhiteSpace())
            {
                query = "Insert into LadgerMain(LadgerID, LadgerName, TemplateName, Category, SubAccount, ParentAccount, Type)" +
                        "values('" + ledgerID + "','" + templeteName + "','" + templeteName + "','Others','" +
                        subAccount + "','" + parentAccount + "'," + parentUnder + ")";
                lstQuery.Add(query);
                query = "Insert into LedgerStatus( LedgerID, FinYearID, OpeningBalance, CurrentBalance) " +
                        "Values('" + ledgerID + "'," + FinancialYearTools._YearID + "," +
                        amountBalance + "," + amountBalance + ")";
                lstQuery.Add(query);
            }
            else
            {
                query = "Update LadgerMain set LadgerID='" + ledgerID + "',LadgerName='" + templeteName
                        + "',TemplateName='" + templeteName + "',SubAccount='" + subAccount
                        + "',ParentAccount='" + parentAccount + "',Type=" + parentUnder 
                        + " where LadgerID='" + mLedgerIdForUpdate + "'";
                lstQuery.Add(query);
                query = "Update LedgerStatus set OpeningBalance=" + amountBalance + ", CurrentBalance=" +
                        amountBalance + " where LedgerID='" + ledgerID + "'," + FinancialYearTools._YearID + ",,)";
                lstQuery.Add(query);
            }
            if (SQLHelper.GetInstance().ExecuteTransection(lstQuery, out msg))
            {
                LedgerTools.GetOtherLedgers();
                mIsSuccess = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg + "\nData not save", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowData()
        {
            string query = "Select * from LadgerMain where LadgerID ='" + mLedgerIdForUpdate + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                txtAcName.Text = dt.Rows[0]["TemplateName"].ToString();
                cmbSubAccount.Text = dt.Rows[0]["SubAccount"].ToString();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (IsValidAccountName())
                {
                    DataSave();
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddOthersAcHead_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess)
            {
                OnClose(txtAcName.Text.GetDBFormatString());
            }
        }
        private void txtOpeningBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
