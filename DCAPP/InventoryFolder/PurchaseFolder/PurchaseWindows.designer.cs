namespace DAPRO
{
    partial class PurchaseWindows
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPurchaseReturn = new System.Windows.Forms.Button();
            this.btnBillEntry = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.btnAdvancePayment = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btnAdvancePayment);
            this.panel1.Controls.Add(this.btnPurchaseReturn);
            this.panel1.Controls.Add(this.btnBillEntry);
            this.panel1.Controls.Add(this.btnOrder);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 43);
            this.panel1.TabIndex = 0;
            // 
            // btnPurchaseReturn
            // 
            this.btnPurchaseReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPurchaseReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnPurchaseReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPurchaseReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPurchaseReturn.FlatAppearance.BorderSize = 0;
            this.btnPurchaseReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPurchaseReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchaseReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseReturn.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPurchaseReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPurchaseReturn.Location = new System.Drawing.Point(407, 0);
            this.btnPurchaseReturn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnPurchaseReturn.Name = "btnPurchaseReturn";
            this.btnPurchaseReturn.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnPurchaseReturn.Size = new System.Drawing.Size(151, 43);
            this.btnPurchaseReturn.TabIndex = 2;
            this.btnPurchaseReturn.Text = "Purchase &Return";
            this.btnPurchaseReturn.UseVisualStyleBackColor = false;
            this.btnPurchaseReturn.Click += new System.EventHandler(this.btnPurchaseReturn_Click);
            // 
            // btnBillEntry
            // 
            this.btnBillEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBillEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBillEntry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBillEntry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBillEntry.FlatAppearance.BorderSize = 0;
            this.btnBillEntry.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBillEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBillEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillEntry.ForeColor = System.Drawing.Color.Black;
            this.btnBillEntry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBillEntry.Location = new System.Drawing.Point(79, 0);
            this.btnBillEntry.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnBillEntry.Name = "btnBillEntry";
            this.btnBillEntry.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnBillEntry.Size = new System.Drawing.Size(167, 43);
            this.btnBillEntry.TabIndex = 1;
            this.btnBillEntry.Text = "&Purchase Bill";
            this.btnBillEntry.UseVisualStyleBackColor = false;
            this.btnBillEntry.Click += new System.EventHandler(this.btnbill_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.Transparent;
            this.btnOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrder.FlatAppearance.BorderSize = 0;
            this.btnOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrder.Location = new System.Drawing.Point(245, 0);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnOrder.Size = new System.Drawing.Size(163, 43);
            this.btnOrder.TabIndex = 1;
            this.btnOrder.Text = "&Order";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // pnlWindow
            // 
            this.pnlWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlWindow.Location = new System.Drawing.Point(0, 43);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(715, 412);
            this.pnlWindow.TabIndex = 2;
            // 
            // btnAdvancePayment
            // 
            this.btnAdvancePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdvancePayment.BackColor = System.Drawing.Color.Transparent;
            this.btnAdvancePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdvancePayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdvancePayment.FlatAppearance.BorderSize = 0;
            this.btnAdvancePayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnAdvancePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvancePayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvancePayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAdvancePayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdvancePayment.Location = new System.Drawing.Point(556, 0);
            this.btnAdvancePayment.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnAdvancePayment.Name = "btnAdvancePayment";
            this.btnAdvancePayment.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnAdvancePayment.Size = new System.Drawing.Size(157, 43);
            this.btnAdvancePayment.TabIndex = 3;
            this.btnAdvancePayment.Text = "&Adv.Payment";
            this.btnAdvancePayment.UseVisualStyleBackColor = false;
            this.btnAdvancePayment.Click += new System.EventHandler(this.btnAdvancePayment_Click);
            // 
            // PurchaseWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 455);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.panel1);
            this.Name = "PurchaseWindows";
            this.Text = "SalesWindows";
            this.Shown += new System.EventHandler(this.PurchaseWindows_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnBillEntry;
        private System.Windows.Forms.Button btnPurchaseReturn;
        private System.Windows.Forms.Button btnAdvancePayment;
    }
}