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
    public class B2BFormate
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
            mDt.Columns.Add("6", typeof(string));
            mDt.Columns.Add("7", typeof(string));
            mDt.Columns.Add("8", typeof(string));
            mDt.Columns.Add("9", typeof(string));
            mDt.Columns.Add("10", typeof(string));
            mDt.Columns.Add("11", typeof(string));
        }

        public static DataTable B2BInvoiceGenerate(string startDate, string endDate)
        {
            InitialColumn();

            mDt.Rows.Clear();

            mDt.Rows.Add("Summary For B2B(4)");
            mDt.Rows.Add("No. of Recipients", "No. of Invoices", "", "Total Invoice Value", "", "", "", "", "", "Total Taxable Value", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("GSTIN/UIN of Recipient", "Invoice Number", "Invoice date", "Invoice Value", "Place Of Supply", "Reverse Charge",
            "Invoice Type", "E-Commerce GSTIN", "Rate", "Taxable Value", "Cess Amount");

            //Condition
            string condition = " where (ISNULL(BillingGSTNO, '') <> '') and  InvoiceDate between '" + startDate + "' and '" + endDate + "' and Status<>'Cancel' ";
            //
            #region sINGLE
            string query =
             /////For Total Invoice amount and invoice other details purpose
             "select Tab2.*,Tab1.CeassAmount,Tab1.GSTRate,Tab1.TaxAmount from  " +
            "(select Sum(TotalInvoiceAmount) as TotalAmount,InvoiceNo,CONVERT(varchar(11), InvoiceDate, 106) AS InvoiceDate,  " +
            "Status,BillingGSTNO, BillingState,BillingStateCode, InvoiceType from Invoice   " +
             condition + "group by  InvoiceNo,InvoiceDate,Status,BillingGSTNO, BillingState,BillingStateCode, InvoiceType) as Tab2 " +
            /////For  InvoiceDetails 
            "inner join (SELECT   InvoiceDetails.InvoiceNo, item.GSTRate, SUM(InvoiceDetails.CeassAmount) AS CeassAmount,  " +
            "SUM(InvoiceDetails.TaxAmount) AS TaxAmount FROM   InvoiceDetails INNER JOIN item ON item.ID = InvoiceDetails.ItemID    " +
            " GROUP BY item.GSTRate, InvoiceDetails.InvoiceNo ) as Tab1  " +
            "   on Tab2.InvoiceNo=Tab1.InvoiceNo order by Tab2.InvoiceDate,Tab2.InvoiceNo";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double TaxAmount = 0d, TotalAmount = 0d, CeassAmount = 0d, GSTRate = 0d;
                    double.TryParse(item["TaxAmount"].ToString(), out TaxAmount);
                    double.TryParse(item["TotalAmount"].ToString(), out TotalAmount);
                    double.TryParse(item["GSTRate"].ToString(), out GSTRate);
                    double.TryParse(item["CeassAmount"].ToString(), out CeassAmount);


                    mDt.Rows.Add(item["BillingGSTNO"], item["InvoiceNo"], item["InvoiceDate"], TotalAmount.ToString("0.00"), item["BillingStateCode"] + "-" + item["BillingState"], "N", item["InvoiceType"], ""
                    , GSTRate.ToString("0.00"), TaxAmount.ToString("0.00"), CeassAmount.ToString("0.00"));
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            condition = "(ISNULL(Invoice.BillingGSTNO, '') <> '') and  Invoice.InvoiceDate between '" + startDate + "' and '" + endDate + "' and Invoice.Status<>'Cancel' ";

            string totamount = "(select sum(Inv.TotalInvoiceAmount)  from (select  InvoiceNo,sum(TotalInvoiceAmount) as  TotalInvoiceAmount from  Invoice where " + condition + "  group by InvoiceNo) as Inv) as TotalAmount";

            query = "SELECT  COUNT(Distinct Invoice.InvoiceNo) as TotalInvoiceNo,COUNT(Distinct Invoice.BillingGSTNO) as TotalBillingGSTNO, " +
            "" + totamount + " ,sum( TaxAmount) as TotalTaxAmount,sum(InvoiceDetails.CeassAmount) as TotalCeassAmount " +
            "FROM         Invoice INNER JOIN " +
            "InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo " +
            "WHERE   " + condition + "";

            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt1.IsValidDataTable())
            {
                double totalTaxAmount = 0d, totalTotalAmount = 0d, totalCeassAmount = 0d;

                mDt.Rows[2][0] = dt1.Rows[0]["TotalBillingGSTNO"];
                mDt.Rows[2][1] = dt1.Rows[0]["TotalInvoiceNo"];

                double.TryParse(dt1.Rows[0]["TotalAmount"].ToString(), out totalTotalAmount);

                double.TryParse(dt1.Rows[0]["TotalTaxAmount"].ToString(), out totalTaxAmount);
                double.TryParse(dt1.Rows[0]["TotalCeassAmount"].ToString(), out totalCeassAmount);

                mDt.Rows[2][3] = totalTotalAmount.ToString("0.00");
                mDt.Rows[2][9] = totalTaxAmount.ToString("0.00");

                mDt.Rows[2][10] = totalCeassAmount.ToString("0.00");
            }


            #endregion
            return mDt;
        }


        #region eXPORT
        public B2BFormate()
        {

        }

        public static void ExportToExcel(string fileName, string startDate, string endDate)
        {
            B2BInvoiceGenerate(startDate, endDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "B2B";

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
            oSheet.get_Range("A2", "K2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "K2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "K4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


            oSheet.get_Range("D1", "D" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";
            oSheet.get_Range("I1", "I" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";
            oSheet.get_Range("J1", "J" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";
            oSheet.get_Range("K1", "K" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

            oSheet.get_Range("A1", "K" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "K2").Font.Bold = true;
            oSheet.get_Range("A1", "K2").Font.Size = 12;
            oSheet.get_Range("A1", "C" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("A1", "C" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //BorderLine
            oSheet.get_Range("A1", "K3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "K3");
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
