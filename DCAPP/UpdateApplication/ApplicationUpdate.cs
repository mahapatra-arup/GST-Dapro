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
using System.Threading;

using System.Windows.Forms;

namespace DAPRO
{
    public partial class ApplicationUpdate : Form
    {
        string CurrentAssemblyTitle, CurrentAssemblyDescription, CurrentAssemblyConfiguration, CurrentAssemblyCompany,
        CurrentAssemblyProduct, CurrentAssemblyCopyright, CurrentAssemblyTrademark, CurrentAssemblyCulture, CurrentAssemblyVersion,
        CurrentAssemblyFileVersion;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUpdateSource.Text = txtUpdateSource.Text.Trim();
            mCurrentApplicationLocation = Application.StartupPath.ToString();

           
            GetCurrentAppAssemblyDetails();
            GetUpdateAppAssemblyDetails();

            UpdateVersionCheck();
        }

        string UpdateAssemblyTitle,UpdateAssemblyFileVersion;

        string mCurrentApplicationLocation = "";
        public ApplicationUpdate()
        {
            InitializeComponent();
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.Image = appIcon.ToBitmap();

            //enter system directory;
            txtUpdateSource.Text = Path.GetPathRoot(Environment.SystemDirectory) + "\\Release";
            mCurrentApplicationLocation = Application.StartupPath.ToString();

            GetCurrentAppAssemblyDetails();
            GetUpdateAppAssemblyDetails();

            UpdateVersionCheck();
        }
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure You update your application ?","Ask", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                UpdateApp.RunCmd(CurrentAssemblyProduct, txtUpdateSource.Text.Trim(), mCurrentApplicationLocation,ref pnl);
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                txtUpdateSource.Text = fb.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetCurrentAppAssemblyDetails()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                CurrentAssemblyTitle = assembly.GetName().Name;
                CurrentAssemblyVersion = assembly.GetName().Version.ToString();
                
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                CurrentAssemblyCompany = fvi.CompanyName;
                CurrentAssemblyTrademark = fvi.LegalTrademarks;
                CurrentAssemblyFileVersion = fvi.FileVersion;
                CurrentAssemblyCopyright = fvi.LegalCopyright;
                CurrentAssemblyProduct=fvi.ProductName;
                CurrentAssemblyDescription = fvi.FileDescription;


                rtxtCurrentAppDetails.Text = CurrentAssemblyTitle + "\n\n" + "Version :" +CurrentAssemblyVersion +"\n"
                                               + "Description :" + CurrentAssemblyDescription;

                lblCopyright.Text = CurrentAssemblyCopyright;
            }
            catch (Exception e)
            {
                pnl.BackColor = Color.Red;
                MessageBox.Show("Current Application " + e.Message);
            }
        }
        private void GetUpdateAppAssemblyDetails()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(txtUpdateSource.Text.Trim() + "\\"+CurrentAssemblyProduct+".exe");
                UpdateAssemblyFileVersion = fvi.FileVersion;
                UpdateAssemblyTitle = fvi.ProductName;

                rtxtUpdateAppDetails.Text = UpdateAssemblyTitle + "\n\n" + "Version :" + UpdateAssemblyFileVersion;
            }
            catch (Exception e)
            {
                pnl.BackColor = Color.Red;
                MessageBox.Show("Update Application Source Problem : " + e.Message);
            }
        }

        private void UpdateVersionCheck()
        {
            try
            {
                var version1 = new Version(CurrentAssemblyVersion);
                var version2 = new Version(UpdateAssemblyFileVersion);

                var result = version1.CompareTo(version2);

                if (result > 0)
                {
                    lblMsgUpdate.Text = "Update are Not Available,";
                    lblMsgUpdate.ForeColor = Color.Yellow;
                    pnl.BackColor = Color.Red;
                    btnInstall.Enabled = false;


                }
                else if (result < 0)
                {
                    lblMsgUpdate.Text = "Update Are Available Please \n install Update Version";
                    lblMsgUpdate.ForeColor = Color.Green;
                    pnl.BackColor = Color.Green;
                    btnInstall.Enabled = true;
                }
                else
                {
                    lblMsgUpdate.Text = "Solve some error Please install \n Update Version";
                    lblMsgUpdate.ForeColor = Color.Green;
                    pnl.BackColor = Color.Green;
                    btnInstall.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
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
    }
}
