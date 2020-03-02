namespace DAPRO.User
{
    partial class UserWindow
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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnEditPermition = new System.Windows.Forms.Button();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnChangePw = new System.Windows.Forms.Button();
            this.pnlWindow = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMenu.BackColor = System.Drawing.Color.Black;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.btnEditPermition);
            this.pnlMenu.Controls.Add(this.btnCreateUser);
            this.pnlMenu.Controls.Add(this.btnChangePw);
            this.pnlMenu.Location = new System.Drawing.Point(-1, -1);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(706, 43);
            this.pnlMenu.TabIndex = 2;
            // 
            // btnEditPermition
            // 
            this.btnEditPermition.BackColor = System.Drawing.Color.Transparent;
            this.btnEditPermition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditPermition.FlatAppearance.BorderSize = 0;
            this.btnEditPermition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPermition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditPermition.ForeColor = System.Drawing.Color.White;
            this.btnEditPermition.Location = new System.Drawing.Point(321, 0);
            this.btnEditPermition.Name = "btnEditPermition";
            this.btnEditPermition.Size = new System.Drawing.Size(185, 44);
            this.btnEditPermition.TabIndex = 1;
            this.btnEditPermition.Text = "&Change Access Permission";
            this.btnEditPermition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditPermition.UseVisualStyleBackColor = false;
            this.btnEditPermition.Click += new System.EventHandler(this.btnEditPermition_Click);
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateUser.FlatAppearance.BorderSize = 0;
            this.btnCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateUser.ForeColor = System.Drawing.Color.White;
            this.btnCreateUser.Location = new System.Drawing.Point(163, 0);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(152, 44);
            this.btnCreateUser.TabIndex = 1;
            this.btnCreateUser.Text = "&Create User";
            this.btnCreateUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreateUser.UseVisualStyleBackColor = false;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // btnChangePw
            // 
            this.btnChangePw.BackColor = System.Drawing.SystemColors.Control;
            this.btnChangePw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangePw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePw.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePw.ForeColor = System.Drawing.Color.Maroon;
            this.btnChangePw.Location = new System.Drawing.Point(-1, -1);
            this.btnChangePw.Name = "btnChangePw";
            this.btnChangePw.Size = new System.Drawing.Size(169, 44);
            this.btnChangePw.TabIndex = 0;
            this.btnChangePw.Text = "&Change Password";
            this.btnChangePw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChangePw.UseVisualStyleBackColor = false;
            this.btnChangePw.Click += new System.EventHandler(this.btnChangePw_Click);
            // 
            // pnlWindow
            // 
            this.pnlWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlWindow.Location = new System.Drawing.Point(-2, 41);
            this.pnlWindow.Name = "pnlWindow";
            this.pnlWindow.Size = new System.Drawing.Size(707, 330);
            this.pnlWindow.TabIndex = 3;
            // 
            // UserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(704, 371);
            this.Controls.Add(this.pnlWindow);
            this.Controls.Add(this.pnlMenu);
            this.Name = "UserWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "UserWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.UserWindow_Shown);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnChangePw;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Panel pnlWindow;
        private System.Windows.Forms.Button btnEditPermition;
    }
}