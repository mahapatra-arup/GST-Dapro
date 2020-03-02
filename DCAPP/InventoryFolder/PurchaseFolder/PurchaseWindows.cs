using System;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class PurchaseWindows : Form
    {
        /// <summary>
        /// Window Form Objects
        /// </summary>
        private Form currentForm;
        private PurchaseOrderWindow frmpurchaseOrderWindow;
        private PurchaseChallanList frmPurchaseChallanlist;
        private PurchaseBillingWindow frmPurchasebillingwindow;
        private NoteAndVoucherList frmNoteAndVoucherList;
        public PurchaseWindows()
        {
            InitializeComponent();
            ApplicationDesign.btnSubSubMenuObject = btnBillEntry;
            object o = (object)btnBillEntry;
            ApplicationDesign.SetSubSubButtonDesign(ref o);
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);

            frmpurchaseOrderWindow = new PurchaseOrderWindow();
            frmpurchaseOrderWindow.WindowState = FormWindowState.Maximized;
            frmpurchaseOrderWindow.FormBorderStyle = FormBorderStyle.None;
            frmpurchaseOrderWindow.TopLevel = false;

            currentForm = frmpurchaseOrderWindow;
            frmpurchaseOrderWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmpurchaseOrderWindow);
            frmpurchaseOrderWindow.Show();
        }
        private void PurchaseWindows_Shown(object sender, EventArgs e)
        {
            frmPurchasebillingwindow = new PurchaseBillingWindow();
            frmPurchasebillingwindow.WindowState = FormWindowState.Maximized;
            frmPurchasebillingwindow.FormBorderStyle = FormBorderStyle.None;
            frmPurchasebillingwindow.TopLevel = false;

            currentForm = frmPurchasebillingwindow;
            frmPurchasebillingwindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmPurchasebillingwindow);
            frmPurchasebillingwindow.Show();
        }
        private void btnbill_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);

            frmPurchasebillingwindow = new PurchaseBillingWindow();
            frmPurchasebillingwindow.WindowState = FormWindowState.Maximized;
            frmPurchasebillingwindow.FormBorderStyle = FormBorderStyle.None;
            frmPurchasebillingwindow.TopLevel = false;

            currentForm = frmPurchasebillingwindow;
            frmPurchasebillingwindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmPurchasebillingwindow);
            frmPurchasebillingwindow.Show();

        }
        private void btnPurchaseReturn_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);

            frmNoteAndVoucherList = new NoteAndVoucherList(NoteAndVoucherList._Type.Debit_Note);
            frmNoteAndVoucherList.WindowState = FormWindowState.Maximized;
            frmNoteAndVoucherList.FormBorderStyle = FormBorderStyle.None;
            frmNoteAndVoucherList.TopLevel = false;

            currentForm = frmNoteAndVoucherList;
            frmNoteAndVoucherList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmNoteAndVoucherList);
            frmNoteAndVoucherList.Show();
        }

        private void btnAdvancePayment_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);

            AdvancePaymentList frmAdvanceReceiptList = new AdvancePaymentList();
            //frmAdvanceReceiptList = new NoteAndVoucherList();
            frmAdvanceReceiptList.WindowState = FormWindowState.Maximized;
            frmAdvanceReceiptList.FormBorderStyle = FormBorderStyle.None;
            frmAdvanceReceiptList.TopLevel = false;

            currentForm = frmAdvanceReceiptList;
            frmAdvanceReceiptList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmAdvanceReceiptList);
            frmAdvanceReceiptList.Show();
        }
    }
}
