using System;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class TransectionWindow : Form
    {
        /// <summary>
        /// Window Form Objects
        /// </summary>
        private Form currentForm;
        private CashBook frmCashBook;
        private AdvancePaymentList frmAdvanceReceiptList;
        private BankWindow frmBankWindow;
        private TransectionList frmTransectionList;
        public TransectionWindow()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnCashBook;
            object o = (object)btnCashBook;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }
        private void btnChallan_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmAdvanceReceiptList = new AdvancePaymentList();
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
        private void ExpenseWindows_Shown(object sender, EventArgs e)
        {
            frmCashBook = new CashBook(CashBook._Type.CASH);
            frmCashBook.WindowState = FormWindowState.Maximized;
            frmCashBook.FormBorderStyle = FormBorderStyle.None;
            frmCashBook.TopLevel = false;

            currentForm = frmCashBook;
            frmCashBook.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCashBook);
            frmCashBook.Show();
        }
        private void btnExpenses_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmTransectionList = new TransectionList();
            frmTransectionList.WindowState = FormWindowState.Maximized;
            frmTransectionList.FormBorderStyle = FormBorderStyle.None;
            frmTransectionList.TopLevel = false;

            currentForm = frmTransectionList;
            frmTransectionList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmTransectionList);
            frmTransectionList.Show();
        }
        private void btnBanking_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmBankWindow = new BankWindow();
            //frmAdvanceReceiptList = new NoteAndVoucherList();
            frmBankWindow.WindowState = FormWindowState.Maximized;
            frmBankWindow.FormBorderStyle = FormBorderStyle.None;
            frmBankWindow.TopLevel = false;

            currentForm = frmBankWindow;
            frmBankWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmBankWindow);
            frmBankWindow.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);
            frmCashBook = new CashBook(CashBook._Type.CASH);
            frmCashBook.WindowState = FormWindowState.Maximized;
            frmCashBook.FormBorderStyle = FormBorderStyle.None;
            frmCashBook.TopLevel = false;

            currentForm = frmCashBook;
            frmCashBook.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCashBook);
            frmCashBook.Show();
        }
    }
}
