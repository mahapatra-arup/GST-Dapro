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
    public partial class SubGroupReport : Form
    {
        private string msg = "";
        private string mFromDate, mToDate;
        public enum _Type
        {
            Sundry_Creditor,
            Sundry_Debtors,
            SALES_RETURN,
            PURCHASE_RETURN
        }
        private _Type mType;
        public SubGroupReport(_Type type)
        {
            InitializeComponent();
            GridDesign();
            mType = type;
            switch (mType)
            {
                case _Type.Sundry_Debtors:
                    lblHeader.Text = "Sundry Debtors";
                    break;
                case _Type.Sundry_Creditor:
                    lblHeader.Text = "Sundry Creditors";
                    break;
                case _Type.SALES_RETURN:
                    lblHeader.Text = "Sales Return";
                    break;
                case _Type.PURCHASE_RETURN:
                    lblHeader.Text = "Sundry Creditors";
                    break;
                default:
                    break;
            }
        }
        private void GridDesign()
        {
            dgvStatement.Columns["SlnoDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatement.Columns["LedgerNameDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStatement.Columns["AmountDr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStatement.Columns["AmountCr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        private void GenerateReport()
        {
            mFromDate = dtpFromDate.Text;
            mToDate = dtpToDate.Text;
            lblTotalAmountCr.Text = "";
            string subAccount = "";
            switch (mType)
            {
                case _Type.Sundry_Debtors:
                    lblHeader.Text = "Sundry Debtors";
                    subAccount = "Sundry Debtors";
                    break;
                case _Type.Sundry_Creditor:
                    lblHeader.Text = "Sundry Creditors";
                    subAccount = "Sundry Creditors";
                    break;
                default:
                    break;
            }
            string query = "Select LadgerMain.LadgerID,LadgerMain.TemplateName,LedgerStatus.CurrentBalance from LadgerMain " +
                           "Inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where LadgerMain.SubAccount='" + subAccount + "' order by LadgerMain.TemplateName";

            ShowStatement(query);
        }
        private void SubGroupReport_Shown(object sender, EventArgs e)
        {
            toolStripCurrentFinYear_Click(null, null);
            GenerateReport();
        }
        private void ShowStatement(string query)
        {
            dgvStatement.Rows.Clear();

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            double totAmountDr = 0d, totAmountCr = 0d;
            if (dt.IsValidDataTable())
            {
                int slno = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string ladgerID = row["LadgerID"].ToString();
                    string ledgerName = row["TemplateName"].ToString();
                    string amountStr = row["CurrentBalance"].ToString();
                    double amount = 0d;
                    double.TryParse(amountStr, out amount);
                    if (amount >= 0)
                    {
                        totAmountDr += Math.Abs(amount);
                        dgvStatement.Rows.Add(ladgerID, slno++, ledgerName, Math.Abs(amount).toString(), null);
                    }
                    else
                    {
                        totAmountCr += Math.Abs(amount);
                        dgvStatement.Rows.Add(ladgerID, slno++, ledgerName,null, Math.Abs(amount).toString());
                    }
                }
            }
            lblTotalAmountDr.Text = totAmountDr.toString();
            lblTotalAmountCr.Text = totAmountCr.toString();
        }
    }
}
