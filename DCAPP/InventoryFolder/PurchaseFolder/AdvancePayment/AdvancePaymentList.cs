using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AdvancePaymentList : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public AdvancePaymentList()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");

        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("slno", typeof(string));
            mDtTable.Columns.Add("PaymentNo", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));
            //mDtTable.Columns.Add("Status", typeof(string));

            dgvadvanceReceipt.DataSource = mDtTable;
            //DataGridViewLinkColumn lnkCol = new DataGridViewLinkColumn();
            //lnkCol.Name = "Action";
            //lnkCol.ReadOnly = false;
            //dgvadvanceReceipt.Columns.Add(lnkCol);
            dgvadvanceReceipt.Columns["slno"].HeaderText = "SL. NO.";
            dgvadvanceReceipt.Columns["PaymentNo"].HeaderText = "RECEIPT NO.";
            dgvadvanceReceipt.Columns["Date"].HeaderText = "DATE";
            dgvadvanceReceipt.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvadvanceReceipt.Columns["Amount"].HeaderText = "AMOUNT";

            dgvadvanceReceipt.Columns["slno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvadvanceReceipt.Columns["PaymentNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvadvanceReceipt.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvadvanceReceipt.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvadvanceReceipt.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvadvanceReceipt.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvadvanceReceipt.Columns["slno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["PaymentNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
           // dgvadvanceReceipt.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvadvanceReceipt.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i < dgvadvanceReceipt.Columns.Count; i++)
            {
                dgvadvanceReceipt.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void OrderWindow_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
        }
        private void dgvBills_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                dgvadvanceReceipt.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvadvanceReceipt.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvadvanceReceipt.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvadvanceReceipt.Cursor = Cursors.Hand;
            }
        }
        private void dgvBills_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvadvanceReceipt.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvadvanceReceipt.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                dgvadvanceReceipt.Cursor = Cursors.Default;
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateAdvancePaymentList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateAdvancePaymentList();
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
            GenerateAdvancePaymentList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateAdvancePaymentList();
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvadvanceReceipt.SelectedCells.Count > 0 && e.RowIndex != -1 && dgvadvanceReceipt.Columns[e.ColumnIndex].Name != "Action")
            {
                string orderID = dgvadvanceReceipt.CurrentRow.Cells["OrderID"].Value.ToString();
                string status = dgvadvanceReceipt.CurrentRow.Cells["Status"].Value.ToString();
                OrderEntry frmOrderEntry = new OrderEntry(orderID, status);
                frmOrderEntry.OnClose += GenerateAdvancePaymentList;
                frmOrderEntry.ShowDialog();
            }

        }

        private void dgvadvanceReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex>=0 && e.ColumnIndex>=0 && dgvadvanceReceipt.Columns[e.ColumnIndex].Name=="Action")
            //{
            //    string advanceNoteno = dgvadvanceReceipt.CurrentRow.Cells["ReceiptNo"].Value.ToString();
            //    CreditNoteIssue crnoteissu = new CreditNoteIssue(CreditNoteIssue._NoteType.Refund_Voucher, advanceNoteno);
            //    crnoteissu.OnClose += GenerateAdvancePaymentList;
            //    crnoteissu.ShowDialog();
            //}
        }

        private void dgvadvanceReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0&&e.ColumnIndex>=0)
            {
                if (UserTools._IsEdit)
                {
                    string paymentno = dgvadvanceReceipt.Rows[e.RowIndex].Cells["PaymentNo"].Value.ToString();
                    AdvancePayment adreceipt = new AdvancePayment(paymentno);
                    adreceipt.Onclose += GenerateAdvancePaymentList;
                    adreceipt.ShowDialog(); 
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void GenerateAdvancePaymentList()
        {
            mDtTable.Clear();
            int i = 1;
            string query = "SELECT  AdvancePayment.PaymentNo,convert(varchar(11),AdvancePayment.PaymentDate,106) as Date, " +
                           "AdvancePayment.Total,AdvancePayment.Status,Ledgers. LedgerName " +
                           "FROM  AdvancePayment inner join Ledgers on AdvancePayment.LedgerID=Ledgers.LedgerID where AdvancePayment.PaymentDate between '" +
                           mFromDate + "' and '" + mToDate + "' order by AdvancePayment.PaymentNo desc ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string paymentNo = item["PaymentNo"].ToString();
                    string paymentDate = item["Date"].ToString();
                    string partyName = item["LedgerName"].ToString();
                    float totAmount = float.Parse(item["Total"].ToString());
                    string status = item["Status"].ToString();

                    mDtTable.Rows.Add(i++,paymentNo, paymentDate, partyName, totAmount.ToString("0.00"));
                }
            }
        }
    }
}
