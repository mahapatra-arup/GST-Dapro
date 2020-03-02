using DAPRO.SoftwareLicense;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public  class IsActivated
    {
        public static bool IsActivatedStatus()
        {
            Cursor.Current = Cursors.WaitCursor;
            //****************** Date Update From Online Server to Offline server*********************************************************
            DateUpdateFromOnlineServer();
            //******************activation check*********************************************************

            #region Full
            if (RegEdit.ReadSubkeyAppsPremiumValue() == true)//==true,Because its nullable value return
            {
                if (SoftwareLicenceTools.IsValidSerialKey())
                {
                    //Server connection test only Full Version Software
                    if (RegEdit.ReadSubkeyMachineValue() == 1)
                    {
                        #region Server
                        if (SoftwareLicenceTools.IsValidLicenseDate())
                        {

                            if (SoftwareLicenceTools.UpdateCurrentDate(DateTime.Now))
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("System Date Is not Recognized, Please Contect Service Provider", "DAPPRO licence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Application licence is expire are you activate your application ?", "DAPPRO licence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                SoftwareActivation frm = new SoftwareActivation();
                                frm.ShowDialog();
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }

                    }
                    #endregion
                    else if (RegEdit.ReadSubkeyMachineValue() == 0)
                    {
                        #region Client
                        if (SoftwareLicenceTools.TestServerConnection())
                        {
                            if (SoftwareLicenceTools.IsValidLicenseDate())
                            {

                                return true;
                            }
                            else
                            {
                                if (MessageBox.Show("Application licence is expire are you activate your application ?", "DAPPRO licence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    SoftwareActivation frm = new SoftwareActivation();
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }

                        else
                        {
                            ServerManagement frmTestServer = new ServerManagement();
                            frmTestServer.OnClose += FrmTestServer_OnClose;
                            frmTestServer.ShowDialog();
                        }
                        #endregion
                    }
                    else
                    {
                        SoftwareActivation frm = new SoftwareActivation();
                        frm.ShowDialog();
                    }

                }
                else
                {
                    SoftwareActivation frm = new SoftwareActivation();
                    frm.ShowDialog();
                }
            }
            #endregion

            #region Trail
            else if (RegEdit.ReadSubkeyAppsPremiumValue() == false)
            {
                if (SoftwareLicenceTools.IsValidLicenseDate())
                {
                    if (SoftwareLicenceTools.UpdateCurrentDate(DateTime.Now))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("System Date Is not Recognized, Please Contect Service Provider", "DAPPRO licence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (MessageBox.Show("Application licence is expire are you activate your application ?", "DAPPRO licence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        SoftwareActivation frm = new SoftwareActivation();
                        frm.ShowDialog();
                    }
                    else
                    {
                        Application.Exit();
                    }

                }
            }

            #endregion

            #region FirstTime
            else
            {
                SoftwareActivation frm = new SoftwareActivation();
                frm.ShowDialog();
            }
            #endregion
            return false;
            Cursor.Current = Cursors.Default;
        }

        private static void FrmTestServer_OnClose(bool obj)
        {
            if (obj)
            {
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }
        }

        private static void DateUpdateFromOnlineServer()
        {
            //******************Check Internet connection*********************************************************
            if (CheckInternetconnection.CheckForInternetConnection())
            {
                //******************online date and system date same or not check*********************************************************
                DateTime? onlineDate = OnlineServerDateTimeTools.GetOnlineServerDate();
                if (onlineDate!=null)
                {
                    if (DateTime.Now.Date == onlineDate.Value.Date)
                    {
                        #region Update datetime from online to offline server
                        //***********************Update datetime from online to offline server**************************************
                        string Activation_Key = "", StartDate = "", EndDate = "", msg = "";
                        string mEncryptStartDate = "", mEncryptEndDate = "", mEncryptCurrentDate = "";
                        if (OnlineActivationTools.IsValidLicenseDate(out Activation_Key))
                        {
                            //*******************GET ONLINE DATABASE <START DATE> AND <END DATE> PERIOD*********************
                            OnlineActivationTools.OutOlnActivationDate(Activation_Key, out StartDate, out EndDate);

                            //*******************ENCRIPT DATE ****************************************************************
                            string CurrentDate = DateTime.Now.ToString();
                            mEncryptStartDate = CryptorEngine.Encrypt(StartDate, true);
                            mEncryptEndDate = CryptorEngine.Encrypt(EndDate, true);
                            mEncryptCurrentDate = CryptorEngine.Encrypt(CurrentDate, true);


                            string qry = "update ApplicationInfo set StartDate='" + mEncryptStartDate + "',EndDate='" + mEncryptEndDate + "',CurrentDate='" + mEncryptCurrentDate + "'";
                            if (SQLHelper.GetInstance().ExcuteQuery(qry, out msg))
                            {
                            }
                        }
                        #endregion

                    }
                    else 
                    {
                        if (MessageBox.Show("Your system Date and Time is not currect \n you can chang the Date and Time in now ? ", "Invalid Date", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                        {
                            //*******************Set System Date ****************************************************************
                            onlineDate.SetSystemDate();
                            DesktopNotify("System Date Changing Successfull", ToolTipIcon.Info);
                        }
                        else
                        {
                            DesktopNotify("Please Set Current Date and Time in Your system", ToolTipIcon.Warning);
                            Application.Exit();
                        }
                    }
                }
            }
        }
        #region Notify
        private static void DesktopNotify(string MSG, ToolTipIcon icon)
        {
            NotifyIcon notifyIcon1 = new NotifyIcon();
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.BalloonTipText = MSG;
            notifyIcon1.BalloonTipTitle = "DAPRO";
            notifyIcon1.Text = MSG;
            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            //Optional - handle doubleclicks on the icon:
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked1;
            notifyIcon1.ShowBalloonTip(10000);
        }

        private static void NotifyIcon1_BalloonTipClicked1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
       
        #endregion
    }
}
