using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPRO.SoftwareLicense
{
    class OnlineOrgTools
    {
        public static string gOrgName ,
                             gOrgContectNo ,
                             gOrgEmail ,
                             gOrgRemarks;

        public static string CreateCompanyName()
        {
            if (!gOrgName.ISNullOrWhiteSpace())
            {
                string str = gOrgName.Replace(" ", "_");
                 str = str.Replace("[","");
                 str = str.Replace("]", "");
                 str = str.Replace("(", "");
                 str = str.Replace(")", "");
                 str = str.Replace("-", "_");
                 str = str.Replace("@", "");
                string result = str.Length>70? str.Substring(0, 69): str;
                return result;
            }
            else
            {
                return "DAPRO";
            }
        }
    }
}
