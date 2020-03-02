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
    public class DOCSFormate
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

        public static DataTable DOCSInvoiceGenerate(string startDate, string endDate)
        {
            InitialColumn();

            mDt.Rows.Clear();

            mDt.Rows.Add("Summary of documents issued during the tax period /n (13)");
            mDt.Rows.Add("", "", "", "Total Number", "Total Cancelled");
            mDt.Rows.Add();
            mDt.Rows.Add("Nature  of Document", "Sr. No. From", "Sr. No. To", "Total Number", "Cancelled");

            double _Total = 0d, _TotalCalcel = 0d;
            //*************Single Value Fill***************************
            DataTable dt = Query(startDate, endDate);
            double tot = 0d, _totCalcel = 0d;
            foreach (DataRow item in dt.Rows)
            {
                mDt.Rows.Add(item["1"], item["2"], item["3"], item["4"], item["5"]);
                double.TryParse(item["4"].ToString(), out tot);
                double.TryParse(item["5"].ToString(), out _totCalcel);
                _Total += tot;
                _TotalCalcel += _totCalcel;
            }
            //*************Single Value Fill***************************
            mDt.Rows[2][3] = _Total;
            mDt.Rows[2][4] = _TotalCalcel;

            return mDt;
        }


        #region eXPORT
        public DOCSFormate()
        {

        }

        public static void ExportToExcel(string fileName, string startDate, string endDate)
        {
            DOCSInvoiceGenerate(startDate, endDate);

            Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            string filePath = fileName;
            Excel.Workbook xlWorkBook = oXL.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            Excel.Range oRng;

            var oSheet = (Excel.Worksheet)worksheets.Add(worksheets[oXL.ActiveWorkbook.Worksheets.Count], Type.Missing, Type.Missing, Type.Missing);
            oSheet.Name = "DOCS";

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
            oSheet.get_Range("A2", "E2").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244))))));
            oSheet.get_Range("A1", "E2").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            oSheet.get_Range("A4", "E4").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);


            oSheet.get_Range("D1", "E" + (mDt.Rows.Count + 1)).NumberFormat = "#,###,###0.00";

            oSheet.get_Range("A1", "E" + (mDt.Rows.Count + 1)).Font.FontStyle = "Cambria";
            oSheet.get_Range("A1", "E2").Font.Bold = true;
            oSheet.get_Range("A1", "E2").Font.Size = 12;
            oSheet.get_Range("B1", "C" + (mDt.Rows.Count + 1)).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.get_Range("B1", "C" + (mDt.Rows.Count + 1)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //BorderLine
            oSheet.get_Range("A1", "E3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            oRng = oSheet.get_Range("A1", "E3");
            oRng.EntireColumn.AutoFit();
            #endregion

            oSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //*************its use for  delete <Defult 'Sheet 1'> ******************************
            //Name : delete <Defult 'Sheet 1'>
            try { ((Excel.Worksheet)oXL.ActiveWorkbook.Sheets["Sheet1"]).Delete(); } catch (Exception) { }
            //********************************************************************************** 


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

        private static DataTable Query(string startDate, string endDate)
        {
            DataTable _Dt = new DataTable();

            _Dt.Columns.Add("1", typeof(string));
            _Dt.Columns.Add("2", typeof(string));
            _Dt.Columns.Add("3", typeof(string));
            _Dt.Columns.Add("4", typeof(string));
            _Dt.Columns.Add("5", typeof(string));

            //*************Invoice for outward supply***************************
            string Query = "select ID,InvoiceNo,Status  from Invoice where InvoiceDate between '" + startDate + "' and '" + endDate + "' " +
            " order by  ID asc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                _Dt.Rows.Add("Invoice for outward supply", dt.Rows[0]["InvoiceNo"], dt.Rows[dt.Rows.Count - 1]["InvoiceNo"],
                dt.Rows.Count, dt.Select("Status = 'Cancel'").Length);
            }
            else
            {
                _Dt.Rows.Add("Invoice for outward supply", "", "", "", "");
            }

            //*************Debit Note***************************
            dt = new DataTable();
            Query = "select ID,InvoiceOrADRNo,Status,DocumentType from CDRNote where InvoiceOrADRDate between '" + startDate + "' and '" + endDate + "' and DocumentType='D' " +
            " order by  ID asc";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                _Dt.Rows.Add("Debit Note", dt.Rows[0]["InvoiceOrADRNo"], dt.Rows[dt.Rows.Count - 1]["InvoiceOrADRNo"],
                dt.Rows.Count, dt.Select("Status = 'Cancel'").Length);
            }
            else
            {
                _Dt.Rows.Add("Debit Note", "", "", "", "");
            }

            //*************Delivery Challan for job work***************************
            dt = new DataTable();
            Query = "select ID,ChallanNo,Status from Challan where  ChallanDate between '" + startDate + "' and '" + endDate + "' " +
            "order by  ID asc";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                _Dt.Rows.Add("Delivery Challan for job work", dt.Rows[0]["ChallanNo"], dt.Rows[dt.Rows.Count - 1]["ChallanNo"],
                dt.Rows.Count, dt.Select("Status = 'Cancel'").Length);

            }
            else
            {
                _Dt.Rows.Add("Delivery Challan for job work", "", "", "", "");
            }
            //*************Invoice for inward supply from unregistered person***************************
            dt = new DataTable();
            Query = "select ID,InvoiceNo,Status from PurchaseBill where  " +
            "LedgerId in (Select LadgerID from LadgerMain where (ISNULL(Gstin, '') = ''))  and " +
            "InvoiceDate between '" + startDate + "' and '" + endDate + "' " +
            "order by  ID asc";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);

            if (dt.IsValidDataTable())
            {
                _Dt.Rows.Add("Invoice for inward supply from unregistered person", dt.Rows[0]["InvoiceNo"], dt.Rows[dt.Rows.Count - 1]["InvoiceNo"],
                dt.Rows.Count, dt.Select("Status = 'Cancel'").Length);

            }
            else
            {
                _Dt.Rows.Add("Invoice for inward supply from unregistered person", "", "", "", "");
            }


            //*************Refund Voucher***************************
            dt = new DataTable();
            Query = "select ID,InvoiceOrADRNo,Status,DocumentType from CDRNote where InvoiceOrADRDate between '" + startDate + "' and '" + endDate + "' and DocumentType='R' " +
            "order by  ID asc";
            dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);


            if (dt.IsValidDataTable())
            {
                _Dt.Rows.Add("Refund Voucher", dt.Rows[0]["InvoiceOrADRNo"], dt.Rows[dt.Rows.Count - 1]["InvoiceOrADRNo"],
                dt.Rows.Count, dt.Select("Status = 'Cancel'").Length);

            }
            else
            {
                _Dt.Rows.Add("Refund Voucher", "", "", "", "");
            }
            return _Dt;
        }
    }
}
