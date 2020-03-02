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
    public partial class Summary_CategoryWise : Form
    {
        string msg = "", mCateGoryName = "", mFromDate = "", mToDate = "";
        public Summary_CategoryWise()
        {
            InitializeComponent();
            cmbCateGory.AddItemCategory();

            cmbCateGory.SelectedIndex = cmbCateGory.Items.Count > 0 ? 0 : -1;
            toolStripCurrentMonth_Click(null, null);
            this.FitToVertical();
        }

        #region --------------------------- Purchase --------------------------
        private void GenaratePurchaseSummary_Categorywise()
        {
            double totalamount = 0d;
            lblTotalAmount.Text = "0".toRound();
            string catagoryname = "";
            dgvSummary.Rows.Clear();
            string QUERY = "select ItemCategory.CategoryName,PurchaseBillDetails.Unit,PurchaseBillDetails.Rate," +
                            "SUM(PurchaseBillDetails.Quantity) as qty,(PurchaseBillDetails.Rate*SUM(PurchaseBillDetails.Quantity)) as total " +
                            " from PurchaseBillDetails inner join PurchaseBill on PurchaseBill.BillID = PurchaseBillDetails.Billid " +
                            " inner join item on PurchaseBillDetails.ItemID = item.ID inner join ItemCategory on item.CategoryId = ItemCategory.id " +
                            " where CategoryName = '" + mCateGoryName + "' and PurchaseBill.InvoiceDate between '" + mFromDate + "' and '" + mToDate + "' " +
                            " group by PurchaseBillDetails.Unit,ItemCategory.CategoryName,PurchaseBillDetails.Rate";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(QUERY, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    catagoryname = item["CategoryName"].ToString();
                    string qty = item["qty"].ToString();
                    string unit = item["Unit"].ToString();
                    string rate = item["Rate"].ToString();
                    string Total = item["total"].ToString();
                    double totald = 0d;
                    double.TryParse(Total, out totald);
                    totalamount = totalamount + totald;
                    dgvSummary.Rows.Add(catagoryname, qty, unit, rate.toRound(), Total.toRound());

                }
            }
            lblTotalAmount.Text = totalamount.toString();
            lblCategory.Text = catagoryname;


        }
        private void CalCulateQuantutyForPurchase()
        {
            txtrQty.Text = "";
          string  qtuandunit = "";
            string query = "select ItemCategory.CategoryName,PurchaseBillDetails.Unit,Sum(PurchaseBillDetails.Quantity) as qty from PurchaseBillDetails " +
                          " inner join PurchaseBill on PurchaseBill.BillID = PurchaseBillDetails.Billid  inner " +
                          " join item on PurchaseBillDetails.ItemID = item.ID  inner " +
                         " join ItemCategory on item.CategoryId = ItemCategory.id where CategoryName = '" + mCateGoryName + "' " +
                         " and PurchaseBill.InvoiceDate between '" + mFromDate + "' and '" + mToDate + "' group by PurchaseBillDetails.Unit,ItemCategory.CategoryName ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string unit = item["Unit"].ToString();
                    string qty = item["qty"].ToString();
                    qtuandunit = qtuandunit+qty + "                      " + unit + "\n";
                    txtrQty.Text = qtuandunit;
                }
            }

        }

        #endregion --------------------------- End Purchase --------------------------

        #region --------------------------- InVoice --------------------------
        private void GenarateInVoiceSummary_Categorywise()
        {
            double totalamount = 0d;
            lblTotalAmount.Text = "0".toRound();
            string catagoryname = "";
            dgvSummary.Rows.Clear();
            string QUERY = " select ItemCategory.CategoryName,InvoiceDetails.Unit,InvoiceDetails.Rate," +
                            " SUM(InvoiceDetails.Quantity) as qty,(InvoiceDetails.Rate * SUM(InvoiceDetails.Quantity)) as total " +
                             " from InvoiceDetails inner join Invoice on InvoiceDetails.InvoiceNo = Invoice.InvoiceNo inner " +
                             " join item on InvoiceDetails.ItemID = item.ID inner join ItemCategory on item.CategoryId = ItemCategory.id " +
                              " where CategoryName = '" + mCateGoryName + "' and Invoice.InvoiceDate between '" + mFromDate + "' and '" + mToDate + "' " +
                                " group by InvoiceDetails.Unit,ItemCategory.CategoryName,InvoiceDetails.Rate ";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(QUERY, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    catagoryname = item["CategoryName"].ToString();
                    string qty = item["qty"].ToString();
                    string unit = item["Unit"].ToString();
                    string rate = item["Rate"].ToString();
                    string Total = item["total"].ToString();
                    double totald = 0d;
                    double.TryParse(Total, out totald);
                    totalamount = totalamount + totald;
                    dgvSummary.Rows.Add(catagoryname, qty, unit, rate.toRound(), Total.toRound());

                }
            }
            lblTotalAmount.Text = totalamount.toString();
            lblCategory.Text = catagoryname;


        }
        private void CalCulateQuantutyForInVoice()
        {
            txtrQty.Clear();
            string qtuandunit = "";
            string query = "select ItemCategory.CategoryName,InvoiceDetails.Unit,Sum(InvoiceDetails.Quantity) as qty " +
                             " from InvoiceDetails inner join Invoice on InvoiceDetails.InvoiceNo = Invoice.InvoiceNo " +
                             " inner join item on InvoiceDetails.ItemID = item.ID inner join ItemCategory on item.CategoryId = ItemCategory.id" +
                              " where CategoryName = '" + mCateGoryName + "' and Invoice.InvoiceDate between '" + mFromDate + "' and '" + mToDate + "' group by InvoiceDetails.Unit,ItemCategory.CategoryName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                
                foreach (DataRow item in dt.Rows)
                {

                    string unit = item["Unit"].ToString();
                    string qty = item["qty"].ToString();
                    qtuandunit = qtuandunit+ qty + "                      " + unit + "\n";
                    txtrQty.Text = qtuandunit;
                }
            }

        }

        #endregion --------------------------- End InVoice --------------------------

        #region --------------------------- Stock --------------------------
        private void GenarateStockSummary_Categorywise()
        {
            double totalamount = 0d;
            lblTotalAmount.Text = "0".toRound();
            string catagoryname = "";
            dgvSummary.Rows.Clear();
            string QUERY = "  select ItemCategory.CategoryName,StockSummary.HighestUnit,StockSummary.PurchaseRate," +
                         " SUM(StockSummary.HighestStockQty) as qty,(StockSummary.PurchaseRate * SUM(StockSummary.HighestStockQty)) as total " +
                         " from StockSummary inner join item on StockSummary.ItemID = item.ID  inner " +
                        " join ItemCategory on item.CategoryId = ItemCategory.id  where CategoryName = '" + mCateGoryName + "'" +
                        " and Isnull(StockSummary.PurchaseRate, '')!= '' group by StockSummary.HighestUnit,ItemCategory.CategoryName,StockSummary.PurchaseRate";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(QUERY, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    catagoryname = item["CategoryName"].ToString();
                    string qty = item["qty"].ToString();
                    string unit = item["HighestUnit"].ToString();
                    string rate = item["PurchaseRate"].ToString();
                    string Total = item["total"].ToString();
                    double totald = 0d;
                    double.TryParse(Total, out totald);
                    totalamount = totalamount + totald;
                    dgvSummary.Rows.Add(catagoryname, qty, unit, rate.toRound(), Total.toRound());

                }
            }
            lblTotalAmount.Text = totalamount.toString();
            lblCategory.Text = catagoryname;


        }
        private void CalCulateQuantutyForStock()
        {
            txtrQty.Clear();
            string qtuandunit = "";
            string query = "select ItemCategory.CategoryName,StockSummary.HighestUnit,Sum(StockSummary.HighestStockQty) as qty " +
                         " from StockSummary inner join item on StockSummary.ItemID = item.ID " +
                         " inner join ItemCategory on item.CategoryId = ItemCategory.id " +
                          " where CategoryName = '"+mCateGoryName+"' and Isnull(StockSummary.PurchaseRate, '')!= '' group by StockSummary.HighestUnit,ItemCategory.CategoryName ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {

                    string unit = item["HighestUnit"].ToString();
                    string qty = item["qty"].ToString();
                    qtuandunit = qtuandunit+ qty + "                      " + unit + "\n";
                    txtrQty.Text = qtuandunit;
                }
            }

        }

        #endregion --------------------------- End Stock --------------------------

        private void cmbCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!cmbCateGory.Text.ISNullOrWhiteSpace())
            {
                pnlFilter.Show();
                if (rbtnpurchase.Checked)
                {
                    mCateGoryName = cmbCateGory.Text;
                    GenaratePurchaseSummary_Categorywise();
                    CalCulateQuantutyForPurchase();
                }
                else if (rbtnSales.Checked)
                {
                    mCateGoryName = cmbCateGory.Text;
                    GenarateInVoiceSummary_Categorywise();
                    CalCulateQuantutyForInVoice();
                }
                else if (rbtnStock.Checked)
                {
                    pnlFilter.Hide();
                    mCateGoryName = cmbCateGory.Text;
                    GenarateStockSummary_Categorywise();
                    CalCulateQuantutyForStock();
                }

            }
        }

        #region --------------------------FilterBy----------------------------
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            cmbCateGory_SelectedIndexChanged(null, null);
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();

            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            cmbCateGory_SelectedIndexChanged(null, null);

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
            pnlCustomDate.Hide();

            mFromDate = "01-" + month.GetMonthMMM() + "-" + year;
            mToDate = DateTime.DaysInMonth(year, month) + "-" + month.GetMonthMMM() + "-" + year;
            lblFilterHeader.Text = toolStripPreviousMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            cmbCateGory_SelectedIndexChanged(null, null);

        }
        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Hide();

            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            cmbCateGory_SelectedIndexChanged(null, null);

        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlCustomDate.Show();
            mFromDate = dtmStartDate.Text;
            mToDate = dtmEndDate.Text;
            lblFilterHeader.Text = customToolStripMenuItem.Text + " (" + mFromDate + " - " + mToDate + ")";
            cmbCateGory_SelectedIndexChanged(null, null);

        }
        private void dtmStartDate_Leave(object sender, EventArgs e)
        {
            customToolStripMenuItem_Click(null, null);
        }
        private void dtmEndDate_Leave(object sender, EventArgs e)
        {
            customToolStripMenuItem_Click(null, null);
        }
        #endregion -------------------------- End FilterBy----------------------------

        private void rbtnpurchase_CheckedChanged(object sender, EventArgs e)
        {
            cmbCateGory_SelectedIndexChanged(null, null);
        }
        private void rbtnSales_CheckedChanged(object sender, EventArgs e)
        {
            cmbCateGory_SelectedIndexChanged(null, null);
        }
        private void rbtnStock_CheckedChanged(object sender, EventArgs e)
        {
            cmbCateGory_SelectedIndexChanged(null, null);
        }

        #region ----------------------- Export To Excel--------------
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            if (rbtnpurchase.Checked)
            {
                sfd.FileName = "Purchse_Cate_Summary.xls";
            }
            else if (rbtnSales.Checked)
            {
                sfd.FileName = "Sales_Cate_Summary.xls";
            }
            else if (rbtnStock.Checked)
            {
                sfd.FileName = "Stock_Cate_Summary.xls";
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                ExportToExcel(sfd.FileName);
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
                for (int index = 0; index <= dgvSummary.Columns.Count - 1; index++)
                {
                    if (dgvSummary.Columns[index].Visible)
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvSummary.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "E" + (dgvSummary.Rows.Count + 4));
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

                Excel.Range qtyandrare = mWorkSheet.get_Range("D2", "E" + (dgvSummary.Rows.Count + 1));
                qtyandrare.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                qtyandrare.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                #endregion

                 


                Excel.Range heder2 = mWorkSheet.get_Range("A" + (dgvSummary.Rows.Count + 3), "E" + (dgvSummary.Rows.Count + 3));
                heder2.RowHeight = 28;
                heder2.Font.Size = 16;
                heder2.Font.Bold = true;
                heder2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                mWorkSheet.Cells[dgvSummary.Rows.Count + 3,1 ] = "CATEGORY NAME";

                heder2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                Excel.Range MERGE1 = mWorkSheet.get_Range("B" + (dgvSummary.Rows.Count + 3), "C" + (dgvSummary.Rows.Count + 3));
                MERGE1.MergeCells = true;
                mWorkSheet.Cells[dgvSummary.Rows.Count + 3, 2] = "QUANTITY UNIT";

                Excel.Range MERGE = mWorkSheet.get_Range("D" + (dgvSummary.Rows.Count + 3), "E" + (dgvSummary.Rows.Count + 3));
                MERGE.MergeCells=true;
               
                mWorkSheet.Cells[dgvSummary.Rows.Count + 3, 4] = "TOTAL AMOUNT";

                mWorkSheet.Cells[dgvSummary.Rows.Count + 4, 1] = lblCategory.Text;

                Excel.Range MERGE3 = mWorkSheet.get_Range("B" + (dgvSummary.Rows.Count + 4), "C" + (dgvSummary.Rows.Count + 4));
                MERGE3.MergeCells = true;

                mWorkSheet.get_Range("B" + (dgvSummary.Rows.Count + 4), "B" + (dgvSummary.Rows.Count + 4)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                mWorkSheet.get_Range("B" + (dgvSummary.Rows.Count + 4), "B" + (dgvSummary.Rows.Count + 4)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                mWorkSheet.Cells[dgvSummary.Rows.Count + 4, 2] = txtrQty.Text;

                Excel.Range MERGE4 = mWorkSheet.get_Range("D" + (dgvSummary.Rows.Count + 4), "E" + (dgvSummary.Rows.Count + 4));
                MERGE4.MergeCells = true;
                mWorkSheet.get_Range("D" + (dgvSummary.Rows.Count + 4), "D" + (dgvSummary.Rows.Count + 4)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                mWorkSheet.get_Range("D" + (dgvSummary.Rows.Count + 4), "D" + (dgvSummary.Rows.Count + 4)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                mWorkSheet.Cells[dgvSummary.Rows.Count + 4, 4] = lblTotalAmount.Text;

                Excel.Range autofill2 = mWorkSheet.get_Range("A" + (dgvSummary.Rows.Count + 4), "E" + (dgvSummary.Rows.Count + 4));
                autofill2.Rows.RowHeight = 85.5;


                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvSummary.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvSummary.Columns.Count - 1; j++)
                    {
                        if (dgvSummary.Columns[j].Visible)
                        {
                            DataGridViewCell cell = dgvSummary[j, i];
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
        #endregion ------------------------------------Export To Excel----------------------------------



    }

}
