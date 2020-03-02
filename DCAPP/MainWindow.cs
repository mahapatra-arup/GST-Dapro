using System;
using System.Drawing;
using System.Windows.Forms;
using DAPRO.User;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using DAPRO.SoftwareLicense;
using System.Reflection;

namespace DAPRO
{
    public partial class MainWindow : Form
    {
        string msg = "";
        private bool mIsSlideCollapse = false;
        private bool mIsMaximize = true;
        private Point mPreviousLocation;
        private Size mPreviousSize;
        /// <summary>
        /// Window Form Objects
        /// </summary>
        private Form currentForm;
        private InventoryWindow frmInventoryWindow;
        private CustomerWindows frmCustomerWindow;
        private SuppliersMainWindow frmSuppliersMainWindow;
        private EmployeeWindow frmEmployeeWindow;
        private ReportWindow frmReportWindow;
        private UserWindow frmUserWindow;
        private StockWindow frmStockWindow;
        private SalesWindows frmSalesWindows;
        private SettingsWindow frmSettingsWindow;
        private PurchaseWindows frmpurchaseWindow;
        private NoteAndVoucherList frmnoteAndVoucherWindow;

        public MainWindow()
        {
            InitializeComponent();
            AppInfo();
            GetButtonControl();
            pnlLogin.BringToFront();
            this.FormBorderStyle = FormBorderStyle.None;
            ThisMaximize();

            mPreviousSize = new Size(859, 517);
            int x = (this.Width - Screen.PrimaryScreen.WorkingArea.Width) / 2;
            int y = (this.Height - Screen.PrimaryScreen.WorkingArea.Height) / 2;
            mPreviousLocation = new Point(x, y);

            ApplicationDesign.btnObject = btnDashBoard;
            object o = (object)btnDashBoard;
            ApplicationDesign.SetButtonDesign(ref o);
            //Arup
            ChangeDateFormate();
            AccessPermission.GrantAccess();
        }
        private void AppInfo()
        {
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.BackgroundImage = appIcon.ToBitmap();
            //application details
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblAppsName.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblCopiright.Text = fvi.LegalCopyright;
        }
        private void GetButtonControl()
        {
            //List<Control> list = new List<Control>();

            //GetAllControl(flowLayoutPanel1, list);
            int i = 0;
            foreach (Control control in flowLayoutPanel1.Controls)
            {

                if (control.GetType() == typeof(Button))
                {

                    UserTools._Controls[i] = control.Name.ToString();
                    i++;
                }
            }
        }
        private void BtnEnable()
        {
            string query = "Select permision from UserControl where UserID='" + UserTools._UserID + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null && obj != DBNull.Value)
            {
                string sbtn = obj.ToString();
                string[] btnarry = sbtn.Split(',');
                foreach (object i in btnarry)
                {
                    for (int j = 0; j < UserTools._Controls.Length; j++)
                    {
                        if (UserTools._Controls[j] == i.ToString())
                        {
                            ((Button)flowLayoutPanel1.Controls[UserTools._Controls[j]]).Visible = true;
                        }

                    }
                }
            }
        }
        private void ThisMaximize()
        {
            Size currentSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.Size = currentSize;
            this.Location = new Point(0, 0);
        }
        /// <summary>
        /// Dragging
        /// </summary>
# region Drag
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
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            ThisMaximize();
        }
        private void CreateTxtFile()
        {
            string fileName = Application.StartupPath + "\\systemfile.txt";
            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    DateTime date = DateTime.Now.Date;
                    sw.WriteLine(date.ToString("dd-MMM-yyyy"));
                }
            }
            catch (Exception)
            {
            }
        }
        private bool TestServerConnection()
        {
            if (SQLHelper.GetInstance().ExcuteQuery("Select * from UserControl", out msg))
            {
                return true;
            }
            return false;
        }
        private void ChangeDateFormate()
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            rkey.SetValue("sShortDate", "dd-MMM-yyyy");
            rkey.SetValue("sLongDate", "d MMMM,yyyy");
        }
        private void MainWdfindow_Shsaown1(object sender, EventArgs e)
        {
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
                                LoginWindow login = new LoginWindow();
                                login.TopLevel = false;
                                login.FormBorderStyle = FormBorderStyle.None;
                                login.onClose += new Action<bool>(login_onClose);

                                pnlLoginShow.Controls.Clear();
                                pnlLoginShow.Controls.Add(login);
                                login.Show();
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

                                LoginWindow login = new LoginWindow();
                                login.TopLevel = false;
                                login.FormBorderStyle = FormBorderStyle.None;
                                login.onClose += new Action<bool>(login_onClose);

                                pnlLoginShow.Controls.Clear();
                                pnlLoginShow.Controls.Add(login);
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
                        LoginWindow login = new LoginWindow();
                        login.TopLevel = false;
                        login.FormBorderStyle = FormBorderStyle.None;
                        login.onClose += new Action<bool>(login_onClose);

                        pnlLoginShow.Controls.Clear();
                        pnlLoginShow.Controls.Add(login);
                        login.Show();
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
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (IsActivated.IsActivatedStatus())
            {
                LoginWindow login = new LoginWindow();
                login.TopLevel = false;
                login.FormBorderStyle = FormBorderStyle.None;
                login.onClose += new Action<bool>(login_onClose);

                pnlLoginShow.Controls.Clear();
                pnlLoginShow.Controls.Add(login);
                login.Show();
            }
        }

        private void FrmTestServer_OnClose(bool obj)
        {
            this.Close();
        }
        private void Process_Exited(object sender, EventArgs e)
        {
        }
        void login_onClose(bool flag)
        {
            if (flag)
            {
                //Desktop notification
                DesktopNotify("Login Successfull", ToolTipIcon.Info);

                DefaultClass cls = new DefaultClass();
                BtnEnable();

                pnlLogin.SendToBack();
                pnlOrg.BringToFront();
                CreateTxtFile();
                if (!ORG_Tools.InitDetails())
                {
                    Init();
                }
                else
                {
                    ProceedtoContinue();
                }
            }
            else
            {
                this.Close();
            }
            lblUn.Visible = true;
            lbllogout.Visible = true;
            lblUn.Text = ORG_Tools._OrganizationName + " Login as " + UserTools._UserName;
        }

        #region Notify
        private void DesktopNotify(string MSG, ToolTipIcon icon)
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
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
            notifyIcon1.ShowBalloonTip(10000);
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            //click the ballon then fire it
        }
        #endregion

        /// <summary>
        /// Use First Time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Init()
        {
            CreateCompany frmCreateCompany = new CreateCompany(true);
            frmCreateCompany.onClose += FrmCreateCompany_onClose;
            frmCreateCompany.TopLevel = false;
            frmCreateCompany.FormBorderStyle = FormBorderStyle.None;
            pnlOrg.Controls.Clear();
            pnlOrg.Controls.Add(frmCreateCompany);
            frmCreateCompany.Show();
        }
        private void FrmCreateCompany_onClose()
        {
            if (FinancialYearTools.ISAccountDateExist())
            {
                ProceedtoContinue();
            }
            else
            {
                AddfinancialYear frmAddfinancialYear = new AddfinancialYear(FinancialYearTools._YearID, "&Next", false);
                frmAddfinancialYear.OnClose += FrmAddfinancialYear_OnClose;
                frmAddfinancialYear.TopLevel = false;
                frmAddfinancialYear.WindowState = FormWindowState.Maximized;
                frmAddfinancialYear.FormBorderStyle = FormBorderStyle.None;
                pnlOrg.Controls.Clear();
                pnlOrg.Controls.Add(frmAddfinancialYear);
                frmAddfinancialYear.Show();
            }
        }
        private void ProceedtoContinue()
        {
            if (LedgerTools.GetCashLedgers())
            {
                if (FinancialYearTools.ISAccountDateExist())
                {
                    pnlOrg.SendToBack();
                    pnlLogin.SendToBack();
                    pnlWindow.BringToFront();
                    btnAccounts_Click(btnDashBoard, null);
                }
                else
                {
                    AddfinancialYear frmAddfinancialYear = new AddfinancialYear(FinancialYearTools._YearID, "&Next", false);
                    frmAddfinancialYear.OnClose += FrmAddfinancialYear_OnClose;
                    frmAddfinancialYear.TopLevel = false;
                    frmAddfinancialYear.WindowState = FormWindowState.Maximized;
                    frmAddfinancialYear.FormBorderStyle = FormBorderStyle.None;
                    pnlOrg.Controls.Clear();
                    pnlOrg.Controls.Add(frmAddfinancialYear);
                    frmAddfinancialYear.Show();
                }

            }
            else
            {
                AddCashAccount frmAddCashAccount = new AddCashAccount();
                frmAddCashAccount.OnClose += ProceedtoContinue;
                frmAddCashAccount.TopLevel = false;
                frmAddCashAccount.FormBorderStyle = FormBorderStyle.None;
                pnlOrg.Controls.Clear();
                pnlOrg.Controls.Add(frmAddCashAccount);
                frmAddCashAccount.Show();
            }
        }
        private void FrmAddfinancialYear_OnClose()
        {
            ProceedtoContinue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnMenuHide_Click(object sender, EventArgs e)
        {
            if (mIsSlideCollapse)
            {
                splitContainer1.SplitterDistance = 165;
                mIsSlideCollapse = false;
            }
            else
            {
                splitContainer1.SplitterDistance = 45;
                mIsSlideCollapse = true;
            }
            if (currentForm != null)
            {
                currentForm.Size = pnlWindow.Size;
                pnlWindow.Controls.Clear();
                pnlWindow.Controls.Add(currentForm);
                currentForm.Show();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            //if (SQLHelper.IsChangeAnyDataBase)
            //{
            //   if( MessageBox.Show("Do you Wnat To BackUp?", "Back Up", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        MessageBox.Show("done");
            //    }
            //}
            if (MessageBox.Show("Are sure to close the application ?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (mIsMaximize)
            {
                btnMaximize.Image = global::DAPRO.Properties.Resources.maximize_14;
                this.Size = mPreviousSize;
                this.Location = mPreviousLocation;
                mIsMaximize = false;
            }
            else
            {
                mPreviousSize = new Size(this.Width, this.Height);
                mPreviousLocation = this.Location;
                ThisMaximize();
                mIsMaximize = true;
                btnMaximize.Image = global::DAPRO.Properties.Resources.minimize_14;
            }
            if (currentForm != null)
            {
                currentForm.Size = pnlWindow.Size;
                pnlWindow.Controls.Clear();
                pnlWindow.Controls.Add(currentForm);
                currentForm.Show();
            }
        }
        private void btnMenuHide_MouseHover(object sender, EventArgs e)
        {
            btnMenuHide.Image = global::DAPRO.Properties.Resources.MenuBar_Big;
        }
        private void btnMenuHide_MouseLeave(object sender, EventArgs e)
        {
            btnMenuHide.Image = global::DAPRO.Properties.Resources.MenuBar_Small;
        }

        /// <summary>
        /// Module Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStaff_Click(object sender, EventArgs e)
        {

        }
        private void btnAccounts_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            DashboardWindow frmDashboardWindow = new DashboardWindow();
            frmDashboardWindow.WindowState = FormWindowState.Maximized;
            frmDashboardWindow.FormBorderStyle = FormBorderStyle.None;
            frmDashboardWindow.TopLevel = false;

            currentForm = frmDashboardWindow;
            frmDashboardWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmDashboardWindow);
            frmDashboardWindow.Show();
        }
        private void btnSupplier_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);
            frmSuppliersMainWindow = new SuppliersMainWindow();
            frmSuppliersMainWindow.FormBorderStyle = FormBorderStyle.None;
            frmSuppliersMainWindow.WindowState = FormWindowState.Maximized;
            frmSuppliersMainWindow.TopLevel = false;
            currentForm = frmSuppliersMainWindow;
            frmSuppliersMainWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmSuppliersMainWindow);
            frmSuppliersMainWindow.Show();
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);
            if (frmCustomerWindow == null)
            {
                frmCustomerWindow = new CustomerWindows();
                frmCustomerWindow.FormBorderStyle = FormBorderStyle.None;
                frmCustomerWindow.WindowState = FormWindowState.Maximized;
                frmCustomerWindow.TopLevel = false;
            }
            currentForm = frmCustomerWindow;
            frmCustomerWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCustomerWindow);
            frmCustomerWindow.Show();
        }
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);
            if (frmEmployeeWindow == null)
            {
                frmEmployeeWindow = new EmployeeWindow();
                frmEmployeeWindow.FormBorderStyle = FormBorderStyle.None;
                frmEmployeeWindow.WindowState = FormWindowState.Maximized;
                frmEmployeeWindow.TopLevel = false;
            }
            currentForm = frmEmployeeWindow;
            frmEmployeeWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmEmployeeWindow);
            frmEmployeeWindow.Show();
        }
        private void btnUserWindow_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmUserWindow = new UserWindow();
            frmUserWindow.WindowState = FormWindowState.Maximized;
            frmUserWindow.FormBorderStyle = FormBorderStyle.None;
            frmUserWindow.TopLevel = false;

            currentForm = frmUserWindow;
            frmUserWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmUserWindow);
            frmUserWindow.Show();
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmReportWindow = new ReportWindow();
            frmReportWindow.WindowState = FormWindowState.Maximized;
            frmReportWindow.FormBorderStyle = FormBorderStyle.None;
            frmReportWindow.TopLevel = false;

            currentForm = frmReportWindow;
            frmReportWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmReportWindow);
            frmReportWindow.Show();
        }
        private void btnGoodsScervices_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            if (frmInventoryWindow == null)
            {
                frmInventoryWindow = new InventoryWindow();
                frmInventoryWindow.WindowState = FormWindowState.Maximized;
                frmInventoryWindow.FormBorderStyle = FormBorderStyle.None;
                frmInventoryWindow.TopLevel = false;
            }

            currentForm = frmInventoryWindow;
            frmInventoryWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmInventoryWindow);
            frmInventoryWindow.Show();
        }
        private void btnSales_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmSalesWindows = new SalesWindows();
            frmSalesWindows.WindowState = FormWindowState.Maximized;
            frmSalesWindows.FormBorderStyle = FormBorderStyle.None;
            frmSalesWindows.TopLevel = false;

            currentForm = frmSalesWindows;
            frmSalesWindows.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmSalesWindows);
            frmSalesWindows.Show();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmSettingsWindow = new SettingsWindow();
            frmSettingsWindow.WindowState = FormWindowState.Maximized;
            frmSettingsWindow.FormBorderStyle = FormBorderStyle.None;
            frmSettingsWindow.TopLevel = false;

            currentForm = frmSettingsWindow;
            frmSettingsWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmSettingsWindow);
            frmSettingsWindow.Show();
        }
        private void btnPurchas_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmpurchaseWindow = new PurchaseWindows();
            frmpurchaseWindow.WindowState = FormWindowState.Maximized;
            frmpurchaseWindow.FormBorderStyle = FormBorderStyle.None;
            frmpurchaseWindow.TopLevel = false;

            currentForm = frmpurchaseWindow;
            frmpurchaseWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmpurchaseWindow);
            frmpurchaseWindow.Show();

        }
        private void btnCrDrNote_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            frmnoteAndVoucherWindow = new NoteAndVoucherList(NoteAndVoucherList._Type.Credit_Note);
            frmnoteAndVoucherWindow.WindowState = FormWindowState.Maximized;
            frmnoteAndVoucherWindow.FormBorderStyle = FormBorderStyle.None;
            frmnoteAndVoucherWindow.TopLevel = false;

            currentForm = frmnoteAndVoucherWindow;
            frmnoteAndVoucherWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmnoteAndVoucherWindow);
            frmnoteAndVoucherWindow.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SoftwareBackupSystem frmBackup = new SoftwareBackupSystem();
            frmBackup.ShowDialog();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DbUpdate.Dbupdate();
        }
        private void btnBanking_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            BankWindow frmBankWindow = new BankWindow();
            frmBankWindow.WindowState = FormWindowState.Maximized;
            frmBankWindow.FormBorderStyle = FormBorderStyle.None;
            frmBankWindow.TopLevel = false;

            currentForm = frmBankWindow;
            frmBankWindow.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmBankWindow);
            frmBankWindow.Show();
        }
        private void btnExpense_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetButtonDesign(ref sender);

            TransectionList frmTransectionList = new TransectionList();
            frmTransectionList.WindowState = FormWindowState.Maximized;
            frmTransectionList.FormBorderStyle = FormBorderStyle.None;
            frmTransectionList.TopLevel = false;

            currentForm = frmTransectionList;
            frmTransectionList.Size = pnlWindow.Size;
            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmTransectionList);
            frmTransectionList.Show();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}
