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
    public partial class CashBook : Form
    {
        private string msg = "";
        private string mFromDate, mToDate;
        private double mOpeningBalance = 0d;
        string mSubQuery = "";

        public enum _Type
        {
            CASH,
            BANK,
            SALES,
            PURCHASE,
            SALES_RETURN,
            PURCHASE_RETURN
        }
        private _Type mType;
        public CashBook(_Type type)
        {
            InitializeComponent();
            GridDesign();
            mType = type;
            switch (mType)
            {
                case _Type.CASH:
                    lblHeader.Text = "Cash Statement";
                    break;
                case _Type.BANK:
                    lblHeader.Text = "Bank Statement";
                    break;
                case _Type.SALES:
                    lblHeader.Text = "Sales Register";
                    break;
                case _Type.PURCHASE:
                    lblHeader.Text = "Purchase Register";
                    break;
                case _Type.SALES_RETURN:
                    lblHeader.Text = "Sales Return Register";
                    break;
                case _Type.PURCHASE_RETURN:
                    lblHeader.Text = "Purchase Return Register";
                    break;
                default:
                    break;
            }
        }
        private void GridDesign()
        {
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
        private void btnStatement_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        /// <summary>
        /// Multiple  ledgers opening
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private double GetLedgersOpeningBalanceByDate(string date)
        {
            double opening = GetLedgersOpeningBalanceByFinYear();
            double totOpening = 0d;
            string query = "Select (CASE WHEN debit IS NULL THEN 0 ELSE debit END) " +
                           "     - (CASE WHEN credit IS NULL THEN 0 ELSE credit END) FROM("+
                           "Select sum (Amount_Dr)as debit,sum(Amount_Cr)as credit from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom in(" + mSubQuery + ") " +
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
                           "where LadgerID in(" + mSubQuery + ") " +
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
            mFromDate = dtpFromDate.Text;
            mToDate = dtpToDate.Text;
            lblCreditBalance.Text = "";
            lblDebitBalance.Text = "";
            lblDebit.Text = "";
            lblCredit.Text = "";
            switch (mType)
            {
                case _Type.CASH:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Cash'";
                    break;
                case _Type.BANK:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Bank'";
                    break;
                case _Type.SALES:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Sales'";
                    break;
                case _Type.PURCHASE:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Purchase'";
                    break;
                case _Type.SALES_RETURN:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Sales_Return'";
                    break;
                case _Type.PURCHASE_RETURN:
                    mSubQuery = "Select LadgerID from LadgerMain where Category='Purchase_Return'";
                    break;
                default:
                    break;
            }
            string query = "Select convert(varchar(11),Transection.Date,106) as date, " +
                           "Transection.LedgerIdFrom,Transection.TransectionType,Transection.No, " +
                           "Transection.Mode,Transection.ChequeNo,Transection.Amount_Dr, " +
                           "Transection.Amount_Cr,LadgerMain.TemplateName from Transection " +
                           "Inner join LadgerMain on Transection.LedgerIdTo=LadgerMain.LadgerID " +
                           "where LedgerIdFrom in(" + mSubQuery + ") " +
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
            mOpeningBalance = GetLedgersOpeningBalanceByDate( openingDate.ToString("dd-MMM-yyyy"));
            double closing = mOpeningBalance;
            int slno = 1;
            double totDebit = 0d, totCredit = 0d;

            if (mOpeningBalance > 0)
            {
                totDebit += mOpeningBalance;
                dgvStatement.Rows.Add("", slno++, mFromDate, "To Opening Balance", "", "",
                                         "", "", mOpeningBalance.toString(), "", mOpeningBalance.toString());
            }
            else if(mOpeningBalance < 0)
            {
                totCredit += mOpeningBalance;
                dgvStatement.Rows.Add("", slno++, mFromDate, "To Opening Balance", "", "",
                                         "", "", "",Math.Abs(mOpeningBalance).toString(), mOpeningBalance.toString());
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
            lblDebit.Text = totDebit.ToString("0.00");
            lblCredit.Text = totCredit.ToString("0.00");
            lblBalanceDate.Text = mToDate;
            if (closing >= 0)
            {
                lblDebitBalance.Text = closing.ToString("0.00");
            }
            else
            {
                lblCreditBalance.Text = Math.Abs(closing).ToString("0.00");
            }

            #endregion
        }
        private void CashBook_Shown(object sender, EventArgs e)
        {
            FrmCreateCompany_onClose();
            toolStripCurrentMonth_Click(null, null);
            GenerateReport();
        }
        private void FrmCreateCompany_onClose()
        {
            if (!LedgerTools.GetCashLedgers())
            {
                AddCashAccount frmAddCashAccount = new AddCashAccount();
                frmAddCashAccount.OnClose += GenerateReport;
                frmAddCashAccount.WindowState = FormWindowState.Normal;
                frmAddCashAccount.ShowDialog();
            }
        }
    }
}
