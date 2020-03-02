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
    public partial class InventoryWindow : Form
    {
        private Form currentForm;
        private StockWindow frmStockWindow;
        private PurchaseWindows frmPurchaseWindows;
        public InventoryWindow()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnStock;
            object o = (object)btnStock;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }
        private void btnStock_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);
            if (frmStockWindow != null)
            {
                frmStockWindow = new StockWindow();
                frmStockWindow.WindowState = FormWindowState.Maximized;
                frmStockWindow.FormBorderStyle = FormBorderStyle.None;
                frmStockWindow.TopLevel = false;
            }
            currentForm = frmStockWindow;
            frmStockWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmStockWindow);
            frmStockWindow.Show();
        }
        private void InventoryWindow_Shown(object sender, EventArgs e)
        {
            frmStockWindow = new StockWindow();
            frmStockWindow.WindowState = FormWindowState.Maximized;
            frmStockWindow.FormBorderStyle = FormBorderStyle.None;
            frmStockWindow.TopLevel = false;

            currentForm = frmStockWindow;
            frmStockWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmStockWindow);
            frmStockWindow.Show();
        }
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            frmPurchaseWindows = new PurchaseWindows();
            frmPurchaseWindows.WindowState = FormWindowState.Maximized;
            frmPurchaseWindows.FormBorderStyle = FormBorderStyle.None;
            frmPurchaseWindows.TopLevel = false;

            currentForm = frmPurchaseWindows;
            frmPurchaseWindows.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmPurchaseWindows);
            frmPurchaseWindows.Show();
        }
    }
}
