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
    public partial class AddfinancialYear : Form
    {
        public event Action OnClose;
        string msg = "";
        private string mYearIDForEdit = "";
        public AddfinancialYear(string yearID, string saveBtnText, bool cancelBtnVisible)
        {
            InitializeComponent();
            mYearIDForEdit = yearID;
            btnSave.Text = saveBtnText;
            btnCancel.Visible = cancelBtnVisible;
            txtFyearName.Text = FinancialYearTools._YearName;
            dtmStartingDate.Text = FinancialYearTools._StartDate;
            dtmEndingDate.Text = FinancialYearTools._EndDate;
            try
            {
                dtpAccountStart.Value = FinancialYearTools._AccountDate;
            }
            catch (Exception)
            {
                dtpAccountStart.Text = FinancialYearTools._StartDate;
            }
        }
        private void DataSave()
        {
            List<string> listquery = new List<string>();

            string query = "";
            string finYear = txtFyearName.Text.GetDBFormatString();
            string startDate = dtmStartingDate.Text;
            string endDate = dtmEndingDate.Text;
            string accountStart = dtpAccountStart.Text;

            if (mYearIDForEdit.ISNullOrWhiteSpace())
            {
                query = "update FinancialYear set CurentFyear='-1' where CurentFyear='0'";
                listquery.Add(query);
                query = "update FinancialYear set CurentFyear='0' where CurentFyear='1'";
                listquery.Add(query);
                query = "Insert into FinancialYear(FinancialYearName,StartingDate, " +
                        "EndingDate,BookStart) values('" + finYear + "','" + startDate
                        + "','" + endDate + "','" + accountStart + "')";
            }
            else
            {
                query = "Update FinancialYear set FinancialYearName='" + finYear
                        + "',StartingDate='" + startDate + "',EndingDate='" + endDate
                        + "',BookStart= '" + accountStart + "' where ID=" + mYearIDForEdit + "";
            }
            listquery.Add(query);
            if (MessageBox.Show("Do you want to save ? ", "Financial Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(listquery, out msg))
                {
                    FinancialYearTools.GetYear();
                    FinancialYearTools.ISAccountDateExist();
                    FinancialYearTools.GetFinancialYearDetails();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Internal Error\n" + msg, "Financial Year", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Your data is not save.", "Financial Year", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private bool IsValidAccountStartDate()
        {
            DateTime startDate = dtmStartingDate.Value;
            DateTime endDate = dtmEndingDate.Value;
            DateTime accDate = dtpAccountStart.Value;
            if (accDate >= startDate && accDate <= endDate)
            {
                return true;
            }
            MessageBox.Show("Please enter a valid account start date.\nAccount date must be between start date and end date of financial year.", "Invalid Account Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtFyearName.Text.ISNullOrWhiteSpace())
            {
                if (IsValidAccountStartDate())
                {
                    DataSave();
                }
            }
            else
            {
                MessageBox.Show("Enter financial year.", "Financial Year", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtFyearName.Select();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddfinancialYear_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnClose?.Invoke();
        }
    }
}
