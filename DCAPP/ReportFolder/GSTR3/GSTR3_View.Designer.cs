namespace DAPRO.ReportFolder.GSTR3
{
    partial class GSTR3_View
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.pnlCustomDate = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtmEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtmStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblmnth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.CmbFlterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCustomDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 48);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowExportButton = false;
            this.crystalReportViewer1.ShowGotoPageButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowTextSearchButton = false;
            this.crystalReportViewer1.ShowZoomButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(680, 361);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // pnlCustomDate
            // 
            this.pnlCustomDate.Controls.Add(this.label3);
            this.pnlCustomDate.Controls.Add(this.dtmEndDate);
            this.pnlCustomDate.Controls.Add(this.dtmStartDate);
            this.pnlCustomDate.Location = new System.Drawing.Point(257, 10);
            this.pnlCustomDate.Name = "pnlCustomDate";
            this.pnlCustomDate.Size = new System.Drawing.Size(264, 21);
            this.pnlCustomDate.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
            this.dtmEndDate.Location = new System.Drawing.Point(141, 1);
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
            this.lblmnth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblmnth.Location = new System.Drawing.Point(189, 11);
            this.lblmnth.Name = "lblmnth";
            this.lblmnth.Size = new System.Drawing.Size(62, 20);
            this.lblmnth.TabIndex = 7;
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
            this.cmbMonth.Location = new System.Drawing.Point(257, 11);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(180, 21);
            this.cmbMonth.TabIndex = 5;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // CmbFlterBy
            // 
            this.CmbFlterBy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CmbFlterBy.FormattingEnabled = true;
            this.CmbFlterBy.Items.AddRange(new object[] {
            "Month Wise",
            "Custom Date"});
            this.CmbFlterBy.Location = new System.Drawing.Point(86, 10);
            this.CmbFlterBy.Name = "CmbFlterBy";
            this.CmbFlterBy.Size = new System.Drawing.Size(97, 21);
            this.CmbFlterBy.TabIndex = 6;
            this.CmbFlterBy.SelectedIndexChanged += new System.EventHandler(this.CmbFlterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Filter By :";
            // 
            // GSTR3_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 409);
            this.Controls.Add(this.pnlCustomDate);
            this.Controls.Add(this.lblmnth);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.CmbFlterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "GSTR3_View";
            this.Text = "GSTR3_View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlCustomDate.ResumeLayout(false);
            this.pnlCustomDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel pnlCustomDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtmEndDate;
        private System.Windows.Forms.DateTimePicker dtmStartDate;
        private System.Windows.Forms.Label lblmnth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox CmbFlterBy;
        private System.Windows.Forms.Label label2;
    }
}