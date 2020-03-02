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
    public partial class BankWindow : Form
    {
        private string msg = "";
        private string mFromDate, mToDate;
        private string mLadgerID;
        private double mOpeningBalance = 0d;
        public BankWindow()
        {
            InitializeComponent();
            dgvStatement.Columns["SlnoDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatement.Columns["LedgerNameDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStatement.Columns["VoucherType"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStatement.Columns["NoDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStatement.Columns["TransectionType"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStatement.Columns["ChequeNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatement.Columns["AmountDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStatement.Columns["AmountCredit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStatement.Columns["BalanceStmt"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void BankWindow_Shown(object sender, EventArgs e)
        {
            GetAllBankDetails();
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
        }
        private void btnAddNewBank_Click(object sender, EventArgs e)
        {
            AddBankLedger frmAddBankLedger = new AddBankLedger();
            frmAddBankLedger.OnClose += GetAllBankDetails;
            frmAddBankLedger.ShowDialog();
        }
        private void GetAllBankDetails()
        {
            dgvBank.Rows.Clear();
            double totBankAmount = 0f;
            string query = "Select LadgerMain.LadgerID,LadgerMain.TemplateName,bank.BankName,LedgerBankDetails.*,LedgerStatus.CurrentBalance from LadgerMain " +
                           "inner join LedgerBankDetails on LadgerMain.LadgerID = LedgerBankDetails.LedgerID " +
                           "inner join Bank on LedgerBankDetails.BankID=Bank.ID " +
                           "inner join LedgerStatus on LadgerMain.LadgerID = LedgerStatus.LedgerID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ledgerID = row["LadgerID"].ToString();
                    string templateName = row["TemplateName"].ToString();
                    string bankName = row["BankName"].ToString();
                    string branch = row["BranchName"].ToString();
                    string ifsc = row["IFSC"].ToString();
                    string accNo = row["ACNo"].ToString();
                    double amount = 0f;
                    double.TryParse(row["CurrentBalance"].ToString(), out amount);
                    totBankAmount += amount;
                    string amountStr = "Rs. " + amount.toString();
                    dgvBank.Rows.Add(ledgerID, templateName, bankName, branch, ifsc, accNo, amountStr);
                }
            }
            lblTotalBank.Text = "Rs. " + totBankAmount.toString();
        }

        /// <summary>
        /// Date Filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripPreviousMonth_Click(object sender, EventArgs e)
        {
            int month = 1, year = 0000;
            if (DateTime.Now.Month > 1)
            {
                month = DateTime.Now.Month - 1;
                year = DateTime.Now.Year;
            }
            else
            {
                month = 12;
                year = DateTime.Now.Year - 1;
            }

            mFromDate = "01-" + month.GetMonthMMM() + "-" + year;
            mToDate = DateTime.DaysInMonth(year, month) + "-" + month.GetMonthMMM() + "-" + year;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }

        /// <summary>
        /// Bank Statement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private double GetLedgersOpeningBalanceByDate(string date)
        {
            double opening = GetLedgersOpeningBalanceByFinYear();
            double totOpening = 0d;
            string query = "Select (CASE WHEN debit IS NULL THEN 0 ELSE debit END) " +
                           "     - (CASE WHEN credit IS NULL THEN 0 ELSE credit END) FROM(" +
                           "Select sum (Amount_Dr)as debit,sum(Amount_Cr)as credit from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom in('" + mLadgerID + "') " +
                           "and Date between '" + FinancialYearTools._StartDate + "' and '"
                           + date + "') as TempTable";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out totOpening);
            }
            return (totOpening + opening);
        }
        private double GetLedgersOpeningBalanceByFinYear()
        {
            double amount = 0d;
            string query = "Select OpeningBalance from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerID in('" + mLadgerID + "') " +
                           "and FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            return amount;
        }
        private void btnStatement_Click(object sender, EventArgs e)
        {
            mLadgerID = "";
            if (dgvBank.SelectedRows.Count > 0)
            {
                mLadgerID = dgvBank.SelectedRows[0].Cells["LedgerID"].Value.ToString();
            }
            GenerateReport();
        }
        private void GenerateReport()
        {
            mFromDate = dtpFromDate.Text;
            mToDate = dtpToDate.Text;
            lblCreditBalance.Text = "";
            lblDebitBalance.Text = "";
            lblDebit.Text = "";
            lblCredit.Text = "";
           
            string query = "Select convert(varchar(11),Transection.Date,106) as date, " +
                           "Transection.LedgerIdFrom,Transection.TransectionType,Transection.No, " +
                           "Transection.Mode,Transection.ChequeNo,Transection.Amount_Dr, " +
                           "Transection.Amount_Cr,LadgerMain.TemplateName from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom in('" + mLadgerID + "') " +
                           "and Date between '" + mFromDate + "' and '" + mToDate + "' order by Transection.SlNo";

            ShowStatement(query);
        }
        private void ShowStatement(string query)
        {
            dgvStatement.Rows.Clear();

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            ///Set Opening
            #region Set Opening
            DateTime openingDate;
            DateTime.TryParse(mFromDate, out openingDate);
            openingDate = openingDate.AddDays(-1);
            mOpeningBalance = GetLedgersOpeningBalanceByDate(openingDate.ToString("dd-MMM-yyyy"));
            double closing = mOpeningBalance;
            int slno = 1;
            double totDebit = 0d, totCredit = 0d;

            if (mOpeningBalance > 0)
            {
                totDebit += mOpeningBalance;
                dgvStatement.Rows.Add("", slno++, mFromDate, "To Opening Balance", "", "",
                                         "", "", mOpeningBalance.toString(), "", mOpeningBalance.toString());
            }
            else if (mOpeningBalance < 0)
            {
                totCredit += mOpeningBalance;
                dgvStatement.Rows.Add("", slno++, mFromDate, "To Opening Balance", "", "",
                                         "", "", "", mOpeningBalance.toString(), mOpeningBalance.toString());
            }
            #endregion
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    double debit = 0d, credit = 0d;
                    string date = row["Date"].ToString();
                    string voucherType = row["TransectionType"].ToString();
                    string voucherNo = row["No"].ToString();
                    string ledgerId = row["LedgerIdFrom"].ToString();
                    string ledgerName = row["TemplateName"].ToString();
                    object amountDr = row["Amount_Dr"];
                    bool flag = amountDr.ISValidObject() ? double.TryParse(amountDr.ToString(), out debit) : false;
                    object amountCr = row["Amount_Cr"];
                    flag = amountCr.ISValidObject() ? double.TryParse(amountCr.ToString(), out credit) : false;
                    object transectionMode = row["Mode"];
                    object chkNo = row["ChequeNo"];
                    closing = (closing + debit) - credit;
                    totDebit += debit;
                    totCredit += credit;
                    dgvStatement.Rows.Add(ledgerId, slno++, date, ledgerName, voucherType, voucherNo,
                                          transectionMode, chkNo, amountDr.toRound(), amountCr.toRound(), closing.toString());
                }
            }
            #region Closing
            lblDebit.Text = totDebit.toString();
            lblCredit.Text = totCredit.toString();
            lblBalanceDate.Text = mToDate;
            if (closing >= 0)
            {
                lblDebitBalance.Text = closing.toString();
            }
            else
            {
                lblCreditBalance.Text = Math.Abs(closing).toString();
            }

            #endregion
        }
        private void TotalDebit_Credit(string ledgerID)
        {
            double totDebit = 0d, totCredit = 0d;
            string query = "Select sum (Amount_Dr) as Debit,sum(Amount_Cr) as Credit from Transection " +
                         "where LedgerIdFrom='" + ledgerID + "' " +
                         "and Date between '" + mFromDate + "' and '" + mToDate + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                double.TryParse(dt.Rows[0]["Debit"].ToString(), out totDebit);
                double.TryParse(dt.Rows[0]["Credit"].ToString(), out totCredit);
            }
            lblDebit.Text = totDebit.ToString("0.00");
            lblCredit.Text = totCredit.ToString("0.00");
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (dgvBank.SelectedRows.Count > 0)
            {
                mLadgerID = dgvBank.SelectedRows[0].Cells["LedgerID"].Value.ToString();
                string ledgerName = dgvBank.SelectedRows[0].Cells["TemplateName"].Value.ToString();
                BankTransection frmBankTransection = new BankTransection(BankTransection._Mode.Deposit, mLadgerID, ledgerName);
                frmBankTransection.OnClose += GetAllBankDetails;
                frmBankTransection.ShowDialog();
            }
        }

        private void dgvBank_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBank.SelectedRows.Count>0)
            {
                string ledgerID = dgvBank.SelectedRows[0].Cells["LedgerID"].Value.ToString();
                AddBankLedger frm = new DAPRO.AddBankLedger(ledgerID);
                frm.OnClose += GetAllBankDetails;
                frm.ShowDialog();
            }
        }

        private void btnWithdrawal_Click(object sender, EventArgs e)
        {
            if (dgvBank.SelectedRows.Count > 0)
            {
                mLadgerID = dgvBank.SelectedRows[0].Cells["LedgerID"].Value.ToString();
                string ledgerName = dgvBank.SelectedRows[0].Cells["TemplateName"].Value.ToString();
                BankTransection frmBankTransection = new BankTransection(BankTransection._Mode.Withdrawal, mLadgerID, ledgerName);
                frmBankTransection.OnClose += GetAllBankDetails;
                frmBankTransection.ShowDialog();
            }
        }
    }
}
