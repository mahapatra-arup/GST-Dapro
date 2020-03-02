namespace DAPRO
{
    partial class BankTransection
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
            this.rbtnDeposit = new System.Windows.Forms.RadioButton();
            this.rbtnWithdrawal = new System.Windows.Forms.RadioButton();
            this.cmbCashLedger = new System.Windows.Forms.ComboBox();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.dtpDateCheque = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBankLedger = new System.Windows.Forms.Label();
            this.pnlWithdrwal = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblJurnalNo = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlWithdrwal.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtnDeposit
            // 
            this.rbtnDeposit.AutoSize = true;
            this.rbtnDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeposit.Location = new System.Drawing.Point(26, 92);
            this.rbtnDeposit.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnDeposit.Name = "rbtnDeposit";
            this.rbtnDeposit.Size = new System.Drawing.Size(73, 20);
            this.rbtnDeposit.TabIndex = 0;
            this.rbtnDeposit.TabStop = true;
            this.rbtnDeposit.Text = "Deposit";
            this.rbtnDeposit.UseVisualStyleBackColor = true;
            this.rbtnDeposit.CheckedChanged += new System.EventHandler(this.rbtnDeposit_CheckedChanged);
            // 
            // rbtnWithdrawal
            // 
            this.rbtnWithdrawal.AutoSize = true;
            this.rbtnWithdrawal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnWithdrawal.Location = new System.Drawing.Point(157, 92);
            this.rbtnWithdrawal.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnWithdrawal.Name = "rbtnWithdrawal";
            this.rbtnWithdrawal.Size = new System.Drawing.Size(92, 20);
            this.rbtnWithdrawal.TabIndex = 1;
            this.rbtnWithdrawal.TabStop = true;
            this.rbtnWithdrawal.Text = "Withdrawal";
            this.rbtnWithdrawal.UseVisualStyleBackColor = true;
            // 
            // cmbCashLedger
            // 
            this.cmbCashLedger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashLedger.DropDownWidth = 521;
            this.cmbCashLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCashLedger.FormattingEnabled = true;
            this.cmbCashLedger.ItemHeight = 16;
            this.cmbCashLedger.Location = new System.Drawing.Point(26, 122);
            this.cmbCashLedger.Margin = new System.Windows.Forms.Padding(5);
            this.cmbCashLedger.Name = "cmbCashLedger";
            this.cmbCashLedger.Size = new System.Drawing.Size(583, 24);
            this.cmbCashLedger.TabIndex = 2;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChequeNo.Location = new System.Drawing.Point(9, 35);
            this.txtChequeNo.MaxLength = 6;
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(109, 22);
            this.txtChequeNo.TabIndex = 0;
            this.txtChequeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpDateCheque
            // 
            this.dtpDateCheque.CustomFormat = "dd-MMM-yyyy";
            this.dtpDateCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateCheque.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateCheque.Location = new System.Drawing.Point(122, 35);
            this.dtpDateCheque.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDateCheque.Name = "dtpDateCheque";
            this.dtpDateCheque.Size = new System.Drawing.Size(130, 22);
            this.dtpDateCheque.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(119, 15);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 237;
            this.label11.Text = "Issue Date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 15);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 16);
            this.label12.TabIndex = 238;
            this.label12.Text = "Cheque No";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(488, 293);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(367, 293);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 37);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(346, 167);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 18);
            this.label16.TabIndex = 244;
            this.label16.Text = "Amount :";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(421, 165);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.MaxLength = 1800;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(188, 22);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescription.Location = new System.Drawing.Point(26, 253);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(320, 39);
            this.txtDescription.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 233);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 16);
            this.label4.TabIndex = 246;
            this.label4.Text = "Narration (if any max 500 characters)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 18);
            this.label1.TabIndex = 238;
            this.label1.Text = "A/C :";
            // 
            // lblBankLedger
            // 
            this.lblBankLedger.AutoSize = true;
            this.lblBankLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankLedger.Location = new System.Drawing.Point(67, 53);
            this.lblBankLedger.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBankLedger.Name = "lblBankLedger";
            this.lblBankLedger.Size = new System.Drawing.Size(80, 18);
            this.lblBankLedger.TabIndex = 238;
            this.lblBankLedger.Text = "_________";
            // 
            // pnlWithdrwal
            // 
            this.pnlWithdrwal.Controls.Add(this.txtChequeNo);
            this.pnlWithdrwal.Controls.Add(this.dtpDateCheque);
            this.pnlWithdrwal.Controls.Add(this.label11);
            this.pnlWithdrwal.Controls.Add(this.label12);
            this.pnlWithdrwal.Location = new System.Drawing.Point(17, 165);
            this.pnlWithdrwal.Name = "pnlWithdrwal";
            this.pnlWithdrwal.Size = new System.Drawing.Size(281, 68);
            this.pnlWithdrwal.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 238;
            this.label2.Text = "SL NO#";
            // 
            // lblJurnalNo
            // 
            this.lblJurnalNo.AutoSize = true;
            this.lblJurnalNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJurnalNo.Location = new System.Drawing.Point(116, 15);
            this.lblJurnalNo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblJurnalNo.Name = "lblJurnalNo";
            this.lblJurnalNo.Size = new System.Drawing.Size(80, 18);
            this.lblJurnalNo.TabIndex = 238;
            this.lblJurnalNo.Text = "_________";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(477, 15);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(132, 22);
            this.dtpDate.TabIndex = 247;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 248;
            this.label3.Text = "Dated :";
            // 
            // BankTransection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 343);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlWithdrwal);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblJurnalNo);
            this.Controls.Add(this.lblBankLedger);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCashLedger);
            this.Controls.Add(this.rbtnWithdrawal);
            this.Controls.Add(this.rbtnDeposit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BankTransection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Transection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BankTransection_FormClosing);
            this.Shown += new System.EventHandler(this.BankTransection_Shown);
            this.pnlWithdrwal.ResumeLayout(false);
            this.pnlWithdrwal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnDeposit;
        private System.Windows.Forms.RadioButton rbtnWithdrawal;
        private System.Windows.Forms.ComboBox cmbCashLedger;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.DateTimePicker dtpDateCheque;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBankLedger;
        private System.Windows.Forms.Panel pnlWithdrwal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblJurnalNo;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
    }
}