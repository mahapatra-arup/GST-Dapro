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
    public partial class CreateUser : Form
    {
        string checkbox = string.Empty;
        string msg = "";
        CheckBox box = new CheckBox();
        public CreateUser()
        {
            InitializeComponent();
            //DefaultClass df = new DefaultClass();
            Getchkbox();

        }
        private void DataSave()
        {
            CheckUnCheck();

            string userName = txtUserName.Text.GetDBFormatString();
            string pasword = txtPassword.Text.GetDBFormatString();
            string qury = "insert into UserControl(UserName,Password,permision,Isdelete,IsEdit,IsCancel)values('" + userName + "','" + pasword + "','" + checkbox + "','" + chkDelete.Checked + "','" + chkEdit.Checked + "','" + ChkCancel.Checked + "')";
            if (SQLHelper.GetInstance().ExcuteQuery(qury, out msg))
            {
                MessageBox.Show("User Create Successfully ", "User Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Clear();
                txtPassword.Clear();
                txtRePassword.Clear();
                for (int i = 0; i < chklistbox.Items.Count; i++)
                {
                    chklistbox.SetItemChecked(i, false);

                }
                chkDelete.Checked = false;
                chkEdit.Checked = false;
                ChkCancel.Checked = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                if (!IsExistingUser())
                {
                    DataSave();
                }
            }
        }
        private bool IsExistingUser()
        {
            string query = "select UserName from UserControl where  UserName='" + txtUserName.Text.GetDBFormatString() + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                toolTip1.Show("This User Name already use\nTry another user name", txtUserName, txtUserName.Location.X, txtUserName.Height - 100, 1500);
                // MessageBox.Show("This User Name already use\nTry another user name", "User Create", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUserName.Focus();
                return true;
            }
            return false;
        }
        private bool IsValidEntry()
        {
            int j = 0;
            if (txtUserName.Text.ISNullOrWhiteSpace())
            {
                toolTip1.Show("Enter user name.", txtUserName, txtUserName.Location.X - (txtUserName.Width + 10), txtUserName.Height - 100, 1500);
                txtUserName.Select();
                return false;
            }
            if (txtPassword.Text != txtRePassword.Text)
            {
                toolTip1.Show("New Password Mismatch !!", txtRePassword,1500);
                // MessageBox.Show("Oops!!Password Mismatch !!\n\nEnter Same Password in Booth  Boxes ", "User Create", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRePassword.Select();
                return false;
            }
            for (int i = 0; i < chklistbox.Items.Count; i++)
            {
                if (chklistbox.GetItemChecked(i))
                {
                    j = 1;
                    break;
                }
            }
            if (j == 0)
            {
                toolTip1.Show("Please give atleast one permission ", chklistbox, 1500);

                //MessageBox.Show("Please Give atleast One permission", "Permissin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                chklistbox.Focus();
                return false;
            }

            return true;
        }
        private void Getchkbox()
        {
            foreach (string i in UserTools._Controls)
            {
                if (i != string.Empty && i != null)
                {
                    chklistbox.Items.Add(i);
                }
            }



        }
        private void CheckUnCheck()
        {
            checkbox = string.Empty;
            for (int i = 0; i < chklistbox.Items.Count; i++)
            {
                if (chklistbox.GetItemChecked(i))
                {
                    checkbox = checkbox + chklistbox.Items[i].ToString() + ",";
                }

            }
        }
        private void chklistboxText_SelectedIndexChanged(object sender, EventArgs e)
        {
            chklistbox.SelectedIndex = chklistbox.SelectedIndex;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '☺';
            txtRePassword.PasswordChar = '☺';
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            txtPassword.PasswordChar = default(char);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            txtRePassword.PasswordChar = default(char);
        }
    }
}
