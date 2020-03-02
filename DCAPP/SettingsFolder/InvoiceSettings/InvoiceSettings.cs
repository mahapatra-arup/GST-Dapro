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
    public partial class InvoiceSettings : Form
    {
        //public bool mIsUpdate = false;
        string msg = "", query = "";
        List<string> mlistqury = new List<string>();
        public InvoiceSettings()
        {
            InitializeComponent();
            CheckedChangedEventMinus();
            ShowData();
            SetData();
            CheckedChangedEventPlus();

        }
        private void CheckedChangedEventMinus()
        {
            chkOrgNAme.CheckedChanged -= chkOrgNAme_CheckedChanged;
            chkOrgAddress.CheckedChanged -= chkOrgAddress_CheckedChanged;
            chkOrgState.CheckedChanged -= chkOrgState_CheckedChanged;
            chkOrgDist.CheckedChanged -= chkOrgDist_CheckedChanged;
            chkOrgPin.CheckedChanged -= chkOrgPin_CheckedChanged;
            chkOrgContact.CheckedChanged -= chkOrgContact_CheckedChanged;
            chkOrgTown.CheckedChanged -= chkOrgTown_CheckedChanged;

            chkCIN.CheckedChanged -= chkCIN_CheckedChanged;
            chkGSTIN.CheckedChanged -= chkGSTIN_CheckedChanged;
            chkStateCode.CheckedChanged -= chkStateCode_CheckedChanged;
            chkPAN.CheckedChanged -= chkPAN_CheckedChanged;
            chkOrgEmail.CheckedChanged -= chkOrgEmail_CheckedChanged;
            chkOrgWebsite.CheckedChanged -= chkOrgWebsite_CheckedChanged;
            chkOrgLogo.CheckedChanged -= chkOrgLogo_CheckedChanged;

            chkbxBillingNAme.CheckedChanged -= chkbxBillingNAme_CheckedChanged;
            chkbxBillingAddress.CheckedChanged -= chkbxBillingAddress_CheckedChanged;
            chkbxBillingTown.CheckedChanged -= chkbxBillingTown_CheckedChanged;
            chkbxBillingState.CheckedChanged -= chkbxBillingState_CheckedChanged;
            chkbxBIllPin.CheckedChanged -= chkbxBIllPin_CheckedChanged;
            chkbxBillingDist.CheckedChanged -= chkbxBillingDist_CheckedChanged;
            chkbxBillingContactNo.CheckedChanged -= chkbxBillingContactNo_CheckedChanged;
            chkbxBillingGST.CheckedChanged -= chkbxBillingGST_CheckedChanged;
            chkbxBillingPlaceOfSupply.CheckedChanged -= chkbxBillingPlaceOfSupply_CheckedChanged;
            chkBillingTerm.CheckedChanged -= chkBillingTerm_CheckedChanged;
            chkBillingDueDate.CheckedChanged -= chkBillingDueDate_CheckedChanged;

            chkShippedName.CheckedChanged -= chkShippedName_CheckedChanged;
            chkShippingAddress.CheckedChanged -= chkShippingAddress_CheckedChanged;
            chkbxShippingTown.CheckedChanged -= chkbxShippingTown_CheckedChanged;
            chkShippingState.CheckedChanged -= chkbxState_CheckedChanged;
            chkbxShippingDist.CheckedChanged -= chkbxShippingDist_CheckedChanged;
            chkbxShippingPIN.CheckedChanged -= chkbxShippingPIN_CheckedChanged;
            chkbxShippingContactNo.CheckedChanged -= chkbxShippingContactNo_CheckedChanged;
            chkbxShippingGstin.CheckedChanged -= chkbxShippingGstin_CheckedChanged;
            chkbxShippingPlaceOfSupply.CheckedChanged -= chkbxShippingPlaceOfSupply_CheckedChanged;

            chkbxBank.CheckedChanged -= chkbxBank_CheckedChanged;
            chkbxDeclaration.CheckedChanged -= chkbxDeclaration_CheckedChanged;
            chkbxTermsAndCondition.CheckedChanged -= chkbxTermsAndCondition_CheckedChanged;
            chkbxPrepardBy.CheckedChanged -= chkbxPrepardBy_CheckedChanged;
            chkbxAuthentication.CheckedChanged -= chkbxAuthentication_CheckedChanged;
            chkbxSignatureAuthority.CheckedChanged -= chkbxSignatureAuthority_CheckedChanged;
            chkbxSignaturefor.CheckedChanged -= chkbxSignaturefor_CheckedChanged;
            rbtnEndOfPage.CheckedChanged -= rbtnEndOfPage_CheckedChanged;

        }
        private void CheckedChangedEventPlus()
        {
            chkOrgNAme.CheckedChanged += chkOrgNAme_CheckedChanged;
            chkOrgAddress.CheckedChanged += chkOrgAddress_CheckedChanged;
            chkOrgState.CheckedChanged += chkOrgState_CheckedChanged;
            chkOrgDist.CheckedChanged += chkOrgDist_CheckedChanged;
            chkOrgPin.CheckedChanged += chkOrgPin_CheckedChanged;
            chkOrgContact.CheckedChanged += chkOrgContact_CheckedChanged;
            chkOrgTown.CheckedChanged += chkOrgTown_CheckedChanged;

            chkCIN.CheckedChanged += chkCIN_CheckedChanged;
            chkGSTIN.CheckedChanged += chkGSTIN_CheckedChanged;
            chkStateCode.CheckedChanged += chkStateCode_CheckedChanged;
            chkPAN.CheckedChanged += chkPAN_CheckedChanged;
            chkOrgEmail.CheckedChanged += chkOrgEmail_CheckedChanged;
            chkOrgWebsite.CheckedChanged += chkOrgWebsite_CheckedChanged;
            chkOrgLogo.CheckedChanged += chkOrgLogo_CheckedChanged;

            chkbxBillingNAme.CheckedChanged += chkbxBillingNAme_CheckedChanged;
            chkbxBillingAddress.CheckedChanged += chkbxBillingAddress_CheckedChanged;
            chkbxBillingTown.CheckedChanged += chkbxBillingTown_CheckedChanged;
            chkbxBillingState.CheckedChanged += chkbxBillingState_CheckedChanged;
            chkbxBIllPin.CheckedChanged += chkbxBIllPin_CheckedChanged;
            chkbxBillingDist.CheckedChanged += chkbxBillingDist_CheckedChanged;
            chkbxBillingContactNo.CheckedChanged += chkbxBillingContactNo_CheckedChanged;
            chkbxBillingGST.CheckedChanged += chkbxBillingGST_CheckedChanged;
            chkbxBillingPlaceOfSupply.CheckedChanged += chkbxBillingPlaceOfSupply_CheckedChanged;
            chkBillingTerm.CheckedChanged += chkBillingTerm_CheckedChanged;
            chkBillingDueDate.CheckedChanged += chkBillingDueDate_CheckedChanged;

            chkShippedName.CheckedChanged += chkShippedName_CheckedChanged;
            chkShippingAddress.CheckedChanged += chkShippingAddress_CheckedChanged;
            chkbxShippingTown.CheckedChanged += chkbxShippingTown_CheckedChanged;
            chkShippingState.CheckedChanged += chkbxState_CheckedChanged;
            chkbxShippingDist.CheckedChanged += chkbxShippingDist_CheckedChanged;
            chkbxShippingPIN.CheckedChanged += chkbxShippingPIN_CheckedChanged;
            chkbxShippingContactNo.CheckedChanged += chkbxShippingContactNo_CheckedChanged;
            chkbxShippingGstin.CheckedChanged += chkbxShippingGstin_CheckedChanged;
            chkbxShippingPlaceOfSupply.CheckedChanged += chkbxShippingPlaceOfSupply_CheckedChanged;

            chkbxBank.CheckedChanged += chkbxBank_CheckedChanged;
            chkbxDeclaration.CheckedChanged += chkbxDeclaration_CheckedChanged;
            chkbxTermsAndCondition.CheckedChanged += chkbxTermsAndCondition_CheckedChanged;
            chkbxPrepardBy.CheckedChanged += chkbxPrepardBy_CheckedChanged;
            chkbxAuthentication.CheckedChanged += chkbxAuthentication_CheckedChanged;
            chkbxSignatureAuthority.CheckedChanged += chkbxSignatureAuthority_CheckedChanged;
            chkbxSignaturefor.CheckedChanged += chkbxSignaturefor_CheckedChanged;
            rbtnEndOfPage.CheckedChanged += rbtnEndOfPage_CheckedChanged;

        }
        private void ShowData()
        {
            lblCIN.Text = ORG_Tools._CorporateNo;
            lblGST.Text = ORG_Tools._GSTin;
            lblPAN.Text = ORG_Tools._PAN;
            lblState.Text = ORG_Tools._StateCode;

            lblOrgName.Text = ORG_Tools._OrganizationName;
            lblOrgAddress.Text = ORG_Tools._Address;
            lblOrgTown.Text = ORG_Tools._CityTown;
            lblOrgStateAddresss.Text = ORG_Tools._State;
            lblOrgDist.Text = ORG_Tools._Dist;
            lblOrgPIN.Text = ORG_Tools._PIN;
            lblOrgContact.Text = ORG_Tools._ContactNo1;
            lblEmail.Text = ORG_Tools._Email;
            lblWebsite.Text = ORG_Tools._website;
            picbOrgLogo.Image = ORG_Tools._Logo;

            lblBillingNAme.Text = ORG_Tools._NameBilling;
            lblBillingAddress.Text = ORG_Tools._AddressBilling;
            lblBillingTon.Text = ORG_Tools._CityTownBilling;
            lblBillingState.Text = ORG_Tools._StateBilling;
            lblBillingDist.Text = ORG_Tools._DistBilling;
            lblBillingPIN.Text = ORG_Tools._PINBilling;
            lblBillingCnoNo.Text = ORG_Tools._ContactNo1;
            lblBillingGSTIN.Text = ORG_Tools._GSTin;
            lblBillingPlaceOfSupply.Text = ORG_Tools._AddressBilling;

            lblShippingName.Text = ORG_Tools._NameShipping;
            lblShippingAddress.Text = ORG_Tools._AddressShipping;
            lblShippingTown.Text = ORG_Tools._CityTownShipping;
            lblShippingState.Text = ORG_Tools._StateShipping;
            lblShippingDist.Text = ORG_Tools._DistShipping;
            lblShippingPIN.Text = ORG_Tools._PINShipping;
            lblShippingContact.Text = ORG_Tools._ContactShipping;
            lblShippingGstin.Text = ORG_Tools._GSTin;
            lblShippingPlaceOfSupply.Text = ORG_Tools._AddressShipping;

        }

        private void SetData()
        {
            if (!INVOICE_TOOLS._ISInvoiceCreate)
            {
                txtInvoiceStartfrom.Enabled = true;
                txtInvoiceStartfrom.Text = INVOICE_TOOLS._InvoiceStartFrom.ToString();
            }
            else
            {
                txtInvoiceStartfrom.Enabled = false;
                // txtInvoiceStartfrom.Text = "1";
                txtInvoiceStartfrom.Text = INVOICE_TOOLS._InvoiceStartFrom.ToString();

            }
            txtInvoiceString.Text = INVOICE_TOOLS._InvoiceString;
            txtBankDetails.Text = INVOICE_TOOLS._OrgBankDetailsText;
            txtDeclaration.Text = INVOICE_TOOLS._DeclarationText;
            txtTermsAndCondition.Text = INVOICE_TOOLS._OrgTermsAndConditionText;
            txtSignatureString.Text = INVOICE_TOOLS._SignatureForText;
            txtSignatureAuthority.Text = INVOICE_TOOLS._SignatureAuthorityText;
            txtAuthentication.Text = INVOICE_TOOLS._AuthenticationText;
            txtPreparedBY.Text = INVOICE_TOOLS._PreparedByText;


            chkCIN.Checked = INVOICE_TOOLS._IsOrgCIN;
            chkGSTIN.Checked = INVOICE_TOOLS._IsOrgGSTIN;
            chkPAN.Checked = INVOICE_TOOLS._IsOrgPAN;
            chkStateCode.Checked = INVOICE_TOOLS._IsOrgStateWithCode;

            chkOrgNAme.Checked = INVOICE_TOOLS._IsOrgName;
            chkOrgAddress.Checked = INVOICE_TOOLS._IsOrgAddress;
            chkOrgTown.Checked = INVOICE_TOOLS._IsOrgCityTown;
            chkOrgState.Checked = INVOICE_TOOLS._IsOrgState;
            chkOrgDist.Checked = INVOICE_TOOLS._IsOrgDistrict;
            chkOrgPin.Checked = INVOICE_TOOLS._IsOrgPin;
            chkOrgContact.Checked = INVOICE_TOOLS._IsOrgContactNo;
            chkOrgEmail.Checked = INVOICE_TOOLS._IsOrgMailID;
            chkOrgWebsite.Checked = INVOICE_TOOLS._IsOrgWebsite;
            chkOrgLogo.Checked = INVOICE_TOOLS._IsOrgLogo;


            chkbxBillingNAme.Checked = INVOICE_TOOLS._IsBillingName;
            chkbxBillingAddress.Checked = INVOICE_TOOLS._IsBillingAddress;
            chkbxBillingTown.Checked = INVOICE_TOOLS._IsBillingCityTown;
            chkbxBillingState.Checked = INVOICE_TOOLS._IsBillingState;
            chkbxBillingDist.Checked = INVOICE_TOOLS._IsBillingDistrict;
            chkbxBIllPin.Checked = INVOICE_TOOLS._IsBillingPin;
            chkbxBillingContactNo.Checked = INVOICE_TOOLS._IsBillingContact;
            chkbxBillingGST.Checked = INVOICE_TOOLS._IsBillingGST;
            chkbxBillingPlaceOfSupply.Checked = INVOICE_TOOLS._IsBillingPlaceOfSupply;

            chkShippedName.Checked = INVOICE_TOOLS._IsShippingName;
            chkShippingAddress.Checked = INVOICE_TOOLS._IsShippingAddress;
            chkbxShippingTown.Checked = INVOICE_TOOLS._IsShippingCityTown;
            chkShippingState.Checked = INVOICE_TOOLS._IsShippingState;
            chkbxShippingDist.Checked = INVOICE_TOOLS._IsShippingDistrict;
            chkbxShippingPIN.Checked = INVOICE_TOOLS._IsShippingPin;
            chkbxShippingContactNo.Checked = INVOICE_TOOLS._IsShippingContact;
            chkbxShippingGstin.Checked = INVOICE_TOOLS._IsShippingGST;
            chkbxShippingPlaceOfSupply.Checked = INVOICE_TOOLS._IsShippingPlaceOfSupply;

            chkbxBank.Checked = INVOICE_TOOLS._IsOrgBankDetails;
            chkbxDeclaration.Checked = INVOICE_TOOLS._IsDeclaration;
            chkbxTermsAndCondition.Checked = INVOICE_TOOLS._IsOrgTermsAndCondition;
            chkBillingDueDate.Checked = INVOICE_TOOLS._IsBillingDueDate;
            chkBillingTerm.Checked = INVOICE_TOOLS._IsBillingTerm;

            chkbxPrepardBy.Checked = INVOICE_TOOLS._IsPreparedBy;
            chkbxAuthentication.Checked = INVOICE_TOOLS._IsAuthentication;
            chkbxSignatureAuthority.Checked = INVOICE_TOOLS._IsSignatureAuthority;
            chkbxSignaturefor.Checked = INVOICE_TOOLS._IsSignatureFor;
            rbtnEndOfPage.Checked = INVOICE_TOOLS._IsFooterBottomOfthePage;
            rbtnEndOfContent.Checked = !INVOICE_TOOLS._IsFooterBottomOfthePage;






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
        private void txtDeclaration_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void chkbxPrepardBy_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsPreparedby='" + chkbxPrepardBy.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Preparedby.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxPrepardBy.Checked)
            {
                txtPreparedBY.Focus();
            }

        }
        private void chkbxAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsAuthentication='" + chkbxAuthentication.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Authentication.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxAuthentication.Checked)
            {
                txtAuthentication.Focus();
            }
        }
        private void chkbxBank_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsBankDetails='" + chkbxBank.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Org BankDetails.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxBank.Checked)
            {
                txtBankDetails.Focus();
            }
        }
        private void chkbxDeclaration_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsDeclaration='" + chkbxDeclaration.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at  Declaration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxDeclaration.Checked)
            {
                txtDeclaration.Focus();

            }
        }
        private void chkbxTermsAndCondition_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsTermAndCondition='" + chkbxTermsAndCondition.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at TermAndCondition.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxTermsAndCondition.Checked)
            {
                txtTermsAndCondition.Focus();

            }
        }

        /// <summary>
        /// SAVE
        /// </summary>

        private void chkCIN_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set OrgCin='" + chkCIN.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at chgkedBox Cin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkGSTIN_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgGSTN='" + chkGSTIN.Checked + "'";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org Gstin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkPAN_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ORGPanNo='" + chkPAN.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org Pan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void chkOrgNAme_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgName='" + chkOrgNAme.Checked + "'";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkOrgAddress_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgAddress='" + chkOrgAddress.Checked + "'";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOrgTown_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgTown='" + chkOrgTown.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org City/Town.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOrgState_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgState='" + chkOrgState.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org state.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkOrgDist_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set orgDist='" + chkOrgDist.Checked + "'";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org dist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOrgPin_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set OrgPin='" + chkOrgPin.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org pin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkOrgContact_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set OrgPhNo='" + chkOrgContact.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxBillingNAme_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set BillingName='" + chkbxBillingNAme.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Billing Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxBillingAddress_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingAddress='" + chkbxBillingAddress.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxBillingTown_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingTown='" + chkbxBillingTown.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing Town", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxBillingState_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingState='" + chkbxBillingState.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing sate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxBillingDist_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingDist='" + chkbxBillingDist.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing Dist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxBIllPin_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set BillingPin='" + chkbxBIllPin.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing pin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxBillingContactNo_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingConNo='" + chkbxBillingContactNo.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billling Contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxBillingGST_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingGSTN='" + chkbxBillingGST.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing Gstin. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxBillingPlaceOfSupply_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set BillingPlaceOfSupply='" + chkbxBillingPlaceOfSupply.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Billing place of supply.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkBillingTerm_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsBillingTerm='" + chkBillingTerm.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing term.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkBillingDueDate_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set IsBillingDueDate='" + chkBillingDueDate.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at billing due date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShippedName_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingName='" + chkShippedName.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Shipping name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShippingAddress_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingAddress='" + chkShippingAddress.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxShippingTown_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingTown='" + chkbxShippingTown.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping town", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxState_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingState='" + chkShippingState.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxShippingDist_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingDist='" + chkbxShippingDist.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping district.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxShippingPIN_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingPin='" + chkbxShippingPIN.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping pin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxShippingContactNo_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingConNo='" + chkbxShippingContactNo.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxShippingGstin_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingGSTN='" + chkbxShippingGstin.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at shipping Gstin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkbxShippingPlaceOfSupply_CheckedChanged(object sender, EventArgs e)
        {

            query = "update InvoiceSettings set ShippingPlaceOfSupply='" + chkbxShippingPlaceOfSupply.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Shipping place of supply.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtInvoiceString_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtInvoiceString.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtInvoiceString.Text.GetDBFormatString() + "'";
            query = "update InvoiceSettings set InvoiceStart=" + txtBoxString + "";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBankDetails_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtBankDetails.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtBankDetails.Text.GetDBFormatString() + "'";
            if (txtBankDetails.Text.ISNullOrWhiteSpace())
            {
                chkbxBank.Checked = false;
            }
            query = "update InvoiceSettings set BankDetails=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Bank details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDeclaration_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtDeclaration.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtDeclaration.Text.GetDBFormatString() + "'";

            if (txtDeclaration.Text.ISNullOrWhiteSpace())
            {
                chkbxDeclaration.Checked = false;
            }
            query = "update InvoiceSettings set Declaration=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Decreartion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtTermsAndCondition_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtTermsAndCondition.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtTermsAndCondition.Text.GetDBFormatString() + "'";

            if (txtTermsAndCondition.Text.ISNullOrWhiteSpace())
            {
                chkbxTermsAndCondition.Checked = false;
            }
            query = "update InvoiceSettings set TermAndCondition=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at TermAndCondition.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtPreparedBY_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtPreparedBY.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtPreparedBY.Text.GetDBFormatString() + "'";

            if (txtPreparedBY.Text.ISNullOrWhiteSpace())
            {
                chkbxPrepardBy.Checked = false;
            }
            query = "update InvoiceSettings set PrepardBy=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at PrepardBy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAuthentication_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtAuthentication.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtAuthentication.Text.GetDBFormatString() + "'";

            if (txtAuthentication.Text.ISNullOrWhiteSpace())
            {
                chkbxAuthentication.Checked = false;
            }
            query = "update InvoiceSettings set Authentication=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Authentication.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSignatureString_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtSignatureString.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtSignatureString.Text.GetDBFormatString() + "'";

            if (txtSignatureString.Text.ISNullOrWhiteSpace())
            {
                chkbxSignaturefor.Checked = false;
            }
            query = "update InvoiceSettings set SignatureFor=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Signaturefor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSignatureAuthority_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtSignatureAuthority.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtSignatureAuthority.Text.GetDBFormatString() + "'";

            if (txtSignatureAuthority.Text.ISNullOrWhiteSpace())
            {
                chkbxSignatureAuthority.Checked = false;
            }
            query = "update InvoiceSettings set SignatureAuthority=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at SignatureAuthority.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkbxSignatureAuthority_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsSignatureAuthority='" + chkbxSignatureAuthority.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Signature Authority.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxSignatureAuthority.Checked)
            {
                txtSignatureAuthority.Focus();
            }
        }

        private void chkbxSignaturefor_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsSignatureFor='" + chkbxSignaturefor.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at SignatureFor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (chkbxSignaturefor.Checked)
            {
                txtSignatureString.Focus();
            }
        }

        private void InvoiceSettings_Leave(object sender, EventArgs e)
        {
            INVOICE_TOOLS.InitDetails();
            INVOICE_TOOLS.CheckInvoiceCreateOrNot();
        }

        private void chkStateCode_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set OrgStateWithCode='" + chkStateCode.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at org Statewith code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBankDetails_TextChanged(object sender, EventArgs e)
        {
            int remaning = txtBankDetails.MaxLength - txtBankDetails.Text.Length;
            lblbankmax.Text = remaning.ToString();
        }
        private void txtDeclaration_TextChanged(object sender, EventArgs e)
        {
            int remaning = txtDeclaration.MaxLength - txtDeclaration.Text.Length;
            lbldeclartion.Text = remaning.ToString();
        }
        private void txtTermsAndCondition_TextChanged(object sender, EventArgs e)
        {
            int remaning = txtTermsAndCondition.MaxLength - txtTermsAndCondition.Text.Length;
            lbltermandcondition.Text = remaning.ToString();
        }

        private void chkOrgEmail_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set Email='" + chkOrgEmail.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Org Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOrgWebsite_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set Website='" + chkOrgWebsite.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Org Website.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkOrgLogo_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set Logo='" + chkOrgLogo.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Org Logo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEstimateStartfrom_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtInvoiceStartfrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbtnEndOfPage_CheckedChanged(object sender, EventArgs e)
        {
            query = "update InvoiceSettings set IsBottomOfPage='" + rbtnEndOfPage.Checked + "'";

            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at Is Bottom Of Page.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtInvoiceStartfrom_Leave(object sender, EventArgs e)
        {
            string txtBoxString = txtInvoiceStartfrom.Text.ISNullOrWhiteSpace() ? "1" : "" + txtInvoiceStartfrom.Text.GetDBFormatString() + "";

            query = "update InvoiceSettings set SerialStartFrom=" + txtBoxString + "";
            if (!SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                MessageBox.Show("Internal Error at SerialStartFrom.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
