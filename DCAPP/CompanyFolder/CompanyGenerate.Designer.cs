namespace DAPRO
{
    partial class CompanyGenerate
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
            this.pnlCreateWindow = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lnlBack = new System.Windows.Forms.LinkLabel();
            this.btnGenerateCompany = new System.Windows.Forms.Button();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.chkDefult = new System.Windows.Forms.CheckBox();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.lblMSG = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDefultSet = new System.Windows.Forms.Button();
            this.lblCreate = new System.Windows.Forms.LinkLabel();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCreateWindow.SuspendLayout();
            this.pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateWindow
            // 
            this.pnlCreateWindow.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlCreateWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCreateWindow.Controls.Add(this.label1);
            this.pnlCreateWindow.Controls.Add(this.lnlBack);
            this.pnlCreateWindow.Controls.Add(this.btnGenerateCompany);
            this.pnlCreateWindow.Controls.Add(this.txtCompanyName);
            this.pnlCreateWindow.Controls.Add(this.chkDefult);
            this.pnlCreateWindow.Controls.Add(this.pnl1);
            this.pnlCreateWindow.Location = new System.Drawing.Point(5, 5);
            this.pnlCreateWindow.Name = "pnlCreateWindow";
            this.pnlCreateWindow.Size = new System.Drawing.Size(419, 238);
            this.pnlCreateWindow.TabIndex = 0;
            this.pnlCreateWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.pnlCreateWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.pnlCreateWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Linen;
            this.label1.Location = new System.Drawing.Point(31, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "COMPANY NAME ";
            // 
            // lnlBack
            // 
            this.lnlBack.AutoSize = true;
            this.lnlBack.BackColor = System.Drawing.Color.Linen;
            this.lnlBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnlBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnlBack.Location = new System.Drawing.Point(17, 17);
            this.lnlBack.Name = "lnlBack";
            this.lnlBack.Size = new System.Drawing.Size(52, 15);
            this.lnlBack.TabIndex = 3;
            this.lnlBack.TabStop = true;
            this.lnlBack.Text = "<< BACK";
            this.lnlBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlBack_LinkClicked);
            // 
            // btnGenerateCompany
            // 
            this.btnGenerateCompany.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerateCompany.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateCompany.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGenerateCompany.Location = new System.Drawing.Point(266, 138);
            this.btnGenerateCompany.Name = "btnGenerateCompany";
            this.btnGenerateCompany.Size = new System.Drawing.Size(109, 29);
            this.btnGenerateCompany.TabIndex = 2;
            this.btnGenerateCompany.Text = "GENERATE";
            this.btnGenerateCompany.UseVisualStyleBackColor = false;
            this.btnGenerateCompany.Click += new System.EventHandler(this.btnGenerateCompany_Click);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Location = new System.Drawing.Point(33, 94);
            this.txtCompanyName.MaxLength = 70;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(343, 21);
            this.txtCompanyName.TabIndex = 0;
            this.txtCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompanyName_KeyPress);
            // 
            // chkDefult
            // 
            this.chkDefult.AutoSize = true;
            this.chkDefult.BackColor = System.Drawing.Color.Linen;
            this.chkDefult.Enabled = false;
            this.chkDefult.Location = new System.Drawing.Point(32, 132);
            this.chkDefult.Name = "chkDefult";
            this.chkDefult.Size = new System.Drawing.Size(120, 17);
            this.chkDefult.TabIndex = 1;
            this.chkDefult.Text = "Set Defult Company";
            this.chkDefult.UseVisualStyleBackColor = false;
            this.chkDefult.CheckedChanged += new System.EventHandler(this.chkDefult_CheckedChanged);
            // 
            // pnl1
            // 
            this.pnl1.BackColor = System.Drawing.Color.Linen;
            this.pnl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl1.Controls.Add(this.lblMSG);
            this.pnl1.Controls.Add(this.button1);
            this.pnl1.Controls.Add(this.btnDefultSet);
            this.pnl1.Controls.Add(this.lblCreate);
            this.pnl1.Controls.Add(this.cmbDatabase);
            this.pnl1.Controls.Add(this.label2);
            this.pnl1.Location = new System.Drawing.Point(10, 9);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(396, 217);
            this.pnl1.TabIndex = 0;
            // 
            // lblMSG
            // 
            this.lblMSG.AutoSize = true;
            this.lblMSG.ForeColor = System.Drawing.Color.Red;
            this.lblMSG.Location = new System.Drawing.Point(22, 163);
            this.lblMSG.Name = "lblMSG";
            this.lblMSG.Size = new System.Drawing.Size(13, 13);
            this.lblMSG.TabIndex = 16;
            this.lblMSG.Text = "_";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(359, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDefultSet
            // 
            this.btnDefultSet.BackColor = System.Drawing.SystemColors.Control;
            this.btnDefultSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefultSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDefultSet.Location = new System.Drawing.Point(255, 128);
            this.btnDefultSet.Name = "btnDefultSet";
            this.btnDefultSet.Size = new System.Drawing.Size(107, 29);
            this.btnDefultSet.TabIndex = 2;
            this.btnDefultSet.Text = "DEFULT SET";
            this.btnDefultSet.UseVisualStyleBackColor = false;
            this.btnDefultSet.Click += new System.EventHandler(this.btnDefultSet_Click);
            // 
            // lblCreate
            // 
            this.lblCreate.AutoSize = true;
            this.lblCreate.Location = new System.Drawing.Point(24, 123);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(85, 13);
            this.lblCreate.TabIndex = 1;
            this.lblCreate.TabStop = true;
            this.lblCreate.Text = "Create Company";
            this.lblCreate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCreate_LinkClicked);
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.cmbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(23, 83);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(341, 21);
            this.cmbDatabase.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "COMPANY NAME ";
            // 
            // CompanyGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(430, 249);
            this.Controls.Add(this.pnlCreateWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompanyGenerate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompanyGenerate";
            this.Shown += new System.EventHandler(this.CompanyGenerate_Shown);
            this.pnlCreateWindow.ResumeLayout(false);
            this.pnlCreateWindow.PerformLayout();
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCreateWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Button btnGenerateCompany;
        private System.Windows.Forms.CheckBox chkDefult;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Button btnDefultSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblCreate;
        private System.Windows.Forms.LinkLabel lnlBack;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblMSG;
    }
}