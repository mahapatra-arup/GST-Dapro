using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class BankAdd : Form
    {
        string msg = "";
        public event Action<string> onclose;
        public BankAdd()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool IsvalidEntry()
        {
            if (txtBankname.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter a bank name", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtBankname.Focus();
                return false;
            }
            return true;
        }
        private void SaveData()
        {
            string query = "insert into bank(BankName) values('" + txtBankname.Text.GetDBFormatString() + "')";
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                BAnkTools.GetBank();
                MessageBox.Show("Save Successfully", "save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool DuplicateEntry()
        {
            string query = "Select Bankname where bankname='" + txtBankname.Text.GetDBFormatString() + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                MessageBox.Show("Duplicate bank found.", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtBankname.Focus();
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
                if (DuplicateEntry())
                {
                    SaveData();
                }
            }
        }
        private void BankAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose(txtBankname.Text);
            }
        }
    }
}
