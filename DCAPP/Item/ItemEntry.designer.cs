namespace DAPRO
{
    partial class ItemEntry
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
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.State = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblsachascode = new System.Windows.Forms.Label();
            this.cmbComodityNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlQuantity = new System.Windows.Forms.Panel();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnAddunit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGstRate = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCESSRate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNaration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCgst = new System.Windows.Forms.TextBox();
            this.txtSgst = new System.Windows.Forms.TextBox();
            this.txtIGst = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTemplatNAme = new System.Windows.Forms.TextBox();
            this.txtCompanyNAme = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTaxability = new System.Windows.Forms.GroupBox();
            this.pnlTax = new System.Windows.Forms.Panel();
            this.chkRCM = new System.Windows.Forms.CheckBox();
            this.chkITC = new System.Windows.Forms.CheckBox();
            this.pnlQuantity.SuspendLayout();
            this.pnlTaxability.SuspendLayout();
            this.pnlTax.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(154, 79);
            this.txtItemName.MaxLength = 50;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(563, 22);
            this.txtItemName.TabIndex = 1;
            this.txtItemName.TextChanged += new System.EventHandler(this.txtItemName_TextChanged);
            // 
            // State
            // 
            this.State.AutoSize = true;
            this.State.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.State.Location = new System.Drawing.Point(36, 82);
            this.State.Name = "State";
            this.State.Size = new System.Drawing.Size(84, 16);
            this.State.TabIndex = 17;
            this.State.Text = "*Item Name :";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(450, 515);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(128, 36);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "&SAVE";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(584, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 36);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblsachascode
            // 
            this.lblsachascode.AutoSize = true;
            this.lblsachascode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsachascode.Location = new System.Drawing.Point(36, 261);
            this.lblsachascode.Name = "lblsachascode";
            this.lblsachascode.Size = new System.Drawing.Size(107, 16);
            this.lblsachascode.TabIndex = 22;
            this.lblsachascode.Text = "Comodity Code :";
            // 
            // cmbComodityNo
            // 
            this.cmbComodityNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbComodityNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbComodityNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbComodityNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbComodityNo.FormattingEnabled = true;
            this.cmbComodityNo.Location = new System.Drawing.Point(154, 257);
            this.cmbComodityNo.Name = "cmbComodityNo";
            this.cmbComodityNo.Size = new System.Drawing.Size(161, 24);
            this.cmbComodityNo.TabIndex = 8;
            this.cmbComodityNo.Leave += new System.EventHandler(this.cmbComodityNo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Category :";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(154, 110);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(331, 24);
            this.cmbCategory.TabIndex = 2;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.ForeColor = System.Drawing.Color.Black;
            this.btnAddCategory.Location = new System.Drawing.Point(487, 109);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(25, 26);
            this.btnAddCategory.TabIndex = 3;
            this.btnAddCategory.Text = "+";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Goods",
            "Services"});
            this.cmbType.Location = new System.Drawing.Point(130, 21);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(206, 32);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 25);
            this.label9.TabIndex = 27;
            this.label9.Text = "Item Type ";
            // 
            // pnlQuantity
            // 
            this.pnlQuantity.Controls.Add(this.cmbUnit);
            this.pnlQuantity.Controls.Add(this.label21);
            this.pnlQuantity.Controls.Add(this.btnAddunit);
            this.pnlQuantity.Location = new System.Drawing.Point(500, 231);
            this.pnlQuantity.Name = "pnlQuantity";
            this.pnlQuantity.Size = new System.Drawing.Size(230, 40);
            this.pnlQuantity.TabIndex = 7;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(59, 10);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(135, 24);
            this.cmbUnit.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(7, 14);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 16);
            this.label21.TabIndex = 32;
            this.label21.Text = "*Unit :";
            // 
            // btnAddunit
            // 
            this.btnAddunit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddunit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddunit.ForeColor = System.Drawing.Color.Black;
            this.btnAddunit.Location = new System.Drawing.Point(196, 9);
            this.btnAddunit.Name = "btnAddunit";
            this.btnAddunit.Size = new System.Drawing.Size(24, 26);
            this.btnAddunit.TabIndex = 1;
            this.btnAddunit.Text = "+";
            this.btnAddunit.UseVisualStyleBackColor = true;
            this.btnAddunit.Click += new System.EventHandler(this.btnAddunit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "*GST :";
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
            this.cmbGstRate.Location = new System.Drawing.Point(137, 21);
            this.cmbGstRate.Name = "cmbGstRate";
            this.cmbGstRate.Size = new System.Drawing.Size(189, 24);
            this.cmbGstRate.TabIndex = 0;
            this.cmbGstRate.SelectedIndexChanged += new System.EventHandler(this.cmbGstRate_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(342, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "CESS :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(514, 101);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 16);
            this.label15.TabIndex = 22;
            this.label15.Text = "%";
            // 
            // txtCESSRate
            // 
            this.txtCESSRate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCESSRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCESSRate.Location = new System.Drawing.Point(396, 101);
            this.txtCESSRate.MaxLength = 5;
            this.txtCESSRate.Name = "txtCESSRate";
            this.txtCESSRate.Size = new System.Drawing.Size(113, 22);
            this.txtCESSRate.TabIndex = 2;
            this.txtCESSRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCESSRate_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(514, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 16);
            this.label16.TabIndex = 33;
            this.label16.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(342, 45);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(51, 16);
            this.label17.TabIndex = 32;
            this.label17.Text = "CGST :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(342, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 16);
            this.label18.TabIndex = 32;
            this.label18.Text = "SGST :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(515, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(20, 16);
            this.label19.TabIndex = 33;
            this.label19.Text = "%";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(36, 290);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(113, 16);
            this.label23.TabIndex = 17;
            this.label23.Text = "Item Description : ";
            // 
            // txtNaration
            // 
            this.txtNaration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNaration.Location = new System.Drawing.Point(154, 287);
            this.txtNaration.MaxLength = 550;
            this.txtNaration.Name = "txtNaration";
            this.txtNaration.Size = new System.Drawing.Size(565, 22);
            this.txtNaration.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(342, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "IGST :";
            // 
            // txtCgst
            // 
            this.txtCgst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCgst.Enabled = false;
            this.txtCgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCgst.Location = new System.Drawing.Point(396, 42);
            this.txtCgst.MaxLength = 5;
            this.txtCgst.Name = "txtCgst";
            this.txtCgst.ReadOnly = true;
            this.txtCgst.Size = new System.Drawing.Size(113, 22);
            this.txtCgst.TabIndex = 2;
            // 
            // txtSgst
            // 
            this.txtSgst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSgst.Enabled = false;
            this.txtSgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSgst.Location = new System.Drawing.Point(396, 71);
            this.txtSgst.MaxLength = 5;
            this.txtSgst.Name = "txtSgst";
            this.txtSgst.ReadOnly = true;
            this.txtSgst.Size = new System.Drawing.Size(113, 22);
            this.txtSgst.TabIndex = 3;
            // 
            // txtIGst
            // 
            this.txtIGst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIGst.Enabled = false;
            this.txtIGst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIGst.Location = new System.Drawing.Point(396, 13);
            this.txtIGst.MaxLength = 5;
            this.txtIGst.Name = "txtIGst";
            this.txtIGst.ReadOnly = true;
            this.txtIGst.Size = new System.Drawing.Size(113, 22);
            this.txtIGst.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(515, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "%";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lime;
            this.panel1.Location = new System.Drawing.Point(22, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 3);
            this.panel1.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(36, 202);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 16);
            this.label12.TabIndex = 195;
            this.label12.Text = "*Template Name :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(36, 173);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 16);
            this.label14.TabIndex = 196;
            this.label14.Text = "Company :";
            // 
            // txtTemplatNAme
            // 
            this.txtTemplatNAme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplatNAme.Location = new System.Drawing.Point(154, 199);
            this.txtTemplatNAme.MaxLength = 500;
            this.txtTemplatNAme.Name = "txtTemplatNAme";
            this.txtTemplatNAme.Size = new System.Drawing.Size(565, 22);
            this.txtTemplatNAme.TabIndex = 6;
            // 
            // txtCompanyNAme
            // 
            this.txtCompanyNAme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCompanyNAme.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCompanyNAme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyNAme.Location = new System.Drawing.Point(154, 170);
            this.txtCompanyNAme.MaxLength = 50;
            this.txtCompanyNAme.Name = "txtCompanyNAme";
            this.txtCompanyNAme.Size = new System.Drawing.Size(565, 22);
            this.txtCompanyNAme.TabIndex = 5;
            this.txtCompanyNAme.TextChanged += new System.EventHandler(this.txtCompanyNAme_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Sub Category :";
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSubCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSubCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubCategory.FormattingEnabled = true;
            this.cmbSubCategory.Location = new System.Drawing.Point(154, 140);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(331, 24);
            this.cmbSubCategory.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(151, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 13);
            this.label7.TabIndex = 195;
            this.label7.Text = "You will find everywhere this item using template name";
            // 
            // pnlTaxability
            // 
            this.pnlTaxability.Controls.Add(this.pnlTax);
            this.pnlTaxability.Controls.Add(this.cmbGstRate);
            this.pnlTaxability.Controls.Add(this.label4);
            this.pnlTaxability.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTaxability.Location = new System.Drawing.Point(17, 328);
            this.pnlTaxability.Name = "pnlTaxability";
            this.pnlTaxability.Size = new System.Drawing.Size(700, 181);
            this.pnlTaxability.TabIndex = 10;
            this.pnlTaxability.TabStop = false;
            this.pnlTaxability.Text = "Tax Details";
            // 
            // pnlTax
            // 
            this.pnlTax.Controls.Add(this.chkRCM);
            this.pnlTax.Controls.Add(this.chkITC);
            this.pnlTax.Controls.Add(this.label6);
            this.pnlTax.Controls.Add(this.label19);
            this.pnlTax.Controls.Add(this.txtCgst);
            this.pnlTax.Controls.Add(this.txtSgst);
            this.pnlTax.Controls.Add(this.label16);
            this.pnlTax.Controls.Add(this.label10);
            this.pnlTax.Controls.Add(this.txtIGst);
            this.pnlTax.Controls.Add(this.label1);
            this.pnlTax.Controls.Add(this.label15);
            this.pnlTax.Controls.Add(this.label18);
            this.pnlTax.Controls.Add(this.txtCESSRate);
            this.pnlTax.Controls.Add(this.label17);
            this.pnlTax.Location = new System.Drawing.Point(13, 51);
            this.pnlTax.Name = "pnlTax";
            this.pnlTax.Size = new System.Drawing.Size(554, 129);
            this.pnlTax.TabIndex = 1;
            // 
            // chkRCM
            // 
            this.chkRCM.AutoSize = true;
            this.chkRCM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRCM.Location = new System.Drawing.Point(124, 17);
            this.chkRCM.Name = "chkRCM";
            this.chkRCM.Size = new System.Drawing.Size(128, 20);
            this.chkRCM.TabIndex = 0;
            this.chkRCM.Text = "RCM  Applicable";
            this.chkRCM.UseVisualStyleBackColor = true;
            // 
            // chkITC
            // 
            this.chkITC.AutoSize = true;
            this.chkITC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkITC.Location = new System.Drawing.Point(124, 41);
            this.chkITC.Name = "chkITC";
            this.chkITC.Size = new System.Drawing.Size(119, 20);
            this.chkITC.TabIndex = 1;
            this.chkITC.Text = "ITC  Applicable";
            this.chkITC.UseVisualStyleBackColor = true;
            // 
            // ItemEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 563);
            this.Controls.Add(this.pnlTaxability);
            this.Controls.Add(this.txtTemplatNAme);
            this.Controls.Add(this.txtCompanyNAme);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtNaration);
            this.Controls.Add(this.pnlQuantity);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbSubCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbComodityNo);
            this.Controls.Add(this.lblsachascode);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.State);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Entry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemEntry_FormClosing);
            this.pnlQuantity.ResumeLayout(false);
            this.pnlQuantity.PerformLayout();
            this.pnlTaxability.ResumeLayout(false);
            this.pnlTaxability.PerformLayout();
            this.pnlTax.ResumeLayout(false);
            this.pnlTax.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label State;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblsachascode;
        private System.Windows.Forms.ComboBox cmbComodityNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbGstRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCESSRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnAddunit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNaration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCgst;
        private System.Windows.Forms.TextBox txtSgst;
        private System.Windows.Forms.TextBox txtIGst;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTemplatNAme;
        private System.Windows.Forms.TextBox txtCompanyNAme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSubCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox pnlTaxability;
        private System.Windows.Forms.CheckBox chkRCM;
        private System.Windows.Forms.CheckBox chkITC;
        private System.Windows.Forms.Panel pnlTax;
    }
}