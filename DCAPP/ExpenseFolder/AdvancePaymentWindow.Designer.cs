namespace DAPRO
{
    partial class AdvancePaymentWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancePaymentWindow));
            this.btnNewSupplier = new System.Windows.Forms.Button();
            this.lblSlNo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSuppliersName = new System.Windows.Forms.ComboBox();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBillingDescription = new System.Windows.Forms.TextBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPaymentAccount = new System.Windows.Forms.ComboBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.txtAdvanceAmount = new System.Windows.Forms.TextBox();
            this.pnlChequeDetails = new System.Windows.Forms.GroupBox();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.dtpDateCheque = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlChequeDetails.SuspendLayout();
            this.SuspendLayout();
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
            this.btnNewSupplier.Location = new System.Drawing.Point(730, 84);
            this.btnNewSupplier.Margin = new System.Windows.Forms.Padding(5);
            this.btnNewSupplier.Name = "btnNewSupplier";
            this.btnNewSupplier.Size = new System.Drawing.Size(29, 30);
            this.btnNewSupplier.TabIndex = 1;
            this.btnNewSupplier.UseVisualStyleBackColor = false;
            this.btnNewSupplier.Click += new System.EventHandler(this.btnNewSupplier_Click);
            // 
            // lblSlNo
            // 
            this.lblSlNo.AutoSize = true;
            this.lblSlNo.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlNo.Location = new System.Drawing.Point(254, 20);
            this.lblSlNo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSlNo.Name = "lblSlNo";
            this.lblSlNo.Size = new System.Drawing.Size(68, 27);
            this.lblSlNo.TabIndex = 11;
            this.lblSlNo.Text = "____";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(11, 18);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(232, 27);
            this.label10.TabIndex = 12;
            this.label10.Text = "Advance Payment  #";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "*Suppliers Name ";
            // 
            // cmbSuppliersName
            // 
            this.cmbSuppliersName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSuppliersName.FormattingEnabled = true;
            this.cmbSuppliersName.Location = new System.Drawing.Point(13, 88);
            this.cmbSuppliersName.Margin = new System.Windows.Forms.Padding(5);
            this.cmbSuppliersName.Name = "cmbSuppliersName";
            this.cmbSuppliersName.Size = new System.Drawing.Size(717, 24);
            this.cmbSuppliersName.TabIndex = 0;
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpBillDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(640, 40);
            this.dtpBillDate.Margin = new System.Windows.Forms.Padding(5);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(122, 22);
            this.dtpBillDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(529, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Peyment Date :";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(645, 309);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 37);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(499, 309);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 249);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Description (If any) ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Play", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(639, 196);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "ADVANCE AMOUNT ";
            // 
            // txtBillingDescription
            // 
            this.txtBillingDescription.Location = new System.Drawing.Point(13, 269);
            this.txtBillingDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtBillingDescription.MaxLength = 1800;
            this.txtBillingDescription.Multiline = true;
            this.txtBillingDescription.Name = "txtBillingDescription";
            this.txtBillingDescription.Size = new System.Drawing.Size(440, 58);
            this.txtBillingDescription.TabIndex = 4;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(702, 149);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(28, 16);
            this.lblBalance.TabIndex = 35;
            this.lblBalance.Text = "Rs.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 126);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 16);
            this.label8.TabIndex = 32;
            this.label8.Text = "Peyment Method :";
            // 
            // cmbPaymentAccount
            // 
            this.cmbPaymentAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentAccount.DropDownWidth = 521;
            this.cmbPaymentAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentAccount.FormattingEnabled = true;
            this.cmbPaymentAccount.ItemHeight = 16;
            this.cmbPaymentAccount.Location = new System.Drawing.Point(185, 146);
            this.cmbPaymentAccount.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentAccount.Name = "cmbPaymentAccount";
            this.cmbPaymentAccount.Size = new System.Drawing.Size(508, 24);
            this.cmbPaymentAccount.TabIndex = 3;
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
            "Cheque",
            "NEFT",
            "RTGS",
            "IMPS"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(16, 146);
            this.cmbPaymentMethod.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(159, 24);
            this.cmbPaymentMethod.TabIndex = 2;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            // 
            // txtAdvanceAmount
            // 
            this.txtAdvanceAmount.Location = new System.Drawing.Point(623, 217);
            this.txtAdvanceAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdvanceAmount.MaxLength = 1800;
            this.txtAdvanceAmount.Name = "txtAdvanceAmount";
            this.txtAdvanceAmount.Size = new System.Drawing.Size(161, 22);
            this.txtAdvanceAmount.TabIndex = 6;
            this.txtAdvanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlChequeDetails
            // 
            this.pnlChequeDetails.Controls.Add(this.txtChequeNo);
            this.pnlChequeDetails.Controls.Add(this.dtpDateCheque);
            this.pnlChequeDetails.Controls.Add(this.label2);
            this.pnlChequeDetails.Controls.Add(this.cmbBank);
            this.pnlChequeDetails.Controls.Add(this.label5);
            this.pnlChequeDetails.Controls.Add(this.label4);
            this.pnlChequeDetails.Location = new System.Drawing.Point(12, 176);
            this.pnlChequeDetails.Name = "pnlChequeDetails";
            this.pnlChequeDetails.Size = new System.Drawing.Size(592, 63);
            this.pnlChequeDetails.TabIndex = 36;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(443, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Issue Date";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(338, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cheque No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bank Name";
            // 
            // AdvancePaymentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 359);
            this.Controls.Add(this.pnlChequeDetails);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPaymentAccount);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtAdvanceAmount);
            this.Controls.Add(this.txtBillingDescription);
            this.Controls.Add(this.dtpBillDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNewSupplier);
            this.Controls.Add(this.lblSlNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSuppliersName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancePaymentWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancePaymentWindow_FormClosing);
            this.pnlChequeDetails.ResumeLayout(false);
            this.pnlChequeDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewSupplier;
        private System.Windows.Forms.Label lblSlNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSuppliersName;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBillingDescription;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPaymentAccount;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.TextBox txtAdvanceAmount;
        private System.Windows.Forms.GroupBox pnlChequeDetails;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.DateTimePicker dtpDateCheque;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}