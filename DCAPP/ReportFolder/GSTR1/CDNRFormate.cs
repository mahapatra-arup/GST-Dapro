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
    public class CDNRFormate
    {
        private static string msg = "";
        private static DataTable mDt = new DataTable();
        //private static object missing;
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
            mDt.Columns.Add("12", typeof(string));
            mDt.Columns.Add("13", typeof(string));
        }

        public static DataTable CDNRnvoiceGenerate(string startDate, string endDate)
        {
            InitialColumn();

            mDt.Rows.Clear();

            mDt.Rows.Add("Summary For CDNR(9B)");
            mDt.Rows.Add("No. of Recipients", "No. of Invoices", "", "No. of Notes/Vouchers", "", "", "", "", "Total Note/Refund Voucher Value", "", "Total Taxable Value", "Total Cess");
            mDt.Rows.Add();
            mDt.Rows.Add("GSTIN/UIN of Recipient", "Invoice/Advance Receipt Number", "Invoice / Advance Receipt date", "Note/Refund Voucher Number", "Note/Refund Voucher date", "Document Type",
            "Reason For Issuing document", "Place Of Supply", "Note/Refund Voucher Value", "Rate", "Taxable Value", "Cess Amount", "Pre GST");
            
            //Condition
            string condition = " (ISNULL(CDRNote.GSTIN, '') <> '') AND(CDRNote.RefundDate BETWEEN '"+startDate+"' AND '"+endDate+"') ";
            //
            #region sINGLE
            string query = "SELECT     CDRNote.NoteNo, CDRNote.GSTIN, CDRNote.InvoiceOrADRNo,CONVERT(varchar(11), CDRNote.InvoiceOrADRDate,106) as InvoiceOrADRDate,CONVERT(varchar(11),  CDRNote.RefundDate) as RefundDate, CDRNote.DocumentType, CDRNote.PlaceOfSupply, SUM(CDRNote.NoteValue) " +
            "AS NoteValue, (CASE WHEN CDRNote.IsPreGstCrNote = 'Yes' THEN 'Y' ELSE 'N' END) AS PreGstCrNote, CDRNoteDetails.Rate, SUM(CDRNoteDetails.TaxAmount) AS TaxAmount, " +
            "SUM(CDRNoteDetails.CeassAmount)AS CeassAmount, CDRNoteDetails.Reason " +
            "FROM         CDRNote INNER JOIN " +
            "CDRNoteDetails ON CDRNote.NoteID = CDRNoteDetails.NoteID " +
            "WHERE "+condition+" " +
            "GROUP BY CDRNote.NoteNo, CDRNote.GSTIN, CDRNote.InvoiceOrADRNo, CDRNote.InvoiceOrADRDate, CDRNote.RefundDate, CDRNote.DocumentType, CDRNote.PlaceOfSupply, CDRNote.IsPreGstCrNote, " +
            "CDRNoteDetails.Rate, CDRNoteDetails.Reason ";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    double TaxAmount = 0d, NoteValue = 0d, CeassAmount = 0d, Rate = 0d;
                    double.TryParse(item["TaxAmount"].ToString(), out TaxAmount);
                    double.TryParse(item["NoteValue"].ToString(), out NoteValue);
                    double.TryParse(item["Rate"].ToString(), out Rate);
                    double.TryParse(item["CeassAmount"].ToString(), out CeassAmount);

                    mDt.Rows.Add(item["GSTIN"], item["InvoiceOrADRNo"], item["InvoiceOrADRDate"], item["NoteNo"], item["RefundDate"], item["DocumentType"], item["Reason"], item["PlaceOfSupply"]
                    , NoteValue.ToString("0.00"), Rate.ToString("0.00"), TaxAmount.ToString("0.00"), CeassAmount.ToString("0.00"), item["PreGstCrNote"]);
                }
            }
            #endregion

            #region TOTAL
            //Get Total
            string totamount = "(select sum(Inv.Notevalue)  from (select  NoteNo,sum(NoteValue) as  Notevalue from  CDRNote where " + condition + "  group by NoteNo) as Inv) as TotalNoteValue";

            query = "SELECT   "+totamount+", COUNT(Distinct CDRNote.NoteNo) AS CountNoteNo, COUNT(Distinct CDRNote.GSTIN)AS CountGSTIN, COUNT(Distinct CDRNote.InvoiceOrADRNo) AS CountInvoiceOrADRNo, " +
            "SUM(CDRNoteDetails.TaxAmount) AS TotalTaxAmount, SUM(CDRNoteDetails.CeassAmount) AS TotalCeassAmount " +
            "FROM         CDRNote INNER JOIN " +
            "CDRNoteDetails ON CDRNote.NoteID = CDRNoteDetails.NoteID " +
            "WHERE "+condition+" ";

            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt1.IsValidDataTable())
            {
                double totalTaxAmount = 0d, totalNoteValue = 0d, totalCeassAmount = 0d;

                mDt.Rows[2][0] = dt1.Rows[0]["CountGSTIN"];
                mDt.Rows[2][1] = dt1.Rows[0]["CountInvoiceOrADRNo"];
                mDt.Rows[2][3] = dt1.Rows[0]["CountNoteNo"];

                double.TryParse(dt1.Rows[0]["TotalNoteValue"].ToString(), out totalNoteValue);
                double.TryParse(dt1.Rows[0]["TotalTaxAmount"].ToString(), out totalTaxAmount);
                double.TryParse(dt1.Rows[0]["TotalCeassAmount"].ToString(), out totalCeassAmount);

                mDt.Rows[2][8] = totalNoteValue.ToString("0.00");
                mDt.Rows[2][10] = totalTaxAmount.ToString("0.00");
                mDt.Rows[2][11] = totalCeassAmount.ToString("0.00");
            }
            #endregion
            return mDt;
        }

        #region eXPORT
        public CDNRFormate()
        {

        }

        public static void ExportToExcel(string fileName, string startDate, string endDate)
        {
            CDNRnvoiceGenerate(startDate, endDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "CDNR";

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
            oSheet.get_Range("A2", "M2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "M2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "M4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);

            oSheet.get_Range("I3", "L" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

            oSheet.get_Range("A1", "M" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "M2").Font.Bold = true;
            oSheet.get_Range("A1", "M2").Font.Size = 12;
            oSheet.get_Range("A1", "H" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("A1", "H" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //BorderLine
            oSheet.get_Range("A1", "M3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "M3");
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
