
namespace DAPRO
{
    public class DefaultClass
    {
        public DefaultClass()
        {
            INVOICE_TOOLS.InitDetails();
            //OtherSettingTools.InitEstimateDetails();
            //OtherSettingTools.InitChallanDetails();
            //OtherSettingTools.InitCreditNoteDetails();
            //OtherSettingTools.InitPurchaseOrderDetails();
            //OtherSettingTools.InitAdvanceReceiptVoucherDetails();
            //OtherSettingTools.InitRefundVoucherDetails();
            //OtherSettingTools.InitReceiptVoucherDetails();
            //OtherSettingTools.InitAdvancePaymentVoucherDetails();
            //OtherSettingTools.InitPaymentVoucherDetails();
            //OtherSettingTools.InitDebitNoteDetails();
            OtherSettingTools.CallAllFunctionOfOtherSettingTools();

            UnitTools.GetUnit();
            ComodityCodeTools.GetHSNCode();
            ComodityCodeTools.GetSACCode();

            BillingTermTools.GetBillingTerms();
            EmployeeDesignationTools.GetDesignation();
            BAnkTools.GetBank();
            UserTools.GetUserName();
            StateTool.GetState();

            FinancialYearTools.GetYear();
            SubGroupTools.GetIncomeExpenseGroup();
            SubGroupTools.GetUnder();

            CashOrBankAccountTools.GetCashAccount();
            CashOrBankAccountTools.GetBankAcHead();

            Supplier.GetSuppliers();
            BillClas.GetAccountHeadForExpense();
            ExpenseTools.GetExpenseAccountHead();
            AccountHeadTools.SetCategory();

            ItemTools.GetItem();
            ItemTools.GetItemCategory();
            ItemTools.GetSubCategory();
            ItemTools.GetItemCompany();

            AccountHeadTools.GetSalesLedgerID();
            AccountHeadTools.GetSalesReturnLedgerID();
            AccountHeadTools.GetPurchaseLedgerID();
            AccountHeadTools.GetPurchaseReturnLedgerID();
            Customertools.GetCustomers();
            ///ledgers
            LedgerTools.GetAllLedgers();
            LedgerTools.GetAccountLedgers();
            LedgerTools.GetPartyLedgers();
            LedgerTools.GetBankLedgers();
            LedgerTools.GetCashLedgers();
            LedgerTools.GetCash_BankLedgers();
            LedgerTools.GetCustomerLedgers();
            LedgerTools.GetSupplierLedgers();
            LedgerTools.GetOtherLedgers();
            LedgerTools.GetReceivableAccountHeadID();

            SubAccountTools.GetSubAccountForExpense();

            CashCustomersTools.GetCashCustomers();
        }
    }
}
