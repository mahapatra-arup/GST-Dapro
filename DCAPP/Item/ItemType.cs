using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.Item
{
    public partial class ItemType : Form
    {
        string msg = "";
        public event Action<string> onclose;
        public ItemType()
        {
            InitializeComponent();
        }
        private void submit()
        {
            string type = txtType.Text.GetDBFormatString();
            string query = " Insert into ItemCategory(CategoryName)values('" + type + "') ";
            if (txtType.Text != "")
            {
                DialogResult dialog = MessageBox.Show("Do you Want To Save? ", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    bool bl = SQLHelper.GetInstance().ExcuteQuery(query, out msg);
                    if (bl == true)
                    {

                        MessageBox.Show("your Data is Save Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show("your Data is Not Save ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                    MessageBox.Show("your Data is Not Save ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


            }

            else
            {
                MessageBox.Show("Please Give Type Of Item ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtType.Focus();
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string type = txtType.Text.GetDBFormatString();
            string query = "select * from ItemCategory where CategoryName='" + type + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);

            if (dt.Rows.Count <= 0)
            {
                submit();

            }
            else
                MessageBox.Show("Duplicate Entry ", "Dublicate", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            txtType.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtType.Text.ToLower());

        }

        private void ItemType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose!=null)
            {
                onclose(txtType.Text);
            }
        }
    }
}
