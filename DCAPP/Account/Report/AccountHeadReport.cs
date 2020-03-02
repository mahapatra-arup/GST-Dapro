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
    public partial class AccountHeadReport : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        private string mLadgerID;
        public AccountHeadReport()
        {
            InitializeComponent();
            InitTable();
        }
        public AccountHeadReport(string ladgerID,string dateFrom,string dateTo)
        {
            InitializeComponent();
            InitTable();
            mFromDate = dateFrom;
            mToDate = dateTo;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
            mLadgerID = ladgerID;
        }
        private void GenerateReport()
        {
            mDtTable.Rows.Clear();
            double balanceAmount = 0d;
            if (!cmbAccountHead.Text.ISNullOrWhiteSpace())
            {
                string accountHeadID = ((KeyValuePair<string, string>)cmbAccountHead.SelectedItem).Key.ToString();
                string query = "Select TransectionId,SlNo,TransectionType,convert(varchar(11),Date,106) as 'Date', " +
                               "Amount,LedgerName,AccountHeadName,Purpose from LadgerTransectionView " +
                               "where LedgerID='" + accountHeadID + "' and date between '" + mFromDate + "' and '" + mToDate + "' " +
                               "and TransectionType in('BillPayment','AdvancePayment','Expense') order by date";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query,out msg);
                if (dt.IsValidDataTable())
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string transectioId = item["TransectionId"].ToString();
                        string date = item["Date"].ToString();
                        string type = item["TransectionType"].ToString();
                        string refNo =  item["SlNo"].ToString();
                        string supplierName = item["LedgerName"].ToString();
                        string description = item["Purpose"].ToString();
                        string accountHead = item["AccountHeadName"].ToString();
                        float amount = 0f;
                        try { amount = float.Parse(item["Amount"].ToString()); }
                        catch (Exception) { }
                        balanceAmount += amount;
                        mDtTable.Rows.Add(transectioId, date, type, refNo, supplierName, description, accountHead, amount.ToString("0.00"), balanceAmount.ToString("0.00"));
                    }
                }
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblDate.Text = toolStripToday.Text;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblDate.Text = toolStripCurrentMonth.Text;
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
            lblDate.Text = toolStripPreviousMonth.Text;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblDate.Text = toolStripCurrentFinYear.Text;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void cmbAccountHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void AccountHeadReport_Shown(object sender, EventArgs e)
        {
            cmbAccountHead.AddSuppliers();
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
            cmbAccountHead.SelectedValue = mLadgerID;
        }
        private void dgvExpenseList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void InitTable()
        {
            mDtTable = new DataTable();
            mDtTable.Columns.Add("TransectionID", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("Type", typeof(string));
            mDtTable.Columns.Add("Refno", typeof(string));
            mDtTable.Columns.Add("Name", typeof(string));
            mDtTable.Columns.Add("Description", typeof(string));
            mDtTable.Columns.Add("AccountHead", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));
            mDtTable.Columns.Add("Balance", typeof(string));
            dgvExpenseList.DataSource = mDtTable;

            dgvExpenseList.Columns["TransectionID"].Visible = false;
            dgvExpenseList.Columns["AccountHead"].HeaderText = "Account Head";

            dgvExpenseList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvExpenseList.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvExpenseList.Columns["Refno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvExpenseList.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvExpenseList.Columns["Balance"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvExpenseList.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExpenseList.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
