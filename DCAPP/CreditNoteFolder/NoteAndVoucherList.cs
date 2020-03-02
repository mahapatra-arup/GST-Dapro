using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class NoteAndVoucherList : Form
    {
        private string msg = "", mfilter = string.Empty, mExcelFileName = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public enum _Type
        {
            Credit_Note,
            Debit_Note,
            Refund_Voucher
        }
        private _Type mType;
        public NoteAndVoucherList(_Type type)
        {
            InitializeComponent();
            InitDtTable();
            mType = type;
            switch (mType)
            {
                case _Type.Credit_Note:
                    lblHeader.Text = "Sales Return Register";
                    mExcelFileName = "Sales_Return_Register.Xls";
                    break;
                case _Type.Debit_Note:
                    lblHeader.Text = "Purchase Return Register";
                    mExcelFileName = "Purchase_Return_Register.Xls";
                    break;
                case _Type.Refund_Voucher:
                    lblHeader.Text = "Refund Voucher Register";
                    mExcelFileName = "Refund_Return_Register.Xls";
                    break;
                default:
                    break;
            }
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("slno", typeof(string));
            mDtTable.Columns.Add("Noteid", typeof(string));
            mDtTable.Columns.Add("VoucherType", typeof(string));
            mDtTable.Columns.Add("NoteNo", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("InvoiceNo", typeof(string));
            mDtTable.Columns.Add("InvoiceDate", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));
            mDtTable.Columns.Add("WillAdjustAmount", typeof(string));
            mDtTable.Columns.Add("Status", typeof(string));

            dgvNote.DataSource = mDtTable;
            dgvNote.Columns["Noteid"].Visible = false;

            dgvNote.Columns["slno"].HeaderText = "Sl. NO.";
            dgvNote.Columns["NoteNo"].HeaderText = "NOTE NO.";
            dgvNote.Columns["Date"].HeaderText = "  DATE  ";
            dgvNote.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvNote.Columns["InvoiceNo"].HeaderText = "INVOICE NO";
            dgvNote.Columns["InvoiceDate"].HeaderText = "INVOICE DATE";
            dgvNote.Columns["Amount"].HeaderText = "AMOUNT";
            dgvNote.Columns["VoucherType"].HeaderText = "VOUCHER TYPE";
            dgvNote.Columns["WillAdjustAmount"].HeaderText = "WILL ADJUST AMOUNT";
            dgvNote.Columns["Status"].HeaderText = "STATUS";

            dgvNote.Columns["NoteNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["PartyName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["VoucherType"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["Amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNote.Columns["WillAdjustAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNote.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvNote.Columns["NoteNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvNote.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNote.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNote.Columns["WillAdjustAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["PartyName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvNote.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvNote.Columns["slno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvNote.Columns["NoteNo"].Width = 25;
            dgvNote.Columns["Date"].Width = 25;
            dgvNote.Columns["InvoiceNo"].Width = 25;
            dgvNote.Columns["InvoiceDate"].Width = 25;
            dgvNote.Columns["Amount"].Width = 30;
            dgvNote.Columns["WillAdjustAmount"].Width = 25;
            dgvNote.Columns["VoucherType"].Width = 25;
            dgvNote.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i < dgvNote.Columns.Count; i++)
            {
                dgvNote.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void GenerateNoteList()
        {
            mDtTable.Clear();
            int i = 1;
            switch (mType)
            {
                case _Type.Credit_Note:
                    mfilter = " and DocumentType='C'";
                    break;
                case _Type.Debit_Note:
                    mfilter = " and DocumentType='D'";
                    break;
                case _Type.Refund_Voucher:
                    mfilter = " and DocumentType='R'";
                    break;
                default:
                    break;
            }
            string query = "SELECT CDRNote.InvoiceOrADRNo,CDRNote.InvoiceOrADRDate,CDRNote.NoteNo, " +
                           "CDRNote.status,CDRNote.DueAmount,CDRNote.NoteID, " +
                           "convert(varchar(11),CDRNote.RefundDate,106) as Date, " +
                           "CDRNote.NoteValue,CDRNote.DocumentType,LadgerMain.TemplateName " +
                           "FROM  CDRNote inner join LadgerMain on CDRNote.LedgerId=LadgerMain.LadgerID " +
                           "where CDRNote.RefundDate between '" +
                           mFromDate + "' and '" + mToDate + "' " + mfilter + " order by CDRNote.ID desc ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string noteNo = item["NoteNo"].ToString();
                    string noteid = item["NoteID"].ToString();
                    string refunddateDate = item["Date"].ToString();
                    string partyName = item["TemplateName"].ToString();
                    double totAmount = double.Parse(item["NoteValue"].ToString());
                    string Type = item["DocumentType"].ToString();
                    string invoiceNo = item["InvoiceOrADRNo"].ToString();
                    string invoiceDate = item["InvoiceOrADRDate"].ToString();
                    string voucherType = "";
                    string status = item["status"].ToString();
                    double willadjustamount = item["dueamount"].ToString().ISNullOrWhiteSpace() ? 0 : double.Parse(item["dueamount"].ToString());
                    if (willadjustamount != 0)
                    {
                        status = "Open";
                    }
                    voucherType = Type == "C" ? "CREDIT_NOTE" : Type == "D" ? "DEBIT_NOTE" : "REFUND_VOUCHER";
                    mDtTable.Rows.Add(i++, noteid, voucherType, noteNo, refunddateDate, partyName, 
                                      invoiceNo, invoiceDate, totAmount.toString(), willadjustamount.toString(), status);
                }
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
                dgvNote.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvNote.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvNote.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvNote.Cursor = Cursors.Hand;
            }
        }
        private void dgvBills_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvNote.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvNote.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                dgvNote.Cursor = Cursors.Default;
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateNoteList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + "  To  " + mToDate + ")";
            GenerateNoteList();
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
            GenerateNoteList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateNoteList();
        }
        private void dgvadvanceReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvNote.Columns[e.ColumnIndex].Name == "Action")
            {
                string advanceNoteno = dgvNote.CurrentRow.Cells["ReceiptNo"].Value.ToString();
                CreditNoteIssue crnoteissu = new CreditNoteIssue(CreditNoteIssue._NoteType.Refund_Voucher, advanceNoteno);
                crnoteissu.OnClose += GenerateNoteList;
                crnoteissu.ShowDialog();
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = mExcelFileName;
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "H1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= dgvNote.Columns.Count - 1; index++)
                {
                    if (dgvNote.Columns[index].Visible && dgvNote.Columns[index].Name != "Action")
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvNote.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "H" + (dgvNote.Rows.Count + 1));
                autofill.EntireColumn.AutoFit();

                autofill.HorizontalAlignment = Excel.XlHAlign.xlHAlignJustify;
                autofill.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                autofill.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                //// amount right ALIGN

                Excel.Range amountalignment = mWorkSheet.get_Range("F2", "G" + (dgvNote.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvNote.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvNote.Columns.Count - 1; j++)
                    {
                        if (dgvNote.Columns[j].Visible && dgvNote.Columns[j].Name != "Action")
                        {
                            DataGridViewCell cell = dgvNote[j, i];
                            mWorkSheet.Cells[i + 2, col] = cell.Value;
                            if (dgvNote.Columns[j].Name == "Status")
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
        private void dgvadvanceReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (UserTools._IsEdit)
                {
                    //string status = dgvNote.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                    string noteid = dgvNote.Rows[e.RowIndex].Cells["noteid"].Value.ToString();
                    double totAmount = double.Parse(dgvNote.Rows[e.RowIndex].Cells["amount"].Value.ToString());
                    double willadjustamount = double.Parse(dgvNote.Rows[e.RowIndex].Cells["willadjustamount"].Value.ToString());
                    string status = "Close";
                    if (willadjustamount == totAmount)
                    {
                        status = "Open";
                    }

                    CreditNoteIssue creditnote = new CreditNoteIssue(noteid, status, "");
                    creditnote.OnClose += GenerateNoteList;
                    creditnote.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbFilterBytype.Text.ISNullOrWhiteSpace())
            {
                if (cmbFilterBytype.Text == "ALL")
                {
                    mfilter = string.Empty;
                    GenerateNoteList();
                }
                else if (cmbFilterBytype.Text == "CREDIT_NOTE")
                {
                    mfilter = " and DocumentType='C'";
                    GenerateNoteList();
                }
                else if (cmbFilterBytype.Text == "DEBIT_NOTE")
                {
                    mfilter = " and DocumentType='D'";
                    GenerateNoteList();
                }
                else
                {
                    mfilter = " and DocumentType='R'";
                    GenerateNoteList();
                }
            }
        }
    }
}
