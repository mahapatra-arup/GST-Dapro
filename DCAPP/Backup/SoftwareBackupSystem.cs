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

namespace DAPRO
{
    public partial class SoftwareBackupSystem : Form
    {
        string msg = "";
        public SoftwareBackupSystem()
        {
            InitializeComponent();
            Design();
            btn.Text = "HOME";
        }
        private void Design()
        {
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.Image = appIcon.ToBitmap();
            //application details
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblAppsName.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblCopiright.Text = fvi.LegalCopyright;
            lblVersion.Text = fvi.FileVersion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            btn.Text = "BACKUP";
            btnMennualBackup.Visible = true;
            btnRestoreBackup.Visible = false;
            btnAutoBackup.Visible = true;
            btnTimeSheduleBackup.Visible = true;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            btn.Text = "RESTORE";
            btnMennualBackup.Visible = false;
            btnRestoreBackup.Visible = true;
            btnAutoBackup.Visible = false;
            btnTimeSheduleBackup.Visible = false;
        }

        #region Drage
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }

        }

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void splitContainer1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        private void btnMennualBackup_Click(object sender, EventArgs e)
        {
            pnSettingWindow.Visible = true;
            pnlBack_Restore.Visible = true;
            pnSettingWindow.BringToFront();
            pnlBack_Restore.BringToFront();

            //Backup_Restore Window Design
            btnBackupDatabase.Enabled = false;
            btnBackupDatabase.BackColor = Color.LightGray;
            btnRestoreDatabase.Enabled = false;
            btnRestoreDatabase.BackColor = Color.LightGray;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            pnSettingWindow.Visible = true;
            pnSettingWindow.BringToFront();

            nudTime.Enabled = true;
            //sHOW TIME AND LOCATION
            try
            {
                txtSource.Text = XMLTools.ReadXmlElementString(Application.StartupPath + "\\StoreProcedureInfo.xml", "BACKUPSOURCE");


                string source = XMLTools.ReadXmlElementString(Application.StartupPath + "\\StoreProcedureInfo.xml", "BACKUPTIME").Trim();
                string[] split = new string[] {":" };
                string[] result = source.Split(split, StringSplitOptions.None);
                nudTime.Value = int.Parse(result[0]);
                nudMinutTime.Value = int.Parse(result[1]);

            }
            catch (Exception exx)
            {
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            pnSettingWindow.Visible = false;
            pnSettingWindow.SendToBack();

            ///Restore and Back up Panel
            pnlBack_Restore.Visible = false;
            pnlBack_Restore.SendToBack();
        }

        #region Backup_Restore
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fb.SelectedPath;
            }
        }

        private void btnRestoreBackup_Click(object sender, EventArgs e)
        {
            pnSettingWindow.Visible = true;
            pnlBack_Restore.Visible = true;
            pnSettingWindow.BringToFront();
            pnlBack_Restore.BringToFront();

            //Backup_Restore Window Design
            btnBackupDatabase.Enabled = false;
            btnBackupDatabase.BackColor = Color.LightGray;
            btnRestoreDatabase.Enabled = false;
            btnRestoreDatabase.BackColor = Color.LightGray;

            if (UserTools._UserName.ToUpper() == "ADMIN")
            {
                grpRestore.Enabled = true;
            }
            else
            {
                grpRestore.Enabled = false;
                MessageBox.Show("This feature is not available in " + UserTools._UserName + "", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnBackupBrouse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = fb.SelectedPath;
                btnBackupDatabase.Enabled = true;
                btnBackupDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
            }
        }

        private void btnBrouseRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Application.StartupPath;
            fd.Filter = "Backup Files(*.zip)|*.zip";
            fd.FilterIndex = 0;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                btnRestoreDatabase.Enabled = true;
                btnRestoreDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                txtRestore.Text = fd.FileName;
            }
        }

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            if (!txtRestore.Text.ISNullOrWhiteSpace())
            {
                if (Islogin(UserTools._UserName, textBox1.Text))
                {
                    if (MessageBox.Show("Are you sure you want to continue ?\nAfter restored your current data will be lost and new data will be load.", "Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        string location = txtRestore.Text.GetDBFormatString();
                        if (SQLHelper.GetRestore(location))
                        {
                            if (MessageBox.Show("Restore complete.Restart your application ??", "Restore Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) ;
                            { Application.Restart(); }
                        }
                        else
                            MessageBox.Show("Restore not complete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Please Insert Valid Password", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select backup file path.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBackupDatabase_Click(object sender, EventArgs e)
        {

            string location = txtBackup.Text.GetDBFormatString();
            if (!location.ISNullOrWhiteSpace())
            {
                Cursor = Cursors.WaitCursor;
                if (SQLHelper.GetNewBackUp(location))
                {
                    MessageBox.Show("Database backup successfully completed.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                    MessageBox.Show("Back up not complete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Select backup file location.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        private void btnTimeSheduleBackup_Click(object sender, EventArgs e)
        {
            nudTime.Enabled = true;
            string BACKUPSOURCE = XMLTools.ReadXmlElementString(Application.StartupPath + "\\StoreProcedureInfo.xml", "BACKUPSOURCE");
            string BACKUPTIME = XMLTools.ReadXmlElementString(Application.StartupPath + "\\StoreProcedureInfo.xml", "BACKUPTIME");
            if (!BACKUPSOURCE.ISNullOrWhiteSpace()&& !BACKUPTIME.ISNullOrWhiteSpace())
            {
                Backup_RestoreTools brt = new Backup_RestoreTools();
                brt.BAT_FILE_CREATE_AND_RUN_CMD(BACKUPSOURCE, BACKUPTIME); 
            }
            else
            {
                MessageBox.Show("At first go to Update Your Setting --->");
                btnSetting_Click(null, null);
            }
        }

        private void btnAutoBackup_Click(object sender, EventArgs e)
        {
            nudTime.Enabled = false;
            string BACKUPSOURCE = XMLTools.ReadXmlElementString(Application.StartupPath + "\\StoreProcedureInfo.xml", "BACKUPSOURCE");
            if (!BACKUPSOURCE.ISNullOrWhiteSpace())
            {
                FolderCreate.TEMPFolderCreate();//at first create temp folder Its must be importent

                Backup_RestoreTools.ComputerStartedBackup(BACKUPSOURCE);
                string APPPath = Application.StartupPath + "\\TEMP";
                CreateShortcutTools.CreateStartupShortcut(APPPath, APPPath + "\\DATABASE_BACKUP.bat");
                MessageBox.Show("PROCESS COMPLETE");
            }
            else
            {
                MessageBox.Show("Click 'OK' and go to Update Your Setting --->");
                btnSetting_Click(null, null);
            }
        }

        private void BTNSAVE_Click(object sender, EventArgs e)
        {
            if (!txtSource.Text.ISNullOrWhiteSpace())
            {
                XMLTools.CreateStoreProcedureInfoXml(txtSource.Text, nudTime.Value.ToString()+":"+nudMinutTime.Value.ToString());
                MessageBox.Show("PROCESS COMPLETE");

                ///Reopen from
                SoftwareBackupSystem.ActiveForm.Dispose();
                SoftwareBackupSystem SSystem = new SoftwareBackupSystem();
                SSystem.ShowDialog();
            }
            else
            {
                MessageBox.Show("PLEASE BROWSE YOUR LOCATION");
            }
        }

        private bool Islogin(string userName, string password)
        {
            string qurey = "select UserName from UserControl where UserName='" + userName + "' and Password ='" + password + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(qurey, out msg);
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        private void btnAutoBackup_MouseHover(object sender, EventArgs e)
        {
            btnStatus.Visible = true;
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            if (File.Exists(startupFolder + "\\DATABASE_BACKUP.lnk"))
            {
                btnStatus.Text = "ACTIVATED";
            }
            else
            {
                btnStatus.Text = "NOT ACTIVATED";
            }
        }
        private void btnAutoBackup_MouseLeave(object sender, EventArgs e)
        {
            btnStatus.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
