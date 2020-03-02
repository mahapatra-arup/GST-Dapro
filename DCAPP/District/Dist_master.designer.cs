namespace DAPRO
{
    partial class Dist_master
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
            this.txtDist = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.Dist = new System.Windows.Forms.Label();
            this.State = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDist
            // 
            this.txtDist.BackColor = System.Drawing.Color.White;
            this.txtDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDist.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDist.Location = new System.Drawing.Point(112, 97);
            this.txtDist.Margin = new System.Windows.Forms.Padding(4);
            this.txtDist.MaxLength = 50;
            this.txtDist.Name = "txtDist";
            this.txtDist.Size = new System.Drawing.Size(310, 22);
            this.txtDist.TabIndex = 0;
            this.txtDist.TextChanged += new System.EventHandler(this.txtDist_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(315, 147);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(200, 147);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(107, 36);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(112, 47);
            this.cmbState.Margin = new System.Windows.Forms.Padding(4);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(317, 24);
            this.cmbState.TabIndex = 0;
            // 
            // Dist
            // 
            this.Dist.AutoSize = true;
            this.Dist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dist.ForeColor = System.Drawing.Color.Black;
            this.Dist.Location = new System.Drawing.Point(42, 100);
            this.Dist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Dist.Name = "Dist";
            this.Dist.Size = new System.Drawing.Size(62, 16);
            this.Dist.TabIndex = 0;
            this.Dist.Text = "* District :";
            // 
            // State
            // 
            this.State.AutoSize = true;
            this.State.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.State.ForeColor = System.Drawing.Color.Black;
            this.State.Location = new System.Drawing.Point(42, 47);
            this.State.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.State.Name = "State";
            this.State.Size = new System.Drawing.Size(45, 16);
            this.State.TabIndex = 0;
            this.State.Text = "State :";
            // 
            // Dist_master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 224);
            this.Controls.Add(this.txtDist);
            this.Controls.Add(this.State);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Dist);
            this.Controls.Add(this.cmbState);
            this.Controls.Add(this.btnSubmit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dist_master";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "District";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dist_master_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDist;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label Dist;
        private System.Windows.Forms.Label State;
    }
}

