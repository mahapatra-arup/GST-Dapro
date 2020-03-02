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
    public class B2CSFormate
    {
        private static string msg = "";
        private static DataTable mDt = new DataTable();

        private static void InitialColumn()
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
        }

        public static DataTable B2CSInvoiceGenerate(string startDate, string EndDate)
        {
            InitialColumn();
            if (mDt.Rows.Count > 0)
            {
                mDt.Rows.Clear();
            }
            mDt.Rows.Add("Summary For B2CS(7)");
            mDt.Rows.Add("", "", "", "Total Taxable Value", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("Type", "Place Of Supply", "Rate", "Taxable Value", "Cess Amount", "E-Commerce GSTIN");

            #region sINGLE

            string query = "DECLARE @STATECODE NVARCHAR(200) " +
            "SET @STATECODE=(SELECT TOP(1)StateID FROM OrganizationDetails) " +
            "SELECT   Invoice.BillingState,Invoice.BillingStateCode , item.GSTRate  , SUM(InvoiceDetails.CeassAmount) AS CeassAmount, SUM(InvoiceDetails.TaxAmount) AS TaxAmount " +
            "FROM         Invoice INNER JOIN InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo INNER JOIN  " +
            "item ON item.ID = InvoiceDetails.ItemID WHERE   (ISNULL(Invoice.BillingGSTNO, '') ='')  AND (( INVOICE.BillingStateCode=@STATECODE) OR ( INVOICE.BillingStateCode!=@STATECODE  and TotalAmount<=250000)) " +
            " and  Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel' GROUP BY BillingState,Invoice.BillingStateCode,  GSTRate  " +
            "ORDER BY Invoice.BillingState,Invoice.BillingStateCode ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double TaxAmount = 0d, CeassAmount = 0d, GSTRate = 0d;
                    double.TryParse(item["TaxAmount"].ToString(), out TaxAmount);
                    double.TryParse(item["GSTRate"].ToString(), out GSTRate);
                    double.TryParse(item["CeassAmount"].ToString(), out CeassAmount);


                    mDt.Rows.Add("OE", item["BillingStateCode"] + "-" + item["BillingState"], GSTRate.ToString("0.00"), TaxAmount.ToString("0.00"), CeassAmount.ToString("0.00"), "");
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            query = "DECLARE @STATECODE NVARCHAR(200) " +
            "SET @STATECODE=(SELECT TOP(1)StateID FROM OrganizationDetails) " +
            "SELECT  sum( TaxAmount) as TotalTaxAmount,sum(InvoiceDetails.CeassAmount) as TotalCeassAmount " +
            "FROM         Invoice INNER JOIN  " +
            "InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo  " +
            "WHERE    (ISNULL(Invoice.BillingGSTNO, '') ='') AND ((INVOICE.BillingStateCode=@STATECODE) or (INVOICE.BillingStateCode!=@STATECODE  and TotalAmount<=250000)) " +
            "and  Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel'";

            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt1.IsValidDataTable())
            {
                double totalTaxAmount = 0d, totalCeassAmount = 0d;

                double.TryParse(dt1.Rows[0]["TotalTaxAmount"].ToString(), out totalTaxAmount);
                double.TryParse(dt1.Rows[0]["TotalCeassAmount"].ToString(), out totalCeassAmount);

                mDt.Rows[2][3] = totalTaxAmount.ToString("0.00");
                mDt.Rows[2][4] = totalCeassAmount.ToString("0.00");
            }

            return mDt;
            #endregion
        }


        #region eXPORT
        public B2CSFormate()
        {

        }

        public static void ExportToExcel(string fileName, string startDate, string EndDate)
        {
            B2CSInvoiceGenerate(startDate, EndDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "B2CS";

            #region Contant
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
            oSheet.get_Range("A2", "F2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "F2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "F4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);

            oSheet.get_Range("C1", "E" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

            oSheet.get_Range("A1", "F" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "F2").Font.Bold = true;
            oSheet.get_Range("A1", "F2").Font.Size = 12;
            oSheet.get_Range("A1", "B" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("A1", "B" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //BorderLine
            oSheet.get_Range("A1", "F3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "F3");
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

