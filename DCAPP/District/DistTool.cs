using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public static class DistTool
    {
        public static Dictionary<string, string> _DicDistrict = new Dictionary<string, string>();
        private static string msg = "";

        public static void GetDist()
        {
            _DicDistrict.Clear();
            string query = "Select ID,DistName from District order by DistName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["DIstName"].ToString();

                    _DicDistrict.Add(id, name);
                }
            }
        }
        public static void AddDist(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicDistrict.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicDistrict, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;
            }
        }
        public static void AddDist(this ComboBox cmb, string stateID)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            _DicDistrict.Clear();
            string query = "Select ID,DistName from District where StateID=" + stateID + " order by DistName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["DIstName"].ToString();

                    _DicDistrict.Add(id, name);
                }
            }
            if (!_DicDistrict.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicDistrict, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;
            }
        }
    }
}
