using System;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class SalesWindows : Form
    {
        /// <summary>
        /// Window Form Objects
        /// </summary>
        private Form currentForm;
        private EstimateList frmEstimateList;
        private OrderWindow frmOrderWindow;
        private ChallanList frmChallanlist;
        private InvoiceWindow frmInvoiceWindow;
        private AdvanceReceiptList frmAdvanceReceiptList;
        private NoteAndVoucherList frmNoteAndVoucherList;

        public SalesWindows()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnInvoice;
            object o = (object)btnInvoice;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }
        private void btnInvoice_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmInvoiceWindow = new InvoiceWindow();
            frmInvoiceWindow.WindowState = FormWindowState.Maximized;
            frmInvoiceWindow.FormBorderStyle = FormBorderStyle.None;
            frmInvoiceWindow.TopLevel = false;

            currentForm = frmInvoiceWindow;
            frmInvoiceWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmInvoiceWindow);
            frmInvoiceWindow.Show();
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmOrderWindow = new OrderWindow();
            frmOrderWindow.WindowState = FormWindowState.Maximized;
            frmOrderWindow.FormBorderStyle = FormBorderStyle.None;
            frmOrderWindow.TopLevel = false;

            currentForm = frmOrderWindow;
            frmOrderWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmOrderWindow);
            frmOrderWindow.Show();
        }
        private void btnEstimate_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmEstimateList = new EstimateList();
            frmEstimateList.WindowState = FormWindowState.Maximized;
            frmEstimateList.FormBorderStyle = FormBorderStyle.None;
            frmEstimateList.TopLevel = false;

            currentForm = frmEstimateList;
            frmEstimateList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmEstimateList);
            frmEstimateList.Show();
        }
        private void SalesWindows_Shown(object sender, EventArgs e)
        {
            frmInvoiceWindow = new InvoiceWindow();
            frmInvoiceWindow.WindowState = FormWindowState.Maximized;
            frmInvoiceWindow.FormBorderStyle = FormBorderStyle.None;
            frmInvoiceWindow.TopLevel = false;

            currentForm = frmInvoiceWindow;
            frmInvoiceWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmInvoiceWindow);
            frmInvoiceWindow.Show();
        }
        private void btnAdvanceReceipt_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmAdvanceReceiptList = new AdvanceReceiptList();
            frmAdvanceReceiptList.WindowState = FormWindowState.Maximized;
            frmAdvanceReceiptList.FormBorderStyle = FormBorderStyle.None;
            frmAdvanceReceiptList.TopLevel = false;

            currentForm = frmAdvanceReceiptList;
            frmAdvanceReceiptList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmAdvanceReceiptList);
            frmAdvanceReceiptList.Show();
        }
        private void btnSalesReturn_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmNoteAndVoucherList = new NoteAndVoucherList(NoteAndVoucherList._Type.Credit_Note);
            frmNoteAndVoucherList.WindowState = FormWindowState.Maximized;
            frmNoteAndVoucherList.FormBorderStyle = FormBorderStyle.None;
            frmNoteAndVoucherList.TopLevel = false;

            currentForm = frmNoteAndVoucherList;
            frmNoteAndVoucherList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmNoteAndVoucherList);
            frmNoteAndVoucherList.Show();
        }
        private void btnRefund_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmNoteAndVoucherList = new NoteAndVoucherList(NoteAndVoucherList._Type.Refund_Voucher);
            frmNoteAndVoucherList.WindowState = FormWindowState.Maximized;
            frmNoteAndVoucherList.FormBorderStyle = FormBorderStyle.None;
            frmNoteAndVoucherList.TopLevel = false;

            currentForm = frmNoteAndVoucherList;
            frmNoteAndVoucherList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmNoteAndVoucherList);
            frmNoteAndVoucherList.Show();
        }
    }
}
