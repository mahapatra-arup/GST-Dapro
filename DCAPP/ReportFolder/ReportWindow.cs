using DAPRO.ReportFolder.GSTR3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class ReportWindow : Form
    {
        public ReportWindow()
        {
            InitializeComponent();
        }
        private void btnAccountList_Click(object sender, EventArgs e)
        {
            AccountHeadList frmAccountHeadList = new AccountHeadList();
            frmAccountHeadList.WindowState = FormWindowState.Maximized;
            frmAccountHeadList.FormBorderStyle = FormBorderStyle.None;
            frmAccountHeadList.TopLevel = false;

            frmAccountHeadList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmAccountHeadList);
            frmAccountHeadList.Show();
        }
        private void tsGSTR1_Click(object sender, EventArgs e)
        {
            InvoiceExcelReportView frmInvoiceExcelReportView = new InvoiceExcelReportView();
            frmInvoiceExcelReportView.TopLevel = false;
            frmInvoiceExcelReportView.FormBorderStyle = FormBorderStyle.None;
            frmInvoiceExcelReportView.WindowState = FormWindowState.Maximized;

            frmInvoiceExcelReportView.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmInvoiceExcelReportView);
            frmInvoiceExcelReportView.Show();
        }
        private void tsGstr2_Click(object sender, EventArgs e)
        {
            PurchaseReportView frmPurchaseReportView = new PurchaseReportView();
            frmPurchaseReportView.TopLevel = false;
            frmPurchaseReportView.FormBorderStyle = FormBorderStyle.None;
            frmPurchaseReportView.WindowState = FormWindowState.Maximized;

            frmPurchaseReportView.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmPurchaseReportView);
            frmPurchaseReportView.Show();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LedgerReport frmLedgerReport = new LedgerReport(LedgerReport._LedgerCategory.Party);
            frmLedgerReport.TopLevel = false;
            frmLedgerReport.FormBorderStyle = FormBorderStyle.None;
            frmLedgerReport.WindowState = FormWindowState.Maximized;

            frmLedgerReport.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmLedgerReport);
            frmLedgerReport.Show();
        }
        private void sundryDebtorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubGroupReport frmCashBook = new SubGroupReport(SubGroupReport._Type.Sundry_Debtors);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void sundryCreditorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubGroupReport frmCashBook = new SubGroupReport(SubGroupReport._Type.Sundry_Creditor);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void cashAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.CASH);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void bankStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.BANK);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.SALES);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.PURCHASE);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.SALES_RETURN);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frmCashBook = new CashBook(CashBook._Type.PURCHASE_RETURN);
            frmCashBook.FitToVertical();
            frmCashBook.ShowDialog(this);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            GSTR3_View frmGSTR3_View = new GSTR3_View();
            frmGSTR3_View.TopLevel = false;
            frmGSTR3_View.FormBorderStyle = FormBorderStyle.None;
            frmGSTR3_View.WindowState = FormWindowState.Maximized;

            frmGSTR3_View.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmGSTR3_View);
            frmGSTR3_View.Show();
            Cursor = Cursors.Default;

        }
    }
}
