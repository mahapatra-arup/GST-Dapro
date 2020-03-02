using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.SoftwareLicense
{
   
    public partial class OrgDetailsWindow : Form
    {
        public event Action<string, string, string> OnClose;
        string mKey = string.Empty;
        public OrgDetailsWindow(string key)
        {
            InitializeComponent();
            mKey = key;
                        Design();
            cmbEmailExe.Text = "gmail";
        }

        private void Design()
        {
            //get app icon and show pblogo;
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            pbLogo.Image = appIcon.ToBitmap();
            //application details
            Assembly assembly = Assembly.GetExecutingAssembly();
            lblAppsname.Text = assembly.GetName().Name;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
           // lblCompanyMName.Text = fvi.CompanyName;
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

        #endregion
        private void OrgDetailsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose!=null)
            {
                string mail = (txtEmail.Text + lbl1.Text + cmbEmailExe.Text + lbl2.Text).Trim().ToLower();
                OnClose(txtorgName.Text, txtContectNo.Text, mail);
            }
        }
       
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!txtEmail.Text.ISNullOrWhiteSpace() &&!cmbEmailExe.Text.ISNullOrWhiteSpace())
            {
                this.Close(); 
            }
            else
            {
                txtEmail.Focus();
                toolTip1.Show("Sorry! Invalid E-MAIL", txtEmail, 10, txtEmail.Height-300, 1000);
            }
        }

        private void txtContectNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {

                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtContectNo_TextChanged(object sender, EventArgs e)
        {
            txtContectNo.ForeColor = Color.Red;
            if (!txtContectNo.Text.ISNullOrWhiteSpace())
            {
                if (txtContectNo.Text.Length == 10 || txtContectNo.Text.Length == 12)
                {
                    txtContectNo.ForeColor = Color.Black;
                }
            }
        }
    }
}
