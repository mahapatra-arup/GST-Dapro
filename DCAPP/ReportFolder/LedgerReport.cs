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
    public partial class LedgerReport : Form
    {
        private string msg = "";
        private string mFromDate, mToDate;
        private string mLadgerID;
        private double mOpeningBalance = 0d;
        public enum _LedgerCategory
        {
            All,
            Party,
            Account,
            Sales,
            Purchase,
            SundryDebtors,
            SundryCreditors,
            Customer,
            Supplier,
            Cash,
            Bank,
            Other
        }
        private _LedgerCategory mCategory;
        public LedgerReport(_LedgerCategory category)
        {
            InitializeComponent();
            mCategory = category;
            AddLedgers();
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
        }
        public LedgerReport(_LedgerCategory category, string ledgerName, string dateFrom, string dateTo)
        {
            InitializeComponent();
            mFromDate = dateFrom;
            mToDate = dateTo;
            mCategory = category;
            AddLedgers();
            cmbLedgers.Text = ledgerName;
        }
        private void LedgerReport_Shown(object sender, EventArgs e)
        {
            dgvDebit.Columns["LedgerNameDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCredit.Columns["ledgerNameCr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDebit.Columns["AmountDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCredit.Columns["AmountCr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            GenerateReport();
        }
        private void AddLedgers()
        {
            switch (mCategory)
            {
                case _LedgerCategory.All:
                    cmbLedgers.ADDAllLedgers();
                    break;
                case _LedgerCategory.Party:
                    lblHeader.Text = "Party Report";
                    cmbLedgers.AddPartyLedgers();
                    break;
                case _LedgerCategory.Account:
                    cmbLedgers.AddAccountLedgers();
                    break;
                case _LedgerCategory.Sales:
                    break;
                case _LedgerCategory.Purchase:
                    break;
                case _LedgerCategory.SundryDebtors:
                    lblHeader.Text = "Sundry Debtors";
                    break;
                case _LedgerCategory.SundryCreditors:
                    lblHeader.Text = "Sundry Creditors";
                    break;
                case _LedgerCategory.Customer:
                    cmbLedgers.ADDCustomerLedgers();
                    break;
                case _LedgerCategory.Supplier:
                    cmbLedgers.ADDSupplierLedgers();
                    break;
                case _LedgerCategory.Cash:
                    break;
                case _LedgerCategory.Bank:
                    break;
                case _LedgerCategory.Other:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Multiple  ledgers opening
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private double GetLedgersOpeningBalanceByDate(string ledgerID, string date)
        {
            double opening = GetLedgersOpeningBalanceByFinYear(ledgerID);
            double totOpening = 0d;
            string query = "Select (CASE WHEN debit IS NULL THEN 0 ELSE debit END) " +
                           "     - (CASE WHEN credit IS NULL THEN 0 ELSE credit END) from( " +
                           "Select sum (Amount_Dr)as debit,sum(Amount_Cr)as credit from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom in('" + ledgerID + "') " +
                           "and Date between '" + FinancialYearTools._StartDate +
                           "' and '" + date + "') as TempTable";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out totOpening);
            }
            return (totOpening + opening);
        }
        private double GetLedgersOpeningBalanceByFinYear(string ledgerID)
        {
            double amount = 0d;
            string query = "Select OpeningBalance from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerID in('" + ledgerID + "') " +
                           "and FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            return amount;
        }
        private void GenerateReport()
        {
            if (!cmbLedgers.Text.ISNullOrWhiteSpace())
            {
                #region Set Opening
                DateTime openingDate;
                DateTime.TryParse(mFromDate, out openingDate);
                openingDate = openingDate.AddDays(-1);
                mOpeningBalance = GetLedgersOpeningBalanceByDate(mLadgerID, openingDate.ToString("dd-MMM-yyyy"));
                double closing = mOpeningBalance;
                double totDebit = 0d, totCredit = 0d;
                dgvDebit.Rows.Clear();
                dgvCredit.Rows.Clear();
                if (mOpeningBalance > 0)
                {
                    totDebit = mOpeningBalance;
                    dgvDebit.Rows.Add(mLadgerID, mFromDate, "To Opening Balance", "", totDebit.toString());
                }
                else if (mOpeningBalance < 0)
                {
                    totCredit =Math.Abs(mOpeningBalance);
                    dgvCredit.Rows.Add(mLadgerID, mFromDate, "To Opening Balance", "", totCredit.toString());
                }
                #endregion
                totDebit += ShowDebitAccount(mLadgerID);
                totCredit += ShowCreditAccount(mLadgerID);
                double atjstAmt = totDebit - totCredit;
                if (atjstAmt < 0)
                {
                    lblAtjstDr.Text = Math.Abs(atjstAmt).toString();
                    totDebit += Math.Abs(atjstAmt);
                }
                else if (atjstAmt > 0)
                {
                    lblAtjstCr.Text = Math.Abs(atjstAmt).toString();
                    totCredit += Math.Abs(atjstAmt);
                }
                lblDebit.Text = totDebit.toString();
                lblCredit.Text = totCredit.toString();
            }
        }
        private double ShowDebitAccount(string ledgerID)
        {
            double totDebit = 0d;
            string query = "Select convert(varchar(11),Transection.Date,106) as date,Transection.Amount_Dr, " +
                           "Transection.LedgerIdFrom,Transection.No,LadgerMain.TemplateName from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom='" + ledgerID + "' and Amount_Dr is not null " +
                           "and Date between '" + mFromDate + "' and '" + mToDate + "' order by Transection.Slno";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    double amount = 0d;
                    string date = row["Date"].ToString();
                    string voucherNo = row["No"].ToString();
                    string ledgerId = row["LedgerIdFrom"].ToString();
                    string ledgerName = row["TemplateName"].ToString();
                    string amountStr = row["Amount_Dr"].ToString();
                    double.TryParse(amountStr, out amount);
                    totDebit += amount;
                    dgvDebit.Rows.Add(ledgerID, date, ledgerName, voucherNo, amount.toString());
                }
            }
            return totDebit;
        }
        private double ShowCreditAccount(string ledgerID)
        {
            double totCredit = 0d;

            string query = "Select convert(varchar(11),Transection.Date,106) as date,Transection.Amount_Cr, " +
                             "Transection.LedgerIdFrom,Transection.No,LadgerMain.TemplateName from Transection " +
                             "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                             "where LedgerIdFrom='" + ledgerID + "' and Amount_Cr is not null " +
                             "and Date between '" + mFromDate + "' and '" + mToDate + "' order by Transection.Slno";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    double amount = 0d;
                    string date = row["Date"].ToString();
                    string voucherNo = row["No"].ToString();
                    string ledgerId = row["LedgerIdFrom"].ToString();
                    string ledgerName = row["TemplateName"].ToString();
                    string amountStr = row["Amount_Cr"].ToString();
                    double.TryParse(amountStr, out amount);
                    totCredit += amount;
                    dgvCredit.Rows.Add(ledgerID, date, ledgerName, voucherNo, amount.toString());
                }
            }
            return totCredit;
        }
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
        private void btnRunReport_Click(object sender, EventArgs e)
        {
            mLadgerID = "";
            lblAtjstDr.Text = "";
            lblAtjstCr.Text = "";
            mFromDate = dtpFromDate.Text;
            mToDate = dtpToDate.Text;
            if (!cmbLedgers.Text.ISNullOrWhiteSpace())
            {
                mLadgerID = ((KeyValuePair<string, string>)cmbLedgers.SelectedItem).Key.ToString();
            }
            GenerateReport();
        }
    }
}
