using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.User
{
    public partial class UserWindow : Form
    {
        public UserWindow()
        {
            InitializeComponent();
            ApplicationDesign.btnSubMenuObject = btnChangePw;
            object o = (object)btnChangePw;
            ApplicationDesign.SetSubButtonDesign(ref o);
        }

        private void btnChangePw_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);

            ChangePasswoerd frmChangePasswoerd = new ChangePasswoerd();
            frmChangePasswoerd.TopLevel = false;
            frmChangePasswoerd.FormBorderStyle = FormBorderStyle.None;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmChangePasswoerd);
            frmChangePasswoerd.Show();
        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);
            CreateUser frmCreateUser = new CreateUser();
            frmCreateUser.TopLevel = false;
            frmCreateUser.FormBorderStyle = FormBorderStyle.None;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmCreateUser);
            frmCreateUser.Show();
        }
        private void btnEditPermition_Click(object sender, EventArgs e)
        {
            ApplicationDesign.SetSubButtonDesign(ref sender);
            PermissionWindows permissionWindows = new PermissionWindows();
            permissionWindows.TopLevel = false;
            permissionWindows.FormBorderStyle = FormBorderStyle.None;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(permissionWindows);
            permissionWindows.Show();

        }
        private void UserWindow_Shown(object sender, EventArgs e)
        {
            if (UserTools._UserName == "Admin")
            {
                btnCreateUser.Visible = true;
                btnEditPermition.Visible = true;
            }
            else
            {
                btnCreateUser.Visible = false;
                btnEditPermition.Visible = false;
            }
            ChangePasswoerd frmChangePasswoerd = new ChangePasswoerd();
            frmChangePasswoerd.TopLevel = false;
            frmChangePasswoerd.FormBorderStyle = FormBorderStyle.None;

            pnlWindow.Controls.Clear();
            pnlWindow.Controls.Add(frmChangePasswoerd);
            frmChangePasswoerd.Show();
        }
    }
}
