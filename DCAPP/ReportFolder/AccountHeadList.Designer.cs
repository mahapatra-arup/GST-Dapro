namespace DAPRO
{
    partial class AccountHeadList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountHeadList));
            this.dgvLedgerAccount = new System.Windows.Forms.DataGridView();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Under = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnEditAccountHead = new System.Windows.Forms.Button();
            this.AddAccountHead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLedgerAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLedgerAccount
            // 
            this.dgvLedgerAccount.AllowUserToAddRows = false;
            this.dgvLedgerAccount.AllowUserToDeleteRows = false;
            this.dgvLedgerAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLedgerAccount.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLedgerAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgvLedgerAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLedgerAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLedgerAccount.ColumnHeadersHeight = 50;
            this.dgvLedgerAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLedgerAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerID,
            this.LedgerName,
            this.Under,
            this.debit,
            this.Credit,
            this.LinkColumn});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLedgerAccount.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLedgerAccount.EnableHeadersVisualStyles = false;
            this.dgvLedgerAccount.Location = new System.Drawing.Point(-1, 67);
            this.dgvLedgerAccount.MultiSelect = false;
            this.dgvLedgerAccount.Name = "dgvLedgerAccount";
            this.dgvLedgerAccount.RowHeadersVisible = false;
            this.dgvLedgerAccount.RowHeadersWidth = 20;
            this.dgvLedgerAccount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvLedgerAccount.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.dgvLedgerAccount.RowTemplate.Height = 33;
            this.dgvLedgerAccount.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLedgerAccount.Size = new System.Drawing.Size(784, 330);
            this.dgvLedgerAccount.TabIndex = 0;
            this.dgvLedgerAccount.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLedgerAccount_CellContentClick);
            // 
            // LedgerID
            // 
            this.LedgerID.HeaderText = "ID";
            this.LedgerID.Name = "LedgerID";
            this.LedgerID.Visible = false;
            // 
            // LedgerName
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.LedgerName.DefaultCellStyle = dataGridViewCellStyle2;
            this.LedgerName.HeaderText = "ACCOUNT";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Under
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Under.DefaultCellStyle = dataGridViewCellStyle3;
            this.Under.FillWeight = 30F;
            this.Under.HeaderText = "UNDER";
            this.Under.Name = "Under";
            this.Under.ReadOnly = true;
            this.Under.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // debit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.debit.DefaultCellStyle = dataGridViewCellStyle4;
            this.debit.FillWeight = 30F;
            this.debit.HeaderText = "DEBIT BALANCE";
            this.debit.Name = "debit";
            this.debit.ReadOnly = true;
            this.debit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Credit
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Credit.DefaultCellStyle = dataGridViewCellStyle5;
            this.Credit.FillWeight = 30F;
            this.Credit.HeaderText = "CREDIT BALANCE";
            this.Credit.Name = "Credit";
            this.Credit.ReadOnly = true;
            this.Credit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LinkColumn
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LinkColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.LinkColumn.FillWeight = 30F;
            this.LinkColumn.HeaderText = "";
            this.LinkColumn.Name = "LinkColumn";
            this.LinkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LinkColumn.Visible = false;
            // 
            // btnEditAccountHead
            // 
            this.btnEditAccountHead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditAccountHead.BackColor = System.Drawing.Color.Transparent;
            this.btnEditAccountHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditAccountHead.FlatAppearance.BorderSize = 0;
            this.btnEditAccountHead.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAccountHead.ForeColor = System.Drawing.Color.Maroon;
            this.btnEditAccountHead.Image = ((System.Drawing.Image)(resources.GetObject("btnEditAccountHead.Image")));
            this.btnEditAccountHead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditAccountHead.Location = new System.Drawing.Point(448, 27);
            this.btnEditAccountHead.Name = "btnEditAccountHead";
            this.btnEditAccountHead.Size = new System.Drawing.Size(149, 36);
            this.btnEditAccountHead.TabIndex = 101;
            this.btnEditAccountHead.Text = "&Edit";
            this.btnEditAccountHead.UseVisualStyleBackColor = false;
            this.btnEditAccountHead.Visible = false;
            this.btnEditAccountHead.Click += new System.EventHandler(this.btnEditAccountHead_Click);
            // 
            // AddAccountHead
            // 
            this.AddAccountHead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddAccountHead.BackColor = System.Drawing.Color.Transparent;
            this.AddAccountHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddAccountHead.FlatAppearance.BorderSize = 0;
            this.AddAccountHead.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddAccountHead.ForeColor = System.Drawing.Color.Maroon;
            this.AddAccountHead.Image = ((System.Drawing.Image)(resources.GetObject("AddAccountHead.Image")));
            this.AddAccountHead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddAccountHead.Location = new System.Drawing.Point(603, 27);
            this.AddAccountHead.Name = "AddAccountHead";
            this.AddAccountHead.Size = new System.Drawing.Size(176, 36);
            this.AddAccountHead.TabIndex = 100;
            this.AddAccountHead.Text = "    &New Account ";
            this.AddAccountHead.UseVisualStyleBackColor = false;
            this.AddAccountHead.Visible = false;
            this.AddAccountHead.Click += new System.EventHandler(this.AddAccountHead_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Play", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 102;
            this.label1.Text = "Account List";
            // 
            // AccountHeadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 396);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditAccountHead);
            this.Controls.Add(this.AddAccountHead);
            this.Controls.Add(this.dgvLedgerAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountHeadList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Others Ac Head";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.ShowOthersAcHead_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLedgerAccount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLedgerAccount;
        private System.Windows.Forms.Button btnEditAccountHead;
        private System.Windows.Forms.Button AddAccountHead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Under;
        private System.Windows.Forms.DataGridViewTextBoxColumn debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewLinkColumn LinkColumn;
    }
}