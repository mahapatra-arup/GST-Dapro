namespace DAPRO
{
    partial class PurchaseBillingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseBillingWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkOverDue = new System.Windows.Forms.CheckBox();
            this.chkDue = new System.Windows.Forms.CheckBox();
            this.chkPaid = new System.Windows.Forms.CheckBox();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.txtInVoiceNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlCustomDate = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtmStartDate = new System.Windows.Forms.DateTimePicker();
            this.rbtnDueDate = new System.Windows.Forms.RadioButton();
            this.rbtnInvoiceDate = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilterHeader = new System.Windows.Forms.Label();
            this.btnFilterOk = new System.Windows.Forms.Button();
            this.lblfilterString = new System.Windows.Forms.Label();
            this.lblFilterClear = new System.Windows.Forms.LinkLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dgvBills = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewBillEntry = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCustomDate.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 27);
            this.label1.TabIndex = 106;
            this.label1.Text = "Purchase Register";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.pnlFilter);
            this.panel1.Controls.Add(this.lblfilterString);
            this.panel1.Controls.Add(this.lblFilterClear);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Controls.Add(this.dgvBills);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnNewBillEntry);
            this.panel1.Location = new System.Drawing.Point(-1, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 407);
            this.panel1.TabIndex = 105;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.SystemColors.Control;
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(754, 4);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 31);
            this.btnExport.TabIndex = 109;
            this.btnExport.Text = "Export Excel";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.groupBox1);
            this.pnlFilter.Controls.Add(this.txtPartyName);
            this.pnlFilter.Controls.Add(this.txtInVoiceNo);
            this.pnlFilter.Controls.Add(this.label4);
            this.pnlFilter.Controls.Add(this.label3);
            this.pnlFilter.Controls.Add(this.panel2);
            this.pnlFilter.Controls.Add(this.btnFilterOk);
            this.pnlFilter.Location = new System.Drawing.Point(3, 41);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(360, 237);
            this.pnlFilter.TabIndex = 107;
            this.pnlFilter.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkOverDue);
            this.groupBox1.Controls.Add(this.chkDue);
            this.groupBox1.Controls.Add(this.chkPaid);
            this.groupBox1.Location = new System.Drawing.Point(6, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "By Status";
            // 
            // ChkOverDue
            // 
            this.ChkOverDue.AutoSize = true;
            this.ChkOverDue.Location = new System.Drawing.Point(267, 19);
            this.ChkOverDue.Name = "ChkOverDue";
            this.ChkOverDue.Size = new System.Drawing.Size(69, 17);
            this.ChkOverDue.TabIndex = 2;
            this.ChkOverDue.Text = "OverDue";
            this.ChkOverDue.UseVisualStyleBackColor = true;
            // 
            // chkDue
            // 
            this.chkDue.AutoSize = true;
            this.chkDue.Location = new System.Drawing.Point(153, 18);
            this.chkDue.Name = "chkDue";
            this.chkDue.Size = new System.Drawing.Size(46, 17);
            this.chkDue.TabIndex = 1;
            this.chkDue.Text = "Due";
            this.chkDue.UseVisualStyleBackColor = true;
            // 
            // chkPaid
            // 
            this.chkPaid.AutoSize = true;
            this.chkPaid.Location = new System.Drawing.Point(38, 18);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Size = new System.Drawing.Size(47, 17);
            this.chkPaid.TabIndex = 0;
            this.chkPaid.Text = "Paid";
            this.chkPaid.UseVisualStyleBackColor = true;
            // 
            // txtPartyName
            // 
            this.txtPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPartyName.Location = new System.Drawing.Point(81, 104);
            this.txtPartyName.MaxLength = 50;
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.Size = new System.Drawing.Size(264, 20);
            this.txtPartyName.TabIndex = 2;
            // 
            // txtInVoiceNo
            // 
            this.txtInVoiceNo.Location = new System.Drawing.Point(81, 76);
            this.txtInVoiceNo.MaxLength = 16;
            this.txtInVoiceNo.Name = "txtInVoiceNo";
            this.txtInVoiceNo.Size = new System.Drawing.Size(264, 20);
            this.txtInVoiceNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Party Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Invoice No:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlCustomDate);
            this.panel2.Controls.Add(this.rbtnDueDate);
            this.panel2.Controls.Add(this.rbtnInvoiceDate);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.lblFilterHeader);
            this.panel2.Location = new System.Drawing.Point(2, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 60);
            this.panel2.TabIndex = 0;
            // 
            // pnlCustomDate
            // 
            this.pnlCustomDate.Controls.Add(this.label2);
            this.pnlCustomDate.Controls.Add(this.dtmEndDate);
            this.pnlCustomDate.Controls.Add(this.dtmStartDate);
            this.pnlCustomDate.Location = new System.Drawing.Point(86, 22);
            this.pnlCustomDate.Name = "pnlCustomDate";
            this.pnlCustomDate.Size = new System.Drawing.Size(264, 29);
            this.pnlCustomDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "To";
            // 
            // dtmEndDate
            // 
            this.dtmEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmEndDate.Location = new System.Drawing.Point(141, 5);
            this.dtmEndDate.Name = "dtmEndDate";
            this.dtmEndDate.Size = new System.Drawing.Size(113, 20);
            this.dtmEndDate.TabIndex = 1;
            this.dtmEndDate.ValueChanged += new System.EventHandler(this.dtmEndDate_ValueChanged);
            // 
            // dtmStartDate
            // 
            this.dtmStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmStartDate.Location = new System.Drawing.Point(3, 5);
            this.dtmStartDate.Name = "dtmStartDate";
            this.dtmStartDate.Size = new System.Drawing.Size(113, 20);
            this.dtmStartDate.TabIndex = 0;
            this.dtmStartDate.ValueChanged += new System.EventHandler(this.dtmStartDate_ValueChanged);
            // 
            // rbtnDueDate
            // 
            this.rbtnDueDate.AutoSize = true;
            this.rbtnDueDate.Location = new System.Drawing.Point(225, 3);
            this.rbtnDueDate.Name = "rbtnDueDate";
            this.rbtnDueDate.Size = new System.Drawing.Size(71, 17);
            this.rbtnDueDate.TabIndex = 1;
            this.rbtnDueDate.TabStop = true;
            this.rbtnDueDate.Text = "Due Date";
            this.rbtnDueDate.UseVisualStyleBackColor = true;
            // 
            // rbtnInvoiceDate
            // 
            this.rbtnInvoiceDate.AutoSize = true;
            this.rbtnInvoiceDate.Location = new System.Drawing.Point(13, 3);
            this.rbtnInvoiceDate.Name = "rbtnInvoiceDate";
            this.rbtnInvoiceDate.Size = new System.Drawing.Size(83, 17);
            this.rbtnInvoiceDate.TabIndex = 0;
            this.rbtnInvoiceDate.TabStop = true;
            this.rbtnInvoiceDate.Text = "InvoiceDate";
            this.rbtnInvoiceDate.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(6, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(78, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripToday,
            this.toolStripCurrentMonth,
            this.toolStripPreviousMonth,
            this.toolStripCurrentFinYear,
            this.customToolStripMenuItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(66, 22);
            this.toolStripDropDownButton1.Text = "Filter By";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripToday
            // 
            this.toolStripToday.Name = "toolStripToday";
            this.toolStripToday.Size = new System.Drawing.Size(171, 22);
            this.toolStripToday.Text = "Today";
            this.toolStripToday.Click += new System.EventHandler(this.toolStripToday_Click);
            // 
            // toolStripCurrentMonth
            // 
            this.toolStripCurrentMonth.Name = "toolStripCurrentMonth";
            this.toolStripCurrentMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentMonth.Text = "Current Month";
            this.toolStripCurrentMonth.Click += new System.EventHandler(this.toolStripCurrentMonth_Click);
            // 
            // toolStripPreviousMonth
            // 
            this.toolStripPreviousMonth.Name = "toolStripPreviousMonth";
            this.toolStripPreviousMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripPreviousMonth.Text = "Previous Month";
            this.toolStripPreviousMonth.Click += new System.EventHandler(this.toolStripPreviousMonth_Click);
            // 
            // toolStripCurrentFinYear
            // 
            this.toolStripCurrentFinYear.Name = "toolStripCurrentFinYear";
            this.toolStripCurrentFinYear.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentFinYear.Text = "Current Fin. Year";
            this.toolStripCurrentFinYear.Click += new System.EventHandler(this.toolStripCurrentFinYear_Click);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.customToolStripMenuItem.Text = "Custom Date";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // lblFilterHeader
            // 
            this.lblFilterHeader.AutoSize = true;
            this.lblFilterHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterHeader.Location = new System.Drawing.Point(87, 28);
            this.lblFilterHeader.Name = "lblFilterHeader";
            this.lblFilterHeader.Size = new System.Drawing.Size(29, 16);
            this.lblFilterHeader.TabIndex = 100;
            this.lblFilterHeader.Text = "___";
            // 
            // btnFilterOk
            // 
            this.btnFilterOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterOk.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFilterOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFilterOk.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFilterOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterOk.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterOk.ForeColor = System.Drawing.Color.White;
            this.btnFilterOk.Image = global::DAPRO.Properties.Resources.Refresh24;
            this.btnFilterOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterOk.Location = new System.Drawing.Point(264, 194);
            this.btnFilterOk.Margin = new System.Windows.Forms.Padding(5);
            this.btnFilterOk.Name = "btnFilterOk";
            this.btnFilterOk.Size = new System.Drawing.Size(89, 31);
            this.btnFilterOk.TabIndex = 4;
            this.btnFilterOk.Text = "     &OK";
            this.btnFilterOk.UseVisualStyleBackColor = false;
            this.btnFilterOk.Click += new System.EventHandler(this.btnFilterOk_Click);
            // 
            // lblfilterString
            // 
            this.lblfilterString.AutoSize = true;
            this.lblfilterString.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilterString.Location = new System.Drawing.Point(95, 15);
            this.lblfilterString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfilterString.Name = "lblfilterString";
            this.lblfilterString.Size = new System.Drawing.Size(35, 16);
            this.lblfilterString.TabIndex = 108;
            this.lblfilterString.Text = "Filter";
            // 
            // lblFilterClear
            // 
            this.lblFilterClear.AutoSize = true;
            this.lblFilterClear.Location = new System.Drawing.Point(135, 17);
            this.lblFilterClear.Name = "lblFilterClear";
            this.lblFilterClear.Size = new System.Drawing.Size(85, 13);
            this.lblFilterClear.TabIndex = 106;
            this.lblFilterClear.TabStop = true;
            this.lblFilterClear.Text = "Set Default Filter";
            this.lblFilterClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFilterClear_LinkClicked);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(10, 10);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(78, 27);
            this.toolStrip2.TabIndex = 105;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Checked = true;
            this.toolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 24);
            this.toolStripButton1.Text = "Filter by";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // dgvBills
            // 
            this.dgvBills.AllowUserToAddRows = false;
            this.dgvBills.AllowUserToDeleteRows = false;
            this.dgvBills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBills.BackgroundColor = System.Drawing.Color.White;
            this.dgvBills.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvBills.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBills.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBills.ColumnHeadersHeight = 45;
            this.dgvBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBills.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBills.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBills.Location = new System.Drawing.Point(0, 39);
            this.dgvBills.Name = "dgvBills";
            this.dgvBills.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBills.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.dgvBills.RowTemplate.Height = 40;
            this.dgvBills.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBills.Size = new System.Drawing.Size(869, 363);
            this.dgvBills.TabIndex = 97;
            this.dgvBills.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellDoubleClick);
            this.dgvBills.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvBills_EditingControlShowing);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(218, 9);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 24);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "    ";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnFilterOk_Click);
            // 
            // btnNewBillEntry
            // 
            this.btnNewBillEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewBillEntry.BackColor = System.Drawing.Color.Transparent;
            this.btnNewBillEntry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewBillEntry.FlatAppearance.BorderSize = 0;
            this.btnNewBillEntry.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewBillEntry.ForeColor = System.Drawing.Color.Maroon;
            this.btnNewBillEntry.Image = ((System.Drawing.Image)(resources.GetObject("btnNewBillEntry.Image")));
            this.btnNewBillEntry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewBillEntry.Location = new System.Drawing.Point(636, 4);
            this.btnNewBillEntry.Name = "btnNewBillEntry";
            this.btnNewBillEntry.Size = new System.Drawing.Size(110, 31);
            this.btnNewBillEntry.TabIndex = 100;
            this.btnNewBillEntry.Text = "    &NEW  BILL";
            this.btnNewBillEntry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewBillEntry.UseVisualStyleBackColor = false;
            this.btnNewBillEntry.Click += new System.EventHandler(this.btnNewBillEntry_Click);
            // 
            // PurchaseBillingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 455);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "PurchaseBillingWindow";
            this.Text = "Billing Windows";
            this.Shown += new System.EventHandler(this.BillingWindow_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlCustomDate.ResumeLayout(false);
            this.pnlCustomDate.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvBills;
        private System.Windows.Forms.Button btnNewBillEntry;
        private System.Windows.Forms.Label lblfilterString;
        private System.Windows.Forms.LinkLabel lblFilterClear;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkOverDue;
        private System.Windows.Forms.CheckBox chkDue;
        private System.Windows.Forms.CheckBox chkPaid;
        private System.Windows.Forms.TextBox txtPartyName;
        private System.Windows.Forms.TextBox txtInVoiceNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlCustomDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmEndDate;
        private System.Windows.Forms.DateTimePicker dtmStartDate;
        private System.Windows.Forms.RadioButton rbtnDueDate;
        private System.Windows.Forms.RadioButton rbtnInvoiceDate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.Button btnFilterOk;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
    }
}