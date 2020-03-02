using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAPRO.SalesFolder.Invoice;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Threading;
using System.Drawing.Drawing2D;
using System.IO;

namespace DAPRO
{
    public partial class InvoiceExcelReportView : Form
    {
        string mActiveMenuberName = "";
        DateTime _FirstDate, _LastDate;
        int MethodExecutioncount = 0;
        delegate void ToEX();
        public InvoiceExcelReportView()
        {
            InitializeComponent();
            CmbFlterBy.SelectedIndex = 0;
        }
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _MonthDigit = DateTime.ParseExact(cmbMonth.Text, "MMM", CultureInfo.InvariantCulture).Month;
            DateTime now = DateTime.Now;
            _FirstDate = new DateTime(now.Year, _MonthDigit, 1);
            _LastDate = _FirstDate.AddMonths(1).AddDays(-1);

            #region MONTH WISE REPORT
            switch (mActiveMenuberName)
            {
                case "B2B":
                    b2BREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "B2CL":
                    b2CLREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "B2CS":
                    b2CSREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "CDNR":
                    cDNRFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "CDNUR":
                    cDNURFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "AT":
                    aTREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "EXAM":
                    eXAMREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "HSN":
                    hSNREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "DOCS":
                    dOCSFORMATEToolStripMenuItem_Click(null, null);
                    break;
                    #endregion
            }
        }
        #region Export To .. Single section wise
        
        private void ToExcel_SingleSectionWise()
        {
            //****Maximum Progressber Process Declare**********;
            progressBar1.Maximum = 9;
           

            //*********************Array Define*********************
            string[] SectionNames = { "b2b", "b2cl", "b2cs", "cdnr", "cdnur", "at", "exem", "hsn", "docs" };
            //*********************Data Table Array Define*********************
            DataTable[] lstDT = {
           B2BFormate.B2BInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           B2CLFormate.B2CLInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           B2CSFormate.B2CSInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           CDNRFormate.CDNRnvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           CDNURFormate.CDNURnvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           ATFormate.ATInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           EXEMPFormate.EXEMInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           HSNFormate.HSNInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           DOCSFormate.DOCSInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy")),
           };
            //*******Create  <ToEX ToEX = delegate> Delegate variable***************
            ToEX ToEX = delegate
            {
                FolderBrowserDialog sfd = new FolderBrowserDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    string path = sfd.SelectedPath+"\\Section_wise_CSV_files"; // or whatever  
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    int i = 0;
                    foreach (string item in SectionNames)
                    {
                  
                        ExportToExcel_SingleSectionWise(path +"\\" + item+ ".csv", lstDT[i], item);
                        //**********method wise progressber increase**********
                        progressBar1.Value = ++MethodExecutioncount;
                        i++;
                    }
                    Cursor = Cursors.Default;
                    progressBar1.Value = 0;
                   MessageBox.Show("Export Successfull");
                }
            };
            //*******point  <ToEX ToEX = delegate> Delegate variable***************
            ToEX();
        }

        private void ExportToExcel_SingleSectionWise(string fileName, DataTable mdt,string sectionname)
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

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                //**********worksheet row frees******************
                oSheet.Application.ActiveWindow.SplitRow = 1;
                oSheet.Application.ActiveWindow.FreezePanes = true;
                //**********worksheet Name Define******************
                oSheet.Name = sectionname;
                oXL.DisplayAlerts = false;


                int rowCount = mdt.Rows.Count;
                #region Set cell values
                for (int i = 3; i <= rowCount - 1; i++)
                {
                    for (int j = 0; j <= mdt.Columns.Count - 1; j++)
                    {
                        object cell = mdt.Rows[i][j];
                        oSheet.Cells[i-2, j + 1] = cell;
                       
                    }
                }
                #endregion

                #region Header Format
                Excel.Range formatRange00;
                formatRange00 = oSheet.get_Range(oSheet.Cells[1,1], oSheet.Cells[1, mdt.Columns.Count]);
                formatRange00.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Bisque);

                Excel.Range formatRange1;
                formatRange1 = oSheet.get_Range(oSheet.Cells[2,1], oSheet.Cells[ mdt.Rows.Count-3, mdt.Columns.Count]);

                formatRange1.NumberFormat = "#,###,###0.00";
                formatRange1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange1.WrapText = true;
                //AutoFit columns A:Z.
                formatRange1.EntireColumn.AutoFit();
                //formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray); 
                #endregion

                //*************its use for  delete <Defult 'Sheet 1'> ******************************
                //Name : delete <Defult 'Sheet 1'>
                try { ((Excel.Worksheet)oXL.ActiveWorkbook.Sheets["Sheet2"]).Delete(); } catch (Exception) { }
                try { ((Excel.Worksheet)oXL.ActiveWorkbook.Sheets["Sheet3"]).Delete(); } catch (Exception) { }
                //********************************************************************************** 
                
                oWB.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVMSDOS, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                // oWB.Close(true, misValue, misValue);
                oXL.Quit();
                //Format A1:D1 as bold, vertical alignment = center.
                oXL.DisplayAlerts = true;
                oXL.Visible = false;
                oXL.UserControl = false;
            }

            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Excel Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region EXPORT TO Will all section print one workbook
        private void ToExcel()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "GSTR1";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                // progressBar1.Visible = true;
                ExportToExcel(sfd.FileName);
                // progressBar1.Visible = false;
                Cursor = Cursors.Default;
            }
        }
        private void ExportToExcel(string fileName)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            //Excel.Range oRng;
            object misValue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));

                //Delete old Work Sheet
                //oXL.DisplayAlerts = false;
                try
                {
                    int ExcelsheetCount = oXL.ActiveWorkbook.Worksheets.Count;
                    int j = 1;
                    for (j = ExcelsheetCount; j > 1; j--)
                    {
                        ((Excel.Worksheet)oXL.ActiveWorkbook.Sheets[j]).Delete();
                    }
                }
                catch (Exception) { }

                //WorkBook Save
                oWB.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                oWB.Close(true, misValue, misValue);
                oXL.Quit();

                //Maximum Progressber Process Declare;
                progressBar1.Maximum = 9;

                //method wise progressber increase
                progressBar1.Value = ++MethodExecutioncount;
                B2BFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                B2CLFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                B2CSFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                CDNRFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                CDNURFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                ATFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                EXEMPFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                HSNFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
                progressBar1.Value = ++MethodExecutioncount;
                DOCSFormate.ExportToExcel(fileName, _FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));

                MessageBox.Show("Export complete.", "Export To Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Process Complite Then Zero Initialize
                progressBar1.Value = 0;
                MethodExecutioncount = 0;
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
        #endregion

        #region eVENT
        private void b2BREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "B2B";
            dgvView.DataSource = B2BFormate.B2BInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }



        private void b2CLREPORTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "B2CL";
            dgvView.DataSource = B2CLFormate.B2CLInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void b2CSREPORTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "B2CS";
            dgvView.DataSource = B2CSFormate.B2CSInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void hSNREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "HSN";
            dgvView.DataSource = HSNFormate.HSNInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void cDNRFORMATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "CDNR";
            dgvView.DataSource = CDNRFormate.CDNRnvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void cDNURFORMATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "CDNUR";
            dgvView.DataSource = CDNURFormate.CDNURnvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void dOCSFORMATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "DOCS";
            dgvView.DataSource = DOCSFormate.DOCSInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void eXAMREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "EXAM";
            dgvView.DataSource = EXEMPFormate.EXEMInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }
        #endregion

        private void aTREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mActiveMenuberName = "AT";
            dgvView.DataSource = ATFormate.ATInvoiceGenerate(_FirstDate.ToString("dd-MMM-yyyy"), _LastDate.ToString("dd-MMM-yyyy"));
            GriedDesign();
        }

        private void CmbFlterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbFlterBy.Text == "Month Wise")
            {
                lblmnth.Text = "Month :";
                cmbMonth.Show();
                pnlCustomDate.Hide();
                cmbMonth.Text = DateTime.Now.ToString("MMM");
                cmbMonth_SelectedIndexChanged(null, null);
            }
            else if (CmbFlterBy.Text == "Custom Date")
            {
                cmbMonth.Hide();
                lblmnth.Text = "Date :";
                pnlCustomDate.Show();
                dtmStartDate_ValueChanged(null, null);
            }
        }

        private void dtmStartDate_ValueChanged(object sender, EventArgs e)
        {
            _FirstDate = dtmStartDate.Value; ;
            _LastDate = dtmEndDate.Value;

            #region MONTH WISE REPORT
            switch (mActiveMenuberName)
            {
                case "B2B":
                    b2BREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "B2CL":
                    b2CLREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "B2CS":
                    b2CSREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "CDNR":
                    cDNRFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "CDNUR":
                    cDNURFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "AT":
                    aTREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "EXAM":
                    eXAMREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "HSN":
                    hSNREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "DOCS":
                    dOCSFORMATEToolStripMenuItem_Click(null, null);
                    break;
                    #endregion
            }
        }

        private void dtmEndDate_ValueChanged(object sender, EventArgs e)
        {
            _FirstDate = dtmStartDate.Value; ;
            _LastDate = dtmEndDate.Value;

            #region MONTH WISE REPORT
            switch (mActiveMenuberName)
            {
                case "B2B":
                    b2BREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "B2CL":
                    b2CLREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "B2CS":
                    b2CSREPORTToolStripMenuItem1_Click(null, null);
                    break;
                case "CDNR":
                    cDNRFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "CDNUR":
                    cDNURFORMATEToolStripMenuItem_Click(null, null);
                    break;
                case "AT":
                    aTREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "EXAM":
                    eXAMREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "HSN":
                    hSNREPORTToolStripMenuItem_Click(null, null);
                    break;
                case "DOCS":
                    dOCSFORMATEToolStripMenuItem_Click(null, null);
                    break;
                    #endregion
            }
        }

        private void wITHALLSECTIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///export method
            if (!string.IsNullOrEmpty(cmbMonth.Text))
            {
                ToExcel();
            }
        }

        private void sINGELSECTIONWISEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///export method
            if (!string.IsNullOrEmpty(cmbMonth.Text))
            {
                ToExcel_SingleSectionWise();
            }
        }

        private void GriedDesign()
        {
            if (dgvView.Rows.Count > 0)
            {
                dgvView.Rows[0].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244)))));
                dgvView.Rows[0].Cells[0].Style.ForeColor = System.Drawing.Color.White;
                dgvView.Rows[1].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(113)))), ((int)(((byte)(244)))));
                dgvView.Rows[1].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                dgvView.Rows[3].DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
            }
        }
    }
}
