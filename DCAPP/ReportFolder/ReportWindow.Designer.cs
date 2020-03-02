namespace DAPRO
{
    partial class ReportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAccountList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAccountReport = new System.Windows.Forms.ToolStripSplitButton();
            this.salesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundryDebtorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundryCreditorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsGSTR1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsGstr2 = new System.Windows.Forms.ToolStripButton();
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 48);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(26, 16);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAccountList,
            this.toolStripSeparator1,
            this.tsAccountReport,
            this.toolStripSeparator4,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.tsGSTR1,
            this.toolStripSeparator2,
            this.tsGstr2,
            this.toolStripSeparator5,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 6);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.toolStrip1.Size = new System.Drawing.Size(781, 42);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAccountList
            // 
            this.btnAccountList.AutoSize = false;
            this.btnAccountList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAccountList.ForeColor = System.Drawing.Color.White;
            this.btnAccountList.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountList.Image")));
            this.btnAccountList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountList.Name = "btnAccountList";
            this.btnAccountList.Size = new System.Drawing.Size(120, 39);
            this.btnAccountList.Text = "Account List";
            this.btnAccountList.Click += new System.EventHandler(this.btnAccountList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // tsAccountReport
            // 
            this.tsAccountReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsAccountReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salesReportToolStripMenuItem,
            this.purchaseReportToolStripMenuItem,
            this.sundryDebtorsToolStripMenuItem,
            this.sundryCreditorsToolStripMenuItem,
            this.salesReturnToolStripMenuItem,
            this.purchaseReturnToolStripMenuItem,
            this.cashAccountToolStripMenuItem,
            this.bankStatementToolStripMenuItem});
            this.tsAccountReport.ForeColor = System.Drawing.Color.White;
            this.tsAccountReport.Image = ((System.Drawing.Image)(resources.GetObject("tsAccountReport.Image")));
            this.tsAccountReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsAccountReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAccountReport.Name = "tsAccountReport";
            this.tsAccountReport.Size = new System.Drawing.Size(155, 33);
            this.tsAccountReport.Text = "Accounting Reports";
            this.tsAccountReport.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // salesReportToolStripMenuItem
            // 
            this.salesReportToolStripMenuItem.Name = "salesReportToolStripMenuItem";
            this.salesReportToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.salesReportToolStripMenuItem.Text = "Sales Report";
            this.salesReportToolStripMenuItem.Click += new System.EventHandler(this.salesReportToolStripMenuItem_Click);
            // 
            // purchaseReportToolStripMenuItem
            // 
            this.purchaseReportToolStripMenuItem.Name = "purchaseReportToolStripMenuItem";
            this.purchaseReportToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.purchaseReportToolStripMenuItem.Text = "Purchase Report";
            this.purchaseReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReportToolStripMenuItem_Click);
            // 
            // sundryDebtorsToolStripMenuItem
            // 
            this.sundryDebtorsToolStripMenuItem.Name = "sundryDebtorsToolStripMenuItem";
            this.sundryDebtorsToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.sundryDebtorsToolStripMenuItem.Text = "Sundry Debtors";
            this.sundryDebtorsToolStripMenuItem.Click += new System.EventHandler(this.sundryDebtorsToolStripMenuItem_Click);
            // 
            // sundryCreditorsToolStripMenuItem
            // 
            this.sundryCreditorsToolStripMenuItem.Name = "sundryCreditorsToolStripMenuItem";
            this.sundryCreditorsToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.sundryCreditorsToolStripMenuItem.Text = "Sundry Creditors";
            this.sundryCreditorsToolStripMenuItem.Click += new System.EventHandler(this.sundryCreditorsToolStripMenuItem_Click);
            // 
            // salesReturnToolStripMenuItem
            // 
            this.salesReturnToolStripMenuItem.Name = "salesReturnToolStripMenuItem";
            this.salesReturnToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.salesReturnToolStripMenuItem.Text = "Sales Return";
            this.salesReturnToolStripMenuItem.Click += new System.EventHandler(this.salesReturnToolStripMenuItem_Click);
            // 
            // purchaseReturnToolStripMenuItem
            // 
            this.purchaseReturnToolStripMenuItem.Name = "purchaseReturnToolStripMenuItem";
            this.purchaseReturnToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.purchaseReturnToolStripMenuItem.Text = "Purchase Return";
            this.purchaseReturnToolStripMenuItem.Click += new System.EventHandler(this.purchaseReturnToolStripMenuItem_Click);
            // 
            // cashAccountToolStripMenuItem
            // 
            this.cashAccountToolStripMenuItem.Name = "cashAccountToolStripMenuItem";
            this.cashAccountToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.cashAccountToolStripMenuItem.Text = "Cash Statement";
            this.cashAccountToolStripMenuItem.Click += new System.EventHandler(this.cashAccountToolStripMenuItem_Click);
            // 
            // bankStatementToolStripMenuItem
            // 
            this.bankStatementToolStripMenuItem.Name = "bankStatementToolStripMenuItem";
            this.bankStatementToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.bankStatementToolStripMenuItem.Text = "Bank Statement";
            this.bankStatementToolStripMenuItem.Click += new System.EventHandler(this.bankStatementToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(94, 33);
            this.toolStripButton1.Text = "Party Report";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 36);
            // 
            // tsGSTR1
            // 
            this.tsGSTR1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsGSTR1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsGSTR1.Image = ((System.Drawing.Image)(resources.GetObject("tsGSTR1.Image")));
            this.tsGSTR1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsGSTR1.Name = "tsGSTR1";
            this.tsGSTR1.Size = new System.Drawing.Size(62, 33);
            this.tsGSTR1.Text = "GSTR-1";
            this.tsGSTR1.Click += new System.EventHandler(this.tsGSTR1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // tsGstr2
            // 
            this.tsGstr2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsGstr2.ForeColor = System.Drawing.Color.White;
            this.tsGstr2.Image = ((System.Drawing.Image)(resources.GetObject("tsGstr2.Image")));
            this.tsGstr2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsGstr2.Name = "tsGstr2";
            this.tsGstr2.Size = new System.Drawing.Size(62, 33);
            this.tsGstr2.Text = "GSTR-2";
            this.tsGstr2.Click += new System.EventHandler(this.tsGstr2_Click);
            // 
            // pnlWindow
            // 
            this.pnlWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlWindow.Controls.Add(this.pictureBox1);
            this.pnlWindow.Controls.Add(this.label1);
            this.pnlWindow.Location = new System.Drawing.Point(0, 45);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(782, 382);
            this.pnlWindow.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(240, 145);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 77);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "ACCOUNTING REPORTS";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(71, 33);
            this.toolStripButton2.Text = "GSTR-3B";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 36);
            // 
            // ReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 427);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.panel1);
            this.Name = "ReportWindow";
            this.Text = "ReportWindow";
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlWindow.ResumeLayout(false);
            this.pnlWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAccountList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsGSTR1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsGstr2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSplitButton tsAccountReport;
        private System.Windows.Forms.ToolStripMenuItem salesReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sundryDebtorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sundryCreditorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bankStatementToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}