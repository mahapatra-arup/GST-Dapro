using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace DAPRO.SalesFolder.Invoice
{
    public class ATFormate
    {
        private static string msg = "";
        private static DataTable mDt = new DataTable();
        private static object missing;

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
        }

        public static DataTable ATInvoiceGenerate(string startDate, string endDate)
        {
            InitialColumn();

            if (mDt.Rows.Count > 0)
            {
                mDt.Rows.Clear();
            }
            double totalTaxAmount = 0d, totalCeassAmount = 0d;

            mDt.Rows.Add("Summary For Advance Received (11B)");
            mDt.Rows.Add("", "", "Total Advance Received", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("Place Of Supply", "Rate", "Gross Advance Received", "Cess Amount");

            #region sINGLE
            string query = "SELECT AdvanceReceiptVoucher.BillingStateCode,Item.GSTRate,AdvanceReceiptVoucher.BillingState,SUM(TaxValue) AS TaxValue,SUM(CessAmount) AS CessAmount FROM AdvanceReceiptVoucher " +
            "INNER JOIN item ON Item.ID = AdvanceReceiptVoucher.ItemId " +
            "GROUP BY AdvanceReceiptVoucher.BillingStateCode,AdvanceReceiptVoucher.BillingState,Item.GSTRate " +
            "ORDER BY Item.GSTRate DESC";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double TaxAmount = 0d, CeassAmount = 0d, GSTRate = 0d;
                    double.TryParse(item["TaxValue"].ToString(), out TaxAmount);
                    double.TryParse(item["GSTRate"].ToString(), out GSTRate);
                    double.TryParse(item["CessAmount"].ToString(), out CeassAmount);


                    mDt.Rows.Add(item["BillingStateCode"] + "-" + item["BillingState"]
                    , GSTRate.ToString("0.00"), TaxAmount.ToString("0.00"), CeassAmount.ToString("0.00"));

                    //tOTAL cALCULATE
                    totalTaxAmount += TaxAmount;
                    totalCeassAmount += CeassAmount;
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            mDt.Rows[2][2] = totalTaxAmount.ToString("0.00");
            mDt.Rows[2][3] = totalCeassAmount.ToString("0.00");

            #endregion
            return mDt;
        }

        #region eXPORT
        public ATFormate()
        {

        }

        public static void ExportToExcel(string fileName, string startDate, string endDate)
        {
            ATInvoiceGenerate(startDate, endDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "AT";

            #region cotent
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
            oSheet.get_Range("A2", "D2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "D2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "D4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


            oSheet.get_Range("B1", "D" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

            oSheet.get_Range("A1", "D" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "D2").Font.Bold = true;
            oSheet.get_Range("A1", "D2").Font.Size = 12;
            //BorderLine
            oSheet.get_Range("A1", "D3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "D3");
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
        #endregion
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
    }
}
