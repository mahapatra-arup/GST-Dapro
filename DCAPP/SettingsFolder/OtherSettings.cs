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
    public partial class OtherSettings : Form
    {
        string msg = "";
        List<string> mlistquery = new List<string>();
        public OtherSettings()
        {
            InitializeComponent();
            ShowData();
            ChechAnyVoucharMakeOrnot();

        }
        private void ShowData()
        {
            txtestimateno.Text = OtherSettingTools._EstimateStart;
            txtChallanno.Text = OtherSettingTools._ChallanStart;
            txtpurchaseOrderno.Text = OtherSettingTools._PurchaseOrderStart;
            txtCreditNoteNo.Text = OtherSettingTools._CreditNoteStart;
            txtDebitNoteNo.Text = OtherSettingTools._DebitNoteStart;
            txtAdvanceReceiptNo.Text = OtherSettingTools._AdvanceReceiptVoucherStart;
            txtAdvancePaymentNo.Text = OtherSettingTools._AdvancePaymentVoucherStart;
            txtRefuntVoucherNo.Text = OtherSettingTools._RefundVoucherStart;
            txtreceiptVoucherNo.Text = OtherSettingTools._ReceiptVoucherStart;
            txtPaymentVoucharNo.Text = OtherSettingTools._PaymentVoucherStart;

            txtEstimateStartfrom.Text = OtherSettingTools._EstimateSerialStart;
            txtChallanStartfrom.Text = OtherSettingTools._ChallanSerialStart;
            txtPurchaseOrderStartfrom.Text = OtherSettingTools._PurchaseOrderSerialStart;
            txtCreditNoteStartFrom.Text = OtherSettingTools._CreditNoteSerialStart;
            txtDebitNoteStartFrom.Text = OtherSettingTools._DebitNoteSerialStart;
            txtAdvanceReceiptStartFrom.Text = OtherSettingTools._AdvanceReceiptVoucherSerialStart;
            txtAdvancePaymentStartFrom.Text = OtherSettingTools._AdvancePaymentVoucherSerialStart;
            txtRefuntVoucherStartFrom.Text = OtherSettingTools._RefundVoucherSerialStart;
            txtreceiptVoucherStartFrom.Text = OtherSettingTools._ReceiptVoucherSerialStart;
            txtPaymentVoucharStartFrom.Text = OtherSettingTools._PaymentVoucherSerialStart;
            chkThousandSeperator.Checked= OtherSettingTools._IsThousandSeparate;
            ShowDecimalPlace();

            if (OtherSettingTools._IsPurchasePercent)
            {
                rbtnPurchase.Checked = true;
            }
            else
            {
                rbtnMrp.Checked = true;
            }

        }
        private void ShowDecimalPlace()
        {
            if (OtherSettingTools._DecemalePlace == 0)
            {
                rbtnDecimalNone.Checked = true;
            }
            else if (OtherSettingTools._DecemalePlace == 2)
            {
                rbtnDecimalTwo.Checked = true;
            }
            else
            {
                rbtnDecimalFour.Checked = true;
            }
        }
        private void ChechAnyVoucharMakeOrnot()
        {
            if (OtherSettingTools._IsEstimateBillGenarate)
            {
                txtEstimateStartfrom.Text = OtherSettingTools._EstimateSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._EstimateSerialStart;
                txtEstimateStartfrom.Enabled = false;
            }
            if (OtherSettingTools._IsChallanBillgenarate)
            {
                txtChallanStartfrom.Text = OtherSettingTools._ChallanSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._ChallanSerialStart;
                txtChallanStartfrom.Enabled = false;
            }
            if (OtherSettingTools._IsPurxhaseOrderBillgenarate)
            {
                txtPurchaseOrderStartfrom.Text = OtherSettingTools._PurchaseOrderSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._PurchaseOrderSerialStart;
                txtPurchaseOrderStartfrom.Enabled = false;
            }
            if (OtherSettingTools._IsCreditNoteBillgenarate)
            {
                txtCreditNoteStartFrom.Text = OtherSettingTools._CreditNoteSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._CreditNoteSerialStart;
                txtCreditNoteStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsDebitNoteBillgenarate)
            {
                txtDebitNoteStartFrom.Text = OtherSettingTools._DebitNoteSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._DebitNoteSerialStart;
                txtDebitNoteStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsAdvanceReceiptBillgenarate)
            {
                txtAdvanceReceiptStartFrom.Text = OtherSettingTools._AdvanceReceiptVoucherSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._AdvanceReceiptVoucherSerialStart;
                txtAdvanceReceiptStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsRefundVoucharBillgenarate)
            {
                txtRefuntVoucherStartFrom.Text = OtherSettingTools._RefundVoucherSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._RefundVoucherSerialStart;
                txtRefuntVoucherStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsAdvancePaymentBillgenarate)
            {
                txtAdvancePaymentStartFrom.Text = OtherSettingTools._AdvancePaymentVoucherSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._AdvancePaymentVoucherSerialStart;
                txtAdvancePaymentStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsReceiptVoucherBillgenarate)
            {
                txtreceiptVoucherStartFrom.Text = OtherSettingTools._ReceiptVoucherSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._ReceiptVoucherSerialStart;
                txtreceiptVoucherStartFrom.Enabled = false;
            }
            if (OtherSettingTools._IsPaymentVoucherBillgenarate)
            {
                txtPaymentVoucharStartFrom.Text = OtherSettingTools._PaymentVoucherSerialStart.ISNullOrWhiteSpace() ? "1" : OtherSettingTools._PaymentVoucherSerialStart;
                txtPaymentVoucharStartFrom.Enabled = false;
            }

        }
        private void txtInvoiceString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '/' || e.KeyChar == '\b' || char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
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
        private bool IsvalidEntry()
        {
            Control[] txtbox = new Control[] { txtestimateno, txtpurchaseOrderno, txtCreditNoteNo, txtDebitNoteNo, txtAdvanceReceiptNo, txtRefuntVoucherNo, txtreceiptVoucherNo, txtChallanno, txtAdvancePaymentNo, txtPaymentVoucharNo };
            foreach (Control item in txtbox)
            {
                Control[] txtbox2 = new Control[] { txtestimateno, txtpurchaseOrderno, txtCreditNoteNo, txtDebitNoteNo, txtAdvanceReceiptNo, txtRefuntVoucherNo, txtreceiptVoucherNo, txtChallanno, txtAdvancePaymentNo, txtPaymentVoucharNo };
                foreach (Control item2 in txtbox)
                {
                    if (item != item2)
                    {
                        if (item.Text.ToUpper() == item2.Text.ToUpper() && !item.Text.ISNullOrWhiteSpace())
                        {
                            MessageBox.Show("Duplicate prefix \"" + item.Text + "\"", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            item.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsvalidEntry())
            {
                DataSave();
            }
        }
        private void DataSave()
        {
            mlistquery.Clear();
            string query = "";
            #region data
            #region No
            string estmateno = txtestimateno.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtestimateno.Text.GetDBFormatString() + "'";
            string challanno = txtChallanno.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtChallanno.Text.GetDBFormatString() + "'";
            string purchaseorderno = txtpurchaseOrderno.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtpurchaseOrderno.Text.GetDBFormatString() + "'";
            string creditNoteNo = txtCreditNoteNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtCreditNoteNo.Text.GetDBFormatString() + "'";
            string debitNoteNo = txtDebitNoteNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtDebitNoteNo.Text.GetDBFormatString() + "'";
            string advanceReceiptNo = txtAdvanceReceiptNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtAdvanceReceiptNo.Text.GetDBFormatString() + "'";
            string advancePaymentNo = txtAdvancePaymentNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtAdvancePaymentNo.Text.GetDBFormatString() + "'";
            string refuntVoucherNo = txtRefuntVoucherNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtRefuntVoucherNo.Text.GetDBFormatString() + "'";
            string receiptVoucherNo = txtreceiptVoucherNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtreceiptVoucherNo.Text.GetDBFormatString() + "'";
            string paymentVoucharNo = txtPaymentVoucharNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtPaymentVoucharNo.Text.GetDBFormatString() + "'";
            #endregion

            #region startfrom
            string estmatestartfrom = txtEstimateStartfrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtEstimateStartfrom.Text.GetDBFormatString() + "";
            string challanstartfrom = txtChallanStartfrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtChallanStartfrom.Text.GetDBFormatString() + "";
            string purchaseOrderStartfrom = txtPurchaseOrderStartfrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtPurchaseOrderStartfrom.Text.GetDBFormatString() + "";
            string creditNoteStartFrom = txtCreditNoteStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtCreditNoteStartFrom.Text.GetDBFormatString() + "";
            string debitNoteStartFrom = txtDebitNoteStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtDebitNoteStartFrom.Text.GetDBFormatString() + "";
            string advanceReceiptStartFrom = txtAdvanceReceiptStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtAdvanceReceiptStartFrom.Text.GetDBFormatString() + "";
            string advancePaymentStartFrom = txtAdvancePaymentStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtAdvancePaymentStartFrom.Text.GetDBFormatString() + "";
            string refuntVoucherStartFrom = txtRefuntVoucherStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtRefuntVoucherStartFrom.Text.GetDBFormatString() + "";
            string receiptVoucherStartFrom = txtreceiptVoucherStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtreceiptVoucherStartFrom.Text.GetDBFormatString() + "";
            string paymentVoucharStartFrom = txtPaymentVoucharStartFrom.Text.ISNullOrWhiteSpace() ? "NULL" : "" + txtPaymentVoucharStartFrom.Text.GetDBFormatString() + "";

            string IsPercentageInMrp = rbtnMrp.Checked ? "True" : "False";
            string IsThousandSeparate = chkThousandSeperator.Checked ? "True" : "False";


            #endregion
            #endregion
            #region Query
            query = "Update VoucherSettings set VoucherNoStart=" + estmateno + ",VoucherStartFrom=" + estmatestartfrom + " where VoucherType='Estimate'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + challanno + ",VoucherStartFrom=" + challanstartfrom + " where VoucherType='Challan'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + creditNoteNo + ",VoucherStartFrom=" + creditNoteStartFrom + " where VoucherType='CreditNote'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + purchaseorderno + ",VoucherStartFrom=" + purchaseOrderStartfrom + " where VoucherType='PurchaseOrder'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + advanceReceiptNo + ",VoucherStartFrom=" + advanceReceiptStartFrom + " where VoucherType='AdvanceReceiptVoucher'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + receiptVoucherNo + ",VoucherStartFrom=" + receiptVoucherStartFrom + " where VoucherType='ReceiptVoucher'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + refuntVoucherNo + ",VoucherStartFrom=" + refuntVoucherStartFrom + " where VoucherType='RefundVoucher'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + advancePaymentNo + ",VoucherStartFrom=" + advancePaymentStartFrom + " where VoucherType='AdvancePaymentVoucher'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + paymentVoucharNo + ",VoucherStartFrom=" + paymentVoucharStartFrom + " where VoucherType='PaymentVoucher'";
            mlistquery.Add(query);
            query = "Update VoucherSettings set VoucherNoStart=" + debitNoteNo + ",VoucherStartFrom=" + debitNoteStartFrom + " where VoucherType='DebitNote'";
            mlistquery.Add(query);
            //Update ToolsTable 
            query = "Update ToolsTable set IsPercentageInMRP='" + IsPercentageInMrp + "',IsThousandSeparate='"+IsThousandSeparate+"'";
            mlistquery.Add(query);
            SaveDecimalPlace();
            #endregion
            #region Execute
            if (SQLHelper.GetInstance().ExecuteTransection(mlistquery, out msg))
            {
                MessageBox.Show("Data save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OtherSettingTools.CallAllFunctionOfOtherSettingTools();
            }
            else
            {
                MessageBox.Show(msg);
            }

            #endregion
        }
        private void SaveDecimalPlace()
        {
            int dPlace = 0;
            if (rbtnDecimalNone.Checked)
            {
                dPlace = 0;
            }
            else if (rbtnDecimalTwo.Checked)
            {
                dPlace = 2;
            }
            else
            {
                dPlace = 4;
            }
            string query = "Update ToolsTable set DecimalPlace=" + dPlace + "";
            mlistquery.Add(query);
        }
        private void OtherSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
