using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class TransectionList : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public TransectionList()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();
            //Slno,BillNo,MemoNo,PartyName,BillingDate,DueDate,TotalAmount,DueAmount,Status
            mDtTable.Columns.Add("Slno", typeof(string));
            mDtTable.Columns.Add("BillNo", typeof(string));
            mDtTable.Columns.Add("MemoNo", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("BillingDate", typeof(string));
            mDtTable.Columns.Add("DueDate", typeof(string));
            mDtTable.Columns.Add("TotalAmount", typeof(string));
            mDtTable.Columns.Add("DueAmount", typeof(string));
            mDtTable.Columns.Add("Status", typeof(string));

            dgvBills.DataSource = mDtTable;

            DataGridViewTextBoxColumn txtCell = new DataGridViewTextBoxColumn();
            txtCell.Name = "Action";
            txtCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns.Add(txtCell);

            dgvBills.Columns["Slno"].Visible = false;
            //Slno,BillNo,MemoNo,PartyName,BillingDate,DueDate,TotalAmount,DueAmount,Status

            dgvBills.Columns["BillNo"].ReadOnly = true;
            dgvBills.Columns["MemoNo"].ReadOnly = true;
            dgvBills.Columns["PartyName"].ReadOnly = true;
            dgvBills.Columns["BillingDate"].ReadOnly = true;
            dgvBills.Columns["DueDate"].ReadOnly = true;
            dgvBills.Columns["TotalAmount"].ReadOnly = true;
            dgvBills.Columns["DueAmount"].ReadOnly = true;
            dgvBills.Columns["Status"].ReadOnly = true;
            dgvBills.Columns["Action"].ReadOnly = false;
            //Slno,BillNo,MemoNo,PartyName,BillingDate,DueDate,TotalAmount,DueAmount,Status

            dgvBills.Columns["BillNo"].HeaderText = "BILLING NO.";
            dgvBills.Columns["MemoNo"].HeaderText = "MEMO NO.";
            dgvBills.Columns["PartyName"].HeaderText = "PAYEE NAME";
            dgvBills.Columns["BillingDate"].HeaderText = "BILLING DATE";
            dgvBills.Columns["DueDate"].HeaderText = "DUE DATE";
            dgvBills.Columns["TotalAmount"].HeaderText = "TOTAL AMOUNT";
            dgvBills.Columns["DueAmount"].HeaderText = "DUE AMOUNT";
            dgvBills.Columns["Status"].HeaderText = "STATUS";

            //Slno,BillNo,MemoNo,PartyName,BillingDate,DueDate,TotalAmount,DueAmount,Status

            dgvBills.Columns["BillNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["MemoNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["PartyName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["BillingDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["DueDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBills.Columns["DueAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBills.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBills.Columns["BillNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["MemoNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["BillingDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["DueDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["TotalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["DueAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void GenerateBillingList()
        {
            mDtTable.Clear();
            int i = 0;
             
            string query = "Select Expense.id,SlNo,BillNo, convert(varchar(11), BillingDate, 106) as BillingDate, "+
                           "convert(varchar(11), DueDate, 106) as DueDate,MemoNo " +
                           ",TotalAmount,DueAmount,Status,RCM, LadgerMain.TemplateName from Expense "+
                           "inner join LadgerMain on  Expense.LedgerId = LadgerMain.LadgerID "+
                           "where BillingDate between '" + mFromDate + "' and '" + mToDate 
                           + "'  order by Expense.id desc ";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string slno = item["SlNo"].ToString();
                    string billno = item["BillNo"].ToString();
                    string memono = item["MemoNo"].ToString();
                    string partyname = item["TemplateName"].ToString();
                    string billingdate = item["BillingDate"].ToString();
                    DateTime duedate = DateTime.Parse(item["DueDate"].ToString());
                    double totAmount = double.Parse(item["TotalAmount"].ToString());
                    double dueamount = double.Parse(item["DueAmount"].ToString());
                    string status = item["Status"].ToString();

                    #region MyRegion

                    Color clr = new Color();
                    clr = Color.Black;
                    DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();

                    cmbCell.Style.Font = new Font("Arial", 8f);
                    cmbCell.Style.ForeColor = Color.Blue;
                    //cmbCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cmbCell.FlatStyle = FlatStyle.Flat;
                    cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                    if (status != "Cancel")
                    {
                        cmbCell.Items.Add("  <Select>  ");
                        cmbCell.Items.Add("Bill Payment");
                        cmbCell.Items.Add("Cancel");

                        if (dueamount > 0)
                        {
                            if ( duedate >= DateTime.Now.Date)
                            {
                                clr = Color.Orange;
                                status = "Due";
                            }
                            else
                            {
                                clr = Color.Red;
                                status = "Overdue";
                            }
                            cmbCell.Value = "  <Select>  ";
                        }
                        else
                        {
                            cmbCell.Items.RemoveAt(1);
                            cmbCell.Items.RemoveAt(1);
                            clr = Color.ForestGreen;
                            status = "Paid";
                        }
                    }
                    else
                    {
                        clr = Color.DeepPink;
                        status = "Cancel";
                    }

                    #endregion

                    mDtTable.Rows.Add(slno, billno, memono, partyname, billingdate, duedate,
                                      totAmount.toString(), dueamount.toString());
                    dgvBills.Rows[i].Cells["Action"] = cmbCell;
                    dgvBills.Rows[i].Cells["Status"].Value = status;
                    dgvBills.Rows[i].Cells["Status"].Style.ForeColor = clr;
                    i++;
                }
            }
        }
        //private string GetAccountHead(string billID)
        //{
        //    string query = "Select * from BillDetailsSub " +
        //                  "inner join AccountHead on BillDetailsSub.AccountHeadID=AccountHead.AccountHeadID " +
        //                  "Where BillDetailsSub.BillID='" + billID + "'";
        //    DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
        //    if (dt.IsValidDataTable())
        //    {
        //        if (dt.Rows.Count == 1)
        //        {
        //            return dt.Rows[0]["AccountHeadName"].ToString();
        //        }
        //    }
        //    return "-----";
        //}
        private float GetBalance(string billID, float totalAmount, out string status)
        {
            status = "";
            string query = "Select Sum(Amount) from Transection " +
                           "Where BillDetailsID='" + billID + "' and TransectionType<>'BILL'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                float dueAmount = 0f;
                try
                {
                    dueAmount = totalAmount - float.Parse(o.ToString());
                }
                catch (Exception)
                {
                }
                if (dueAmount > 0)
                {
                    status = "Open";
                    return dueAmount;
                }
                else
                {
                    status = "Paid";
                    return dueAmount;
                }

            }
            status = "Open";
            return totalAmount;
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateBillingList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateBillingList();
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
            GenerateBillingList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateBillingList();
        }
        private void dgvBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void BillWindow_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
        }
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            BillEntry objBill = new BillEntry();
            objBill.OnClose += GenerateBillingList;
            objBill.Show(this);
        }
        private void btnAdvance_Click(object sender, EventArgs e)
        {
            AdvancePaymentWindow objBill = new AdvancePaymentWindow();
            objBill.OnClose += GenerateBillingList;
            objBill.Show(this);
        }
        private void TransectionWindow_Load(object sender, EventArgs e)
        {

        }
        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        private void lblFilterHeader_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBills.Columns[dgvBills.CurrentCell.ColumnIndex].Name == "Action" && dgvBills.CurrentRow.Index != -1)
            {
                if (e.Control is ComboBox)
                {
                    ComboBox cmb = e.Control as ComboBox;
                    cmb.SelectedIndexChanged -= Cmb_SelectedIndexChanged;
                    cmb.SelectedIndexChanged += Cmb_SelectedIndexChanged;
                }
            }
        }
        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.Text == "Bill Payment")
            {
                string billslno = dgvBills.CurrentRow.Cells["slno"].Value.ToString();
                string supplier = dgvBills.CurrentRow.Cells["PartyName"].Value.ToString();
                BillPayment frmReceiptVoucher = new BillPayment(BillPayment._FromWhere.Expense_Bill, billslno, supplier);
                frmReceiptVoucher.OnClose += GenerateBillingList;
                frmReceiptVoucher.ShowDialog();
            }
            else if (cmb.Text == "Cancel")
            {
                if (UserTools._IsCancel)
                {
                    //string billid = dgvBills.CurrentRow.Cells["Billid"].Value.ToString();
                    //string status = dgvBills.CurrentRow.Cells["STATUS"].Value.ToString();
                    //PurchaseBillEntry purchasebillentry = new PurchaseBillEntry(billid, status);
                    //purchasebillentry.CancelBill(status);
                    //GenerateBillingList();
                }
                else
                {
                    MessageBox.Show("permission denied", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    GenerateBillingList();
                }

            }


        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            ExpenseEntry frmExpenseEntry = new ExpenseEntry();
            frmExpenseEntry.FitDownToTop();
            frmExpenseEntry.OnClose += GenerateBillingList;
            frmExpenseEntry.ShowDialog();
        }
    }
}
