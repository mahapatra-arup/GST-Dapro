using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;

namespace DAPRO.SalesFolder.Invoice
{
    public class HSNFormate
    {

        private static string msg = "";
        private static DataTable mDt = new DataTable();

        public static void InitialColumn()
        {
            if (mDt.Columns.Count > 0)
            {
                mDt.Columns.Clear();
            }
            mDt.Columns.Add("1", typeof(string));
            mDt.Columns.Add("2", typeof(string));
            mDt.Columns.Add("3", typeof(string));
            mDt.Columns.Add("4", typeof(string));
            mDt.Columns.Add("5", typeof(string));
            mDt.Columns.Add("6", typeof(string));
            mDt.Columns.Add("7", typeof(string));
            mDt.Columns.Add("8", typeof(string));
            mDt.Columns.Add("9", typeof(string));
            mDt.Columns.Add("10", typeof(string));
        }

        public static DataTable HSNInvoiceGenerate( string startDate, string EndDate)
        {
            InitialColumn();

            if (mDt.Rows.Count > 0)
            {
                mDt.Rows.Clear();
            }

            mDt.Rows.Add("Summary For HSN(12)");
            mDt.Rows.Add("No. of HSN", "", "", "", "Total Value", "Total Taxable Value", "Total Integrated Tax", "Total Central Tax", "Total State/UT Tax", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("HSN", "Description", "UQC", "Total Quantity", "Total Value", "Taxable Value",
            "Integrated Tax Amount", "Central Tax Amount", "State/UT Tax Amount", "Cess Amount");

            #region sINGLE
            string query = "SELECT  item.ItemName,item.ComodityCode,Unit.UnitShortName,Unit.UnitFullName,SUM(InvoiceDetails.Quantity) as Quantity, " +
            "sum(InvoiceDetails.Amount) as amount ,sum(InvoiceDetails.TaxAmount) as TaxAmount, sum(InvoiceDetails.IGSTAmount) as IGSTAmount, " +
            "sum(InvoiceDetails.CGSTAmount) as CGSTAmount,sum(InvoiceDetails.SGSTAmount) as SGSTAmount,sum(InvoiceDetails.CeassAmount) as CeassAmount " +
            "FROM  Invoice  " +
            "INNER JOIN InvoiceDetails on InvoiceDetails.InvoiceNo=Invoice.InvoiceNo  " +
            "INNER JOIN item ON item.ID = InvoiceDetails.ItemID  " +
            "INNER JOIN Unit ON Unit.ID = item.UnitId " +
            " where Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel' " +
            "group by item.ItemName,item.ComodityCode,Unit.UnitShortName,Unit.UnitFullName  order by item.ItemName,Unit.UnitShortName,Unit.UnitFullName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double TaxAmount = 0d, Amount = 0d, CeassAmount = 0d, cgst = 0d, igst = 0d, sgst = 0d, qty = 0d;
                    double.TryParse(item["Quantity"].ToString(), out qty);
                    double.TryParse(item["TaxAmount"].ToString(), out TaxAmount);
                    double.TryParse(item["Amount"].ToString(), out Amount);

                    double.TryParse(item["IGSTAmount"].ToString(), out igst);
                    double.TryParse(item["CGSTAmount"].ToString(), out cgst);
                    double.TryParse(item["SGSTAmount"].ToString(), out sgst);
                    double.TryParse(item["CeassAmount"].ToString(), out CeassAmount);


                    mDt.Rows.Add(item["ComodityCode"], item["ItemName"], item["UnitShortName"].ToString() + "-" + item["UnitFullName"].ToString(),
                    qty.ToString("0.00"), Amount.ToString("0.00"), TaxAmount.ToString("0.00"), igst.ToString("0.00"), cgst.ToString("0.00"),
                    sgst.ToString("0.00"), CeassAmount.ToString("0.00"));
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            query = "SELECT  count(Distinct InvoiceDetails.HSNCode) as CountHsn, sum(InvoiceDetails.Amount) as Totalamount    " +
            ",sum(InvoiceDetails.TaxAmount) as TotalTaxAmount, sum(InvoiceDetails.IGSTAmount) as TotalIGSTAmount,  " +
            "sum(InvoiceDetails.CGSTAmount) as TotalCGSTAmount,sum(InvoiceDetails.SGSTAmount) as TotalSGSTAmount,sum(InvoiceDetails.CeassAmount) as TotalCeassAmount  " +
            "FROM  Invoice   " +
            "INNER JOIN InvoiceDetails on InvoiceDetails.InvoiceNo=Invoice.InvoiceNo   " +
            "INNER JOIN item ON item.ID = InvoiceDetails.ItemID  " +
            "INNER JOIN Unit ON Unit.ID = item.UnitId  "+
            "Where Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel'";

            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt1.IsValidDataTable())
            {
                double TotalTaxAmount = 0d, TotalAmount = 0d, TotalCeassAmount = 0d, Totalcgst = 0d, Totaligst = 0d, Totalsgst = 0d;int counthsn=0;

                int.TryParse(dt1.Rows[0]["CountHsn"].ToString(), out counthsn);
                double.TryParse(dt1.Rows[0]["TotalTaxAmount"].ToString(), out TotalTaxAmount);
                double.TryParse(dt1.Rows[0]["TotalAmount"].ToString(), out TotalAmount);

                double.TryParse(dt1.Rows[0]["TotalIGSTAmount"].ToString(), out Totaligst);
                double.TryParse(dt1.Rows[0]["TotalCGSTAmount"].ToString(), out Totalcgst);
                double.TryParse(dt1.Rows[0]["TotalSGSTAmount"].ToString(), out Totalsgst);
                double.TryParse(dt1.Rows[0]["TotalCeassAmount"].ToString(), out TotalCeassAmount);

                mDt.Rows[2][0] = counthsn.ToString();
                mDt.Rows[2][4] = TotalAmount.ToString("0.00");
                mDt.Rows[2][5] = TotalTaxAmount.ToString("0.00");
                mDt.Rows[2][6] = Totaligst.ToString("0.00");
                mDt.Rows[2][7] = Totalcgst.ToString("0.00");
                mDt.Rows[2][8] = Totalsgst.ToString("0.00");
                mDt.Rows[2][9] = TotalCeassAmount.ToString("0.00");
            }
            #endregion
            return mDt;
        }


        #region eXPORT
        public HSNFormate()
        {
           
        }

        public static void ExportToExcel(string fileName, string startDate, string EndDate)
        {
           HSNInvoiceGenerate(startDate, EndDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath,0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            
            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "HSN";

            #region Content
            int i = 0;
                int j = 0;

                for (i = 0; i <= mDt.Rows.Count - 1; i++)
                {
                    for (j = 0; j <= mDt.Columns.Count - 1; j++)
                    {
                        oSheet.Cells[i + 1, j + 1] = mDt.Rows[i][j];// cell.Value;
                    }
                }
                oXL.StandardFont = "Cambria";

                oSheet.get_Range("A1", "A1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
                oSheet.get_Range("A2", "J2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
                oSheet.get_Range("A1", "J2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                oSheet.get_Range("A4", "J4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


                oSheet.get_Range("D1", "J" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

                oSheet.get_Range("A1", "J" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
                oSheet.get_Range("A1", "J2").Font.Bold = true;
                oSheet.get_Range("A1", "J2").Font.Size = 12;
                oSheet.get_Range("A1", "C" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", "C" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //BorderLine
                oSheet.get_Range("A1", "J3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                oRng = oSheet.get_Range("A1", "J3");
                oRng.EntireColumn.AutoFit();
            #endregion

            oSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            

            oSheet.Select();
            xlWorkBook.Save();
            xlWorkBook.Close();

            releaseObject(oSheet);
            releaseObject(worksheets);
            releaseObject(xlWorkBook);
            releaseObject(oXL);
        }

        private static void releaseObject(object obj)

        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }

            catch (Exception ex)

            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }

            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
