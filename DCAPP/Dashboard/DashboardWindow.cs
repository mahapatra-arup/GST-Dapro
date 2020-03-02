using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class DashboardWindow : Form
    {
        private string msg = "";
        private string mFromDate, mToDate;

        public DashboardWindow()
        {
            InitializeComponent();
            AppInfo();
            ExpenseGrid();
            lblCurrentORGName.Text = ORG_Tools._OrganizationName;
            pbORGLogo.BackgroundImage = ORG_Tools._Logo;
            lblFinancialYear.Text = FinancialYearTools._YearName;
            lblCurrentPeriod.Text = FinancialYearTools._StartDate + "  To  " + FinancialYearTools._EndDate;
            lblBooksOfAccountStart.Text = "Books of Account start from : " + FinancialYearTools._AccountDate.ToString("dd-MMM-yyyy");
        }
        private void ExpenseGrid()
        {
            dgvExpenseList.Columns["LedgerName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvExpenseList.Columns["Amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void AppInfo()
        {
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            //application details
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblProductName.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblVersion.Text = fvi.FileVersion;
            lblCompanyName.Text = fvi.CompanyName;
            lblDataSource.Text = SQLHelper.mInitalCatalog;
        }
        private void GetCashBalance()
        {
            double amount = 0d;
            string query = "Select sum(CurrentBalance) from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerMain.Category ='Cash' " +
                           "and LedgerStatus.FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            btnCashBalance.Text = "Rs. " + amount.toString();
        }
        private void GetAccountReceivable()
        {
            double amount = 0d;
            string query = "Select Sum(CurrentBalance) from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerMain.subaccount ='Sundry Debtors' and ledgerstatus.currentbalance>0 " +
                           "and LedgerStatus.FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            btnAR.Text = "Rs. " + Math.Abs(amount).toString();
        }
        private void GetAccountPayable()
        {
            double amount = 0d;
            string query = "Select SUM(CurrentBalance) from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerMain.subaccount ='Sundry Creditors' and ledgerstatus.currentbalance<0  " +
                           "and LedgerStatus.FinYearID = " + FinancialYearTools._YearID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                double.TryParse(o.ToString(), out amount);
            }
            btnAP.Text = "Rs. " + Math.Abs(amount).toString();
        }
        private void GetExpenseList()
        {
            dgvExpenseList.Rows.Clear();
            double totExpense = 0f;
            string query = "Select LadgerMain.LadgerID,LadgerMain.TemplateName,SUM(Transection.Amount_Dr) as Dr from Transection " +
                            "inner join LadgerMain on Transection.LedgerIdFrom = LadgerMain.LadgerID " +
                            "where LadgerMain.Type = 'Expense' " +
                            "and Transection.Date between '" + mFromDate + "' and '" + mToDate + "' " +
                            "group by LadgerMain.LadgerID,LadgerMain.TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string acountHeadID = row["LadgerID"].ToString();
                    string acountHead = row["TemplateName"].ToString();
                    string amount = row["Dr"].ToString();
                    double amt = 0f;
                    try
                    {
                        amt = double.Parse(amount);
                    }
                    catch (Exception) { }
                    totExpense += amt;
                    amount = "Rs. " + amt.toString();
                    dgvExpenseList.Rows.Add(acountHeadID, acountHead, amount);
                }
            }
            lblTotalExpense.Text = "Rs. " + totExpense.toString();
        }
        private void btnCashBalance_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.CASH);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void DashboardWindow_Shown(object sender, EventArgs e)
        {
            GetCashBalance();
            GetAccountReceivable();
            GetAccountPayable();
            toolStripCurrentMonth_Click(null, null);
        }
        private void btnAR_Click(object sender, EventArgs e)
        {
            SubGroupReport frmCashBook = new SubGroupReport(SubGroupReport._Type.Sundry_Debtors);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void btnAP_Click(object sender, EventArgs e)
        {
            SubGroupReport frmCashBook = new SubGroupReport(SubGroupReport._Type.Sundry_Creditor);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }

        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GetExpenseList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GetExpenseList();
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
            lblFilterHeader.Text = toolStripPreviousMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GetExpenseList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GetExpenseList();
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {

        }
    }
}
