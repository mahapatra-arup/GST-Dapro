using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class UnitEntry : Form
    {
        string msg = "";
        public event Action<string> onclose;
        public UnitEntry()
        {
            InitializeComponent();
        }
        private bool IsvalidEntry()
        {
            if (txtUnitFullName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter unit full name", "Unit Of Masure", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUnitFullName.Focus();
                return false;

            }
            if (txtUnitShortName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter unit short name", "Unit Of Masure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnitShortName.Focus();
                return false;
            }
            return true;

        }
        private bool IsDuplicateEntry()
        {
            string Unitname = txtUnitFullName.Text.GetDBFormatString();
            string query = "select  UnitFullName from Unit where UnitFullName='" + Unitname + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null && obj != DBNull.Value)
            {
                MessageBox.Show("Duplicate unit found.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUnitFullName.Focus();
                return false;
            }
            string UnitCode = txtUnitShortName.Text.GetDBFormatString();
            string query2 = "Select  UnitFullName from Unit where UnitShortName='" + UnitCode + "'";
            object obj2 = SQLHelper.GetInstance().ExcuteScalar(query2, out msg);
            if (obj2 != null && obj2 != DBNull.Value)
            {
                MessageBox.Show("Duplicate unit found.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUnitShortName.Focus();
                return false;
            }
            return true;
        }
        private void DataSave()
        {
            string unitname = txtUnitFullName.Text.GetDBFormatString();
            string unitcode = txtUnitShortName.Text.GetDBFormatString();
            string query = "Insert into unit values('" + unitname + "','" + unitcode + "') ";
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                UnitTools.GetUnit();
                this.Close();
            }
            else
            {
                MessageBox.Show("Internal Error.", "Unit Of Masure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtUnitFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == 40 || e.KeyChar == 41 || e.KeyChar == 44 || e.KeyChar == 45 || e.KeyChar == 46))
            {
                MessageBox.Show("Please enter charecters only", "Charecters Only", MessageBoxButtons.OK, MessageBoxIcon.Information);

                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
                if (IsDuplicateEntry())
                {
                    DataSave();
                }
            }
        }
        private void UnitEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose(txtUnitShortName.Text);
            }
        }
        private void txtUnitFullName_TextChanged(object sender, EventArgs e)
        {
            if (txtUnitFullName.Text.Length <= 3)
            {
                txtUnitShortName.Text = txtUnitFullName.Text;
            }
            //else
            //{
            //    txtUnitShortName.Text = txtUnitFullName.Text.Substring(1, 3);
            //}
        }
    }

}
