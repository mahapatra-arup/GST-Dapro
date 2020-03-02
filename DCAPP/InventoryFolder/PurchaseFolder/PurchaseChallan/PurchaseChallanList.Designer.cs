namespace DAPRO
{
    partial class PurchaseChallanList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvChallanList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearchByName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFilterHeader = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallanList)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvChallanList
            // 
            this.dgvChallanList.AllowUserToAddRows = false;
            this.dgvChallanList.AllowUserToDeleteRows = false;
            this.dgvChallanList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChallanList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChallanList.BackgroundColor = System.Drawing.Color.White;
            this.dgvChallanList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChallanList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChallanList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvChallanList.ColumnHeadersHeight = 45;
            this.dgvChallanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChallanList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvChallanList.Location = new System.Drawing.Point(1, 85);
            this.dgvChallanList.Name = "dgvChallanList";
            this.dgvChallanList.ReadOnly = true;
            this.dgvChallanList.RowHeadersVisible = false;
            this.dgvChallanList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.dgvChallanList.RowTemplate.Height = 40;
            this.dgvChallanList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChallanList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvChallanList.Size = new System.Drawing.Size(813, 225);
            this.dgvChallanList.TabIndex = 106;
            this.dgvChallanList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChallanList_CellContentClick);
            this.dgvChallanList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChallanList_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cmbFilterBy);
            this.panel1.Controls.Add(this.txtSearchByName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dgvChallanList);
            this.panel1.Controls.Add(this.lblFilterHeader);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(1, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 316);
            this.panel1.TabIndex = 106;
            // 
            // txtSearchByName
            // 
            this.txtSearchByName.Location = new System.Drawing.Point(0, 62);
            this.txtSearchByName.Name = "txtSearchByName";
            this.txtSearchByName.Size = new System.Drawing.Size(657, 20);
            this.txtSearchByName.TabIndex = 107;
            this.txtSearchByName.TextChanged += new System.EventHandler(this.txtSearchByName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Search By Name";
            // 
            // lblFilterHeader
            // 
            this.lblFilterHeader.AutoSize = true;
            this.lblFilterHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterHeader.Location = new System.Drawing.Point(102, 15);
            this.lblFilterHeader.Name = "lblFilterHeader";
            this.lblFilterHeader.Size = new System.Drawing.Size(29, 16);
            this.lblFilterHeader.TabIndex = 105;
            this.lblFilterHeader.Text = "___";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(12, 10);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(78, 25);
            this.toolStrip1.TabIndex = 104;
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
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(66, 22);
            this.toolStripDropDownButton1.Text = "Filter By";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripToday
            // 
            this.toolStripToday.Name = "toolStripToday";
            this.toolStripToday.Size = new System.Drawing.Size(171, 22);
            this.toolStripToday.Text = "Today";
            this.toolStripToday.Click += new System.EventHandler(this.toolStripToday_Click);
            // 
            // toolStripCurrentMonth
            // 
            this.toolStripCurrentMonth.Name = "toolStripCurrentMonth";
            this.toolStripCurrentMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentMonth.Text = "Current Month";
            this.toolStripCurrentMonth.Click += new System.EventHandler(this.toolStripCurrentMonth_Click);
            // 
            // toolStripPreviousMonth
            // 
            this.toolStripPreviousMonth.Name = "toolStripPreviousMonth";
            this.toolStripPreviousMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripPreviousMonth.Text = "Previous Month";
            this.toolStripPreviousMonth.Click += new System.EventHandler(this.toolStripPreviousMonth_Click);
            // 
            // toolStripCurrentFinYear
            // 
            this.toolStripCurrentFinYear.Name = "toolStripCurrentFinYear";
            this.toolStripCurrentFinYear.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentFinYear.Text = "Current Fin. Year";
            this.toolStripCurrentFinYear.Click += new System.EventHandler(this.toolStripCurrentFinYear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 29);
            this.label1.TabIndex = 107;
            this.label1.Text = "CHALLAN LIST";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "Issue Challan",
            "Direct Invoice"});
            this.cmbFilterBy.Location = new System.Drawing.Point(664, 60);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(141, 21);
            this.cmbFilterBy.TabIndex = 109;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(662, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "Filter By ";
            // 
            // PurchaseChallanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 386);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseChallanList";
            this.Text = "ChallanList";
            this.Load += new System.EventHandler(this.ChallanList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallanList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvChallanList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearchByName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label2;
    }
}