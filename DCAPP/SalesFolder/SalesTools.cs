using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class SalesTools
    {
        private static string msg = "";

        public static bool GetOrderNos(this ListBox lst, string ledgerid)
        {
            Dictionary<string, string> dicOrderno = new Dictionary<string, string>();
            string query = "select SlNo,CustomerOrderNo,OrderDate from SalesOrder Where Ledgerid='" + ledgerid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string orderdate = DateTime.Parse(item["OrderDate"].ToString()).ToString("dd-MMM-yyy");
                    string customerorderno = item["CustomerOrderNo"].ToString();
                    string id = item["SlNo"].ToString();
                    string name = id + " #" + customerorderno + " # " + orderdate;

                    dicOrderno.Add(id, name);
                }
                if (!dicOrderno.IsNullOrEmpty())
                {
                    lst.DataSource = new BindingSource(dicOrderno, null);
                    lst.DisplayMember = "Value";
                    lst.ValueMember = "Key";

                    lst.SelectedIndex = -1;
                    return true;
                }
            }
            return false;
        }
    }
}
