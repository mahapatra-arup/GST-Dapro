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
    public partial class ChallanList : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public ChallanList()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");

        }
        private void btnNewChallan_Click(object sender, EventArgs e)
        {
            ChallanEntry challanentry = new ChallanEntry("", "");
            challanentry.OnClose += GenerateChallanList;
            challanentry.Show(this);
        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("challanId", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("NO", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));

            dgvChallanList.DataSource = mDtTable;
            DataGridViewLinkColumn lnkCol = new DataGridViewLinkColumn();
            lnkCol.Name = "Action";
            lnkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns.Add(lnkCol);
            dgvChallanList.Columns["challanId"].Visible = false;

            dgvChallanList.Columns["Date"].HeaderText = "DATE";
            dgvChallanList.Columns["NO"].HeaderText = "NO.";
            dgvChallanList.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvChallanList.Columns["Amount"].HeaderText = "AMOUNT";

            dgvChallanList.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChallanList.Columns["NO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChallanList.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanList.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvChallanList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column in dgvChallanList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            OrderEntry frmOrderEntry = new OrderEntry();
            frmOrderEntry.OnClose += GenerateChallanList;
            frmOrderEntry.Show(this);
        }
        private void ChallanList_Load(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(null, null);
            GenerateChallanList();
        }
        private void GenerateChallanList()
        {
            mDtTable.Clear();
            int i = 0;
            string query = "SELECT Challan.*,CONVERT(varchar(11),Challandate,106) as challanDate1 ,LadgerMain.LadgerName " +
                           "FROM  Challan inner join LadgerMain on Challan.LedgerID=LadgerMain.LadgerID " +
                           "where Challandate between '" + mFromDate + "' and '" + mToDate + "' order by SlNo desc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string challanId = item["ChallanID"].ToString();
                    string challanno = item["ChallanNo"].ToString();
                    string chalandate = item["challanDate1"].ToString();
                    string partyName = item["LadgerName"].ToString();
                    float totAmount = float.Parse(item["TotalAmount"].ToString());
                    string status = item["Status"].ToString();
                    #region MyRegion
                    DataGridViewLinkCell lnkCell = new DataGridViewLinkCell();
                    //lnkCell.ToolTipText = "Click to genarate invoice.";
                    if (status == "Open")
                    {
                        lnkCell.ToolTipText = "Invoice";
                        lnkCell.Value = "Invoice";
                    }

                    #endregion

                    mDtTable.Rows.Add(challanId, chalandate, challanno, partyName, totAmount.ToString("0.00"));
                    dgvChallanList.Rows[i].Cells["Action"] = lnkCell;

                    i++;
                }
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateChallanList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateChallanList();
        }
        private void toolStripPreviousMonth_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
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
            GenerateChallanList();
        }
        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            if (!txtSearchByName.Text.ISNullOrWhiteSpace())
            {
                DataView dtv = new DataView(mDtTable);
                dtv.RowFilter = string.Format("PartyName like '{0}%'", txtSearchByName.Text);
                dgvChallanList.DataSource = dtv;


            }
            else
            {
                GenerateChallanList();
            }
        }

        /// <summary>
        /// Grid Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvChallanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChallanList.Columns[e.ColumnIndex].Name != "Action" && e.RowIndex != -1)
            {
                if (UserTools._IsEdit)
                {
                    int rowindex = dgvChallanList.SelectedCells[0].RowIndex;
                    string challanid = dgvChallanList.Rows[rowindex].Cells["challanId"].Value.ToString();
                    ChallanEntry challanEntry = new ChallanEntry(challanid);
                    challanEntry.OnClose += GenerateChallanList;
                    challanEntry.Show(this); 
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            
        }
        private void dgvChallanList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                dgvChallanList.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvChallanList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvChallanList.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvChallanList.Cursor = Cursors.Hand;
            }
        }
        private void dgvChallanList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvChallanList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvChallanList.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                dgvChallanList.Cursor = Cursors.Default;
            }
        }
        private void dgvChallanList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvChallanList.Columns[e.ColumnIndex].Name == "Action")
            {
                int rowindex = dgvChallanList.SelectedCells[0].RowIndex;
                string challanid = dgvChallanList.Rows[rowindex].Cells["challanId"].Value.ToString();
                InvoiceFrmChallan challanEntry = new InvoiceFrmChallan(InvoiceFrmChallan._Invoicefrom.Challan, challanid);
                challanEntry.OnClose += GenerateChallanList;
                challanEntry.Show(this);
            }
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateChallanList();
        }
    }
}
