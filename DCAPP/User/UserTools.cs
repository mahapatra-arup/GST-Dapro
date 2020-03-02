using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class UserTools
    {
        public static string _UserID = "", _UserName="";
        public static string[] _Controls = new string[20];
        public static bool _IsDelete, _IsEdit, _IsCancel;
        public static Dictionary<string, string> _DicUserName = new Dictionary<string, string>();
        private static string msg = "";
        public static void GetUserName()
        {
            _DicUserName.Clear();
            string query = "select UserID,UserName,IsDelete,IsEdit,IsCancel from UserControl order by UserName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["UserID"].ToString();
                    string UnName = item["UserName"].ToString();
                    
                    _DicUserName.Add(id, UnName);
                }
            }
        }
        public static bool AddUserName(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicUserName.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicUserName, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
                return true;
            }
            return false;
        }
        public static void GetEditDeletePermision()
        {
            string query = "select IsDelete,IsEdit,IsCancel from UserControl "+
                           "Where UserID='"+_UserID+"' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    _IsDelete = item["IsDelete"].ToString() == "True" ? true : false;
                    _IsEdit = item["IsEdit"].ToString() == "True" ? true : false;
                    _IsCancel = item["IsCancel"].ToString() == "True" ? true : false;
                }
            }
        }
    }
}
