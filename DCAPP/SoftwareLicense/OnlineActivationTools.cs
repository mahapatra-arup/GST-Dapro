using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAPRO
{
    class OnlineActivationTools
    {
        #region Activation
        public static bool InstallStatus = false;
        public static string msg = string.Empty;
        private static string mActivationKey = string.Empty, mOrg_Name = string.Empty, mOrg_ContactNo = string.Empty, mOrg_Email = string.Empty
        , mInstalletion_Purpose = string.Empty, mRemarks = string.Empty, mMotherboard_ID = string.Empty, mMAC_Id = string.Empty, mHDD_No = string.Empty;
        private static bool mActivated = false, mActivation_Type = false;

        public static List<string> lstQuery = new List<string>();
        //**************************************************************************
        //NAME: IsOnlineActivation
        //DESCRIPTION: IT WILL BE DISCUSS HOW TO CONFIGURE ONLINE SERVER DATABSE???? 
        //**************************************************************************
        public static bool IsOnlineActivation(string activationKey,
        string Org_Name, string Org_ContactNo, string Org_Email
        , string Remarks)
        {
            #region Variable
            mActivationKey = activationKey;
            mOrg_Name = Org_Name;
            mOrg_ContactNo = Org_ContactNo;
            mOrg_Email = Org_Email;
            mRemarks = Remarks;
            //Arup v1.a.m
            mMotherboard_ID = HardwareInfo.GetBaseBoardSlNo();
            mMAC_Id = HardwareInfo.GetMACAddress();
            mHDD_No = HardwareInfo.GetHDDSerialNo();

            mActivated = true;
            //***************TYPE MEANS tRAL OR FULL VERSION********************
            mActivation_Type = true;
            #endregion

            string Query = "SELECT  Activation_Key FROM Dapro_Activation where Activation_Key=@Key and Activated='false' and  " +
                "(ISNULL(Motherboard_ID, '') = '') and (ISNULL(HDD_No, '') = '') and (ISNULL(MAC_Id, '') = '')";
            AzureSQLHelper.SetParamiterWithValue("Key", activationKey.Trim());
            object o = AzureSQLHelper.GetInstance().ExcuteScalar(Query);
            if (o != null)
            {
                InstallStatus = FirstTimeSaveActivationInfo();
            }
            else
            {
                Query = "SELECT  Activation_Key FROM Dapro_Activation where Activation_Key=@Key and Activated='true' and Motherboard_ID='" + mMotherboard_ID + "' " +
                "and MAC_Id='" + mMAC_Id + "' and HDD_No='" + mHDD_No + "' and (Expiry_date < CAST(Getdate() AS DATE))";
                AzureSQLHelper.SetParamiterWithValue("Key", activationKey.Trim());
                object obj = AzureSQLHelper.GetInstance().ExcuteScalar(Query);
                if (obj != null)
                {
                    InstallStatus = RenewalSaveActivationInfo();
                }
            }
            return InstallStatus;
        }
        //**************************************************************************
        //NAME:      FirstTimeSaveActivationInfo
        //DESCRIPTION:  FIRST TIME ONLINE SERVER CONFIG
        //**************************************************************************
        private static bool FirstTimeSaveActivationInfo()
        {
            lstQuery.Clear();
            mInstalletion_Purpose = "FIRST_TIME";

            List<SqlParameter> SubParamList = new List<SqlParameter>();
            List<List<SqlParameter>> ParamList = new List<List<SqlParameter>>();
            //*************************Get Current date *******************
            string startDate = "(SELECT GETDATE())";
            //*************************and next add 1 year in start date and substract 1 day in days *******************
            string expireDate = "(SELECT DATEADD(day,-1,(DATEADD(year,1,GETDATE()))))";
            string Query = "UPDATE   Dapro_Activation SET  Activation_Date=" + startDate + ",Activation_Type=@Activation_Type,Motherboard_ID=@Motherboard_ID,MAC_Id=@MAC_Id, " +
            "HDD_No=@HDD_No,Activated=@Activated,Org_Name=@Org_Name,Org_ContactNo=@Org_ContactNo,Org_Email=@Org_Email " +
            " WHERE Activation_Key=@Activation_Key";
            SubParamList.Add(new SqlParameter("@Activation_Key", mActivationKey));
            SubParamList.Add(new SqlParameter("@Activation_Type", mActivation_Type));
            SubParamList.Add(new SqlParameter("@Motherboard_ID", mMotherboard_ID));
            SubParamList.Add(new SqlParameter("@MAC_Id", mMAC_Id));
            SubParamList.Add(new SqlParameter("@HDD_No", mHDD_No));
            SubParamList.Add(new SqlParameter("@Activated", mActivated));
            SubParamList.Add(new SqlParameter("@Org_Name", mOrg_Name));
            SubParamList.Add(new SqlParameter("@Org_ContactNo", mOrg_ContactNo));
            SubParamList.Add(new SqlParameter("@Org_Email", mOrg_Email));

            lstQuery.Add(Query);
            ParamList.Add(SubParamList);

            //******************ActivationInfo***************************
            SubParamList = new List<SqlParameter>();
            Query = "INSERT INTO Dapro_ActivationInfo(Activation_Key,Start_Date,Expiry_date,Installation_Purpose,Remarks) " +
            "VALUES(@Activation_Key,"+ startDate + "," + expireDate + ",@Installation_Purpose,@Remarks)";
            SubParamList.Add(new SqlParameter("@Activation_Key", mActivationKey));
            SubParamList.Add(new SqlParameter("@Installation_Purpose", mInstalletion_Purpose));
            SubParamList.Add(new SqlParameter("@Remarks", mRemarks));

            lstQuery.Add(Query);
            ParamList.Add(SubParamList);
            bool bo = AzureSQLHelper.GetInstance().ExecuteTransection(lstQuery, ParamList);
            if (bo)
            {
                return true;
            }
            return false;
        }
        //**************************************************************************
        //NAME:      RenewalSaveActivationInfo
        //DESCRIPTION:  RENEWAL TIME ONLINE SERVER CONFIG
        //**************************************************************************
        private static bool RenewalSaveActivationInfo()
        {
            lstQuery.Clear();
            mInstalletion_Purpose = "RENEWAL";

            List<SqlParameter> SubParamList = new List<SqlParameter>();
            List<List<SqlParameter>> ParamList = new List<List<SqlParameter>>();

            //*************************Get From Date and To date Use key*******************
            string fromDate = "", toDate = "";
            OutOlnActivationDate(mActivationKey, out fromDate, out toDate);//i use only (toDate)
            //*************************at first 1 day add in last activation date*******************
            string startDate = "(SELECT DATEADD(day,1,'"+ toDate + "'))";
            //*************************and next add 1 year in start date and substract 1 day in days *******************
            string expireDate = "(SELECT DATEADD(day,-1,(DATEADD(year,1,"+ startDate + "))))";
            string Query = "INSERT INTO Dapro_ActivationInfo(Activation_Key,Start_Date,Expiry_date,Installation_Purpose,Remarks) " +
            "VALUES(@Activation_Key,"+ startDate + "," + expireDate + ",@Installation_Purpose,@Remarks)";
            SubParamList.Add(new SqlParameter("@Activation_Key", mActivationKey));
            SubParamList.Add(new SqlParameter("@Installation_Purpose", mInstalletion_Purpose));
            SubParamList.Add(new SqlParameter("@Remarks", mRemarks));

            lstQuery.Add(Query);
            ParamList.Add(SubParamList);
            bool bo = AzureSQLHelper.GetInstance().ExecuteTransection(lstQuery, ParamList);
            if (bo)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Bool Tools
        //**************************************************************************
        //NAME:      IsEmailNull
        //PURPOSE:  CHECK VALID VALID MAIL OR NOT USE KEY
        //**************************************************************************
        public static bool IsEmailNull(string key)
        {
            string Query = "SELECT  * FROM Dapro_Activation where Activation_Key=@Key and (ISNULL(Org_Email, '') <> '') ";
            AzureSQLHelper.SetParamiterWithValue("Key", key);
            object o = AzureSQLHelper.GetInstance().ExcuteScalar(Query);
            if (o != null)
            {
                return true;
            }
            return false;
        }
        //**************************************************************************
        //NAME:      IsValidKey
        //PURPOSE:  CHECK VALID KEY ,THE CONDITION IS IF ACTIVATE=TRUE THE HARDWARE INFO NOT EMTY
        //IF ACTIVATE=FALSE THEN HARDWARE INFO EMPTY
        //**************************************************************************
        public static bool IsValidKey(string key)
        {
            mMotherboard_ID = HardwareInfo.GetBaseBoardSlNo();
            mMAC_Id = HardwareInfo.GetMACAddress();
            mHDD_No = HardwareInfo.GetHDDSerialNo();

            string Query = "SELECT  * FROM Dapro_Activation where Activation_Key=@Key  and ((Activated='true' and Motherboard_ID='" + mMotherboard_ID + "' " +
                "and MAC_Id='" + mMAC_Id + "' and HDD_No='" + mHDD_No + "') or (Activated='false' and (ISNULL(Motherboard_ID, '') = '') and (ISNULL(HDD_No, '') = '') and (ISNULL(MAC_Id, '') = '')))";
            AzureSQLHelper.SetParamiterWithValue("Key", key);
            object o = AzureSQLHelper.GetInstance().ExcuteScalar(Query);
            if (o != null)
            {
                return true;
            }
            return false;
        }

        //**************************************************************************
        //NAME:      IsValidLicenseDate
        //PURPOSE:  Check this installlation key,or mac_id...etc installlation date between online server date
        //**************************************************************************
        public static bool IsValidLicenseDate(out string Activation_Key)
        {
            Activation_Key = "";
            string motherboard_ID = HardwareInfo.GetBaseBoardSlNo();
            string MAC_Id = HardwareInfo.GetMACAddress();
            string HDD_No = HardwareInfo.GetHDDSerialNo();
            //Get Online server currentdate
            string Qry = "select top(1)ID,Activation_Key from dbo.Dapro_ActivationInfo where Activation_Key = (SELECT Activation_Key FROM  Dapro_Activation " +
            "where   Motherboard_ID = '" + motherboard_ID + "' " +
            "and MAC_Id = '" + MAC_Id + "' and Dapro_Activation.HDD_No = '" + HDD_No + "'  and Activated = 'true') " +
            "and (Expiry_date >= CAST(Getdate() AS DATE)) " +
            "order by ID desc";
            DataTable d = AzureSQLHelper.GetInstance().ExcuteNonQuery(Qry);
            if (d.IsValidDataTable())
            {
                Activation_Key = d.Rows[0]["Activation_Key"].ToString();
                return true;
            }
            return false;
        }
        #endregion

        #region Get Tools
        //**********************************************************************************
        //NAME:OutOlnActivationDate
        //DESCRIPTION: GET ONLINE SERVER DATETIME USE KEY(START DATE AND EXPIRE DATE)
        //**********************************************************************************
        public static void OutOlnActivationDate(string key, out string dateFrom, out string dateTo)
        {
            dateFrom = ""; dateTo = "";
            string Query = "SELECT  * FROM Dapro_ActivationInfo where ID=(SELECT MAX(ID) FROM Dapro_ActivationInfo WHERE Activation_Key=@Key)";
            AzureSQLHelper.SetParamiterWithValue("Key", key);
            DataTable dt = AzureSQLHelper.GetInstance().ExcuteNonQuery(Query);
            if (dt.IsValidDataTable())
            {
                dateFrom=dt.Rows[0]["Start_Date"].ToString();
                dateTo= dt.Rows[0]["Expiry_date"].ToString();
            }
        }
        #endregion
    }
}
                                                                          //ARUP//
                                                                        //SIGNATURE//