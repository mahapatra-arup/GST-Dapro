namespace DAPRO
{
    partial class EstimateList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstimateList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.txtEstimateNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlCustomDate = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtmStartDate = new System.Windows.Forms.DateTimePicker();
            this.rbtnEstimateDate = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilterHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFilterOk = new System.Windows.Forms.Button();
            this.lblfilterString = new System.Windows.Forms.Label();
            this.lblFilterClear = new System.Windows.Forms.LinkLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dgvEstimate = new System.Windows.Forms.DataGridView();
            this.btnNewEstimate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCustomDate.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstimate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.pnlFilter);
            this.panel1.Controls.Add(this.lblfilterString);
            this.panel1.Controls.Add(this.lblFilterClear);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Controls.Add(this.dgvEstimate);
            this.panel1.Controls.Add(this.btnNewEstimate);
            this.panel1.Location = new System.Drawing.Point(1, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(859, 289);
            this.panel1.TabIndex = 104;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.txtPartyName);
            this.pnlFilter.Controls.Add(this.txtEstimateNo);
            this.pnlFilter.Controls.Add(this.label4);
            this.pnlFilter.Controls.Add(this.label3);
            this.pnlFilter.Controls.Add(this.cmbFilterBy);
            this.pnlFilter.Controls.Add(this.panel2);
            this.pnlFilter.Controls.Add(this.label2);
            this.pnlFilter.Controls.Add(this.btnFilterOk);
            this.pnlFilter.Location = new System.Drawing.Point(1, 47);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(360, 172);
            this.pnlFilter.TabIndex = 115;
            this.pnlFilter.Visible = false;
            // 
            // txtPartyName
            // 
            this.txtPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPartyName.Location = new System.Drawing.Point(81, 106);
            this.txtPartyName.MaxLength = 50;
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.Size = new System.Drawing.Size(264, 20);
            this.txtPartyName.TabIndex = 2;
            // 
            // txtEstimateNo
            // 
            this.txtEstimateNo.Location = new System.Drawing.Point(81, 76);
            this.txtEstimateNo.MaxLength = 50;
            this.txtEstimateNo.Name = "txtEstimateNo";
            this.txtEstimateNo.Size = new System.Drawing.Size(264, 20);
            this.txtEstimateNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Party Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Estimate No :";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "All",
            "Date Over",
            "Date In"});
            this.cmbFilterBy.Location = new System.Drawing.Point(81, 136);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(123, 21);
            this.cmbFilterBy.TabIndex = 109;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlCustomDate);
            this.panel2.Controls.Add(this.rbtnEstimateDate);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.lblFilterHeader);
            this.panel2.Location = new System.Drawing.Point(2, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 60);
            this.panel2.TabIndex = 0;
            // 
            // pnlCustomDate
            // 
            this.pnlCustomDate.Controls.Add(this.label5);
            this.pnlCustomDate.Controls.Add(this.dtmEndDate);
            this.pnlCustomDate.Controls.Add(this.dtmStartDate);
            this.pnlCustomDate.Location = new System.Drawing.Point(86, 22);
            this.pnlCustomDate.Name = "pnlCustomDate";
            this.pnlCustomDate.Size = new System.Drawing.Size(264, 29);
            this.pnlCustomDate.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 102;
            this.label5.Text = "To";
            // 
            // dtmEndDate
            // 
            this.dtmEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmEndDate.Location = new System.Drawing.Point(141, 5);
            this.dtmEndDate.Name = "dtmEndDate";
            this.dtmEndDate.Size = new System.Drawing.Size(113, 20);
            this.dtmEndDate.TabIndex = 1;
            this.dtmEndDate.ValueChanged += new System.EventHandler(this.dtmEndDate_ValueChanged);
            // 
            // dtmStartDate
            // 
            this.dtmStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmStartDate.Location = new System.Drawing.Point(3, 5);
            this.dtmStartDate.Name = "dtmStartDate";
            this.dtmStartDate.Size = new System.Drawing.Size(113, 20);
            this.dtmStartDate.TabIndex = 0;
            this.dtmStartDate.ValueChanged += new System.EventHandler(this.dtmStartDate_ValueChanged);
            // 
            // rbtnEstimateDate
            // 
            this.rbtnEstimateDate.AutoSize = true;
            this.rbtnEstimateDate.Location = new System.Drawing.Point(13, 3);
            this.rbtnEstimateDate.Name = "rbtnEstimateDate";
            this.rbtnEstimateDate.Size = new System.Drawing.Size(91, 17);
            this.rbtnEstimateDate.TabIndex = 0;
            this.rbtnEstimateDate.TabStop = true;
            this.rbtnEstimateDate.Text = "Estimate Date";
            this.rbtnEstimateDate.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(6, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(78, 25);
            this.toolStrip1.TabIndex = 2;
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
            this.toolStripCurrentFinYear,
            this.customToolStripMenuItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.customToolStripMenuItem.Text = "Custom Date";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // lblFilterHeader
            // 
            this.lblFilterHeader.AutoSize = true;
            this.lblFilterHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterHeader.Location = new System.Drawing.Point(87, 28);
            this.lblFilterHeader.Name = "lblFilterHeader";
            this.lblFilterHeader.Size = new System.Drawing.Size(29, 16);
            this.lblFilterHeader.TabIndex = 100;
            this.lblFilterHeader.Text = "___";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "Date In :";
            // 
            // btnFilterOk
            // 
            this.btnFilterOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterOk.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFilterOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFilterOk.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFilterOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterOk.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterOk.ForeColor = System.Drawing.Color.White;
            this.btnFilterOk.Image = global::DAPRO.Properties.Resources.Refresh24;
            this.btnFilterOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterOk.Location = new System.Drawing.Point(240, 129);
            this.btnFilterOk.Margin = new System.Windows.Forms.Padding(5);
            this.btnFilterOk.Name = "btnFilterOk";
            this.btnFilterOk.Size = new System.Drawing.Size(102, 31);
            this.btnFilterOk.TabIndex = 4;
            this.btnFilterOk.Text = "     &OK";
            this.btnFilterOk.UseVisualStyleBackColor = false;
            this.btnFilterOk.Click += new System.EventHandler(this.btnFilterOk_Click);
            // 
            // lblfilterString
            // 
            this.lblfilterString.AutoSize = true;
            this.lblfilterString.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilterString.Location = new System.Drawing.Point(98, 21);
            this.lblfilterString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfilterString.Name = "lblfilterString";
            this.lblfilterString.Size = new System.Drawing.Size(41, 20);
            this.lblfilterString.TabIndex = 116;
            this.lblfilterString.Text = "Filter";
            // 
            // lblFilterClear
            // 
            this.lblFilterClear.AutoSize = true;
            this.lblFilterClear.Location = new System.Drawing.Point(152, 23);
            this.lblFilterClear.Name = "lblFilterClear";
            this.lblFilterClear.Size = new System.Drawing.Size(79, 13);
            this.lblFilterClear.TabIndex = 114;
            this.lblFilterClear.TabStop = true;
            this.lblFilterClear.Text = "Set Defalt Filter";
            this.lblFilterClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFilterClear_LinkClicked);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(8, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(78, 27);
            this.toolStrip2.TabIndex = 113;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Checked = true;
            this.toolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 24);
            this.toolStripButton1.Text = "Filter by";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // dgvEstimate
            // 
            this.dgvEstimate.AllowUserToAddRows = false;
            this.dgvEstimate.AllowUserToDeleteRows = false;
            this.dgvEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEstimate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstimate.BackgroundColor = System.Drawing.Color.White;
            this.dgvEstimate.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEstimate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEstimate.ColumnHeadersHeight = 45;
            this.dgvEstimate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEstimate.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEstimate.Location = new System.Drawing.Point(-1, 46);
            this.dgvEstimate.Name = "dgvEstimate";
            this.dgvEstimate.ReadOnly = true;
            this.dgvEstimate.RowHeadersVisible = false;
            this.dgvEstimate.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.dgvEstimate.RowTemplate.Height = 40;
            this.dgvEstimate.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEstimate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEstimate.Size = new System.Drawing.Size(858, 240);
            this.dgvEstimate.TabIndex = 106;
            this.dgvEstimate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstimate_CellContentClick);
            this.dgvEstimate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstimate_CellDoubleClick);
            // 
            // btnNewEstimate
            // 
            this.btnNewEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewEstimate.BackColor = System.Drawing.Color.Transparent;
            this.btnNewEstimate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewEstimate.FlatAppearance.BorderSize = 0;
            this.btnNewEstimate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewEstimate.ForeColor = System.Drawing.Color.Maroon;
            this.btnNewEstimate.Image = ((System.Drawing.Image)(resources.GetObject("btnNewEstimate.Image")));
            this.btnNewEstimate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewEstimate.Location = new System.Drawing.Point(580, 4);
            this.btnNewEstimate.Name = "btnNewEstimate";
            this.btnNewEstimate.Size = new System.Drawing.Size(149, 36);
            this.btnNewEstimate.TabIndex = 100;
            this.btnNewEstimate.Text = "     &NEW ESTIMATE";
            this.btnNewEstimate.UseVisualStyleBackColor = false;
            this.btnNewEstimate.Click += new System.EventHandler(this.btnNewEstimate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 27);
            this.label1.TabIndex = 105;
            this.label1.Text = "Sales Estimate History\r\n";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.SystemColors.Control;
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(732, 7);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 31);
            this.btnExport.TabIndex = 117;
            this.btnExport.Text = "Export Excel";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // EstimateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(857, 334);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "EstimateList";
            this.Text = "EstimateList";
            this.Load += new System.EventHandler(this.EstimateList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlCustomDate.ResumeLayout(false);
            this.pnlCustomDate.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstimate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvEstimate;
        private System.Windows.Forms.Button btnNewEstimate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.TextBox txtPartyName;
        private System.Windows.Forms.TextBox txtEstimateNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlCustomDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtmEndDate;
        private System.Windows.Forms.DateTimePicker dtmStartDate;
        private System.Windows.Forms.RadioButton rbtnEstimateDate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.Button btnFilterOk;
        private System.Windows.Forms.Label lblfilterString;
        private System.Windows.Forms.LinkLabel lblFilterClear;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button btnExport;
    }
}