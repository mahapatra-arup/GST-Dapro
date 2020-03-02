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
    public partial class Sales_OrderItemSummary : Form
    {
        string msg = "";
        DataTable mdt = new DataTable();
        private string mFromDate, mToDate,mExcelFileName="";
        public enum _Type
        {
            Order,
            Sales,
        }
        private _Type mType;
        public Sales_OrderItemSummary(_Type type)
        {
            InitializeComponent();
            this.FitToHorazonal();
            mType = type;
            switch (mType)
            {
                case _Type.Order:
                    InitOrderDataTable();
                    lblHeader.Text = "Order Summary";
                    mExcelFileName = "Order_Summary.xls";
                    break;
                case _Type.Sales:
                    InitSalesDataTable();
                    lblHeader.Text = "Sales Summary";
                    mExcelFileName = "Sales_Summary.xls";

                    break;
                default:
                    break;
            }
        }
        private void InitOrderDataTable()
        {
            // SLNO,DeliveryDate,Party,VchNo,TemplateName,HSNsacCode,
            //    Qty, Rate,Unit,OrderDate,TermsofDelivery,TotalAmount

            mdt.Columns.Add("SLNO", (typeof(int)));
            mdt.Columns.Add("DeliveryDate", (typeof(string)));
            mdt.Columns.Add("Party", (typeof(string)));
            mdt.Columns.Add("VchNo", (typeof(string)));
            mdt.Columns.Add("TemplateName", (typeof(string)));
            mdt.Columns.Add("HSNsacCode", (typeof(string)));
            mdt.Columns.Add("Qty", (typeof(string)));
            mdt.Columns.Add("Rate", (typeof(string)));
            mdt.Columns.Add("Unit", (typeof(string)));
            mdt.Columns.Add("OrderDate", (typeof(string)));
            mdt.Columns.Add("TermsofDelivery", (typeof(string)));
            mdt.Columns.Add("TotalAmount", (typeof(decimal)));

            grd.DataSource = mdt;
            grd.Columns["TotalAmount"].DefaultCellStyle.Format = "0.00##";
            #region Header Text
            // SLNO,DeliveryDate,Party,VchNo,TemplateName,HSNsacCode,
            //    Qty, Rate,Unit,OrderDate,TermsofDelivery,TotalAmount
            grd.Columns["SLNO"].HeaderText = "SL NO";
            grd.Columns["DeliveryDate"].HeaderText = "DELIVERY DATE";
            grd.Columns["VchNo"].HeaderText = "VOUCHAR NO";
            grd.Columns["Party"].HeaderText = "PARTY NAME";
            grd.Columns["TemplateName"].HeaderText = "ITEM DESCRIPTION";
            grd.Columns["HSNsacCode"].HeaderText = "HSN/SAC CODE";
            grd.Columns["Rate"].HeaderText = "RATE";
            grd.Columns["Unit"].HeaderText = "UNIT";
            grd.Columns["Qty"].HeaderText = "QTY";
            grd.Columns["OrderDate"].HeaderText = "ORDER DATE";
            grd.Columns["TermsofDelivery"].HeaderText = "TERMS OF DELIVERY";
            grd.Columns["TotalAmount"].HeaderText = "ORDER AMOUNT";

            #endregion

            #region Width
            grd.Columns["Party"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grd.Columns["SLNO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["DeliveryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grd.Columns["VchNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["TemplateName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["HSNsacCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Rate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["OrderDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["TermsofDelivery"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["TotalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            #endregion
     
            #region Header Alignment
            grd.Columns["SLNO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["VchNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["DeliveryDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["HSNsacCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TemplateName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Unit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Rate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["OrderDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["TermsofDelivery"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TotalAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion

            #region Cell Alignment
            grd.Columns["SLNO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["VchNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["DeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["HSNsacCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TemplateName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["OrderDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["TermsofDelivery"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion
        }
        private void InitSalesDataTable()
        {
            // SLNO,InvoiceDate,Party,VchNo,TemplateName,HSNsacCode,
            //    Qty, Rate,Unit,TotalAmount

            mdt.Columns.Add("SLNO", (typeof(int)));
            mdt.Columns.Add("InvoiceDate", (typeof(string)));
            mdt.Columns.Add("Party", (typeof(string)));
            mdt.Columns.Add("VchNo", (typeof(string)));
            mdt.Columns.Add("TemplateName", (typeof(string)));
            mdt.Columns.Add("HSNsacCode", (typeof(string)));
            mdt.Columns.Add("Qty", (typeof(string)));
            mdt.Columns.Add("Rate", (typeof(string)));
            mdt.Columns.Add("Unit", (typeof(string)));
            mdt.Columns.Add("TotalAmount", (typeof(decimal)));

            grd.DataSource = mdt;
            grd.Columns["TotalAmount"].DefaultCellStyle.Format = "0.00##";
            #region Header Text
            // SLNO,InvoiceDate,Party,VchNo,TemplateName,HSNsacCode,
            //    Qty, Rate,Unit,TotalAmount
            grd.Columns["SLNO"].HeaderText = "SL NO";
            grd.Columns["InvoiceDate"].HeaderText = "DELIVERY DATE";
            grd.Columns["VchNo"].HeaderText = "VOUCHAR NO";
            grd.Columns["Party"].HeaderText = "PARTY NAME";
            grd.Columns["TemplateName"].HeaderText = "ITEM DESCRIPTION";
            grd.Columns["HSNsacCode"].HeaderText = "HSN/SAC CODE";
            grd.Columns["Rate"].HeaderText = "RATE";
            grd.Columns["Unit"].HeaderText = "UNIT";
            grd.Columns["Qty"].HeaderText = "QTY";
            grd.Columns["TotalAmount"].HeaderText = "ORDER AMOUNT";

            #endregion

            #region Width
            grd.Columns["Party"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grd.Columns["SLNO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["InvoiceDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grd.Columns["VchNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["TemplateName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["HSNsacCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["Rate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns["TotalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            #endregion

            #region Header Alignment
            grd.Columns["SLNO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["VchNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["InvoiceDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["HSNsacCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TemplateName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Unit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Rate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["TotalAmount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion

            #region Cell Alignment
            grd.Columns["SLNO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["VchNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["InvoiceDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd.Columns["HSNsacCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["TemplateName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grd.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion
        }
        private void GenerateReport()
        {
            mFromDate = dtpFromDate.Text;
            mToDate = dtpToDate.Text;
            switch (mType)
            {
                case _Type.Order:
                    ShowOrderDetails();
                    break;
                case _Type.Sales:
                    ShowSalesDetails();
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Order Summary
        /// </summary>
        private void ShowOrderDetails()
        {
            string query = "Select SalesOrder.OrderId, SalesOrder.CustomerOrderNo,convert(varchar(11), " +
                           "SalesOrder.OrderDate,106) as OrderDate, SalesOrder.TotalAmount, SalesOrder.Status, " +
                           "SalesOrder.TermsofDelivery,convert(varchar(11),SalesOrder.DeliveryDate,106) as DeliveryDate,  " +
                           "LadgerMain.TemplateName FROM SalesOrder " +
                           "Inner join LadgerMain on SalesOrder.LedgerID=LadgerMain.LadgerID " +
                           "where DeliveryDate between '" + mFromDate + "' and '" + mToDate + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            mdt.Clear();
            if (dt.IsValidDataTable())
            {
                int slno = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string orderID = row["OrderId"].ToString();
                    string ledgerName = row["TemplateName"].ToString();
                    string orderNo = row["CustomerOrderNo"].ToString();
                    string orderDate = row["OrderDate"].ToString();
                    string totalAmount = row["TotalAmount"].toRound();
                    string termsofDelivery = row["TermsofDelivery"].ToString();
                    string status = row["Status"].ToString();
                    string deliveryDate = row["DeliveryDate"].ToString();

                    string itemName = "", hsnCode = "", qty = "", rate = "", unit = "";
                    GetOrderItemDetails(orderID, out itemName, out hsnCode, out qty, out rate, out unit);
                    mdt.Rows.Add(slno++, deliveryDate, ledgerName, orderNo, itemName, hsnCode, qty,
                                 rate.toRound(), unit, orderDate, termsofDelivery, totalAmount);
                }
            }
        }
        private void GetOrderItemDetails(string orderID, out string itemName, out string hsnCode, out string qty, out string rate, out string unit)
        {
            itemName = ""; hsnCode = ""; qty = ""; rate = ""; unit = "";
            string query = "Select * FROM SalesOrderDetails where OrderID='" + orderID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                int slno = 1;
                // SLNO,Party,VchNo,OrderDate,DeliveryDate,TermsofDelivery,ItemID,HSNsacCode,
                //   TemplateName, Qty, Unit,Rate,TotalAmount
                foreach (DataRow row in dt.Rows)
                {
                    if (slno == 1)
                    {
                        itemName = slno.ToString() + ") " + row["ItemName"].ToString();
                        hsnCode = row["ComodityCode"].ToString();
                        qty = row["Qty"].ToString();
                        rate = row["Rate"].ToString();
                        unit = row["Unit"].ToString();
                    }
                    else
                    {
                        itemName += "\n" + slno.ToString() + ") " + row["ItemName"].ToString();
                        hsnCode += "\n" + row["ComodityCode"].ToString();
                        qty += "\n" + row["Qty"].ToString();
                        rate += "\n" + row["Rate"].ToString();
                        unit += "\n" + row["Unit"].ToString();
                    }
                    slno++;
                }
            }
        }
       
        /// <summary>
        /// Sales Summary
        /// </summary>
        private void ShowSalesDetails()
        {
            string query = "Select Invoice.InvoiceNo, convert(varchar(11), Invoice.InvoiceDate,106) as InvoiceDate, "+
                           "Invoice.TotalInvoiceAmount, Invoice.Status, LadgerMain.TemplateName FROM Invoice " +
                           "Inner join LadgerMain on Invoice.LedgerID=LadgerMain.LadgerID " +
                           "where InvoiceDate between '" + mFromDate + "' and '" + mToDate + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            mdt.Clear();
            if (dt.IsValidDataTable())
            {
                int slno = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string invoiceNo = row["InvoiceNo"].ToString();

                    string ledgerName = row["TemplateName"].ToString();
                    string invoiceDate = row["InvoiceDate"].ToString();
                    string totalAmount = row["TotalInvoiceAmount"].toRound();
                    string status = row["Status"].ToString();

                    string itemName = "", hsnCode = "", qty = "", rate = "", unit = "";
                    GetSalesItemDetails(invoiceNo, out itemName, out hsnCode, out qty, out rate, out unit);
                    mdt.Rows.Add(slno++, invoiceDate, ledgerName, invoiceNo, itemName, hsnCode, qty,
                                 rate.toRound(), unit,  totalAmount);
                }
            }
        }
        private void GetSalesItemDetails(string invoiceNo, out string itemName, out string hsnCode, out string qty, out string rate, out string unit)
        {
            itemName = ""; hsnCode = ""; qty = ""; rate = ""; unit = "";
            string query = "Select * FROM InvoiceDetails where InvoiceNo='" + invoiceNo + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                int slno = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (slno == 1)
                    {
                        itemName = slno.ToString() + ") " + row["ItemName"].ToString();
                        hsnCode = row["HSNCode"].ToString();
                        qty = row["Quantity"].ToString();
                        rate = row["Rate"].ToString();
                        unit = row["Unit"].ToString();
                    }
                    else
                    {
                        itemName += "\n" + slno.ToString() + ") " + row["ItemName"].ToString();
                        hsnCode += "\n" + row["HSNCode"].ToString();
                        qty += "\n" + row["Quantity"].ToString();
                        rate += "\n" + row["Rate"].ToString();
                        unit += "\n" + row["Unit"].ToString();
                    }
                    slno++;
                }
            }
        }

        /// <summary>
        /// Date Filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
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
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            dtpFromDate.Text = mFromDate;
            dtpToDate.Text = mToDate;
        }
        private void btnStatement_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = mExcelFileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                switch (mType)
                {
                    case _Type.Order:
                        ExportToExcelOrder(sfd.FileName);
                        break;
                    case _Type.Sales:
                        ExportToExcelSales(sfd.FileName);
                        break;
                    default:
                        break;
                }
                Cursor = Cursors.Default;
            }
        }
        private void ExportToExcelOrder(string fileName)
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "L1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= grd.Columns.Count - 1; index++)
                {
                    if (grd.Columns[index].Visible)
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = grd.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "L" + (grd.Rows.Count + 1));
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

                Excel.Range qtyandrare = mWorkSheet.get_Range("G2", "H" + (grd.Rows.Count + 1));
                qtyandrare.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                qtyandrare.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range amountalignment = mWorkSheet.get_Range("L2", "L" + (grd.Rows.Count + 1));
                qtyandrare.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                qtyandrare.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= grd.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= grd.Columns.Count - 1; j++)
                    {
                        if (grd.Columns[j].Visible)
                        {
                            DataGridViewCell cell = grd[j, i];
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
        private void ExportToExcelSales(string fileName)
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "J1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= grd.Columns.Count - 1; index++)
                {
                    if (grd.Columns[index].Visible)
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = grd.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "J" + (grd.Rows.Count + 1));
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

                Excel.Range qtyandrare = mWorkSheet.get_Range("G2", "H" + (grd.Rows.Count + 1));
                qtyandrare.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                qtyandrare.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range amountalignment = mWorkSheet.get_Range("J2", "J" + (grd.Rows.Count + 1));
                qtyandrare.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                qtyandrare.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= grd.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= grd.Columns.Count - 1; j++)
                    {
                        if (grd.Columns[j].Visible)
                        {
                            DataGridViewCell cell = grd[j, i];
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

        private void Sales_OrderItemSummary_Shown(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(null, null);
            GenerateReport();
        }
    }
}
