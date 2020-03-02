using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class AdvanceReceiptList : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public AdvanceReceiptList()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("ReceiptNo", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("OrderNo", typeof(string));
            mDtTable.Columns.Add("OrderDate", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(double));
            mDtTable.Columns.Add("DueAmount", typeof(double));
            mDtTable.Columns.Add("Status", typeof(string));

            dgvadvanceReceipt.DataSource = mDtTable;
            DataGridViewLinkColumn lnkCol = new DataGridViewLinkColumn();
            lnkCol.Name = "Action";
            lnkCol.ReadOnly = false;
            dgvadvanceReceipt.Columns.Add(lnkCol);
            dgvadvanceReceipt.Columns["ReceiptNo"].HeaderText = "RECEIPT NO.";
            dgvadvanceReceipt.Columns["Date"].HeaderText = "RECEIPT DATE";
            dgvadvanceReceipt.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvadvanceReceipt.Columns["OrderNo"].HeaderText = "ORDER NO.";
            dgvadvanceReceipt.Columns["OrderDate"].HeaderText = "ORDER DATE";
            dgvadvanceReceipt.Columns["Amount"].HeaderText = "AMOUNT";
            dgvadvanceReceipt.Columns["DueAmount"].HeaderText = "DUE AMOUNT";

            dgvadvanceReceipt.Columns["ReceiptNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvadvanceReceipt.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvadvanceReceipt.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvadvanceReceipt.Columns["DueAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvadvanceReceipt.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvadvanceReceipt.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvadvanceReceipt.Columns["ReceiptNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["Date"].Width = 24;
            dgvadvanceReceipt.Columns["OrderNo"].Width = 24;
            dgvadvanceReceipt.Columns["OrderDate"].Width = 24;
            dgvadvanceReceipt.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["DueAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvadvanceReceipt.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
            GenerateAdvanceReceiptList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateAdvanceReceiptList();
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
            GenerateAdvanceReceiptList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateAdvanceReceiptList();
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvadvanceReceipt.SelectedCells.Count > 0 && e.RowIndex != -1 && dgvadvanceReceipt.Columns[e.ColumnIndex].Name != "Action")
            {
                string orderID = dgvadvanceReceipt.CurrentRow.Cells["OrderID"].Value.ToString();
                string status = dgvadvanceReceipt.CurrentRow.Cells["Status"].Value.ToString();
                OrderEntry frmOrderEntry = new OrderEntry(orderID, status);
                frmOrderEntry.OnClose += GenerateAdvanceReceiptList;
                frmOrderEntry.ShowDialog();
            }

        }
        private void dgvadvanceReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0 && e.ColumnIndex>=0 && dgvadvanceReceipt.Columns[e.ColumnIndex].Name=="Action")
            {
                string advanceNoteno = dgvadvanceReceipt.CurrentRow.Cells["ReceiptNo"].Value.ToString();
                CreditNoteIssue crnoteissu = new CreditNoteIssue(CreditNoteIssue._NoteType.Refund_Voucher, advanceNoteno);
                crnoteissu.OnClose += GenerateAdvanceReceiptList;
                crnoteissu.ShowDialog();
            }
        }
        private void dgvadvanceReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0&&e.ColumnIndex>=0)
            {
                if (UserTools._IsEdit)
                {
                    object advamt = dgvadvanceReceipt.Rows[e.RowIndex].Cells["Amount"].Value.ToString();
                    double adtot = advamt.ISValidObject() ? double.Parse(advamt.ToString()) : 0;
                    object dueamunt = dgvadvanceReceipt.Rows[e.RowIndex].Cells["DueAmount"].Value.ToString();
                    double due = dueamunt.ISValidObject() ? double.Parse(dueamunt.ToString()) : 0;
                    string status = "Open";
                    if (adtot!= due)
                    {
                        status = "Close";
                    }
                    string receiptno = dgvadvanceReceipt.Rows[e.RowIndex].Cells["ReceiptNo"].Value.ToString();
                    AdvanceReceipt adreceipt = new AdvanceReceipt(receiptno, status, "");
                    adreceipt.Onclose += GenerateAdvanceReceiptList;
                    adreceipt.ShowDialog(); 
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Advance_Receipt.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                ExportToExcel(sfd.FileName); // Here dataGridview1 is your grid view name 
                Cursor = Cursors.Default;
            }
        }
        private void ExportToExcel(string fileName)
        {
            Excel.Application mApplication;
            Excel._Workbook mWorkBook;
            Excel._Worksheet mWorkSheet;
            //Excel.Range oRng;

            object misValue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                mApplication = new Excel.Application();
                mApplication.Visible = false;

                //Get a new workbook.
                mWorkBook = (Excel._Workbook)(mApplication.Workbooks.Add(Missing.Value));
                mWorkSheet = (Excel._Worksheet)mWorkBook.ActiveSheet;
                mApplication.DisplayAlerts = false;

                #region Header

                Excel.Range heder = mWorkSheet.get_Range("A1", "F1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= dgvadvanceReceipt.Columns.Count - 1; index++)
                {
                    if (dgvadvanceReceipt.Columns[index].Visible && dgvadvanceReceipt.Columns[index].Name != "Action")
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvadvanceReceipt.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "F" + (dgvadvanceReceipt.Rows.Count + 1));
                autofill.EntireColumn.AutoFit();

                autofill.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                autofill.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                // amount right ALIGN

                Excel.Range amountalignment = mWorkSheet.get_Range("D2", "E" + (dgvadvanceReceipt.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvadvanceReceipt.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvadvanceReceipt.Columns.Count - 1; j++)
                    {
                        if (dgvadvanceReceipt.Columns[j].Visible && dgvadvanceReceipt.Columns[j].Name != "Action")
                        {
                            DataGridViewCell cell = dgvadvanceReceipt[j, i];
                            mWorkSheet.Cells[i + 2, col] = cell.Value;
                            if (dgvadvanceReceipt.Columns[j].Name == "Status")
                            {
                                if (cell.Value.ToString() == "Open")
                                {
                                    mWorkSheet.get_Range(mWorkSheet.Cells[i + 2, col], mWorkSheet.Cells[i + 2, col]).Font.Color = Excel.XlRgbColor.rgbGreen;
                                }
                                else if (cell.Value.ToString() == "Close")
                                {
                                    mWorkSheet.get_Range(mWorkSheet.Cells[i + 2, col], mWorkSheet.Cells[i + 2, col]).Font.Color = Excel.XlRgbColor.rgbRed;
                                }
                            }
                            col++;
                        }
                    }

                }
                try
                {
                    mWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    if (MessageBox.Show("Export complete.\nYou Want to Read The Excel Document ?", "Export To Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        mApplication.Visible = true;
                        mApplication.UserControl = true;
                    }
                    else
                    {
                        mWorkBook.Close(true, misValue, misValue);
                        mApplication.Quit();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("File Was opened.So,Can not overide.\n Please Close the Excel file and try again latter...", "Export To Excel", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }


            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerateAdvanceReceiptList()
        {
            mDtTable.Clear();
            int i = 0;
            string query = "SELECT AdvanceReceiptVoucher.OrderNo,AdvanceReceiptVoucher.OrderDate, "+
                           "AdvanceReceiptVoucher.DueAmount,AdvanceReceiptVoucher.ReceiptNo, "+
                           "convert(varchar(11),AdvanceReceiptVoucher.ReceiptDate,106) as Date, " +
                           "AdvanceReceiptVoucher.Total,AdvanceReceiptVoucher.Status,Ledgers. LedgerName " +
                           "FROM  AdvanceReceiptVoucher "+
                           "inner join Ledgers on AdvanceReceiptVoucher.LedgerID=Ledgers.LedgerID "+
                           "where AdvanceReceiptVoucher.ReceiptDate between '" +
                           mFromDate + "' and '" + mToDate + "' "+
                           "order by AdvanceReceiptVoucher.ReceiptDate,AdvanceReceiptVoucher.ReceiptNo desc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string orderNo = item["OrderNo"].ToString();
                    string orderDate = item["OrderDate"].ToString();
                    string receiptNo = item["ReceiptNo"].ToString();
                    string receiptDate = item["Date"].ToString();
                    string partyName = item["LedgerName"].ToString();
                    double totAmount = double.Parse(item["Total"].ToString());
                    double dueamount = double.Parse(item["DueAmount"].ToString());
                    string status = item["Status"].ToString();
                    #region MyRegion
                    Color clr = new Color();
                    clr = Color.Black;

                    DataGridViewLinkCell lnkCell = new DataGridViewLinkCell();

                    if (dueamount!=0)
                    {
                        lnkCell.Value = "Create Refund Voucher";
                    }
                    #endregion

                    mDtTable.Rows.Add(receiptNo, receiptDate, partyName,orderNo,orderDate,
                                      totAmount.toString(), dueamount.toString(), status);
                    dgvadvanceReceipt.Rows[i].Cells["Action"] = lnkCell;
                    i++;
                }
            }
        }
    }
}
