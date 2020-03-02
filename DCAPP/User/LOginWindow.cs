using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.User
{
    public partial class LoginWindow : Form
    {
        string msg = "";
        public bool flag = false;
        public event Action<bool> onClose;
        public LoginWindow()
        {
            InitializeComponent();
            UserTools.GetUserName();
            cmbUserName.AddUserName();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            this.ActiveControl = cmbUserName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                btnLogin_Click(null,null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.C))
            {
                btnCancel_Click(null,null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Islogin())
            {
                UserTools._UserID = ((KeyValuePair<string, string>)cmbUserName.SelectedItem).Key.ToString();
                UserTools._UserName = ((KeyValuePair<string, string>)cmbUserName.SelectedItem).Value.ToString();
                FinancialYearTools.GetFinancialYearDetails();
                UserTools.GetEditDeletePermision();
                
                flag = true;
                this.Close();
            }
            else
            {
                flag = false;
                toolTip1.Show("SORRY!INVALID PASSWORD",cmbUserName,10,cmbUserName.Height-100,1000);
                // MessageBox.Show("SORRY! INVALID PASSWORD", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool Islogin()
        {
            string userName = cmbUserName.Text;
            string password = txtPassword.Text.GetDBFormatString();
            string qurey = "select UserName from UserControl where UserName='" + userName + "' and Password ='" + password + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(qurey, out msg);
            if (obj != null)
            {
               
                return true;

            }
            return false;
        }

        private void LoginWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onClose != null)
            {
                onClose(flag);
            }
        }

        private void txtPw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}

