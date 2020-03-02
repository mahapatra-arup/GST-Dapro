using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPRO.SoftwareLicense
{
    class RegEdit
    {
        /// <summary>
        /// Create "Installnode" Sub key
        /// </summary>
        /// <param name="SubkeyName"></param>
        public static void CreateSubKey(string SubkeyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key!=null)
            {
                if (key.OpenSubKey("InstallNode", true) == null)
                {
                    key = Registry.CurrentUser.OpenSubKey("Software", true);
                    if (!SubkeyName.ISNullOrWhiteSpace())
                    {
                        key.CreateSubKey(SubkeyName);
                    }
                } 
            }
        }
        /// <summary>
        /// create sub_sub key in "installnode"
        /// </summary>
        /// <param name="SubkeyName"></param>
        public static bool CreateSubKeyPremium(string Premiumstr)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key != null)
            {
                CreateSubKey("InstallNode");
                key = key.OpenSubKey("InstallNode", true);
                if (key.OpenSubKey("Apps", true) == null)
                {
                    key.CreateSubKey("Apps");
                }
                key = key.OpenSubKey("Apps", true);
                string encrptKey = CryptorEngine.Encrypt(Premiumstr, true);
                key.SetValue("PremiumValue", encrptKey);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Read Subkey Apps Premium Value
        /// if  return string.empty then open activate window 
        /// other wise Return true then full and
        /// false the trail version
        /// </summary>
        /// <returns></returns>
        public static bool? ReadSubkeyAppsPremiumValue()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key != null)
            {
                RegistryKey sk1 = key.OpenSubKey("InstallNode");
                if (sk1 != null)
                {
                    RegistryKey sk2 = sk1.OpenSubKey("Apps");
                    if (sk2 != null)
                    {
                        object PremiumValue = sk2.GetValue("PremiumValue");
                        if (PremiumValue != null)
                        {
                            string PV = CryptorEngine.Decrypt(PremiumValue.ToString(), true).Trim().ToUpper();
                            if (PV == "TRUE")
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            return null;//means not activate
        }
        /// <summary>
        /// CreateSubKey for Client or Server Machine Purpose
        /// </summary>
        /// <param name="Premiumstr"></param>
        /// <returns></returns>
        public static bool CreateSubKeyMachineValue(string MachineValuestr)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key != null)
            {
                CreateSubKey("InstallNode");
                key = key.OpenSubKey("InstallNode", true);
                if (key.OpenSubKey("Apps", true) == null)
                {
                    key.CreateSubKey("Apps");
                }
                key = key.OpenSubKey("Apps", true);
                key.SetValue("MachineValue", MachineValuestr);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Read Machine Value  for Client or Server Machine Purpose
        /// </summary>
        /// <returns></returns>
        public static int? ReadSubkeyMachineValue()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            if (key != null)
            {
                RegistryKey sk1 = key.OpenSubKey("InstallNode");
                if (sk1 != null)
                {
                    RegistryKey sk2 = sk1.OpenSubKey("Apps");
                    if (sk2 != null)
                    {
                        object MachineValue = sk2.GetValue("MachineValue");
                        if (MachineValue != null)
                        {
                            int mv = 0;
                            int.TryParse(MachineValue.ToString().Trim(), out mv);
                            return mv;
                        }
                    }
                }
            }
            return null;//means client
        }
    }
}
