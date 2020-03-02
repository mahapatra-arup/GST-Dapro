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
    public partial class StockWindow : Form
    {
        private Form currentForm;
        private StockList frmStockList;
        private ProductEntryHistory frmProductEntryHistory;
        public StockWindow()
        {
            InitializeComponent();
            ApplicationDesign.btnSubSubMenuObject = btnStock;
            object o = (object)btnStock;
            ApplicationDesign.SetSubSubButtonDesign(ref o);
        }
        private void btnStock_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);
            frmStockList = new StockList();
            frmStockList.WindowState = FormWindowState.Maximized;
            frmStockList.FormBorderStyle = FormBorderStyle.None;
            frmStockList.TopLevel = false;

            currentForm = frmStockList;
            frmStockList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmStockList);
            frmStockList.Show();
        }
        private void StockWindow_Shown(object sender, EventArgs e)
        {
            frmStockList = new StockList();
            frmStockList.WindowState = FormWindowState.Maximized;
            frmStockList.FormBorderStyle = FormBorderStyle.None;
            frmStockList.TopLevel = false;

            currentForm = frmStockList;
            frmStockList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmStockList);
            frmStockList.Show();
        }
        private void btnStockEntryHistory_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubSubButtonDesign(ref sender);
            frmProductEntryHistory = new ProductEntryHistory();
            frmProductEntryHistory.WindowState = FormWindowState.Maximized;
            frmProductEntryHistory.FormBorderStyle = FormBorderStyle.None;
            frmProductEntryHistory.TopLevel = false;

            currentForm = frmProductEntryHistory;
            frmProductEntryHistory.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmProductEntryHistory);
            frmProductEntryHistory.Show();
        }
        private void orderSummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sales_OrderItemSummary frmSales_OrderItemSummary = new Sales_OrderItemSummary(Sales_OrderItemSummary._Type.Order);
            frmSales_OrderItemSummary.ShowDialog();
        }
        private void salesSummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sales_OrderItemSummary frmSales_OrderItemSummary = new Sales_OrderItemSummary(Sales_OrderItemSummary._Type.Sales);
            frmSales_OrderItemSummary.ShowDialog();
        }
        private void partySalesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartySalesSummary frmPartySalesSummary = new PartySalesSummary(PartySalesSummary._Type.Party);
            frmPartySalesSummary.ShowDialog();
        }

        private void productSalesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartySalesSummary frmPartySalesSummary = new PartySalesSummary(PartySalesSummary._Type.Item);
            frmPartySalesSummary.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void itemSummaryByCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Summary_CategoryWise categoryWise = new Summary_CategoryWise();
            categoryWise.ShowDialog();
        }
    }
}
