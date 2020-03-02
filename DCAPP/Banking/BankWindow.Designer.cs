namespace DAPRO
{
    partial class BankWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnWithdrawal = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.btnAddNewBank = new System.Windows.Forms.Button();
            this.lblTotalBank = new System.Windows.Forms.Label();
            this.dgvBank = new System.Windows.Forms.DataGridView();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IFSC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblDebit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBalanceDate = new System.Windows.Forms.Label();
            this.lblDebitBalance = new System.Windows.Forms.Label();
            this.lblCreditBalance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStatement = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnWithdrawal);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeposit);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddNewBank);
            this.splitContainer1.Panel1.Controls.Add(this.lblTotalBank);
            this.splitContainer1.Panel1.Controls.Add(this.dgvBank);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.dgvStatement);
            this.splitContainer1.Panel2.Controls.Add(this.dtpToDate);
            this.splitContainer1.Panel2.Controls.Add(this.dtpFromDate);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.btnStatement);
            this.splitContainer1.Size = new System.Drawing.Size(1190, 550);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnWithdrawal
            // 
            this.btnWithdrawal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWithdrawal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWithdrawal.Location = new System.Drawing.Point(1006, 119);
            this.btnWithdrawal.Name = "btnWithdrawal";
            this.btnWithdrawal.Size = new System.Drawing.Size(116, 32);
            this.btnWithdrawal.TabIndex = 110;
            this.btnWithdrawal.Text = "&Withdrawal";
            this.btnWithdrawal.UseVisualStyleBackColor = true;
            this.btnWithdrawal.Click += new System.EventHandler(this.btnWithdrawal_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeposit.Location = new System.Drawing.Point(1006, 84);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(116, 32);
            this.btnDeposit.TabIndex = 110;
            this.btnDeposit.Text = "&Deposit";
            this.btnDeposit.UseVisualStyleBackColor = true;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // btnAddNewBank
            // 
            this.btnAddNewBank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewBank.Location = new System.Drawing.Point(1006, 50);
            this.btnAddNewBank.Name = "btnAddNewBank";
            this.btnAddNewBank.Size = new System.Drawing.Size(116, 32);
            this.btnAddNewBank.TabIndex = 110;
            this.btnAddNewBank.Text = "&Add bank A/C";
            this.btnAddNewBank.UseVisualStyleBackColor = true;
            this.btnAddNewBank.Click += new System.EventHandler(this.btnAddNewBank_Click);
            // 
            // lblTotalBank
            // 
            this.lblTotalBank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBank.Location = new System.Drawing.Point(831, 26);
            this.lblTotalBank.Name = "lblTotalBank";
            this.lblTotalBank.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblTotalBank.Size = new System.Drawing.Size(171, 22);
            this.lblTotalBank.TabIndex = 109;
            this.lblTotalBank.Text = "___";
            this.lblTotalBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBank.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvBank.BackgroundColor = System.Drawing.Color.White;
            this.dgvBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBank.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBank.ColumnHeadersHeight = 30;
            this.dgvBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerID,
            this.TemplateName,
            this.BankName,
            this.Branch,
            this.IFSC,
            this.AccountNo,
            this.Balance});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBank.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvBank.EnableHeadersVisualStyles = false;
            this.dgvBank.Location = new System.Drawing.Point(3, 51);
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.ReadOnly = true;
            this.dgvBank.RowHeadersVisible = false;
            this.dgvBank.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 12, 8, 8);
            this.dgvBank.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.RowTemplate.Height = 45;
            this.dgvBank.RowTemplate.ReadOnly = true;
            this.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBank.Size = new System.Drawing.Size(997, 136);
            this.dgvBank.TabIndex = 108;
            this.dgvBank.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBank_CellDoubleClick);
            // 
            // LedgerID
            // 
            this.LedgerID.HeaderText = "LedgerID";
            this.LedgerID.Name = "LedgerID";
            this.LedgerID.ReadOnly = true;
            this.LedgerID.Visible = false;
            // 
            // TemplateName
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TemplateName.DefaultCellStyle = dataGridViewCellStyle2;
            this.TemplateName.HeaderText = "Bank A/C";
            this.TemplateName.Name = "TemplateName";
            this.TemplateName.ReadOnly = true;
            this.TemplateName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BankName
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BankName.DefaultCellStyle = dataGridViewCellStyle3;
            this.BankName.HeaderText = "Bank Name";
            this.BankName.Name = "BankName";
            this.BankName.ReadOnly = true;
            this.BankName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Branch
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Branch.DefaultCellStyle = dataGridViewCellStyle4;
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            this.Branch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IFSC
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IFSC.DefaultCellStyle = dataGridViewCellStyle5;
            this.IFSC.FillWeight = 40F;
            this.IFSC.HeaderText = "IFSC";
            this.IFSC.Name = "IFSC";
            this.IFSC.ReadOnly = true;
            this.IFSC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AccountNo
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AccountNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.AccountNo.FillWeight = 50F;
            this.AccountNo.HeaderText = "Account No.";
            this.AccountNo.Name = "AccountNo";
            this.AccountNo.ReadOnly = true;
            this.AccountNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Balance
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Balance.DefaultCellStyle = dataGridViewCellStyle7;
            this.Balance.FillWeight = 50F;
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.Balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Play", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 22);
            this.label2.TabIndex = 107;
            this.label2.Text = "BANK ACCOUNTS";
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
            this.panel1.Location = new System.Drawing.Point(-1, 299);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1194, 55);
            this.panel1.TabIndex = 141;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 16);
            this.label4.TabIndex = 124;
            this.label4.Text = "To";
            // 
            // dgvStatement
            // 
            this.dgvStatement.AllowUserToAddRows = false;
            this.dgvStatement.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvStatement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStatement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStatement.BackgroundColor = System.Drawing.Color.White;
            this.dgvStatement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStatement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
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
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvStatement.GridColor = System.Drawing.SystemColors.Control;
            this.dgvStatement.Location = new System.Drawing.Point(-1, 37);
            this.dgvStatement.Name = "dgvStatement";
            this.dgvStatement.ReadOnly = true;
            this.dgvStatement.RowHeadersVisible = false;
            this.dgvStatement.RowHeadersWidth = 5;
            this.dgvStatement.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 8, 2, 4);
            this.dgvStatement.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatement.RowTemplate.Height = 35;
            this.dgvStatement.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatement.Size = new System.Drawing.Size(1191, 263);
            this.dgvStatement.TabIndex = 140;
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.VoucherType.DefaultCellStyle = dataGridViewCellStyle11;
            this.VoucherType.FillWeight = 40F;
            this.VoucherType.HeaderText = "Vch Type";
            this.VoucherType.Name = "VoucherType";
            this.VoucherType.ReadOnly = true;
            this.VoucherType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoDr
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NoDr.DefaultCellStyle = dataGridViewCellStyle12;
            this.NoDr.FillWeight = 35F;
            this.NoDr.HeaderText = "Vch No.";
            this.NoDr.Name = "NoDr";
            this.NoDr.ReadOnly = true;
            this.NoDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TransectionType
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TransectionType.DefaultCellStyle = dataGridViewCellStyle13;
            this.TransectionType.FillWeight = 30F;
            this.TransectionType.HeaderText = "Transection Type";
            this.TransectionType.Name = "TransectionType";
            this.TransectionType.ReadOnly = true;
            this.TransectionType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ChequeNo
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ChequeNo.DefaultCellStyle = dataGridViewCellStyle14;
            this.ChequeNo.FillWeight = 25F;
            this.ChequeNo.HeaderText = "Chk No";
            this.ChequeNo.Name = "ChequeNo";
            this.ChequeNo.ReadOnly = true;
            this.ChequeNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AmountDr
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.AmountDr.DefaultCellStyle = dataGridViewCellStyle15;
            this.AmountDr.FillWeight = 40F;
            this.AmountDr.HeaderText = "Debit";
            this.AmountDr.Name = "AmountDr";
            this.AmountDr.ReadOnly = true;
            this.AmountDr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AmountDr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AmountCredit
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "N2";
            dataGridViewCellStyle16.NullValue = null;
            this.AmountCredit.DefaultCellStyle = dataGridViewCellStyle16;
            this.AmountCredit.FillWeight = 40F;
            this.AmountCredit.HeaderText = "Credit";
            this.AmountCredit.Name = "AmountCredit";
            this.AmountCredit.ReadOnly = true;
            this.AmountCredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BalanceStmt
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N2";
            dataGridViewCellStyle17.NullValue = null;
            this.BalanceStmt.DefaultCellStyle = dataGridViewCellStyle17;
            this.BalanceStmt.FillWeight = 40F;
            this.BalanceStmt.HeaderText = "Balance";
            this.BalanceStmt.Name = "BalanceStmt";
            this.BalanceStmt.ReadOnly = true;
            this.BalanceStmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BalanceStmt.Visible = false;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(255, 10);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(128, 22);
            this.dtpToDate.TabIndex = 122;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(95, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 22);
            this.dtpFromDate.TabIndex = 123;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(5, 7);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(87, 27);
            this.toolStrip1.TabIndex = 121;
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
            // btnStatement
            // 
            this.btnStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatement.Location = new System.Drawing.Point(389, 8);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(116, 26);
            this.btnStatement.TabIndex = 110;
            this.btnStatement.Text = "&Run Statement";
            this.btnStatement.UseVisualStyleBackColor = true;
            this.btnStatement.Click += new System.EventHandler(this.btnStatement_Click);
            // 
            // BankWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 550);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "BankWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bank Window";
            this.Shown += new System.EventHandler(this.BankWindow_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatement)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnAddNewBank;
        private System.Windows.Forms.Label lblTotalBank;
        private System.Windows.Forms.DataGridView dgvBank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWithdrawal;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn IFSC;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.Label lblDebit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBalanceDate;
        private System.Windows.Forms.Label lblDebitBalance;
        private System.Windows.Forms.Label lblCreditBalance;
        private System.Windows.Forms.DataGridView dgvStatement;
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
    }
}