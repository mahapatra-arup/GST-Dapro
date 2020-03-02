using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class ExpenseTools
    {
        static string msg = "";
        public static Dictionary<string, string> _DicExpenseHead = new Dictionary<string, string>();
        public static void GetExpenseAccountHead()
        {
            _DicExpenseHead.Clear();
            string query = "SELECT AccountHead.AccountHeadID,AccountHead.AccountHeadName FROM AccountHead " +
                           "inner join GroupSub on AccountHead.GroupSubID=GroupSub.GroupSubID " +
                           "where GroupSub.GroupName in ('" + Groups.GroupSubEnum.EXPENSE.ToString() + "') and Category in('Supplier','Employee','Customer') " +
                           "order by AccountHead. AccountHeadName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string accountHeadID = item["AccountHeadID"].ToString();
                    string accountHeadName = item["AccountHeadName"].ToString();
                    string acHeadAndGroup = accountHeadName;
                    _DicExpenseHead.Add(accountHeadID, acHeadAndGroup);
                }
            }
        }
        public static bool AddExpenseAccountHead(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicExpenseHead.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicExpenseHead, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
    }
}
