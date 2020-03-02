using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class BillClas
    {
        static string msg = "";
        public static Dictionary<string, string> _DicAcHeadForBill = new Dictionary<string, string>();
        public static void GetAccountHeadForExpense()
        {
            _DicAcHeadForBill.Clear();
            string query = "SELECT AccountHead.AccountHeadID,AccountHead.AccountHeadName,GroupSub.GroupName FROM AccountHead " +
                           "Inner join GroupSub on AccountHead.GroupSubID=GroupSub.GroupSubID " +
                           "where GroupSub.GroupName in ('" + Groups.GroupSubEnum.EXPENSE.ToString() + "','" + Groups.GroupSubEnum.FIXED_ASSETS.ToString() + "') " +
                           "order by AccountHead. AccountHeadName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query,out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string accountHeadID = item["AccountHeadID"].ToString();
                    string accountHeadName = item["AccountHeadName"].ToString();
                    string subGroup= item["GroupName"].ToString();
                    string acHeadAndGroup = accountHeadName + "    [" + subGroup + "]";
                    _DicAcHeadForBill.Add(accountHeadID, acHeadAndGroup);
                }
            }
        }
        public static bool AddAccountHeadForExpense(this ComboBox cmbBox)
        {
            if (cmbBox.Items.Count > 0)
            {
                (cmbBox.DataSource as BindingSource).Clear();
            }
            if (!_DicAcHeadForBill.IsNullOrEmpty())
            {
                cmbBox.DataSource = new BindingSource(_DicAcHeadForBill, null);
                cmbBox.DisplayMember = "Value";
                cmbBox.ValueMember = "Key";
                cmbBox.SelectedIndex = -1;
                return true;
            }
            return false;
        }
    }
}
