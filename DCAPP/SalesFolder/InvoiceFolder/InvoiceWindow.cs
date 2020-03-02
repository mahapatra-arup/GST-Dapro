using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class InvoiceWindow : Form
    {
        private string msg = "", mQueryClouse = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public InvoiceWindow()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            AutoCompleteSource();
        }
        private void AutoCompleteSource()
        {
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            txtPartyName.AutoCompleteCustomSource = acsc;
            string query = "select LadgerMain.TemplateName from Invoice" +
                " inner join LadgerMain on Invoice.LedgerId=LadgerMain.LadgerID ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string partyname = item["TemplateName"].ToString();
                    acsc.Add(partyname);
                }
            }

        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("InvoiceNo", typeof(string));
            mDtTable.Columns.Add("Buyer", typeof(string));
            mDtTable.Columns.Add("TotalAmount", typeof(string));
            mDtTable.Columns.Add("DueAmount", typeof(string));
            mDtTable.Columns.Add("dueDate", typeof(string));
            mDtTable.Columns.Add("STATUS", typeof(string));

            dgvBills.DataSource = mDtTable;
            DataGridViewTextBoxColumn txtCell = new DataGridViewTextBoxColumn();
            txtCell.Name = "Action";
            txtCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns.Add(txtCell);

            dgvBills.Columns["Date"].HeaderText = "INVOICE DATE";
            dgvBills.Columns["InvoiceNo"].HeaderText = "INVOICE NO.";
            dgvBills.Columns["Buyer"].HeaderText = "CUSTOMER NAME";
            dgvBills.Columns["dueDate"].HeaderText = "DUE DATE";
            dgvBills.Columns["DueAmount"].HeaderText = "DUE AMOUNT";
            dgvBills.Columns["TotalAmount"].HeaderText = "TOTAL AMOUNT";
            dgvBills.Columns["STATUS"].HeaderText = "STATUS";

            dgvBills.Columns["Buyer"].ReadOnly = true;
            dgvBills.Columns["Date"].ReadOnly = true;
            dgvBills.Columns["InvoiceNo"].ReadOnly = true;
            dgvBills.Columns["DueAmount"].ReadOnly = true;
            dgvBills.Columns["dueDate"].ReadOnly = true;
            dgvBills.Columns["TotalAmount"].ReadOnly = true;
            dgvBills.Columns["STATUS"].ReadOnly = true;
            dgvBills.Columns["Action"].ReadOnly = false;

            dgvBills.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["InvoiceNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["DueAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBills.Columns["dueDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBills.Columns["STATUS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBills.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["InvoiceNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["dueDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["DueAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["TotalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i < dgvBills.Columns.Count; i++)
            {
                dgvBills.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();

            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            //GenerateBillingList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            //GenerateBillingList();
        }
        private void toolStripPreviousMonth_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
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
            //GenerateBillingList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            //GenerateBillingList();
        }
        private void InvoiceWindow_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
            rbtnInvoiceDate.Checked = true;
            btnFilterOk_Click(null, null);
        }
        private void dgvBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvBills.Columns[e.ColumnIndex].Name == "Action")
            //{
            //    string billID = dgvBills.CurrentRow.Cells["BillNO"].Value.ToString();
            //    string suppliers = dgvBills.CurrentRow.Cells["Buyer"].Value.ToString();
            //    ReceiptVoucher objBillPayment = new ReceiptVoucher(billID, suppliers);
            //    objBillPayment.OnClose += GenerateBillingList;
            //    objBillPayment.Show(this);
            //}
        }
        private void GenerateBillingList()
        {
            mDtTable.Clear();

            int i = 0;
            string query = "Select  InvoiceNo,BillingTo, InvoiceDate, LadgerMain.TemplateName,DueDate, " +
                           "TotalInvoiceAmount,DueAmount, Status from Invoice inner join LadgerMain on " +
                           "Invoice.LedgerId=LadgerMain.LadgerID where " + mQueryClouse 
                           + " order by slno desc ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string billingname = item["BillingTo"].ToString();
                    string invoicedate = !item["InvoiceDate"].ToString().ISNullOrWhiteSpace() ? DateTime.Parse(item["InvoiceDate"].ToString()).ToString("dd-MMM-yyyy") : "";
                    string invoiceNo = item["InvoiceNo"].ToString();
                    string customername = item["TemplateName"].ToString();
                    string partyName = item["TemplateName"].ToString() == "CASH" ? billingname == "CASH" ? billingname : item["TemplateName"].ToString() + " ( " + billingname + " ) " : item["TemplateName"].ToString();

                    string amountStr = item["TotalInvoiceAmount"].ToString();
                    double totAmount = amountStr.ISNullOrWhiteSpace() ? 0f : double.Parse(amountStr);
                    string dueDateStr = !item["DueDate"].ToString().ISNullOrWhiteSpace() ? DateTime.Parse(item["DueDate"].ToString()).ToString("dd-MMM-yyyy") : "";
                    DateTime? dueDate = !dueDateStr.ISNullOrWhiteSpace() ? DateTime.Parse(dueDateStr) : (DateTime?)null;
                    string status = item["Status"].ToString();
                    string dueamountStr = item["DueAmount"].ToString();
                    double dueamount = !dueamountStr.ISNullOrWhiteSpace() ? double.Parse(dueamountStr) : 0f;

                    #region MyRegion
                    Color clr = new Color();
                    clr = Color.Black;
                    DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();
                    cmbCell.Style.Font = new Font("Arial", 9f);
                    cmbCell.Style.ForeColor = Color.Blue;
                    //cmbCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cmbCell.FlatStyle = FlatStyle.Flat;
                    cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                    cmbCell.Items.Add("         <Select>         ");
                    if (status != "Cancel")
                    {
                        cmbCell.Items.Add("Receive Payment");
                        cmbCell.Items.Add("Cancel");
                        if (!IsCreditNoteIssue(invoiceNo) && customername != "CASH")
                        {
                            cmbCell.Items.Add("Credit Note");
                        }
                        cmbCell.Items.Add("Original for Recipient");
                        cmbCell.Items.Add("Duplicate for Transporter");
                        cmbCell.Items.Add("Triplicate for Supplier");
                        cmbCell.Value = "         <Select>         ";

                        if (dueamount > 0)
                        {
                            if (dueDate != null && dueDate >= DateTime.Now.Date)
                            {
                                clr = Color.OrangeRed;
                                status = "Due";
                            }
                            else
                            {
                                clr = Color.Red;
                                status = "Overdue";
                            }
                            cmbCell.Value = "         <Select>         ";
                        }
                        else
                        {
                            cmbCell.Items.RemoveAt(1);
                            if (customername != "CASH")
                            {
                                cmbCell.Items.RemoveAt(1);
                            }
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

                    mDtTable.Rows.Add(invoicedate, invoiceNo, partyName, totAmount.toString(),
                                      dueamount.toString(), dueDateStr, status);

                    dgvBills.Rows[i].Cells["Action"] = cmbCell;
                    dgvBills.Rows[i].Cells["Status"].Value = status;
                    dgvBills.Rows[i].Cells["Status"].Style.ForeColor = clr;
                    i++;
                }
            }
        }
        private bool IsCreditNoteIssue(string invoiceNo)
        {
            string query = "select * from CDRNote Where InvoiceOrADRNo='" + invoiceNo + "' and DocumentType='C'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return true;
            }
            return false;
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
        private void btnDirectInvoice_Click(object sender, EventArgs e)
        {
            Invoice_Direct invoicedirect = new Invoice_Direct();
            invoicedirect.OnClose += Invoicedirect_onClose;
            invoicedirect.ShowDialog();
        }
        private void Invoicedirect_onClose()
        {
            GenerateBillingList();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pnlFilter.Visible)
            {
                pnlFilter.Hide();
            }
            else
            {
                pnlFilter.Show();
            }
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Show();
            mFromDate = dtmStartDate.Text;
            mToDate = dtmEndDate.Text;
            lblFilterHeader.Text = customToolStripMenuItem.Text + " (" + mFromDate + " - " + mToDate + ")";


        }
        private void dtmStartDate_ValueChanged(object sender, EventArgs e)
        {
            customToolStripMenuItem_Click(null, null);

        }
        private void dtmEndDate_ValueChanged(object sender, EventArgs e)
        {
            customToolStripMenuItem_Click(null, null);
        }
        private void lblFilterClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rbtnInvoiceDate.Checked = true;
            toolStripCurrentMonth_Click(null, null);
            txtInVoiceNo.Clear();
            txtPartyName.Clear();
            chkPaid.Checked = true;
            chkDue.Checked = true;
            ChkOverDue.Checked = true;
            btnFilterOk_Click(null, null);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = " Sales_Inoice.xls";
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "G1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= dgvBills.Columns.Count - 1; index++)
                {
                    if (dgvBills.Columns[index].Visible && dgvBills.Columns[index].Name != "Action")
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvBills.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "G" + (dgvBills.Rows.Count + 1));
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

                Excel.Range amountalignment = mWorkSheet.get_Range("D1", "E" + (dgvBills.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvBills.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvBills.Columns.Count - 1; j++)
                    {
                        if (dgvBills.Columns[j].Visible && dgvBills.Columns[j].Name != "Action")
                        {
                            DataGridViewCell cell = dgvBills[j, i];
                            mWorkSheet.Cells[i + 2, col] = cell.Value;
                            if (dgvBills.Columns[j].Name == "STATUS")
                            {
                                if (cell.Value.ToString() == "Overdue")
                                {
                                    mWorkSheet.get_Range(mWorkSheet.Cells[i + 2, col], mWorkSheet.Cells[i + 2, col]).Font.Color = Excel.XlRgbColor.rgbRed;
                                }
                                else if (cell.Value.ToString() == "Due")
                                {
                                    mWorkSheet.get_Range(mWorkSheet.Cells[i + 2, col], mWorkSheet.Cells[i + 2, col]).Font.Color = Excel.XlRgbColor.rgbOrange;
                                }
                                else if (cell.Value.ToString() == "Paid")
                                {
                                    mWorkSheet.get_Range(mWorkSheet.Cells[i + 2, col], mWorkSheet.Cells[i + 2, col]).Font.Color = Excel.XlRgbColor.rgbGreen;
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

        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.SelectedCells.Count >= 0 && dgvBills.Columns[e.ColumnIndex].Name != "Action" && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (UserTools._IsEdit)
                {
                    string invoiceno = dgvBills.CurrentRow.Cells["invoiceno"].Value.ToString();
                    string status = dgvBills.CurrentRow.Cells["STATUS"].Value.ToString();
                    Invoice_Direct invoicedirect = new Invoice_Direct(invoiceno, status, "");
                    invoicedirect.OnClose += GenerateBillingList;
                    invoicedirect.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Edit permission denied by Admin", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    GenerateBillingList();
                }
            }
        }

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            mQueryClouse = "";
            if (rbtnInvoiceDate.Checked)
            {
                mQueryClouse = "InvoiceDate between '" + mFromDate + "' and '" + mToDate + "'";
                lblfilterString.Text = "Invoice Date in " + lblFilterHeader.Text + ";";
            }
            else if (rbtnDueDate.Checked)
            {
                mQueryClouse = "DueDate between '" + mFromDate + "' and '" + mToDate + "'";
                lblfilterString.Text = "DueDate Date in " + lblFilterHeader.Text + ";";
            }
            if (!txtInVoiceNo.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and InvoiceNo='" + txtInVoiceNo.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Invoice No =" + txtInVoiceNo.Text + ";";
            }
            if (!txtPartyName.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and TemplateName='" + txtPartyName.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Party Name=" + txtPartyName.Text + ";";
            }

            string sChkPaid = "", sChkDue = "", sChkOverDue = "",
                sChkPaidDue = "", sChkPaidOverDue = "", sChkDueOverDue = "";
            if (chkPaid.Checked && chkDue.Checked && ChkOverDue.Checked)
            {

            }
            else
            {
                if (chkPaid.Checked && chkDue.Checked)
                {
                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkPaidDue = " and DueAmount<='0' or (DueAmount>'0' and DueDate >='" + date + "')";
                    mQueryClouse = mQueryClouse + sChkPaidDue;
                    lblfilterString.Text = lblfilterString.Text + "Status=Paid and Due;";
                }
                else if (chkPaid.Checked && ChkOverDue.Checked)
                {

                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkPaidOverDue = " and DueAmount<='0' or (DueAmount>'0' and DueDate <'" + date + "')";
                    mQueryClouse = mQueryClouse + sChkPaidOverDue;
                    lblfilterString.Text = lblfilterString.Text + "Status=Paid and OverDue;";

                }
                else if (chkDue.Checked && ChkOverDue.Checked)
                {
                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkDueOverDue = " and DueAmount>'0'";
                    mQueryClouse = mQueryClouse + sChkDueOverDue;
                    lblfilterString.Text = lblfilterString.Text + "Status=Due and OverDue;";

                }
                else if (chkPaid.Checked)
                {
                    sChkPaid = " and DueAmount<='0'";
                    mQueryClouse = mQueryClouse + sChkPaid;
                    lblfilterString.Text = lblfilterString.Text + "Status=Paid;";


                }
                else if (chkDue.Checked)
                {
                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkDue = " and (DueAmount > '0' and DueDate >= '" + date + "')";
                    mQueryClouse = mQueryClouse + sChkDue;
                    lblfilterString.Text = lblfilterString.Text + "Status=Due;";
                }
                else if (ChkOverDue.Checked)
                {
                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkOverDue = " and (DueAmount>'0' and DueDate <'" + date + "')";
                    mQueryClouse = mQueryClouse + sChkOverDue;
                    lblfilterString.Text = lblfilterString.Text + "Status= OverDue;";
                }
            }
            lblFilterClear.Location = new Point(lblfilterString.Location.X + lblfilterString.Width + 2, lblFilterClear.Location.Y);
            pnlFilter.Hide();
            GenerateBillingList();
        }
        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.Text == "Receive Payment")
            {
                string billNo = dgvBills.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                string customer = dgvBills.CurrentRow.Cells["Buyer"].Value.ToString();
                ReceiptVoucher frmReceiptVoucher = new ReceiptVoucher(billNo, customer);
                frmReceiptVoucher.OnClose += GenerateBillingList;
                frmReceiptVoucher.ShowDialog();
            }
            else if (cmb.Text == "Original for Recipient")
            {
                string billNo = dgvBills.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                string suppliers = dgvBills.CurrentRow.Cells["Buyer"].Value.ToString();
                InvoiceReportViewer frmInvoiceReportViewer = new InvoiceReportViewer(InvoiceReportViewer._InvoiceCopy.Original, billNo);
                frmInvoiceReportViewer.OnClose += GenerateBillingList;
                frmInvoiceReportViewer.ShowDialog();
            }
            else if (cmb.Text == "Duplicate for Transporter")
            {
                string billNo = dgvBills.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                string suppliers = dgvBills.CurrentRow.Cells["Buyer"].Value.ToString();
                InvoiceReportViewer frmInvoiceReportViewer = new InvoiceReportViewer(InvoiceReportViewer._InvoiceCopy.Duplicate, billNo);
                frmInvoiceReportViewer.OnClose += GenerateBillingList;
                frmInvoiceReportViewer.ShowDialog();
            }
            else if (cmb.Text == "Triplicate for Supplier")
            {
                string billNo = dgvBills.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                string suppliers = dgvBills.CurrentRow.Cells["Buyer"].Value.ToString();
                InvoiceReportViewer frmInvoiceReportViewer = new InvoiceReportViewer(InvoiceReportViewer._InvoiceCopy.Triplicate, billNo);
                frmInvoiceReportViewer.OnClose += GenerateBillingList;
                frmInvoiceReportViewer.ShowDialog();
            }
            else if (cmb.Text == "Credit Note")
            {

                string invoiceNo = dgvBills.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                CreditNoteIssue creditnoteissu = new CreditNoteIssue(CreditNoteIssue._NoteType.Credit_Note, invoiceNo);
                creditnoteissu.OnClose += GenerateBillingList;
                creditnoteissu.ShowDialog();

            }
            else if (cmb.Text == "Cancel")
            {
                if (UserTools._IsCancel)
                {
                    string invoiceno = dgvBills.CurrentRow.Cells["invoiceno"].Value.ToString();
                    string status = dgvBills.CurrentRow.Cells["STATUS"].Value.ToString();
                    Invoice_Direct invoicedirect = new Invoice_Direct(invoiceno, status, "");
                    invoicedirect.InvoiceCancel();
                    GenerateBillingList();
                }
                else
                {
                    MessageBox.Show("Cancel permission denied by Admin", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    GenerateBillingList();
                }
            }

        }
    }
}
