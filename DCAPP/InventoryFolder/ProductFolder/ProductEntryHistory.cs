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
    public partial class ProductEntryHistory : Form
    {
        string msg = "";
        DataTable mdt = new DataTable();
        public ProductEntryHistory()
        {
            InitializeComponent();
            InitDataTable();
        }
        private void InitDataTable()
        {
            mdt.Columns.Add("SLNO", (typeof(int)));

            mdt.Columns.Add("ItemID", (typeof(string)));
            mdt.Columns.Add("VoucherNo", (typeof(string)));
            mdt.Columns.Add("HSNsacCode", (typeof(string)));
            mdt.Columns.Add("ItemName", (typeof(string)));
            mdt.Columns.Add("TemplateName", (typeof(string)));
            mdt.Columns.Add("BatchNo", (typeof(string)));
            mdt.Columns.Add("Category", (typeof(string)));
            mdt.Columns.Add("Stock", (typeof(string)));
            mdt.Columns.Add("SalseRate", (typeof(string)));
            mdt.Columns.Add("SalseUnit", (typeof(string)));
            mdt.Columns.Add("Company", (typeof(string)));
            mdt.Columns.Add("MRP", (typeof(string)));
            mdt.Columns.Add("MfgDate", (typeof(string)));
            mdt.Columns.Add("ExpDate", (typeof(string)));
            mdt.Columns.Add("PurchaseRate", (typeof(string)));
            mdt.Columns.Add("GSTRate", (typeof(string)));
            mdt.Columns.Add("CESSRate", (typeof(string)));
            mdt.Columns.Add("Type", (typeof(string)));

            dgvStockHistoryList.DataSource = mdt;

            #region Header Text

            dgvStockHistoryList.Columns["ItemID"].Visible = false;
            dgvStockHistoryList.Columns["TemplateName"].Visible = false;
            dgvStockHistoryList.Columns["SLNO"].Visible = false;

            dgvStockHistoryList.Columns["SLNO"].HeaderText = "SL No";
            dgvStockHistoryList.Columns["VoucherNo"].HeaderText = "Voucher No.";
            dgvStockHistoryList.Columns["ItemName"].HeaderText = "Item Name";
            dgvStockHistoryList.Columns["TemplateName"].HeaderText = "Item Description";
            dgvStockHistoryList.Columns["HSNsacCode"].HeaderText = "HSN/SAC Code";
            dgvStockHistoryList.Columns["Category"].HeaderText = "Category";
            dgvStockHistoryList.Columns["Stock"].HeaderText = "Available Stock";
            dgvStockHistoryList.Columns["MRP"].HeaderText = "MRP";
            dgvStockHistoryList.Columns["SalseRate"].HeaderText = "Sales Rate";
            dgvStockHistoryList.Columns["SalseUnit"].HeaderText = "Unit";
            dgvStockHistoryList.Columns["GSTRate"].HeaderText = "GST Rate";
            dgvStockHistoryList.Columns["CESSRate"].HeaderText = "CESS Rate";
            dgvStockHistoryList.Columns["type"].HeaderText = "Item Type";

            dgvStockHistoryList.Columns["BatchNo"].HeaderText = "Batch No.";
            dgvStockHistoryList.Columns["MfgDate"].HeaderText = "MFG Date";
            dgvStockHistoryList.Columns["ExpDate"].HeaderText = "EXP Date";
            dgvStockHistoryList.Columns["PurchaseRate"].HeaderText = "Purchase Rate";
            #endregion

            #region AllCells
            dgvStockHistoryList.Columns["SLNO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["VoucherNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["HSNsacCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //grd.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["GSTRate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["SalseUnit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["BatchNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["MfgDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["ExpDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["PurchaseRate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockHistoryList.Columns["type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            #endregion

            #region Header Alignment
            dgvStockHistoryList.Columns["VoucherNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["HSNsacCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["BatchNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["PurchaseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["TemplateName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["Category"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["Stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockHistoryList.Columns["PurchaseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockHistoryList.Columns["SalseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockHistoryList.Columns["SalseUnit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockHistoryList.Columns["Company"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            #endregion

            #region Cell Alignment
            dgvStockHistoryList.Columns["SLNO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockHistoryList.Columns["VoucherNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockHistoryList.Columns["HSNsacCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["GSTRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockHistoryList.Columns["type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockHistoryList.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockHistoryList.Columns["SalseRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockHistoryList.Columns["SalseUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockHistoryList.Columns["Company"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvStockHistoryList.Columns["PurchaseRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion
        }
        private void ShowDitails()
        {
            string query = "Select StockHistory.*,item.*,ItemCategory.CategoryName from StockHistory " +
                           "inner join item on StockHistory.ItemID = item.ID "+
                           "left join ItemCategory ON item.CategoryId = ItemCategory.ID "+
                           "order by StockHistory.ID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            mdt.Clear();

            if (dt.IsValidDataTable())
            {
                int slno = 0;

                foreach (DataRow row in dt.Rows)
                {
                    #region Data
                    string availQtyStatus = "", mrp = "", salesrate = "", saleUnit = "", prate = "";
                    string ItemID = row["id"].ToString();
                    string voucherNo = row["PurchaseBillNo"].ToString();
                    string HsnSacCode = row["ComodityCode"].ToString();
                    string ItemName = row["itemName"].ToString();
                    string tempName = row["TemplateName"].ToString();
                    string categoryName = row["CategoryName"].ToString();
                    string batchNo = row["BatchNo"].ToString();
                    string company = row["Company"].ToString();
                    string mfgDate = row["MfgDate"].ToString();
                    string expDate = row["EXPDate"].ToString();
                    string highestunit = row["HighestUnit"].ToString();
                    string higheststockqty = row["HighestStockQty"].ToString();
                    string highestrateo = row["HighestRate"].toRound();
                    string highestMRP = row["HighestMRP"].ToString();
                    string middleunit = row["MiddleUnit"].ToString();
                    string middlestockqty = row["MiddleStockQty"].ToString();
                    string middlerateo = row["MiddleRate"].toRound();
                    string middleMRP = row["MiddleMRP"].ToString();
                    string lowestunit = row["LowestUnit"].ToString();
                    string loweststockqty = row["LowestStockQty"].ToString();
                    string lowestrate = row["LowestRate"].toRound();
                    string lowestMRP = row["LowestMRP"].ToString();
                    string highestMeasureQty = row["HighestMeasureQty"].ToString();
                    string lowestMeasureQty = row["LowestMeasureQty"].ToString();
                    string purchaseQty = row["PurchaseQty"].ToString();
                    string purchaseRate = row["PurchaseRate"].toRound();
                    string purchaseUnit = row["PurchaseUnit"].ToString();
                    string gstStr = row["GSTRate"].ToString();
                    string itemType = row["ItemType"].ToString();

                    mrp = highestMeasureQty.ISNullOrWhiteSpace() ? highestMRP + "/" + highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? highestMRP + "/" + highestunit + " \n" + middleMRP + "/" + middleunit : highestMRP + "/" + highestunit + " \n" + middleMRP + "/" + middleunit + " \n" + lowestMRP + "/" + lowestunit;
                    // salesrate = highestMeasureQty.ISNullOrWhiteSpace() ? highestrateo + "/" + highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? highestrateo + "/" + highestunit + " \n" + middlerateo + "/" + middleunit : highestrateo + "/" + highestunit + " \n" + middlerateo + "/" + middleunit + " \n" + lowestrate + "/" + lowestunit;
                    salesrate = highestMeasureQty.ISNullOrWhiteSpace() ? highestrateo : lowestMeasureQty.ISNullOrWhiteSpace() ? highestrateo + "\n" + middlerateo : highestrateo + "\n" + middlerateo + "\n" + lowestrate;
                    saleUnit = highestMeasureQty.ISNullOrWhiteSpace() ? highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? highestunit + "\n" + middleunit : highestunit + "\n" + middleunit + "\n" + lowestunit;
                    prate = purchaseRate.ISNullOrWhiteSpace() ? "----" : purchaseRate + "/" + purchaseUnit;

                    double availQty = 0d;
                    double.TryParse(higheststockqty, out availQty);
                    availQtyStatus = availQty.ToString();
                    Color clr = Color.Black;
                    if (availQty <= 0)
                    {
                        availQtyStatus = "Out of Stock";
                        clr = Color.Pink;
                    }
                    else
                    {
                        availQtyStatus = highestMeasureQty.ISNullOrWhiteSpace() ? higheststockqty + " " + highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? higheststockqty + " " + highestunit + "  / \n" + middlestockqty + " " + middleunit : higheststockqty + " " + highestunit + "  / \n" + middlestockqty + " " + middleunit + "  / \n" + loweststockqty + " " + lowestunit;
                    }
                    if (gstStr != "Exampted" && (gstStr != "Non GST" && gstStr != "Nil"))
                    {
                        gstStr = gstStr + "%";
                    }
                    string cessStr = row["CessRate"].ToString();
                    if (!cessStr.ISNullOrWhiteSpace())
                    {
                        cessStr = cessStr + "%";
                    }

                    #endregion
                    mdt.Rows.Add(slno++, ItemID, voucherNo,HsnSacCode, ItemName, tempName, batchNo, categoryName, availQtyStatus,
                                 salesrate, saleUnit, company, mrp, DateTime.Parse(mfgDate).ToString("dd-MMM-yyyy"), DateTime.Parse(expDate).ToString("dd-MMM-yyyy"),
                                 prate, gstStr, cessStr, itemType);
                    if (availQtyStatus == "Out of Stock")
                    {
                        dgvStockHistoryList.Rows[slno - 1].DefaultCellStyle.BackColor = clr;
                    }
                    if (DateTime.Parse(expDate) < DateTime.Now.Date)
                    {
                        dgvStockHistoryList.Rows[slno - 1].Cells["EXPDate"].Style.BackColor = Color.MediumVioletRed;
                    }
                }
            }
        }
        private void txtSerachByName_TextChanged(object sender, EventArgs e)
        {
            DataView dtv = new DataView(mdt);
            dtv.RowFilter = string.Format("TemplateName like '{0}%'", txtSerachByName.Text.GetDBFormatString());
            dgvStockHistoryList.DataSource = dtv;
        }
        private void ProductEntryHistory_Shown(object sender, EventArgs e)
        {
            ShowDitails();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Stock_Update_List.xls";
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

                Excel.Range heder = mWorkSheet.get_Range("A1", "P1");
                heder.RowHeight = 28;
                heder.Font.Size = 16;
                heder.Font.Bold = true;
                heder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                heder.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                heder.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                heder.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                //add Header
                int xlColIndex = 1;
                for (int index = 0; index <= dgvStockHistoryList.Columns.Count - 1; index++)
                {
                    if (dgvStockHistoryList.Columns[index].Visible)
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvStockHistoryList.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "P" + (dgvStockHistoryList.Rows.Count + 1));
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

                Excel.Range amountalignment = mWorkSheet.get_Range("G2", "G" + (dgvStockHistoryList.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvStockHistoryList.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvStockHistoryList.Columns.Count - 1; j++)
                    {
                        if (dgvStockHistoryList.Columns[j].Visible)
                        {
                            DataGridViewCell cell = dgvStockHistoryList[j, i];
                            mWorkSheet.Cells[i + 2, col] = cell.Value;
                            if (dgvStockHistoryList.Columns[j].Name == "Stock")
                            {
                                if (cell.Value.ToString() == "Out of Stock")
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
    }
}
