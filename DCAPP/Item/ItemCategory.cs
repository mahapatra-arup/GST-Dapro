using System;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class ItemCategory : Form
    {
        string msg = "";
        public event Action<string> onclose;
        public ItemCategory()
        {
            InitializeComponent();
        }
        private void SavaData()
        {
            string type = txtType.Text.GetDBFormatString();
            string query = " Insert into ItemCategory(CategoryName)values('" + type + "') ";
            if (!txtType.Text.ISNullOrWhiteSpace())
            {
                if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
                {
                    ItemTools.GetItemCategory();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data not save.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter category.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtType.Focus();
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string type = txtType.Text.GetDBFormatString();
            string query = "Select * from ItemCategory where CategoryName='" + type + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);

            if (dt.Rows.Count <= 0)
            {
                SavaData();
            }
            else
                MessageBox.Show("Duplicate category found.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void txtType_Leave(object sender, EventArgs e)
        {
            txtType.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtType.Text.ToLower());

        }
        private void ItemType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose(txtType.Text);
            }
        }
    }
}
