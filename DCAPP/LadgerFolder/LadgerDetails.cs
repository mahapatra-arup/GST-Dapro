using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAPRO;

namespace DAPRO
{
    public partial class LedgerDetails : Form
    {
        private string msg = "";
        private bool mIsSuccess = false;
        public enum _LedgerCategory
        {
            Supplier,
            Customer,
            Employee,
            Cash_Customer
        }
        public _LedgerCategory mladgerCategory;
        public enum _Type
        {
            show,
            showDialog
        }
        private _Type mType;
        public event Action<string> OnClose;
        private string mtableid = "";
        string mEstimateid = "";
        private string mLedgerIdForEdit = "";
        List<string> mlstQry = new List<string>();

        string mSubAccount = "";
        string mParentAccount = "";
        string mAccountType = "NULL";

        public LedgerDetails(_LedgerCategory ladgerCategory, _Type type)
        {
            InitializeComponent();
            cmbLedgerCategory.Enabled = false;
            mladgerCategory = ladgerCategory;
            mType = type;
            cmbLedgerCategory.Text = mladgerCategory.ToString();
            cmbState.AddState();
            cmbStateShipping.AddState();
            cmbStateBilling.AddState();
            cmbDist.AddDist();
            cmbDistShipping.AddDist();
            cmbDistBilling.AddDist();
            cmbBankName.AddBank();
            cmbBillingTerms.AddBillingTerms();
            cmbDesignation.AddDesignation();
            cmbState.Text = ORG_Tools._State;

            cmbBillingTerms.Text = "ON RECEIPT";
            cmbGSTCatagory.Text = "Consumer";
            if (mladgerCategory != _LedgerCategory.Customer)
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
            }
            if (mladgerCategory == _LedgerCategory.Customer)
            {
                SetSameStateNotRegularCustomer();
            }
        }
        public LedgerDetails(_LedgerCategory ladgerCategory, _Type type, string estimateid, string frmwhere)// from estimate
        {
            InitializeComponent();
            cmbLedgerCategory.Enabled = false;
            mladgerCategory = ladgerCategory;
            mEstimateid = estimateid;
            mType = type;
            cmbLedgerCategory.Text = mladgerCategory.ToString();
            cmbState.AddState();
            cmbStateShipping.AddState();
            cmbStateBilling.AddState();
            cmbDist.AddDist();
            cmbDistShipping.AddDist();
            cmbDistBilling.AddDist();
            cmbBankName.AddBank();
            cmbBillingTerms.AddBillingTerms();
            cmbDesignation.AddDesignation();
            cmbBillingTerms.Text = "ON RECEIPT";
            GetEstimatedetails();
            if (mladgerCategory == _LedgerCategory.Customer)
            {
                SetSameStateNotRegularCustomer();
            }
        }
        private void GetEstimatedetails()
        {
            string query = "select * from estimate where estimateid='" + mEstimateid + "' ";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                txtLedgerName.Text = dt.Rows[0]["PartyName"].ToString();
                cmbState.Text = dt.Rows[0]["StateName"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtMobile.Text = dt.Rows[0]["ContactNo"].ToString();
                txttemplatename.Text = dt.Rows[0]["TemplateName"].ToString();
            }
        }
        public LedgerDetails(_LedgerCategory ladgerCategory, _Type type, string ledgersId)
        {
            InitializeComponent();
            cmbLedgerCategory.Enabled = false;
            mladgerCategory = ladgerCategory;
            mType = type;
            mLedgerIdForEdit = ledgersId;
            cmbLedgerCategory.Text = mladgerCategory.ToString();
            cmbState.AddState();
            cmbDist.AddDist();
            cmbStateShipping.AddState();
            cmbDistShipping.AddDist();
            cmbStateBilling.AddState();
            cmbDistBilling.AddDist();
            cmbBankName.AddBank();
            cmbBillingTerms.AddBillingTerms();
            cmbDesignation.AddDesignation();

            ShowDetails();
            cmbLedgerCategory.Enabled = false;
            if (mladgerCategory == _LedgerCategory.Customer)
            {
                SetSameStateNotRegularCustomer();
            }
            if (mladgerCategory != _LedgerCategory.Customer)
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
            }
        }
        private void SetSameStateNotRegularCustomer()
        {
            if (!ORG_Tools._IsRegularGST)
            {
                cmbState.Text = ORG_Tools._State;
                cmbState.Enabled = false;
                cmbStateBilling.Text = ORG_Tools._State;
                cmbStateBilling.Enabled = false;
                cmbStateShipping.Text = ORG_Tools._State;
                cmbStateShipping.Enabled = false;
            }
        }
        private void ShowDetails()
        {
            switch (mladgerCategory)
            {
                case _LedgerCategory.Supplier:
                    ShowSupplierDetails();
                    break;
                case _LedgerCategory.Customer:
                    ShowCustomerDetails();
                    break;
                case _LedgerCategory.Employee:
                    ShowEmployeeDetails();
                    break;
                case _LedgerCategory.Cash_Customer:
                    ShowCashCustomersDetails();
                    break;
                default:
                    break;
            }
        }
        private void ShowCashCustomersDetails()
        {
            string qry = "select Ledgers.*,ladgermain.GSTIN,GSTRegistrationType,TemplateName from Ledgers left join ladgermain on ledgers.ledgerid=ladgermain.ladgerid Where Ledgers.id='" + mtableid + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
            if (dt.IsValidDataTable())
            {
                txtLedgerName.Text = dt.Rows[0]["LedgerName"].ToString();
                txttemplatename.Text = dt.Rows[0]["TemplateName"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtTown.Text = dt.Rows[0]["City_Town"].ToString();
                cmbState.Text = dt.Rows[0]["State"].ToString();
                cmbDist.Text = dt.Rows[0]["Dist"].ToString();
                txtPIN.Text = dt.Rows[0]["PinCode"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPanNO.Text = dt.Rows[0]["PAN"].ToString();
                cmbGSTCatagory.Text = dt.Rows[0]["GSTRegistrationType"].ToString();
                txtGSTIN.Text = dt.Rows[0]["GSTIN"].ToString();
                txtAadherNo.Text = dt.Rows[0]["AadharNo"].ToString();
            }
        }
        private void ShowSupplierDetails()
        {
            string qry = "SELECT  LadgerMain.*,Ledgers.*,Suppliers.Website,Suppliers.Note, " +
                         "LedgerStatus.Openingbalance,LedgerStatus.Date from Ledgers " +
                         " inner join LadgerMain on Ledgers.LedgerID=LadgerMain.LadgerID " +
                         " inner join Suppliers on LadgerMain.LadgerID=Suppliers.LedgerID " +
                         " inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID" +
                         " where Ledgers.LedgerID='" + mLedgerIdForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
            if (dt.IsValidDataTable())
            {
                txtLedgerName.Text = dt.Rows[0]["LedgerName"].ToString();
                txttemplatename.Text = dt.Rows[0]["TemplateName"].ToString();
                txtCompanyName.Text = dt.Rows[0]["Company"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtTown.Text = dt.Rows[0]["City_Town"].ToString();
                cmbState.Text = dt.Rows[0]["State"].ToString();
                cmbDist.Text = dt.Rows[0]["Dist"].ToString();
                txtPIN.Text = dt.Rows[0]["PinCode"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPanNO.Text = dt.Rows[0]["PAN"].ToString();
                cmbGSTCatagory.Text = dt.Rows[0]["GSTRegistrationType"].ToString();
                txtGSTIN.Text = dt.Rows[0]["GSTIN"].ToString();

                txtNote.Text = dt.Rows[0]["Note"].ToString();
                txtWebsite.Text = dt.Rows[0]["Website"].ToString();
                string openingbalstr = dt.Rows[0]["Openingbalance"].ToString();
                double openingbal = 0d;
                double.TryParse(openingbalstr, out openingbal);
                txtOpeningBalance.Text = openingbal == 0 ? "" : openingbal < 0 ? (-(openingbal)).ToString() : openingbal.ToString();
                cmbcrdr.Text = openingbal < 0 ? "Cr." : "Dr.";
                dtpDateIOpening.Value = DateTime.Parse(dt.Rows[0]["Date"].ToString());
                cmbBillingTerms.Text = dt.Rows[0]["BillingTerms"].ToString();
                string bankId = dt.Rows[0]["BankID"].ToString();
                cmbBankName.Text = GetBankName(bankId);
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();
                txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
                txtAadherNo.Text = dt.Rows[0]["AadharNo"].ToString();
            }
        }
        private void ShowCustomerDetails()
        {
            string qry = "SELECT  Ledgers.*,LadgerMain.*, Customers.BillingName, " +
                         "Customers.BillingAddress, Customers.BillingTown, Customers.BillingState, " +
                         "Customers .BillingDist,Customers.BillingPIN,Customers.ShippingName, " +
                         "Customers.ShippingContactNo, Customers.ShippingAddress, Customers.ShippingTown, " +
                         "Customers.ShippingDist,Customers.ShippingState,Customers.ShippingPIN, " +
                         "Customers.Website, LedgerStatus.OpeningBalance,LedgerStatus.Date, Customers.Note " +
                         "from Ledgers " +
                         "inner join LadgerMain on Ledgers.LedgerID=LadgerMain.LadgerID " +
                         " inner join Customers on LadgerMain.LadgerID=Customers.LedgerID " +
                         " inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID" +
                         " where Ledgers.LedgerID='" + mLedgerIdForEdit + "'";


            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
            if (dt.IsValidDataTable())
            {

                txtLedgerName.Text = dt.Rows[0]["LedgerName"].ToString();
                txttemplatename.Text = dt.Rows[0]["TemplateName"].ToString();
                txtCompanyName.Text = dt.Rows[0]["Company"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtTown.Text = dt.Rows[0]["City_Town"].ToString();

                //string stateId= GetSateName(stateId);
                cmbState.Text = dt.Rows[0]["State"].ToString();
                cmbDist.Text = dt.Rows[0]["Dist"].ToString();
                txtPIN.Text = dt.Rows[0]["PinCode"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPanNO.Text = dt.Rows[0]["PAN"].ToString();

                cmbGSTCatagory.Text = dt.Rows[0]["GSTRegistrationType"].ToString();
                txtGSTIN.Text = dt.Rows[0]["GSTIN"].ToString();

                txtAddressShipping.Text = dt.Rows[0]["ShippingAddress"].ToString();
                txtTownShipping.Text = dt.Rows[0]["ShippingTown"].ToString();
                cmbStateShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                cmbDistShipping.Text = dt.Rows[0]["ShippingDist"].ToString();
                txtPinShipping.Text = dt.Rows[0]["ShippingPIN"].ToString();
                txtContactNoShipping.Text = dt.Rows[0]["ShippingContactNo"].ToString();
                txtNameShipping.Text = dt.Rows[0]["ShippingName"].ToString();

                txtNameBilling.Text = dt.Rows[0]["BillingName"].ToString();
                txtAddressBilling.Text = dt.Rows[0]["BillingAddress"].ToString();
                txtTownBilling.Text = dt.Rows[0]["BillingTown"].ToString();
                cmbStateBilling.Text = dt.Rows[0]["BillingState"].ToString();
                cmbDistBilling.Text = dt.Rows[0]["BillingDist"].ToString();
                txtPINBilling.Text = dt.Rows[0]["BillingPIN"].ToString();


                txtNote.Text = dt.Rows[0]["Note"].ToString();
                txtWebsite.Text = dt.Rows[0]["Website"].ToString();

                string openingbalstr = dt.Rows[0]["Openingbalance"].ToString();
                double openingbal = 0d;
                double.TryParse(openingbalstr, out openingbal);
                txtOpeningBalance.Text = openingbal == 0 ? "" : openingbal < 0 ? (-(openingbal)).ToString() : openingbal.ToString();
                cmbcrdr.Text = openingbal < 0 ? "Cr." : "Dr.";
                dtpDateIOpening.Value = DateTime.Parse(dt.Rows[0]["Date"].ToString());

                //string tempBillingTerm = dt.Rows[0]["BillingTermsID"].ToString();
                cmbBillingTerms.Text = dt.Rows[0]["BillingTerms"].ToString();

                string bankId = dt.Rows[0]["BankID"].ToString();
                cmbBankName.Text = GetBankName(bankId);
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
                txtAadherNo.Text = dt.Rows[0]["AadharNo"].ToString();
                txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();
            }
        }
        private void ShowEmployeeDetails()
        {
            string qry = "SELECT  Ledgers.*,LadgerMain.TemplateName,LedgerStatus.Openingbalance, " +
                         "LedgerStatus.Date,Employee.* from Ledgers " +
                         "inner join Employee on Ledgers.LedgerID=Employee.LadgerID " +
                         "inner join LadgerMain on Ledgers.LedgerID=LadgerMain.LadgerID " +
                         " inner join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID" +
                         " where Ledgers.LedgerID='" + mLedgerIdForEdit + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
            if (dt.IsValidDataTable())
            {

                txtLedgerName.Text = dt.Rows[0]["LedgerName"].ToString();
                txttemplatename.Text = dt.Rows[0]["TemplateName"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtTown.Text = dt.Rows[0]["City_Town"].ToString();
                //string stateId= GetSateName(stateId);
                cmbState.Text = dt.Rows[0]["State"].ToString();
                cmbDist.Text = dt.Rows[0]["Dist"].ToString();
                txtPIN.Text = dt.Rows[0]["PinCode"].ToString();

                txtEmployeeID.Text = dt.Rows[0]["EmpCode"].ToString();
                string designationId = dt.Rows[0]["DesignationId"].ToString();
                cmbDesignation.Text = GetDesignationName(designationId);
                cmbGender.Text = dt.Rows[0]["Gender"].ToString();
                cmbBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
                txtDOB.Text = dt.Rows[0]["DOB"].ToString().ISNullOrWhiteSpace() ? "" : DateTime.Parse(dt.Rows[0]["DOB"].ToString()).ToString("dd-MMM-yyyy");

                string openingbalstr = dt.Rows[0]["Openingbalance"].ToString();
                double openingbal = 0d;
                double.TryParse(openingbalstr, out openingbal);
                txtOpeningBalance.Text = openingbal == 0 ? "" : openingbal < 0 ? (-(openingbal)).ToString() : openingbal.ToString();
                cmbcrdr.Text = openingbal < 0 ? "Cr." : "Dr.";
                dtpDateIOpening.Value = DateTime.Parse(dt.Rows[0]["Date"].ToString());
                txtjoingdate.Text = dt.Rows[0]["JoiningDate"].ToString().ISNullOrWhiteSpace() ? "" : DateTime.Parse(dt.Rows[0]["JoiningDate"].ToString()).ToString("dd-MMM-yyyy");
                textBox1.Text = dt.Rows[0]["EmailIdWork"].ToString();//emailwork
                txtNote.Text = dt.Rows[0]["Remarks"].ToString();

                string bankId = dt.Rows[0]["BankID"].ToString();
                cmbBankName.Text = GetBankName(bankId);
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
                txtAadherNo.Text = dt.Rows[0]["AadharNo"].ToString();
                txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();


                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtPanNO.Text = dt.Rows[0]["PAN"].ToString();

            }
        }
        private string GetSateName(string id)
        {
            string query = "Select StateName from State where ID='" + id + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
        private string GetBankName(string id)
        {
            string query = "Select BankName from Bank where ID='" + id + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }

            return null;
        }
        private string GetDesignationName(string id)
        {
            string query = "Select DesignationName from EmployeeDesignation where ID='" + id + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }

            return null;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddNewSuppliers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mIsSuccess == true)
            {
                OnClose(txttemplatename.Text);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsMendatoryFilled())
            {
                if (iSValidTextLength())
                {
                    if (IsNotLedgerDuplicate())
                    {
                        DataSave();
                    }
                }
            }

        }
        private bool iSValidTextLength()
        {
            if (!txtPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtPIN.Text.Length != 6)
                {
                    MessageBox.Show("Please enter a valid PIN no", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPIN.Focus();
                    return false;
                }
            }
            if (!txtMobile.Text.ISNullOrWhiteSpace())
            {
                if (txtMobile.TextLength != 10 && txtMobile.TextLength != 12)
                {
                    MessageBox.Show("Please enter a valid Mobile no.", "Mobile No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtMobile.Focus();
                    return false;
                }
            }
            if (!txtEmail.Text.ISNullOrWhiteSpace())
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Please enter a valid Email ID.", "Mobile No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEmail.Focus();
                    return false;
                }
            }
            if (!txtPhone.Text.ISNullOrWhiteSpace())
            {
                if (txtPhone.TextLength != 12 && txtPhone.TextLength != 10)
                {
                    MessageBox.Show("Please Enter a Valid Phone No", "Phone No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPhone.Focus();
                    return false;
                }
            }
            if (!txtPanNO.Text.ISNullOrWhiteSpace())
            {
                if (txtPanNO.TextLength != 10)
                {
                    MessageBox.Show("Please Enter a Valid 10 Digit PAN No", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPanNO.Focus();
                    return false;
                }
            }
            if (!txtAadherNo.Text.ISNullOrWhiteSpace())
            {
                if (txtAadherNo.TextLength != 14)
                {
                    MessageBox.Show("Please Enter a Valid 12 Digit Aadhar No", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAadherNo.Focus();
                    return false;
                }
            }
            if (cmbLedgerCategory.Text == "Employee")
            {
                if (!txtDOB.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        DateTime.Parse(txtDOB.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter a valid Date.", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtDOB.Focus();
                        return false;
                    }
                }
                if (!txtjoingdate.Text.ISNullOrWhiteSpace())
                {
                    try
                    {
                        DateTime.Parse(txtjoingdate.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter a valid Date.", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtjoingdate.Focus();
                        return false;
                    }
                }

            }
            if (mladgerCategory == _LedgerCategory.Customer)
            {
                if (!txtPINBilling.Text.ISNullOrWhiteSpace())
                {
                    if (txtPINBilling.TextLength != 6)
                    {
                        MessageBox.Show("Please enter a valid PIN no", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtPINBilling.Focus();
                        return false;
                    }
                }

                if (!txtContactNoShipping.Text.ISNullOrWhiteSpace())
                {
                    if (txtContactNoShipping.TextLength != 10 && txtContactNoShipping.TextLength != 12)
                    {
                        MessageBox.Show("Please enter a valid no. for shipping.", "Mobile No", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtContactNoShipping.Focus();
                        return false;
                    }
                }
                if (!txtPinShipping.Text.ISNullOrWhiteSpace())
                {
                    if (txtPinShipping.TextLength != 6)
                    {
                        MessageBox.Show("Please enter a valid PIN no for shipping.", "PIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtPinShipping.Focus();
                        return false;
                    }
                }
            }
            return true;
        }
        private bool FillGstinNumberOrNot()
        {
            if (cmbGSTCatagory.Text == "Regular" || cmbGSTCatagory.Text == "Composition")
            {
                if (txtGSTIN.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("PLlease Enter GSTIN", "GSTIN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtGSTIN.Focus();
                    return false;
                }
                if (!IsInValidGstInNumber())
                {
                    return false;
                }
            }
            return true;
        }
        private void DataSave()
        {
            #region Get Data
            mlstQry.Clear();
            string ledgerId = "";
            string ledgerName = txtLedgerName.Text.GetDBFormatString();
            string ledgerCategory = cmbLedgerCategory.Text;
            string gstregcategory = cmbGSTCatagory.Text.GetDBFormatString();
            string gstin = "NULL";

            if (!txtGSTIN.Text.ISNullOrWhiteSpace())
            {
                gstin = "'" + txtGSTIN.Text.GetDBFormatString() + "'";
            }
            string companyName = txtCompanyName.Text.GetDBFormatString();
            string address = txtAddress.Text.GetDBFormatString();
            string town = txtTown.Text.GetDBFormatString();
            string stateID = (!cmbState.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbState.SelectedItem).Value.ToString() : "");
            string pin = txtPIN.Text;
            string dist = (!cmbDist.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDist.SelectedItem).Value.ToString() : "");
            string mobile = txtMobile.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text.GetDBFormatString();

            string panNo = txtPanNO.Text.GetDBFormatString();
            string billingTermID = (!cmbBillingTerms.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbBillingTerms.SelectedItem).Value.ToString() : "null");

            string bankId = (!cmbBankName.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbBankName.SelectedItem).Key.ToString() : "null");

            string branch = txtBranchName.Text.GetDBFormatString();
            string accountNo = txtAccountNo.Text.GetDBFormatString();
            string ifsc = txtIFSC.Text.GetDBFormatString();
            string qry = "";
            string queryandforcashcustomer = string.Empty;
            string templateName = txttemplatename.Text.GetDBFormatString();
            string aadharNo = txtAadherNo.Text.ISNullOrWhiteSpace() ? "NULL" : "'" + txtAadherNo.Text.GetDBFormatString() + "'";
            #endregion
            #region Insert Ledger Main
            ledgerId = Guid.NewGuid().ToString();
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                qry = "Insert into LadgerMain(LadgerID,LadgerName,Category, " +
                      "GSTRegistrationType,GSTIN,TemplateName,SubAccount, ParentAccount, Type) " +
                      "values('" + ledgerId + "','" + ledgerName + "','" + ledgerCategory
                      + "','" + gstregcategory + "'," + gstin + ",'" + templateName
                      + "','" + mSubAccount + "','" + mParentAccount + "'," + mAccountType + ")";
                mlstQry.Add(qry);
            }
            else
            {
                qry = "Update LadgerMain set LadgerName='" + ledgerName + "',Category='" + ledgerCategory 
                      + "',GSTRegistrationType='" + gstregcategory + "',TemplateName='" + templateName 
                      + "', GSTIN=" + gstin + " where   LadgerID='" + mLedgerIdForEdit + "'";
                mlstQry.Add(qry);
            }
            #endregion

            #region Insert into Ledgers
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                qry = "Insert into Ledgers (LedgerID, LedgerName, Company, Address, City_Town, State,Dist, PinCode, Email," +
                      "Mobile, Phone, BillingTerms, PAN, BankID,BranchName,IFSC,AccountNo,AadharNo) " +
                      "Values ('" + ledgerId + "','" + ledgerName + "','" + companyName + "','" + address + "','" + town +
                      "','" + stateID + "','" + dist + "','" + pin + "','" + email + "','" + mobile + "','" + phone +
                      "','" + billingTermID + "','" + panNo + "'," + bankId + ",'" + branch + "','" + ifsc + "','" + accountNo + "'," + aadharNo + ")";
                mlstQry.Add(qry);
            }
            else
            {
                qry = "Update Ledgers set LedgerName='" + ledgerName + "', Company='" + companyName + "', Address='" +
                      address + "', City_Town='" + town + "', State='" + stateID + "',Dist='" + dist + "', PinCode='" +
                      pin + "', Email='" + email + "',Mobile='" + mobile + "', Phone='" + phone + "', BillingTerms='" +
                      billingTermID + "', PAN='" + panNo + "',BankID=" + bankId + ",BranchName='" + branch
                      + "',IFSC='" + ifsc + "',AccountNo='" + accountNo + "',AadharNo=" + aadharNo + " where  LedgerID='" + mLedgerIdForEdit + "'" + queryandforcashcustomer + "";
                mlstQry.Add(qry);
            }
            #region InsertINLedgerStatus
            InsertIntoLedgerStatus(ledgerId);
            #endregion

            #endregion

            #region Insert into Respect By Ledger
            if (cmbLedgerCategory.Text == "Supplier")
            {
                SaveSupplierDetails(ledgerId);
            }
            else if (cmbLedgerCategory.Text == "Customer")
            {
                SaveCustomerDetails(ledgerId);
            }
            else if (cmbLedgerCategory.Text == "Employee")
            {
                SaveEmployeeDetails(ledgerId);
            }
            #endregion
            #region Update estimate
            if (!mEstimateid.ISNullOrWhiteSpace())
            {
                qry = "Update Estimate set PartyName='" + ledgerName + "',StateName='" + cmbState.Text.GetDBFormatString()
                    + "',Address='" + address + "',ContactNo='" + mobile + "', TemplateName='" + templateName + "' where estimateid='" + mEstimateid + "'";
                mlstQry.Add(qry);
            }
            #endregion
            #region Execute Query
            if (SQLHelper.GetInstance().ExecuteTransection(mlstQry, out msg))
            {
                mIsSuccess = true;
                #region Refresh Tools
                if (cmbLedgerCategory.Text == "Customer")
                {
                    Customertools.GetCustomers();
                    CashCustomersTools.GetCashCustomers();
                    LedgerTools.GetAllLedgers();
                    LedgerTools.GetPartyLedgers();
                    LedgerTools.GetCustomerLedgers();
                }
                else if (cmbLedgerCategory.Text == "Supplier")
                {
                    Supplier.GetSuppliers();
                    LedgerTools.GetAllLedgers();
                    LedgerTools.GetPartyLedgers();
                    LedgerTools.GetSupplierLedgers();
                }
                else if (cmbLedgerCategory.Text == "Employee")
                {
                    EmployeeTools.GetEmployee();
                }
                else if (cmbLedgerCategory.Text == "Cash_Customer")
                {
                    CashCustomersTools.GetCashCustomers();
                }
                #endregion

                switch (mType)
                {
                    case _Type.show:
                        MessageBox.Show(cmbLedgerCategory.Text + " successfully added.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetData();
                        break;
                    case _Type.showDialog:
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Internully Error.\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        private void InsertIntoLedgerStatus(string ledgerid)
        {
            #region data
            string openingBalancestr = txtOpeningBalance.Text;
            double openningBalance = openingBalancestr.ISNullOrWhiteSpace() ? 0d : double.Parse(openingBalancestr);
            string croDr = cmbcrdr.Text;
            openningBalance = openningBalance == 0 ? 0d : croDr.ISNullOrWhiteSpace() ? 0d : croDr == "Dr." ? openningBalance : -(openningBalance);
            string openingDate = dtpDateIOpening.Text;
            #endregion
            string query = "";
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                query = "insert into LedgerStatus(LedgerID,FinYearID,OpeningBalance, " +
                        "CurrentBalance,date) values('" + ledgerid + "'," +
                        FinancialYearTools._YearID + "," + openningBalance + "," +
                        openningBalance + ",'" + openingDate + "')";
                mlstQry.Add(query);
            }
            else
            {
                double openingPrev = 0d, closingPrev = 0d;

                GetCurrentOpeningStatus(mLedgerIdForEdit, out openingPrev, out closingPrev);
                double addtoClosing = (openningBalance) - (openingPrev);
                double closingPrsnt = (closingPrev) + (addtoClosing);

                query = "Update LedgerStatus set OpeningBalance=" + openningBalance +
                        ", CurrentBalance=" + closingPrsnt + ",date='" + openingDate +
                        "' where FinYearID=" + FinancialYearTools._YearID +
                        " and LedgerID='" + mLedgerIdForEdit + "'";
                mlstQry.Add(query);
            }
        }
        private void GetCurrentOpeningStatus(string ledgerID, out double opening, out double closing)
        {
            opening = 0d; closing = 0d;
            string query = "Select * from LedgerStatus where LedgerID='" + ledgerID
                           + "' and FinYearID=" + FinancialYearTools._YearID + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                double.TryParse(dt.Rows[0]["OpeningBalance"].ToString(), out opening);
                double.TryParse(dt.Rows[0]["CurrentBalance"].ToString(), out closing);
            }
        }
        private void SaveSupplierDetails(string ledgerId)
        {
            string website = txtWebsite.Text.GetDBFormatString();
            string note = txtNote.Text.GetDBFormatString();
            string opening = txtOpeningBalance.Text;
            string query = "";
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into Suppliers (LedgerID, Website, Note) " +
                        "Values('" + ledgerId + "','" + website + "','" + note + "')";
                mlstQry.Add(query);
            }
            else
            {
                query = "Update Suppliers set  Website='" + website + "',Note='" + note + "'" +
                        " where LedgerID='" + mLedgerIdForEdit + "'";
                mlstQry.Add(query);
            }
        }
        private void SaveCustomerDetails(string ledgerId)
        {
            string address = txtAddress.Text.GetDBFormatString();
            string town = txtTown.Text.GetDBFormatString();
            string state = (!cmbState.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbState.SelectedItem).Value.ToString() : "");
            string pin = txtPIN.Text;
            string dist = (!cmbDist.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDist.SelectedItem).Value.ToString() : "");

            string nameBillingg = txtNameBilling.Text.GetDBFormatString();
            string addressBilling = txtAddressBilling.Text.GetDBFormatString();
            string townBilling = txtTownBilling.Text.GetDBFormatString();
            string stateBilling = (!cmbStateBilling.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Value.ToString() : "");
            string pinBilling = txtPINBilling.Text;
            string distBilling = (!cmbDistBilling.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDistBilling.SelectedItem).Value.ToString() : "");

            string nameShipping = txtNameShipping.Text.GetDBFormatString();
            string addressShipping = txtAddressShipping.Text.GetDBFormatString();
            string townShipping = txtTownShipping.Text.GetDBFormatString();
            string stateShipping = (!cmbStateShipping.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Value.ToString() : "");
            string pinShipping = txtPinShipping.Text;
            string distShipping = (!cmbDistShipping.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDistShipping.SelectedItem).Value.ToString() : "");

            string website = txtWebsite.Text.GetDBFormatString();
            string note = txtNote.Text.GetDBFormatString();
            //string opening = txtOpeningBalance.Text;
            //float openingBalance = (opening.ISNullOrWhiteSpace()) ? 0 : float.Parse(opening);
            //string date = DateTime.Now.Date.ToString();
            string mobileNoShipping = txtContactNoShipping.Text.GetDBFormatString();


            #region DataEntry
            string query = "";
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into Customers (LedgerID, BillingName,BillingAddress, BillingTown,BillingDist, BillingState, BillingPIN, ShippingName,ShippingAddress, " +
                        "ShippingTown,ShippingDist, ShippingState, ShippingPIN, ShippingContactNo,Website, Note) " +
                        "Values('" + ledgerId + "','" + nameBillingg + "','" + addressBilling + "','" + townBilling + "','" + distBilling + "','" + stateBilling + "','" + pinBilling
                        + "','" + nameShipping + "','" + addressShipping + "','" + townShipping + "','" + distShipping + "','" + stateShipping
                        + "','" + pinShipping + "','" + mobileNoShipping + "','" + website + "','" + note + "')";
                mlstQry.Add(query);
            }
            else
            {
                query = "Update Customers set BillingName='" + nameBillingg + "', BillingAddress='" + addressBilling + "', BillingTown='" + townBilling + "',BillingDist='" +
                        distBilling + "', BillingState='" + stateBilling + "', BillingPIN='" + pinBilling + "',ShippingName='" + nameShipping + "', ShippingAddress='" + addressShipping +
                        "',ShippingTown='" + townShipping + "',ShippingDist='" + distShipping + "', ShippingState='" +
                        stateShipping + "', ShippingPIN='" + pinShipping + "', ShippingContactNo='" + mobileNoShipping + "',Website='" + website + "',Note='" + note + "' where LedgerID='" + mLedgerIdForEdit + "'";
                mlstQry.Add(query);
            }
            #endregion

        }
        private bool IsBillingAddressFill()
        {
            if (txtNameBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Biller Name.", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                txtNameBilling.Focus();
                return false;
            }
            if (txtAddressBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Billing Address.", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                txtAddressBilling.Focus();
                return false;
            }
            if (txtTownBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Billing Town/City.", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                txtTownBilling.Focus();
                return false;
            }
            if (cmbStateBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select Billing State", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                cmbStateBilling.Focus();
                return false;
            }
            if (cmbDistBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select  BillingDistrict", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                cmbDistBilling.Focus();
                return false;
            }
            if (txtPINBilling.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Billing PIN.", " Billing Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 1;
                txtPINBilling.Focus();
                return false;
            }
            return true;
        }
        private bool IsShippingAddressFill()
        {
            if (txtNameShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Shipping Name.", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                txtNameShipping.Focus();
                return false;
            }
            if (txtAddressShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Shipping Address.", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                txtAddressShipping.Focus();
                return false;
            }
            if (txtContactNoShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Shipping Mobile No.", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                txtContactNoShipping.Focus();
                return false;
            }
            if (txtTownShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter  Shipping Town/City.", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                txtTownShipping.Focus();
                return false;
            }
            if (cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select Shipping State", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                cmbStateShipping.Focus();
                return false;
            }
            if (cmbDistShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select  Shipping District", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                cmbDistShipping.Focus();
                return false;
            }
            if (txtPinShipping.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Shipping PIN.", " Shipping Address", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 2;
                txtPinShipping.Focus();
                return false;
            }
            return true;
        }
        private void SaveEmployeeDetails(string ledgerId)
        {
            #region EmployeeData
            string empId = txtEmployeeID.Text.GetDBFormatString();
            string gender = cmbGender.Text.GetDBFormatString();
            string bloodGroup = cmbBloodGroup.Text.GetDBFormatString();
            string joinDate = "NULL";
            string dob = "NULL";

            DateTime joinDated;
            DateTime.TryParse(txtjoingdate.Text.GetDBFormatString(), out joinDated);
            DateTime dobd;
            DateTime.TryParse(txtDOB.Text.GetDBFormatString(), out dobd);
            joinDate = !txtjoingdate.Text.ISNullOrWhiteSpace() ? "'" + joinDated.ToString("dd-MMM-yyyy") + "'" : "NULL";
            dob = !txtDOB.Text.ISNullOrWhiteSpace() ? "'" + dobd.ToString("dd-MMM-yyyy") + "'" : "NULL";
            string mailWork = textBox1.Text.GetDBFormatString();
            string note = txtNote.Text.GetDBFormatString();
            string designationId = (!cmbDesignation.Text.ISNullOrWhiteSpace() ? "'" + ((KeyValuePair<string, string>)cmbDesignation.SelectedItem).Key.ToString() + "'" : "NULL");
            #endregion
            string qry = "";

            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                qry = "Insert into Employee(LadgerID,DesignationId,EmailIdWork,JoiningDate,EmpCode,Gender,DOB,BloodGroup,Remarks) " +
                        "values('" + ledgerId + "'," + designationId + ",'" + mailWork + "'," + joinDate + ",'" + empId + "','" + gender + "'," + dob
                        + ",'" + bloodGroup + "','" + note + "')";
                mlstQry.Add(qry);

            }
            else
            {
                qry = "update Employee set LadgerID='" + mLedgerIdForEdit + "',DesignationId=" + designationId + ",EmailIdWork='" + mailWork + "',JoiningDate=" + joinDate
                    + ",EmpCode='" + empId + "',Gender='" + gender + "',DOB=" + dob + ",BloodGroup ='" + bloodGroup + "',Remarks='" + note + "' where LadgerID='" + mLedgerIdForEdit + "' ";
                mlstQry.Add(qry);

            }

        }
        private bool IsNotLedgerDuplicate()
        {
            if (cmbLedgerCategory.Text != "Cash_Customer")
            {
                string qry = "";
                string templateladgerName = txttemplatename.Text.Trim().GetDBFormatString();
                qry = "SELECT TemplateName,Category FROM  LadgerMain where TemplateName='" + templateladgerName + "'";
                if (!mLedgerIdForEdit.ISNullOrWhiteSpace())
                {
                    qry = "SELECT TemplateName,Category FROM  LadgerMain where TemplateName='" + templateladgerName + "' and LadgerID<>'" + mLedgerIdForEdit + "'";

                }
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
                if (dt.IsValidDataTable())
                {
                    string category = dt.Rows[0]["Category"].ToString();
                    MessageBox.Show(cmbLedgerCategory.Text + " details already exist as \"" + category + "\".", "Duplicate " + cmbLedgerCategory.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtLedgerName.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        private bool IsMendatoryFilled()
        {
            #region COMMON MENDATORY
            if (txtLedgerName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter " + cmbLedgerCategory.Text + " name.", cmbLedgerCategory.Text + " Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtLedgerName.Focus();
                return false;
            }
            if (txttemplatename.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter " + cmbLedgerCategory.Text + " Template name.", cmbLedgerCategory.Text + " Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txttemplatename.Focus();
                return false;
            }

            if (txtMobile.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("enter Mobile number.", cmbLedgerCategory.Text + " Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMobile.Focus();
                return false;
            }
            if (cmbState.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select state name.", cmbLedgerCategory.Text + " Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbState.Focus();
                return false;
            }
            #endregion  
            if (cmbLedgerCategory.Text != "Employee")
            {
                if (cmbGSTCatagory.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select GST Category.", " Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbGSTCatagory.Focus();
                    return false;
                }
                else
                {
                    if (!FillGstinNumberOrNot())
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        private bool IsInValidGstInNumber()
        {
            if (!txtGSTIN.Text.ISNullOrWhiteSpace())
            {
                if (txtGSTIN.TextLength != 15)
                {
                    MessageBox.Show("Please Enter a Valid GSTIN", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGSTIN.Focus();
                    return false;
                }
            }
            if (!CheckValidGSTIN())
            {
                MessageBox.Show("Please Enter a Valid GSTIN", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGSTIN.Focus();
                return false;
            }
            return true;
        }
        private void ResetData()
        {
            mlstQry.Clear();
            mLedgerIdForEdit = "";
            txtLedgerName.Clear();
            txttemplatename.Clear();
            txtCompanyName.Clear();
            txtAddress.Clear();
            txtTown.Clear();
            txtPIN.Clear();
            txtPanNO.Clear();
            txtGSTIN.Clear();
            cmbGSTCatagory.SelectedIndex = -1;
            txtMobile.Clear();
            txtPhone.Clear();
            txtOpeningBalance.Clear();
            txtEmail.Clear();
            txtNote.Clear();
            txtWebsite.Clear();
            txtDOB.Clear();
            txtBranchName.Clear();
            txtAccountNo.Clear();
            txtIFSC.Clear();
            cmbBankName.SelectedIndex = -1;
            cmbDesignation.SelectedIndex = -1;
            txtEmployeeID.Clear();
            txtjoingdate.Clear();
            textBox1.Clear();
            cmbState.Text = ORG_Tools._State;
            cmbDist.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            cmbBloodGroup.SelectedIndex = -1;
            cmbBillingTerms.SelectedIndex = -1;

            txtNameShipping.Clear();
            txtAddressShipping.Clear();
            txtTownShipping.Clear();
            txtPinShipping.Clear();
            cmbDistShipping.SelectedIndex = -1;
            cmbStateShipping.SelectedIndex = -1;
            txtContactNoShipping.Clear();

            txtNameBilling.Clear();
            txtAddressBilling.Clear();
            txtTownBilling.Clear();
            txtPINBilling.Clear();
            cmbDistBilling.SelectedIndex = -1;
            cmbStateBilling.SelectedIndex = -1;

        }
        private void AddNewSuppliers_Shown(object sender, EventArgs e)
        {
            txtLedgerName.Focus();
        }
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Billing details & Shipping Details same as ledger details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Same as ledger
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbState.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
                FillGstInNo(stateID);
                cmbDist.AddDist(stateID);
                if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
                {
                    cmbStateBilling.Text = cmbState.Text;
                    cmbStateShipping.Text = cmbState.Text;
                }
            }

        }

        private void FillGstInNo(string stateID)
        {
            if (!txtGSTIN.Text.ISNullOrWhiteSpace())
            {
                string gstin = txtGSTIN.Text.Substring(2, txtGSTIN.Text.Length - 2);
                txtGSTIN.Text = stateID + gstin;
            }
        }

        private void txtLedgerName_TextChanged(object sender, EventArgs e)
        {
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
            {
                txtNameBilling.Text = txtLedgerName.Text;
                txtNameShipping.Text = txtLedgerName.Text;
            }
        }
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
            {
                txtAddressBilling.Text = txtAddress.Text;
                txtAddressShipping.Text = txtAddress.Text;
            }
        }
        private void txtTown_TextChanged(object sender, EventArgs e)
        {
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
            {
                txtTownBilling.Text = txtTown.Text;
                txtTownShipping.Text = txtTown.Text;
            }
        }
        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            txtPIN.ForeColor = Color.Red;

            if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
            {
                txtPINBilling.Text = txtPIN.Text;
                txtPinShipping.Text = txtPIN.Text;
            }
            if (!txtPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtPIN.Text.Length == 6)
                {
                    txtPIN.ForeColor = Color.Black;
                }

            }
        }
        private void cmbDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mLedgerIdForEdit.ISNullOrWhiteSpace())///Only new ledger entry
            {
                cmbDistBilling.Text = cmbDist.Text;
                cmbDistShipping.Text = cmbDist.Text;
            }
        }
        private void chkSameAsAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsAddress.Checked)
            {
                txtNameBilling.Text = txtLedgerName.Text;
                txtAddressBilling.Text = txtAddress.Text;
                txtTownBilling.Text = txtTown.Text;
                txtPINBilling.Text = txtPIN.Text;
                cmbStateBilling.Text = cmbState.Text;
                cmbDistBilling.Text = cmbDist.Text;
            }
            else
            {
                txtNameBilling.Text = "";
                txtAddressBilling.Text = "";
                txtTownBilling.Text = "";
                txtPINBilling.Text = "";
                cmbDistBilling.SelectedIndex = -1;
                cmbStateBilling.SelectedIndex = -1;


            }
        }
        private void cmbLedgerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLedgerCategory.Text == "Supplier")
            {
                pnlOthers.Show();
                pnlEmployee.Hide();
                cmbGSTCatagory.Text = "Regular";
                cmbGSTCatagory.Items.RemoveAt(3);
                cmbcrdr.Text = "CR.";
                mSubAccount = "Sundry Creditors";
                mParentAccount = "Current Liability";
                mAccountType = "NULL";
            }
            else if (cmbLedgerCategory.Text == "Customer")
            {
                pnlOthers.Show();
                pnlEmployee.Hide();
                cmbcrdr.Text = "DR.";
                mSubAccount = "Sundry Debtors";
                mParentAccount = "Current Assets";
                mAccountType = "NULL";
            }
            else if (cmbLedgerCategory.Text == "Employee")
            {
                pnlOthers.Hide();
                pnlEmployee.Show();
            }
            else if (cmbLedgerCategory.Text == "Cash_Customer")
            {
                pnlOthers.Hide();
                pnlEmployee.Hide();
                pnlBank.Hide();
                pnlOpeningBalance.Hide();
                cmbGSTCatagory.Text = "Consumer";
                this.Height = 390;
                if (!mLedgerIdForEdit.ISNullOrWhiteSpace())
                {
                    mtableid = mLedgerIdForEdit;
                    mLedgerIdForEdit = LedgerTools._CashLedgerId;
                }
            }
            lblCategoryName.Text = "*" + cmbLedgerCategory.Text + " Name :";
        }
        private void cmbStateShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Key.ToString();
                cmbDistShipping.AddDist(stateID);
            }
        }

        #endregion

        /// <summary>
        /// Employee Designation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDesig_Click(object sender, EventArgs e)
        {
            CreateDesignation frm = new CreateDesignation();
            frm.OnClose += Frm_OnClose;
            frm.ShowDialog();
        }
        private void Frm_OnClose(string obj)
        {
            cmbDesignation.AddDesignation();
            cmbDesignation.Text = obj.ToString();
        }

        /// <summary>
        /// Bank Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBank_Click(object sender, EventArgs e)
        {
            BankAdd frmbankadd = new BankAdd();
            frmbankadd.onclose += Frm_onclose;
            frmbankadd.ShowDialog();
        }
        private void Frm_onclose(string obj)
        {
            DefaultClass dfltcls = new DefaultClass();
            cmbBankName.AddBank();
            cmbBankName.Text = obj.ToString();
        }
        //
        /// <summary>
        /// Data validation on leave 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGSTCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGSTCatagory.Text == "Unregister" || cmbGSTCatagory.Text == "Consumer")
            {
                txtGSTIN.Clear();
                txtGSTIN.Enabled = false;
                lblGstin.Enabled = false;
            }
            else
            {
                txtGSTIN.Enabled = true;
                lblGstin.Enabled = true;
            }
        }
        private void txtGSTIN_Leave(object sender, EventArgs e)
        {
            if (!txtGSTIN.Text.ISNullOrWhiteSpace())
            {
                if (txtGSTIN.TextLength != 15)
                {
                    MessageBox.Show("Please enter a valid GSTIN No", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtGSTIN.Focus();
                }
            }
        }
        private void txtMobile_Leave(object sender, EventArgs e)
        {

        }
        private void chkbxSameAsaddressForShipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSameAsaddressForShipping.Checked)
            {
                txtNameShipping.Text = txtLedgerName.Text;
                txtContactNoShipping.Text = txtMobile.Text;
                txtAddressShipping.Text = txtAddress.Text;
                txtTownShipping.Text = txtTown.Text;
                txtPinShipping.Text = txtPIN.Text;
                cmbStateShipping.Text = cmbState.Text;
                cmbDistShipping.Text = cmbDist.Text;
            }
            else
            {

                txtNameShipping.Text = "";
                txtContactNoShipping.Text = "";
                txtAddressShipping.Text = "";
                txtTownShipping.Text = "";
                txtPinShipping.Text = "";
                cmbDistShipping.SelectedIndex = -1;
                cmbStateShipping.SelectedIndex = -1;
            }
        }
        private void cmbStateBilling_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbStateBilling.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Key.ToString();
                cmbDistBilling.AddDist(stateID);
            }
        }
        private void txtPIN_Leave(object sender, EventArgs e)
        {

            if (mladgerCategory == _LedgerCategory.Customer)
            {
                tabControl1.SelectedIndex = 1;
            }
        }
        private void txtPINBilling_Leave(object sender, EventArgs e)
        {

            if (mladgerCategory == _LedgerCategory.Customer)
            {
                tabControl1.SelectedIndex = 2;
            }
        }

        /// <summary>
        /// Billing Terms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBillingTerms_Click(object sender, EventArgs e)
        {
            BillingTermEntry frmBillingTermEntry = new BillingTermEntry();
            frmBillingTermEntry.OnClose += FrmBillingTermEntry_onClose;
            frmBillingTermEntry.ShowDialog();
        }
        private void FrmBillingTermEntry_onClose(string obj)
        {
            cmbBillingTerms.AddBillingTerms();
            cmbBillingTerms.Text = obj.ToString();
        }


        /// <summary>
        /// Add District
        /// Billing, Shipping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDistAddress_Click(object sender, EventArgs e)
        {
            string state = (!cmbState.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbState.SelectedItem).Value.ToString() : "");

            Dist_master frmDist_master = new Dist_master(state);
            frmDist_master.onclose += FrmDist_master_onclose;
            frmDist_master.ShowDialog();
        }
        private void FrmDist_master_onclose(string obj)
        {
            cmbDist.AddDist();
            cmbDist.Text = obj.ToString();
        }
        private void btnAddDistBillingAddress_Click(object sender, EventArgs e)
        {
            string state = (!cmbStateBilling.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Value.ToString() : "");

            Dist_master frmDist_master = new Dist_master(state);
            frmDist_master.onclose += FrmDist_master_onclose1;
            frmDist_master.ShowDialog();
        }
        private void FrmDist_master_onclose1(string obj)
        {
            cmbDistBilling.AddDist();
            cmbDistBilling.Text = obj.ToString();
        }
        private void btnAddDistShippingAddress_Click(object sender, EventArgs e)
        {
            string state = (!cmbStateShipping.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Value.ToString() : "");

            Dist_master frmDist_master = new Dist_master(state);
            frmDist_master.onclose += FrmDist_master_onclose2;
            frmDist_master.ShowDialog();
        }
        private void FrmDist_master_onclose2(string obj)
        {
            cmbDistShipping.AddDist();
            cmbDistShipping.Text = obj.ToString();
        }
        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            txtMobile.ForeColor = Color.Red;

            if (mLedgerIdForEdit.ISNullOrWhiteSpace())
            {
                txtContactNoShipping.Text = txtMobile.Text;
            }

            if (!txtMobile.Text.ISNullOrWhiteSpace())
            {
                if (txtMobile.Text.Length == 10 || txtMobile.Text.Length == 12)
                {
                    txtMobile.ForeColor = Color.Black;
                }
            }
        }
        private void txtPINBilling_TextChanged(object sender, EventArgs e)
        {
            txtPINBilling.ForeColor = Color.Red;

            if (!txtPINBilling.Text.ISNullOrWhiteSpace())
            {
                if (txtPINBilling.Text.Length == 6)
                {
                    txtPINBilling.ForeColor = Color.Black;
                }

            }
        }
        private void txtPinShipping_TextChanged(object sender, EventArgs e)
        {
            txtPinShipping.ForeColor = Color.Red;

            if (!txtPinShipping.Text.ISNullOrWhiteSpace())
            {
                if (txtPinShipping.Text.Length == 6)
                {
                    txtPinShipping.ForeColor = Color.Black;
                }

            }
        }
        private void txtContactNoShipping_TextChanged(object sender, EventArgs e)
        {
            txtContactNoShipping.ForeColor = Color.Red;

            if (!txtContactNoShipping.Text.ISNullOrWhiteSpace())
            {
                if (txtContactNoShipping.Text.Length == 10)
                {
                    txtContactNoShipping.ForeColor = Color.Black;
                }

            }
        }
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            txtPhone.ForeColor = Color.Red;

            if (!txtPhone.Text.ISNullOrWhiteSpace())
            {
                if (txtPhone.Text.Length == 10 || txtPhone.Text.Length == 12)
                {
                    txtPhone.ForeColor = Color.Black;
                }

            }
        }
        private void txtPanNO_TextChanged(object sender, EventArgs e)
        {

            txtPanNO.ForeColor = Color.Red;

            if (!txtPanNO.Text.ISNullOrWhiteSpace())
            {
                if (txtPanNO.Text.Length == 10)
                {
                    txtPanNO.ForeColor = Color.Black;
                }

            }
        }
        private bool CheckValidPan(string txttxt)
        {
            bool temp = false;
            string str = txttxt;
            if (str.Length <= 5)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp = !String.IsNullOrEmpty(str) && Char.IsDigit(str[i]);
                }
            }
            else if (str.Length > 5 && str.Length < 10)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    temp = !String.IsNullOrEmpty(str) && Char.IsLetter(str[i]);
                }
            }
            else if (str.Length == 10)
            {
                temp = !String.IsNullOrEmpty(str) && Char.IsDigit(str[9]);
            }
            return temp;
        }
        private void txtGSTIN_TextChanged(object sender, EventArgs e)
        {
            if (!cmbState.Text.ISNullOrWhiteSpace())
            {
                txtGSTIN.ForeColor = Color.Red;

                if (!txtGSTIN.Text.ISNullOrWhiteSpace())
                {
                    if (CheckValidGSTIN())
                    {
                        txtGSTIN.ForeColor = Color.Black;
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select state");
            }
        }
        private bool CheckValidGSTIN()
        {
            string str = txtGSTIN.Text;
            string statecode = ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
            try
            {
                if (str.Substring(0, 2) != statecode)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (str.Length < 13)
            {
                if (CheckValidPan(str.Substring(2, str.Length - 2)))
                {
                    return false;
                }
            }
            else
            {

                if (CheckValidPan(str.Substring(2, 10)))
                {
                    return false;
                }
                if (str.Length != 15)
                {
                    return false;
                }
            }

            return true;
        }
        private void txtMobile_Leave_1(object sender, EventArgs e)
        {
            txttemplatename.Clear();
            if (!txtMobile.Text.ISNullOrWhiteSpace() && !txtLedgerName.Text.ISNullOrWhiteSpace())
            {
                txttemplatename.Text = txtLedgerName.Text + " (" + txtMobile.Text + ")";
            }
        }
        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            txttemplatename.Clear();
            if (!txtMobile.Text.ISNullOrWhiteSpace() && !txtLedgerName.Text.ISNullOrWhiteSpace())
            {
                txttemplatename.Text = txtLedgerName.Text + " (" + txtMobile.Text + ")";
            }
            else
            {
                txttemplatename.Text = txtLedgerName.Text;

            }
        }

        private void txtDOB_Leave(object sender, EventArgs e)
        {
            if (!txtDOB.Text.ISNullOrWhiteSpace())
            {
                try
                {
                    txtDOB.Text = DateTime.Parse(txtDOB.Text).ToString("dd-MMM-yyyy");
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter valid date", "Date of birth", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDOB.Clear();
                }
            }
        }

        private void txtjoingdate_Leave(object sender, EventArgs e)
        {
            if (!txtjoingdate.Text.ISNullOrWhiteSpace())
            {
                try
                {
                    txtjoingdate.Text = DateTime.Parse(txtjoingdate.Text).ToString("dd-MMM-yyyy");
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter valid date", "Date of join", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtjoingdate.Clear();
                }
            }
        }

        private void txtGSTIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSymbol(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtAadherNo_TextChanged(object sender, EventArgs e)
        {
            txtAadherNo.ForeColor = Color.Red;

            if (!txtAadherNo.Text.ISNullOrWhiteSpace())
            {
                if (txtAadherNo.Text.Length == 14)
                {
                    txtAadherNo.ForeColor = Color.Black;
                }

            }

        }

        private void txtAadherNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                if (e.KeyChar != 8)
                {
                    if (txtAadherNo.Text.Length == 4 || txtAadherNo.Text.Length == 9)
                    {
                        txtAadherNo.Text = txtAadherNo.Text + "-";
                        txtAadherNo.SelectionStart = txtAadherNo.Text.Length;
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPanNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSymbol(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                if (!CheckValidPan(txtPanNO.Text + e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.ForeColor = Color.Red;
            if (IsValidEmail(txtEmail.Text))
            {
                txtEmail.ForeColor = Color.Black;
            }

        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
