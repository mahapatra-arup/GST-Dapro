namespace DAPRO
{
    partial class AdvancePaymentAndCreditNoteLIST
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancePaymentAndCreditNoteLIST));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAdvance = new System.Windows.Forms.DataGridView();
            this.AdvanceReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkAdv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BillDescriptionAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdvanceAmountAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAmountAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjustAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblnoadvancepayment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCreditNote = new System.Windows.Forms.DataGridView();
            this.CRNoteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CrNoteDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CrNoteAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjustAmountCr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCreditNote = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotReceiptAmt = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblInvoiceAmount = new System.Windows.Forms.Label();
            this.lblAdjustAmount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditNote)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(13, 11);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvAdvance);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.lblnoadvancepayment);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCreditNote);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lblCreditNote);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(927, 353);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 15;
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdvance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvAdvance.ColumnHeadersHeight = 30;
            this.dgvAdvance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAdvance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AdvanceReceiptNo,
            this.ChkAdv,
            this.BillDescriptionAdv,
            this.AdvanceAmountAdv,
            this.OpenAmountAdv,
            this.AdjustAmount});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdvance.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvAdvance.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAdvance.Location = new System.Drawing.Point(5, 29);
            this.dgvAdvance.Name = "dgvAdvance";
            this.dgvAdvance.RowHeadersVisible = false;
            this.dgvAdvance.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.dgvAdvance.RowTemplate.Height = 30;
            this.dgvAdvance.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdvance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAdvance.Size = new System.Drawing.Size(912, 154);
            this.dgvAdvance.TabIndex = 1;
            this.dgvAdvance.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdvance_CellClick);
            this.dgvAdvance.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAdvance_EditingControlShowing);
            // 
            // AdvanceReceiptNo
            // 
            this.AdvanceReceiptNo.HeaderText = "AdvanceReceiptNo";
            this.AdvanceReceiptNo.Name = "AdvanceReceiptNo";
            this.AdvanceReceiptNo.Visible = false;
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
            this.BillDescriptionAdv.HeaderText = "RECEIPT DESCRIPTION";
            this.BillDescriptionAdv.Name = "BillDescriptionAdv";
            this.BillDescriptionAdv.ReadOnly = true;
            this.BillDescriptionAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AdvanceAmountAdv
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.AdvanceAmountAdv.DefaultCellStyle = dataGridViewCellStyle12;
            this.AdvanceAmountAdv.FillWeight = 25F;
            this.AdvanceAmountAdv.HeaderText = "ADVANCE AMOUNT";
            this.AdvanceAmountAdv.Name = "AdvanceAmountAdv";
            this.AdvanceAmountAdv.ReadOnly = true;
            this.AdvanceAmountAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OpenAmountAdv
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = null;
            this.OpenAmountAdv.DefaultCellStyle = dataGridViewCellStyle13;
            this.OpenAmountAdv.DividerWidth = 3;
            this.OpenAmountAdv.FillWeight = 25F;
            this.OpenAmountAdv.HeaderText = "OPEN AMOUNT";
            this.OpenAmountAdv.Name = "OpenAmountAdv";
            this.OpenAmountAdv.ReadOnly = true;
            this.OpenAmountAdv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AdjustAmount
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.AdjustAmount.DefaultCellStyle = dataGridViewCellStyle14;
            this.AdjustAmount.DividerWidth = 2;
            this.AdjustAmount.FillWeight = 25F;
            this.AdjustAmount.HeaderText = "ADJUST AMOUNT";
            this.AdjustAmount.Name = "AdjustAmount";
            this.AdjustAmount.ReadOnly = true;
            this.AdjustAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(919, 26);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(4, 207);
            this.label9.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 25);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(4, 207);
            this.label5.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(921, 3);
            this.label3.TabIndex = 0;
            // 
            // lblnoadvancepayment
            // 
            this.lblnoadvancepayment.AutoSize = true;
            this.lblnoadvancepayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnoadvancepayment.Location = new System.Drawing.Point(354, 89);
            this.lblnoadvancepayment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnoadvancepayment.Name = "lblnoadvancepayment";
            this.lblnoadvancepayment.Size = new System.Drawing.Size(202, 25);
            this.lblnoadvancepayment.TabIndex = 0;
            this.lblnoadvancepayment.Text = "No Advance Payment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(386, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Advance Payment List";
            // 
            // dgvCreditNote
            // 
            this.dgvCreditNote.AllowUserToAddRows = false;
            this.dgvCreditNote.AllowUserToDeleteRows = false;
            this.dgvCreditNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCreditNote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCreditNote.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCreditNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCreditNote.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCreditNote.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvCreditNote.ColumnHeadersHeight = 30;
            this.dgvCreditNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCreditNote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CRNoteId,
            this.ChkColumn,
            this.CrNoteDescription,
            this.CrNoteAmount,
            this.OpenAmount,
            this.AdjustAmountCr});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCreditNote.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvCreditNote.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCreditNote.Location = new System.Drawing.Point(7, 27);
            this.dgvCreditNote.Name = "dgvCreditNote";
            this.dgvCreditNote.RowHeadersVisible = false;
            this.dgvCreditNote.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.dgvCreditNote.RowTemplate.Height = 30;
            this.dgvCreditNote.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCreditNote.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCreditNote.Size = new System.Drawing.Size(912, 133);
            this.dgvCreditNote.TabIndex = 0;
            this.dgvCreditNote.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellClick);
            this.dgvCreditNote.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvBills_EditingControlShowing);
            // 
            // CRNoteId
            // 
            this.CRNoteId.HeaderText = "CRNoteId";
            this.CRNoteId.Name = "CRNoteId";
            this.CRNoteId.Visible = false;
            // 
            // ChkColumn
            // 
            this.ChkColumn.FillWeight = 7F;
            this.ChkColumn.HeaderText = "";
            this.ChkColumn.Name = "ChkColumn";
            this.ChkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // CrNoteDescription
            // 
            this.CrNoteDescription.FillWeight = 60F;
            this.CrNoteDescription.HeaderText = "Cr. Note Description";
            this.CrNoteDescription.Name = "CrNoteDescription";
            this.CrNoteDescription.ReadOnly = true;
            this.CrNoteDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CrNoteAmount
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N2";
            dataGridViewCellStyle17.NullValue = null;
            this.CrNoteAmount.DefaultCellStyle = dataGridViewCellStyle17;
            this.CrNoteAmount.FillWeight = 25F;
            this.CrNoteAmount.HeaderText = "Cr Note Amount";
            this.CrNoteAmount.Name = "CrNoteAmount";
            this.CrNoteAmount.ReadOnly = true;
            this.CrNoteAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OpenAmount
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N2";
            dataGridViewCellStyle18.NullValue = null;
            this.OpenAmount.DefaultCellStyle = dataGridViewCellStyle18;
            this.OpenAmount.DividerWidth = 3;
            this.OpenAmount.FillWeight = 25F;
            this.OpenAmount.HeaderText = "OPEN AMOUNT";
            this.OpenAmount.Name = "OpenAmount";
            this.OpenAmount.ReadOnly = true;
            this.OpenAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AdjustAmountCr
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "N2";
            dataGridViewCellStyle19.NullValue = null;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.AdjustAmountCr.DefaultCellStyle = dataGridViewCellStyle19;
            this.AdjustAmountCr.DividerWidth = 2;
            this.AdjustAmountCr.FillWeight = 25F;
            this.AdjustAmountCr.HeaderText = "ADJUST AMOUNT";
            this.AdjustAmountCr.Name = "AdjustAmountCr";
            this.AdjustAmountCr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(921, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(4, 145);
            this.label10.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 23);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(4, 145);
            this.label8.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 165);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(919, 3);
            this.label7.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(919, 3);
            this.label4.TabIndex = 0;
            // 
            // lblCreditNote
            // 
            this.lblCreditNote.AutoSize = true;
            this.lblCreditNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditNote.Location = new System.Drawing.Point(328, 81);
            this.lblCreditNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreditNote.Name = "lblCreditNote";
            this.lblCreditNote.Size = new System.Drawing.Size(359, 25);
            this.lblCreditNote.TabIndex = 0;
            this.lblCreditNote.Text = "No Credit Note Issue For This Customer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(408, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Credit Note List";
            // 
            // lblTotReceiptAmt
            // 
            this.lblTotReceiptAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotReceiptAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotReceiptAmt.Location = new System.Drawing.Point(798, 421);
            this.lblTotReceiptAmt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotReceiptAmt.Name = "lblTotReceiptAmt";
            this.lblTotReceiptAmt.Size = new System.Drawing.Size(140, 22);
            this.lblTotReceiptAmt.TabIndex = 21;
            this.lblTotReceiptAmt.Text = "0.00";
            this.lblTotReceiptAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnCancel.Location = new System.Drawing.Point(795, 452);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 37);
            this.btnCancel.TabIndex = 19;
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
            this.btnSave.Location = new System.Drawing.Point(654, 452);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "    &Done";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(648, 426);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "DUE AMOUNT :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(648, 371);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 18);
            this.label11.TabIndex = 21;
            this.label11.Text = "INVOICE AMOUNT :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoiceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceAmount.Location = new System.Drawing.Point(798, 367);
            this.lblInvoiceAmount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Size = new System.Drawing.Size(140, 22);
            this.lblInvoiceAmount.TabIndex = 21;
            this.lblInvoiceAmount.Text = "0.00";
            this.lblInvoiceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAdjustAmount
            // 
            this.lblAdjustAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdjustAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustAmount.Location = new System.Drawing.Point(798, 395);
            this.lblAdjustAmount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAdjustAmount.Name = "lblAdjustAmount";
            this.lblAdjustAmount.Size = new System.Drawing.Size(140, 22);
            this.lblAdjustAmount.TabIndex = 21;
            this.lblAdjustAmount.Text = "0.00";
            this.lblAdjustAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(648, 399);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(143, 18);
            this.label14.TabIndex = 21;
            this.label14.Text = "ADJUST AMOUNT :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdvancePaymentAndCreditNoteLIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 498);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAdjustAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInvoiceAmount);
            this.Controls.Add(this.lblTotReceiptAmt);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancePaymentAndCreditNoteLIST";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Receipt Voucher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceiptVoucher_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvAdvance;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTotReceiptAmt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblnoadvancepayment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCreditNote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvCreditNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdvanceReceiptNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDescriptionAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdvanceAmountAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenAmountAdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjustAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRNoteId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrNoteDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrNoteAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjustAmountCr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblInvoiceAmount;
        private System.Windows.Forms.Label lblAdjustAmount;
        private System.Windows.Forms.Label label14;
    }
}