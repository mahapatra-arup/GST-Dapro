using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAPRO.SoftwareLicense
{
    public static class SoftwareLicenceTools
    {
        public static string msg = "";
        public static bool IsValidLicenseDate()
        {
            DateTime cdATE = DateTime.Now;
            string query = "Select CurrentDate,EndDate from ApplicationInfo";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                string CurrentDate = dt.Rows[0]["CurrentDate"].ToString();
                string EndDate = dt.Rows[0]["EndDate"].ToString();

                DateTime? DCreaptCurrentDate = null; DateTime? DCreaptEndDate = null;
                try { DCreaptCurrentDate = DateTime.Parse(CryptorEngine.Decrypt(CurrentDate, true)); } catch (Exception e) { }
                try { DCreaptEndDate = DateTime.Parse(CryptorEngine.Decrypt(EndDate, true)); } catch (Exception e) { }

                if (DCreaptCurrentDate != null && DCreaptEndDate != null)
                {
                    if (DCreaptCurrentDate <= cdATE && DCreaptEndDate >= cdATE)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsValidSerialKey()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key != null)
            {
                RegistryKey sk1 = key.OpenSubKey("InstallNode");
                if (sk1 != null)
                {
                    RegistryKey sk2 = sk1.OpenSubKey("Ver32");
                    if (sk2 != null)
                    {
                        object serialKey = sk2.GetValue("RunDll");
                        if (serialKey != null)
                        {
                            return true;
                            //string decrptKey = LisenceUtils.Decrypt(serialKey.ToString(), true);
                            //if (LisenceUtils.IsValidCurrentLisence(decrptKey))
                            //{
                            //    return true;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Invalid serial key.", "SchoolPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                        }
                    }
                }
            }
            return false;
        }

        public static bool UpdateCurrentDate(DateTime date)
        {
            string dt = CryptorEngine.Encrypt(date.ToString("dd-MMM-yyyy"),true);
            string query = "update ApplicationInfo set CurrentDate='" + dt + "'";
            bool isSuccess = SQLHelper.GetInstance().ExcuteQuery(query, out msg);
            return isSuccess;
        }

        public static bool TestServerConnection()
        {
            if (SQLHelper.GetInstance().ExcuteQuery("Select 1", out msg))
            {
                return true;
            }
            return false;
        }
    }
}
