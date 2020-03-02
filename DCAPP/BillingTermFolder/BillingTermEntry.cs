using System;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class BillingTermEntry : Form
    {
        public event Action<string> OnClose;
        string msg = "";
        public BillingTermEntry()
        {
            InitializeComponent();
        }
        private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                if (ISDupliCateEntry())
                {
                    Datasave();
                }

            }

        }
        private void Datasave()
        {
            string termname = txtTermName.Text.GetDBFormatString();
            string days = txtDays.Text.GetDBFormatString();
            string query = "Insert into BillingTerms(TermsName,Days) values('" + termname + "'," + days + ") ";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internally problem", "problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                BillingTermTools.GetBillingTerms();
                this.Close();
            }
        }
        private bool ISDupliCateEntry()
        {
            string termname = txtTermName.Text.GetDBFormatString();
            string days = txtDays.Text.GetDBFormatString();
            string query = "select Days from BillingTerms where TermsName='" + termname + "' ";
            string query2 = "select TermsName from BillingTerms where Days='" + days + "' ";
            string query3 = "select * from BillingTerms where TermsName='" + termname + "' and Days='" + days + "' ";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            object obj2 = SQLHelper.GetInstance().ExcuteScalar(query2, out msg);
            object obj3 = SQLHelper.GetInstance().ExcuteScalar(query3, out msg);
            if (obj3.ISValidObject())
            {
                MessageBox.Show("Enter term name and days alredy exsist.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTermName.Focus();
                return false;
            }
            if (obj.ISValidObject())
            {
                MessageBox.Show("Enter term name alredy exsist With \"" + obj.ToString() + "\" days.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTermName.Focus();
                return false;
            }
            if (obj2.ISValidObject())
            {
                MessageBox.Show("Enter days alredy exsist with \"" + obj2.ToString() + "\" term name. ", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDays.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidEntry()
        {
            if (txtTermName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter term name ", "save", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTermName.Focus();
                return false;
            }
            if (txtDays.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter how many days ", "save", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDays.Focus();
                return false;
            }
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BillingTermEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose!=null)
            {
                OnClose(txtTermName.Text);
            }
        }
    }
}
