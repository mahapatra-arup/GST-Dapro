using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAPRO
{
    public static class OtherSettingTools
    {
        public static string msg = "";

        #region ************* Voucher Setting  *************

        public static string _EstimateStart = "",
                             _ChallanStart = "",
                             _CreditNoteStart = "",
                             _PurchaseOrderStart = "",
                             _AdvanceReceiptVoucherStart = "",
                             _ReceiptVoucherStart = "",
                             _RefundVoucherStart = "",
                             _AdvancePaymentVoucherStart = "",
                             _PaymentVoucherStart = "",
                             _DebitNoteStart = "",
                               _EstimateSerialStart = "",
                             _ChallanSerialStart = "",
                             _CreditNoteSerialStart = "",
                             _PurchaseOrderSerialStart = "",
                             _AdvanceReceiptVoucherSerialStart = "",
                             _ReceiptVoucherSerialStart = "",
                             _RefundVoucherSerialStart = "",
                             _AdvancePaymentVoucherSerialStart = "",
                             _PaymentVoucherSerialStart = "",
                             _DebitNoteSerialStart = "";

        public static bool _IsEstimateBillGenarate = false,
                            _IsChallanBillgenarate = false,
                            _IsPurxhaseOrderBillgenarate = false,
                            _IsCreditNoteBillgenarate = false,
                            _IsDebitNoteBillgenarate = false,
                            _IsRefundVoucharBillgenarate = false,
                            _IsAdvanceReceiptBillgenarate = false,
                            _IsAdvancePaymentBillgenarate = false,
                            _IsReceiptVoucherBillgenarate = false,
                            _IsPaymentVoucherBillgenarate = false;

        public static void CallAllFunctionOfOtherSettingTools()
        {
            InitEstimateDetails();
            InitChallanDetails();
            InitCreditNoteDetails();
            InitPurchaseOrderDetails();
            InitAdvanceReceiptVoucherDetails();
            InitReceiptVoucherDetails();
            InitRefundVoucherDetails();
            InitAdvancePaymentVoucherDetails();
            InitPaymentVoucherDetails();
            InitDebitNoteDetails();
            CheckVoucherGenerateorNot();

            InitToolsTable();
        }

        public static void InitEstimateDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='Estimate' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _EstimateStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _EstimateSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void InitChallanDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='Challan' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _ChallanStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _ChallanSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();

            }
        }
        public static void InitCreditNoteDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='CreditNote' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _CreditNoteStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _CreditNoteSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();

            }
        }
        public static void InitPurchaseOrderDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='PurchaseOrder' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _PurchaseOrderStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _PurchaseOrderSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();

            }
        }
        public static void InitAdvanceReceiptVoucherDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='AdvanceReceiptVoucher' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _AdvanceReceiptVoucherStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _AdvanceReceiptVoucherSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();

            }
        }
        public static void InitReceiptVoucherDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='ReceiptVoucher' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _ReceiptVoucherStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _ReceiptVoucherSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void InitRefundVoucherDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='RefundVoucher' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _RefundVoucherStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _RefundVoucherSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void InitAdvancePaymentVoucherDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='AdvancePaymentVoucher' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _AdvancePaymentVoucherStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _AdvancePaymentVoucherSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void InitPaymentVoucherDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='PaymentVoucher' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _PaymentVoucherStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _PaymentVoucherSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void InitDebitNoteDetails()
        {
            string query = "Select VoucherNoStart,VoucherStartFrom from VoucherSettings where VoucherType='DebitNote' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _DebitNoteStart = dt.Rows[0]["VoucherNoStart"].ToString();
                _DebitNoteSerialStart = dt.Rows[0]["VoucherStartFrom"].ToString();
            }
        }
        public static void CheckVoucherGenerateorNot()
        {
            string query = "select * from estimate";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsEstimateBillGenarate = true;
            }
            query = "select * from challan";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsChallanBillgenarate = true;
            }
            query = "select * from PurchaseOrder";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsPurxhaseOrderBillgenarate = true;
            }
            query = "select * from CDRNote Where DocumentType='C'";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsCreditNoteBillgenarate = true;
            }
            query = "select * from CDRNote Where DocumentType='D'";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsDebitNoteBillgenarate = true;
            }
            query = "select * from AdvanceReceiptVoucher";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsAdvanceReceiptBillgenarate = true;
            }
            query = "select * from CDRNote Where DocumentType='R'";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _IsRefundVoucharBillgenarate = true;
            }
        }

        #endregion ************* End Voucher Setting Variable *************

        #region ************* ToolsTable **************

        public static int _DecemalePlace =2;
        public static bool _IsPurchasePercent =false, _IsMrpPercent = false,_IsThousandSeparate=false;

        public static void InitToolsTable()
        {
            _IsPurchasePercent = false;
            _IsMrpPercent = false;
            string query = "Select DecimalPlace, IsPercentageInMRP,IsThousandSeparate from ToolsTable";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _DecemalePlace =int.Parse( dt.Rows[0]["DecimalPlace"].ToString());
                _IsThousandSeparate= bool.Parse(dt.Rows[0]["IsThousandSeparate"].ToString());
                bool IsMrp =bool.Parse(dt.Rows[0]["IsPercentageInMRP"].ToString());
                if (IsMrp)
                {
                    _IsMrpPercent = true;
                }
                else
                {
                    _IsPurchasePercent = true;
                }
            }
        }
        #endregion ************* End ToolsTable **************

    }
}
