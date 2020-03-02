using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DAPRO
{
    public static class ORG_Tools
    {
        #region Data Members
        public static string _OrganizationName = "",
           _Address = "",
           _CityTown = "",
           _Dist = "",
           _State = "",
           _StateCode = "",
           _PIN = "",
           _PAN = "",
           _CorporateNo = "",
           _GSTtype = "",
           _GSTin = "",
            _GSTCompositionType = "",
            _GSTCompositionPersentage = "",
           _ContactNo1 = "",
           _AlternativeNo = "",
           _Email = "",
           _website = "",
           _TIN = "",
           _BusinessCatagory = "",

       _NameBilling = "",
       _AddressBilling = "",
       _CityTownBilling = "",
       _DistBilling = "",
       _StateBilling = "",
       _PINBilling = "",

       _NameShipping = "",
       _ContactShipping = "",
       _AddressShipping = "",
       _CityTownShipping = "",
       _DistShipping = "",
       _StateShipping = "",
       _PINShipping = "",
        _CompositeDeclaration="";

        #endregion
        public static bool _IsRegularGST = false, _IsCompositGST = false, _IsUnregisterGST = false;
        public static Bitmap _Logo;
        public static byte[] _LogoByte;

        static string msg = "";
        public static bool InitDetails()
        {
            //string query = "Select OrganizationDetails.*,State.StateName,State.StateCode from OrganizationDetails inner join State on OrganizationDetails.StateID=State.ID";
            string query = "Select OrganizationDetails.*,OrganizationAddress.*,State.StateName,State.StateCode from OrganizationDetails inner join state on OrganizationDetails.StateID = State.ID inner join OrganizationAddress on OrganizationDetails.OrganizationName = OrganizationAddress.OrganizationName";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                _OrganizationName = dt.Rows[0]["OrganizationName"].ToString();
                _Address = dt.Rows[0]["Address"].ToString();
                _CityTown = dt.Rows[0]["CityTown"].ToString();
                _Dist = dt.Rows[0]["Dist"].ToString();
                _State = dt.Rows[0]["StateName"].ToString();
                _StateCode = dt.Rows[0]["StateCode"].ToString();
                _PIN = dt.Rows[0]["PIN"].ToString();
                _PAN = dt.Rows[0]["PAN"].ToString();
                _CorporateNo = dt.Rows[0]["CorporateNo"].ToString();
                _GSTtype = dt.Rows[0]["GSTtype"].ToString();
                _GSTin = dt.Rows[0]["GSTin"].ToString();
                _GSTCompositionType = dt.Rows[0]["CompositionType"].ToString();
                _GSTCompositionPersentage = dt.Rows[0]["CompositionPercentage"].ToString();
                _GSTin = dt.Rows[0]["GSTin"].ToString();
                _ContactNo1 = dt.Rows[0]["ContactNo1"].ToString();
                _AlternativeNo = dt.Rows[0]["AlternativeNo"].ToString();
                _Email = dt.Rows[0]["Email"].ToString();
                _website = dt.Rows[0]["website"].ToString();
                _TIN = dt.Rows[0]["TIN"].ToString();
                _BusinessCatagory = dt.Rows[0]["BusinessCatagory"].ToString();

                _NameBilling = dt.Rows[0]["BillingName"].ToString();
                _AddressBilling = dt.Rows[0]["BillingAddress"].ToString();
                _CityTownBilling = dt.Rows[0]["BillingTown"].ToString();
                _DistBilling = dt.Rows[0]["BillingDist"].ToString();
                _StateBilling = dt.Rows[0]["BillingState"].ToString();
                _PINBilling = dt.Rows[0]["BillingPIN"].ToString();

                _NameShipping = dt.Rows[0]["ShippingName"].ToString();
                _ContactShipping = dt.Rows[0]["ShippingContactNo"].ToString();
                _AddressShipping = dt.Rows[0]["ShippingAddress"].ToString();
                _CityTownShipping = dt.Rows[0]["ShippingTown"].ToString();
                _DistShipping = dt.Rows[0]["ShippingDist"].ToString();
                _StateShipping = dt.Rows[0]["ShippingState"].ToString();
                _PINShipping = dt.Rows[0]["ShippingPIN"].ToString();

                if (_GSTtype == "Regular")
                {
                    _IsRegularGST = true;
                    _IsUnregisterGST = false;
                    _IsCompositGST = false;
                }
                else if (_GSTtype == "Composition")
                {
                    _IsCompositGST = true;
                    _IsRegularGST = false;
                    _IsUnregisterGST = false;
                    _CompositeDeclaration= dt.Rows[0]["CompositionDeclaration"].ToString();
                }
                else if (_GSTtype == "Unregister")
                {
                    _IsUnregisterGST = true;
                    _IsRegularGST = false;
                    _IsCompositGST = false;
                }
                GetImage();
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void GetImage()
        {
            string query = "Select LOGO from OrganizationDetails";
            _LogoByte = SQLHelper.GetInstance().ExcuteQueryAndGetImageData(query, out msg);
            if (_LogoByte != null)
            {
                MemoryStream msI = new MemoryStream(_LogoByte, 0, _LogoByte.Length);
                Bitmap btmap = new Bitmap(msI);
                _Logo = ConfigureImage(btmap, 128, 128, 1);
            }
            else
            {
            }
        }
        public static Bitmap ConfigureImage(Bitmap image, int maxWidth, int maxHeight, int quality)
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
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            // newImage.Save(filePath, imageCodecInfo, encoderParameters);
            return newImage;
        }
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        ////
        public static void GetORGDetails(out string name, out string address, out string legalNos, out string stateCode, out string state)
        {
            name = ""; address = ""; legalNos = ""; stateCode = ""; state = "";
            name = ORG_Tools._OrganizationName;
            address = INVOICE_TOOLS._IsOrgAddress ? _Address + " \n" : "";
            address += INVOICE_TOOLS._IsOrgCityTown ? _CityTown : "";
            address += INVOICE_TOOLS._IsOrgDistrict ? ", " + _Dist + " \n" : "";
            address += INVOICE_TOOLS._IsOrgState ? _State : "";
            address += INVOICE_TOOLS._IsOrgPin ? ", " + _PIN + " \n" : "";
            address += INVOICE_TOOLS._IsOrgContactNo ? _ContactNo1 + " \n" : "";
            address += INVOICE_TOOLS._IsOrgMailID ? _Email : "";
            address += INVOICE_TOOLS._IsOrgWebsite ? " , " + _website : "";

            address += "\nState :" + _StateCode + "-" + _State;

            legalNos = INVOICE_TOOLS._IsOrgGSTIN ? "GSTIN : " + _GSTin + " \n" : "";
            legalNos += INVOICE_TOOLS._IsOrgCIN ? "CIN    : " + _CorporateNo + " \n" : "";
            legalNos += INVOICE_TOOLS._IsOrgPAN ? "PAN    : " + _PAN + " \n" : "";

            stateCode = ORG_Tools._StateCode;
            state = ORG_Tools._State;
        }
    }
}
