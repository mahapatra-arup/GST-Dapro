using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class PurchaseOrderWindow : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public PurchaseOrderWindow()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("OrderID", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("NO", typeof(string));
            mDtTable.Columns.Add("EstimateNo", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("DeliveryDate", typeof(string));
            mDtTable.Columns.Add("Status", typeof(string));
            mDtTable.Columns.Add("StatusForChallan", typeof(string));

            dgvBills.DataSource = mDtTable;
            //DataGridViewColumn txt = new DataGridViewColumn();
            //txt.Name = "Action";
            //txt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns.Add("Action","Action");
            dgvBills.Columns["Date"].ReadOnly = true;
            dgvBills.Columns["NO"].ReadOnly = true;
            dgvBills.Columns["EstimateNo"].ReadOnly = true;
            dgvBills.Columns["PartyName"].ReadOnly = true;
            dgvBills.Columns["DeliveryDate"].ReadOnly = true;
            dgvBills.Columns["Status"].ReadOnly = true;
            dgvBills.Columns["Action"].ReadOnly = false;

            dgvBills.Columns["Action"].Width = 20;

            dgvBills.Columns["OrderID"].Visible = false;
            dgvBills.Columns["StatusForChallan"].Visible = false;

            dgvBills.Columns["Date"].HeaderText = "  DATE   ";
            dgvBills.Columns["NO"].HeaderText = "   NO.   ";
            dgvBills.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvBills.Columns["EstimateNo"].HeaderText = "ESTIMATE NO";
            dgvBills.Columns["DeliveryDate"].HeaderText = "DELIVERY DATE";
            ///Header Alignment
            dgvBills.Columns["EstimateNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["PartyName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["NO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvBills.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["DeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["NO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["EstimateNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBills.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBills.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBills.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["DeliveryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["EstimateNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBills.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvBills.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < dgvBills.Columns.Count; i++)
            {
                dgvBills.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            PurchaseOrderEntry frmpurchaseOrderEntry = new PurchaseOrderEntry();
            frmpurchaseOrderEntry.OnClose += GenerateOrderList;
            frmpurchaseOrderEntry.Show(this);
        }
        private void OrderWindow_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(toolStripCurrentMonth, null);
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
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateOrderList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " to " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateOrderList();
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
            GenerateOrderList();
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateOrderList();
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            


        }
        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvBills.EditingControlShowing -= dgvBills_EditingControlShowing;
            if (dgvBills.Columns[dgvBills.CurrentCell.ColumnIndex].Name == "Action" && dgvBills.CurrentRow.Index != -1)
            {
                if (e.Control is ComboBox)
                {
                    ComboBox cmb = e.Control as ComboBox;
                    cmb.SelectedIndexChanged += Cmb_SelectedIndexChanged;
                }
            }

        }
        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cmb = (ComboBox)sender;
            if (cmb.Text == "Challan")
            {
                string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                PurchaseChallanEntry frmChallanEntry = new PurchaseChallanEntry(orderID);
                frmChallanEntry.OnClose += GenerateOrderList;
                frmChallanEntry.ShowDialog();
            }
            if (cmb.Text == "Bill")
            {
                string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                PurchaseBillEntry frmbillEntry = new PurchaseBillEntry(PurchaseBillEntry._CameFrom.Order, orderID, "");
                frmbillEntry.OnClose += GenerateOrderList;
                frmbillEntry.ShowDialog();
            }
            if (cmb.Text == "Advance Payment")
            {
                string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                AdvancePayment frmadvancePayment = new AdvancePayment(AdvancePayment._FromWherere.Order,orderID);
                frmadvancePayment.Onclose += GenerateOrderList;
                frmadvancePayment.ShowDialog();
            }
        }
        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.SelectedCells.Count > 0 && e.RowIndex != -1 && dgvBills.Columns[e.ColumnIndex].Name != "Action")
            {
                if (UserTools._IsEdit)
                {
                    string orderID = dgvBills.CurrentRow.Cells["OrderID"].Value.ToString();
                    string status = dgvBills.CurrentRow.Cells["Status"].Value.ToString();
                    string StatusForchallan = dgvBills.CurrentRow.Cells["StatusForchallan"].Value.ToString();
                    PurchaseOrderEntry frmOrderEntry = new PurchaseOrderEntry(orderID, status, StatusForchallan);
                    frmOrderEntry.OnClose += GenerateOrderList;
                    frmOrderEntry.ShowDialog(); 
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
            sfd.FileName = " Purchase_Order.xls";
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

                Excel.Range autofill = mWorkSheet.get_Range("A1", "F" + (dgvBills.Rows.Count + 1));
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

                //Excel.Range amountalignment = mWorkSheet.get_Range("D2", "D" + (dgvBills.Rows.Count + 1));
                //amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
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

        private void GenerateOrderList()
        {
            mDtTable.Clear();
            int i = 0;
            string query = "SELECT  PurchaseOrder.DeliveryDate,PurchaseOrder.OrderID,StatusForchallan,Estimateno,PurchaseOrderNo, convert(varchar(11),PurchaseOrder.OrderDate,106) as Date, " +
                           "PurchaseOrder.Description, PurchaseOrder.SlNo,Ledgers. LedgerName,PurchaseOrder.Status " +
                           "FROM  PurchaseOrder inner join Ledgers on PurchaseOrder.LedgerID=Ledgers.LedgerID where PurchaseOrder.OrderDate between '" +
                           mFromDate + "' and '" + mToDate + "' order by PurchaseOrder.OrderDate,PurchaseOrder.PurchaseOrderNo desc ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string deliverydatestr  = !item["DeliveryDate"].ToString().ISNullOrWhiteSpace() ? DateTime.Parse(item["DeliveryDate"].ToString()).ToString("dd-MMM-yyyy") : "";
                    string orderID = item["OrderID"].ToString();
                    string orderNo = item["PurchaseOrderNo"].ToString();
                    string estimateno = item["Estimateno"].ToString();
                    string orderDate = item["Date"].ToString();
                    string partyName = item["LedgerName"].ToString();
                    string status = item["Status"].ToString();
                    string statusforChallan = item["StatusForchallan"].ToString();
                    mDtTable.Rows.Add(orderID, orderDate, orderNo, estimateno, partyName, deliverydatestr, status, statusforChallan);

                    #region MyRegion
                    Color clr = new Color();
                    clr = Color.Black;
                    if (status == "Open")
                    {
                        DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();
                        cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cmbCell.FlatStyle = FlatStyle.Flat;

                        if (statusforChallan == "Close")
                        {
                            dgvBills.Rows[i].Cells["Action"].ReadOnly = false;
                            cmbCell.ToolTipText = "Create Bill";
                            cmbCell.MaxDropDownItems = 3;
                            cmbCell.Items.Add("Advance Payment");
                            //cmbCell.Items.Add("Challan");
                            cmbCell.Items.Add("Bill");
                            dgvBills.Rows[i].Cells["Action"] = cmbCell;

                        }
                        else
                        {
                            dgvBills.Rows[i].Cells["Action"].ReadOnly = false;
                            //cmbCell.ToolTipText = "Create Challan";
                            cmbCell.MaxDropDownItems = 2;
                            cmbCell.Items.Add("Advance Payment");
                            //cmbCell.Items.Add("Challan");
                            dgvBills.Rows[i].Cells["Action"] = cmbCell;
                        }
                    }
                    else
                    {
                        dgvBills.Rows[i].Cells["Action"].ReadOnly = true;
                    }

                    #endregion

                    i++;
                }
            }
        }
    }
}
