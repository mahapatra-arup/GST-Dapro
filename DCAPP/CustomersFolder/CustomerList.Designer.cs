﻿namespace DAPRO
{
    partial class CustomerList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSuppliersDetails = new System.Windows.Forms.DataGridView();
            this.lblFilterHeader = new System.Windows.Forms.Label();
            this.AddSuppliersDetails = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripToday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreviousMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentFinYear = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliersDetails)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgvSuppliersDetails);
            this.panel1.Controls.Add(this.lblFilterHeader);
            this.panel1.Controls.Add(this.AddSuppliersDetails);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 359);
            this.panel1.TabIndex = 107;
            // 
            // dgvSuppliersDetails
            // 
            this.dgvSuppliersDetails.AllowUserToAddRows = false;
            this.dgvSuppliersDetails.AllowUserToDeleteRows = false;
            this.dgvSuppliersDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSuppliersDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliersDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSuppliersDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvSuppliersDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSuppliersDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSuppliersDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSuppliersDetails.ColumnHeadersHeight = 50;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSuppliersDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSuppliersDetails.Location = new System.Drawing.Point(0, 45);
            this.dgvSuppliersDetails.Name = "dgvSuppliersDetails";
            this.dgvSuppliersDetails.ReadOnly = true;
            this.dgvSuppliersDetails.RowHeadersWidth = 30;
            this.dgvSuppliersDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSuppliersDetails.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.dgvSuppliersDetails.RowTemplate.Height = 45;
            this.dgvSuppliersDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSuppliersDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSuppliersDetails.Size = new System.Drawing.Size(736, 311);
            this.dgvSuppliersDetails.TabIndex = 97;
            this.dgvSuppliersDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuppliersDetails_CellClick);
            this.dgvSuppliersDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuppliersDetails_CellContentClick);
            this.dgvSuppliersDetails.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuppliersDetails_CellMouseEnter);
            this.dgvSuppliersDetails.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuppliersDetails_CellMouseLeave);
            // 
            // lblFilterHeader
            // 
            this.lblFilterHeader.AutoSize = true;
            this.lblFilterHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterHeader.Location = new System.Drawing.Point(12, 17);
            this.lblFilterHeader.Name = "lblFilterHeader";
            this.lblFilterHeader.Size = new System.Drawing.Size(29, 16);
            this.lblFilterHeader.TabIndex = 102;
            this.lblFilterHeader.Text = "___";
            this.lblFilterHeader.Visible = false;
            // 
            // AddSuppliersDetails
            // 
            this.AddSuppliersDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSuppliersDetails.BackColor = System.Drawing.Color.Transparent;
            this.AddSuppliersDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddSuppliersDetails.FlatAppearance.BorderSize = 0;
            this.AddSuppliersDetails.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddSuppliersDetails.ForeColor = System.Drawing.Color.Maroon;
            this.AddSuppliersDetails.Image = ((System.Drawing.Image)(resources.GetObject("AddSuppliersDetails.Image")));
            this.AddSuppliersDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddSuppliersDetails.Location = new System.Drawing.Point(585, 6);
            this.AddSuppliersDetails.Name = "AddSuppliersDetails";
            this.AddSuppliersDetails.Size = new System.Drawing.Size(149, 36);
            this.AddSuppliersDetails.TabIndex = 104;
            this.AddSuppliersDetails.Text = "    &New Customer";
            this.AddSuppliersDetails.UseVisualStyleBackColor = false;
            this.AddSuppliersDetails.Click += new System.EventHandler(this.AddSuppliersDetails_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(12, 12);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(78, 25);
            this.toolStrip1.TabIndex = 101;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
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
            // 
            // toolStripCurrentMonth
            // 
            this.toolStripCurrentMonth.Name = "toolStripCurrentMonth";
            this.toolStripCurrentMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentMonth.Text = "Current Month";
            // 
            // toolStripPreviousMonth
            // 
            this.toolStripPreviousMonth.Name = "toolStripPreviousMonth";
            this.toolStripPreviousMonth.Size = new System.Drawing.Size(171, 22);
            this.toolStripPreviousMonth.Text = "Previous Month";
            // 
            // toolStripCurrentFinYear
            // 
            this.toolStripCurrentFinYear.Name = "toolStripCurrentFinYear";
            this.toolStripCurrentFinYear.Size = new System.Drawing.Size(171, 22);
            this.toolStripCurrentFinYear.Text = "Current Fin. Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Play", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(326, 27);
            this.label2.TabIndex = 108;
            this.label2.Text = "Customers  [Sundry Debtors]";
            // 
            // CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(736, 398);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Customer List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.CustomerWindow_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliersDetails)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvSuppliersDetails;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripToday;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreviousMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripCurrentFinYear;
        private System.Windows.Forms.Button AddSuppliersDetails;
        private System.Windows.Forms.Label label2;
    }
}