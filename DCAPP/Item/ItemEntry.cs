using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

namespace DAPRO
{
    public partial class ItemEntry : Form
    {
        string msg = "";
        string mItemIDForEdit = "";
        public event Action<string> onclose;
        public ItemEntry()
        {
            InitializeComponent();
            chkITC.Checked = true;
            if (ORG_Tools._BusinessCatagory == "Goods & Services")
            {
                cmbType.Enabled = true;
            }
            else if (ORG_Tools._BusinessCatagory == "Goods")
            {
                cmbType.Enabled = false;
                cmbType.Text = ORG_Tools._BusinessCatagory;
            }
            else
            {
                cmbType.Enabled = false;
                cmbType.Text = ORG_Tools._BusinessCatagory;
            }
            cmbUnit.AddUnit();
            cmbCategory.AddItemCategory();
            cmbSubCategory.AddSubCategory();
        }
        public ItemEntry(string itemID)
        {
            InitializeComponent();
            mItemIDForEdit = itemID;
            if (ORG_Tools._BusinessCatagory == "Goods & Services")
            {
                cmbType.Enabled = true;
            }
            else if (ORG_Tools._BusinessCatagory == "Goods")
            {
                cmbType.Enabled = false;
                cmbType.Text = ORG_Tools._BusinessCatagory;
            }
            else
            {
                cmbType.Enabled = false;
                cmbType.Text = ORG_Tools._BusinessCatagory;
            }
            if (!ORG_Tools._IsRegularGST)
            {
                pnlTaxability.Enabled = false;
            }
            cmbUnit.AddUnit();
            cmbCategory.AddItemCategory();
            txtCompanyNAme.AutoCompleteCustomSource.AddRange(ItemTools._LstItemCompany.ToArray());
            ShowItemDetails(itemID);
        }
        private void ShowItemDetails(string itemID)
        {
            string query = "Select item.*,ItemCategory.CategoryName,Unit.UnitShortName from item " +
                           "left join ItemCategory on item.CategoryId = ItemCategory.ID " +
                           "left join Unit on item.UnitId = Unit.ID " +
                           "where item.ID='" + itemID + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                txtItemName.Text = dt.Rows[0]["TemplateName"].ToString();
                cmbType.Text = dt.Rows[0]["ItemType"].ToString();
                cmbCategory.Text = dt.Rows[0]["CategoryName"].ToString();
                cmbComodityNo.Text = dt.Rows[0]["ComodityCode"].ToString();
                cmbSubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
                cmbGstRate.Text = dt.Rows[0]["GSTRate"].ToString();
                txtCESSRate.Text = dt.Rows[0]["CessRate"].ToString();
                txtCgst.Text = dt.Rows[0]["CGSTRate"].ToString();
                txtSgst.Text = dt.Rows[0]["SGSTRate"].ToString();
                cmbUnit.Text = dt.Rows[0]["UnitShortName"].ToString();
                txtNaration.Text = dt.Rows[0]["Narration"].ToString();
                txtIGst.Text = dt.Rows[0]["IGSTRATE"].ToString();
                txtCompanyNAme.Text = dt.Rows[0]["Company"].ToString();
                txtTemplatNAme.Text = dt.Rows[0]["ItemName"].ToString();

                try
                {
                    chkRCM.Checked = Convert.ToBoolean(dt.Rows[0]["IsRCM"].ToString());
                    chkITC.Checked = Convert.ToBoolean(dt.Rows[0]["IsITC"].ToString());
                }
                catch (Exception)
                {
                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                if (!IsDuplicateTempleteName())
                {
                    DataSave();
                }
            }
        }
        private void DataSave()
        {
            #region Data
            string itemtype = cmbType.Text.GetDBFormatString();
            string itemName = txtItemName.Text.GetDBFormatString();
            string categoryid = cmbCategory.Text.ISNullOrWhiteSpace() ? "NULL" : ((KeyValuePair<string, string>)cmbCategory.SelectedItem).Key.ToString();
            string hsnorsaccode = cmbComodityNo.Text.GetDBFormatString();
            string gstrate = "NULL";
            string cessrate = "NULL";
            string unitcode = "NULL";
            string naration = "NULL";
            string cgst = "NULL";
            string sgst = "NULL";
            string igst = "NULL";
            string CompanyNAme = txtCompanyNAme.Text.GetDBFormatString();
            string templateName = txtTemplatNAme.Text.GetDBFormatString();
            string subcategory = cmbSubCategory.Text.GetDBFormatString();

            if (cmbType.Text == "Goods")
            {
                unitcode = ((KeyValuePair<string, string>)cmbUnit.SelectedItem).Key.ToString();
            }
            if (!txtNaration.Text.ISNullOrWhiteSpace())
            {
                naration = "'" + txtNaration.Text.GetDBFormatString() + "'";
            }
            if (!txtCESSRate.Text.ISNullOrWhiteSpace())
            {
                cessrate = "'" + txtCESSRate.Text.GetDBFormatString() + "'";
            }
            if (!cmbGstRate.Text.ISNullOrWhiteSpace())
            {
                gstrate = "'" + cmbGstRate.Text.GetDBFormatString() + "'";
            }
            if (!txtCgst.Text.ISNullOrWhiteSpace())
            {
                cgst = "'" + txtCgst.Text.GetDBFormatString() + "'";
            }
            if (!txtSgst.Text.ISNullOrWhiteSpace())
            {
                sgst = "'" + txtSgst.Text.GetDBFormatString() + "'";
            }
            if (!txtIGst.Text.ISNullOrWhiteSpace())
            {
                igst = "'" + txtIGst.Text.GetDBFormatString() + "'";
            }
            bool isRCM = chkRCM.Checked;
            bool isITC = chkITC.Checked;
            #endregion
            string query = "";
            if (!mItemIDForEdit.ISNullOrWhiteSpace())
            {
                query = "Update  item set itemName='" + templateName + "',itemType='" + itemtype +
                        "',Categoryid=" + categoryid + ",ComodityCode='" + hsnorsaccode +
                        "',GSTRate=" + gstrate + ",CessRate=" + cessrate + ",CGSTRate=" + cgst + ",sGSTRate="
                        + sgst + ",UnitId=" + unitcode + ",Narration=" + naration + ",IGSTRATE=" + igst
                        + ",Company='" + CompanyNAme + "',TemplateName='" + itemName + "',SubCategory='" +
                        subcategory + "',IsRCM='" + isRCM + "',IsITC='" + isITC + "'  " +
                        "where id='" + mItemIDForEdit + "' ";
            }
            else
            {
                query = "Insert into item(itemName,itemType,Categoryid,ComodityCode,GSTRate, " +
                        "CessRate,CGSTRate,sGSTRate,UnitId,Narration,IGSTRATE,Company, " +
                        "TemplateName,SubCategory,IsRCM,IsITC) values('" + templateName + "','" +
                        itemtype + "'," + categoryid + "," + "'" + hsnorsaccode + "'," +
                        gstrate + "," + cessrate + "," + cgst + "," + sgst + "," + unitcode
                        + "," + naration + "," + igst + ",'" + CompanyNAme + "','" + itemName
                        + "','" + subcategory + "','" + isRCM + "','" + isITC + "')";
            }
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                ItemTools.GetItem();
                ItemTools.GetItemCategory();
                ItemTools.GetSubCategory();
                ItemTools.GetItemCompany();
                this.Close();
            }
            else
            {
                MessageBox.Show("Internal error. \n" + msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsValidEntry()
        {
            if (txtItemName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Give Item Name", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemName.Focus();
                return false;
            }
            if (cmbType.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Which Type of Item", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbType.Focus();
                return false;
            }
            if (txtTemplatNAme.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please Give Item Name", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplatNAme.Focus();
                return false;
            }

            if (cmbType.Text == "Goods")
            {
                if (cmbUnit.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select UNIT.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbUnit.Focus();
                    return false;
                }
            }
            else
            {
                if (cmbGstRate.Text != "Exampted" && cmbGstRate.Text != "Non GST" && !cmbGstRate.Text.ISNullOrWhiteSpace())
                {
                    if (cmbComodityNo.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Please Give SAC Code", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbComodityNo.Focus();
                        return false;
                    }
                }
            }
            if (cmbGstRate.SelectedIndex < 0)
            {
                MessageBox.Show("Please select GST rate.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbGstRate.Focus();
                return false;
            }
            return true;
        }
        private bool IsDuplicateTempleteName()
        {
            string templatename = txtTemplatNAme.Text.GetDBFormatString();
            string query = "";
            if (mItemIDForEdit.ISNullOrWhiteSpace())
            {
                query = "Select ItemName from item where ItemName='" + templatename + "'";
            }
            else
            {
                query = "Select ItemName from item where ItemName='" + templatename
                        + "' and itemid<>'" + mItemIDForEdit + "'";
            }
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null && obj != DBNull.Value)
            {
                MessageBox.Show("Item alredy exist.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTemplatNAme.Focus();
                return true;
            }
            return false;
        }
        //private void txtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46)
        //    {
        //        if (txtSalesRate.Text.Length < 1 && e.KeyChar == '.')
        //        {
        //            e.Handled = true;
        //            txtSalesRate.Text = "0.";
        //            txtSalesRate.SelectionStart = txtSalesRate.Text.Length;


        //        }

        //        else
        //        {
        //            if (e.KeyChar == '.' && txtSalesRate.Text.Contains("."))
        //            {
        //                e.Handled = true;
        //            }

        //            else
        //                e.Handled = false;

        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please Enter Number only", "Number Only", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        e.Handled = true;
        //    }
        //}
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemCategory type = new ItemCategory();
            type.onclose += new Action<string>(type_onclose);
            type.ShowDialog();
        }
        void type_onclose(string obj)
        {
            cmbCategory.AddItemCategory();
            cmbCategory.Text = obj;
        }
        private void ItemEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose(txtTemplatNAme.Text);
            }

        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "Goods")
            {
                lblsachascode.Text = "HSN Code";
                //cmbComodityNo.AddComodityCode();
                cmbComodityNo.Text = "";
                pnlQuantity.Enabled = true;
            }
            else
            {
                lblsachascode.Text = "SAC Code";
                //txtQuantity.Clear();
                cmbUnit.SelectedIndex = -1;
                cmbComodityNo.Text = "";
                // cmbComodityNo.AddComodityCode();
                pnlQuantity.Enabled = false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtCESSRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46)
            {
                if (txtCESSRate.Text.Length < 1 && e.KeyChar == '.')
                {
                    e.Handled = true;
                    txtCESSRate.Text = "0.";
                    txtCESSRate.SelectionStart = txtCESSRate.Text.Length;
                }
                else
                {
                    if (e.KeyChar == '.' && txtCESSRate.Text.Contains("."))
                    {
                        e.Handled = true;
                    }

                    else
                        e.Handled = false;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Number only", "Number Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
        private void cmbGstRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbGstRate.Text.ISNullOrWhiteSpace())
            {
                pnlTax.Enabled = true;
                string gstStr = cmbGstRate.Text;
                if (gstStr != "Exampted" && (gstStr != "Non GST" && gstStr != "Nil"))
                {
                    float gst = float.Parse(gstStr) / 2;
                    txtCgst.Text = gst.ToString();
                    txtSgst.Text = gst.ToString(); txtIGst.Text = gstStr;
                    txtIGst.Text = gstStr;
                }
                else
                {
                    pnlTax.Enabled = false;
                    chkRCM.Checked = false;
                    chkITC.Checked = false;
                    txtCgst.Clear();
                    txtSgst.Clear();
                    txtIGst.Clear();
                    txtCESSRate.Clear();
                }
            }
        }
        private void btnAddunit_Click(object sender, EventArgs e)
        {
            UnitEntry unitentry = new UnitEntry();
            unitentry.onclose += Unitentry_onclose;
            unitentry.ShowDialog();
        }
        private void Unitentry_onclose(string obj)
        {
            cmbUnit.AddUnit();
            cmbUnit.Text = obj.ToString();
        }
        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyNAme.Text.ISNullOrWhiteSpace())
            {
                txtTemplatNAme.Text = txtItemName.Text;
            }
            else
            {
                txtTemplatNAme.Text = txtItemName.Text + " [" + txtCompanyNAme.Text + "]";
            }
        }
        private void txtCompanyNAme_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyNAme.Text.ISNullOrWhiteSpace())
            {
                txtTemplatNAme.Text = txtItemName.Text;
            }
            else
            {
                txtTemplatNAme.Text = txtItemName.Text + " [" + txtCompanyNAme.Text + "]";
            }
        }

        private void cmbComodityNo_Leave(object sender, EventArgs e)
        {
            FindComoditicode();
        }

        private void FindComoditicode()
        {

        }
    }
}
