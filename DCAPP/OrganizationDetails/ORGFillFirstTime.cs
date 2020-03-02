using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAPRO
{
    class ORGFillFirstTime
    {
        string msg = "";
        public void Init()
        {
            string query = "Select GSTtype from OrganizationDetails ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query,out msg);
            if (!o.ISValidObject())
            {
                CreateCompany frm = new CreateCompany(true);
               
                frm.ShowDialog();
            }
        }
    }
}
