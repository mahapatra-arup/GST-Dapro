using DAPRO.SoftwareLicense;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AppServerConfigWIndow : Form
    {
        #region Variable
        string msg = "";
        public event Action<bool, string> OnClose;
        public static string mDataSource = string.Empty;
        public static string mInitalCatalog = string.Empty;
        public static string mUserInstance = string.Empty;
        public static string mAttachedDb = string.Empty;
        public static string mIntegratedSecurity = string.Empty;

        private static string CONNECTION_STRING = "";
        private bool isConnectionSuccess = false;
        private string mApplicationGuid = "";
        private string MachineValue = "";
        private string mKey;
        #endregion

        public AppServerConfigWIndow(string Key)
        {
            InitializeComponent();
            Design();
            mKey = Key;
            //*************************************************************************
            //Name:SQLHelper.mInstance = null;
            //Description:Because Firt "ConnectionString" Store Initial Catalog is Old database name
            //in that case server error >> login faild>>,I Clear MInstance Name and generate New "ConnectionString"*******************
            //*************************************************************************
            SQLHelper.mInstance = null;
            //*******************Comapny window send to back*******************
            pnlCompany.SendToBack();
        }

        private void Design()
        {
            //*********get app icon and show pblogo**********;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.BackgroundImage = appIcon.ToBitmap();
            //****************application details************;
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblAppsname.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblCompanyMName.Text = fvi.CompanyName;
            lblCopyright.Text = fvi.LegalCopyright;
            mApplicationGuid = assembly.GetType().GUID.ToString();
        }
        //*************************************************************************
        //Name:Drag;
        //Description:Panel Draging System
        //************************************************************************

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

        //*************************************************************************
        //Name:AttaachDB;
        //Description:Attach .mdf file in sql database
        //************************************************************************
        private bool AttaachDB()
        {
            CONNECTION_STRING = string.Empty;
            mDataSource = System.Configuration.ConfigurationManager.AppSettings["DataSource"].ToString();
            mInitalCatalog = System.Configuration.ConfigurationManager.AppSettings["InitialCatalog"].ToString();
            mAttachedDb = System.Configuration.ConfigurationManager.AppSettings["AttachDbFilename"].ToString();
            mIntegratedSecurity = System.Configuration.ConfigurationManager.AppSettings["IntegratedSecurity"].ToString();
            mUserInstance = System.Configuration.ConfigurationManager.AppSettings["UserInstance"].ToString();
            if (rbtnServer.Checked)
            {
                CONNECTION_STRING = "Data Source=" + mDataSource + "; Integrated Security = " + mIntegratedSecurity + "; User Instance=" + mUserInstance + "; AttachDbFilename=" + mAttachedDb +
                                   "; Initial Catalog=" + mInitalCatalog + "";
                string query = "Select 1";
                using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        object obj = cmd.ExecuteScalar();
                        if (obj.ISValidObject())
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            return false;
        }
        //*************************************************************************
        //Name:SrverSecurityToolsInsert;
        //Description:create application info table and insert online server datetime in local sql server
        //************************************************************************
        private void SrverSecurityToolsInsert()
        {

            List<string> lstQuery = new List<string>();
            string mEncryptStartDate = "", mEncryptEndDate = "", mEncryptCurrentDate = "";
            if (rbtnServer.Checked)
            {
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
                lstQuery.Add(query);

                string CurrentDate = DateTime.Now.ToString();
                string StartDate = "";
                string EndDate = "";
                //*******************IF VALID KEY THEN  DATE TIME INSERT ONLIE SERVER TO OFFLINE SERVER*********************
                if (mKey.Length == 19)
                {
                    //*******************GET ONLINE DATABASE <START DATE> AND <END DATE> PERIOD*********************
                    OnlineActivationTools.OutOlnActivationDate(mKey, out StartDate, out EndDate);
                }
                else
                {
                    //*******************TRAIL PERIOD*********************
                    StartDate = CurrentDate;
                    EndDate = ((DateTime.Now).AddDays(7d)).ToString();
                }
                //*******************ENCRIPT DATE *********************
                mEncryptStartDate = CryptorEngine.Encrypt(StartDate, true);
                mEncryptEndDate = CryptorEngine.Encrypt(EndDate, true);
                mEncryptCurrentDate = CryptorEngine.Encrypt(CurrentDate, true);

                query = "Insert into ApplicationInfo(ApplicationId,StartDate,EndDate,ApplicationKey,CurrentDate,STATUS) values('" + mApplicationGuid + "','" + mEncryptStartDate + "','" + mEncryptEndDate + "','','" + mEncryptCurrentDate + "','')";
                lstQuery.Add(query);
                if (SQLHelper.GetInstance().ExecuteTransection(lstQuery, out msg))
                {
                    isConnectionSuccess = true;
                    DesktopNotify("Database Connected.", ToolTipIcon.Info);
                }
                else
                {
                    isConnectionSuccess = false;
                    //********Desktop notification*********
                    DesktopNotify("Database connection failed.", ToolTipIcon.Error);
                    //this.Close();
                }
            }
        }
        //*************************************************************************
        //Name:DatabaseDetails;
        //Description:Show Database Details in SQL
        //************************************************************************
        private void DatabaseDetails()
        {
            dataGridView1.DataSource = null;


            string query = "select name,user_access_desc,state_desc,create_date from sys.databases where name in('" + SQLHelper.mInitalCatalog + "')";//not in('master','model','tempdb','msdb')";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Status", typeof(string));
                dt.Rows.Add("Your database is not valid,please \n contact your service provider");
                dataGridView1.DataSource = dt;
                dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Beige;
            }
        }
        //*************************************************************************
        //Name:NewFolderCreate_mdfFileMove;
        //Description:Create New Folder And Move .mdf File when the Client server is selected
        //************************************************************************
        public void NewFolderCreate_mdfFileMove()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string name = "\\TEMP";
            try
            {
                //TEMP folder create
                FolderCreate.TEMPFolderCreate();
                DirectoryInfo d = new DirectoryInfo(Application.StartupPath);
                foreach (var file in d.GetFiles("*.mdf"))
                {
                    File.Move(Application.StartupPath + "\\" + file.Name, path + name);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error(Form:AppServerConfigWIndow)");
            }
        }
        //*************************************************************************
        //Name:Notify;
        //Description:Show Desktop Notification
        //************************************************************************
        #region Notify
        private void DesktopNotify(string MSG, ToolTipIcon icon)
        {
            NotifyIcon notifyIcon1 = new NotifyIcon();
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.BalloonTipText = MSG;
            notifyIcon1.BalloonTipTitle = "Connection";
            notifyIcon1.Text = MSG;
            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            //Optional - handle doubleclicks on the icon:
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
            notifyIcon1.ShowBalloonTip(10000);
        }
        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event
        private void rbtnServer_CheckedChanged(object sender, EventArgs e)
        {
            //**********Change Machine Value and Update Server name <.\sqlexpress>*********
            MachineValue = "1";

            if (rbtnServer.Checked)
            {
                if (MessageBox.Show("Are you sure that you want to Configure a Server machine ", "DATABASE CONNECTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    //***********Check .mdf FIle Alrady Release Folder Exist Or Not/*********
                    string[] list = Directory.GetFiles(Application.StartupPath, "*.mdf");
                    if (list==null||list.Length <= 0)
                    {
                        txtCompanyName.Text = OnlineOrgTools.CreateCompanyName();
                        //*******Open Companyny name panel***************
                        pnlServer.SendToBack();
                    }
                    else
                    {
                        string strSlace = list[0].Split('\\').Last();
                        //********Divided database_name.mdf(.mdfg devided)*********
                        string[] srrName = strSlace.Split('.');
                        //********UpdateConfig Setting Only Use Old Name(mdf file name)*********
                        UpdateAppsConfig(".\\SQLEXPRESS", srrName[0], "|DataDirectory|\\" + strSlace);
                        //********Attach database*********
                        if (AttaachDB())
                        {
                            //********Insert Application Info Table Start Date and Ens Date*********
                            SrverSecurityToolsInsert();
                            DatabaseDetails();
                        }
                        else
                        {
                            MessageBox.Show("ERROR:>> Database Attach Problem");
                        }
                    }
                }
            }
        }
        private void NewServer_OnClose(bool obj)
        {
            isConnectionSuccess = obj;
            DatabaseDetails();
        }
        private void AppServerConfigWIndow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose(isConnectionSuccess, MachineValue);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsSymbol(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsPunctuation(e.KeyChar)) && !(e.KeyChar == '_'))
            {
                e.Handled = true;
            }
        }
        private void rbtnClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnClient.Checked)
            {
                MachineValue = "0";
                if (MessageBox.Show("Are you sure that you want to connected Client machine With Server ", "DATABASE CONNECTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewFolderCreate_mdfFileMove();
                    ServerManagement newServer = new ServerManagement();
                    newServer.OnClose += NewServer_OnClose;
                    newServer.ShowDialog();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string dbname = txtCompanyName.Text.Trim().ToUpper();
            if (!dbname.ISNullOrWhiteSpace())
            {
                //***********Check .bak FIle Alrady Release Folder Exist Or Not/*********
                string[] bakfile = Directory.GetFiles(Application.StartupPath, "dapro.bak");
                if (bakfile.Length >= 1)
                {
                    if (!IsValidDB(dbname))
                    {
                        Cursor = Cursors.WaitCursor;
                        //********Create & Restore database*********
                        //CreateCompanyTools.CreateCompanyUtils(dbname, "dapro.bak", null);
                        //********UpdateConfig Setting*********
                        if (CreateCompanyTools.CreateCompanyDB(dbname))
                        {
                            UpdateAppsConfig(".\\SQLEXPRESS", dbname, "|DataDirectory|\\" + dbname + ".mdf");
                            //********Insert Application Info Table Start Date and Ens Date*********
                            SrverSecurityToolsInsert(); 
                        }
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Alredy Exsist..", "Company", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtCompanyName.Select();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("ERROR:>> .bak file are not available in your application");
                }
                //*******************Comapny window send to back*******************
                pnlCompany.SendToBack();
                //***************Local DB Main Server Connection*******************
                AttaachDB();
            }
            else
            {
                MessageBox.Show("Please Insert Valid Company Name");
            }
            DatabaseDetails();
        }
        #endregion

        //*****************************************************************************************
        //Name:UpdateAppsConfig;
        //Description:Confusion problem>> ,if choose client--> next server>> then Create a problem
        // solve in this problem i use "Update Configaration";
        //*****************************************************************************************
        private void UpdateAppsConfig(string serverName, string DatabaseName, string mdf_FileName)
        {
            if (rbtnServer.Checked)
            {
                //DataTable dataTable = SmoApplication.EnumAvailableSqlServers(true);//Local ClientServer
                //  dataTable.Rows.Add(SmoApplication.EnumAvailableSqlServers(true));//MainServer
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DataSource"].Value = serverName;
                config.AppSettings.Settings["InitialCatalog"].Value = DatabaseName;
                config.AppSettings.Settings["AttachDbFilename"].Value = mdf_FileName;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        //**************************************************************************
        //Name :IsValidDB.
        //Description :IsValidDB.
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

    }
}


//A.M//   
//Signature//