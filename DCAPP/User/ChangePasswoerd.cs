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
    public partial class ChangePasswoerd : Form
    {
        string msg = "";
        string uId = UserTools._UserID;
        string psw;
        public ChangePasswoerd()
        {

            InitializeComponent();
            lblUserName.Text = UserTools._UserName;

        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            string nwpw = txtRePw.Text.GetDBFormatString();
            if (IsValidNewPassword())
            {
                if (IsValidOldPassword())
                {
                    if (!IsSamePassword())
                    {
                        string qurey = "update UserControl set Password='" + nwpw + "'where UserID='" + uId + "'";
                        if (SQLHelper.GetInstance().ExcuteQuery(qurey, out msg))
                        {
                            MessageBox.Show("Password Updated.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } 
                    }
                }
            }
        }
        private bool IsValidNewPassword()
        {
            if (txtPw.Text == txtRePw.Text)
            {
                return true;
            }
            MessageBox.Show("New Password Mismatch.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            txtRePw.Select();
            return false;
        }
        private bool IsValidOldPassword()
        {
            string q = "select Password from UserControl where UserID='" + uId + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(q, out msg);

            if (obj != null)
            {
                psw = obj.ToString();
            }
            if (psw == txtPassword.Text)
            {
                return true;
            }
            MessageBox.Show("Invalid Old Password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            txtPassword.Focus();
            return false;
        }
        private bool IsSamePassword()
        {
            if (txtPw.Text == txtRePw.Text && txtRePw.Text==txtPassword.Text)
            {
                MessageBox.Show("New password same as current password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            txtPassword.PasswordChar = default(char);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '☺';
            txtPw.PasswordChar = '☺';
            txtRePw.PasswordChar = '☺';
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            txtPw.PasswordChar = default(char);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            txtRePw.PasswordChar = default(char);
        }
    }
}
