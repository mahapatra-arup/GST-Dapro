using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DAPRO.SalesFolder.Invoice
{
    public class B2CLFormate
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
            mDt.Columns.Add("7", typeof(string));
            mDt.Columns.Add("8", typeof(string));
        }

        public static DataTable B2CLInvoiceGenerate( string startDate,string EndDate)
        {
            InitialColumn();
            if (mDt.Rows.Count > 0)
            {
                mDt.Rows.Clear();
            }
            mDt.Rows.Add("Summary For B2CL(5)");
            mDt.Rows.Add("No. of Invoices", "", "Total Invoice Value", "", "", "Total Taxable Value", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("Invoice Number", "Invoice date", "Invoice Value", "Place Of Supply", "Rate", "Taxable Value", "Cess Amount", "E-Commerce GSTIN");

            //Condition
            string condition = "(ISNULL(Invoice.BillingGSTNO, '') ='') and Invoice.TotalAmount>250000 and  Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel' ";
            //
            #region sINGLE
            string query = "SELECT     Invoice.InvoiceNo, CONVERT(varchar(11), Invoice.InvoiceDate, 106) AS InvoiceDate,  Invoice.BillingState,Invoice.BillingStateCode, " +
            "item.GSTRate, SUM(InvoiceDetails.CeassAmount) AS CeassAmount, " +
            "SUM(Invoice.TotalInvoiceAmount) AS TotalAmount, SUM(InvoiceDetails.TaxAmount) AS TaxAmount " +
            "FROM         Invoice INNER JOIN " +
            "InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo INNER JOIN " +
            "item ON item.ID = InvoiceDetails.ItemID " +
            "WHERE   "+condition+"  " +
            "GROUP BY Invoice.InvoiceNo,Invoice.InvoiceDate, Invoice.BillingState,Invoice.BillingStateCode,  item.GSTRate  " +
            "ORDER BY InvoiceDate, Invoice.InvoiceNo";
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


                    mDt.Rows.Add( item["InvoiceNo"], item["InvoiceDate"], TotalAmount.ToString("0.00"), item["BillingStateCode"]+"-"+ item["BillingState"], 
                     GSTRate.ToString("0.00"), TaxAmount.ToString("0.00"), CeassAmount.ToString("0.00"),"");
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            string totamount = "(select sum(Inv.TotalInvoiceAmount)  from (select  InvoiceNo,sum(TotalInvoiceAmount) as  TotalInvoiceAmount from  Invoice where " + condition + "  group by InvoiceNo) as Inv) as TotalAmount";

            query = "SELECT  COUNT(Distinct Invoice.InvoiceNo) as TotalInvoiceNo, " +
            ""+totamount+",sum( TaxAmount) as TotalTaxAmount,sum(InvoiceDetails.CeassAmount) as TotalCeassAmount " +
            "FROM         Invoice INNER JOIN  " +
            "InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo  " +
            "WHERE  "+ condition + "   ";

            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt1.IsValidDataTable())
            {
                double totalTaxAmount = 0d, totalTotalAmount = 0d, totalCeassAmount = 0d;

                mDt.Rows[2][0] = dt1.Rows[0]["TotalInvoiceNo"];

                double.TryParse(dt1.Rows[0]["TotalTaxAmount"].ToString(), out totalTaxAmount);
                double.TryParse(dt1.Rows[0]["TotalAmount"].ToString(), out totalTotalAmount);
                double.TryParse(dt1.Rows[0]["TotalCeassAmount"].ToString(), out totalCeassAmount);

                mDt.Rows[2][2] = totalTotalAmount.ToString("0.00");
                mDt.Rows[2][5] = totalTaxAmount.ToString("0.00");

                mDt.Rows[2][6] = totalCeassAmount.ToString("0.00");
            }

            return mDt;
            #endregion
        }


        #region eXPORT
        public B2CLFormate()
        {
        }

        public static void ExportToExcel(string fileName, string startDate, string EndDate)
        {
            B2CLInvoiceGenerate(startDate, EndDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "B2CL";
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
            oSheet.get_Range("A2", "H2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "H2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "H4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


            
            oSheet.get_Range("C1", "C" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";
            oSheet.get_Range("E1", "G" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";


            oSheet.get_Range("A1", "H" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "H2").Font.Bold = true;
            oSheet.get_Range("A1", "H2").Font.Size = 12;
            oSheet.get_Range("A1", "B" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("A1", "B" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            
            //BorderLine
            oSheet.get_Range("A1", "H3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "H3");
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
