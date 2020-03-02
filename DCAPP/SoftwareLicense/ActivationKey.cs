using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPRO.SoftwareLicense
{
    public static class ActivationKey
    {
        public static List<string> _CurrentSerialList;
        public static List<string> GetCurrentSerialList()
        {
            _CurrentSerialList = new List<string>();
            
            #region -------------------1st 25 Key List-------------------------------
            _CurrentSerialList.Add("DR1Y-M2NP-CO3E-DT4A");
            _CurrentSerialList.Add("D5YC-6DNP-C7DE-DT9B");
            _CurrentSerialList.Add("D9YC-MD1P-C2DE-D3LC");
            _CurrentSerialList.Add("4RYC-5DNP-CO6E-DT7D");
            _CurrentSerialList.Add("D8YC-MD9P-CO1E-D2LE");
            _CurrentSerialList.Add("D3YC-M4NP-CO5E-DT6F");
            _CurrentSerialList.Add("DRY7-MDN8-COD9-DTL1");
            _CurrentSerialList.Add("D2YC-MD3P-CO4E-DTL5");
            _CurrentSerialList.Add("DRY6-MDN7-C8DE-DT9L");
            _CurrentSerialList.Add("DR2C-MD3P-CO7E-D5LL");
            _CurrentSerialList.Add("DR5C-M2NP-CO8E-7TLK");
            _CurrentSerialList.Add("D1YC-MDNP-COD2-DTLL");
            _CurrentSerialList.Add("DRYC-M7NP-CO9E-DTLM");
            _CurrentSerialList.Add("D4YC-MDNP-C2DE-DTLN");
            _CurrentSerialList.Add("DRYC-M2NP-CODE-9TLO");
            _CurrentSerialList.Add("D2YC-MDNP-CO5E-DTLP");
            _CurrentSerialList.Add("D2YC-MDNP-C2DE-D2LQ");
            _CurrentSerialList.Add("DR2C-MD5P-CO7E-DT5R");
            _CurrentSerialList.Add("D1YC-1DNP-1ODE-DTLS");
            _CurrentSerialList.Add("DRY6-MDN9-COD7-DTLT");
            _CurrentSerialList.Add("DR7C-MD5P-CO4E-DTLU");
            _CurrentSerialList.Add("D8YC-M4NP-CO8E-11LV");
            _CurrentSerialList.Add("D9YC-MDN9-CO8E-DT7W");
            _CurrentSerialList.Add("DR5C-MD2P-COD1-DT8X");
            _CurrentSerialList.Add("DR2C-MDN3-1OD7-D8LY");
            #endregion-------------------End 1st 25 Key List-------------------------------

            #region -------------------2nd 25 Key List-------------------------------
            _CurrentSerialList.Add("4TLA-C1DE-M5NP-1RY9");
            _CurrentSerialList.Add("D5LA-C62E-MD7P-DR3C");
            _CurrentSerialList.Add("5TL5-C7DE-M3NP-DR1C");
            _CurrentSerialList.Add("D46A-C2DE-M3NP-D8YC");
            _CurrentSerialList.Add("D9LA-C37E-M8NP-DRY3");
            _CurrentSerialList.Add("D7LA-C5DE-MD4P-D5YC");
            _CurrentSerialList.Add("DT5A-C78E-M7NP-DR9C");
            _CurrentSerialList.Add("DTL9-C91E-M8NP-2RYC");
            _CurrentSerialList.Add("5TLA-C1DE-MD8P-DRY7");
            _CurrentSerialList.Add("D5LA-C6DE-MD7P-5RYC");
            _CurrentSerialList.Add("6TLA-C78E-MDN5-D5YC");
            _CurrentSerialList.Add("9TLA-C9DE-8DNP-DR1C");
            _CurrentSerialList.Add("DT5A-C3D8-MDN5-4RYC");
            _CurrentSerialList.Add("6TLA-57DE-M6NP-DRY8");
            _CurrentSerialList.Add("D8LA-C93E-M7NP-D2YC");
            _CurrentSerialList.Add("DTL3-C5D6-M8NP-5RYC");
            _CurrentSerialList.Add("DT6A-11DE-MD2P-D7YC");
            _CurrentSerialList.Add("DT6A-C1DE-MD1P-DR1C");
            _CurrentSerialList.Add("45LA-C5DE-MDN7-D5YC");
            _CurrentSerialList.Add("DT6A-C4DE-4DNP-DR7C");
            _CurrentSerialList.Add("DTL7-C5DE-5DNP-6RYC");
            _CurrentSerialList.Add("1TL6-C5D8-M4NP-D46C");
            _CurrentSerialList.Add("D8LA-C6D6-9DNP-D5YC");
            _CurrentSerialList.Add("DT8A-C9D7-MDNP-D6YC");
            _CurrentSerialList.Add("6TLA-C1DE-5DNP-4RY6");

            #endregion -------------------End 2nd 25 Key List-------------------------------

            #region Emp Active
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL1");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL2");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL3");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL4");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL5");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL6");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL7");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL8");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DTL9");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT10");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT11");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT12");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT13");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT14");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT15");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT16");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT17");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT18");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT19");
            _CurrentSerialList.Add("DRYC-CODE-MDNP-DT20");

            #endregion
            return _CurrentSerialList;
        }
        public static bool IsValidLisenceKey(string key)
        {
            GetCurrentSerialList();
            if (_CurrentSerialList.Contains(key))
            {
                return true;
            }
            return false;
        }
        public static bool SetRegistryKey(string skey)
        {
            if (IsValidLisenceKey(skey))
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

                if (key != null)
                {
                    RegEdit.CreateSubKey("InstallNode");
                    key = key.OpenSubKey("InstallNode", true);
                    key.CreateSubKey("Ver32");
                    key = key.OpenSubKey("Ver32", true);
                    string encrptKey = CryptorEngine.Encrypt(skey, true);
                    key.SetValue("RunDll", encrptKey);

                    return true;
                }
            }
            return false;
        }
    }
}
