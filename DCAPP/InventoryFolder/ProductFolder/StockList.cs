using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class StockList : Form
    {
        string msg = "";
        DataTable mdt = new DataTable();
        public StockList()
        {
            InitializeComponent();
            InitDataTable();
            lblFilterClear.Hide();
            lblfilterString.Text = "";
            rbtnExpiryAll.Checked = true;
            rbtnStockAll.Checked = true;
            cmbCategory.AddItemCategory();
            cmbSubCategory.AddSubCategory();
            cmbCompany.AddItemCompany();
        }
        private void InitDataTable()
        {
            mdt.Columns.Add("StockSummaryID", (typeof(string)));
            mdt.Columns.Add("SLNO", (typeof(int)));

            mdt.Columns.Add("HSNsacCode", (typeof(string)));
            mdt.Columns.Add("ItemName", (typeof(string)));
            mdt.Columns.Add("TemplateName", (typeof(string)));
            mdt.Columns.Add("BatchNo", (typeof(string)));
            mdt.Columns.Add("Category", (typeof(string)));
            mdt.Columns.Add("SubCategory", (typeof(string)));
            mdt.Columns.Add("Stock", (typeof(string)));
            mdt.Columns.Add("NetStock", (typeof(string)));
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
            //HSNsacCode,ItemName,TemplateName,BatchNo,Category,SubCategory,Stock,SalseRate,SalseUnit
            //Company,MRP,MfgDate,ExpDate,PurchaseRate,GSTRate,CESSRate,Type
            dgvStockList.DataSource = mdt;

            #region Header Text

            dgvStockList.Columns["StockSummaryID"].Visible = false;
            dgvStockList.Columns["TemplateName"].Visible = false;
            dgvStockList.Columns["SLNO"].Visible = false;
            dgvStockList.Columns["Stock"].Visible = false;

            dgvStockList.Columns["SLNO"].HeaderText = "SL No";
            dgvStockList.Columns["ItemName"].HeaderText = "Item Name";
            dgvStockList.Columns["TemplateName"].HeaderText = "Item Description";
            dgvStockList.Columns["HSNsacCode"].HeaderText = "HSN/SAC Code";
            dgvStockList.Columns["Category"].HeaderText = "Category";
            dgvStockList.Columns["SubCategory"].HeaderText = "Sub Category";
            dgvStockList.Columns["Stock"].HeaderText = "Available Stock";
            dgvStockList.Columns["NetStock"].HeaderText = "Net Stock";
            dgvStockList.Columns["MRP"].HeaderText = "MRP";
            dgvStockList.Columns["SalseRate"].HeaderText = "Sales Rate";
            dgvStockList.Columns["SalseUnit"].HeaderText = "Unit";
            dgvStockList.Columns["GSTRate"].HeaderText = "GST Rate";
            dgvStockList.Columns["CESSRate"].HeaderText = "CESS Rate";
            dgvStockList.Columns["type"].HeaderText = "Item Type";

            dgvStockList.Columns["BatchNo"].HeaderText = "Batch No.";
            dgvStockList.Columns["MfgDate"].HeaderText = "MFG Date";
            dgvStockList.Columns["ExpDate"].HeaderText = "EXP Date";
            dgvStockList.Columns["PurchaseRate"].HeaderText = "Purchase Rate";
            #endregion

            #region AllCells
            dgvStockList.Columns["SLNO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["ItemName"].Width = 200;
            dgvStockList.Columns["HSNsacCode"].Width = 80;// = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["Category"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["SubCategory"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["GSTRate"].Width = 50;// = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["CESSRate"].Width = 50;
            dgvStockList.Columns["SalseUnit"].Width = 50;//.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["BatchNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["MfgDate"].Width = 100;//.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["ExpDate"].Width =100;//.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["PurchaseRate"].Width = 100;//.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStockList.Columns["type"].Width = 60;//.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            #endregion

            #region Header Alignment
            dgvStockList.Columns["HSNsacCode"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["BatchNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["PurchaseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["TemplateName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["Category"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["SubCategory"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["Stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvStockList.Columns["NetStock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["PurchaseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["SalseRate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["SalseUnit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockList.Columns["Company"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            #endregion

            #region Cell Alignment
            dgvStockList.Columns["SLNO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockList.Columns["HSNsacCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["GSTRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockList.Columns["type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStockList.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["NetStock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["SalseRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockList.Columns["SalseUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStockList.Columns["Company"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvStockList.Columns["PurchaseRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion
        }
        private void ShowDitails()
        {
            string query = "Select StockSummary.*,item.ItemName, item.ItemType, item.ComodityCode, item.GSTRate, " +
                           "item.CessRate, item.CGSTRate, item.SGSTRate, item.Narration, item.IGSTRATE, " +
                           "item.Company, item.TemplateName, item.SubCategory, ItemCategory.CategoryName from StockSummary " +
                           "inner join item on StockSummary.ItemID = item.ID " +
                           "left join ItemCategory ON item.CategoryId = ItemCategory.ID order by item.TemplateName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            mdt.Clear();

            if (dt.IsValidDataTable())
            {
                int slno = 0;
                foreach (DataRow row in dt.Rows)
                {
                    #region Data
                    string availQtyStatus = "", mrp = "", salesrate = "", saleUnit = "", prate = "";
                    string stockSummaryID = row["Id"].ToString();
                    string HsnSacCode = row["ComodityCode"].ToString();
                    string ItemName = row["itemName"].ToString();
                    string tempName = row["TemplateName"].ToString();
                    string categoryName = row["CategoryName"].ToString();
                    string subCategoryName = row["SubCategory"].ToString();
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
                    string NetAvable = "",netmstr="",netlowstr="";
                    double nethqty=0, netmqty=0, netlqty = 0;
                    int highmesureqty = 0, lowsestmwsqty = 0;

                    double.TryParse(higheststockqty,out nethqty);
                    double.TryParse(middlestockqty, out netmqty);
                    double.TryParse(loweststockqty, out netlqty);

                    int.TryParse(highestMeasureQty, out highmesureqty);
                    int.TryParse(lowestMeasureQty, out lowsestmwsqty);
                    try
                    {
                        netmqty = (int)netmqty % highmesureqty;
                        netlqty = (int)netlqty % lowsestmwsqty;
                    }
                    catch (Exception)
                    {
                    }
                    netmstr = netmqty != 0 ? "\n"+netmqty.ToString() + " " + middleunit:string.Empty;
                    netlowstr = netlqty != 0 ? "\n"+netlqty.ToString() + " " + lowestunit:string.Empty;

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
                        NetAvable = "Out of Stock";
                        clr = Color.Pink;
                    }
                    else
                    {
                        availQtyStatus = highestMeasureQty.ISNullOrWhiteSpace() ? higheststockqty + " " + highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? higheststockqty + " " + highestunit + "  / \n" + middlestockqty + " " + middleunit : higheststockqty + " " + highestunit + "  / \n" + middlestockqty + " " + middleunit + "  / \n" + loweststockqty + " " + lowestunit;
                        NetAvable = highestMeasureQty.ISNullOrWhiteSpace() ?((int)nethqty).ToString() + " " + highestunit : lowestMeasureQty.ISNullOrWhiteSpace() ? ((int)nethqty).ToString() + " " + highestunit + netmstr : ((int)nethqty).ToString() + " " + highestunit +netmstr+netlowstr;
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
                    mdt.Rows.Add(stockSummaryID, slno++, HsnSacCode, ItemName, tempName, batchNo, categoryName,
                                 subCategoryName, availQtyStatus, NetAvable, salesrate, saleUnit, company, mrp,
                                 DateTime.Parse(mfgDate).ToString("dd-MMM-yyyy"), DateTime.Parse(expDate).ToString("dd-MMM-yyyy"),
                                 prate, gstStr, cessStr, itemType);
                }
            }
        }
        private void btnAddServicesAndGoods_Click(object sender, EventArgs e)
        {
            if (CheckForStockUpdate())
            {
                StockEntry frmStockEntry = new StockEntry("");
                frmStockEntry.OnClose += new Action(ShowDitails);
                frmStockEntry.ShowDialog();
            }
            else
            {
                MessageBox.Show("Purchase bill not found for stock update.", "Stock Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool CheckForStockUpdate()
        {
            string query = "Select BillNo,InvoiceNo from PurchaseBill where" +
                " BillNO not in (select PurchaseBillNO from StockHistory" +
                " where PurchaseBillNO <> 'NULL' ) ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                return true;
            }
            return false;
        }
        private void txtSerachByName_TextChanged(object sender, EventArgs e)
        {
            DataView dtv = new DataView(mdt);
            dtv.RowFilter = string.Format("TemplateName like '{0}%'", txtSerachByName.Text.GetDBFormatString());
            dgvStockList.DataSource = dtv;
        }
        private void txtSearchByCategory_TextChanged(object sender, EventArgs e)
        {
            DataView dtv = new DataView(mdt);
            dtv.RowFilter = string.Format("Category like '{0}%'", cmbCategory.Text.GetDBFormatString());
            dgvStockList.DataSource = dtv;
        }
        private void ItemListView_Shown(object sender, EventArgs e)
        {
            ShowDitails();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvStockList.SelectedRows.Count > 0)
            {
                string itemName = dgvStockList.SelectedRows[0].Cells["TemplateName"].Value.ToString();
                if (MessageBox.Show("Once record Remove can't be undo.Are you sure ?", "Remove Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string query = "delete from item where TemplateName='" + itemName + "'";
                    if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
                    {
                        ShowDitails();
                    }
                    else
                    {
                        MessageBox.Show("This Item Already in Use  you Can't Remove this.", "Remove Record", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
            }
        }
        private void btnStockEntryManual_Click(object sender, EventArgs e)
        {
            StockEntry frmStockEntry = new StockEntry();
            frmStockEntry.OnClose += new Action(ShowDitails);
            frmStockEntry.ShowDialog();
        }
        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStockList.SelectedRows.Count > 0)
            {
                string stockSummaryID = dgvStockList.SelectedRows[0].Cells["StockSummaryID"].Value.ToString();
                StockEdit frmStockEdit = new StockEdit(stockSummaryID);
                frmStockEdit.OnClose += StockEdit_Onclose;
                frmStockEdit.ShowDialog();
            }
        }
        private void StockEdit_Onclose()
        {
            cmbCategory.AddItemCategory();
            cmbSubCategory.AddSubCategory();
            cmbCompany.AddItemCompany();
            ShowDitails();
        }
        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            txtSerachByName.Clear();
            //HSNsacCode,ItemName,TemplateName,BatchNo,Category,SubCategory,Stock,SalseRate,SalseUnit
            //Company,MRP,MfgDate,ExpDate,PurchaseRate,GSTRate,CESSRate,Type
            string filterBy = "";
            string query = "";
            string category = cmbCategory.Text.GetDBFormatString();
            if (!category.ISNullOrWhiteSpace())
            {
                filterBy = "Category : " + category;
            }
            query = category.ISNullOrWhiteSpace() ? "" : "Category = '" + category + "'";
            string subCategory = cmbSubCategory.Text;
            if (!subCategory.ISNullOrWhiteSpace())
            {
                filterBy += (filterBy.ISNullOrWhiteSpace()) ? " " : " ;  SubCategory : " + subCategory;
                query += (query.ISNullOrWhiteSpace() ? "" : " and ") + "SubCategory = '" + subCategory + "'";
            }
            string company = cmbCompany.Text;
            if (!company.ISNullOrWhiteSpace())
            {
                filterBy += (filterBy.ISNullOrWhiteSpace() ? " " : " ;  ") + "Company : " + company;
                query += (query.ISNullOrWhiteSpace() ? "" : " and ") + "Company = '" + company + "'";
            }

            string stockStatus = rbtnStockAll.Checked ? " " :
                                  (rbtnStockIn.Checked ? " Stock <> 'Out of Stock'" : " Stock = 'Out of Stock'");
            query += (stockStatus.ISNullOrWhiteSpace() ? "" : (query.ISNullOrWhiteSpace() ? " " : " and ") + stockStatus);
            filterBy += stockStatus.ISNullOrWhiteSpace() ? " " : (filterBy.ISNullOrWhiteSpace() ? " " : " ;  ") + stockStatus;

            string currentdate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            string expStatus = rbtnExpiryAll.Checked ? " " :
                                 (rbtnExpiryIn.Checked ? " ExpDate >= '" + currentdate + "'" : " ExpDate < '" + currentdate + "'");
            query += (expStatus.ISNullOrWhiteSpace() ? "" : (query.ISNullOrWhiteSpace() ? " " : " and ") + expStatus);
            filterBy += expStatus.ISNullOrWhiteSpace() ? " " : (filterBy.ISNullOrWhiteSpace() ? " " : " ;  ") + expStatus;

            string gstRate = cmbGstRate.Text.GetDBFormatString();
            if (!gstRate.ISNullOrWhiteSpace())
            {
                query += (query.ISNullOrWhiteSpace() ? "" : " and ") + "GSTRate = '" + gstRate + "'";
                filterBy += (filterBy.ISNullOrWhiteSpace() ? " " : " ;  ") + "GST : " + gstRate;
            }
            filterBy = filterBy.Replace("'", "");
            DataView dtv = new DataView(mdt);
            dtv.RowFilter = query;
            dgvStockList.DataSource = dtv;
            pnlFilter.Hide();
            lblfilterString.Text = filterBy;
            lblFilterClear.Location = new Point(lblfilterString.Location.X + lblfilterString.Width + 2, lblFilterClear.Location.Y);
            if (!filterBy.ISNullOrWhiteSpace())
            {
                lblFilterClear.Show();
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
        private void lblFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSerachByName.Clear();
            lblFilterClear.Hide();
            lblfilterString.Text = "";
            cmbCategory.SelectedIndex = -1;
            cmbSubCategory.SelectedIndex = -1;
            cmbCompany.SelectedIndex = -1;
            rbtnStockAll.Checked = true;
            rbtnExpiryAll.Checked = true;
            cmbGstRate.SelectedIndex = -1;

            DataView dtv = new DataView(mdt);
            dgvStockList.DataSource = dtv;
        }
        private void lblfilterString_Click(object sender, EventArgs e)
        {

        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Stock_List.xls";
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
                for (int index = 0; index <= dgvStockList.Columns.Count - 1; index++)
                {
                    if (dgvStockList.Columns[index].Visible)
                    {
                        mWorkSheet.Cells[1, xlColIndex++] = dgvStockList.Columns[index].HeaderText;
                    }
                }
                #endregion
                #region alginment
                //AUTOFILL And Border

                Excel.Range autofill = mWorkSheet.get_Range("A1", "P" + (dgvStockList.Rows.Count + 1));
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

                Excel.Range amountalignment = mWorkSheet.get_Range("G2", "G" + (dgvStockList.Rows.Count + 1));
                amountalignment.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                amountalignment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                #endregion
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvStockList.RowCount - 1; i++)
                {
                    int col = 1;
                    for (j = 0; j <= dgvStockList.Columns.Count - 1; j++)
                    {
                        if (dgvStockList.Columns[j].Visible)
                        {
                            DataGridViewCell cell = dgvStockList[j, i];
                            mWorkSheet.Cells[i + 2, col] = cell.Value;
                            if (dgvStockList.Columns[j].Name == "NetStock")
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

