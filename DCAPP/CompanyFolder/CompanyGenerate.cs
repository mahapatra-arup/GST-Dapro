using DAPRO.SoftwareLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class CompanyGenerate : Form
    {
        string msg = string.Empty; string CONNECTION_STRING = string.Empty;
        public CompanyGenerate()
        {
            InitializeComponent();
            lblMSG.Text = "";
            pnlCreateWindow.SendToBack();
            pnl1.BringToFront();
            GetDatabaseName();
            //Defult Database set
            cmbDatabase.Text = SQLHelper.mInitalCatalog;
            //Only Admin access "User Create" And "set defult" company
            if (UserTools._UserName.ToUpper() != "ADMIN")
            {
                btnDefultSet.Enabled = false;
                btnGenerateCompany.Enabled = false;
            }
            //Up to 3 Database create Permission
            string[] list = Directory.GetFiles(Application.StartupPath, "*.mdf");
            string[] a = list;
            if (list.Length >= 3)
            {
                lblCreate.Enabled = false;
                btnGenerateCompany.Enabled = false;
                lblMSG.Text = "you are already three's \nCompany created";
            }

        }

        #region create company
        #region Event
        private void txtCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((Char.IsSymbol(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsPunctuation(e.KeyChar)) && !(e.KeyChar == '_'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void btnGenerateCompany_Click(object sender, EventArgs e)
        {

            string dbname = txtCompanyName.Text.Trim().ToUpper();
            string[] bakfile = Directory.GetFiles(Application.StartupPath, "dapro.bak");

            if (!dbname.ISNullOrWhiteSpace())
            {
                if (bakfile.Length >= 1)
                {
                    if (!IsValidDB(dbname))
                    {
                        Cursor = Cursors.WaitCursor;
                        //*************CREATE COMPANY USE CMD *******************
                        //CreateCompanyTools.CreateCompanyUtils(dbname, "dapro.bak",null);
                        //***********************CREATE COMPANY USE C# CODE********************************
                        if (CreateCompanyTools.CreateCompanyDB(dbname))
                        {
                            UpdateApplicationInfo(); 
                        }
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Alredy Exsist..", "Company", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtCompanyName.Select();
                    }
                }
                else
                {

                    MessageBox.Show("Error:> .bak File not avabile.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            GetDatabaseName();
        }

        private void chkDefult_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefult.Checked)
            {
                if (updateConfigFile(txtCompanyName.Text.Trim().ToUpper().GetDBFormatString()))
                {
                    if (MessageBox.Show("Are you sure you want to open [ " + txtCompanyName.Text.Trim().ToUpper() + "] Company", "Open New Company", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                this.Close();
            }
        }
        #endregion

        //**************************************************************************
        //Name :UpdateApplicationInfo
        //Description :Create new company connection string and next create "applicationinfo" table.
        //***************************************************************************
        private void UpdateApplicationInfo()
        {
            string databasename = txtCompanyName.Text.Trim().ToUpper().GetDBFormatString();

            CONNECTION_STRING = "Data Source=" + SQLHelper.mDataSource + "; Integrated Security = " + SQLHelper.mIntegratedSecurity +
            "; Initial Catalog=" + databasename + "";

            string query = "IF  EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApplicationInfo]') AND type in (N'U'))  " +
            "DROP TABLE[dbo].[ApplicationInfo] " +
            "SET ANSI_NULLS ON  " +
            "SET QUOTED_IDENTIFIER ON  " +
            "SET ANSI_PADDING ON  " +
            "CREATE TABLE[dbo].[ApplicationInfo](  " +
            "[Id][int] IDENTITY(1, 1) NOT NULL,  " +
            " [ApplicationId] [varchar](200) NULL,  " +
            "[StartDate]    [varchar](200)  NOT NULL,  [EndDate] [varchar](200)  " +
            "   NOT NULL,  [ApplicationKey] [varchar](200) NULL,  " +
            "[CurrentDate]  [varchar](200)    NULL,[STATUS] [varchar](50) NULL ) ON[PRIMARY]  " +
            "SET ANSI_PADDING OFF";


            if (ExcuteQuery(query, out msg))
            {
                if (InsertTableApplicationInfo())
                {
                    if (!UpdateUserControl(CONNECTION_STRING, databasename))
                    {
                        MessageBox.Show("user control faild.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (!UpdateFinancialYear(CONNECTION_STRING, databasename))
                    {
                        MessageBox.Show("user UpdateFinancialYear faild.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    chkDefult.Enabled = true;
                    MessageBox.Show("Company  Created successfull.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Company are not Created ERROR:" + msg);
            }
        }
        //**************************************************************************
        //Name :UpdateUserControl.
        //Description :search Runing  Database User information in <UserControl> table and all information are paste 
        //new company <UserControl> table.
        //***************************************************************************
        private bool UpdateUserControl(string CONNECTION_STRING, string databasename)
        {
            List<string> lstQueruy = new List<string>();
            string query = "select * from UserControl";
            //Running DN
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                query = "delete  from UserControl";
                lstQueruy.Add(query);
                foreach (DataRow item in dt.Rows)
                {
                    string UserName = item["UserName"].ToString(),
                    UserType = item["UserType"].ToString(),
                    Password = item["Password"].ToString(),
                    permision = item["permision"].ToString(),
                    IsDelete = item["IsDelete"].ToString(),
                    IsEdit = item["IsEdit"].ToString(),
                    IsCancel = item["IsCancel"].ToString();
                    string qry = "INSERT INTO UserControl(UserName,UserType " +
                    ",Password ,permision,IsDelete,IsEdit,IsCancel) VALUES('" + UserName + "','" + UserType + "' " +
                    ",'" + Password + "' ,'" + permision + "','" + IsDelete + "','" + IsEdit + "','" + IsCancel + "')";

                    lstQueruy.Add(qry);
                }
                if (ExecuteTransection(lstQueruy, out msg))
                {
                    return true;
                }
            }
            return false;
        }

        //**************************************************************************
        //Name :UpdateFinancialYear.
        //Description :search Runing  Database User information in <UpdateFinancialYear> table and all information are paste 
        //new company <UpdateFinancialYear> table.
        //***************************************************************************
        private bool UpdateFinancialYear(string CONNECTION_STRING, string databasename)
        {
            List<string> lstQueruy = new List<string>();
            string query = "select * from FinancialYear";
            //Running DN
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                // is Exsist Previos Row
                query = "delete  from FinancialYear";
                lstQueruy.Add(query);
                foreach (DataRow item in dt.Rows)
                {
                    string financialYearName = item["FinancialYearName"].ToString(),
                    startingDate = item["StartingDate"].ToString(),
                    endingDate = item["EndingDate"].ToString(),
                    curentFyear = item["CurentFyear"].ToString();
                    string qry = "INSERT INTO FinancialYear(FinancialYearName,StartingDate " +
                    ",EndingDate ,CurentFyear) VALUES('" + financialYearName + "','" + startingDate + "' " +
                    ",'" + endingDate + "' ,'" + curentFyear + "')";
                    lstQueruy.Add(qry);
                }
                if (ExecuteTransection(lstQueruy, out msg))
                {
                    InsertLedgerStatus();
                    return true;
                }
            }
            return false;
        }

        private void InsertLedgerStatus()
        {
            List<string> listquery = new List<string>();

            string query = "select ID from FinancialYear where CurentFyear='1'";
            object obj = ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                string[] templaetename = new string[] { "Sales A/C", "Sales Return A/C", "Purchase A/C", "Purchase Return A/C", "CASH" };
                string[] catagory = new string[] { "Sales", "Sales_Return", "Purchase", "Purchase_Return", "Customer" };
                for (int i = 0; i < 5; i++)
                {
                    string ledgerid = GetLedgerID(templaetename[i], catagory[i]);
                    query = "insert into ledgerstatus(LedgerID, FinYearID, OpeningBalance, CurrentBalance) values('" + ledgerid + "','" + obj.ToString() + "','0','0')";
                    listquery.Add(query);
                }
                ExecuteTransection(listquery,out msg);
            }
        }

        private string GetLedgerID(string templatename, string category)
        {
            string query = "select LadgerID from ladgermain where TemplateName='" + templatename + "' and Category='" + category + "'";
            object obj = ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }

        //**************************************************************************
        //Name :ExcuteQuery.
        //Description :ExcuteQuery manually create.
        //***************************************************************************
        public bool ExcuteQuery(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    // DebugTracer.Instance.WriteLog(ex.ToString());
                    return false;
                }

            }
        }
        //**************************************************************************
        //Name :ExecuteTransection.
        //Description :ExecuteTransection manually create.
        //***************************************************************************
        public bool ExecuteTransection(List<string> lstQuery, out string msg)
        {
            msg = "";
            if (lstQuery.Count > 0)
            {
                int index = 0;

                using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        SqlTransaction trans;
                        trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Connection = con;
                        cmd.Transaction = trans;

                        int isExecute = 0;
                        foreach (var item in lstQuery)
                        {
                            cmd.CommandText = item.ToString();
                            index++;
                            isExecute = cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        msg = e.Message;
                        //MessageBox.Show(index + " :" + ex.Message);
                    }
                }
            }

            return false;
        }
        //**************************************************************************
        //Name :ExcuteScalar.
        //***************************************************************************

        public object ExcuteScalar(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    //DebugTracer.Instance.WriteLog(ex.ToString());
                    return null;
                }
            }
        }
        //**************************************************************************
        //Name :updateConfigFile.
        //Description :Update "apps.config" InitialCatelog meand database name.
        //***************************************************************************
        public bool updateConfigFile(string dbName)
        {
            try
            {
                //string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["InitialCatalog"].Value = dbName;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sarver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        //**************************************************************************
        //Name :InsertTableApplicationInfo.
        //Description :InsertTableApplicationInfo.
        //***************************************************************************
        public bool InsertTableApplicationInfo()
        {
            string mEncryptStartDate = "", mEncryptEndDate = "", mEncryptCurrentDate = "", mApplicationGuid = "";

            string query = "SELECT ApplicationId, StartDate, EndDate, ApplicationKey, CurrentDate, STATUS   FROM ApplicationInfo";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);//running company
            if (dt.IsValidDataTable())
            {
                mApplicationGuid = dt.Rows[0]["ApplicationId"].ToString();
                mEncryptStartDate = dt.Rows[0]["StartDate"].ToString();
                mEncryptEndDate = dt.Rows[0]["EndDate"].ToString();
                mEncryptCurrentDate = dt.Rows[0]["CurrentDate"].ToString();
                string qry = "Insert into ApplicationInfo(ApplicationId,StartDate,EndDate,ApplicationKey,CurrentDate,STATUS) values('" + mApplicationGuid + "','" + mEncryptStartDate + "','" + mEncryptEndDate + "','','" + mEncryptCurrentDate + "','')";
                if (!ExcuteQuery(qry, out msg))//new company
                {
                    MessageBox.Show("(Table:ApplicationInfo) not Insert Problem" + msg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
        //**************************************************************************
        //Name :InsertTableApplicationInfo.
        //Description :InsertTableApplicationInfo.
        //***************************************************************************
        public bool IsValidDB(string databasebam)
        {
            string CONN_STR = "Data Source=" + SQLHelper.mDataSource + "; Integrated Security = " + SQLHelper.mIntegratedSecurity;

            string query = "SELECT * FROM sys.databases where name='" + databasebam + "'";
            using (SqlConnection con = new SqlConnection(CONN_STR))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    return false;
                }
            }
            return false;
        }

        #region Get Company
        public void GetDatabaseName()
        {
            string CONN_STR = "Data Source=" + SQLHelper.mDataSource + "; Integrated Security = " + SQLHelper.mIntegratedSecurity;
            string query = "SELECT name FROM sys.databases where Name not in('master','model','msdb','tempdb')";
            using (SqlConnection con = new SqlConnection(CONN_STR))
            {
                try
                {
                    SqlDataAdapter sqlAdptr = new SqlDataAdapter(query, con);

                    DataTable dataTable = new DataTable();

                    sqlAdptr.Fill(dataTable);
                    if (dataTable.IsValidDataTable())
                    {
                        foreach (DataRow item in dataTable.Rows)
                        {
                            cmbDatabase.Items.Add(item["name"].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
        }

        private void btnDefultSet_Click(object sender, EventArgs e)
        {
            if (IsValidDB(cmbDatabase.Text.Trim().ToUpper()))
            {
                if (updateConfigFile(cmbDatabase.Text.Trim().ToUpper()))
                {
                    if (MessageBox.Show("Are you sure you want to open [ " + cmbDatabase.Text.Trim().ToUpper() + "] Company", "Open New Company", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
            }
        }

        private void lblCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnl1.SendToBack();
            pnlCreateWindow.BringToFront();
            txtCompanyName.Select();
        }

        private void lnlBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnl1.BringToFront();
            cmbDatabase.Select();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Drag
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        private void CompanyGenerate_Shown(object sender, EventArgs e)
        {
            //********** Check Server machine and full version***************
            if (RegEdit.ReadSubkeyMachineValue() != 1&& RegEdit.ReadSubkeyAppsPremiumValue() != true)
            {
                lblCreate.Enabled = false;
            }
        }
    }
}
