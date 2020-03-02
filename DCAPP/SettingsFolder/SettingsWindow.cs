using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnOrgSettings;
            object o = (object)btnOrgSettings;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }
        private void btnOrgSettings_Click(object sender, EventArgs e)
        {
           ApplicationDesign.SetSubButtonDesign(ref sender);

            CreateCompany frmCreateCompany = new CreateCompany(false);
            frmCreateCompany.WindowState = FormWindowState.Maximized;
            frmCreateCompany.FormBorderStyle = FormBorderStyle.None;
            frmCreateCompany.TopLevel = false;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCreateCompany);
            frmCreateCompany.Show();
        }
        private void btnInvoiceSettings_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            InvoiceSettings frmInvoiceSettings = new InvoiceSettings();
            frmInvoiceSettings.WindowState = FormWindowState.Maximized;
            frmInvoiceSettings.FormBorderStyle = FormBorderStyle.None;
            frmInvoiceSettings.TopLevel = false;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmInvoiceSettings);
            frmInvoiceSettings.Show();
        }
        private void btnOthersSeting_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            OtherSettings frmothersSettings = new OtherSettings();
            frmothersSettings.WindowState = FormWindowState.Maximized;
            frmothersSettings.FormBorderStyle = FormBorderStyle.None;
            frmothersSettings.TopLevel = false;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmothersSettings);
            frmothersSettings.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            CreateCompany frmCreateCompany = new CreateCompany(false);
            frmCreateCompany.WindowState = FormWindowState.Maximized;
            frmCreateCompany.FormBorderStyle = FormBorderStyle.None;
            frmCreateCompany.TopLevel = false;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCreateCompany);
            frmCreateCompany.Show();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            ApplicationUpdate frmothersSettings = new ApplicationUpdate();
            frmothersSettings.ShowDialog();
        }
        private void btnBachupRestore_Click(object sender, EventArgs e)
        {
            SoftwareBackupSystem frmBackup = new SoftwareBackupSystem();
            frmBackup.ShowDialog();
        }

        private void btnCreatecompany_Click(object sender, EventArgs e)
        {
            CompanyGenerate createcompany = new CompanyGenerate();
            createcompany.ShowDialog();
        }
    }
}
