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
    public partial class PermissionWindows : Form
    {
        string checkbox = string.Empty;

        string msg="";
        CheckBox box = new CheckBox();

        public PermissionWindows()
        {
            InitializeComponent();
            DefaultClass df = new DefaultClass();
            CmbUserName.AddUserName();
            Getchkbox();
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
        private void RetrivePermission()
        {
            for (int i = 0; i < chklistbox.Items.Count; i++)
            {
                chklistbox.SetItemChecked(i, false);
            }
            string userid = string.Empty;
            if (CmbUserName.SelectedIndex>=0)
            {
                 userid = ((KeyValuePair<string, string>)CmbUserName.SelectedItem).Key.ToString();
            }
            string query = "Select Permision,IsDelete,IsEdit,IsCancel from usercontrol "+
                           "where userid='" + userid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string sbtn = dt.Rows[0]["Permision"].ToString();
                chkDelete.Checked = dt.Rows[0]["IsDelete"].ToString() == "True" ? true : false;
                chkEdit.Checked = dt.Rows[0]["IsEdit"].ToString() == "True" ? true : false;
                ChkCancel.Checked = dt.Rows[0]["IsCancel"].ToString() == "True" ? true : false;
                string[] btnarry = sbtn.Split(',');
                foreach (object i in btnarry)
                {
                    for (int j = 0; j < chklistbox.Items.Count; j++)
                    {
                        if (chklistbox.Items[j].Equals(i))
                        {
                            chklistbox.SetItemChecked(j,true);
                        }
                    }
                }
            }
        }
        private void CmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrivePermission();
        }
        private void DataSave()
        {
            CheckUnCheck();
            string userid = string.Empty;
            if (CmbUserName.SelectedIndex >= 0)
            {
                userid = ((KeyValuePair<string, string>)CmbUserName.SelectedItem).Key.ToString();

            }
            string query = "update usercontrol set permision='" + checkbox + "',Isdelete='"+chkDelete.Checked+"',IsEdit='"+chkEdit.Checked+"',IsCancel='"+ChkCancel.Checked+"' where userid='" + userid + "'";
            bool bl = SQLHelper.GetInstance().ExcuteQuery(query, out msg);
            if (bl == true)
            {
                MessageBox.Show("Permission Success fully Updated", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserTools.GetEditDeletePermision();
                CmbUserName.SelectedIndex = -1;
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
                DataSave();
            }
        }
        private bool IsValidEntry()
        {
            int j = 0;
            if (CmbUserName.Text.ISNullOrWhiteSpace())
            {
                toolTip1.Show("Select a user for change permission", CmbUserName, CmbUserName.Location.X, CmbUserName.Height - 100, 1500);
                //MessageBox.Show("Select a user for change permission", "Permissin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CmbUserName.Focus();
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
            if (j==0)
            {
                toolTip1.Show("Please Give atleast One permission ", chklistbox, 80, chklistbox.Height - 250, 1500);
                //MessageBox.Show("Please Give atleast One permission", "Permissin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                chklistbox.Focus();
                return false;
            }
            return true;
        }
      
    }
}
