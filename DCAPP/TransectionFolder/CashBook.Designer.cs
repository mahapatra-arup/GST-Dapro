namespace DAPRO
{
    partial class CashBook
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStatement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCreditBalance = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblDebit = new System.Windows.Forms.Label();
            this.dgvStatement = new System.Windows.Forms.DataGridView();
            this.LedgerIdDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SlnoDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNameDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransectionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceStmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBalanceDate = new System.Windows.Forms.Label();
            this.lblDebitBalance = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(229, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 16);
            this.label4.TabIndex = 138;
            this.label4.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(256, 61);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(128, 22);
            this.dtpToDate.TabIndex = 136;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(96, 61);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 22);
            this.dtpFromDate.TabIndex = 137;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(6, 58);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(87, 27);
            this.toolStrip1.TabIndex = 135;
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
            this.toolStripCurrentFinYear});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(75, 24);
            this.toolStripDropDownButton1.Text = "Filter By";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripToday
            // 
            this.toolStripToday.Name = "toolStripToday";
            this.toolStripToday.Size = new System.Drawing.Size(184, 24);
            this.toolStripToday.Text = "Today";
            this.toolStripToday.Click += new System.EventHandler(this.toolStripToday_Click);
            // 
            // toolStripCurrentMonth
            // 
            this.toolStripCurrentMonth.Name = "toolStripCurrentMonth";
            this.toolStripCurrentMonth.Size = new System.Drawing.Size(184, 24);
            this.toolStripCurrentMonth.Text = "Current Month";
            this.toolStripCurrentMonth.Click += new System.EventHandler(this.toolStripCurrentMonth_Click);
            // 
            // toolStripPreviousMonth
            // 
            this.toolStripPreviousMonth.Name = "toolStripPreviousMonth";
            this.toolStripPreviousMonth.Size = new System.Drawing.Size(184, 24);
            this.toolStripPreviousMonth.Text = "Previous Month";
            this.toolStripPreviousMonth.Click += new System.EventHandler(this.toolStripPreviousMonth_Click);
            // 
            // toolStripCurrentFinYear
            // 
            this.toolStripCurrentFinYear.Name = "toolStripCurrentFinYear";
            this.toolStripCurrentFinYear.Size = new System.Drawing.Size(184, 24);
            this.toolStripCurrentFinYear.Text = "Current Fin. Year";
            this.toolStripCurrentFinYear.Click += new System.EventHandler(this.toolStripCurrentFinYear_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(148, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 18);
            this.label5.TabIndex = 129;
            this.label5.Text = "Closing Balance";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStatement
            // 
            this.btnStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatement.Location = new System.Drawing.Point(390, 59);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(116, 26);
            this.btnStatement.TabIndex = 125;
            this.btnStatement.Text = "&Run Statement";
            this.btnStatement.UseVisualStyleBackColor = true;
            this.btnStatement.Click += new System.EventHandler(this.btnStatement_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 130;
            this.label1.Text = "Total ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreditBalance
            // 
            this.lblCreditBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreditBalance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditBalance.Location = new System.Drawing.Point(1057, 28);
            this.lblCreditBalance.Name = "lblCreditBalance";
            this.lblCreditBalance.Size = new System.Drawing.Size(122, 23);
            this.lblCreditBalance.TabIndex = 131;
            this.lblCreditBalance.Text = "_____";
            this.lblCreditBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCredit
            // 
            this.lblCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCredit.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredit.Location = new System.Drawing.Point(1057, 1);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(122, 23);
            this.lblCredit.TabIndex = 133;
            this.lblCredit.Text = "______";
            this.lblCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebit
            // 
            this.lblDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebit.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebit.Location = new System.Drawing.Point(904, 2);
            this.lblDebit.Name = "lblDebit";
            this.lblDebit.Size = new System.Drawing.Size(141, 23);
            this.lblDebit.TabIndex = 134;
            this.lblDebit.Text = "_____";
            this.lblDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvStatement
            // 
            this.dgvStatement.AllowUserToAddRows = false;
            this.dgvStatement.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvStatement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStatement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStatement.BackgroundColor = System.Drawing.Color.White;
            this.dgvStatement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStatement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStatement.ColumnHeadersHeight = 45;
            this.dgvStatement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerIdDr,
            this.SlnoDr,
            this.DateDr,
            this.LedgerNameDr,
            this.VoucherType,
            this.NoDr,
            this.TransectionType,
            this.ChequeNo,
            this.AmountDr,
            this.AmountCredit,
            this.BalanceStmt});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvStatement.GridColor = System.Drawing.SystemColors.Control;
            this.dgvStatement.Location = new System.Drawing.Point(-2, 92);
            this.dgvStatement.Name = "dgvStatement";
            this.dgvStatement.ReadOnly = true;
            this.dgvStatement.RowHeadersVisible = false;
            this.dgvStatement.RowHeadersWidth = 5;
            this.dgvStatement.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 8, 2, 4);
            this.dgvStatement.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.RowTemplate.Height = 35;
            this.dgvStatement.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatement.Size = new System.Drawing.Size(1194, 301);
            this.dgvStatement.TabIndex = 126;
            // 
            // LedgerIdDr
            // 
            this.LedgerIdDr.HeaderText = "LedgerID";
            this.LedgerIdDr.Name = "LedgerIdDr";
            this.LedgerIdDr.ReadOnly = true;
            this.LedgerIdDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerIdDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerIdDr.Visible = false;
            // 
            // SlnoDr
            // 
            this.SlnoDr.FillWeight = 10F;
            this.SlnoDr.HeaderText = "Sl No";
            this.SlnoDr.Name = "SlnoDr";
            this.SlnoDr.ReadOnly = true;
            this.SlnoDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlnoDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DateDr
            // 
            this.DateDr.FillWeight = 28F;
            this.DateDr.HeaderText = "Date";
            this.DateDr.Name = "DateDr";
            this.DateDr.ReadOnly = true;
            this.DateDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DateDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LedgerNameDr
            // 
            this.LedgerNameDr.HeaderText = "Particulars";
            this.LedgerNameDr.Name = "LedgerNameDr";
            this.LedgerNameDr.ReadOnly = true;
            this.LedgerNameDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerNameDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VoucherType
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.VoucherType.DefaultCellStyle = dataGridViewCellStyle3;
            this.VoucherType.FillWeight = 40F;
            this.VoucherType.HeaderText = "Vch Type";
            this.VoucherType.Name = "VoucherType";
            this.VoucherType.ReadOnly = true;
            this.VoucherType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoDr
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NoDr.DefaultCellStyle = dataGridViewCellStyle4;
            this.NoDr.FillWeight = 35F;
            this.NoDr.HeaderText = "Vch No.";
            this.NoDr.Name = "NoDr";
            this.NoDr.ReadOnly = true;
            this.NoDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TransectionType
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TransectionType.DefaultCellStyle = dataGridViewCellStyle5;
            this.TransectionType.FillWeight = 30F;
            this.TransectionType.HeaderText = "Transection Type";
            this.TransectionType.Name = "TransectionType";
            this.TransectionType.ReadOnly = true;
            this.TransectionType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ChequeNo
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ChequeNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.ChequeNo.FillWeight = 25F;
            this.ChequeNo.HeaderText = "Chk No";
            this.ChequeNo.Name = "ChequeNo";
            this.ChequeNo.ReadOnly = true;
            this.ChequeNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AmountDr
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.AmountDr.DefaultCellStyle = dataGridViewCellStyle7;
            this.AmountDr.FillWeight = 40F;
            this.AmountDr.HeaderText = "Debit";
            this.AmountDr.Name = "AmountDr";
            this.AmountDr.ReadOnly = true;
            this.AmountDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AmountDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AmountCredit
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.AmountCredit.DefaultCellStyle = dataGridViewCellStyle8;
            this.AmountCredit.FillWeight = 40F;
            this.AmountCredit.HeaderText = "Credit";
            this.AmountCredit.Name = "AmountCredit";
            this.AmountCredit.ReadOnly = true;
            this.AmountCredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BalanceStmt
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.BalanceStmt.DefaultCellStyle = dataGridViewCellStyle9;
            this.BalanceStmt.FillWeight = 40F;
            this.BalanceStmt.HeaderText = "Balance";
            this.BalanceStmt.Name = "BalanceStmt";
            this.BalanceStmt.ReadOnly = true;
            this.BalanceStmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BalanceStmt.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(8, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(117, 25);
            this.lblHeader.TabIndex = 127;
            this.lblHeader.Text = "Cash Book";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblBalanceDate);
            this.panel1.Controls.Add(this.lblDebitBalance);
            this.panel1.Controls.Add(this.lblCreditBalance);
            this.panel1.Location = new System.Drawing.Point(-2, 392);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1194, 55);
            this.panel1.TabIndex = 139;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblCredit);
            this.panel2.Controls.Add(this.lblDebit);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(-1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1193, 28);
            this.panel2.TabIndex = 135;
            // 
            // lblBalanceDate
            // 
            this.lblBalanceDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBalanceDate.AutoSize = true;
            this.lblBalanceDate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceDate.Location = new System.Drawing.Point(43, 30);
            this.lblBalanceDate.Name = "lblBalanceDate";
            this.lblBalanceDate.Size = new System.Drawing.Size(63, 18);
            this.lblBalanceDate.TabIndex = 130;
            this.lblBalanceDate.Text = "____Date";
            this.lblBalanceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebitBalance
            // 
            this.lblDebitBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebitBalance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebitBalance.Location = new System.Drawing.Point(904, 28);
            this.lblDebitBalance.Name = "lblDebitBalance";
            this.lblDebitBalance.Size = new System.Drawing.Size(141, 23);
            this.lblDebitBalance.TabIndex = 131;
            this.lblDebitBalance.Text = "_____";
            this.lblDebitBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CashBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 446);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnStatement);
            this.Controls.Add(this.dgvStatement);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "CashBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cash Book";
            this.Shown += new System.EventHandler(this.CashBook_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCreditBalance;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.Label lblDebit;
        private System.Windows.Forms.DataGridView dgvStatement;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerIdDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlnoDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNameDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransectionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceStmt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDebitBalance;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblBalanceDate;
    }
}