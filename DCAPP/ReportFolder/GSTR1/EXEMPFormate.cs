using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace DAPRO.SalesFolder.Invoice
{
    public class EXEMPFormate
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
        }

        public static DataTable EXEMInvoiceGenerate( string startDate, string EndDate)
        {
            double TotalnillAmount = 0d, TotalExamptedAmount = 0d, TotalnonGstAmount = 0d;

            InitialColumn();

            if (mDt.Rows.Count > 0)
            {
                mDt.Rows.Clear();
            }
            mDt.Rows.Add("Summary For Nil rated, exempted and \n non GST outward supplies (8)");
            mDt.Rows.Add("", "Total Nil Rated Supplies", "Total Exempted Supplies", "Total Non-GST Supplies");
            mDt.Rows.Add();
            mDt.Rows.Add("Description", "Nil Rated Supplies", "Exempted (other than nil \n rated/non GST supply )", "Non-GST supplies");

            #region sINGLE
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(QueryString(startDate, EndDate), out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double nillAmount = 0d, ExamptedAmount = 0d, nonGstAmount = 0d;

                    double.TryParse(item["NIL"].ToString(), out nillAmount);
                    double.TryParse(item["Exampted"].ToString(), out ExamptedAmount);
                    double.TryParse(item["NonGST"].ToString(), out nonGstAmount);
                    //Total
                    TotalnillAmount += nillAmount;
                    TotalExamptedAmount += ExamptedAmount;
                    TotalnonGstAmount += nonGstAmount;

                    mDt.Rows.Add(item["Description"], nillAmount.ToString("0.00"), ExamptedAmount.ToString("0.00"), nonGstAmount.ToString("0.00"));
                }
            }
            #endregion

            #region TOTAL
            mDt.Rows[2][1] = TotalnillAmount.ToString("0.00");
            mDt.Rows[2][2] = TotalExamptedAmount.ToString("0.00");
            mDt.Rows[2][3] = TotalnonGstAmount.ToString("0.00");
            #endregion

            return mDt;

        }

        #region eXPORT
        public EXEMPFormate()
        {
        }

        public static void ExportToExcel(string fileName,string startDate, string EndDate)
        {
            EXEMInvoiceGenerate(startDate, EndDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "EXEM";

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
                oSheet.get_Range("A2", "D2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
                oSheet.get_Range("A1", "D2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                oSheet.get_Range("A4", "D4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


                oSheet.get_Range("B1", "D" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

                oSheet.get_Range("A1", "D" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
                oSheet.get_Range("A1", "D2").Font.Bold = true;
                oSheet.get_Range("A1", "D2").Font.Size = 12;
                oSheet.get_Range("A1", "A" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", "A" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                oSheet.get_Range("A4", "D4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A4", "D4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
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

        private static string QueryString(string startDate, string EndDate)
        {
            string sum = " SUM(Invoice.TotalInvoiceAmount) ";
            string alljoining = " FROM  Invoice INNER JOIN InvoiceDetails on InvoiceDetails.InvoiceNo=Invoice.InvoiceNo INNER JOIN item ON item.ID = InvoiceDetails.ItemID  ";
            string dateWise = "and  Invoice.InvoiceDate between '" + startDate + "' and '" + EndDate + "' and Status<>'Cancel'";
            string query = "DECLARE @STATECODE NVARCHAR(200)  " +
            "SET @STATECODE=(SELECT TOP(1)StateID FROM OrganizationDetails)  " +

            #region Inter-State supplies to registered persons
 "select ('Inter-State supplies to registered persons') as Description, (SELECT (" + sum + ") as NillRateAmount " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='NIL' and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as NIL,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='Exampted'and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as Exampted,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='Non GST' and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as NonGST  " +
            #endregion
           "Union " +
            #region Intra-State supplies to registered persons
            "select ('Intra-State supplies to registered persons') as Description, (SELECT (" + sum + ") as NillRateAmount " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='NIL' and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as NIL,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode=@STATECODE) and  item.GSTRate='Exampted' and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as Exampted,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode=@STATECODE) and  item.GSTRate='Non GST' and (ISNULL(Invoice.BillingGSTNO, '') <>'')  " + dateWise + ") as NonGST  " +
            #endregion
           "Union " +
            #region Inter-State supplies to unregistered persons
 "select ('Inter-State supplies to unregistered persons') as Description, (SELECT (" + sum + ") as NillRateAmount " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='NIL' and (ISNULL(Invoice.BillingGSTNO, '') ='')  " + dateWise + ") as NIL,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='Exampted' and (ISNULL(Invoice.BillingGSTNO, '') ='')  " + dateWise + ") as Exampted,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode!=@STATECODE) and  item.GSTRate='Non GST' and (ISNULL(Invoice.BillingGSTNO, '') ='')  " + dateWise + ") as NonGST  " +
            #endregion
           "Union " +
            #region Intra-State supplies to unregistered persons
 "select ('Intra-State supplies to unregistered persons') as Description, (SELECT (" + sum + ") as NillRateAmount " + alljoining +
            "WHERE  (Invoice.BillingStateCode=@STATECODE) and  item.GSTRate='NIL' and (ISNULL(Invoice.BillingGSTNO, '') ='')  " + dateWise + ") as NIL,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode=@STATECODE) and  item.GSTRate='Exampted' and (ISNULL(Invoice.BillingGSTNO, '') ='')   " + dateWise + ") as Exampted,   " +

            "(SELECT (" + sum + ") as NillRateAmount   " + alljoining +
            "WHERE  (Invoice.BillingStateCode=@STATECODE) and  item.GSTRate='Non GST' and (ISNULL(Invoice.BillingGSTNO, '') ='')   " + dateWise + ") as NonGST ";
            #endregion

            return query;
        }
    }
}
