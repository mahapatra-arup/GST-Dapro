using System.Drawing;

namespace DAPRO.SoftwareLicense
{
    partial class SoftwareActivation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftwareActivation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblAppsname = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbImgRightWrong = new System.Windows.Forms.PictureBox();
            this.lblCompanyMName = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKey4 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKey3 = new System.Windows.Forms.TextBox();
            this.txtKey2 = new System.Windows.Forms.TextBox();
            this.txtKey1 = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblErrorMessege = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnActivated = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlNetConnection = new System.Windows.Forms.Panel();
            this.pbRefreshInternetConnection = new System.Windows.Forms.PictureBox();
            this.pbInternetError = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImgRightWrong)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlNetConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshInternetConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternetError)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCopyright);
            this.panel1.Controls.Add(this.lblAppsname);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnlNetConnection);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 397);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyright.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.Black;
            this.lblCopyright.Location = new System.Drawing.Point(143, 376);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(424, 20);
            this.lblCopyright.TabIndex = 9;
            this.lblCopyright.Text = "XXX";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAppsname
            // 
            this.lblAppsname.AutoSize = true;
            this.lblAppsname.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblAppsname.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppsname.ForeColor = System.Drawing.Color.Teal;
            this.lblAppsname.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblAppsname.LinkColor = System.Drawing.Color.Teal;
            this.lblAppsname.Location = new System.Drawing.Point(331, 7);
            this.lblAppsname.Name = "lblAppsname";
            this.lblAppsname.Size = new System.Drawing.Size(52, 29);
            this.lblAppsname.TabIndex = 7;
            this.lblAppsname.TabStop = true;
            this.lblAppsname.Text = "___";
            this.lblAppsname.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(543, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(6, 7);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(22, 23);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 2;
            this.pbLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "DAPRO ACTIVATION";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pbImgRightWrong);
            this.panel2.Controls.Add(this.lblCompanyMName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtKey4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtKey3);
            this.panel2.Controls.Add(this.txtKey2);
            this.panel2.Controls.Add(this.txtKey1);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.lblErrorMessege);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.btnActivated);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(-2, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(580, 339);
            this.panel2.TabIndex = 0;
            // 
            // pbImgRightWrong
            // 
            this.pbImgRightWrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImgRightWrong.BackColor = System.Drawing.Color.Transparent;
            this.pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
            this.pbImgRightWrong.Location = new System.Drawing.Point(522, 93);
            this.pbImgRightWrong.Name = "pbImgRightWrong";
            this.pbImgRightWrong.Size = new System.Drawing.Size(36, 26);
            this.pbImgRightWrong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImgRightWrong.TabIndex = 41;
            this.pbImgRightWrong.TabStop = false;
            // 
            // lblCompanyMName
            // 
            this.lblCompanyMName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompanyMName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyMName.FlatAppearance.BorderSize = 0;
            this.lblCompanyMName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCompanyMName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyMName.ForeColor = System.Drawing.Color.Teal;
            this.lblCompanyMName.Location = new System.Drawing.Point(0, 310);
            this.lblCompanyMName.Name = "lblCompanyMName";
            this.lblCompanyMName.Size = new System.Drawing.Size(283, 23);
            this.lblCompanyMName.TabIndex = 10;
            this.lblCompanyMName.Text = "button3";
            this.lblCompanyMName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCompanyMName.UseVisualStyleBackColor = false;
            this.lblCompanyMName.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(387, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "--";
            // 
            // txtKey4
            // 
            this.txtKey4.BackColor = System.Drawing.Color.White;
            this.txtKey4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey4.ContextMenuStrip = this.contextMenuStrip1;
            this.txtKey4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtKey4.ForeColor = System.Drawing.Color.Black;
            this.txtKey4.Location = new System.Drawing.Point(403, 93);
            this.txtKey4.MaxLength = 4;
            this.txtKey4.Name = "txtKey4";
            this.txtKey4.Size = new System.Drawing.Size(109, 26);
            this.txtKey4.TabIndex = 3;
            this.txtKey4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKey4.TextChanged += new System.EventHandler(this.txtKey4_TextChanged);
            this.txtKey4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey1_KeyDown);
            this.txtKey4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            this.txtKey4.Leave += new System.EventHandler(this.txtKey3_Leave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Clear";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(259, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(127, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "--";
            // 
            // txtKey3
            // 
            this.txtKey3.BackColor = System.Drawing.Color.White;
            this.txtKey3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey3.ContextMenuStrip = this.contextMenuStrip1;
            this.txtKey3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtKey3.ForeColor = System.Drawing.Color.Black;
            this.txtKey3.Location = new System.Drawing.Point(275, 93);
            this.txtKey3.MaxLength = 4;
            this.txtKey3.Name = "txtKey3";
            this.txtKey3.Size = new System.Drawing.Size(109, 26);
            this.txtKey3.TabIndex = 2;
            this.txtKey3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKey3.TextChanged += new System.EventHandler(this.txtKey3_TextChanged);
            this.txtKey3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            this.txtKey3.Leave += new System.EventHandler(this.txtKey3_Leave);
            // 
            // txtKey2
            // 
            this.txtKey2.BackColor = System.Drawing.Color.White;
            this.txtKey2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey2.ContextMenuStrip = this.contextMenuStrip1;
            this.txtKey2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtKey2.ForeColor = System.Drawing.Color.Black;
            this.txtKey2.Location = new System.Drawing.Point(146, 93);
            this.txtKey2.MaxLength = 4;
            this.txtKey2.Name = "txtKey2";
            this.txtKey2.Size = new System.Drawing.Size(109, 26);
            this.txtKey2.TabIndex = 1;
            this.txtKey2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKey2.TextChanged += new System.EventHandler(this.txtKey2_TextChanged);
            this.txtKey2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            this.txtKey2.Leave += new System.EventHandler(this.txtKey3_Leave);
            // 
            // txtKey1
            // 
            this.txtKey1.BackColor = System.Drawing.Color.White;
            this.txtKey1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey1.ContextMenuStrip = this.contextMenuStrip1;
            this.txtKey1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtKey1.ForeColor = System.Drawing.Color.Black;
            this.txtKey1.Location = new System.Drawing.Point(16, 93);
            this.txtKey1.MaxLength = 4;
            this.txtKey1.Name = "txtKey1";
            this.txtKey1.Size = new System.Drawing.Size(109, 26);
            this.txtKey1.TabIndex = 0;
            this.txtKey1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKey1.TextChanged += new System.EventHandler(this.txtKey1_TextChanged);
            this.txtKey1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey1_KeyDown);
            this.txtKey1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            this.txtKey1.Leave += new System.EventHandler(this.txtKey3_Leave);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Ivory;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Enabled = false;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.YellowGreen;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.YellowGreen;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.YellowGreen;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(446, 282);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(109, 29);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&FINISH";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblErrorMessege
            // 
            this.lblErrorMessege.AutoSize = true;
            this.lblErrorMessege.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessege.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessege.Location = new System.Drawing.Point(12, 249);
            this.lblErrorMessege.Name = "lblErrorMessege";
            this.lblErrorMessege.Size = new System.Drawing.Size(10, 15);
            this.lblErrorMessege.TabIndex = 12;
            this.lblErrorMessege.Text = ".";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.linkLabel1.LinkColor = System.Drawing.Color.Teal;
            this.linkLabel1.Location = new System.Drawing.Point(279, 207);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(247, 17);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Activate trail version of the application";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Image = global::DAPRO.Properties.Resources.next_22x22_;
            this.button2.Location = new System.Drawing.Point(522, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 27);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnActivated
            // 
            this.btnActivated.BackColor = System.Drawing.Color.AliceBlue;
            this.btnActivated.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActivated.Enabled = false;
            this.btnActivated.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnActivated.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btnActivated.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnActivated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivated.Location = new System.Drawing.Point(16, 147);
            this.btnActivated.Name = "btnActivated";
            this.btnActivated.Size = new System.Drawing.Size(109, 29);
            this.btnActivated.TabIndex = 4;
            this.btnActivated.Text = "ACTIVATE";
            this.btnActivated.UseVisualStyleBackColor = false;
            this.btnActivated.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(16, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Activation code formate : XXXX-XXXX-XXXX-XXXX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter activation code ";
            // 
            // pnlNetConnection
            // 
            this.pnlNetConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pnlNetConnection.Controls.Add(this.pbRefreshInternetConnection);
            this.pnlNetConnection.Controls.Add(this.pbInternetError);
            this.pnlNetConnection.Location = new System.Drawing.Point(0, 55);
            this.pnlNetConnection.Name = "pnlNetConnection";
            this.pnlNetConnection.Size = new System.Drawing.Size(578, 321);
            this.pnlNetConnection.TabIndex = 24;
            // 
            // pbRefreshInternetConnection
            // 
            this.pbRefreshInternetConnection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbRefreshInternetConnection.BackColor = System.Drawing.Color.Transparent;
            this.pbRefreshInternetConnection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbRefreshInternetConnection.BackgroundImage")));
            this.pbRefreshInternetConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRefreshInternetConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRefreshInternetConnection.Location = new System.Drawing.Point(248, 208);
            this.pbRefreshInternetConnection.Name = "pbRefreshInternetConnection";
            this.pbRefreshInternetConnection.Size = new System.Drawing.Size(91, 31);
            this.pbRefreshInternetConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRefreshInternetConnection.TabIndex = 10;
            this.pbRefreshInternetConnection.TabStop = false;
            this.toolTip1.SetToolTip(this.pbRefreshInternetConnection, "Refresh");
            this.pbRefreshInternetConnection.Click += new System.EventHandler(this.pbRefreshInternetConnection_Click);
            // 
            // pbInternetError
            // 
            this.pbInternetError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInternetError.Image = ((System.Drawing.Image)(resources.GetObject("pbInternetError.Image")));
            this.pbInternetError.Location = new System.Drawing.Point(0, 4);
            this.pbInternetError.Name = "pbInternetError";
            this.pbInternetError.Size = new System.Drawing.Size(578, 316);
            this.pbInternetError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbInternetError.TabIndex = 11;
            this.pbInternetError.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "Internet Connection";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SoftwareActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(579, 399);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Name = "SoftwareActivation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoftwareActivation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoftwareActivation_FormClosing);
            this.Shown += new System.EventHandler(this.SoftwareActivation_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImgRightWrong)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlNetConnection.ResumeLayout(false);
            this.pnlNetConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshInternetConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternetError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActivated;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel lblAppsname;
        private System.Windows.Forms.Label lblErrorMessege;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.TextBox txtKey3;
        internal System.Windows.Forms.TextBox txtKey2;
        internal System.Windows.Forms.TextBox txtKey1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button lblCompanyMName;
        private System.Windows.Forms.PictureBox pbInternetError;
        private System.Windows.Forms.Panel pnlNetConnection;
        private System.Windows.Forms.PictureBox pbRefreshInternetConnection;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtKey4;
        private System.Windows.Forms.PictureBox pbImgRightWrong;
        private System.Windows.Forms.Timer timer1;
    }
}