namespace DAPRO
{
    partial class InvoiceExcelReportView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceExcelReportView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlCustomDate = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtmEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtmStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblmnth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.CmbFlterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.b2BREPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b2CLREPORTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.b2CSREPORTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cDNRFORMATEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDNURFORMATEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aTREPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXAMREPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSNREPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dOCSFORMATEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXPORTTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sINGELSECTIONWISEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wITHALLSECTIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlCustomDate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Panel1.Controls.Add(this.pnlCustomDate);
            this.splitContainer1.Panel1.Controls.Add(this.lblmnth);
            this.splitContainer1.Panel1.Controls.Add(this.cmbMonth);
            this.splitContainer1.Panel1.Controls.Add(this.CmbFlterBy);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.dgvView);
            this.splitContainer1.Size = new System.Drawing.Size(1029, 367);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlCustomDate
            // 
            this.pnlCustomDate.Controls.Add(this.label3);
            this.pnlCustomDate.Controls.Add(this.dtmEndDate);
            this.pnlCustomDate.Controls.Add(this.dtmStartDate);
            this.pnlCustomDate.Location = new System.Drawing.Point(252, 2);
            this.pnlCustomDate.Name = "pnlCustomDate";
            this.pnlCustomDate.Size = new System.Drawing.Size(264, 21);
            this.pnlCustomDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(119, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "To";
            // 
            // dtmEndDate
            // 
            this.dtmEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmEndDate.Location = new System.Drawing.Point(141, 2);
            this.dtmEndDate.Name = "dtmEndDate";
            this.dtmEndDate.Size = new System.Drawing.Size(113, 20);
            this.dtmEndDate.TabIndex = 1;
            this.dtmEndDate.ValueChanged += new System.EventHandler(this.dtmEndDate_ValueChanged);
            // 
            // dtmStartDate
            // 
            this.dtmStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmStartDate.Location = new System.Drawing.Point(3, 2);
            this.dtmStartDate.Name = "dtmStartDate";
            this.dtmStartDate.Size = new System.Drawing.Size(113, 20);
            this.dtmStartDate.TabIndex = 0;
            this.dtmStartDate.ValueChanged += new System.EventHandler(this.dtmStartDate_ValueChanged);
            // 
            // lblmnth
            // 
            this.lblmnth.AutoSize = true;
            this.lblmnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmnth.ForeColor = System.Drawing.Color.White;
            this.lblmnth.Location = new System.Drawing.Point(184, 3);
            this.lblmnth.Name = "lblmnth";
            this.lblmnth.Size = new System.Drawing.Size(62, 20);
            this.lblmnth.TabIndex = 2;
            this.lblmnth.Text = "Month :";
            // 
            // cmbMonth
            // 
            this.cmbMonth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "JAN",
            "FEB",
            "MAR",
            "APR",
            "MAY",
            "JUN",
            "JUL",
            "AUG",
            "SEP",
            "OCT",
            "NOV",
            "DEC"});
            this.cmbMonth.Location = new System.Drawing.Point(252, 3);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(180, 21);
            this.cmbMonth.TabIndex = 1;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // CmbFlterBy
            // 
            this.CmbFlterBy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CmbFlterBy.FormattingEnabled = true;
            this.CmbFlterBy.Items.AddRange(new object[] {
            "Month Wise",
            "Custom Date"});
            this.CmbFlterBy.Location = new System.Drawing.Point(81, 2);
            this.CmbFlterBy.Name = "CmbFlterBy";
            this.CmbFlterBy.Size = new System.Drawing.Size(97, 21);
            this.CmbFlterBy.TabIndex = 1;
            this.CmbFlterBy.SelectedIndexChanged += new System.EventHandler(this.CmbFlterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter By :";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 28);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1020, 6);
            this.progressBar1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 26);
            this.panel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b2BREPORTToolStripMenuItem,
            this.b2CLREPORTToolStripMenuItem1,
            this.b2CSREPORTToolStripMenuItem1,
            this.cDNRFORMATEToolStripMenuItem,
            this.cDNURFORMATEToolStripMenuItem,
            this.aTREPORTToolStripMenuItem,
            this.eXAMREPORTToolStripMenuItem,
            this.hSNREPORTToolStripMenuItem,
            this.dOCSFORMATEToolStripMenuItem,
            this.eXPORTTOToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1021, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // b2BREPORTToolStripMenuItem
            // 
            this.b2BREPORTToolStripMenuItem.Name = "b2BREPORTToolStripMenuItem";
            this.b2BREPORTToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.b2BREPORTToolStripMenuItem.Text = "B2B REPORT";
            this.b2BREPORTToolStripMenuItem.Click += new System.EventHandler(this.b2BREPORTToolStripMenuItem_Click);
            // 
            // b2CLREPORTToolStripMenuItem1
            // 
            this.b2CLREPORTToolStripMenuItem1.Name = "b2CLREPORTToolStripMenuItem1";
            this.b2CLREPORTToolStripMenuItem1.Size = new System.Drawing.Size(91, 20);
            this.b2CLREPORTToolStripMenuItem1.Text = "B2CL REPORT";
            this.b2CLREPORTToolStripMenuItem1.Click += new System.EventHandler(this.b2CLREPORTToolStripMenuItem1_Click);
            // 
            // b2CSREPORTToolStripMenuItem1
            // 
            this.b2CSREPORTToolStripMenuItem1.Name = "b2CSREPORTToolStripMenuItem1";
            this.b2CSREPORTToolStripMenuItem1.Size = new System.Drawing.Size(91, 20);
            this.b2CSREPORTToolStripMenuItem1.Text = "B2CS REPORT";
            this.b2CSREPORTToolStripMenuItem1.Click += new System.EventHandler(this.b2CSREPORTToolStripMenuItem1_Click);
            // 
            // cDNRFORMATEToolStripMenuItem
            // 
            this.cDNRFORMATEToolStripMenuItem.Name = "cDNRFORMATEToolStripMenuItem";
            this.cDNRFORMATEToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.cDNRFORMATEToolStripMenuItem.Text = "CDNR FORMATE";
            this.cDNRFORMATEToolStripMenuItem.Click += new System.EventHandler(this.cDNRFORMATEToolStripMenuItem_Click);
            // 
            // cDNURFORMATEToolStripMenuItem
            // 
            this.cDNURFORMATEToolStripMenuItem.Name = "cDNURFORMATEToolStripMenuItem";
            this.cDNURFORMATEToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.cDNURFORMATEToolStripMenuItem.Text = "CDNUR FORMATE";
            this.cDNURFORMATEToolStripMenuItem.Click += new System.EventHandler(this.cDNURFORMATEToolStripMenuItem_Click);
            // 
            // aTREPORTToolStripMenuItem
            // 
            this.aTREPORTToolStripMenuItem.Name = "aTREPORTToolStripMenuItem";
            this.aTREPORTToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.aTREPORTToolStripMenuItem.Text = "AT REPORT";
            this.aTREPORTToolStripMenuItem.Click += new System.EventHandler(this.aTREPORTToolStripMenuItem_Click);
            // 
            // eXAMREPORTToolStripMenuItem
            // 
            this.eXAMREPORTToolStripMenuItem.Name = "eXAMREPORTToolStripMenuItem";
            this.eXAMREPORTToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.eXAMREPORTToolStripMenuItem.Text = "EXEMP REPORT";
            this.eXAMREPORTToolStripMenuItem.Click += new System.EventHandler(this.eXAMREPORTToolStripMenuItem_Click);
            // 
            // hSNREPORTToolStripMenuItem
            // 
            this.hSNREPORTToolStripMenuItem.Name = "hSNREPORTToolStripMenuItem";
            this.hSNREPORTToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.hSNREPORTToolStripMenuItem.Text = "HSN REPORT";
            this.hSNREPORTToolStripMenuItem.Click += new System.EventHandler(this.hSNREPORTToolStripMenuItem_Click);
            // 
            // dOCSFORMATEToolStripMenuItem
            // 
            this.dOCSFORMATEToolStripMenuItem.Name = "dOCSFORMATEToolStripMenuItem";
            this.dOCSFORMATEToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.dOCSFORMATEToolStripMenuItem.Text = "DOCS FORMATE";
            this.dOCSFORMATEToolStripMenuItem.Click += new System.EventHandler(this.dOCSFORMATEToolStripMenuItem_Click);
            // 
            // eXPORTTOToolStripMenuItem
            // 
            this.eXPORTTOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sINGELSECTIONWISEToolStripMenuItem,
            this.wITHALLSECTIONToolStripMenuItem});
            this.eXPORTTOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eXPORTTOToolStripMenuItem.Image")));
            this.eXPORTTOToolStripMenuItem.Name = "eXPORTTOToolStripMenuItem";
            this.eXPORTTOToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.eXPORTTOToolStripMenuItem.Text = "EXPORT TO ...";
            // 
            // sINGELSECTIONWISEToolStripMenuItem
            // 
            this.sINGELSECTIONWISEToolStripMenuItem.Name = "sINGELSECTIONWISEToolStripMenuItem";
            this.sINGELSECTIONWISEToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sINGELSECTIONWISEToolStripMenuItem.Text = "SINGEL SECTION WISE";
            this.sINGELSECTIONWISEToolStripMenuItem.Click += new System.EventHandler(this.sINGELSECTIONWISEToolStripMenuItem_Click);
            // 
            // wITHALLSECTIONToolStripMenuItem
            // 
            this.wITHALLSECTIONToolStripMenuItem.Name = "wITHALLSECTIONToolStripMenuItem";
            this.wITHALLSECTIONToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.wITHALLSECTIONToolStripMenuItem.Text = "WITH ALL SECTION";
            this.wITHALLSECTIONToolStripMenuItem.Click += new System.EventHandler(this.wITHALLSECTIONToolStripMenuItem_Click);
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.AllowUserToOrderColumns = true;
            this.dgvView.AllowUserToResizeColumns = false;
            this.dgvView.AllowUserToResizeRows = false;
            this.dgvView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvView.ColumnHeadersHeight = 50;
            this.dgvView.ColumnHeadersVisible = false;
            this.dgvView.EnableHeadersVisualStyles = false;
            this.dgvView.Location = new System.Drawing.Point(3, 35);
            this.dgvView.Name = "dgvView";
            this.dgvView.ReadOnly = true;
            this.dgvView.Size = new System.Drawing.Size(1021, 300);
            this.dgvView.TabIndex = 0;
            // 
            // InvoiceExcelReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 367);
            this.Controls.Add(this.splitContainer1);
            this.Name = "InvoiceExcelReportView";
            this.Text = "InvoiceExcelReportView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlCustomDate.ResumeLayout(false);
            this.pnlCustomDate.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Label lblmnth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ToolStripMenuItem eXPORTTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b2BREPORTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b2CLREPORTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem b2CSREPORTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eXAMREPORTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSNREPORTToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem cDNRFORMATEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDNURFORMATEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dOCSFORMATEToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem aTREPORTToolStripMenuItem;
        private System.Windows.Forms.ComboBox CmbFlterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCustomDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtmEndDate;
        private System.Windows.Forms.DateTimePicker dtmStartDate;
        private System.Windows.Forms.ToolStripMenuItem sINGELSECTIONWISEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wITHALLSECTIONToolStripMenuItem;
    }
}