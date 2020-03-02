using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AddCashAccount : Form
    {
        string msg = "";
        public event Action OnClose;
        private bool mIsSuccess = false;
        string mLedgerIdForUpdate = "";
        public AddCashAccount()
        {
            InitializeComponent();
        }
        private bool IsValidData()
        {
            if (txtLedgerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter account name", "Account Head", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtLedgerName.Focus();
                return false;
            }

            return true;
        }
        private bool IsValidAccountName()
        {
            string acName = txtLedgerName.Text.GetDBFormatString();
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
                txtLedgerName.Focus();
                return false;
            }
            return true;
        }
        private void DataSave()
        {
            string ledgerID = Guid.NewGuid().ToString();
            string templeteName = txtLedgerName.Text.GetDBFormatString();
            string mSubAccount = "Cash A/C";
            string mParentAccount = "Current Assets";
            string mAccountType = "NULL";
            double amountBalance = 0d;
            double.TryParse(txtOpeningAmount.Text, out amountBalance);

            List<string> lstQuery = new List<string>();
            string query = "";
            if (mLedgerIdForUpdate.ISNullOrWhiteSpace())
            {
                query = "Insert into LadgerMain(LadgerID, LadgerName, TemplateName, Category, SubAccount, ParentAccount, Type)" +
                        "values('" + ledgerID + "','" + templeteName + "','" + templeteName + "','Cash','" +
                        mSubAccount + "','" + mParentAccount + "'," + mAccountType + ")";
                lstQuery.Add(query);
                query = "Insert into LedgerStatus( LedgerID, FinYearID, OpeningBalance, CurrentBalance) " +
                        "Values('" + ledgerID + "'," + FinancialYearTools._YearID + "," +
                        amountBalance + "," + amountBalance + ")";
                lstQuery.Add(query);
            }
            else
            {
                query = "Update LadgerMain set LadgerID='" + ledgerID + "',LadgerName='" + templeteName
                        + "',TemplateName='" + templeteName + "' where LadgerID='" + mLedgerIdForUpdate + "'";
                lstQuery.Add(query);
                query = "Update LedgerStatus set OpeningBalance=" + amountBalance + ", CurrentBalance=" +
                        amountBalance + " where LedgerID='" + ledgerID + "' and FinYearID=" + FinancialYearTools._YearID + "";
                lstQuery.Add(query);
            }
            if (SQLHelper.GetInstance().ExecuteTransection(lstQuery, out msg))
            {
                mIsSuccess = true;
                LedgerTools.GetCashLedgers();
                this.Close();
            }
            else
            {
                MessageBox.Show(msg + "\nData not save", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void AddCashAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess)
            {
                OnClose();
            }
        }
    }
}
