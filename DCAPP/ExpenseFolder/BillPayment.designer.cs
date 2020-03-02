namespace DAPRO
{
    partial class BillPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillPayment));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSuppliersName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.cmbPaymentAccount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotPaymentAmt = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvBills = new System.Windows.Forms.DataGridView();
            this.billid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BillType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillingAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Billno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSlNo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAdvance = new System.Windows.Forms.DataGridView();
            this.advancePaymentNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkAdv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BillDescriptionAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdvanceAmountAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAmountAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjustAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlPaymentMethod = new System.Windows.Forms.Panel();
            this.lblBalance = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.dtpDateCheque = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlChequeDetails = new System.Windows.Forms.GroupBox();
            this.txtPaymentTotalAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvance)).BeginInit();
            this.pnlPaymentMethod.SuspendLayout();
            this.pnlChequeDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Payee :";
            // 
            // cmbSuppliersName
            // 
            this.cmbSuppliersName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSuppliersName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSuppliersName.FormattingEnabled = true;
            this.cmbSuppliersName.Location = new System.Drawing.Point(88, 71);
            this.cmbSuppliersName.Margin = new System.Windows.Forms.Padding(5);
            this.cmbSuppliersName.Name = "cmbSuppliersName";
            this.cmbSuppliersName.Size = new System.Drawing.Size(645, 24);
            this.cmbSuppliersName.TabIndex = 0;
            this.cmbSuppliersName.SelectedIndexChanged += new System.EventHandler(this.cmbSuppliersName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Peyment Method ";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.DropDownWidth = 159;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.ItemHeight = 16;
            this.cmbPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Cheque",
            "NEFT",
            "RTGS",
            "IMPS"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(3, 22);
            this.cmbPaymentMethod.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(122, 24);
            this.cmbPaymentMethod.TabIndex = 0;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            // 
            // cmbPaymentAccount
            // 
            this.cmbPaymentAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentAccount.DropDownWidth = 521;
            this.cmbPaymentAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentAccount.FormattingEnabled = true;
            this.cmbPaymentAccount.ItemHeight = 16;
            this.cmbPaymentAccount.Location = new System.Drawing.Point(135, 21);
            this.cmbPaymentAccount.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentAccount.Name = "cmbPaymentAccount";
            this.cmbPaymentAccount.Size = new System.Drawing.Size(415, 24);
            this.cmbPaymentAccount.TabIndex = 1;
            this.cmbPaymentAccount.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentAccount_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Open Bills";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpBillDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(888, 72);
            this.dtpBillDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(131, 22);
            this.dtpBillDate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(814, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pay Date ";
            // 
            // lblTotPaymentAmt
            // 
            this.lblTotPaymentAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotPaymentAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotPaymentAmt.Location = new System.Drawing.Point(731, 468);
            this.lblTotPaymentAmt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotPaymentAmt.Name = "lblTotPaymentAmt";
            this.lblTotPaymentAmt.Size = new System.Drawing.Size(296, 22);
            this.lblTotPaymentAmt.TabIndex = 7;
            this.lblTotPaymentAmt.Text = "__________";
            this.lblTotPaymentAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotPaymentAmt.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(888, 509);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 37);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(742, 509);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "    &Payment";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvBills
            // 
            this.dgvBills.AllowUserToAddRows = false;
            this.dgvBills.AllowUserToDeleteRows = false;
            this.dgvBills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBills.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBills.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBills.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBills.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvBills.ColumnHeadersHeight = 30;
            this.dgvBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.billid,
            this.ChkColumn,
            this.BillType,
            this.BillDescription,
            this.DueDate,
            this.BillingAmount,
            this.DueAmount,
            this.Billno,
            this.PaymentAmount});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBills.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvBills.Location = new System.Drawing.Point(2, 22);
            this.dgvBills.Name = "dgvBills";
            this.dgvBills.RowHeadersVisible = false;
            this.dgvBills.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.dgvBills.RowTemplate.Height = 30;
            this.dgvBills.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBills.Size = new System.Drawing.Size(1013, 163);
            this.dgvBills.TabIndex = 0;
            this.dgvBills.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellClick);
            this.dgvBills.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellEndEdit);
            this.dgvBills.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellLeave);
            this.dgvBills.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBills_CellMouseDown);
            this.dgvBills.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvBills_EditingControlShowing);
            // 
            // billid
            // 
            this.billid.HeaderText = "TransectionID";
            this.billid.Name = "billid";
            this.billid.Visible = false;
            // 
            // ChkColumn
            // 
            this.ChkColumn.FillWeight = 7F;
            this.ChkColumn.HeaderText = "";
            this.ChkColumn.Name = "ChkColumn";
            this.ChkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // BillType
            // 
            this.BillType.FillWeight = 20F;
            this.BillType.HeaderText = "BILL TYPE";
            this.BillType.Name = "BillType";
            this.BillType.ReadOnly = true;
            this.BillType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BillDescription
            // 
            this.BillDescription.FillWeight = 60F;
            this.BillDescription.HeaderText = "BILL DESCRIPTION";
            this.BillDescription.Name = "BillDescription";
            this.BillDescription.ReadOnly = true;
            this.BillDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DueDate
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DueDate.DefaultCellStyle = dataGridViewCellStyle13;
            this.DueDate.FillWeight = 20F;
            this.DueDate.HeaderText = "DUE DATE";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            this.DueDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BillingAmount
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BillingAmount.DefaultCellStyle = dataGridViewCellStyle14;
            this.BillingAmount.FillWeight = 25F;
            this.BillingAmount.HeaderText = "BILLING AMOUNT";
            this.BillingAmount.Name = "BillingAmount";
            this.BillingAmount.ReadOnly = true;
            this.BillingAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DueAmount
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DueAmount.DefaultCellStyle = dataGridViewCellStyle15;
            this.DueAmount.DividerWidth = 4;
            this.DueAmount.FillWeight = 25F;
            this.DueAmount.HeaderText = "DUE AMOUNT";
            this.DueAmount.Name = "DueAmount";
            this.DueAmount.ReadOnly = true;
            this.DueAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Billno
            // 
            this.Billno.HeaderText = "Billno";
            this.Billno.Name = "Billno";
            this.Billno.Visible = false;
            // 
            // PaymentAmount
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.PaymentAmount.DefaultCellStyle = dataGridViewCellStyle16;
            this.PaymentAmount.DividerWidth = 5;
            this.PaymentAmount.FillWeight = 30F;
            this.PaymentAmount.HeaderText = "PAYMENT AMOUNT";
            this.PaymentAmount.Name = "PaymentAmount";
            this.PaymentAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblSlNo
            // 
            this.lblSlNo.AutoSize = true;
            this.lblSlNo.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlNo.Location = new System.Drawing.Point(194, 18);
            this.lblSlNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSlNo.Name = "lblSlNo";
            this.lblSlNo.Size = new System.Drawing.Size(56, 28);
            this.lblSlNo.TabIndex = 1;
            this.lblSlNo.Text = "____";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 29);
            this.label10.TabIndex = 0;
            this.label10.Text = "Bill Payment #";
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 106);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBills);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvAdvance);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(1017, 317);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgvAdvance
            // 
            this.dgvAdvance.AllowUserToAddRows = false;
            this.dgvAdvance.AllowUserToDeleteRows = false;
            this.dgvAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAdvance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdvance.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAdvance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAdvance.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdvance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvAdvance.ColumnHeadersHeight = 30;
            this.dgvAdvance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAdvance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.advancePaymentNO,
            this.ChkAdv,
            this.BillDescriptionAdv,
            this.AdvanceAmountAdv,
            this.OpenAmountAdv,
            this.AdjustAmount});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdvance.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvAdvance.Location = new System.Drawing.Point(2, 24);
            this.dgvAdvance.Name = "dgvAdvance";
            this.dgvAdvance.RowHeadersVisible = false;
            this.dgvAdvance.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.dgvAdvance.RowTemplate.Height = 30;
            this.dgvAdvance.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdvance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAdvance.Size = new System.Drawing.Size(1013, 99);
            this.dgvAdvance.TabIndex = 1;
            this.dgvAdvance.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdvance_CellClick);
            this.dgvAdvance.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdvance_CellEndEdit);
            this.dgvAdvance.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAdvance_CellMouseDown);
            this.dgvAdvance.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAdvance_EditingControlShowing);
            // 
            // advancePaymentNO
            // 
            this.advancePaymentNO.HeaderText = "advancePaymentNO";
            this.advancePaymentNO.Name = "advancePaymentNO";
            this.advancePaymentNO.Visible = false;
            // 
            // ChkAdv
            // 
            this.ChkAdv.FillWeight = 7F;
            this.ChkAdv.HeaderText = "";
            this.ChkAdv.Name = "ChkAdv";
            this.ChkAdv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // BillDescriptionAdv
            // 
            this.BillDescriptionAdv.FillWeight = 60F;
            this.BillDescriptionAdv.HeaderText = "BILL DESCRIPTION";
            this.BillDescriptionAdv.Name = "BillDescriptionAdv";
            this.BillDescriptionAdv.ReadOnly = true;
            this.BillDescriptionAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AdvanceAmountAdv
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.AdvanceAmountAdv.DefaultCellStyle = dataGridViewCellStyle19;
            this.AdvanceAmountAdv.FillWeight = 25F;
            this.AdvanceAmountAdv.HeaderText = "ADVANCE AMOUNT";
            this.AdvanceAmountAdv.Name = "AdvanceAmountAdv";
            this.AdvanceAmountAdv.ReadOnly = true;
            this.AdvanceAmountAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OpenAmountAdv
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OpenAmountAdv.DefaultCellStyle = dataGridViewCellStyle20;
            this.OpenAmountAdv.DividerWidth = 4;
            this.OpenAmountAdv.FillWeight = 25F;
            this.OpenAmountAdv.HeaderText = "OPEN AMOUNT";
            this.OpenAmountAdv.Name = "OpenAmountAdv";
            this.OpenAmountAdv.ReadOnly = true;
            this.OpenAmountAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AdjustAmount
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black;
            this.AdjustAmount.DefaultCellStyle = dataGridViewCellStyle21;
            this.AdjustAmount.DividerWidth = 4;
            this.AdjustAmount.FillWeight = 25F;
            this.AdjustAmount.HeaderText = "ADJUST AMOUNT";
            this.AdjustAmount.Name = "AdjustAmount";
            this.AdjustAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Advance";
            // 
            // pnlPaymentMethod
            // 
            this.pnlPaymentMethod.Controls.Add(this.lblBalance);
            this.pnlPaymentMethod.Controls.Add(this.label8);
            this.pnlPaymentMethod.Controls.Add(this.cmbPaymentAccount);
            this.pnlPaymentMethod.Controls.Add(this.cmbPaymentMethod);
            this.pnlPaymentMethod.Location = new System.Drawing.Point(10, 425);
            this.pnlPaymentMethod.Name = "pnlPaymentMethod";
            this.pnlPaymentMethod.Size = new System.Drawing.Size(555, 52);
            this.pnlPaymentMethod.TabIndex = 3;
            // 
            // lblBalance
            // 
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(300, 2);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(250, 16);
            this.lblBalance.TabIndex = 240;
            this.lblBalance.Text = "Rs.";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Location = new System.Drawing.Point(13, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 3);
            this.panel1.TabIndex = 6;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(14, 35);
            this.txtChequeNo.MaxLength = 6;
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(130, 22);
            this.txtChequeNo.TabIndex = 1;
            // 
            // dtpDateCheque
            // 
            this.dtpDateCheque.CustomFormat = "dd-MMM-yyyy";
            this.dtpDateCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateCheque.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateCheque.Location = new System.Drawing.Point(189, 33);
            this.dtpDateCheque.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDateCheque.Name = "dtpDateCheque";
            this.dtpDateCheque.Size = new System.Drawing.Size(131, 22);
            this.dtpDateCheque.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cheque No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(186, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Issue Date";
            // 
            // pnlChequeDetails
            // 
            this.pnlChequeDetails.Controls.Add(this.txtChequeNo);
            this.pnlChequeDetails.Controls.Add(this.dtpDateCheque);
            this.pnlChequeDetails.Controls.Add(this.label7);
            this.pnlChequeDetails.Controls.Add(this.label5);
            this.pnlChequeDetails.Location = new System.Drawing.Point(10, 481);
            this.pnlChequeDetails.Name = "pnlChequeDetails";
            this.pnlChequeDetails.Size = new System.Drawing.Size(555, 63);
            this.pnlChequeDetails.TabIndex = 9;
            this.pnlChequeDetails.TabStop = false;
            this.pnlChequeDetails.Text = "Cheque Details";
            this.pnlChequeDetails.Visible = false;
            // 
            // txtPaymentTotalAmount
            // 
            this.txtPaymentTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaymentTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentTotalAmount.Location = new System.Drawing.Point(871, 466);
            this.txtPaymentTotalAmount.MaxLength = 15;
            this.txtPaymentTotalAmount.Name = "txtPaymentTotalAmount";
            this.txtPaymentTotalAmount.Size = new System.Drawing.Size(158, 26);
            this.txtPaymentTotalAmount.TabIndex = 10;
            this.txtPaymentTotalAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaymentTotalAmount_KeyPress);
            this.txtPaymentTotalAmount.Leave += new System.EventHandler(this.txtPaymentTotalAmount_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(695, 470);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Total Payment Amnount :";
            // 
            // BillPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 556);
            this.Controls.Add(this.txtPaymentTotalAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlChequeDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlPaymentMethod);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblSlNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpBillDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSuppliersName);
            this.Controls.Add(this.lblTotPaymentAmt);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BillPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BillPayment_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvance)).EndInit();
            this.pnlPaymentMethod.ResumeLayout(false);
            this.pnlPaymentMethod.PerformLayout();
            this.pnlChequeDetails.ResumeLayout(false);
            this.pnlChequeDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSuppliersName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotPaymentAmt;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvBills;
        private System.Windows.Forms.Label lblSlNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvAdvance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlPaymentMethod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.DateTimePicker dtpDateCheque;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox pnlChequeDetails;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn advancePaymentNO;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDescriptionAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdvanceAmountAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenAmountAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjustAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn billid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillingAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Billno;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentAmount;
        private System.Windows.Forms.TextBox txtPaymentTotalAmount;
        private System.Windows.Forms.Label label4;
    }
}