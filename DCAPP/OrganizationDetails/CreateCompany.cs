using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace DAPRO
{
    public partial class CreateCompany : Form
    {
        public event Action onClose;
        string msg = "";
        List<string> mlstQry = new List<string>();
        private bool mIsUpdate = false;
        private bool mIsFistTime = false;
        public CreateCompany(bool isFirstTime)
        {
            InitializeComponent();
            mIsFistTime = isFirstTime;
            cmbState.AddState();
            cmbRegionalState.AddState();
            cmbStateShipping.AddState();
            cmbStateBilling.AddState();
            pnlcomposition.Hide();
            if (mIsFistTime)
            {
                //this.Opacity = 50;
                btnSave.Text = "&Next";
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            }
            else
            {
                btnSave.Text = "&Save";
                pnlMain.Anchor = AnchorStyles.Left;
                ShowData();
            }
        }
        public void ShowData()
        {
            string query = "Select OrganizationDetails.*,OrganizationAddress.*,State.StateName from OrganizationDetails" +
                            " inner join state on OrganizationDetails.StateID = State.ID" +
                            " inner join OrganizationAddress on OrganizationDetails.OrganizationName = OrganizationAddress.OrganizationName";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                mIsUpdate = true;
                txtOrgName.Text = dt.Rows[0]["OrganizationName"].ToString();

                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtCity.Text = dt.Rows[0]["CityTown"].ToString();
                string StateId = dt.Rows[0]["StateId"].ToString();
                cmbState.Text = GetSateName(StateId);
                cmbDist.Text = dt.Rows[0]["Dist"].ToString();
                txtPIN.Text = dt.Rows[0]["PIN"].ToString();

                txtRegionalAdDress.Text = dt.Rows[0]["AddressRegional"].ToString();
                txtRegionalCity.Text = dt.Rows[0]["CityTownRegional"].ToString();
                string stateRegionalId = dt.Rows[0]["SateRegionalID"].ToString();
                cmbRegionalState.Text = GetSateName(stateRegionalId);
                cmbRegionalDist.Text = dt.Rows[0]["DistRegional"].ToString();
                txtRegionalPIN.Text = dt.Rows[0]["PinRegional"].ToString();

                txtCIN.Text = dt.Rows[0]["CorporateNo"].ToString();
                txtPAN.Text = dt.Rows[0]["PAN"].ToString();
                txtTAN.Text = dt.Rows[0]["TIN"].ToString();

                txtGST.Text = dt.Rows[0]["GSTin"].ToString();
                string gstCategory = dt.Rows[0]["GSTtype"].ToString();
                if (gstCategory.ISNullOrWhiteSpace())
                {
                    pnlGstInformation.Enabled = true;
                }
                else
                {
                    pnlGstInformation.Enabled = false;
                }
                cmbGSTCatagory.Text = dt.Rows[0]["GSTtype"].ToString();
                cmbCompositionType.Text = dt.Rows[0]["CompositionType"].ToString();
                lblPercentage.Text = dt.Rows[0]["CompositionPercentage"].ToString();

                txtContctNo.Text = dt.Rows[0]["ContactNo1"].ToString();
                txtAlternativNo.Text = dt.Rows[0]["AlternativeNo"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtWebSite.Text = dt.Rows[0]["website"].ToString();
                cmbBusinessCategory.Text = dt.Rows[0]["BusinessCatagory"].ToString();


                txtShippingNAme.Text = dt.Rows[0]["ShippingName"].ToString();
                txtContactShiping.Text = dt.Rows[0]["ShippingContactNo"].ToString();
                txtAddressShipping.Text = dt.Rows[0]["ShippingAddress"].ToString();
                txtCityTownShipping.Text = dt.Rows[0]["ShippingTown"].ToString();
                cmbStateShipping.Text = dt.Rows[0]["ShippingState"].ToString();
                cmbDistShipping.Text = dt.Rows[0]["ShippingDist"].ToString();
                txtPinShipping.Text = dt.Rows[0]["ShippingPIN"].ToString();

                txtNameBilling.Text = dt.Rows[0]["BillingName"].ToString();
                txtAddressBilling.Text = dt.Rows[0]["BillingAddress"].ToString();
                txtCityTownBilling.Text = dt.Rows[0]["BillingTown"].ToString();
                cmbStateBilling.Text = dt.Rows[0]["BillingState"].ToString();
                cmbDistBilling.Text = dt.Rows[0]["BillingDist"].ToString();
                txtPinBilling.Text = dt.Rows[0]["BillingPIN"].ToString();
                txtcontactOfBilling.Text = dt.Rows[0]["ContactNo1"].ToString();

                ShowImage();
            }
        }
        private void ShowImage()
        {
            string query = "Select LOGO from OrganizationDetails";
            Byte[] imageData = SQLHelper.GetInstance().ExcuteQueryAndGetImageData(query, out msg);
            if (imageData != null)
            {
                MemoryStream msI = new MemoryStream(imageData, 0, imageData.Length);
                Bitmap btmap = new Bitmap(msI);
                Save(btmap, 128, 128, 1, pictureBoxLogo);
            }
            else
            {
                pictureBoxLogo.BackgroundImage = Properties.Resources.default_Logo;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                DataSave();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg|*.jpg|PNG Files(*.png|*.png|All Files(*.*|*.*)";
            dlg.Title = "Select Logo";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Bitmap btmap = (Bitmap)Image.FromFile(dlg.FileName);
                Save(btmap, 128, 128, 1, pictureBoxLogo);
            }
        }
        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, PictureBox pb)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            // newImage.Save(filePath, imageCodecInfo, encoderParameters);
            pb.BackgroundImage = newImage;
        }
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        private void btnRemovLogo_Click(object sender, EventArgs e)
        {
            pictureBoxLogo.BackgroundImage = Properties.Resources.default_Logo;
        }
        private void DataSave()
        {
            string orgName = txtOrgName.Text.GetDBFormatString();

            string address = txtAddress.Text.GetDBFormatString();
            string city = txtCity.Text.GetDBFormatString();
            string district = ((KeyValuePair<string, string>)cmbDist.SelectedItem).Value.ToString();
            string stateID = ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
            string pin = txtPIN.Text.GetDBFormatString();

            string tin = txtTAN.Text.GetDBFormatString();
            string addressREGional = txtRegionalAdDress.Text.GetDBFormatString();
            string cityREGional = txtRegionalCity.Text.GetDBFormatString();
            string districtREGional = (!cmbRegionalDist.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbRegionalDist.SelectedItem).Value.ToString() : "");
            string stateIDREGional = (!cmbRegionalState.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbRegionalState.SelectedItem).Key.ToString() : "");
            string pinREGional = txtRegionalPIN.Text.GetDBFormatString();

            string PAN = txtPAN.Text.GetDBFormatString();
            string gstCategory = cmbGSTCatagory.Text.GetDBFormatString();
            string gstNo = txtGST.Text.GetDBFormatString();
            string contctNo = txtContctNo.Text.GetDBFormatString();
            string alterContctNo = txtAlternativNo.Text.GetDBFormatString();
            string email = txtEmail.Text.GetDBFormatString();
            string website = txtWebSite.Text.GetDBFormatString();
            string cin = txtCIN.Text.GetDBFormatString();
            string businessCategory = cmbBusinessCategory.Text;

            string nameShipping = txtShippingNAme.Text.GetDBFormatString();
            string contctShipping = txtContactShiping.Text.GetDBFormatString();
            string addressShipping = txtAddressShipping.Text.GetDBFormatString();
            string cityShipping = txtCityTownShipping.Text.GetDBFormatString();
            string districtShipping = (!cmbDistShipping.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDistShipping.SelectedItem).Value.ToString() : "");
            string stateIDShipping = (!cmbStateShipping.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Value.ToString() : "");
            string pinShipping = txtPinShipping.Text.GetDBFormatString();

            string nameBilling = txtNameBilling.Text.GetDBFormatString();
            string addressBilling = txtAddressBilling.Text.GetDBFormatString();
            string cityBilling = txtCityTownBilling.Text.GetDBFormatString();
            string districtBilling = (!cmbDistBilling.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbDistBilling.SelectedItem).Value.ToString() : "");
            string stateIDBilling = (!cmbStateBilling.Text.ISNullOrWhiteSpace() ? ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Value.ToString() : "");
            string pinBilling = txtPinBilling.Text.GetDBFormatString();
            string compositiontype = "NULL";
            string percentage = "NULL";
            string delecration = "NULL";
            if (gstCategory == "Composition")
            {
                compositiontype = "'" + cmbCompositionType.Text.GetDBFormatString() + "'";
                percentage = "" + lblPercentage.Text.GetDBFormatString() + "";
                delecration = "'" + txtCompositeDeclaration.Text.GetDBFormatString() + "'";
            }
            string query = "";
            if (mIsUpdate)
            {
                query = "Update OrganizationDetails set OrganizationName='" + orgName + "',Address='" + address
                        + "',CityTown='" + city + "',Dist='" + district + "',stateID=" + stateID + ",PIN='" + pin
                        + "',PAN='" + PAN + "',CorporateNo='" + cin + "',GSTtype='" + gstCategory + "',GSTin='" + gstNo
                        + "',ContactNo1='" + contctNo + "',AlternativeNo='" + alterContctNo + "',Email='" + email
                        + "',website='" + website + "',TIN='" + tin + "',AddressRegional='" + addressREGional +
                        "',CityTownRegional='" + cityREGional + "',DistRegional='" + districtREGional
                        + "',SateRegionalID='" + stateIDREGional + "',PinRegional='" + pinREGional + "',BusinessCatagory='" + businessCategory + "',CompositionType=" + compositiontype + ",CompositionPercentage=" + percentage + ",CompositionDeclaration=" + delecration + "";
                mlstQry.Add(query);

                query = "update OrganizationAddress set OrganizationName='" + orgName + "',BillingName='" + nameBilling +
                    "',BillingAddress='" + addressBilling + "',BillingTown='" + cityBilling + "',BillingDist='" + districtBilling + "',BillingState='" + stateIDBilling +
                    "',BillingPIN='" + pinBilling + "',ShippingName ='" + nameShipping + "',ShippingAddress='" + addressShipping + "',ShippingTown='" + cityShipping + "',ShippingDist='" + districtShipping + "',ShippingState='" + stateIDShipping + "',ShippingPIN='" + pinShipping + "',ShippingContactNo= '" + contctShipping + "'";
                mlstQry.Add(query);
            }
            else
            {
                query = "Insert into OrganizationDetails(OrganizationName, Address, CityTown, Dist, StateID, " +
                        "PIN, PAN, CorporateNo, GSTtype,GSTin, ContactNo1, AlternativeNo, Email, website,TIN,AddressRegional, " +
                        "CityTownRegional,DistRegional,SateRegionalID,PinRegional,BusinessCatagory,CompositionType,CompositionPercentage,CompositionDeclaration) " +
                        "values('" + orgName + "','" + address + "','" + city + "','" + district + "'," + stateID
                        + ",'" + pin + "','" + PAN + "','" + cin + "','" + gstCategory + "','" + gstNo + "','" + contctNo
                        + "','" + alterContctNo + "','" + email + "','" + website + "','" + tin + "','" + addressREGional +
                        "','" + cityREGional + "','" + districtREGional + "'," + stateIDREGional + ",'" + pinREGional + "','" + businessCategory + "'," + compositiontype + "," + percentage + "," + delecration + ")";

                mlstQry.Add(query);
                query = "insert into OrganizationAddress(OrganizationName,BillingName,BillingAddress,BillingTown,BillingDist,BillingState,BillingPIN,ShippingName,ShippingAddress,ShippingTown,ShippingDist,ShippingState,ShippingPIN,ShippingContactNo) " +
                             "values('" + orgName + "','" + nameBilling + "','" + addressBilling + "','" + cityBilling + "','" + districtBilling + "','" + stateIDBilling + "','" + pinBilling
                          + "','" + nameShipping + "','" + addressShipping + "','" + cityShipping + "','" + districtShipping + "','" + stateIDShipping + "','" + pinShipping + "','" + contctShipping + "')";
                mlstQry.Add(query);
            }
            if (SQLHelper.GetInstance().ExecuteTransection(mlstQry, out msg))
            {
                UpdateLogo();
                ORG_Tools.InitDetails();
                if (mIsFistTime)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Save data.", "Organization Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void UpdateLogo()
        {
            string qu = "Update OrganizationDetails set LOGO =@image ";
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBoxLogo.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bt = ms.GetBuffer();
                bool bl = SQLHelper.GetInstance().ExcuteQuery(qu, "@image", bt, out msg);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private bool IsValidEntry()
        {
            if (txtOrgName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter Organisation name", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrgName.Focus();
                return false;
            }
            if (txtCity.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter City/Town ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCity.Focus();
                return false;
            }
            if (cmbState.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select state ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbState.Focus();
                return false;
            }
            if (cmbDist.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select district ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDist.Focus();
                return false;
            }
            if (!txtPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtPIN.Text.Length < 6)
                {
                    MessageBox.Show("Please enter a valid PIN number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPIN.Focus();
                    return false;
                }
            }
            if (!txtContctNo.Text.ISNullOrWhiteSpace())
            {
                if (txtContctNo.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid phone number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContctNo.Focus();
                    return false;
                }
            }
            if (!txtAlternativNo.Text.ISNullOrWhiteSpace())
            {
                if (txtAlternativNo.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid alternate phone number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlternativNo.Focus();
                    return false;
                }
            }
            if (!txtPinBilling.Text.ISNullOrWhiteSpace())
            {
                if (txtPinBilling.Text.Length < 6)
                {
                    MessageBox.Show("Please enter a valid billing PIN number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPinBilling.Focus();
                    return false;
                }
            }
            if (!txtPinShipping.Text.ISNullOrWhiteSpace())
            {
                if (txtPinShipping.Text.Length < 6)
                {
                    MessageBox.Show("Please enter a valid shipping PIN number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPinShipping.Focus();
                    return false;
                }
            }
            if (!txtRegionalPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtRegionalPIN.Text.Length < 6)
                {
                    MessageBox.Show("Please enter a valid regional PIN number  ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRegionalPIN.Focus();
                    return false;
                }
            }

            if (!txtcontactOfBilling.Text.ISNullOrWhiteSpace())
            {
                if (txtcontactOfBilling.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid billing contact number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcontactOfBilling.Focus();
                    return false;
                }
            }
            if (!txtContactShiping.Text.ISNullOrWhiteSpace())
            {
                if (txtContactShiping.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid shipping contact number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContactShiping.Focus();
                    return false;
                }
            }
            if (!txtPAN.Text.ISNullOrWhiteSpace())
            {
                if (txtPAN.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid PAN number ", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPAN.Focus();
                    return false;
                }
            }
            if (cmbBusinessCategory.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select business category.", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBusinessCategory.Focus();
                return false;
            }
            if (cmbGSTCatagory.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select GST registration category", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGSTCatagory.Focus();
                return false;
            }
            if (txtGST.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter a valid GSTIN.", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGST.Focus();
                return false;
            }
            if (cmbGSTCatagory.Text == "Composition")
            {
                if (cmbCompositionType.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Please Enter composition type .", "ORG Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCompositionType.Focus();
                    return false;
                }
            }
            return true;
        }
        private void txtAlternativNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter Number only", "Number Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbState.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
                btnAddDist.Enabled = true;
                cmbDist.AddDist(stateID);
            }
            else
            {
                btnAddDist.Enabled = false;
            }
            cmbStateBilling.Text = cmbState.Text;
            cmbStateShipping.Text = cmbState.Text;
            cmbRegionalState.Text = cmbState.Text;
        }
        private void btnAddDist_Click(object sender, EventArgs e)
        {
            if (!cmbState.Text.ISNullOrWhiteSpace())
            {
                string state = ((KeyValuePair<string, string>)cmbState.SelectedItem).Value.ToString();
                Dist_master frmDist = new Dist_master(state);
                frmDist.onclose += FrmDist_onclose;
                frmDist.ShowDialog();
            }
        }
        private void FrmDist_onclose(string obj)
        {
            cmbDist.AddDist();
            cmbDist.Text = obj;
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
        private void chkbxREgionalOfficeAddressDo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxREgionalOfficeAddressDo.Checked)
            {
                txtRegionalAdDress.Text = txtAddress.Text;
                txtRegionalCity.Text = txtCity.Text;
                cmbRegionalState.Text = cmbState.Text;
                cmbRegionalDist.Text = cmbDist.Text;
                txtRegionalPIN.Text = txtPIN.Text;
            }
            else
            {
                txtRegionalAdDress.Clear();
                txtRegionalCity.Clear();
                cmbRegionalState.SelectedIndex = -1;
                cmbRegionalDist.SelectedIndex = -1;
                txtRegionalPIN.Clear();
            }
        }
        private void cmbGSTCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCompositeDeclaration.Clear();
            if (cmbGSTCatagory.Text == "Unregistered")
            {
                pnlcomposition.Hide();
                txtGST.Enabled = false;
                txtGST.Clear();
                label9.Enabled = false;
            }
            else if (cmbGSTCatagory.Text == "Composition")
            {
                pnlcomposition.Show();
                txtGST.Enabled = true;
                label9.Enabled = true;
                txtCompositeDeclaration.Text = "Declaration : \"Composition taxable person, not elogible to collect tax on supplies\"";
            }
            else
            {
                pnlcomposition.Hide();
                txtGST.Enabled = true;
                label9.Enabled = true;
            }
        }
        private void cmbRegionalState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbRegionalState.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbRegionalState.SelectedItem).Key.ToString();
                btnAddDistRegional.Enabled = true;
                cmbRegionalDist.AddDist(stateID);
            }
            else
            {
                btnAddDistRegional.Enabled = false;
            }
        }
        private void btnAddDistRegional_Click(object sender, EventArgs e)
        {
            if (!cmbRegionalState.Text.ISNullOrWhiteSpace())
            {
                string state = ((KeyValuePair<string, string>)cmbRegionalState.SelectedItem).Value.ToString();
                Dist_master frmDist = new Dist_master(state);
                frmDist.onclose += FrmDist_onclose1;
                frmDist.ShowDialog();
            }
        }
        private void FrmDist_onclose1(string obj)
        {
            cmbRegionalDist.AddDist();
            cmbRegionalDist.Text = obj;
        }
        private void chkbxSameAddressForShipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSameAddressForShipping.Checked)
            {
                txtShippingNAme.Text = txtOrgName.Text;
                txtContactShiping.Text = txtContctNo.Text;
                txtAddressShipping.Text = txtAddress.Text;
                txtCityTownShipping.Text = txtCity.Text;
                cmbStateShipping.Text = cmbState.Text;
                cmbDistShipping.Text = cmbDist.Text;
                txtPinShipping.Text = txtPIN.Text;
            }
            else
            {
                txtShippingNAme.Clear();
                txtContactShiping.Clear();
                txtAddressShipping.Clear();
                txtCityTownShipping.Clear();
                cmbStateShipping.SelectedIndex = -1;
                cmbDistShipping.SelectedIndex = -1;
                txtPinShipping.Clear();
            }
        }
        private void chkbxSameAddressForBilling_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSameAddressForBilling.Checked)
            {
                txtNameBilling.Text = txtOrgName.Text;
                txtAddressBilling.Text = txtAddress.Text;
                txtCityTownBilling.Text = txtCity.Text;
                cmbStateBilling.Text = cmbState.Text;
                cmbDistBilling.Text = cmbDist.Text;
                txtPinBilling.Text = txtPIN.Text;
                txtcontactOfBilling.Text = txtContctNo.Text;
            }
            else
            {
                txtNameBilling.Clear();
                txtAddressBilling.Clear();
                txtCityTownBilling.Clear();
                cmbStateBilling.SelectedIndex = -1;
                cmbDistBilling.SelectedIndex = -1;
                txtPinBilling.Clear();
                txtcontactOfBilling.Clear();
            }
        }
        private void btnAddDistShipping_Click(object sender, EventArgs e)
        {
            if (!cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                string state = ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Value.ToString();
                Dist_master frmDist = new Dist_master(state);
                frmDist.onclose += FrmDist_onclose2;
                frmDist.ShowDialog();
            }
        }
        private void FrmDist_onclose2(string obj)
        {
            cmbDistShipping.AddDist();
            cmbDistShipping.Text = obj;
        }
        private void btnAddDistBilling_Click(object sender, EventArgs e)
        {
            if (!cmbStateBilling.Text.ISNullOrWhiteSpace())
            {
                string state = ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Value.ToString();
                Dist_master frmDist = new Dist_master(state);
                frmDist.onclose += FrmDist_onclose3;
                frmDist.ShowDialog();
            }
        }
        private void FrmDist_onclose3(string obj)
        {
            cmbDistBilling.AddDist();
            cmbDistBilling.Text = obj;
        }
        private void cmbStateShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbStateShipping.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbStateShipping.SelectedItem).Key.ToString();
                btnAddDistShipping.Enabled = true;
                cmbDistShipping.AddDist(stateID);
            }
            else
            {
                btnAddDistShipping.Enabled = false;
            }
        }
        private void cmbStateBilling_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbStateBilling.Text.ISNullOrWhiteSpace())
            {
                string stateID = ((KeyValuePair<string, string>)cmbStateBilling.SelectedItem).Key.ToString();
                btnAddDistBilling.Enabled = true;
                cmbDistBilling.AddDist(stateID);
            }
            else
            {
                btnAddDistBilling.Enabled = false;
            }
        }
        private void CreateCompany_Load(object sender, EventArgs e)
        {

        }
        private void cmbCompositionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompositionType.Text == "Treaders")
            {
                lblPercentage.Text = 1.ToString();
            }
            else if (cmbCompositionType.Text == "Manufactured")
            {
                lblPercentage.Text = 2.ToString();
            }
            else
            {
                lblPercentage.Text = 5.ToString();
            }
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label17_Click(object sender, EventArgs e)
        {

        }
        private void label34_Click(object sender, EventArgs e)
        {

        }
        private void CreateCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onClose != null)
            {
                onClose();
            }
        }
        private void txtGST_TextChanged(object sender, EventArgs e)
        {
            txtGST.ForeColor = Color.Red;

            if (!txtGST.Text.ISNullOrWhiteSpace())
            {
                if (txtGST.Text.Length == 15)
                {
                    txtGST.ForeColor = Color.Black;
                }
            }
        }
        private void txtPAN_TextChanged(object sender, EventArgs e)
        {
            txtPAN.ForeColor = Color.Red;

            if (!txtPAN.Text.ISNullOrWhiteSpace())
            {
                if (txtPAN.Text.Length == 10)
                {
                    txtPAN.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Same as address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            txtAddressBilling.Text = txtAddress.Text;
            txtAddressShipping.Text = txtAddress.Text;
            txtRegionalAdDress.Text = txtAddress.Text;
        }
        private void txtOrgName_TextChanged(object sender, EventArgs e)
        {
            txtNameBilling.Text = txtOrgName.Text;
            txtShippingNAme.Text = txtOrgName.Text;
        }
        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            txtCityTownBilling.Text = txtCity.Text;
            txtCityTownShipping.Text = txtCity.Text;
            txtRegionalCity.Text = txtCity.Text;
        }
        private void cmbDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDistBilling.Text = cmbDist.Text;
            cmbDistShipping.Text = cmbDist.Text;
            cmbRegionalDist.Text = cmbDist.Text;
        }
        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            txtPinBilling.Text = txtPIN.Text;
            txtPinShipping.Text = txtPIN.Text;
            txtPIN.ForeColor = Color.Red;

            if (!txtPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtPIN.Text.Length == 6)
                {
                    txtPIN.ForeColor = Color.Black;
                }
            }

        }

        private void txtcontactOfBilling_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter Number only", "Number Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void txtGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

        private void txtRegionalPIN_TextChanged(object sender, EventArgs e)
        {
            txtRegionalPIN.ForeColor = Color.Red;

            if (!txtRegionalPIN.Text.ISNullOrWhiteSpace())
            {
                if (txtRegionalPIN.Text.Length == 6)
                {
                    txtRegionalPIN.ForeColor = Color.Black;
                }
            }
        }

        private void txtPinBilling_TextChanged(object sender, EventArgs e)
        {
            txtPinBilling.ForeColor = Color.Red;

            if (!txtPinBilling.Text.ISNullOrWhiteSpace())
            {
                if (txtPinBilling.Text.Length == 6)
                {
                    txtPinBilling.ForeColor = Color.Black;
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

        private void txtcontactOfBilling_TextChanged(object sender, EventArgs e)
        {
            txtcontactOfBilling.ForeColor = Color.Red;

            if (!txtcontactOfBilling.Text.ISNullOrWhiteSpace())
            {
                if (txtcontactOfBilling.Text.Length == 10 || txtcontactOfBilling.Text.Length == 12)
                {
                    txtcontactOfBilling.ForeColor = Color.Black;
                }
            }
        }

        private void txtContactShiping_TextChanged(object sender, EventArgs e)
        {
            txtContactShiping.ForeColor = Color.Red;

            if (!txtContactShiping.Text.ISNullOrWhiteSpace())
            {
                if (txtContactShiping.Text.Length == 10 || txtContactShiping.Text.Length == 12)
                {
                    txtContactShiping.ForeColor = Color.Black;
                }
            }
        }

        private void txtContctNo_TextChanged(object sender, EventArgs e)
        {
            txtContctNo.ForeColor = Color.Red;

            if (!txtContctNo.Text.ISNullOrWhiteSpace())
            {
                if (txtContctNo.Text.Length == 10 || txtContctNo.Text.Length == 12)
                {
                    txtContctNo.ForeColor = Color.Black;
                }
            }
        }

        private void txtAlternativNo_TextChanged(object sender, EventArgs e)
        {
            txtAlternativNo.ForeColor = Color.Red;

            if (!txtAlternativNo.Text.ISNullOrWhiteSpace())
            {
                if (txtAlternativNo.Text.Length == 10 || txtAlternativNo.Text.Length == 12)
                {
                    txtAlternativNo.ForeColor = Color.Black;
                }
            }
        }
    }
}
