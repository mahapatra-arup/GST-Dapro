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
    public partial class Summary : Form
    {
        string msg = "";
        public Summary()
        {
            InitializeComponent();
            GenarateSummary();
        }
        private void GenarateSummary()
        {
            string query = "Select item.ItemName, ItemID,highestunit,PurchaseRate,SUM(HighestStockQty) as Totalqty, " +
                           "SUM((HighestStockQty) * PurchaseRate) as totalamount  from stockSummary " +
                           "inner join item on item.ID = StockSummary.ItemID " +
                           "group by highestunit,ItemID,PurchaseRate,item.ItemName order by itemid";
            DataTable dt1 = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            {
                string pritemid = dt1.Rows[0]["ItemID"].ToString();
                string itemname = dt1.Rows[0]["ItemName"].ToString();
                // string itemname = "";
                string qtywithunit = "";
                string ratewithunit = "";
                string unit = "";
                string qty = "";
                string prate = "";
                foreach (DataRow item1 in dt1.Rows)
                {
                    string itemid = item1["itemid"].ToString();

                p: if (itemid == pritemid)
                    {
                        itemname = item1["ItemName"].ToString();
                        unit = item1["highestunit"].ToString();
                        qty = item1["Totalqty"].ToString();
                        prate= item1["PurchaseRate"].ToString().toRound();
                        ratewithunit = ratewithunit + prate + " / " + unit + "\n"; 
                        qtywithunit = qtywithunit + qty + " / " + unit + "\n";
                    }
                    else
                    {
                        string qury1 = "Select sum((HighestStockQty) * PurchaseRate) as totalAmount  from stockSummary " +
                                       "inner join item on item.ID = StockSummary.ItemID  "+
                                       "where StockSummary.ItemID = '" + pritemid + "'";
                        object obj = SQLHelper.GetInstance().ExcuteScalar(qury1, out msg);
                        if (obj.ISValidObject())
                        {
                            dgvSummary.Rows.Add(pritemid, itemname, qtywithunit, ratewithunit, obj.toRound());
                            qtywithunit = "";
                            ratewithunit = "";
                        }
                        pritemid = itemid;
                        goto p;
                    }
                }
                string qury13 = "Select sum((HighestStockQty) * PurchaseRate) as totalAmount  from stockSummary " +
                                "inner join item on item.ID = StockSummary.ItemID  "+
                                "where StockSummary.ItemID = '" + pritemid + "'";
                object obj2 = SQLHelper.GetInstance().ExcuteScalar(qury13, out msg);
                if (obj2.ISValidObject())
                {
                    dgvSummary.Rows.Add(pritemid, itemname, qtywithunit, ratewithunit, obj2.toRound());
                }
            }
        }
    }
}
