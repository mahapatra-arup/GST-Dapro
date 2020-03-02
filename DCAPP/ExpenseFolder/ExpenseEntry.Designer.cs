namespace DAPRO
{
    partial class ExpenseEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseEntry));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBillAdd = new System.Windows.Forms.Button();
            this.btnNewSupplier = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPaymentDescription = new System.Windows.Forms.TextBox();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.txtMamoNumber = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmbAccountHead = new System.Windows.Forms.ComboBox();
            this.cmbPayeeName = new System.Windows.Forms.ComboBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPaymentAccount = new System.Windows.Forms.ComboBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSlNo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTotPaymentAmt = new System.Windows.Forms.Label();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAccountHeadAdd = new System.Windows.Forms.Button();
            this.pnlChequeDetails = new System.Windows.Forms.GroupBox();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.dtpDateCheque = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.pnlChequeDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(965, 135);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(57, 25);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnBillAdd
            // 
            this.btnBillAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnBillAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBillAdd.FlatAppearance.BorderSize = 0;
            this.btnBillAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillAdd.ForeColor = System.Drawing.Color.Black;
            this.btnBillAdd.Location = new System.Drawing.Point(906, 135);
            this.btnBillAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnBillAdd.Name = "btnBillAdd";
            this.btnBillAdd.Size = new System.Drawing.Size(57, 25);
            this.btnBillAdd.TabIndex = 9;
            this.btnBillAdd.Text = "&Add";
            this.btnBillAdd.UseVisualStyleBackColor = false;
            this.btnBillAdd.Click += new System.EventHandler(this.btnBillAdd_Click);
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
            this.btnNewSupplier.Location = new System.Drawing.Point(597, 66);
            this.btnNewSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewSupplier.Name = "btnNewSupplier";
            this.btnNewSupplier.Size = new System.Drawing.Size(25, 27);
            this.btnNewSupplier.TabIndex = 1;
            this.btnNewSupplier.UseVisualStyleBackColor = false;
            this.btnNewSupplier.Click += new System.EventHandler(this.btnNewSupplier_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(883, 489);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 37);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(737, 489);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 455);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 16);
            this.label7.TabIndex = 37;
            this.label7.Text = "Payment description (If any) :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(350, 114);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "Description";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 455);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Memo No :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(849, 114);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(786, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "*Payment Date :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 114);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Account Head";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "*Payee Name :";
            // 
            // txtPaymentDescription
            // 
            this.txtPaymentDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPaymentDescription.Location = new System.Drawing.Point(15, 475);
            this.txtPaymentDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaymentDescription.MaxLength = 1800;
            this.txtPaymentDescription.Multiline = true;
            this.txtPaymentDescription.Name = "txtPaymentDescription";
            this.txtPaymentDescription.Size = new System.Drawing.Size(395, 50);
            this.txtPaymentDescription.TabIndex = 13;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Location = new System.Drawing.Point(350, 137);
            this.txtItemDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemDescription.MaxLength = 20;
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Size = new System.Drawing.Size(442, 22);
            this.txtItemDescription.TabIndex = 7;
            // 
            // txtMamoNumber
            // 
            this.txtMamoNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMamoNumber.Location = new System.Drawing.Point(418, 475);
            this.txtMamoNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtMamoNumber.MaxLength = 20;
            this.txtMamoNumber.Name = "txtMamoNumber";
            this.txtMamoNumber.Size = new System.Drawing.Size(204, 22);
            this.txtMamoNumber.TabIndex = 12;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(800, 137);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.MaxLength = 11;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(102, 22);
            this.txtAmount.TabIndex = 8;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(894, 70);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(128, 22);
            this.dtpDate.TabIndex = 2;
            // 
            // cmbAccountHead
            // 
            this.cmbAccountHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountHead.DropDownWidth = 400;
            this.cmbAccountHead.FormattingEnabled = true;
            this.cmbAccountHead.ItemHeight = 16;
            this.cmbAccountHead.Location = new System.Drawing.Point(14, 135);
            this.cmbAccountHead.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAccountHead.Name = "cmbAccountHead";
            this.cmbAccountHead.Size = new System.Drawing.Size(302, 24);
            this.cmbAccountHead.TabIndex = 5;
            // 
            // cmbPayeeName
            // 
            this.cmbPayeeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayeeName.FormattingEnabled = true;
            this.cmbPayeeName.Location = new System.Drawing.Point(112, 68);
            this.cmbPayeeName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPayeeName.Name = "cmbPayeeName";
            this.cmbPayeeName.Size = new System.Drawing.Size(481, 24);
            this.cmbPayeeName.TabIndex = 0;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(590, 353);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(31, 18);
            this.lblBalance.TabIndex = 45;
            this.lblBalance.Text = "Rs.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 332);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "*Peyment Method :";
            // 
            // cmbPaymentAccount
            // 
            this.cmbPaymentAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentAccount.DropDownWidth = 521;
            this.cmbPaymentAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentAccount.FormattingEnabled = true;
            this.cmbPaymentAccount.ItemHeight = 16;
            this.cmbPaymentAccount.Location = new System.Drawing.Point(178, 353);
            this.cmbPaymentAccount.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentAccount.Name = "cmbPaymentAccount";
            this.cmbPaymentAccount.Size = new System.Drawing.Size(409, 24);
            this.cmbPaymentAccount.TabIndex = 4;
            this.cmbPaymentAccount.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentAccount_SelectedIndexChanged);
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
            "Cheque"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(13, 353);
            this.cmbPaymentMethod.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(159, 24);
            this.cmbPaymentMethod.TabIndex = 3;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Location = new System.Drawing.Point(11, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 3);
            this.panel1.TabIndex = 48;
            // 
            // lblSlNo
            // 
            this.lblSlNo.AutoSize = true;
            this.lblSlNo.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlNo.Location = new System.Drawing.Point(152, 8);
            this.lblSlNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSlNo.Name = "lblSlNo";
            this.lblSlNo.Size = new System.Drawing.Size(56, 28);
            this.lblSlNo.TabIndex = 47;
            this.lblSlNo.Text = "____";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 9);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 27);
            this.label12.TabIndex = 46;
            this.label12.Text = "Expense  #";
            // 
            // lblTotPaymentAmt
            // 
            this.lblTotPaymentAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotPaymentAmt.Font = new System.Drawing.Font("Play", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotPaymentAmt.Location = new System.Drawing.Point(595, 403);
            this.lblTotPaymentAmt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotPaymentAmt.Name = "lblTotPaymentAmt";
            this.lblTotPaymentAmt.Size = new System.Drawing.Size(425, 22);
            this.lblTotPaymentAmt.TabIndex = 50;
            this.lblTotPaymentAmt.Text = "__________";
            this.lblTotPaymentAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvItemList.ColumnHeadersHeight = 30;
            this.dgvItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.AccountHeadID,
            this.AccountHead,
            this.Description,
            this.Amount,
            this.btnCol});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgvItemList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItemList.Location = new System.Drawing.Point(14, 166);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.dgvItemList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.RowTemplate.Height = 30;
            this.dgvItemList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(1008, 161);
            this.dgvItemList.TabIndex = 11;
            this.dgvItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellClick);
            // 
            // Slno
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Slno.DefaultCellStyle = dataGridViewCellStyle20;
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
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AccountHead.DefaultCellStyle = dataGridViewCellStyle21;
            this.AccountHead.FillWeight = 45F;
            this.AccountHead.HeaderText = "ACCOUNT HEAD";
            this.AccountHead.Name = "AccountHead";
            this.AccountHead.ReadOnly = true;
            this.AccountHead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle22;
            this.Description.HeaderText = "DESCRIPTION";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle23;
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
            // btnAccountHeadAdd
            // 
            this.btnAccountHeadAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAccountHeadAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAccountHeadAdd.FlatAppearance.BorderSize = 0;
            this.btnAccountHeadAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccountHeadAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccountHeadAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAccountHeadAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountHeadAdd.Image")));
            this.btnAccountHeadAdd.Location = new System.Drawing.Point(317, 133);
            this.btnAccountHeadAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccountHeadAdd.Name = "btnAccountHeadAdd";
            this.btnAccountHeadAdd.Size = new System.Drawing.Size(25, 27);
            this.btnAccountHeadAdd.TabIndex = 6;
            this.btnAccountHeadAdd.UseVisualStyleBackColor = false;
            this.btnAccountHeadAdd.Click += new System.EventHandler(this.btnAccountHeadAdd_Click);
            // 
            // pnlChequeDetails
            // 
            this.pnlChequeDetails.Controls.Add(this.txtChequeNo);
            this.pnlChequeDetails.Controls.Add(this.dtpDateCheque);
            this.pnlChequeDetails.Controls.Add(this.label4);
            this.pnlChequeDetails.Controls.Add(this.cmbBank);
            this.pnlChequeDetails.Controls.Add(this.label10);
            this.pnlChequeDetails.Controls.Add(this.label11);
            this.pnlChequeDetails.Location = new System.Drawing.Point(11, 380);
            this.pnlChequeDetails.Name = "pnlChequeDetails";
            this.pnlChequeDetails.Size = new System.Drawing.Size(592, 63);
            this.pnlChequeDetails.TabIndex = 51;
            this.pnlChequeDetails.TabStop = false;
            this.pnlChequeDetails.Text = "Cheque Details";
            this.pnlChequeDetails.Visible = false;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(341, 37);
            this.txtChequeNo.MaxLength = 6;
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(100, 22);
            this.txtChequeNo.TabIndex = 1;
            // 
            // dtpDateCheque
            // 
            this.dtpDateCheque.CustomFormat = "dd-MMM-yyyy";
            this.dtpDateCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateCheque.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateCheque.Location = new System.Drawing.Point(446, 37);
            this.dtpDateCheque.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDateCheque.Name = "dtpDateCheque";
            this.dtpDateCheque.Size = new System.Drawing.Size(131, 22);
            this.dtpDateCheque.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(443, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Issue Date";
            // 
            // cmbBank
            // 
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(8, 37);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(327, 24);
            this.cmbBank.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(338, 18);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Cheque No";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 18);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Bank Name";
            // 
            // ExpenseEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 539);
            this.Controls.Add(this.pnlChequeDetails);
            this.Controls.Add(this.dgvItemList);
            this.Controls.Add(this.lblTotPaymentAmt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSlNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbPaymentAccount);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnBillAdd);
            this.Controls.Add(this.btnAccountHeadAdd);
            this.Controls.Add(this.btnNewSupplier);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPaymentDescription);
            this.Controls.Add(this.txtItemDescription);
            this.Controls.Add(this.txtMamoNumber);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cmbAccountHead);
            this.Controls.Add(this.cmbPayeeName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpenseEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Expense Entry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExpenseEntry_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.pnlChequeDetails.ResumeLayout(false);
            this.pnlChequeDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnBillAdd;
        private System.Windows.Forms.Button btnNewSupplier;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPaymentDescription;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.TextBox txtMamoNumber;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cmbAccountHead;
        private System.Windows.Forms.ComboBox cmbPayeeName;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPaymentAccount;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSlNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotPaymentAmt;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountHeadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewButtonColumn btnCol;
        private System.Windows.Forms.Button btnAccountHeadAdd;
        private System.Windows.Forms.GroupBox pnlChequeDetails;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.DateTimePicker dtpDateCheque;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}