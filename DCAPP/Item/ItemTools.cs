using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DAPRO
{
    public static class ItemTools
    {
        public static Dictionary<string, string> _DicItem = new Dictionary<string, string>();
        public static Dictionary<string, string> _DicCategory = new Dictionary<string, string>();
        public static List<string> _LstSubCategory = new List<string>();
        public static List<string> _LstItemCompany = new List<string>();
        private static string msg = "";

        public static void GetItem()
        {
            _DicItem.Clear();
            string query = "Select ID,itemName from item order by itemName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["itemName"].ToString();

                    _DicItem.Add(id, name);
                }
            }
        }
        public static bool AddItem(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicItem.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicItem, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
        public static void GetItemCategory()
        {
            _DicCategory.Clear();
            string query = "select ID,CategoryName from ItemCategory order by CategoryName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string id = item["ID"].ToString();
                    string name = item["CategoryName"].ToString();

                    _DicCategory.Add(id, name);
                }
            }
        }
        public static bool AddItemCategory(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                (cmb.DataSource as BindingSource).Clear();
            }
            if (!_DicCategory.IsNullOrEmpty())
            {
                cmb.DataSource = new BindingSource(_DicCategory, null);
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Key";

                cmb.SelectedIndex = -1;

                return true;
            }
            return false;
        }
        public static void GetSubCategory()
        {
            _LstSubCategory.Clear();
            string query = "Select distinct SubCategory from item order by SubCategory";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string name = item["SubCategory"].ToString();

                    _LstSubCategory.Add(name);
                }
            }
        }
        public static bool AddSubCategory(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                cmb.Items.Clear();
            }
            if (_LstSubCategory.IsValidList())
            {
                cmb.Items.AddRange(_LstSubCategory.ToArray());
                return true;
            }
            return false;
        }
        public static void GetItemCompany()
        {
            _LstItemCompany.Clear();
            string query = "Select distinct Company from item order by Company";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string name = item["Company"].ToString();
                    _LstItemCompany.Add(name);
                }
            }
        }
        public static bool AddItemCompany(this ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                cmb.Items.Clear();
            }
            if (_LstItemCompany.IsValidList())
            {
                cmb.Items.AddRange(_LstItemCompany.ToArray());
                return true;
            }
            return false;
        }
        public static string GetItemHSNCode(string itemId)
        {
            string query = "Select ComodityCode from item where ID='" + itemId + "'";

            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        public static string GetUnitName(string itemid)
        {
            string query = "select UnitShortname from unit inner join item on unit.id= item.unitid where item.ID='" + itemid + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
        public static double GetItemSalesRate(string itemID)
        {
            string query = "select SalesRate from item where Id='" + itemID + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                try
                {
                    return Math.Round(double.Parse(obj.ToString()), OtherSettingTools._DecemalePlace);
                }
                catch (Exception)
                {
                }
            }
            return 0d;
        }
        public static string IsTaxBillByItem(string itemID, out string gstRate)
        {
            gstRate = "";
            string query = "Select GSTRate from item where Id='" + itemID + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                gstRate = obj.ToString();
                if (gstRate == "Exampted" || gstRate == "Nil" || gstRate == "Non GST")
                {
                    return "Bill of Supply";
                }
            }
            return "Regular";
        }
        public static void GetItemGSTRate(string itemID, out object cGSTRate, out object sGSTRate, out object iGSTRate, out object cessRate)
        {
            cGSTRate = ""; sGSTRate = ""; iGSTRate = ""; cessRate = "";
            string query = "Select * from item where Id='" + itemID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                cGSTRate = dt.Rows[0]["CGSTRate"];
                sGSTRate = dt.Rows[0]["SGSTRate"];
                iGSTRate = dt.Rows[0]["IGSTRate"];
                cessRate = dt.Rows[0]["CessRate"];
            }
        }
        public static void GetItemGSTRateAndAmount(string itemID, bool isIgst, double amount, out string cGSTRate, out string cGSTAmount, out string sGSTRate, out string sGSTAmount, out string iGSTRate, out string iGSTAmount, out string cessRate, out string cessAmount, out string totalWithTax)
        {
            cGSTRate = ""; cGSTAmount = "";
            sGSTRate = ""; sGSTAmount = "";
            iGSTRate = ""; iGSTAmount = "";
            cessRate = ""; cessAmount = "";
            totalWithTax = "";
            object cgst = 0, sgst = 0, igst = 0, cess = 0;
            double cTaxAmt = 0d, sTaxAmt = 0d, itaxAmt = 0d, cssTaxAmt = 0d;
            GetItemGSTRate(itemID, out cgst, out sgst, out igst, out cess);
            if (ORG_Tools._IsRegularGST)
            {
                if (!isIgst)
                {
                    if (cgst != null && cgst.GetType() == typeof(double))
                    {
                        double cRate = double.Parse(cgst.ToString());
                        cGSTRate = cRate.ToString();
                        cTaxAmt = (amount * cRate) / 100;
                        cGSTAmount = cTaxAmt.toString();
                    }
                    if (sgst != null && sgst.GetType() == typeof(double))
                    {
                        double sRate = double.Parse(sgst.ToString());
                        sGSTRate = sRate.ToString();
                        sTaxAmt = (amount * sRate) / 100;
                        sGSTAmount = sTaxAmt.toString();
                    }
                }
                else
                {
                    if (igst != null && igst.GetType() == typeof(double))
                    {
                        double iRate = double.Parse(igst.ToString());
                        iGSTRate = iRate.ToString();
                        itaxAmt = (amount * iRate) / 100;
                        iGSTAmount = itaxAmt.toString();
                    }
                }
                if (cess != null && cess.GetType() == typeof(double))
                {
                    double ccRate = double.Parse(cess.ToString());
                    cessRate = ccRate.ToString();
                    cssTaxAmt = (amount * ccRate) / 100;
                    cessAmount = cssTaxAmt.toString();
                }
            }
            totalWithTax = (amount + cTaxAmt + sTaxAmt + itaxAmt + cssTaxAmt).toString();
        }
        public static void GetItemGSTRateAndAmountForPurchase(string itemID, bool isIgst, bool isregular, double amount, out string cGSTRate, out string cGSTAmount, out string sGSTRate, out string sGSTAmount, out string iGSTRate, out string iGSTAmount, out string cessRate, out string cessAmount, out string totalWithTax)
        {
            cGSTRate = ""; cGSTAmount = "";
            sGSTRate = ""; sGSTAmount = "";
            iGSTRate = ""; iGSTAmount = "";
            cessRate = ""; cessAmount = "";
            totalWithTax = "";
            object cgst = 0, sgst = 0, igst = 0, cess = 0;
            double cTaxAmt = 0d, sTaxAmt = 0d, itaxAmt = 0d, cssTaxAmt = 0d;
            GetItemGSTRate(itemID, out cgst, out sgst, out igst, out cess);
            if (isregular)
            {
                if (!isIgst)
                {
                    if (cgst != null && cgst.GetType() == typeof(double))
                    {
                        double cRate = double.Parse(cgst.ToString());
                        cGSTRate = cRate.ToString();
                        cTaxAmt = (amount * cRate) / 100;
                        cGSTAmount = cTaxAmt.toString();
                    }
                    if (sgst != null && sgst.GetType() == typeof(double))
                    {
                        double sRate = double.Parse(sgst.ToString());
                        sGSTRate = sRate.ToString();
                        sTaxAmt = (amount * sRate) / 100;
                        sGSTAmount = sTaxAmt.toString();
                    }
                }
                else
                {
                    if (igst != null && igst.GetType() == typeof(double))
                    {
                        double iRate = double.Parse(igst.ToString());
                        iGSTRate = iRate.ToString();
                        itaxAmt = (amount * iRate) / 100;
                        iGSTAmount =itaxAmt.toString();
                    }
                }
                if (cess != null && cess.GetType() == typeof(double))
                {
                    double ccRate = double.Parse(cess.ToString());
                    cessRate = ccRate.ToString();
                    cssTaxAmt = (amount * ccRate) / 100;
                    cessAmount =cssTaxAmt.toString();
                }
            }
            totalWithTax = (amount + cTaxAmt + sTaxAmt + itaxAmt + cssTaxAmt).toString();
        }
        public static void GetItemGSTRateIsiGst(string itemID, bool isigst, out string cGSTRate, out string sGSTRate, out string iGSTRate, out string cessRate)
        {
            cGSTRate = ""; sGSTRate = ""; iGSTRate = ""; cessRate = "";
            string query = "Select * from item where Id='" + itemID + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                if (isigst != true)
                {
                    cGSTRate = dt.Rows[0]["CGSTRate"].ToString();
                    sGSTRate = dt.Rows[0]["SGSTRate"].ToString();
                }
                else
                {
                    iGSTRate = dt.Rows[0]["IGSTRate"].ToString();
                }
                cessRate = dt.Rows[0]["CessRate"].ToString();
            }
        }
    }
}
