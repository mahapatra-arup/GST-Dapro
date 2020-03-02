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
    public partial class CreateDesignation : Form
    {
        public event Action<string> OnClose;
        private string msg = "";
        string mdesignation="";
        public CreateDesignation()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataSave()
        {
             mdesignation = txtDesignation.Text.GetDBFormatString();
            string query = "Insert Into EmployeeDesignation(DesignationName) values('" + mdesignation + "')";
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                DefaultClass cls = new DefaultClass();
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtDesignation.Text.ISNullOrWhiteSpace())
            {
                string designation = txtDesignation.Text.GetDBFormatString();
                if (!IsDuplicate(designation))
                {
                    DataSave();
                }
            }
        }

        private bool IsDuplicate(string desig)
        {
            string query = "Select DesignationName from Designation where DesignationName='" + desig + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                MessageBox.Show("Designation already exist.", "Duplicat", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDesignation.Focus();
                return true;
            }
            return false;
        }

        private void CreateDesignation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose!=null)
            {
                OnClose(mdesignation);
            }
        }

        
    }
}
