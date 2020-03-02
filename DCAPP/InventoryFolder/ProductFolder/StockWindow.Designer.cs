namespace DAPRO
{
    partial class StockWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.orderSummaryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.salesSummaryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.partySalesSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productSalesSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStockEntryHistory = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.itemSummaryByCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.btnStockEntryHistory);
            this.panel1.Controls.Add(this.btnStock);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 43);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(842, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(180, 44);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.AutoSize = false;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderSummaryToolStripMenuItem1,
            this.salesSummaryToolStripMenuItem1,
            this.partySalesSummaryToolStripMenuItem,
            this.productSalesSummaryToolStripMenuItem,
            this.itemSummaryByCategoryToolStripMenuItem});
            this.toolStripSplitButton1.ForeColor = System.Drawing.Color.Orange;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(157, 41);
            this.toolStripSplitButton1.Text = "Summary";
            // 
            // orderSummaryToolStripMenuItem1
            // 
            this.orderSummaryToolStripMenuItem1.Name = "orderSummaryToolStripMenuItem1";
            this.orderSummaryToolStripMenuItem1.Size = new System.Drawing.Size(268, 24);
            this.orderSummaryToolStripMenuItem1.Text = "Order Summary";
            this.orderSummaryToolStripMenuItem1.Click += new System.EventHandler(this.orderSummaryToolStripMenuItem1_Click);
            // 
            // salesSummaryToolStripMenuItem1
            // 
            this.salesSummaryToolStripMenuItem1.Name = "salesSummaryToolStripMenuItem1";
            this.salesSummaryToolStripMenuItem1.Size = new System.Drawing.Size(268, 24);
            this.salesSummaryToolStripMenuItem1.Text = "Sales Summary";
            this.salesSummaryToolStripMenuItem1.Click += new System.EventHandler(this.salesSummaryToolStripMenuItem1_Click);
            // 
            // partySalesSummaryToolStripMenuItem
            // 
            this.partySalesSummaryToolStripMenuItem.Name = "partySalesSummaryToolStripMenuItem";
            this.partySalesSummaryToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.partySalesSummaryToolStripMenuItem.Text = "Sales Summary [Party Wise]";
            this.partySalesSummaryToolStripMenuItem.Click += new System.EventHandler(this.partySalesSummaryToolStripMenuItem_Click);
            // 
            // productSalesSummaryToolStripMenuItem
            // 
            this.productSalesSummaryToolStripMenuItem.Name = "productSalesSummaryToolStripMenuItem";
            this.productSalesSummaryToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.productSalesSummaryToolStripMenuItem.Text = "Item Summary";
            this.productSalesSummaryToolStripMenuItem.Click += new System.EventHandler(this.productSalesSummaryToolStripMenuItem_Click);
            // 
            // btnStockEntryHistory
            // 
            this.btnStockEntryHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockEntryHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnStockEntryHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStockEntryHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockEntryHistory.FlatAppearance.BorderSize = 0;
            this.btnStockEntryHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnStockEntryHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockEntryHistory.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockEntryHistory.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnStockEntryHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockEntryHistory.Location = new System.Drawing.Point(693, 0);
            this.btnStockEntryHistory.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnStockEntryHistory.Name = "btnStockEntryHistory";
            this.btnStockEntryHistory.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnStockEntryHistory.Size = new System.Drawing.Size(157, 43);
            this.btnStockEntryHistory.TabIndex = 1;
            this.btnStockEntryHistory.Text = "&Update History";
            this.btnStockEntryHistory.UseVisualStyleBackColor = false;
            this.btnStockEntryHistory.Click += new System.EventHandler(this.btnStockEntryHistory_Click);
            // 
            // btnStock
            // 
            this.btnStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStock.BackColor = System.Drawing.SystemColors.Control;
            this.btnStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStock.FlatAppearance.BorderSize = 0;
            this.btnStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStock.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStock.ForeColor = System.Drawing.Color.Black;
            this.btnStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStock.Location = new System.Drawing.Point(540, 0);
            this.btnStock.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnStock.Name = "btnStock";
            this.btnStock.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnStock.Size = new System.Drawing.Size(157, 43);
            this.btnStock.TabIndex = 1;
            this.btnStock.Text = "&Stock";
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // pnlWindow
            // 
            this.pnlWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlWindow.Location = new System.Drawing.Point(-1, 42);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(1020, 399);
            this.pnlWindow.TabIndex = 3;
            // 
            // itemSummaryByCategoryToolStripMenuItem
            // 
            this.itemSummaryByCategoryToolStripMenuItem.Name = "itemSummaryByCategoryToolStripMenuItem";
            this.itemSummaryByCategoryToolStripMenuItem.Size = new System.Drawing.Size(268, 24);
            this.itemSummaryByCategoryToolStripMenuItem.Text = "Item Summary by Category";
            this.itemSummaryByCategoryToolStripMenuItem.Click += new System.EventHandler(this.itemSummaryByCategoryToolStripMenuItem_Click);
            // 
            // StockWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1019, 440);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.panel1);
            this.Name = "StockWindow";
            this.Text = "StockWindow";
            this.Shown += new System.EventHandler(this.StockWindow_Shown);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStockEntryHistory;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem orderSummaryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salesSummaryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem partySalesSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productSalesSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemSummaryByCategoryToolStripMenuItem;
    }
}