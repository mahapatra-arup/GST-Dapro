namespace DAPRO.User
{
    partial class PermissionWindows
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
            this.pnlCreateUser = new System.Windows.Forms.Panel();
            this.chklistbox = new System.Windows.Forms.CheckedListBox();
            this.ChkCancel = new System.Windows.Forms.CheckBox();
            this.chkEdit = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.CmbUserName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.pnlCreateUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateUser
            // 
            this.pnlCreateUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreateUser.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnlCreateUser.Controls.Add(this.chklistbox);
            this.pnlCreateUser.Controls.Add(this.ChkCancel);
            this.pnlCreateUser.Controls.Add(this.chkEdit);
            this.pnlCreateUser.Controls.Add(this.label3);
            this.pnlCreateUser.Controls.Add(this.label1);
            this.pnlCreateUser.Controls.Add(this.chkDelete);
            this.pnlCreateUser.Controls.Add(this.CmbUserName);
            this.pnlCreateUser.Location = new System.Drawing.Point(-1, 67);
            this.pnlCreateUser.Name = "pnlCreateUser";
            this.pnlCreateUser.Size = new System.Drawing.Size(671, 292);
            this.pnlCreateUser.TabIndex = 9;
            // 
            // chklistbox
            // 
            this.chklistbox.CheckOnClick = true;
            this.chklistbox.FormattingEnabled = true;
            this.chklistbox.Location = new System.Drawing.Point(109, 134);
            this.chklistbox.Name = "chklistbox";
            this.chklistbox.Size = new System.Drawing.Size(179, 154);
            this.chklistbox.TabIndex = 10;
            // 
            // ChkCancel
            // 
            this.ChkCancel.AutoSize = true;
            this.ChkCancel.Location = new System.Drawing.Point(109, 104);
            this.ChkCancel.Name = "ChkCancel";
            this.ChkCancel.Size = new System.Drawing.Size(73, 17);
            this.ChkCancel.TabIndex = 11;
            this.ChkCancel.Text = "IsCancel?";
            this.ChkCancel.UseVisualStyleBackColor = true;
            // 
            // chkEdit
            // 
            this.chkEdit.AutoSize = true;
            this.chkEdit.Location = new System.Drawing.Point(109, 81);
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(58, 17);
            this.chkEdit.TabIndex = 11;
            this.chkEdit.Text = "IsEdit?";
            this.chkEdit.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Permissions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "User Name :";
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(109, 60);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(71, 17);
            this.chkDelete.TabIndex = 11;
            this.chkDelete.Text = "IsDelete?";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // CmbUserName
            // 
            this.CmbUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserName.FormattingEnabled = true;
            this.CmbUserName.Location = new System.Drawing.Point(109, 14);
            this.CmbUserName.Name = "CmbUserName";
            this.CmbUserName.Size = new System.Drawing.Size(179, 21);
            this.CmbUserName.TabIndex = 12;
            this.CmbUserName.SelectedIndexChanged += new System.EventHandler(this.CmbUserName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(2, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "User Permission";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(564, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "Warning";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "You can set your User Permission.";
            // 
            // PermissionWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(667, 399);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlCreateUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "PermissionWindows";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Permission Windows";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlCreateUser.ResumeLayout(false);
            this.pnlCreateUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCreateUser;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox CmbUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ChkCancel;
        private System.Windows.Forms.CheckBox chkEdit;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckedListBox chklistbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}