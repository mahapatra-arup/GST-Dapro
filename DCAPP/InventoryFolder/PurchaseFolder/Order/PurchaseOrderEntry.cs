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
    public partial class PurchaseOrderEntry : Form
    {
        long mSerialNo = OtherSettingTools._PurchaseOrderSerialStart.ISNullOrWhiteSpace() ? 1 : long.Parse(OtherSettingTools._PurchaseOrderSerialStart);
        private string msg = "";
        public event Action OnClose;
        private string mOrderIDForEdit = "";
        private List<string> mLstQuery = new List<string>();
        public PurchaseOrderEntry()
        {
            InitializeComponent();
            this.FitToVertical();
            //DefaultClass df = new DefaultClass();
            cmbSupplierName.AddSuppliers();
            cmbItemName.AddItem();
            cmbUnit.AddUnit();
            cmbStateShipping.AddState();
            GenerateSlNo();
            GetOrgAddressDetails();
        }
        public PurchaseOrderEntry(string orderID, string status, string StatusForchallan)
        {
            InitializeComponent();
            //DefaultClass df = new DefaultClass();

            mOrderIDForEdit = orderID;
            this.FitToVertical();
            cmbSupplierName.AddSuppliers();
            cmbItemName.AddItem();
            cmbUnit.AddUnit();
            cmbStateShipping.AddState();
            GetOrgAddressDetails();
            ViewExistingDataMain();
            ReadOnlyAllControl(status, StatusForchallan);

        }
        private void ReadOnlyAllControl(string status, string StatusForchallan)
        {
            if (status == "Close" || StatusForchallan == "Open")
            {
                foreach (Control item in this.Controls)
                {
                    item.Enabled = false;
                }
            }
            btnCancel.Enabled = true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.A))
            {
                btnNewSupplier_Click(btnNewSupplier, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.A))
            {
                #region MyRegion
                if (!cmbSupplierName.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        string supplierID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
                        LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog, supplierID);
                        frm.OnClose += Frm_OnClose;
                        frm.ShowDialog();
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("Select supplier name.", "Supplier Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                } 
                #endregion
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.I)) ///Add New Item
            {
                btnItemADD_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Shift | Keys.I)) ///Edit Item
            {
                #region Edit Item
                if (!cmbItemName.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                        ItemEntry frm = new ItemEntry(itemID);
                        frm.onclose += item_onclose;
                        frm.ShowDialog();
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("Select item name.", "Item Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cmbCustomerName.Select();
                }
                #endregion
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void GenerateSlNo()
        {
            //int slno = 1;
            string query = "Select max(SlNo) as slno from PurchaseOrder ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                try
                {
                    mSerialNo = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblSlNo.Text = OtherSettingTools._PurchaseOrderStart + mSerialNo.ToString();
        }
        private void ViewExistingDataMain()
        {
            string query = "Select PurchaseOrder.*,LadgerMain.LadgerName, Convert(varchar(11),DeliveryDate,106) as dDate from PurchaseOrder inner join LadgerMain on PurchaseOrder.LedgerID=LadgerMain.LadgerID " +
                           "where OrderId='" + mOrderIDForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                lblSlNo.Text = dt.Rows[0]["PurchaseOrderNo"].ToString();
                cmbSupplierName.Text = dt.Rows[0]["LadgerName"].ToString();
                txtEstimateNo.Text = dt.Rows[0]["Estimateno"].ToString();
                dtpDate.Text = dt.Rows[0]["OrderDate"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtNameShipping.Text = dt.Rows[0]["ShippingTo"].ToString();
                txtAddressShiping.Text = dt.Rows[0]["ShippingAddress"].ToString();
                txtCityShipping.Text = dt.Rows[0]["ShippingTown"].ToString();
                cmbStateShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                cmbDistShipping.Text = dt.Rows[0]["ShippingDist"].ToString();
                txtPinShiping.Text = dt.Rows[0]["ShippingPin"].ToString();
                txtContactNoShipping.Text = dt.Rows[0]["ShippingContactNo"].ToString();
                txtExpectedDeliveryDate.Text = dt.Rows[0]["dDate"].ToString();

                ViewExistingDataDetails();
            }
        }
        private void ViewExistingDataDetails()
        {
            dgvItemList.Rows.Clear();
            string query = "Select PurchaseOrderDetails.* from PurchaseOrderDetails " +
                           "inner join PurchaseOrder on PurchaseOrderDetails.OrderId=PurchaseOrder.OrderId " +
                           "where PurchaseOrder.OrderId='" + mOrderIDForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string comodityCode = item["ComodityCode"].ToString();
                    string itemID = item["ItemId"].ToString();
                    string itemName = item["ItemName"].ToString();
                    string qtyStr = item["Qty"].ToString();
                    string unit = item["Unit"].ToString();

                    dgvItemList.Rows.Add(itemID, mDescriptionSlno, comodityCode, itemName, qtyStr, unit);

                    DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
                    btnCelCol.ToolTipText = "Delete";
                    btnCelCol.Value = "Delete";
                    btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
                    //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
                    dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
                    mDescriptionSlno++;
                }
            }
        }
        private bool IsValidSelection()
        {
            if (cmbSupplierName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select a Supplier name.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSupplierName.Select();
                return false;
            }
            if (txtNameShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please shipping name.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtNameShipping.Select();
                return false;
            }
            if (txtAddressShiping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter shipping address.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAddressShiping.Select();
                return false;
            }
            if (txtCityShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter shipping city.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCityShipping.Select();
                return false;
            }
            if (cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select shipping state.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbStateShipping.Select();
                return false;
            }
            if (cmbDistShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select shipping dist.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbDistShipping.Select();
                return false;
            }
            if (txtPinShiping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter shipping pin.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPinShiping.Select();
                return false;
            }
            if (txtContactNoShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter shipping contact.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtContactNoShipping.Select();
                return false;
            }
            else
            {
                if (txtContactNoShipping.Text.Length != 10 && txtContactNoShipping.Text.Length != 12)
                {
                    MessageBox.Show("Please enter valid contact No.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtContactNoShipping.Select();
                    return false;

                }

            }
            if (dgvItemList.Rows.Count <= 0)
            {
                MessageBox.Show("Please add at least one item.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
                return false;
            }
            return true;
        }
        private bool IsValidData()
        {
            if (cmbItemName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select any item.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
                return false;
            }
            if (cmbQuantity.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter quantity.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbQuantity.Focus();
                return false;
            }

            return true;
        }
        private bool IsDuplicateItemSelect()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemId = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemExist = row.Cells["ItemName"].Value.ToString();
                if (itemName == itemExist)
                {
                    MessageBox.Show("Found duplicate item in below list.", "Item Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbItemName.Select();
                    return true;
                }
            }
            return false;
        }
        private void ResetData()
        {
            mDescriptionSlno = 1;
            GenerateSlNo();
            mOrderIDForEdit = "";
            dgvItemList.Rows.Clear();
            cmbItemName.SelectedIndex = -1;
            cmbQuantity.Text = "";
            cmbUnit.SelectedIndex = -1;
            txtEstimateNo.Clear();
            cmbSupplierName.Text = "";
        }
        private void ResetAddressData()
        {


            txtNameShipping.Text = "";
            txtAddressShiping.Text = "";
            txtCityShipping.Text = "";
            cmbStateShipping.SelectedIndex = -1;
            cmbDistShipping.SelectedIndex = -1;
            txtPinShiping.Text = "";
            txtContactNoShipping.Text = "";
        }
        private int mDescriptionSlno = 1;
        private void DescriptionAdd()
        {
            string itemName = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Value.ToString();
            string itemID = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string hsnCode = ItemTools.GetItemHSNCode(itemID);
            string unit = cmbUnit.Text;
            string qty = cmbQuantity.Text;

            dgvItemList.Rows.Add(itemID, mDescriptionSlno, hsnCode, itemName, qty, unit);
            DataGridViewButtonCell btnCelCol = new DataGridViewButtonCell();
            btnCelCol.ToolTipText = "Delete";
            btnCelCol.Value = "Delete";
            btnCelCol.Style.SelectionBackColor = Color.AntiqueWhite;
            //btnCelCol.InheritedStyle.SelectionBackColor = Color.AntiqueWhite;
            dgvItemList.Rows[mDescriptionSlno - 1].Cells["btnDelete"] = btnCelCol;
            mDescriptionSlno++;
        }
        private void OrderSave()
        {
            #region Data
            mLstQuery.Clear();
            string orderID = Guid.NewGuid().ToString();
            string slNo = mSerialNo.ToString();
            string purchaseOrderNo = lblSlNo.Text.GetDBFormatString();
            string ledgerID = ((KeyValuePair<string, string>)cmbSupplierName.SelectedItem).Key.ToString();
            string orderDate = dtpDate.Text;
            string description = "NULL";
            string status = "Open";
            string statusForchallan = "Close";
            string estimateNo = txtEstimateNo.Text.GetDBFormatString();
            string query = "";
            string deliverydate = "NULL";
            if (!txtDescription.Text.ISNullOrWhiteSpace())
            {
                description = "'" + txtDescription.Text.GetDBFormatString() + "'";
            }
            if (!txtExpectedDeliveryDate.Text.ISNullOrWhiteSpace())
            {
                deliverydate = "'" + DateTime.Parse(txtExpectedDeliveryDate.Text.GetDBFormatString()).ToString("dd-MMM-yyyy") + "'";
            }

            /// Shipping Details
            string nameShipping = txtNameShipping.Text.GetDBFormatString();
            string addressShipping = txtAddressShiping.Text.GetDBFormatString();
            string cityShipping = txtCityShipping.Text.GetDBFormatString();
            string distShipping = ((KeyValuePair<string, string>)cmbDistShipping.SelectedItem).Value.ToString();
            string stateShipping = ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Value.ToString();
            string pinShipping = txtPinShiping.Text;
            string contactNoShipping = txtContactNoShipping.Text;

            #endregion

            #region Query
            if (mOrderIDForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into PurchaseOrder(OrderId, SlNo, PurchaseOrderNo, LedgerID, OrderDate, " +
                        "Description, Status, ShippingTo, ShippingAddress, ShippingTown, ShippingDist, " +
                        "ShippingState, ShippingPin, ShippingContactNo,Estimateno,DeliveryDate,StatusForchallan) " +
                        "Values('" + orderID + "'," + slNo + ",'" + purchaseOrderNo + "','" + ledgerID + "','" + orderDate
                        + "'," + description + ",'" + status + "','" +
                        nameShipping + "','" + addressShipping + "','" + cityShipping + "','" + distShipping + "','" +
                        stateShipping + "','" + pinShipping + "','" + contactNoShipping + "','" + estimateNo + "'," + deliverydate + ",'" + statusForchallan + "')";
                mLstQuery.Add(query);
            }
            else
            {
                query = "Update PurchaseOrder set LedgerID='" + ledgerID + "',Description=" + description + ",Estimateno='" + estimateNo + "',  OrderDate='" + orderDate + "'," +
                        "ShippingTo='" + nameShipping + "',ShippingAddress='" + addressShipping +
                        "',ShippingTown='" + cityShipping + "',ShippingDist='" + distShipping + "',ShippingState='" +
                        stateShipping + "',ShippingPin='" + pinShipping + "',ShippingContactNo='" + contactNoShipping +
                        "' where OrderId='" + mOrderIDForEdit + "'";
                mLstQuery.Add(query);
                query = "Delete from PurchaseOrderDetails where OrderId='" + mOrderIDForEdit + "'";
                mLstQuery.Add(query);
                orderID = mOrderIDForEdit;
            }
            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string itemID = row.Cells["ItemID"].Value.ToString();
                string itemName = row.Cells["ItemName"].Value.ToString();
                string comodityCode = "NULL";
                if (row.Cells["HSN"].Value != null)
                {
                    comodityCode = "'" + row.Cells["HSN"].Value.ToString() + "'";
                }
                string qty = row.Cells["Quantity"].Value.ToString();
                string unit = row.Cells["Unit"].Value.ToString();
                query = "Insert into PurchaseOrderDetails(OrderId, ComodityCode,ItemId,ItemName, Qty, Unit,DueQty)" +
                        "Values('" + orderID + "'," + comodityCode + "," + itemID + ",'" + itemName + "'," + qty +
                        ",'" + unit + "'," + qty + ")";
                mLstQuery.Add(query);
            }
            #endregion

            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
            {
                MessageBox.Show("Order saved.", "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (mOrderIDForEdit.ISNullOrWhiteSpace())
                {
                    ResetData();
                    //ResetAddressData();
                    cmbSupplierName.SelectedIndex = -1;
                    cmbSupplierName.Select();
                    OtherSettingTools._IsPurxhaseOrderBillgenarate = true;
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("internully problem \n" + msg, "Order Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            #endregion
        }
        private void GetOrgAddressDetails()
        {
            #region Billing
            lblBillingName.Text = ORG_Tools._NameBilling;
            lblBillingAddress.Text = ORG_Tools._AddressBilling;
            lblBillingTown.Text = ORG_Tools._CityTownBilling;
            lblBillingState.Text = ORG_Tools._StateBilling;
            lblBillingDist.Text = ORG_Tools._DistBilling;
            lblBillingPin.Text = ORG_Tools._PINBilling;
            #endregion
            #region Shipping
            txtNameShipping.Text = ORG_Tools._NameShipping;
            txtAddressShiping.Text = ORG_Tools._AddressShipping;
            txtCityShipping.Text = ORG_Tools._CityTownShipping;
            cmbStateShipping.Text = ORG_Tools._StateShipping;
            cmbDistShipping.Text = ORG_Tools._DistShipping;
            txtContactNoShipping.Text = ORG_Tools._ContactShipping;
            txtPinShiping.Text = ORG_Tools._PINShipping;
            #endregion
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (!IsDuplicateItemSelect())
                {
                    DescriptionAdd();
                    cmbItemName.SelectedIndex = -1;
                    cmbQuantity.Text = "";
                    cmbUnit.SelectedIndex = -1;
                    cmbItemName.Select();
                }
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            dgvItemList.Rows.Clear();
            mDescriptionSlno = 1;

            cmbItemName.SelectedIndex = -1;
            cmbQuantity.Text = "";
            cmbUnit.SelectedIndex = -1;
        }
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Supplier, LedgerDetails._Type.showDialog);
            frm.OnClose += Frm_OnClose;
            frm.ShowDialog();
        }
        private void Frm_OnClose(string customer)
        {
            cmbSupplierName.AddSuppliers();
            cmbSupplierName.Text = customer;
        }
        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemList.SelectedCells.Count > 0 && e.RowIndex != -1)
            {
                if (dgvItemList.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    mDescriptionSlno = 1;
                    foreach (DataGridViewRow row in dgvItemList.Rows)
                    {
                        row.Cells["SlNo"].Value = mDescriptionSlno++;
                    }
                }
            }
        }
        private void BTNcANCLE_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbQuantity.Text = "";

            if (!cmbItemName.Text.ISNullOrWhiteSpace())
            {
                string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                cmbUnit.Text = ItemTools.GetUnitName(itemid);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidSelection())
            {
                OrderSave();
            }
        }
        private void cmbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void btnItemADD_Click(object sender, EventArgs e)
        {
            ItemEntry frmItemEntry = new ItemEntry();
            frmItemEntry.onclose += new Action<string>(item_onclose);
            frmItemEntry.ShowDialog();
        }
        void item_onclose(string itemname)
        {
            cmbItemName.AddItem();
            cmbUnit.AddUnit();
            cmbItemName.Text = itemname;
        }
        private void cmbStateShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Key.ToString();
                cmbDistShipping.AddDist(stateID);
            }
        }
        private void PurchaseOrderEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void btnCalander_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Visible == true)
            {
                monthCalendar1.Visible = false;
            }
            else
            {
                monthCalendar1.Visible = true;
                monthCalendar1.Focus();
            }
        }
        private void txtExpectedDeliveryDate_Leave(object sender, EventArgs e)
        {
            if (!txtExpectedDeliveryDate.Text.ISNullOrWhiteSpace())
            {
                try
                {
                    txtExpectedDeliveryDate.Text = DateTime.Parse(txtExpectedDeliveryDate.Text).ToString("dd-MMM-yyyy");
                    DateTime vailduptodate = DateTime.Parse(txtExpectedDeliveryDate.Text);
                    if (dtpDate.Value.Date > vailduptodate.Date)
                    {
                        MessageBox.Show("Plese enter a larger date from Estimate Date", "Valid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtExpectedDeliveryDate.Focus();
                        txtExpectedDeliveryDate.Text = dtpDate.Text;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Plese enter a valid date", "Valid Date", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtExpectedDeliveryDate.Focus();
                }

            }
        }
        private void txtExpectedDeliveryDate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46 || e.KeyChar == 47 || e.KeyChar == 45)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void monthCalendar1_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            txtExpectedDeliveryDate.Text = monthCalendar1.SelectionStart.ToString("dd-MMM-yyyy");
            txtExpectedDeliveryDate.Focus();
        }

        private void cmbSupplierName_Leave(object sender, EventArgs e)
        {
            int index = cmbSupplierName.FindStringExact(cmbSupplierName.Text);
            if (index >= 0)
            {
                cmbSupplierName.SelectedIndex = index;
            }
            else
            {
                cmbSupplierName.Text = "";
            }
        }

        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            int index = cmbItemName.FindStringExact(cmbItemName.Text);
            if (index >= 0)
            {
                cmbItemName.SelectedIndex = index;
                string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
                cmbUnit.Text = ItemTools.GetUnitName(itemid);

            }
            else
            {
                cmbQuantity.Text = "";
                cmbItemName.Text = "";
            }
        }

        private void txtContactNoShipping_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtContactNoShipping_TextChanged(object sender, EventArgs e)
        {
            txtContactNoShipping.ForeColor = Color.Red;
            if (!txtContactNoShipping.Text.ISNullOrWhiteSpace())
            {
                if (txtContactNoShipping.Text.Length == 10 || txtContactNoShipping.Text.Length == 12)
                {
                    txtContactNoShipping.ForeColor = Color.Black;
                }
            }
        }

        private void txtPinShiping_TextChanged(object sender, EventArgs e)
        {
            txtPinShiping.ForeColor = Color.Red;
            if (!txtPinShiping.Text.ISNullOrWhiteSpace())
            {
                if (txtPinShiping.Text.Length == 6)
                {
                    txtPinShiping.ForeColor = Color.Black;
                }
            }
        }
    }
}
