using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class StockEdit : Form
    {
        private List<string> mLstUnit = new List<string>();
        string msg = "", mstockSummaryid = "";
        private bool mIsSuccess = false;
        public event Action OnClose;
        public StockEdit(string stockSummaryid)
        {
            InitializeComponent();
            mstockSummaryid = stockSummaryid;
            cmbHighestUnit.AddUnit();
            cmbItemName.AddItem();
            StockDataRetrive();
        }
        private void StockDataRetrive()
        {
            string query = "Select StockSummary.*,item.itemname from StockSummary " +
                           "inner join item on item.id=StockSummary.itemid "+
                           "Where StockSummary.id='" + mstockSummaryid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                cmbItemName.Text = dt.Rows[0]["itemname"].ToString();
                txtBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
                dtmMfgDate.Text = dt.Rows[0]["MfgDate"].ToString();
                dtmExpDate.Text = dt.Rows[0]["EXPDate"].ToString();

                #region Highest unit Data
                lblHighestPriviousStockQty.Text = dt.Rows[0]["HighestStockQty"].ToString();
                lblHighestPreviousUnit.Text = dt.Rows[0]["HighestUnit"].ToString();
                lblHighestPriviousMrp.Text = dt.Rows[0]["HighestMRP"].ToString();
                //  lblHighestPriviousSalesRateWithTax.Text =lblHighestPriviousSalesRateNoTax_TextChanged
                lblHighestPriviousSalesRateNoTax.Text = dt.Rows[0]["HighestRate"].toRound();
                lblPreviousPurchaseRate.Text = dt.Rows[0]["PurchaseRate"].ToString();
                if (OtherSettingTools._IsMrpPercent)
                {
                    string amount = lblHighestPriviousSalesRateNoTax.Text;
                    lblHighestPriviousSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(amount).toRound();

                }
                else
                {
                    lblHighestPriviousSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(lblHighestPriviousSalesRateNoTax.Text);
                    lblPrevousPurchaseRateForSales.Text = CalculateRateForlbl(lblHighestPriviousSalesRateNoTax.Text);
                }
                lblPreviousHighestUnit1.Text = lblPreviousHighestUnit01.Text = lblPreviousHighestUnit11.Text = "/ " + lblHighestPreviousUnit.Text;

                lblPreviousHighestPurchaseUnit.Text= dt.Rows[0]["PurchaseUnit"].ToString();
                #endregion

                #region Middle Unit Data

                lblMiddlePreviousUnit.Text = dt.Rows[0]["MiddleUnit"].ToString();
                lblHigestPreviousMesure.Text = dt.Rows[0]["HighestMeasureQty"].ToString();
                lblMiddlePreviousQty.Text = dt.Rows[0]["MiddleStockQty"].ToString();
                lblMiddlePriviousSalesRate.Text = dt.Rows[0]["MiddleRate"].toRound();
                lblMiddlePreviousMrp.Text = dt.Rows[0]["MiddleMRP"].ToString();
                lblPreviousHighestUnit2.Text = lblHigestPreviousMesure.Text.ISNullOrWhiteSpace() ? "" : lblMiddlePreviousUnit.Text + " / " + lblHighestPreviousUnit.Text;
                lblMiddlePreviousUnit1.Text = lblMiddlePreviousUnit11.Text = "/ " + lblMiddlePreviousUnit.Text;
                lblPreviousMidUnit.Text = lblMiddlePreviousUnit.Text;
                #endregion

                #region Lowest Unit Data

                lblLowestPriviosUnitUnit.Text = dt.Rows[0]["LowestUnit"].ToString();
                lblLowestPreviousMesure.Text = dt.Rows[0]["LowestMeasureQty"].ToString();
                lblLowestPreviousStockQty.Text = dt.Rows[0]["LowestStockQty"].ToString();
                lblLowestPreviousSalesRate.Text = dt.Rows[0]["LowestRate"].toRound();
                lblLowestPreviousMrp.Text = dt.Rows[0]["LowestMRP"].ToString();
                lblMiddlePreviousUnit2.Text = lblLowestPriviosUnitUnit.Text.ISNullOrWhiteSpace() ? "" : lblLowestPriviosUnitUnit.Text + " / " + lblMiddlePreviousUnit.Text;
                lblPreviousLowestUnit1.Text = lblPreviousLowestUnit11.Text = "/ " + lblLowestPriviosUnitUnit.Text;
                lblPreviousLowUnit.Text = lblLowestPriviosUnitUnit.Text;
                #endregion
                FillUpdateData();
            }
        }
        private void FillUpdateData()
        {
            cmbHighestUnit.Text = lblHighestPreviousUnit.Text;
            txtHighestTotalStock.Text = lblHighestPriviousStockQty.Text;
            txtHighestMRP.Text = lblHighestPriviousMrp.Text;
            txtPurchaseRate.Text = lblPreviousPurchaseRate.Text;
            txtPurchaseRateTosALES.Text = lblPrevousPurchaseRateForSales.Text;
            txtHighestSalesRateNoTax.Text = lblHighestPriviousSalesRateNoTax.Text;
            txtHighestSalesRateWithTax.Text = lblHighestPriviousSalesRateWithTax.Text;
            lblHighestUnit1.Text = lblHighestUnit11.Text = lblHighestUnit111.Text= lblPurchaseUnit.Text = lblPreviousHighestUnit1.Text;

            if (!lblHigestPreviousMesure.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Checked = true;
                cmbMiddleUnit.Text = lblMiddlePreviousUnit.Text;
                txtHighestMesure.Text = lblHigestPreviousMesure.Text;
                lblMiddleTotalStock.Text = lblMiddlePreviousQty.Text;
                txtMiddleMRP.Text = lblMiddlePreviousMrp.Text;
                txtMiddleSalesRate.Text = lblMiddlePriviousSalesRate.Text;
                lblHigestUnit2.Text = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "" : cmbMiddleUnit.Text + " / " + cmbHighestUnit.Text;
                lblMiddleUnit1.Text = lblMiddleUnit11.Text = lblPreviousHighestUnit11.Text;
                lblmidunit.Text = cmbMiddleUnit.Text;
            }
            if (!lblLowestPreviousMesure.Text.ISNullOrWhiteSpace())
            {
                btnAddUnitRow_Click(null, null);
                cmbLowestUnit.Text = lblLowestPriviosUnitUnit.Text;
                txtLowestMesure.Text = lblLowestPreviousMesure.Text;
                lblMiddleUnit2.Text = lblMiddlePreviousUnit2.Text;
                txtLowestMRP.Text = lblLowestPreviousMrp.Text;
                txtLowestSalesRate.Text = lblLowestPreviousSalesRate.Text;
                lblLowestTotalStock.Text = lblLowestPreviousStockQty.Text;
                lblLowestUnit1.Text = lblLowestUnit11.Text = lblPreviousLowestUnit11.Text;
                lblLowunit.Text = cmbLowestUnit.Text;
            }


        }
        private void cmbHighestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHighestUnit.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Checked = false;
                //chkMoreUnit.Enabled = false;
                lblHighestUnit1.Text = "/ ";
                lblHighestUnit11.Text = "/ ";
                lblHighestUnit111.Text = "/ ";
                lblPurchaseUnit.Text = "/";
                lblPurchaseRatePerUnit.Text = "/";
                MoreUnitClear();
            }
            else
            {
                //chkMoreUnit.Enabled = true;
                lblHighestUnit1.Text = "/ " + cmbHighestUnit.Text;
                lblHighestUnit11.Text = "/ " + cmbHighestUnit.Text;
                lblHighestUnit111.Text = "/ " + cmbHighestUnit.Text;
                lblPurchaseUnit.Text = "/ " + cmbHighestUnit.Text;
                lblPurchaseRatePerUnit.Text = "/ " + cmbHighestUnit.Text;
            }
        }
        private void MoreUnitClear()
        {
            btnAddUnitRow.Disabl();
            btnUnitReset.Disabl();
            pnlUnit.Enabled = false;
            pnlUnitSmall.Enabled = true;
            
            cmbMiddleUnit.SelectedIndex = -1;
            txtHighestMesure.Clear();
            lblHigestUnit2.Text = "/";
            txtMiddleMRP.Clear();
            lblMiddleUnit1.Text = "/";

            cmbLowestUnit.SelectedIndex = -1;
            txtLowestMesure.Clear();
            lblMiddleUnit2.Text = "/";
            txtLowestMRP.Clear();
            lblLowestUnit1.Text = "/";
            pnlUnit2.Hide();
        }
        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            UnitEntry unitentry = new UnitEntry();
            unitentry.onclose += Unitentry_onclose;
            unitentry.ShowDialog();
        }
        private void Unitentry_onclose(string obj)
        {
            DefaultClass df = new DefaultClass();
            cmbHighestUnit.AddUnit();
            cmbHighestUnit.Text = obj.ToString();
        }
        private void txtHighestTotalStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestTotalStock.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtHighestTotalStock_Leave(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Enabled = false;
            }
            else
            {
                chkMoreUnit.Enabled = true;
            }
        }
        private void txtHighestMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestMRP_Leave(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                chkMoreUnit.Enabled = false;
            }
            else
            {
                chkMoreUnit.Enabled = true;
            }
        }

        private void txtHighestMRP_TextChanged(object sender, EventArgs e)
        {
            if (OtherSettingTools._IsMrpPercent)
            {
                if (!txtHighestMRP.Text.ISNullOrWhiteSpace())
                {
                    if (ORG_Tools._IsRegularGST)
                    {
                        string amount = txtHighestMRP.Text;
                        string highestSalesRateNoTax = CalculateSalesRateNoTaxFromMrp(amount);
                        txtHighestSalesRateNoTax.Text = highestSalesRateNoTax.toRound();
                        txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(highestSalesRateNoTax);
                    }
                    else
                    {
                        txtHighestSalesRateNoTax.Text = txtHighestMRP.Text;
                    }

                }
                else
                {
                    txtHighestSalesRateNoTax.Text = txtHighestSalesRateWithTax.Text = "";
                }
            }
        }


        private void MergeUnits()
        {
            mLstUnit.Clear();
            if (!UnitTools._DicUnit.IsNullOrEmpty())
            {
                foreach (var unit in UnitTools._DicUnit)
                {
                    mLstUnit.Add(unit.Value.ToString());
                }
                mLstUnit.Remove(cmbHighestUnit.Text);
            }
        }

        private void chkMoreUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMoreUnit.Checked)
            {
                MergeUnits();
                pnlUnit.Enabled = true;
                pnlUnit1.Enabled = true;
                btnAddUnitRow.Enabl();
                btnUnitReset.Enabl();
                cmbMiddleUnit.Items.Clear();
                cmbMiddleUnit.Items.AddRange(mLstUnit.ToArray());

                lblHighMesure.Text = cmbHighestUnit.Text + " Mesure";
            }
            else
            {
                MoreUnitClear();
            }
        }
        private void ClearUnit2()
        {
            cmbLowestUnit.SelectedIndex = -1;
            txtLowestMesure.Text = "";
            lblMiddleUnit2.Text = "/";
            txtLowestMRP.Text = "";
            lblLowestUnit1.Text = "/";
            txtLowestSalesRate.Text = "";
            lblLowestTotalStock.Text = "0";
        }
        private void btnAddUnitRow_Click(object sender, EventArgs e)
        {
            bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtMiddleMRP.Text.ISNullOrWhiteSpace() : false;
            if (!cmbMiddleUnit.Text.ISNullOrWhiteSpace() &&
                !txtHighestMesure.Text.ISNullOrWhiteSpace() &&
                !IsMrpBlank)
            {

                mLstUnit.Remove(cmbMiddleUnit.Text);
                pnlUnit2.Visible = true;
                cmbLowestUnit.Items.AddRange(mLstUnit.ToArray());
                lblLowMeSuer.Text = cmbMiddleUnit.Text + " Mesure";
                //pnlUnit1.Enabled = false;
                btnAddUnitRow.Disabl();
            }
            else
            {
                MessageBox.Show("Please fill up unit details.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbMiddleUnit.Select();
            }
        }

        private void btnUnitReset_Click(object sender, EventArgs e)
        {
            chkMoreUnit.Checked = false;
            chkMoreUnit.Enabled = false;
        }

        private void cmbMiddleUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearUnit2();
            lblHigestUnit2.Text = cmbMiddleUnit.Text + " / " + cmbHighestUnit.Text;
            lblMiddleUnit1.Text = lblMiddleUnit11.Text = " / " + cmbMiddleUnit.Text;
            lblmidunit.Text = cmbMiddleUnit.Text;
        }

        private void txtHighestMesure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestMesure.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestMesure_TextChanged(object sender, EventArgs e)
        {
            ClearUnit2();
            string totalmidstock = "0";
            txtMiddleMRP.Text = CalculateRate(txtHighestMRP.Text, txtHighestMesure.Text, txtHighestTotalStock.Text, out totalmidstock);
            txtMiddleSalesRate.Text = CalculateRate(txtHighestSalesRateNoTax.Text, txtHighestMesure.Text, txtHighestTotalStock.Text, out totalmidstock);

            lblMiddleTotalStock.Text = totalmidstock;

        }
        private string CalculateRate(string highrateperqtystr, string lowestMesureqtystr, string totalhigherstockstr, out string lowerTotalStockstr)
        {
            double rate = 0d, highrateperqty = 0d, lowestMesureqty = 0d, totalhigherstock = 0d, lowerTotalStock = 0d;
            double.TryParse(highrateperqtystr, out highrateperqty);
            double.TryParse(lowestMesureqtystr, out lowestMesureqty);
            double.TryParse(totalhigherstockstr, out totalhigherstock);

            rate = lowestMesureqty == 0 ? 0 : (highrateperqty / lowestMesureqty);

            lowerTotalStock = totalhigherstock * lowestMesureqty;
            lowerTotalStockstr = lowerTotalStock.ToString();
            string ratestr = rate == 0 ? "" : rate.ToString();
            return ratestr;
        }

        private void txtMiddleMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtMiddleMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtMiddleMRP_TextChanged(object sender, EventArgs e)
        {
            ClearUnit2();
            txtMiddleSalesRate.Text = txtMiddleMRP.Text.toRound();
        }

        private void txtMiddleSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtMiddleSalesRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmbLowestUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblMiddleUnit2.Text = cmbLowestUnit.Text + " / " + cmbMiddleUnit.Text;
            lblLowestUnit1.Text = lblLowestUnit11.Text = " / " + cmbLowestUnit.Text;
            lblLowunit.Text = cmbLowestUnit.Text;
        }

        private void txtLowestMesure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestMesure.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLowestMesure_TextChanged(object sender, EventArgs e)
        {
            string totalloweststock = "0";
            txtLowestMRP.Text = CalculateRate(txtMiddleMRP.Text, txtLowestMesure.Text, lblMiddleTotalStock.Text, out totalloweststock);
            txtLowestSalesRate.Text = CalculateRate(txtMiddleSalesRate.Text, txtHighestMesure.Text, lblMiddleTotalStock.Text, out totalloweststock);
            lblLowestTotalStock.Text = totalloweststock;

        }

        private void txtLowestMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestMRP.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLowestMRP_TextChanged(object sender, EventArgs e)
        {
            txtLowestSalesRate.Text = txtLowestMRP.Text.toRound();
        }

        private void StockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillCalculatorRunning();
            if (OnClose != null && mIsSuccess)
            {
                OnClose();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            KillCalculatorRunning();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                if (!IsDuplicateBatch())
                {
                    DataSave();
                }
            }
        }

        private void DataSave()
        {
            #region Data
            string batchNo = txtBatchNo.Text.GetDBFormatString();
            string mfgDate = dtmMfgDate.Text.GetDBFormatString();
            string expDate = dtmExpDate.Text.GetDBFormatString();

            string highesttunit = cmbHighestUnit.Text.GetDBFormatString();
            string highestStockQty = txtHighestTotalStock.Text.GetDBFormatString();
            string highestrate = txtHighestSalesRateNoTax.Text.GetDBFormatString();
            string highestMrp = txtHighestMRP.Text.ISNullOrWhiteSpace() ? "NULL" : txtHighestMRP.Text.GetDBFormatString();

            string middleunit = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + cmbMiddleUnit.Text.GetDBFormatString() + "'";
            string middlestockqty = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "NULL" : lblMiddleTotalStock.Text.GetDBFormatString();
            string middlerate = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtMiddleSalesRate.Text.GetDBFormatString();
            string middlemrp = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtMiddleMRP.Text.ISNullOrWhiteSpace() ? "NULL" : txtMiddleMRP.Text.GetDBFormatString();

            string lowestunit = cmbLowestUnit.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + cmbLowestUnit.Text.GetDBFormatString() + "'";
            string lowestTotalStock = cmbLowestUnit.Text.ISNullOrWhiteSpace() ? "NULL" : lblLowestTotalStock.Text.GetDBFormatString();
            string lowestrate = cmbLowestUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtLowestSalesRate.Text.GetDBFormatString();
            string lowestmrp = cmbLowestUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtLowestMRP.Text.ISNullOrWhiteSpace() ? "NULL" : txtLowestMRP.Text.GetDBFormatString();

            string higestMesureQty = cmbMiddleUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtHighestMesure.Text.GetDBFormatString();
            string lowestMesureQty = cmbLowestUnit.Text.ISNullOrWhiteSpace() ? "NULL" : txtLowestMesure.Text.GetDBFormatString();

            #endregion

            string query = "Update StockSummary Set  BatchNo='" + batchNo + "', MfgDate='" + mfgDate
                + "', ExpDate='" + expDate + "',HighestUnit='" + highesttunit + "', HighestStockQty=" + highestStockQty
                + ", HighestRate=" + highestrate + ", HighestMRP=" + highestMrp + ", MiddleUnit=" + middleunit + "," +
                " MiddleStockQty=" + middlestockqty + ", MiddleRate=" + middlerate + ", MiddleMRP=" + middlemrp
                + ", LowestUnit=" + lowestunit + ", LowestStockQty=" + lowestTotalStock + ",LowestRate=" + lowestrate
                + ", LowestMRP=" + lowestmrp + ", HighestMeasureQty=" + higestMesureQty + ", LowestMeasureQty=" + lowestMesureQty + " where id='" + mstockSummaryid + "'";
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Update Successfull.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KillCalculatorRunning();
                mIsSuccess = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private bool IsDuplicateBatch()
        {
            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            string batch = txtBatchNo.Text.GetDBFormatString();

            string query = "Select * from Stocksummary where itemid='" + itemid + "' and BatchNo='" + batch + "' and id<>'" + mstockSummaryid + "' ";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                MessageBox.Show("Duplicate Batch.\n Change Batch no.", "stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            return false;

        }

        private bool IsValidEntry()
        {
            if (cmbItemName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Item not Set.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbItemName.Select();
                return false;
            }
            if (txtBatchNo.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter batch no.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtBatchNo.Select();
                return false;
            }
            if (cmbHighestUnit.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select Highest unit to sale.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbHighestUnit.Select();
                return false;
            }
            if (txtHighestTotalStock.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Stock Qty.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtHighestTotalStock.Select();
                return false;
            }
            if (OtherSettingTools._IsMrpPercent)
            {
                if (txtHighestMRP.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter MRP of the Product.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHighestMRP.Select();
                    return false;
                }
            }
           
            if (txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter sales rate of the Product.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtHighestSalesRateNoTax.Select();
                return false;
            }
            if (!cmbMiddleUnit.Text.ISNullOrWhiteSpace())
            {
                if (txtHighestMesure.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter Mesure of Highest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHighestMesure.Select();
                    return false;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    if (txtMiddleMRP.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter MRP of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtMiddleMRP.Select();
                        return false;
                    }
                }
                if (txtMiddleSalesRate.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter sales rate of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtMiddleSalesRate.Select();
                    return false;
                }
            }
            if (!cmbLowestUnit.Text.ISNullOrWhiteSpace())
            {
                if (txtLowestMesure.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter Mesure of Middle unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtLowestMesure.Select();
                    return false;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    if (txtLowestMRP.Text.ISNullOrWhiteSpace())
                    {
                        MessageBox.Show("Enter MRP of Lowest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtLowestMRP.Select();
                        return false;
                    }
                }
                if (txtLowestSalesRate.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Enter sales rate of Lowest unit.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtLowestSalesRate.Select();
                    return false;
                }
            }
            return true;
        }

        private void txtHighestTotalStock_TextChanged(object sender, EventArgs e)
        {
            chkMoreUnit.Checked = false;
        }

        private void txtHighestSalesRate_TextChanged(object sender, EventArgs e)
        {
            chkMoreUnit.Checked = false;
        }

        private void txtMiddleSalesRate_TextChanged(object sender, EventArgs e)
        {
            ClearUnit2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillUpdateData();
        }
        private void KillCalculatorRunning()
        {
            try
            {
                Process[] name = Process.GetProcessesByName("Calculator");
                if (name.Length == 1)
                {
                    name[0].Kill();
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnCalculator_Click(object sender, EventArgs e)
        {
            KillCalculatorRunning();
            Process calculator = Process.Start("calc");
        }
        private void lblItenEdit_Click(object sender, EventArgs e)
        {
            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            ItemEntry itementry = new ItemEntry(itemid);
            itementry.onclose += Itementry_onclose;
            itementry.ShowDialog();

        }
        private void Itementry_onclose(string obj)
        {
            mIsSuccess = true;
            cmbItemName.AddItem();
            cmbItemName.Text = obj;
        }
        private string CalculateSalesRateWithTaxFromMrp(string amount)
        {
            double amountd = 0d, slalesRate = 0d, totalgst = 0d, igstd = 0d, cessd = 0d;

            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            object sgst = "", cgst, igst = "", cess = "";
            ItemTools.GetItemGSTRate(itemid, out cgst, out sgst, out igst, out cess);

            double.TryParse(amount, out amountd);
            double.TryParse(igst.ISValidObject() ? igst.ToString() : "0", out igstd);
            double.TryParse(cess.ISValidObject() ? cess.ToString() : "0", out cessd);

            totalgst = igstd + cessd;
            slalesRate = amountd + (amountd * (totalgst / 100));
            return slalesRate.toString();
        }

        private string CalculateRateForTextBox(string amount)
        {
            double purchaseOrMrpAmount = 0d, salesamount = 0d, rateAmount = 0d, rate = 0d;
            if (OtherSettingTools._IsPurchasePercent)
            {
                double.TryParse(txtPurchaseRate.Text, out purchaseOrMrpAmount);
            }
            else
            {
                double.TryParse(txtHighestMRP.Text, out purchaseOrMrpAmount);
            }
            double.TryParse(amount, out salesamount);
            rateAmount = salesamount - purchaseOrMrpAmount;
            rate = (100 / purchaseOrMrpAmount) * rateAmount;
            return rate.ToString("0.00");
        }
        private string CalculateRateForlbl(string amount)
        {
            double purchaseOrMrpAmount = 0d, salesamount = 0d, rateAmount = 0d, rate = 0d;
            if (OtherSettingTools._IsPurchasePercent)
            {
                double.TryParse(lblPreviousPurchaseRate.Text, out purchaseOrMrpAmount);
            }
            else
            {
                double.TryParse(lblHighestPriviousMrp.Text, out purchaseOrMrpAmount);
            }
            double.TryParse(amount, out salesamount);
            rateAmount = salesamount - purchaseOrMrpAmount;
            rate = (100 / purchaseOrMrpAmount) * rateAmount;
            return rate.ToString("0.00");
        }

        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPurchaseRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            if (ORG_Tools._IsRegularGST)
            {
                if (!txtPurchaseRate.Text.ISNullOrWhiteSpace())
                {
                    if (OtherSettingTools._IsPurchasePercent)
                    {
                        txtPurchaseRateTosALES_Leave(null, null);
                    }
                }
                else
                {
                    txtHighestSalesRateNoTax.Text = "";
                    txtHighestSalesRateWithTax.Text = "";
                    txtPurchaseRateTosALES.Text = "";

                }
            }
        }

        private void txtPurchaseRateTosALES_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPurchaseRateTosALES.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPurchaseRateTosALES_Leave(object sender, EventArgs e)
        {
            if (!txtPurchaseRate.Text.ISNullOrWhiteSpace())
            {
                //txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(CalculateSalesRate()).toRound();
                txtHighestSalesRateNoTax.Text = CalculateSalesRate();
                //txtHighestSalesRateWithTax.Text = CalculateSalesRate();
            }
            else
            {
                CalculateSalesRateNoTaxFromMrp(txtHighestMRP.Text);
            }
        }
        private string CalculateSalesRate()
        {
            double purchaseOrMrpAmount = 0d, salesRate = 0d, rateAmount = 0d, rate = 0d;
            if (OtherSettingTools._IsPurchasePercent)
            {
                double.TryParse(txtPurchaseRate.Text, out purchaseOrMrpAmount);
            }
            else ///Sales Rate==MRP
            {
                double.TryParse(txtHighestMRP.Text, out purchaseOrMrpAmount);
            }
            double.TryParse(txtPurchaseRateTosALES.Text, out rate);
            rateAmount = (rate / 100) * purchaseOrMrpAmount;
            salesRate = rateAmount + purchaseOrMrpAmount;
            return salesRate.toString();
        }

        private void txtHighestSalesRateNoTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestSalesRateNoTax.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestSalesRateNoTax_Leave(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
                if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
                {
                    chkMoreUnit.Enabled = false;
                }
                else
                {
                    chkMoreUnit.Enabled = true;
                }
            }
            else
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    txtHighestMRP_TextChanged(null, null);
                }
                else
                {
                    txtPurchaseRate_TextChanged(null, null);
                }
            }
        }

        private void txtHighestSalesRateNoTax_TextChanged(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace())
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    string amount = txtHighestSalesRateNoTax.Text;
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(amount).toRound();

                }
                else
                {
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(txtHighestSalesRateNoTax.Text);
                    txtPurchaseRateTosALES.Text = CalculateRateForTextBox(txtHighestSalesRateNoTax.Text);
                }
            }

        }

        private void txtHighestSalesRateWithTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtHighestSalesRateWithTax.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHighestSalesRateWithTax_Leave(object sender, EventArgs e)
        {
            if (!txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
            {
                bool IsMrpBlank = OtherSettingTools._IsMrpPercent ? txtHighestMRP.Text.ISNullOrWhiteSpace() : false;
                if (txtHighestTotalStock.Text.ISNullOrWhiteSpace() || IsMrpBlank || txtHighestSalesRateNoTax.Text.ISNullOrWhiteSpace() || txtHighestSalesRateWithTax.Text.ISNullOrWhiteSpace())
                {
                    chkMoreUnit.Enabled = false;
                }
                else
                {
                    chkMoreUnit.Enabled = true;
                }
                if (OtherSettingTools._IsMrpPercent)
                {
                    string amount = txtHighestSalesRateWithTax.Text;
                    string highestSalesRateNoTax = CalculateSalesRateNoTaxFromMrp(amount);
                    txtHighestSalesRateNoTax.Text = highestSalesRateNoTax.toRound();
                    txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(highestSalesRateNoTax);
                    //txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(txtHighestSalesRateWithTax.Text).toRound();
                }
                else
                {
                    txtHighestSalesRateNoTax.Text = CalculateSalesRateNoTaxFromMrp(txtHighestSalesRateWithTax.Text).toRound();
                    txtPurchaseRateTosALES.Text = CalculateRateForTextBox(txtHighestSalesRateNoTax.Text);
                    txtPurchaseRateTosALES_Leave(null, null);
                }
            }
            else
            {
                if (OtherSettingTools._IsMrpPercent)
                {
                    txtHighestMRP_TextChanged(null, null);
                }
                else
                {
                    txtHighestSalesRateNoTax_TextChanged(null, null);
                }
            }
        }
        private string CalculateSalesRateNoTaxFromMrp(string amount)
        {
            double mrp = 0d, slalesRate = 0d, totalgst = 0d, igstd = 0d, cessd = 0d;

            string itemid = ((KeyValuePair<string, string>)cmbItemName.SelectedItem).Key.ToString();
            object sgst = "", cgst, igst = "", cess = "";
            ItemTools.GetItemGSTRate(itemid, out cgst, out sgst, out igst, out cess);

            double.TryParse(amount, out mrp);
            double.TryParse(igst.ISValidObject() ? igst.ToString() : "0", out igstd);
            double.TryParse(cess.ISValidObject() ? cess.ToString() : "0", out cessd);

            totalgst = igstd + cessd;
            slalesRate = (mrp * 100) / (100 + totalgst);
            //txtHighestSalesRateWithTax.Text = CalculateSalesRateWithTaxFromMrp(slalesRate.ToString());
            return slalesRate.ToString();
        }


        private void txtLowestSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtLowestSalesRate.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
