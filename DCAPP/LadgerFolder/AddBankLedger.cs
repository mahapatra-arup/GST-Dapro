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
    public partial class AddBankLedger : Form
    {
        string msg = "";
        public event Action OnClose;
        private string mLedgerId = "";
        List<string> mlistQuery = new List<string>();

        public AddBankLedger()
        {
            InitializeComponent();
            cmbBankName.AddBank();
            cmbAcType.SelectedIndex = 0;
        }
        public AddBankLedger(string ledgerID)
        {
            InitializeComponent();
            mLedgerId = ledgerID;
            cmbBankName.AddBank();
            cmbAcType.SelectedIndex = 0;
            ShowDetails();
        }
        private void ShowDetails()
        {
            string query = "Select LadgerMain.LadgerName,LadgerMain.TemplateName,LedgerBankDetails.*, "+
                           "Bank.BankName,LedgerStatus.OpeningBalance,LedgerStatus.Date from LadgerMain " +
                           "inner join LedgerBankDetails on LadgerMain.LadgerID= LedgerBankDetails.LedgerID " +
                           "inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "inner join Bank on LedgerBankDetails.BankID=Bank.ID " +
                           "where LadgerMain.LadgerID='" + mLedgerId + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                cmbBankName.Text = dt.Rows[0]["BankName"].ToString();
                txtAcNo.Text = dt.Rows[0]["ACNo"].ToString();
                txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();
                txtMICR.Text = dt.Rows[0]["MICR"].ToString();
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtLedgerName.Text = dt.Rows[0]["TemplateName"].ToString();
                cmbAcType.Text = dt.Rows[0]["ACType"].ToString();

                string openingbalstr = dt.Rows[0]["Openingbalance"].ToString();
                double openingbal = 0d;
                double.TryParse(openingbalstr, out openingbal);
                txtOpeningAmount.Text = openingbal == 0 ? "" : openingbal < 0 ? (-(openingbal)).ToString() : openingbal.ToString();
                cmbcrdr.Text = openingbal < 0 ? "Cr." : "Dr.";
                string openingDate = dt.Rows[0]["Date"].ToString();

                if (!openingDate.ISNullOrWhiteSpace())
                {
                    dtpDate.Text = DateTime.Parse(dt.Rows[0]["Date"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    ToolTip tt = new ToolTip();
                    tt.Show("Opening date not found.\nPlease set opening date.", dtpDate, 500);
                    cmbcrdr.SelectedIndex = 0;
                }
            }
        }
        private bool IsvalidData()
        {
            if (cmbBankName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select bank name", "Add Bank Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbBankName.Select();
                return false;
            }
            if (txtAcNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Account number", "Add Bank Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAcNo.Focus();
                return false;
            }
            if (txtLedgerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Account Name", "Add Bank Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtLedgerName.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidLedger()
        {
            string acName = txtLedgerName.Text.GetDBFormatString();
            string query = "";
            if (mLedgerId.ISNullOrWhiteSpace())
            {
                query = "Select TemplateName from LadgerMain where TemplateName='" + acName + "'";
            }
            else
            {
                query = "Select TemplateName from LadgerMain "+
                        "where TemplateName='" + acName + "' and LadgerID<>'" + mLedgerId + "'";
            }
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                MessageBox.Show("This bank account already exist.", "Bank Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtLedgerName.Focus();
                return false;
            }
            return true;
        }
        private void DataSave()
        {
            string ledgerID = Guid.NewGuid().ToString();
            string bankName = cmbBankName.Text.GetDBFormatString();
            string ledgerName = txtLedgerName.Text.GetDBFormatString();
            string date = dtpDate.Text;
            string headType = cmbAcType.Text;
            string mSubAccount = "Bank A/C";
            string mParentAccount = "Current Assets";
            string mAccountType = "NULL";
            string query = "";
            if (mLedgerId.ISNullOrWhiteSpace())
            {
                query = "Insert into LadgerMain(LadgerID, LadgerName, TemplateName, Category, SubAccount, ParentAccount, Type)" +
                        "values('" + ledgerID + "','" + bankName + "','" + ledgerName + "','Bank','" +
                        mSubAccount + "','" + mParentAccount + "'," + mAccountType + ")";
                mlistQuery.Add(query);
            }
            else
            {
                query = "Update LadgerMain set LadgerName='" + bankName + "', TemplateName='" +
                        ledgerName + "' where LadgerID='" + mLedgerId + "'";
                mlistQuery.Add(query);
            }
            string bankId = ((KeyValuePair<string, string>)cmbBankName.SelectedItem).Key.ToString();
            string acno = txtAcNo.Text;
            string ifsc = txtIFSC.Text;
            string micr = txtMICR.Text;
            string branchName = txtBranchName.Text.GetDBFormatString();
            string address = txtAddress.Text.GetDBFormatString();
            string type = cmbAcType.Text;
            if (mLedgerId.ISNullOrWhiteSpace())
            {
                query = "Insert into LedgerBankDetails(LedgerID, BankID, ACNo, IFSC, MICR, BranchName, " +
                        "Address, ACType) values('" + ledgerID + "','" + bankId + "','" + acno + "','" +
                        ifsc + "','" + micr + "','" + branchName + "','" + address + "','" + type + "')";
                mlistQuery.Add(query);
            }
            else
            {
                query = "Update LedgerBankDetails set BankID='" + bankId + "', ACNo='" + acno
                        + "', IFSC='" + ifsc + "', MICR='" + micr + "', BranchName='" + branchName
                        + "',Address='" + address + "', ACType='" + type + "' where LedgerID='" + mLedgerId + "'";
                mlistQuery.Add(query);
            }
            InsertIntoLedgerStatus(ledgerID);

            if (SQLHelper.GetInstance().ExecuteTransection(mlistQuery, out msg))
            {
                LedgerTools.GetBankLedgers();
                this.Close();
            }
            else
            {
                MessageBox.Show(msg + "\nData not save.", "Bank Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InsertIntoLedgerStatus(string ledgerid)
        {
            #region data
            string openingBalancestr = txtOpeningAmount.Text;
            double openningBalance = openingBalancestr.ISNullOrWhiteSpace() ? 0d : double.Parse(openingBalancestr);
            string croDr = cmbcrdr.Text;
            openningBalance = openningBalance == 0 ? 0d : croDr.ISNullOrWhiteSpace() ? 0d : croDr == "Dr." ? openningBalance : -(openningBalance);
            string openingDate = dtpDate.Text;
            #endregion
            string query = "";
            if (mLedgerId.ISNullOrWhiteSpace())
            {
                query = "insert into LedgerStatus(LedgerID,FinYearID,OpeningBalance, " +
                        "CurrentBalance,date) values('" + ledgerid + "'," +
                        FinancialYearTools._YearID + "," + openningBalance + "," +
                        openningBalance + ",'" + openingDate + "')";
                mlistQuery.Add(query);
            }
            else
            {
                double openingPrev = 0d, closingPrev = 0d;

                GetCurrentOpeningStatus(mLedgerId, out openingPrev, out closingPrev);
                double addtoClosing = (openningBalance) - (openingPrev);
                double closingPrsnt = (closingPrev) + (addtoClosing);

                query = "Update LedgerStatus set OpeningBalance=" + openningBalance +
                        ", CurrentBalance=" + closingPrsnt + ",date='" + openingDate +
                        "' where FinYearID=" + FinancialYearTools._YearID +
                        " and LedgerID='" + mLedgerId + "'";
                mlistQuery.Add(query);
            }
        }
        private void GetCurrentOpeningStatus(string ledgerID, out double opening, out double closing)
        {
            opening = 0d; closing = 0d;
            string query = "Select * from LedgerStatus where LedgerID='" + ledgerID
                           + "' and FinYearID=" + FinancialYearTools._YearID + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                double.TryParse(dt.Rows[0]["OpeningBalance"].ToString(), out opening);
                double.TryParse(dt.Rows[0]["CurrentBalance"].ToString(), out closing);
            }
        }
        private void txtAcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void txtIFSC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)) && !(char.IsLetter(e.KeyChar)) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void txtMICR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)) && !(char.IsLetter(e.KeyChar)) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void txtOpeningAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) || e.KeyChar == '\b' || e.KeyChar == '.')
            {
                if (e.KeyChar == '.')
                {
                    char vaule = e.KeyChar;
                    string text = txtOpeningAmount.Text + vaule;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidData())
            {
                if (IsValidLedger())
                {
                    DataSave();
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddBankAccountHead_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void cmbBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bankneme = cmbBankName.Text;
            string accName = bankneme + " [" + txtAcNo.Text + "]";
            txtLedgerName.Text = accName;
        }
        private void btnAddBank_Click(object sender, EventArgs e)
        {
            BankAdd frmBankAdd = new BankAdd();
            frmBankAdd.onclose += FrmBankAdd_onclose;
            frmBankAdd.ShowDialog();
        }
        private void FrmBankAdd_onclose(string obj)
        {
            cmbBankName.AddBank();
            cmbBankName.Text = obj;
        }
        private void txtAcNo_TextChanged(object sender, EventArgs e)
        {
            string bankneme = cmbBankName.Text;
            string accName = bankneme + " [" + txtAcNo.Text + "]";
            txtLedgerName.Text = accName;
        }
    }
}
