namespace DAPRO
{
    partial class StockList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockList));
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            this.btnStockUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerachByName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStockEntryManual = new System.Windows.Forms.Button();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.cmbGstRate = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtnExpiryOver = new System.Windows.Forms.RadioButton();
            this.rbtnExpiryAll = new System.Windows.Forms.RadioButton();
            this.rbtnExpiryIn = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnStockOut = new System.Windows.Forms.RadioButton();
            this.rbtnStockIn = new System.Windows.Forms.RadioButton();
            this.rbtnStockAll = new System.Windows.Forms.RadioButton();
            this.btnFilterOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.lblfilterString = new System.Windows.Forms.Label();
            this.lblFilterClear = new System.Windows.Forms.LinkLabel();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStockList
            // 
            this.dgvStockList.AllowUserToAddRows = false;
            this.dgvStockList.AllowUserToDeleteRows = false;
            this.dgvStockList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStockList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStockList.ColumnHeadersHeight = 40;
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStockList.EnableHeadersVisualStyles = false;
            this.dgvStockList.Location = new System.Drawing.Point(-1, 141);
            this.dgvStockList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvStockList.MultiSelect = false;
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.ReadOnly = true;
            this.dgvStockList.RowHeadersWidth = 20;
            this.dgvStockList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvStockList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockList.RowTemplate.Height = 3;
            this.dgvStockList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockList.Size = new System.Drawing.Size(992, 287);
            this.dgvStockList.TabIndex = 34;
            this.dgvStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellDoubleClick);
            // 
            // btnStockUpdate
            // 
            this.btnStockUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockUpdate.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnStockUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStockUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnStockUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockUpdate.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockUpdate.ForeColor = System.Drawing.Color.White;
            this.btnStockUpdate.Image = global::DAPRO.Properties.Resources.Refresh24;
            this.btnStockUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockUpdate.Location = new System.Drawing.Point(693, 102);
            this.btnStockUpdate.Margin = new System.Windows.Forms.Padding(5);
            this.btnStockUpdate.Name = "btnStockUpdate";
            this.btnStockUpdate.Size = new System.Drawing.Size(143, 34);
            this.btnStockUpdate.TabIndex = 35;
            this.btnStockUpdate.Text = "&Stock Update";
            this.btnStockUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStockUpdate.UseVisualStyleBackColor = false;
            this.btnStockUpdate.Click += new System.EventHandler(this.btnAddServicesAndGoods_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Play", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 31);
            this.label1.TabIndex = 36;
            this.label1.Text = "Current Stock ";
            // 
            // txtSerachByName
            // 
            this.txtSerachByName.BackColor = System.Drawing.Color.White;
            this.txtSerachByName.Location = new System.Drawing.Point(14, 112);
            this.txtSerachByName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerachByName.Name = "txtSerachByName";
            this.txtSerachByName.Size = new System.Drawing.Size(399, 22);
            this.txtSerachByName.TabIndex = 37;
            this.txtSerachByName.TextChanged += new System.EventHandler(this.txtSerachByName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Product Name ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 40;
            this.label3.Text = "Category";
            // 
            // btnStockEntryManual
            // 
            this.btnStockEntryManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockEntryManual.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnStockEntryManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStockEntryManual.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnStockEntryManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockEntryManual.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockEntryManual.ForeColor = System.Drawing.Color.White;
            this.btnStockEntryManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockEntryManual.Location = new System.Drawing.Point(841, 102);
            this.btnStockEntryManual.Margin = new System.Windows.Forms.Padding(5);
            this.btnStockEntryManual.Name = "btnStockEntryManual";
            this.btnStockEntryManual.Size = new System.Drawing.Size(143, 34);
            this.btnStockEntryManual.TabIndex = 35;
            this.btnStockEntryManual.Text = "&Manual Stock Entry";
            this.btnStockEntryManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStockEntryManual.UseVisualStyleBackColor = false;
            this.btnStockEntryManual.Click += new System.EventHandler(this.btnStockEntryManual_Click);
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSubCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCategory.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubCategory.FormattingEnabled = true;
            this.cmbSubCategory.Location = new System.Drawing.Point(10, 77);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(336, 24);
            this.cmbSubCategory.TabIndex = 42;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(10, 31);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(336, 24);
            this.cmbCategory.TabIndex = 41;
            // 
            // cmbCompany
            // 
            this.cmbCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(10, 125);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(336, 24);
            this.cmbCompany.TabIndex = 42;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.cmbGstRate);
            this.pnlFilter.Controls.Add(this.panel3);
            this.pnlFilter.Controls.Add(this.panel2);
            this.pnlFilter.Controls.Add(this.btnFilterOk);
            this.pnlFilter.Controls.Add(this.cmbCompany);
            this.pnlFilter.Controls.Add(this.label5);
            this.pnlFilter.Controls.Add(this.label4);
            this.pnlFilter.Controls.Add(this.label6);
            this.pnlFilter.Controls.Add(this.label3);
            this.pnlFilter.Controls.Add(this.cmbCategory);
            this.pnlFilter.Controls.Add(this.cmbSubCategory);
            this.pnlFilter.Location = new System.Drawing.Point(7, 78);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(360, 278);
            this.pnlFilter.TabIndex = 43;
            this.pnlFilter.Visible = false;
            // 
            // cmbGstRate
            // 
            this.cmbGstRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGstRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGstRate.FormattingEnabled = true;
            this.cmbGstRate.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "12",
            "18",
            "28",
            "Exampted",
            "Non GST",
            "Nil"});
            this.cmbGstRate.Location = new System.Drawing.Point(84, 239);
            this.cmbGstRate.Name = "cmbGstRate";
            this.cmbGstRate.Size = new System.Drawing.Size(128, 24);
            this.cmbGstRate.TabIndex = 46;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbtnExpiryOver);
            this.panel3.Controls.Add(this.rbtnExpiryAll);
            this.panel3.Controls.Add(this.rbtnExpiryIn);
            this.panel3.Location = new System.Drawing.Point(5, 193);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(341, 37);
            this.panel3.TabIndex = 45;
            // 
            // rbtnExpiryOver
            // 
            this.rbtnExpiryOver.AutoSize = true;
            this.rbtnExpiryOver.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnExpiryOver.Location = new System.Drawing.Point(205, 8);
            this.rbtnExpiryOver.Name = "rbtnExpiryOver";
            this.rbtnExpiryOver.Size = new System.Drawing.Size(118, 20);
            this.rbtnExpiryOver.TabIndex = 43;
            this.rbtnExpiryOver.TabStop = true;
            this.rbtnExpiryOver.Text = "Over Expiry Date";
            this.rbtnExpiryOver.UseVisualStyleBackColor = true;
            // 
            // rbtnExpiryAll
            // 
            this.rbtnExpiryAll.AutoSize = true;
            this.rbtnExpiryAll.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnExpiryAll.Location = new System.Drawing.Point(9, 8);
            this.rbtnExpiryAll.Name = "rbtnExpiryAll";
            this.rbtnExpiryAll.Size = new System.Drawing.Size(40, 20);
            this.rbtnExpiryAll.TabIndex = 43;
            this.rbtnExpiryAll.TabStop = true;
            this.rbtnExpiryAll.Text = "All";
            this.rbtnExpiryAll.UseVisualStyleBackColor = true;
            // 
            // rbtnExpiryIn
            // 
            this.rbtnExpiryIn.AutoSize = true;
            this.rbtnExpiryIn.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnExpiryIn.Location = new System.Drawing.Point(75, 8);
            this.rbtnExpiryIn.Name = "rbtnExpiryIn";
            this.rbtnExpiryIn.Size = new System.Drawing.Size(102, 20);
            this.rbtnExpiryIn.TabIndex = 43;
            this.rbtnExpiryIn.TabStop = true;
            this.rbtnExpiryIn.Text = "In Expiry Date";
            this.rbtnExpiryIn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnStockOut);
            this.panel2.Controls.Add(this.rbtnStockIn);
            this.panel2.Controls.Add(this.rbtnStockAll);
            this.panel2.Location = new System.Drawing.Point(5, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 37);
            this.panel2.TabIndex = 44;
            // 
            // rbtnStockOut
            // 
            this.rbtnStockOut.AutoSize = true;
            this.rbtnStockOut.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnStockOut.Location = new System.Drawing.Point(205, 6);
            this.rbtnStockOut.Name = "rbtnStockOut";
            this.rbtnStockOut.Size = new System.Drawing.Size(91, 20);
            this.rbtnStockOut.TabIndex = 43;
            this.rbtnStockOut.TabStop = true;
            this.rbtnStockOut.Text = "Out of Stock";
            this.rbtnStockOut.UseVisualStyleBackColor = true;
            // 
            // rbtnStockIn
            // 
            this.rbtnStockIn.AutoSize = true;
            this.rbtnStockIn.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnStockIn.Location = new System.Drawing.Point(75, 6);
            this.rbtnStockIn.Name = "rbtnStockIn";
            this.rbtnStockIn.Size = new System.Drawing.Size(69, 20);
            this.rbtnStockIn.TabIndex = 43;
            this.rbtnStockIn.TabStop = true;
            this.rbtnStockIn.Text = "In Stock";
            this.rbtnStockIn.UseVisualStyleBackColor = true;
            // 
            // rbtnStockAll
            // 
            this.rbtnStockAll.AutoSize = true;
            this.rbtnStockAll.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnStockAll.Location = new System.Drawing.Point(9, 6);
            this.rbtnStockAll.Name = "rbtnStockAll";
            this.rbtnStockAll.Size = new System.Drawing.Size(40, 20);
            this.rbtnStockAll.TabIndex = 43;
            this.rbtnStockAll.TabStop = true;
            this.rbtnStockAll.Text = "All";
            this.rbtnStockAll.UseVisualStyleBackColor = true;
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
            this.btnFilterOk.Location = new System.Drawing.Point(257, 235);
            this.btnFilterOk.Margin = new System.Windows.Forms.Padding(5);
            this.btnFilterOk.Name = "btnFilterOk";
            this.btnFilterOk.Size = new System.Drawing.Size(89, 31);
            this.btnFilterOk.TabIndex = 35;
            this.btnFilterOk.Text = "     &OK";
            this.btnFilterOk.UseVisualStyleBackColor = false;
            this.btnFilterOk.Click += new System.EventHandler(this.btnFilterOk_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 106);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 40;
            this.label5.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 40;
            this.label4.Text = "Sub Category";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 245);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 40;
            this.label6.Text = "GST Rate :";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(-2, 54);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(78, 27);
            this.toolStrip1.TabIndex = 44;
            this.toolStrip1.Text = "toolStrip1";
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
            // lblfilterString
            // 
            this.lblfilterString.AutoSize = true;
            this.lblfilterString.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilterString.Location = new System.Drawing.Point(86, 59);
            this.lblfilterString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfilterString.Name = "lblfilterString";
            this.lblfilterString.Size = new System.Drawing.Size(41, 20);
            this.lblfilterString.TabIndex = 39;
            this.lblfilterString.Text = "Filter";
            this.lblfilterString.Click += new System.EventHandler(this.lblfilterString_Click);
            // 
            // lblFilterClear
            // 
            this.lblFilterClear.AutoSize = true;
            this.lblFilterClear.Location = new System.Drawing.Point(140, 61);
            this.lblFilterClear.Name = "lblFilterClear";
            this.lblFilterClear.Size = new System.Drawing.Size(72, 16);
            this.lblFilterClear.TabIndex = 45;
            this.lblFilterClear.TabStop = true;
            this.lblFilterClear.Text = "Clear Filter";
            this.lblFilterClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFilter_LinkClicked);
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
            this.btnExport.Location = new System.Drawing.Point(870, 66);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 31);
            this.btnExport.TabIndex = 46;
            this.btnExport.Text = "Export Excel";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // StockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 427);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblfilterString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSerachByName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStockEntryManual);
            this.Controls.Add(this.btnStockUpdate);
            this.Controls.Add(this.dgvStockList);
            this.Controls.Add(this.lblFilterClear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GOODS AND SERVICES";
            this.Shown += new System.EventHandler(this.ItemListView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.Button btnStockUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerachByName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStockEntryManual;
        private System.Windows.Forms.ComboBox cmbSubCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnStockOut;
        private System.Windows.Forms.RadioButton rbtnStockIn;
        private System.Windows.Forms.RadioButton rbtnStockAll;
        private System.Windows.Forms.RadioButton rbtnExpiryOver;
        private System.Windows.Forms.RadioButton rbtnExpiryAll;
        private System.Windows.Forms.RadioButton rbtnExpiryIn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFilterOk;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label lblfilterString;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lblFilterClear;
        private System.Windows.Forms.ComboBox cmbGstRate;
        private System.Windows.Forms.Button btnExport;
    }
}