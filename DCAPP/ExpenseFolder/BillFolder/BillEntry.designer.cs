namespace DAPRO
{
    partial class BillEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillEntry));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtMamoNumber = new System.Windows.Forms.TextBox();
            this.txtBillingDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNewSupplier = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbAccountHead = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.btnBillAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSlNo = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTotPaymentAmt = new System.Windows.Forms.Label();
            this.btnAddAccountHead = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.Location = new System.Drawing.Point(131, 62);
            this.cmbSupplierName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(537, 24);
            this.cmbSupplierName.TabIndex = 0;
            this.cmbSupplierName.SelectedIndexChanged += new System.EventHandler(this.cmbSuppliersName_SelectedIndexChanged);
            this.cmbSupplierName.Leave += new System.EventHandler(this.cmbSuppliersName_Leave);
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpBillDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(509, 100);
            this.dtpBillDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(159, 22);
            this.dtpBillDate.TabIndex = 3;
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(830, 100);
            this.dtpDueDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(148, 22);
            this.dtpDueDate.TabIndex = 4;
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(131, 102);
            this.txtBillNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtBillNo.MaxLength = 30;
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(273, 22);
            this.txtBillNo.TabIndex = 2;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(756, 162);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.MaxLength = 11;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(102, 22);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // txtMamoNumber
            // 
            this.txtMamoNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMamoNumber.Location = new System.Drawing.Point(93, 348);
            this.txtMamoNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtMamoNumber.MaxLength = 20;
            this.txtMamoNumber.Name = "txtMamoNumber";
            this.txtMamoNumber.Size = new System.Drawing.Size(309, 22);
            this.txtMamoNumber.TabIndex = 10;
            // 
            // txtBillingDescription
            // 
            this.txtBillingDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBillingDescription.Location = new System.Drawing.Point(13, 403);
            this.txtBillingDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtBillingDescription.MaxLength = 1800;
            this.txtBillingDescription.Multiline = true;
            this.txtBillingDescription.Name = "txtBillingDescription";
            this.txtBillingDescription.Size = new System.Drawing.Size(391, 58);
            this.txtBillingDescription.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "*Suppliers Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "*Billing Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(746, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "*Due Date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Bill No :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(802, 141);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Amount";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 351);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Memo No :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 383);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Billing description (If any) :";
            // 
            // btnNewSupplier
            // 
            this.btnNewSupplier.BackColor = System.Drawing.Color.Transparent;
            this.btnNewSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewSupplier.FlatAppearance.BorderSize = 0;
            this.btnNewSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewSupplier.ForeColor = System.Drawing.Color.Black;
            this.btnNewSupplier.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSupplier.Image")));
            this.btnNewSupplier.Location = new System.Drawing.Point(670, 61);
            this.btnNewSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewSupplier.Name = "btnNewSupplier";
            this.btnNewSupplier.Size = new System.Drawing.Size(26, 25);
            this.btnNewSupplier.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnNewSupplier, "New Suppliers");
            this.btnNewSupplier.UseVisualStyleBackColor = false;
            this.btnNewSupplier.Click += new System.EventHandler(this.btnNewSupplier_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(839, 444);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 37);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(693, 444);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 139);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Account Head";
            // 
            // cmbAccountHead
            // 
            this.cmbAccountHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountHead.DropDownWidth = 400;
            this.cmbAccountHead.FormattingEnabled = true;
            this.cmbAccountHead.ItemHeight = 16;
            this.cmbAccountHead.Location = new System.Drawing.Point(14, 160);
            this.cmbAccountHead.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAccountHead.Name = "cmbAccountHead";
            this.cmbAccountHead.Size = new System.Drawing.Size(275, 24);
            this.cmbAccountHead.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 141);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Description";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Location = new System.Drawing.Point(323, 162);
            this.txtItemDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemDescription.MaxLength = 20;
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Size = new System.Drawing.Size(429, 22);
            this.txtItemDescription.TabIndex = 6;
            // 
            // btnBillAdd
            // 
            this.btnBillAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnBillAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBillAdd.FlatAppearance.BorderSize = 0;
            this.btnBillAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillAdd.ForeColor = System.Drawing.Color.Black;
            this.btnBillAdd.Location = new System.Drawing.Point(862, 161);
            this.btnBillAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnBillAdd.Name = "btnBillAdd";
            this.btnBillAdd.Size = new System.Drawing.Size(57, 25);
            this.btnBillAdd.TabIndex = 8;
            this.btnBillAdd.Text = "&Add";
            this.btnBillAdd.UseVisualStyleBackColor = false;
            this.btnBillAdd.Click += new System.EventHandler(this.btnBillAdd_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(11, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 27);
            this.label10.TabIndex = 7;
            this.label10.Text = "Bill Entry #";
            // 
            // lblSlNo
            // 
            this.lblSlNo.AutoSize = true;
            this.lblSlNo.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlNo.Location = new System.Drawing.Point(147, 11);
            this.lblSlNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSlNo.Name = "lblSlNo";
            this.lblSlNo.Size = new System.Drawing.Size(68, 27);
            this.lblSlNo.TabIndex = 7;
            this.lblSlNo.Text = "____";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(921, 161);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(57, 25);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItemList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemList.ColumnHeadersHeight = 30;
            this.dgvItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.AccountHeadID,
            this.AccountHead,
            this.Description,
            this.Amount,
            this.btnCol});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItemList.Location = new System.Drawing.Point(13, 187);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dgvItemList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.RowTemplate.Height = 30;
            this.dgvItemList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(966, 154);
            this.dgvItemList.TabIndex = 52;
            this.dgvItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellClick_1);
            // 
            // Slno
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Slno.DefaultCellStyle = dataGridViewCellStyle2;
            this.Slno.FillWeight = 8F;
            this.Slno.HeaderText = "";
            this.Slno.Name = "Slno";
            this.Slno.ReadOnly = true;
            this.Slno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AccountHeadID
            // 
            this.AccountHeadID.FillWeight = 1F;
            this.AccountHeadID.HeaderText = "AccountHeadID";
            this.AccountHeadID.Name = "AccountHeadID";
            this.AccountHeadID.ReadOnly = true;
            this.AccountHeadID.Visible = false;
            // 
            // AccountHead
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AccountHead.DefaultCellStyle = dataGridViewCellStyle3;
            this.AccountHead.FillWeight = 45F;
            this.AccountHead.HeaderText = "ACCOUNT HEAD";
            this.AccountHead.Name = "AccountHead";
            this.AccountHead.ReadOnly = true;
            this.AccountHead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle4;
            this.Description.HeaderText = "DESCRIPTION";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle5;
            this.Amount.FillWeight = 20F;
            this.Amount.HeaderText = "AMOUNT";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnCol
            // 
            this.btnCol.FillWeight = 12F;
            this.btnCol.HeaderText = "";
            this.btnCol.Name = "btnCol";
            this.btnCol.ReadOnly = true;
            // 
            // lblTotPaymentAmt
            // 
            this.lblTotPaymentAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotPaymentAmt.Font = new System.Drawing.Font("Play", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotPaymentAmt.Location = new System.Drawing.Point(552, 362);
            this.lblTotPaymentAmt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotPaymentAmt.Name = "lblTotPaymentAmt";
            this.lblTotPaymentAmt.Size = new System.Drawing.Size(425, 22);
            this.lblTotPaymentAmt.TabIndex = 53;
            this.lblTotPaymentAmt.Text = "__________";
            this.lblTotPaymentAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddAccountHead
            // 
            this.btnAddAccountHead.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAccountHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddAccountHead.FlatAppearance.BorderSize = 0;
            this.btnAddAccountHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAccountHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccountHead.ForeColor = System.Drawing.Color.Black;
            this.btnAddAccountHead.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAccountHead.Image")));
            this.btnAddAccountHead.Location = new System.Drawing.Point(291, 159);
            this.btnAddAccountHead.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddAccountHead.Name = "btnAddAccountHead";
            this.btnAddAccountHead.Size = new System.Drawing.Size(26, 25);
            this.btnAddAccountHead.TabIndex = 1;
            this.btnAddAccountHead.UseVisualStyleBackColor = false;
            this.btnAddAccountHead.Click += new System.EventHandler(this.btnAddAccountHead_Click);
            // 
            // BillEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 494);
            this.Controls.Add(this.lblTotPaymentAmt);
            this.Controls.Add(this.dgvItemList);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnBillAdd);
            this.Controls.Add(this.btnAddAccountHead);
            this.Controls.Add(this.btnNewSupplier);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblSlNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBillingDescription);
            this.Controls.Add(this.txtItemDescription);
            this.Controls.Add(this.txtMamoNumber);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.dtpBillDate);
            this.Controls.Add(this.cmbAccountHead);
            this.Controls.Add(this.cmbSupplierName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BillEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BillEntry_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSupplierName;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtMamoNumber;
        private System.Windows.Forms.TextBox txtBillingDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNewSupplier;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbAccountHead;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Button btnBillAdd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSlNo;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.Label lblTotPaymentAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountHeadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewButtonColumn btnCol;
        private System.Windows.Forms.Button btnAddAccountHead;
    }
}