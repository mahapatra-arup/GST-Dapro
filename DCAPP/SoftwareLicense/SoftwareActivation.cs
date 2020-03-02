using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.SoftwareLicense
{
    public partial class SoftwareActivation : Form
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftwareActivation));
        bool _IsSuccess = false;
        string mKey = string.Empty;
        string mPremiumStatus = string.Empty;//***trail or full check**
        string mMachineValue = string.Empty;//***client or server check***
        //**********Org Details******************
        string mOrg_Name, mOrg_ContectNo, mOrg_Email;

        public SoftwareActivation()
        {
            InitializeComponent();
            //*******at first create temp folder Its must be importent**********
            FolderCreate.TEMPFolderCreate();
            Design();
            DeleteLDFfile();
            TrailAlradyExistCheck();
            pbImgRightWrong.Image = null;
            pnlNetConnection.SendToBack();
            pnlNetConnection.Visible = false;
        }

        private void GoForActivation()
        {
            if (!CheckInternetconnection.CheckForInternetConnection())
            {
                pnlNetConnection.BringToFront();
                pnlNetConnection.Visible = true;
                timer1.Start();
                return;
            }
            else
            {
                pnlNetConnection.SendToBack();
                pnlNetConnection.Visible = false;

                //******************Open Organization Details**************
                if (OnlineActivationTools.IsValidKey(mKey))
                {
                    if (!OnlineActivationTools.IsEmailNull(mKey))
                    {
                        OrgDetailsWindow orgfrm = new OrgDetailsWindow(mKey);
                        orgfrm.OnClose += Orgfrm_OnClose;
                        orgfrm.ShowDialog();
                    }
                    else
                    {
                        Orgfrm_OnClose("", "", "");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid serial key.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DesktopNotify("Invalid Key\nPlease enter a valid  key.", ToolTipIcon.Warning);
                    btnActivated.Enabled = true;

                }

            }
        }

        private void TrailAlradyExistCheck()
        {
            string msg = "";
            string query = "Select CurrentDate,EndDate from ApplicationInfo";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (RegEdit.ReadSubkeyAppsPremiumValue() != null || dt.IsValidDataTable())
            {
                linkLabel1.Enabled = false;
                lblErrorMessege.Text = "You have already used\n DAPRO trial version on this system";
            }
            else
            {
                lblErrorMessege.Text = "";
                linkLabel1.Enabled = true;
            }
        }

        private void DeleteLDFfile()
        {
            //ldf flie delete
            try
            {
                DirectoryInfo d = new DirectoryInfo(Application.StartupPath);
                foreach (var file in d.GetFiles("*.LDF"))
                {
                    File.Delete(Application.StartupPath + "\\" + file.Name);
                }
            }
            catch (Exception eee)
            {
            }
        }

        private void Design()
        {
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.BackgroundImage = appIcon.ToBitmap();
            //application details
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblAppsname.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblCompanyMName.Text = fvi.CompanyName;
            lblCopyright.Text = fvi.LegalCopyright;
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


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_IsSuccess == true)
            {
                if (RegEdit.CreateSubKeyMachineValue(mMachineValue))
                {
                    //Desktop notification
                    DesktopNotify("Product Activation Successfull", ToolTipIcon.Info);
                    //Next Restart Application
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Machine value not set Form:Software Activation");
                }
            }
        }

        #endregion

        private void SetRegistryKey()
        {
            string skey = mKey;
            if (mKey.Length == 19)
            {
                if (ActivationKey.SetRegistryKey(skey))
                {
                    _IsSuccess = true;
                    pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
                }
                else
                {
                    _IsSuccess = false;
                    pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                    MessageBox.Show("Please enter a valid serial key.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtKey1.Select();
                }
            }
            else
            {
                _IsSuccess = false;
                pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                MessageBox.Show("Please enter a fourteen digit serial key.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKey1.Select();
            }
        }

        #region Event
        private void Orgfrm_OnClose(string arg1, string arg2, string arg3)
        {
            //*********************Org Details**********************
            mOrg_Name = arg1;
            mOrg_ContectNo = arg2;
            mOrg_Email = arg3;

            //******Static Method Store Variable data***************
            OnlineOrgTools.gOrgName = mOrg_Name;
            OnlineOrgTools.gOrgContectNo = mOrg_ContectNo;
            OnlineOrgTools.gOrgEmail = mOrg_Email;

            //********************FIRS ONLINE ACTIVATION*************
            if (OnlineActivationTools.IsOnlineActivation(mKey, mOrg_Name, mOrg_ContectNo, mOrg_Email, "Remarks"))
            {
                linkLabel1.Enabled = false;
                //*******************set Registry***********************
                RegistrySetSetting();

                //*************SQL Server Connection*********************
                if (_IsSuccess)
                {
                    Cursor = Cursors.WaitCursor;
                    AppServerConfigWIndow FRMAppServer = new AppServerConfigWIndow(mKey);
                    FRMAppServer.OnClose += FRMAppServer_OnClose;
                    FRMAppServer.ShowDialog();
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                DesktopNotify("Already Activated.", ToolTipIcon.Warning);
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            btnActivated.Enabled = false;  //
            //*********Its must be importent because its define
            // installlation process is prime******************
            mPremiumStatus = "True";
            //***********Online Activation Process*************
            GoForActivation();
        }

        private void FRMAppServer_OnClose(bool arg1, string arg2)
        {
            _IsSuccess = arg1;
            mMachineValue = arg2;
            if (arg1)
            {
                //******** if btnfinish is enabled true then ****************
                btnNext.Enabled = true;
                btnClose.Enabled = false;
                pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
            }
            else
            {
                pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                lblErrorMessege.Text = "Application Activation and Server Connection \n Problem please Contact Service Provider";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //************Key text clear***************
            txtKey1.Text = "";
            txtKey2.Text = "";
            txtKey3.Text = "";
            txtKey4.Text = "";
            //**********Its must be importent because its define 
            //installlation process is Trail********************
            mPremiumStatus = "False";

            //****************Local Server Connection*************
            AppServerConfigWIndow FRMAppServerTrail = new AppServerConfigWIndow(mKey);
            FRMAppServerTrail.OnClose += FRMAppServerTrail_OnClose;
            FRMAppServerTrail.ShowDialog();
        }

        private void FRMAppServerTrail_OnClose(bool arg1, string arg2)
        {
            _IsSuccess = arg1;
            mMachineValue = arg2;
            if (!arg1)
            {
                _IsSuccess = false;
                pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                lblErrorMessege.Text = "Trail Period Activation and Server Connection \n Problem please Contact Service Provider";
            }
            else
            {
                _IsSuccess = true;
                //*****registry set for trail or full purpose**********
                RegistrySetSetting();

                //************false full activation********************
                txtKey1.Enabled = false;
                txtKey2.Enabled = false;
                txtKey3.Enabled = false;
                txtKey4.Enabled = false;
                btnActivated.Enabled = false;
                //***************************
                btnNext.Enabled = true;
            }
        }

        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !(e.KeyChar == '\b'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '\b'))
            {

            }
        }

        private void txtKey3_Leave(object sender, EventArgs e)
        {
            mKey = txtKey1.Text + "-" + txtKey2.Text + "-" + txtKey3.Text + "-" + txtKey4.Text;
        }

        private void txtKey1_TextChanged(object sender, EventArgs e)
        {
            if (txtKey1.Text.Length == 4)
            {
                txtKey2.Focus();
                txtKey2.Select();
            }
            CheckValidKeyOrNotInSoftware();
        }

        private void txtKey2_TextChanged(object sender, EventArgs e)
        {
            if (txtKey2.Text.Length == 4)
            {
                txtKey3.Focus();
                txtKey3.Select();
            }
            CheckValidKeyOrNotInSoftware();
        }

        private void txtKey1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.V && Control.ModifierKeys == Keys.Control)
            {
                //********at first clear  textbox*************
                txtKey1.Text = "";
                txtKey2.Text = "";
                txtKey3.Text = "";
                txtKey4.Text = "";

                string Copydate = Clipboard.GetText().Trim();
                //Clipboard.Clear();
                string[] key = Copydate.Split('-');
                try
                {
                    txtKey1.Text = key[0];
                    txtKey2.Text = key[1];
                    txtKey3.Text = key[2];
                    txtKey4.Text = key[3];
                }
                catch (Exception)
                {
                    _IsSuccess = false;
                }
            }
            CheckValidKeyOrNotInSoftware();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKey1.Text = "";
            txtKey2.Text = "";
            txtKey3.Text = "";
            txtKey4.Text = "";
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtKey1.Focused || txtKey2.Focused || txtKey3.Focused || txtKey4.Focused)
            {
                pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
                _IsSuccess = true;
                txtKey1.Text = "";
                txtKey2.Text = "";
                txtKey3.Text = "";
                txtKey4.Text = "";

                string Copydate = Clipboard.GetText().Trim();
                // Clipboard.Clear();
                string[] key = Copydate.Split('-');
                try
                {
                    txtKey1.Text = key[0];
                    txtKey2.Text = key[1];
                    txtKey3.Text = key[2];
                    txtKey4.Text = key[3];
                }
                catch (Exception)
                {
                    _IsSuccess = false;
                    pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                }
            }
            //***********Check valid or not**************************
            if (mKey.Length == 19)
            {
                if (!ActivationKey.SetRegistryKey(mKey))
                {
                    btnActivated.Enabled = false;
                    btnActivated.Enabled = false;
                    pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                    return;
                }
                else
                {
                    btnActivated.Enabled = true;
                    _IsSuccess = true;
                    pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
                    btnActivated.Enabled = true;
                }
            }
        }

        private void txtKey4_TextChanged(object sender, EventArgs e)
        {
            mKey = txtKey1.Text + "-" + txtKey2.Text + "-" + txtKey3.Text + "-" + txtKey4.Text;
            CheckValidKeyOrNotInSoftware();
        }
        private void txtKey3_TextChanged(object sender, EventArgs e)
        {
            if (txtKey3.Text.Length == 4)
            {
                txtKey4.Focus();
                txtKey4.Select();
            }
            CheckValidKeyOrNotInSoftware();
        }


        private void pbRefreshInternetConnection_Click(object sender, EventArgs e)
        {
            GoForActivation();
        }
        private void SoftwareActivation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DesktopNotify("Internet connection.\nPlease check your internet connection.", ToolTipIcon.Warning);
            pnlNetConnection.SendToBack();
            pnlNetConnection.Visible = false;

            timer1.Stop();
        }
        #endregion

        #region Notify
        private void DesktopNotify(string MSG, ToolTipIcon icon)
        {
            NotifyIcon notifyIcon1 = new NotifyIcon();
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.BalloonTipText = MSG;
            notifyIcon1.BalloonTipTitle = "Activation";
            notifyIcon1.Text = MSG;
            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            //Optional - handle doubleclicks on the icon:
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
            notifyIcon1.ShowBalloonTip(5000);
        }
        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {

        }

        private void SoftwareActivation_Shown(object sender, EventArgs e)
        {
            string Activation_Key = "";
            //***********If alrady active and Activate=true then only show local server connection**********
            if (OnlineActivationTools.IsValidLicenseDate(out Activation_Key))
            {
                string[] key = Activation_Key.Split('-');
                try
                {
                    txtKey1.Text = key[0];
                    txtKey2.Text = key[1];
                    txtKey3.Text = key[2];
                    txtKey4.Text = key[3];

                    txtKey1.Enabled = false;
                    txtKey2.Enabled = false;
                    txtKey3.Enabled = false;
                    txtKey4.Enabled = false;
                    btnActivated.Enabled = false;
                    linkLabel1.Enabled = false;
                }
                catch (Exception)
                {
                }

                Cursor = Cursors.WaitCursor;
                AppServerConfigWIndow FRMAppServer = new AppServerConfigWIndow(mKey);
                FRMAppServer.OnClose += FRMAppServer_OnClose;
                FRMAppServer.ShowDialog();
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //********************************************************************************
        //Name : RegistrySetSetting
        //Descriptiopn :

        ///At first delete old registry
        /// If premium version is true then sEtregistry method call and Set key in registry.
        /// Full or Trail Version Set  in registry
        //***************************************************************************************
        private void RegistrySetSetting()
        {
            #region Delete Registry
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\InstallNode");
            }
            catch (Exception) { }
            #endregion

            if (_IsSuccess == true)
            {
                //***********only for Full version set registry**************
                if (mPremiumStatus == "True")
                {
                    SetRegistryKey();
                }
                //
                //**************Full or Trail Version Set***********************
                if (!RegEdit.CreateSubKeyPremium(mPremiumStatus))
                {
                    MessageBox.Show("Premium value not set Form:Software Activation");
                }
            }
        }

        private void CheckValidKeyOrNotInSoftware()
        {
            if (mKey.Length == 19)
            {
                if (!ActivationKey.IsValidLisenceKey(mKey))
                {
                    _IsSuccess = false;
                    btnActivated.Enabled = false;
                    btnActivated.Enabled = false;
                    pbImgRightWrong.Image = global::DAPRO.Properties.Resources.Exit;
                    return;
                }
                else
                {
                    btnActivated.Enabled = true;
                    _IsSuccess = true;
                    pbImgRightWrong.Image = ((System.Drawing.Image)(resources.GetObject("pbImgRightWrong.Image")));
                    btnActivated.Enabled = true;
                }
            }
            else
            {
                pbImgRightWrong.Image = global::DAPRO.Properties.Resources.btnClearSelect;
            }
        }

    }
}


//A.M//   
//Signature//