using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class OrderWindow : Form
    {
        private string msg = "", mQueryClouse = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public OrderWindow()
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
            string query = "select LadgerMain.TemplateName from SalesOrder" +
                " inner join LadgerMain on SalesOrder.LedgerId=LadgerMain.LadgerID ";
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

            mDtTable.Columns.Add("OrderID", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("NO", typeof(string));
            mDtTable.Columns.Add("slno", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("DeleveryDate", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));
            mDtTable.Columns.Add("Status", typeof(string));

            dgvBills.DataSource = mDtTable;
            DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
            txtCol.Name = "Action";
            txtCol.ReadOnly = false;
            //txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns.Add(txtCol);
            dgvBills.Columns["Action"].Width = 21;
            dgvBills.Columns["OrderID"].Visible = false;
            dgvBills.Columns["slno"].Visible = false;

            dgvBills.Columns["Date"].HeaderText = "ORDER DATE";
            dgvBills.Columns["NO"].HeaderText = "ORDER NO.";
            dgvBills.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvBills.Columns["DeleveryDate"].HeaderText = "DELEVERY DATE";
            dgvBills.Columns["Amount"].HeaderText = "AMOUNT";

            dgvBills.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["DeleveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["NO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBills.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBills.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["DeleveryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i < dgvBills.Columns.Count; i++)
            {
                dgvBills.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            OrderEntry frmOrderEntry = new OrderEntry();
            frmOrderEntry.OnClose += GenerateOrderList;
            frmOrderEntry.Show(this);
        }
        private void OrderWindow_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
            rbtnOrderDate.Checked = true;
            btnFilterOk_Click(null, null);
        }
        private void dgvBills_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                dgvBills.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvBills.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvBills.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvBills.Cursor = Cursors.Hand;
            }
        }
        private void dgvBills_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvBills.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvBills.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                dgvBills.Cursor = Cursors.Default;
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();

            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            //GenerateOrderList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            //GenerateOrderList();
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
            //GenerateOrderList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            //GenerateOrderList();
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.SelectedCells.Count > 0 && e.RowIndex != -1 && dgvBills.Columns[e.ColumnIndex].Name != "Action")
            {
                if (UserTools._IsEdit)
                {
                    string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                    string status = dgvBills.CurrentRow.Cells["Status"].Value.ToString();
                    OrderEntry frmOrderEntry = new OrderEntry(orderID, status);
                    frmOrderEntry.OnClose += GenerateOrderList;
                    frmOrderEntry.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }
        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBills.Columns[dgvBills.CurrentCell.ColumnIndex].Name == "Action" && dgvBills.CurrentRow.Index >= 0)
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
            if (cmb.Text == "Create Challan")
            {
                string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                string orderNo = dgvBills.CurrentRow.Cells["No"].Value.ToString();
                ChallanEntry frmChallanEntry = new ChallanEntry(orderID, orderNo);
                frmChallanEntry.OnClose += GenerateOrderList;
                frmChallanEntry.ShowDialog();
            }
            else if (cmb.Text == "Advance receipt")
            {
                string orderid = dgvBills.CurrentRow.Cells["Orderid"].Value.ToString();
                AdvanceReceipt ar = new AdvanceReceipt(AdvanceReceipt._FromWherere.Order, orderid);
                ar.ShowDialog();
            }
            else if (cmb.Text == "Create Invoice")
            {
                string name = dgvBills.CurrentRow.Cells["PartyName"].Value.ToString();
                string slno = dgvBills.CurrentRow.Cells["slno"].Value.ToString();
                Invoice_Direct frmChallanEntry = new Invoice_Direct(slno, name);
                frmChallanEntry.OnClose += GenerateOrderList;
                frmChallanEntry.ShowDialog();
            }
            else if (cmb.Text == "Cancel Order")
            {
                string name = dgvBills.CurrentRow.Cells["PartyName"].Value.ToString();
                string slno = dgvBills.CurrentRow.Cells["slno"].Value.ToString();
                if (OrderCancel(slno))
                {
                    GenerateOrderList();
                }
            }
        }
        private bool OrderCancel(string orderNo)
        {
            string query = "Select * from AdvanceReceiptVoucher where OrderNo='" +
                           orderNo + "' and Status='Open'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string advanceDate = dt.Rows[0]["ReceiptDate"].ToString();
                string amount = dt.Rows[0]["DueAmount"].ToString();
                MessageBox.Show("Can't cancel this order.\nYou have already taken advance this order.", "Order Cancel", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                query = "Update SalesOrder set Status='Cancel' where CustomerOrderNo='" + orderNo + "'";
                if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
                {
                    return true;
                }
            }
            return false;
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
            rbtnOrderDate.Checked = true;
            toolStripCurrentMonth_Click(null, null);
            txtOrderNo.Clear();
            txtPartyName.Clear();
            chkOpen.Checked = true;
            chkClose.Checked = true;
            btnFilterOk_Click(null, null);
        }
        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

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
            sfd.FileName = " Sales_Order.xls";
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

                Excel.Range autofill = mWorkSheet.get_Range("A1", "E" + (dgvBills.Rows.Count + 1));
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

                Excel.Range amountalignment = mWorkSheet.get_Range("D2", "D" + (dgvBills.Rows.Count + 1));
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
                            if (dgvBills.Columns[j].Name == "Status")
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

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            mQueryClouse = "";
            if (rbtnOrderDate.Checked)
            {
                mQueryClouse = "OrderDate between '" + mFromDate + "' and '" + mToDate + "'";
                lblfilterString.Text = "Order Date in " + lblFilterHeader.Text + ";";
            }

            if (!txtOrderNo.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and CustomerOrderNo='" + txtOrderNo.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Order No =" + txtOrderNo.Text + ";";
            }
            if (!txtPartyName.Text.ISNullOrWhiteSpace())
            {
                mQueryClouse = mQueryClouse + " and TemplateName='" + txtPartyName.Text.GetDBFormatString() + "'";
                lblfilterString.Text = lblfilterString.Text + "Party Name=" + txtPartyName.Text + ";";
            }

            string sChkopen = "", sChkClose = "", schkOpenAndClose = "";
            if (chkOpen.Checked && chkClose.Checked)
            {
                schkOpenAndClose = " and (status='Open' or status='Close')";
                mQueryClouse = mQueryClouse + schkOpenAndClose;
                lblfilterString.Text = lblfilterString.Text + "Status=Open and Close;";

            }
            else
            {
                if (chkOpen.Checked)
                {
                    sChkopen = " and status='Open'";
                    mQueryClouse = mQueryClouse + sChkopen;
                    lblfilterString.Text = lblfilterString.Text + "Status=Open;";

                }
                else if (chkClose.Checked)
                {
                    string date = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    sChkClose = " and status='Close'";
                    mQueryClouse = mQueryClouse + sChkClose;
                    lblfilterString.Text = lblfilterString.Text + "Status=Close;";
                }
            }
            lblFilterClear.Location = new Point(lblfilterString.Location.X + lblfilterString.Width + 2, lblFilterClear.Location.Y);
            pnlFilter.Hide();
            GenerateOrderList();
        }
        private void GenerateOrderList()
        {
            mDtTable.Clear();
            int i = 0;
            string query = "SELECT  SalesOrder.OrderID,SalesOrder.SlNo,SalesOrder.CustomerOrderNo, convert(varchar(11),SalesOrder.OrderDate,106) as Date, " +
                           "SalesOrder.TotalAmount,SalesOrder.Description, SalesOrder.SlNo,  SalesOrder.IsSalesOrder, SalesOrder.IsPurchaseOrder,convert(varchar(11),SalesOrder.DeliveryDate,106) as DeliveryDate,LadgerMain. TemplateName,SalesOrder.Status " +
                           "FROM  SalesOrder inner join LadgerMain on SalesOrder.LedgerID=LadgerMain.LadgerID where " + mQueryClouse + " order by SalesOrder.OrderDate,SalesOrder.ID desc ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string orderID = item["OrderID"].ToString();
                    string slno = item["SlNo"].ToString();
                    string orderNo = item["CustomerOrderNo"].ToString();
                    string orderDate = item["Date"].ToString();
                    string deliveryDate = item["DeliveryDate"].ToString();
                    string partyName = item["TemplateName"].ToString();
                    double totAmount = double.Parse(item["TotalAmount"].ToString());
                    string isSalesOrder = item["IsSalesOrder"].ToString();
                    string isPurchaseOrder = item["IsPurchaseOrder"].ToString();
                    string status = item["Status"].ToString();
                    #region MyRegion
                    Color clr = new Color();
                    clr = Color.Black;

                    DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();

                    cmbCell.Style.Font = new Font("Arial", 7f);
                    cmbCell.Style.ForeColor = Color.Blue;
                    //cmbCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cmbCell.FlatStyle = FlatStyle.Flat;
                    cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                    if (status == "Open")
                    {
                        cmbCell.Items.Add("Advance receipt");
                        //cmbCell.Items.Add("Create Challan");
                        cmbCell.Items.Add("Create Invoice");
                        cmbCell.Items.Add("Cancel Order");
                    }
                    else
                    {
                        cmbCell.Items.Add("Advance receipt");
                        cmbCell.Items.Add("Cancel Order");
                    }
                    #endregion

                    mDtTable.Rows.Add(orderID, orderDate, orderNo, slno, partyName, deliveryDate, 
                                      totAmount.toString(), status);
                    dgvBills.Rows[i].Cells["Action"] = cmbCell;
                    i++;
                }
            }
        }
    }
}
