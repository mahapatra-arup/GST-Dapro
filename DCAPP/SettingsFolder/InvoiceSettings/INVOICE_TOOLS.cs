using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace DAPRO
{
    public static class INVOICE_TOOLS
    {
        #region String Variable
        public static string _InvoiceString = "",
                            _AuthenticationText = "",
                           _OrgBankDetailsText = "",
                           _OrgTermsAndConditionText = "",
                           _DeclarationText = "",
                           _SignatureForText = "",
                           _SignatureAuthorityText = "",
                           _PreparedByText = "",
                            msg = "";
        #endregion
        public static long _InvoiceStartFrom;
        #region Bool Variable
        public static bool
                            _ISInvoiceCreate = false,
                             _IsOrgCIN,
                             _IsOrgGSTIN = false,
                             _IsOrgPAN = false,
                             _IsOrgStateWithCode = false,


                           _IsOrgName = false,
                           _IsOrgAddress = false,
                           _IsOrgCityTown = false,
                           _IsOrgState = false,
                           _IsOrgDistrict = false,
                           _IsOrgPin = false,
                           _IsOrgContactNo = false,
                           _IsOrgMailID = false,
                           _IsOrgWebsite = false,
                           _IsOrgLogo = false,

                           _IsBillingName = false,
                           _IsBillingAddress = false,
                           _IsBillingCityTown = false,
                           _IsBillingState = false,
                           _IsBillingDistrict = false,
                           _IsBillingPin = false,
                           _IsBillingContact = false,
                           _IsBillingGST = false,
                           _IsBillingPlaceOfSupply = false,

                           _IsShippingName = false,
                           _IsShippingAddress = false,
                           _IsShippingCityTown = false,
                           _IsShippingState = false,
                           _IsShippingDistrict = false,
                           _IsShippingPin = false,
                           _IsShippingContact = false,
                           _IsShippingGST = false,
                           _IsShippingPlaceOfSupply = false,

                           _IsOrgBankDetails = false,
                           _IsOrgTermsAndCondition = false,
                           _IsDeclaration = false,
                           _IsSignatureFor = false,
                           _IsSignatureAuthority = false,
                           _IsPreparedBy = false,

                         _IsBillingTerm = false,
                         _IsBillingDueDate = false,
                        _IsAuthentication = false,
                        _IsFooterBottomOfthePage=false;
        #endregion  
        public static void CheckInvoiceCreateOrNot()
        {
            string query = "select * from invoice";
            if (SQLHelper.GetInstance().ExcuteNonQuery(query, out msg).IsValidDataTable())
            {
                _ISInvoiceCreate = true;
            }

        }
        public static void InitDetails()
        {
            string query = "Select * from InvoiceSettings ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                #region Assign value

                _InvoiceString = dt.Rows[0]["InvoiceStart"].ToString();
                long.TryParse(dt.Rows[0]["SerialStartFrom"].ToString(), out _InvoiceStartFrom);
                bool.TryParse(dt.Rows[0]["OrgCin"].ToString(), out _IsOrgCIN);
                bool.TryParse(dt.Rows[0]["OrgGSTN"].ToString(), out _IsOrgGSTIN);
                bool.TryParse(dt.Rows[0]["ORGPanNo"].ToString(), out _IsOrgPAN);
                bool.TryParse(dt.Rows[0]["OrgStateWithCode"].ToString(), out _IsOrgStateWithCode);
                bool.TryParse(dt.Rows[0]["OrgName"].ToString(), out _IsOrgName);
                bool.TryParse(dt.Rows[0]["OrgAddress"].ToString(), out _IsOrgAddress);
                bool.TryParse(dt.Rows[0]["OrgTown"].ToString(), out _IsOrgCityTown);
                bool.TryParse(dt.Rows[0]["OrgState"].ToString(), out _IsOrgState);
                bool.TryParse(dt.Rows[0]["orgDist"].ToString(), out _IsOrgDistrict);
                bool.TryParse(dt.Rows[0]["OrgPin"].ToString(), out _IsOrgPin);
                bool.TryParse(dt.Rows[0]["OrgPhNo"].ToString(), out _IsOrgContactNo);
                bool.TryParse(dt.Rows[0]["Email"].ToString(), out _IsOrgMailID);
                bool.TryParse(dt.Rows[0]["Website"].ToString(), out _IsOrgWebsite);
                bool.TryParse(dt.Rows[0]["Logo"].ToString(), out _IsOrgLogo);

                bool.TryParse(dt.Rows[0]["BillingName"].ToString(), out _IsBillingName);
                bool.TryParse(dt.Rows[0]["BillingAddress"].ToString(), out _IsBillingAddress);
                bool.TryParse(dt.Rows[0]["BillingTown"].ToString(), out _IsBillingCityTown);
                bool.TryParse(dt.Rows[0]["BillingState"].ToString(), out _IsBillingState);
                bool.TryParse(dt.Rows[0]["BillingDist"].ToString(), out _IsBillingDistrict);
                bool.TryParse(dt.Rows[0]["BillingPin"].ToString(), out _IsBillingPin);
                bool.TryParse(dt.Rows[0]["BillingConNo"].ToString(), out _IsBillingContact);
                bool.TryParse(dt.Rows[0]["BillingGSTN"].ToString(), out _IsBillingGST);
                bool.TryParse(dt.Rows[0]["BillingPlaceOfSupply"].ToString(), out _IsBillingPlaceOfSupply);

                bool.TryParse(dt.Rows[0]["ShippingName"].ToString(), out _IsShippingName);
                bool.TryParse(dt.Rows[0]["ShippingAddress"].ToString(), out _IsShippingAddress);
                bool.TryParse(dt.Rows[0]["ShippingTown"].ToString(), out _IsShippingCityTown);
                bool.TryParse(dt.Rows[0]["ShippingState"].ToString(), out _IsShippingState);
                bool.TryParse(dt.Rows[0]["ShippingDist"].ToString(), out _IsShippingDistrict);
                bool.TryParse(dt.Rows[0]["ShippingPin"].ToString(), out _IsShippingPin);
                bool.TryParse(dt.Rows[0]["ShippingConNo"].ToString(), out _IsShippingContact);
                bool.TryParse(dt.Rows[0]["ShippingGSTN"].ToString(), out _IsShippingGST);
                bool.TryParse(dt.Rows[0]["ShippingPlaceOfSupply"].ToString(), out _IsShippingPlaceOfSupply);

                bool.TryParse(dt.Rows[0]["IsBankDetails"].ToString(), out _IsOrgBankDetails);
                bool.TryParse(dt.Rows[0]["IsTermAndCondition"].ToString(), out _IsOrgTermsAndCondition);
                bool.TryParse(dt.Rows[0]["IsDeclaration"].ToString(), out _IsDeclaration);
                bool.TryParse(dt.Rows[0]["IsBillingTerm"].ToString(), out _IsBillingTerm);
                bool.TryParse(dt.Rows[0]["IsBillingDueDate"].ToString(), out _IsBillingDueDate);

                bool.TryParse(dt.Rows[0]["IsPreparedby"].ToString(), out _IsPreparedBy);
                bool.TryParse(dt.Rows[0]["IsAuthentication"].ToString(), out _IsAuthentication);
                bool.TryParse(dt.Rows[0]["IsSignatureAuthority"].ToString(), out _IsSignatureAuthority);
                bool.TryParse(dt.Rows[0]["IsSignatureFor"].ToString(), out _IsSignatureFor);
                bool.TryParse(dt.Rows[0]["IsBottomOfPage"].ToString(), out _IsFooterBottomOfthePage);

                _OrgBankDetailsText = dt.Rows[0]["BankDetails"].ToString();
                _DeclarationText = dt.Rows[0]["Declaration"].ToString();
                _OrgTermsAndConditionText = dt.Rows[0]["TermAndCondition"].ToString();
                _PreparedByText = dt.Rows[0]["PrepardBy"].ToString();
                _AuthenticationText = dt.Rows[0]["Authentication"].ToString();
                _SignatureAuthorityText = dt.Rows[0]["SignatureAuthority"].ToString();
                _SignatureForText = dt.Rows[0]["SignatureFor"].ToString();
                CheckInvoiceCreateOrNot();
                #endregion
            }
        }
    }
}
