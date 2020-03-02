using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace DAPRO
{
    public partial class EstimateList : Form
    {
        string mcustomername = "", mQueryClouse="";
        bool IsLedgerSuccess = true;
        DataTable mDtTable;
        private string mFromDate, mToDate;
        string msg = "";
        public EstimateList()
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
            string query = "select partyname from Estimate";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string partyname = item["partyname"].ToString();
                    acsc.Add(partyname);
                }
            }

        }

        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("Estimateid", typeof(string));
            mDtTable.Columns.Add("EstimateNo", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("ExpiryDate", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));

            dgvEstimate.DataSource = mDtTable;

            DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            linkColumn.Name = "Action";
            linkColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvEstimate.Columns.Add(linkColumn);

            dgvEstimate.Columns["Estimateid"].Visible = false;

            dgvEstimate.Columns["Date"].HeaderText = "ESTIMATE DATE";
            dgvEstimate.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvEstimate.Columns["Amount"].HeaderText = "AMOUNT";
            dgvEstimate.Columns["EstimateNo"].HeaderText = "ESTIMATE NO";
            dgvEstimate.Columns["ExpiryDate"].HeaderText = "EXPIRY DATE";

            dgvEstimate.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstimate.Columns["EstimateNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstimate.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstimate.Columns["ExpiryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstimate.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvEstimate.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvEstimate.Columns["EstimateNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvEstimate.Columns["ExpiryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvEstimate.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvEstimate.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            foreach (DataGridViewColumn column in dgvEstimate.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void GenerateEstimateList()
        {
            DateTime currentDate = DateTime.Now.Date;
            mDtTable.Clear();
            int i = 0;
            string query = "Select  EstimateNo,Estimateid, TemplateName, Address, ContactNo, convert(varchar(11),Date,106) as date, " +
                           "convert(varchar(11),ValidUpTo,106) as ValidUpTo, TotalAmount, Description, " +
                           "Status from Estimate where  "+ mQueryClouse + "  order by EstimateNo,Date desc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string date = item["Date"].ToString();
                    string estimateNo= item["EstimateNo"].ToString();
                    string estimateid = item["Estimateid"].ToString();
                    string partyName = item["TemplateName"].ToString();
                    string description = item["Description"].ToString();
                    string totAmount = double.Parse(item["TotalAmount"].ToString()).ToString("0.00");
                    string expiryDate = item["ValidUpTo"].ToString();

                    #region MyRegion
                    DateTime duedate = DateTime.Parse(expiryDate);
                    DataGridViewLinkCell lnkCell = new DataGridViewLinkCell();

                    if (duedate.Date >= currentDate.Date)
                    {

                        lnkCell.Value = "Order";
                    }
                    
                    #endregion

                    mDtTable.Rows.Add(date, estimateid, estimateNo, partyName, expiryDate, totAmount);
                    dgvEstimate.Rows[i].Cells["Action"] = lnkCell;
                    i++;
                }
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateEstimateList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateEstimateList();
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
            GenerateEstimateList();
        }
        private void btnNewEstimate_Click(object sender, EventArgs e)
        {
            EstimateGenerator estimategenerator = new EstimateGenerator();
            estimategenerator.onclose += GenerateEstimateList;
            estimategenerator.Show(this);
        }
        private void dgvEstimate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstimate.Columns[e.ColumnIndex].Name == "Action" && e.RowIndex!=-1)
            {
                IsLedgerSuccess = true;
                mcustomername = dgvEstimate.CurrentRow.Cells["PartyName"].Value.ToString();
                string estimateno = dgvEstimate.CurrentRow.Cells["EstimateNo"].Value.ToString();
                string estimateid = dgvEstimate.CurrentRow.Cells["Estimateid"].Value.ToString();
                if (!IsexsistCustomer(mcustomername))
                {
                    IsLedgerSuccess = false;
                    LedgerDetails ledgerdetails = new LedgerDetails(LedgerDetails._LedgerCategory.Customer,LedgerDetails._Type.showDialog, estimateid,"estimate");
                    ledgerdetails.OnClose += Ledgerdetails_OnClose;
                    ledgerdetails.ShowDialog();
                }
                if (IsLedgerSuccess)
                {
                    OrderEntry  orderentry = new OrderEntry(estimateid, mcustomername, estimateno);
                    orderentry.ShowDialog();
                } 
            }
        }

        private void Ledgerdetails_OnClose(string customername)
        {
            IsLedgerSuccess = true;
            mcustomername = customername;
            GenerateEstimateList();
        }

        private bool IsexsistCustomer(string customername)
        {
            string query = "Select TemplateName from LadgerMain Where TemplateName='" + customername.Trim().GetDBFormatString() + "' and Category='Customer'";
            if(SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                return true;
            }
            return false;
        }

        private void EstimateList_Load(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
            rbtnEstimateDate.Checked = true;
            btnFilterOk_Click(null, null);
        }

        private void dgvEstimate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstimate.Columns[e.ColumnIndex].Name!= "Action" && e.RowIndex!= -1)
            {
                if (UserTools._IsEdit)
                {
                    int rowindex = dgvEstimate.SelectedCells[0].RowIndex;
                    string estimateid = dgvEstimate.Rows[rowindex].Cells["Estimateid"].Value.ToString();
                    string Vaildupto = dgvEstimate.Rows[rowindex].Cells["ExpiryDate"].Value.ToString();
                    EstimateGenerator estimategenerator = new EstimateGenerator(estimateid, Vaildupto);
                    estimategenerator.onclose += GenerateEstimateList;
                    estimategenerator.Show(this);  
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
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

        private void lblFilterClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rbtnEstimateDate.Checked = true;
            toolStripCurrentMonth_Click(null, null);
            txtEstimateNo.Clear();
            txtPartyName.Clear();
            cmbFilterBy.Text = "";
            btnFilterOk_Click(null, null);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = " Sales_Estimate.xls";
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "E1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= dgvEstimate.Columns.Count - 1; index++)
                {
                    if (dgvEstimate.Columns[index].Visible && dgvEstimate.Columns[index].Name != "Action")
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvEstimate.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "E" + (dgvEstimate.Rows.Count + 1));
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

                Excel.Range amountalignment = mWorkSheet.get_Range("E2", "E" + (dgvEstimate.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvEstimate.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvEstimate.Columns.Count - 1; j++)
                    {
                        if (dgvEstimate.Columns[j].Visible && dgvEstimate.Columns[j].Name != "Action")
                        {
                            DataGridViewCell cell = dgvEstimate[j, i];
                            mWorkSheet.Cells[i + 2, col++] = cell.Value;
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

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            mQueryClouse = "";
            if (rbtnEstimateDate.Checked)
            {
                mQueryClouse = "Date between '" + mFromDate + "' and '" + mToDate + "'";
                lblfilterString.Text = "Estimate Date in " + lblFilterHeader.Text + ";";
            }

            if (!txtEstimateNo.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and EstimateNo='" + txtEstimateNo.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Estimate No =" + txtEstimateNo.Text + ";";
            }
            if (!txtPartyName.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and PartyName='" + txtPartyName.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Party Name=" + txtPartyName.Text + ";";
            }
            if (cmbFilterBy.Text!="All"&& !cmbFilterBy.Text.ISNullOrWhiteSpace())
            {
                string currentDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                if (cmbFilterBy.Text == "Date Over")
                {
                    mQueryClouse = mQueryClouse +" and ValidUpTo<'" + currentDate + "'";
                    lblfilterString.Text = lblfilterString.Text + " ValidUpTo Date< " + currentDate + ";";
                }
               else if (cmbFilterBy.Text == "Date In")
                {
                    mQueryClouse = mQueryClouse + " and ValidUpTo>='" + currentDate + "'";
                    lblfilterString.Text = lblfilterString.Text + " ValidUpTo Date>=" + currentDate + ";";
                }
            }

            lblFilterClear.Location = new Point(lblfilterString.Location.X + lblfilterString.Width + 2, lblFilterClear.Location.Y);
            pnlFilter.Hide();
            GenerateEstimateList();
        }

        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            //GenerateEstimateList();
        }

    }
}
