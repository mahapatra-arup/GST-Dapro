using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class Groups
    {
        public static string msg = "";
        public enum GroupHeadEnum
        {
            FUND_AND_LIABILITIES,
            PROPERTIES_AND_ASSETS,
            EXPENSE,
            INCOME
        }

        public static Dictionary<string, string> HeadGroupsDic = new Dictionary<string, string>();
        public static void InitHeadGroups()
        {
            HeadGroupsDic.Add("1", "FUND_AND_LIABILITY");
            HeadGroupsDic.Add("2", "PROPERTIES_AND_ASSETS");
            HeadGroupsDic.Add("3", "EXPENSE");
            HeadGroupsDic.Add("4", "INCOME");
        }
        public static bool AddHeadGroups(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            cmb.DataSource = new BindingSource(HeadGroupsDic, null);
            cmb.DisplayMember = "Value";
            cmb.ValueMember = "Key";
            return true;
        }
        public static string GetHeadGroupID(string headGroupName)
        {
            string query = "Select HeadGroupID from HeadGroup where GroupName='" + headGroupName.GetDBFormatString() + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        public static string GetMainGroupNameBySubGroup(string subGroup)
        {
            string query = "Select GroupName from Groups where GroupID= " +
                           "(Select GroupID from GroupSub where SubGroup='" + subGroup + "')";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        /// <summary>
        /// Sub Groups
        /// </summary>
        /// <param name="subGroupName"></param>
        /// <returns></returns>
        /// 
        public enum GroupSubEnum
        {
            CURRENT_LIABILITIES,
            CURRENT_ASSETS,
            FIXED_ASSETS,
            EXPENSE,
            INCOME,
            DEPRECIATION,
            JURNAL,
            INVESTMENT,
            STUDENT_FEES,
            SUNDRY_CREDITORS
        }
        public static DataTable GetSubGroup(string headGroupID)
        {
            string query = "Select * from SubGroup where HeadGroupID=" + headGroupID + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                return dt;
            }
            return null;
        }
        public static bool GetSubGroups(this ComboBox cmb, string headGroupID)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            string sql = "Select SubGroupID,GroupName from SubGroup where HeadGroupID='" + headGroupID + "' order by GroupName";
            List<string> lstSection = new List<string>();
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(sql, out msg);
            Dictionary<string, string> dicSecWithSelect = new Dictionary<string, string>();
            if (dt.IsValidDataTable())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string secID = row["SubGroupID"].ToString();
                    string secName = row["GroupName"].ToString();
                    dicSecWithSelect.Add(secID, secName);
                }
                cmb.DataSource = new BindingSource(dicSecWithSelect, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";
                cmb.SelectedIndex = -1;
                return true;
            }

            return false;
        }
        public static string GetSubGroupID(string subGroupName)
        {
            string query = "Select GroupSubID from GroupSub where GroupName='" + subGroupName.GetDBFormatString() + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        public static bool IsNotDuplicateSubGroup(string subGroup)
        {
            string query = "Select SubGroupID from SubGroup where GroupName='" + subGroup.GetDBFormatString() + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                MessageBox.Show("Sub group already exist.", "Found duplicate sub group", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        public static bool IsNotDuplicateSubGroupWhenEdit(string subGroup, string subGroupID)
        {
            string query = "Select SubGroupID from SubGroup " +
                           "where GroupName='" + subGroup.GetDBFormatString() + "' and SubGroupID<>" + subGroupID + "";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                MessageBox.Show("Sub group already exist.", "Found duplicate sub group", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        public static string GetGroupSubHeadID(string GroupName)
        {
            string query = "Select SubGroupID from GroupSubHead where GroupName='" + GroupName + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }

        /// <summary>
        /// Sub Sub Groups
        /// </summary>
        /// <param name="subSubGroupName"></param>
        /// <returns></returns>
        /// 
        public enum GroupSubSubEnum
        {
            BANK_ACCOUNT,
            CASH_IN_HAND,
            LOAN_AND_ADVANCE,
            OUTSTANDING_LIABILITIES
        }
        public static string GetSubSubGroupID(string subSubGroupName)
        {
            string query = "Select GroupSubSubID from GroupSubSub where GroupName='" + subSubGroupName.GetDBFormatString() + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
    }
}

