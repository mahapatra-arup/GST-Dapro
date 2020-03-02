namespace DAPRO
{
    partial class LedgerReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbLedgers = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDebit = new System.Windows.Forms.DataGridView();
            this.LedgerIdDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNameDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAtjstDr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDebit = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAtjstCr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.dgvCredit = new System.Windows.Forms.DataGridView();
            this.ledgerIdCr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateCr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ledgerNameCr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountCr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnRunReport = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebit)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredit)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLedgers
            // 
            this.cmbLedgers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLedgers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLedgers.DropDownWidth = 400;
            this.cmbLedgers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLedgers.FormattingEnabled = true;
            this.cmbLedgers.ItemHeight = 16;
            this.cmbLedgers.Location = new System.Drawing.Point(5, 79);
            this.cmbLedgers.Margin = new System.Windows.Forms.Padding(5);
            this.cmbLedgers.Name = "cmbLedgers";
            this.cmbLedgers.Size = new System.Drawing.Size(492, 24);
            this.cmbLedgers.TabIndex = 108;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Location = new System.Drawing.Point(515, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 2);
            this.panel1.TabIndex = 115;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Play", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(8, 28);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(194, 25);
            this.lblHeader.TabIndex = 112;
            this.lblHeader.Text = "ACCOUNT HISTORY";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(515, 75);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(87, 27);
            this.toolStrip1.TabIndex = 109;
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
            // dgvDebit
            // 
            this.dgvDebit.AllowUserToAddRows = false;
            this.dgvDebit.AllowUserToDeleteRows = false;
            this.dgvDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDebit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDebit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDebit.BackgroundColor = System.Drawing.Color.White;
            this.dgvDebit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDebit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDebit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvDebit.ColumnHeadersHeight = 45;
            this.dgvDebit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerIdDr,
            this.DateDr,
            this.LedgerNameDr,
            this.NoDr,
            this.AmountDr});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDebit.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvDebit.Location = new System.Drawing.Point(-2, 26);
            this.dgvDebit.Name = "dgvDebit";
            this.dgvDebit.ReadOnly = true;
            this.dgvDebit.RowHeadersVisible = false;
            this.dgvDebit.RowHeadersWidth = 5;
            this.dgvDebit.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.dgvDebit.RowTemplate.Height = 40;
            this.dgvDebit.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDebit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDebit.Size = new System.Drawing.Size(531, 240);
            this.dgvDebit.TabIndex = 116;
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
            // DateDr
            // 
            this.DateDr.FillWeight = 30F;
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
            // NoDr
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NoDr.DefaultCellStyle = dataGridViewCellStyle20;
            this.NoDr.FillWeight = 35F;
            this.NoDr.HeaderText = "Voucher No.";
            this.NoDr.Name = "NoDr";
            this.NoDr.ReadOnly = true;
            this.NoDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AmountDr
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = null;
            dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.AmountDr.DefaultCellStyle = dataGridViewCellStyle21;
            this.AmountDr.FillWeight = 30F;
            this.AmountDr.HeaderText = "Amount";
            this.AmountDr.Name = "AmountDr";
            this.AmountDr.ReadOnly = true;
            this.AmountDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AmountDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 114);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.lblAtjstDr);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lblDebit);
            this.splitContainer1.Panel1.Controls.Add(this.dgvDebit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.lblAtjstCr);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.lblCredit);
            this.splitContainer1.Panel2.Controls.Add(this.dgvCredit);
            this.splitContainer1.Size = new System.Drawing.Size(1057, 332);
            this.splitContainer1.SplitterDistance = 528;
            this.splitContainer1.TabIndex = 117;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-1, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(530, 29);
            this.label5.TabIndex = 117;
            this.label5.Text = " DEBIT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAtjstDr
            // 
            this.lblAtjstDr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAtjstDr.BackColor = System.Drawing.Color.White;
            this.lblAtjstDr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAtjstDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtjstDr.Location = new System.Drawing.Point(-1, 265);
            this.lblAtjstDr.Name = "lblAtjstDr";
            this.lblAtjstDr.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblAtjstDr.Size = new System.Drawing.Size(530, 31);
            this.lblAtjstDr.TabIndex = 117;
            this.lblAtjstDr.Text = " ";
            this.lblAtjstDr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 117;
            this.label1.Text = "Total :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebit
            // 
            this.lblDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebit.Location = new System.Drawing.Point(389, 302);
            this.lblDebit.Name = "lblDebit";
            this.lblDebit.Size = new System.Drawing.Size(138, 23);
            this.lblDebit.TabIndex = 117;
            this.lblDebit.Text = "_____";
            this.lblDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(-1, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(530, 29);
            this.label6.TabIndex = 117;
            this.label6.Text = "CREDIT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAtjstCr
            // 
            this.lblAtjstCr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAtjstCr.BackColor = System.Drawing.Color.White;
            this.lblAtjstCr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAtjstCr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtjstCr.Location = new System.Drawing.Point(-1, 265);
            this.lblAtjstCr.Name = "lblAtjstCr";
            this.lblAtjstCr.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblAtjstCr.Size = new System.Drawing.Size(527, 31);
            this.lblAtjstCr.TabIndex = 117;
            this.lblAtjstCr.Text = " ";
            this.lblAtjstCr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 117;
            this.label3.Text = "Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCredit
            // 
            this.lblCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredit.Location = new System.Drawing.Point(384, 302);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(138, 23);
            this.lblCredit.TabIndex = 117;
            this.lblCredit.Text = "_____";
            this.lblCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvCredit
            // 
            this.dgvCredit.AllowUserToAddRows = false;
            this.dgvCredit.AllowUserToDeleteRows = false;
            this.dgvCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCredit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCredit.BackgroundColor = System.Drawing.Color.White;
            this.dgvCredit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCredit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCredit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvCredit.ColumnHeadersHeight = 45;
            this.dgvCredit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ledgerIdCr,
            this.dateCr,
            this.ledgerNameCr,
            this.dataGridViewTextBoxColumn1,
            this.amountCr});
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCredit.DefaultCellStyle = dataGridViewCellStyle27;
            this.dgvCredit.Location = new System.Drawing.Point(-2, 26);
            this.dgvCredit.Name = "dgvCredit";
            this.dgvCredit.ReadOnly = true;
            this.dgvCredit.RowHeadersVisible = false;
            this.dgvCredit.RowHeadersWidth = 20;
            this.dgvCredit.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.dgvCredit.RowTemplate.Height = 40;
            this.dgvCredit.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCredit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCredit.Size = new System.Drawing.Size(528, 240);
            this.dgvCredit.TabIndex = 116;
            // 
            // ledgerIdCr
            // 
            dataGridViewCellStyle24.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.ledgerIdCr.DefaultCellStyle = dataGridViewCellStyle24;
            this.ledgerIdCr.HeaderText = "LedgerID";
            this.ledgerIdCr.Name = "ledgerIdCr";
            this.ledgerIdCr.ReadOnly = true;
            this.ledgerIdCr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ledgerIdCr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ledgerIdCr.Visible = false;
            // 
            // dateCr
            // 
            this.dateCr.FillWeight = 30F;
            this.dateCr.HeaderText = "Date";
            this.dateCr.Name = "dateCr";
            this.dateCr.ReadOnly = true;
            this.dateCr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dateCr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ledgerNameCr
            // 
            this.ledgerNameCr.HeaderText = "Particulars";
            this.ledgerNameCr.Name = "ledgerNameCr";
            this.ledgerNameCr.ReadOnly = true;
            this.ledgerNameCr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ledgerNameCr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewTextBoxColumn1.FillWeight = 35F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Voucher No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // amountCr
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Format = "N2";
            dataGridViewCellStyle26.NullValue = null;
            this.amountCr.DefaultCellStyle = dataGridViewCellStyle26;
            this.amountCr.FillWeight = 30F;
            this.amountCr.HeaderText = "Amount";
            this.amountCr.Name = "amountCr";
            this.amountCr.ReadOnly = true;
            this.amountCr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.amountCr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(738, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 16);
            this.label4.TabIndex = 120;
            this.label4.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(765, 78);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(128, 22);
            this.dtpToDate.TabIndex = 118;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(605, 78);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 22);
            this.dtpFromDate.TabIndex = 119;
            // 
            // btnRunReport
            // 
            this.btnRunReport.Location = new System.Drawing.Point(902, 76);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(59, 25);
            this.btnRunReport.TabIndex = 121;
            this.btnRunReport.Text = "&Run";
            this.btnRunReport.UseVisualStyleBackColor = true;
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);
            // 
            // LedgerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 450);
            this.Controls.Add(this.btnRunReport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmbLedgers);
            this.MinimizeBox = false;
            this.Name = "LedgerReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Shown += new System.EventHandler(this.LedgerReport_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebit)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbLedgers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.DataGridView dgvDebit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvCredit;
        private System.Windows.Forms.Label lblDebit;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.Label lblAtjstDr;
        private System.Windows.Forms.Label lblAtjstCr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnRunReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerIdDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNameDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ledgerIdCr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateCr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ledgerNameCr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountCr;
    }
}