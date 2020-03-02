using System;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class CustomerWindows : Form
    {
        /// <summary>
        /// Window Form Objects
        /// </summary>
        private Form currentForm;
        private CustomerList frmCustomerWindow;
        private CashCustomerList frmCashCustomerlist;
        //private PurchaseBillingWindow frmPurchasebillingwindow;
        public CustomerWindows()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnCustomer;
            object o = (object)btnCustomer;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }
        private void PurchaseWindows_Shown(object sender, EventArgs e)
        {
            frmCustomerWindow = new CustomerList();
            frmCustomerWindow.WindowState = FormWindowState.Maximized;
            frmCustomerWindow.FormBorderStyle = FormBorderStyle.None;
            frmCustomerWindow.TopLevel = false;

            currentForm = frmCustomerWindow;
            frmCustomerWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCustomerWindow);
            frmCustomerWindow.Show();
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmCustomerWindow = new CustomerList();
            frmCustomerWindow.WindowState = FormWindowState.Maximized;
            frmCustomerWindow.FormBorderStyle = FormBorderStyle.None;
            frmCustomerWindow.TopLevel = false;

            currentForm = frmCustomerWindow;
            frmCustomerWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCustomerWindow);
            frmCustomerWindow.Show();
        }

    }
}
