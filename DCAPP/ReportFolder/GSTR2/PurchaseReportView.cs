using System;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO
{
    public partial class PurchaseReportView : Form
    {
        private string msg = "";
        private int MethodExecutioncount = 0;
        DataTable _Dt = new DataTable();
        private DateTime _FirstDate, _LastDate;

        private void dgvView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgvView.ClearSelection();
            if (_Dt.Rows.Count - 1 == e.RowIndex)
            {
                dgvView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Bisque;
                Rectangle hcRct = dgvView.GetCellDisplayRectangle(1, e.RowIndex, true);

                int multiRowWidth = 0;//dgvView.Columns[e.RowIndex].Width;
                foreach (DataGridViewColumn column in dgvView.Columns)
                {
                    if (1 <= column.Index && 4 >= column.Index)
                    {
                        multiRowWidth += column.Width;
                    }
                }

                //Rectengle x,y location and Hight and Width Set;
                Rectangle headRct = new Rectangle(hcRct.Left - 3, hcRct.Top + 1, multiRowWidth + 7, dgvView.Rows[dgvView.Rows.Count - 1].Height - 2);

                //Rectengle clor;
                e.Graphics.FillRectangle(new SolidBrush(dgvView.Rows[e.RowIndex].DefaultCellStyle.BackColor), headRct);
                ////border draw
                // e.Graphics.DrawRectangle(Pens.Red, headRct);
                // e.Graphics.DrawString(array[i], dgvitemList.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, hcRct.Left + (headRct.Width / 2) - 20, headerTop);
            }
        }

        public PurchaseReportView()
        {
            InitializeComponent();
            Inatialize();
            cmbMonth.Text = DateTime.Now.ToString("MMM");
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _MonthDigit = DateTime.ParseExact(cmbMonth.Text, "MMM", CultureInfo.InvariantCulture).Month;
            DateTime now = DateTime.Now;
            _FirstDate = new DateTime(now.Year, _MonthDigit, 1);
            _LastDate = _FirstDate.AddMonths(1).AddDays(-1);
            PurchaseDetails(_FirstDate.ToString("dd-MMM-yyy"), _LastDate.ToString("dd-MMM-yyy"));
        }

        private void Inatialize()
        {
            _Dt.Columns.Add("SL\nNO", typeof(string));
            _Dt.Columns.Add("DATE", typeof(string));
            _Dt.Columns.Add("PARTY NAME", typeof(string));
            _Dt.Columns.Add("GSTIN", typeof(string));
            _Dt.Columns.Add("BILL NO", typeof(string));
            _Dt.Columns.Add("TAXABLE VALUE", typeof(string));
            _Dt.Columns.Add("CGST\n2.5%", typeof(string));
            _Dt.Columns.Add("SGST\n2.5%", typeof(string));
            _Dt.Columns.Add("IGST\n5%", typeof(string));
            _Dt.Columns.Add("CGST\n6%", typeof(string));
            _Dt.Columns.Add("SGST\n6%", typeof(string));
            _Dt.Columns.Add("IGST\n12%", typeof(string));
            _Dt.Columns.Add("CGST\n9%", typeof(string));
            _Dt.Columns.Add("SGST\n9%", typeof(string));
            _Dt.Columns.Add("IGST\n18%", typeof(string));
            _Dt.Columns.Add("CGST\n14%", typeof(string));
            _Dt.Columns.Add("SGST\n14%", typeof(string));
            _Dt.Columns.Add("IGST\n28%", typeof(string));
            _Dt.Columns.Add("CESS", typeof(string));
            _Dt.Columns.Add("TOTAL GST", typeof(string));
            _Dt.Columns.Add("TOTAL", typeof(string));

            dgvView.DataSource = _Dt;
            //dgvView.Columns["Lagdger Name"].Frozen = true;
            dgvView.Columns["TAXABLE VALUE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["TOTAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["TOTAL GST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["CGST\n14%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["SGST\n14%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["SGST\n9%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["CGST\n9%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["SGST\n6%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["CGST\n6%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["SGST\n2.5%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["CGST\n2.5%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvView.Columns["IGST\n5%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["IGST\n12%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["IGST\n18%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["IGST\n28%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns["CESS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            for (int i = 0; i < dgvView.Columns.Count; i++)
            {
                dgvView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = " GST2.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                ExportToExcel(sfd.FileName); // Here dataGridview1 is your grid view name 
                Cursor = Cursors.Default;
            }
        }

        private void PurchaseDetails(string startDate, string endDate)
        {
            #region VARIABLE
            double mAllTotal = 0d, mAllGstAll = 0d, mAllTaxibleValue = 0d,
            //**********CGST,SGST**************
            mAllCGST_2_5 = 0d, mAllSGST_2_5 = 0d,
            mAllCGST_6 = 0d, mAllSGST_6 = 0d,
            mAllCGST_9 = 0d, mAllSGST_9 = 0d,
            mAllCGST_14 = 0d, mAllSGST_14 = 0d;
            //***************IGST**************
            double mAllIGST_5 = 0d, mAllIGST_12 = 0d,
            mAllIGST_18 = 0d, mAllIGST_28 = 0d;
            //***************CESS**************
            double mAllCESS = 0d;
            #endregion

            DataTable dt = new DataTable();
            _Dt.Rows.Clear();
            int i = 0;

            #region QUERY
            string Query = "select  PurchaseBill.LedgerId,LadgerMain.LadgerName,LadgerMain.GSTIN,Convert(varchar(11),InvoiceDate,106) as InvoiceDate,InvoiceNo,Sum(PurchaseBillDetails.TaxAmount) as TaxableAmount  " +
            //% WISE TOTAL CGST,SGST
            ", (a.CGSTAmount_2_5) as CGSTAmount_2_5, (a.SGSTAmount_2_5) as SGSTAmount_2_5  " +
            ", (b.CGSTAmount_6) as CGSTAmount_6, (b.SGSTAmount_6) as SGSTAmount_6  " +
            ", (c.CGSTAmount_9) as CGSTAmount_9, (c.SGSTAmount_9) as SGSTAmount_9  " +
            ", (d.CGSTAmount_14) as CGSTAmount_14, (d.SGSTAmount_14) as SGSTAmount_14,  " +

            //% WISE TOTAL IGST
            "(I_5.IGSTAmount_5) as IGSTAmount_5, (I_12.IGSTAmount_12) as IGSTAmount_12, (I_18.IGSTAmount_18) as IGSTAmount_18, " +
            "(I_28.IGSTAmount_28) as IGSTAmount_28, " +

            //% WISE TOTAL CESS
            "(Cess.CessAmount) as CessAmount, " +

            //TOTAL GST(CGST+SGST+IGST )   
            "(ISNULL((CGSTAmount_2_5), 0) + ISNULL((SGSTAmount_2_5), 0)  " +
            "+ ISNULL((CGSTAmount_6), 0) + ISNULL((SGSTAmount_6), 0) + ISNULL((CGSTAmount_9), 0)  " +
            "+ ISNULL((SGSTAmount_9), 0) + ISNULL((CGSTAmount_14), 0) + ISNULL((SGSTAmount_14), 0)) as TotalGST,  " +
            //
            "ISNULL((IGSTAmount_5), 0),ISNULL((IGSTAmount_12), 0),ISNULL((IGSTAmount_18), 0),ISNULL((IGSTAmount_28), 0), " +

            //TOTAL
            " ISNULL(SUM(PurchaseBillDetails.Total), 0)  as Total  " +

            "from PurchaseBill  " +
            "inner join PurchaseBillDetails on PurchaseBill.BillID = PurchaseBillDetails.Billid  " +
            "inner join LadgerMain on LadgerMain.LadgerID = PurchaseBill.LedgerId  " +

            //jOINING RESULT FOR CGST,SGST
            "left join (select Billid, Sum(CGSTAmount) as CGSTAmount_2_5, Sum(SGSTAmount) as SGSTAmount_2_5 from PurchaseBillDetails where CGSTRate = 2.5 and SGSTRate = 2.5 group by Billid) as a on a.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(CGSTAmount) as CGSTAmount_6, Sum(SGSTAmount) as SGSTAmount_6 from PurchaseBillDetails where CGSTRate = 6 and SGSTRate = 6 group by Billid) as b on b.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(CGSTAmount) as CGSTAmount_9, Sum(SGSTAmount) as SGSTAmount_9 from PurchaseBillDetails where CGSTRate = 9 and SGSTRate = 9 group by Billid) as c on c.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(CGSTAmount) as CGSTAmount_14, Sum(SGSTAmount) as SGSTAmount_14 from PurchaseBillDetails where CGSTRate = 14 and SGSTRate = 14 group by Billid) as d on d.Billid = PurchaseBill.BillID  " +

            //JOINING RESULT FOR IGST
            "left join(select Billid, Sum(IGSTAmount) as IGSTAmount_5 from PurchaseBillDetails where IGSTRate = 5 group by Billid) as I_5 on I_5.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(IGSTAmount) as IGSTAmount_12 from PurchaseBillDetails where IGSTRate = 12 group by Billid) as I_12 on I_12.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(IGSTAmount) as IGSTAmount_18 from PurchaseBillDetails where IGSTRate = 18 group by Billid) as I_18 on I_18.Billid = PurchaseBill.BillID  " +
            "left join (select Billid, Sum(IGSTAmount) as IGSTAmount_28 from PurchaseBillDetails where IGSTRate = 28 group by Billid) as I_28 on I_28.Billid = PurchaseBill.BillID  " +

            //JOINING RESULT FOR CESS
            "left join(select Billid, Sum(CeassAmount) as CessAmount from PurchaseBillDetails   group by Billid) as Cess on Cess.Billid = PurchaseBill.BillID " +

            //GROUP BY CLOUSE
            "Where InvoiceDate BETWEEN '" + startDate + "' AND '" + endDate + "' and Status<>'Cancel' " +
            "Group by PurchaseBill.LedgerId,LadgerMain.LadgerName,LadgerMain.GSTIN,InvoiceDate,InvoiceNo,  " +
            "CGSTAmount_2_5, SGSTAmount_2_5, " +
            "CGSTAmount_6, SGSTAmount_6, CGSTAmount_9, " +
            "SGSTAmount_9, CGSTAmount_14, SGSTAmount_14, " +
            "IGSTAmount_5, IGSTAmount_12,  " +
            "IGSTAmount_18,  IGSTAmount_28,  " +
            "CessAmount " +
            " order by InvoiceDate,InvoiceNo";
            #endregion

            dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                //***************maximum length of Progress Ber*******************
                progressBar1.Maximum = dt.Rows.Count;
                foreach (DataRow item in dt.Rows)
                {
                    //**************Increase  ProgressBer Value********************
                    progressBar1.Value = ++MethodExecutioncount;

                    #region LOCAL vARIABLE
                    //***************CGST,SGST**************
                    double Total = 0d, GstAll = 0d, TaxibleValue = 0d,
                    CGST_2_5 = 0d, SGST_2_5 = 0d,
                    CGST_6 = 0d, SGST_6 = 0d,
                    CGST_9 = 0d, SGST_9 = 0d,
                    CGST_14 = 0d, SGST_14 = 0d;
                    //***************IGST**************
                    double IGST_5 = 0d, IGST_12 = 0d,
                    IGST_18 = 0d, IGST_28 = 0d;
                    //***************CESS**************
                    double CESS = 0d;
                    #endregion

                    #region VALUE INITIALIZE FROM DATATABLE
                    //***************TOTAL**************
                    double.TryParse(item["Total"].ToString(), out Total);
                    double.TryParse(item["TotalGst"].ToString(), out GstAll);
                    double.TryParse(item["TaxableAmount"].ToString(), out TaxibleValue);

                    //***************CGST,SGST**************
                    double.TryParse(item["CGSTAmount_2_5"].ToString(), out CGST_2_5);
                    double.TryParse(item["SGSTAmount_2_5"].ToString(), out SGST_2_5);

                    double.TryParse(item["CGSTAmount_6"].ToString(), out CGST_6);
                    double.TryParse(item["SGSTAmount_6"].ToString(), out SGST_6);

                    double.TryParse(item["CGSTAmount_9"].ToString(), out CGST_9);
                    double.TryParse(item["SGSTAmount_9"].ToString(), out SGST_9);

                    double.TryParse(item["CGSTAmount_14"].ToString(), out CGST_14);
                    double.TryParse(item["SGSTAmount_14"].ToString(), out SGST_14);

                    //***************IGST**************
                    double.TryParse(item["IGSTAmount_5"].ToString(), out IGST_5);
                    double.TryParse(item["IGSTAmount_12"].ToString(), out IGST_12);
                    double.TryParse(item["IGSTAmount_18"].ToString(), out IGST_18);
                    double.TryParse(item["IGSTAmount_28"].ToString(), out IGST_28);

                    //***************CESS**************
                    double.TryParse(item["CESSAMOUNT"].ToString(), out CESS);
                    #endregion

                    #region DATATABLE ROW ADD
                    //*********************************DATATABLE ROW ADD************************
                    _Dt.Rows.Add(++i, item["InvoiceDate"], item["LadgerName"], item["GSTIN"], item["InvoiceNo"],
                    TaxibleValue, CGST_2_5, SGST_2_5, IGST_5, CGST_6, SGST_6, IGST_12, CGST_9, SGST_9, IGST_18, CGST_14, SGST_14, IGST_28, CESS, GstAll, Total);
                    #endregion

                    #region tOTAL CALCULATE
                    //**********Total Calculate*********************
                    mAllTotal += Total;
                    mAllGstAll += GstAll;
                    mAllTaxibleValue += TaxibleValue;
                    //**CGST,SGST**
                    mAllCGST_2_5 += CGST_2_5;
                    mAllSGST_2_5 += SGST_2_5;
                    mAllCGST_6 += CGST_6;
                    mAllSGST_6 += SGST_6;
                    mAllCGST_9 += CGST_9;
                    mAllSGST_9 += SGST_9;
                    mAllCGST_14 += CGST_14;
                    mAllSGST_14 += SGST_14;
                    //****IGST****
                    mAllIGST_5 += IGST_5;
                    mAllIGST_12 += IGST_12;
                    mAllIGST_18 += IGST_18;
                    mAllIGST_28 += IGST_28;
                    //****CESS****
                    mAllCESS += CESS;
                    #endregion
                }
            }
            #region TOTAL VALUE ADD IN LAST POSITION ROW
            //*******************"ALL TOTAL" ROW ADD IN LAST POSITION ****************
            _Dt.Rows.Add();
            _Dt.Rows[_Dt.Rows.Count - 1][0] = "TOTAL";
            _Dt.Rows[_Dt.Rows.Count - 1][5] = mAllTaxibleValue.ToString("0.00");

            _Dt.Rows[_Dt.Rows.Count - 1][6] = mAllCGST_2_5.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][7] = mAllSGST_2_5.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][8] = mAllIGST_5.ToString("0.00");

            _Dt.Rows[_Dt.Rows.Count - 1][9] = mAllCGST_6.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][10] = mAllSGST_6.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][11] = mAllIGST_12.ToString("0.00");

            _Dt.Rows[_Dt.Rows.Count - 1][12] = mAllCGST_9.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][13] = mAllSGST_9.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][14] = mAllIGST_18.ToString("0.00");


            _Dt.Rows[_Dt.Rows.Count - 1][15] = mAllCGST_14.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][16] = mAllSGST_14.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][17] = mAllIGST_28.ToString("0.00");

            _Dt.Rows[_Dt.Rows.Count - 1][18] = mAllCESS.ToString("0.00");

            _Dt.Rows[_Dt.Rows.Count - 1][19] = mAllGstAll.ToString("0.00");
            _Dt.Rows[_Dt.Rows.Count - 1][20] = mAllTotal.ToString("0.00");
            #endregion

            //*******************PROGRESSBER ****************
            progressBar1.Value = 0;
            MethodExecutioncount = 0;

        }

        private void ExportToExcel(string fileName)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            object misValue = System.Reflection.Missing.Value;
            try
            {

                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = false;
                oXL.DisplayAlerts = false;
                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                for (int index = 0; index <= dgvView.Columns.Count - 1; index++)
                {
                    oSheet.Cells[1, index + 1] = dgvView.Columns[index].HeaderText;
                }

                //EXCEL SHEET DESIGN
                Excel.Range oRng0 = oSheet.get_Range("A2", "U1");
                oRng0.Font.Bold = true;
                oRng0.Font.Size = 11;
                oRng0.Font.FontStyle = "Calibri";
                oRng0.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRng0.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oRng0.WrapText = true;
                oRng0.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                oSheet.get_Range("F1", "U" + (dgvView.Rows.Count + 1)).NumberFormat = "#,###,###0.00";
                //header color
                oSheet.get_Range("A1", "U1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Azure);
                //Font size
                oSheet.get_Range("A2", "U" + (dgvView.Rows.Count)).Font.Size = 9;
                //FOOTER color
                oSheet.get_Range("A" + (dgvView.Rows.Count + 1), "P" + (dgvView.Rows.Count + 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);
                //////Marge cell/////
                Excel.Range rng1 = oSheet.get_Range("A" + (dgvView.Rows.Count + 1), "E" + (dgvView.Rows.Count + 1));
                rng1.Merge(Missing.Value);
                //Auto fit
                //oRng = oSheet.get_Range("A2", "P" + (dgvView.Rows.Count + 1));
                //oRng.EntireRow.AutoFit();
                //oRng.EntireColumn.AutoFit();
                int i = 0;
                int j = 0;

                for (i = 0; i <= dgvView.RowCount - 1; i++)
                {
                    for (j = 0; j <= dgvView.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dgvView[j, i];
                        oSheet.Cells[i + 2, j + 1] = cell.Value;
                    }
                }
                oWB.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //oWB.Close(true, misValue, misValue);
                //oXL.Quit();

                oXL.Visible = true;
                oXL.UserControl = true;
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
