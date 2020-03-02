using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    static class ApplicationDesign
    {
        public static Button btnObject;
        public static Button btnMenuObject;
        public static Button btnSubMenuObject;
        public static Button btnSubSubMenuObject;

        public static Size appSize;
        public static Point appLoc;

        public static Color _FormBCColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(192)))), ((int)(((byte)(54)))));
        public static void SetAppSize(ref object thisObj)
        {
            Form frm = (Form)thisObj;
            frm.Size = appSize;
        }
        public static void SetButtonDesign(ref object sender)
        {
            Button btn = (Button)sender;
            if (btnObject != null && btnObject != btn)
            {
                //btnObject.BackgroundImage = null;
                //btn.FlatAppearance.BorderSize = 0;
                //btnObject.FlatAppearance.MouseOverBackColor = Color.Transparent;
                //  btnObject.ForeColor = Color.White; //System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                btnObject.BackColor = Color.Transparent;
                // btnObject.FlatAppearance.BorderColor = Color.DimGray;
                // btnObject.Image = null;
                // btn.BackgroundImage = global::ELibrary.Properties.Resources.Untitled1;
                // btn.FlatAppearance.BorderSize = 1;
                // btn.FlatAppearance.BorderColor = Color.PaleTurquoise;
                //btn.Image = global::Nenos.Properties.Resources.oie_transparent1;
                // btn.ImageAlign = ContentAlignment.MiddleRight;
                // btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
                //btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(162)))), ((int)(((byte)(91)))));
                btn.BackColor = Color.CornflowerBlue;
                btnObject = btn;
            }
        }
        public static void SetSubButtonDesign(ref object sender)
        {
            Button btn = (Button)sender;
            if (btnSubMenuObject != btn)
            {
                //btnObject.BackgroundImage = null;
                //btnSubMenuObject.FlatAppearance.BorderSize = 0;
                // btnSubMenuObject.Image = null;
                btnSubMenuObject.FlatAppearance.MouseOverBackColor = Color.Gray; //System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                btnSubMenuObject.ForeColor = Color.White;//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                btnSubMenuObject.BackColor = Color.Transparent;
                //btnSubMenuObject.FlatAppearance.BorderColor = Color.DimGray;
                // btn.BackgroundImage = global::ELibrary.Properties.Resources.Untitled1;
                //btn.FlatAppearance.BorderSize = 1;
                //btn.SendToBack();
                // btn.Image = global::Nenos.Properties.Resources.oie_transparent1;
                //btn.ImageAlign = ContentAlignment.MiddleRight;
                //  btn.FlatAppearance.BorderColor = Color.PaleTurquoise;
                // btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                btn.BackColor = Color.FromKnownColor(KnownColor.Control);//System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
                btn.ForeColor = Color.Black; //System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                btnSubMenuObject = btn;
            }
        }
        public static void SetSubSubButtonDesign(ref object sender)
        {
            Button btn = (Button)sender;
            if (btnSubSubMenuObject != btn)
            {
                //btnObject.BackgroundImage = null;
                //btnSubMenuObject.FlatAppearance.BorderSize = 0;
                // btnSubMenuObject.Image = null;
                //btnSubMenuObject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                btnSubSubMenuObject.ForeColor = Color.White;//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                btnSubSubMenuObject.BackColor = Color.Transparent;
                //btnSubMenuObject.FlatAppearance.BorderColor = Color.DimGray;
                // btn.BackgroundImage = global::ELibrary.Properties.Resources.Untitled1;
                //btn.FlatAppearance.BorderSize = 1;
                btn.SendToBack();
                // btn.Image = global::Nenos.Properties.Resources.oie_transparent1;
                //btn.ImageAlign = ContentAlignment.MiddleRight;
                //  btn.FlatAppearance.BorderColor = Color.PaleTurquoise;
                // btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                btn.BackColor = Color.WhiteSmoke; //System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(131)))), ((int)(((byte)(222)))));
                btn.ForeColor = Color.Maroon; //System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                btnSubSubMenuObject = btn;
            }
        }
        public static void FitToPopUp(this object thisObj)
        {
            Form frm = (Form)thisObj;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int width = frm.Width;
            int difWidth = screenWidth - width;
            if (difWidth < 0)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                frm.Height = screenHeight - 195;
                int x = difWidth / 2;
                frm.Location = new Point(x, 195);
            }

        }
        public static void FitToHorazonal(this object thisObj)
        {
            Form frm = (Form)thisObj;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int width = frm.Width;
            int difWidth = screenWidth - width;
            if (difWidth < 0)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                frm.Height = screenHeight - 110;
                frm.Location = new Point(0, 110);
                frm.Width = screenWidth;
            }

        }
        public static void FitToVertical(this object thisObj)
        {
            Form frm = (Form)thisObj;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int width = frm.Width;
            int difWidth = screenWidth - width;
            if (difWidth < 0)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                frm.Height = screenHeight;
                frm.Location = new Point(difWidth / 2, 0);
            }

        }
        public static void FitDownToTop(this object thisObj)
        {
            Form frm = (Form)thisObj;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int objHeight = frm.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int width = frm.Width;
            int difWidth = screenWidth - width;
            int difHeight = screenHeight - objHeight;
            if (difWidth < 0 || difHeight < 0)
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                frm.Location = new Point(difWidth / 2, difHeight);
            }

        }
        public static void FitOnDown(this object thisObj)
        {
            if (thisObj is Form)
            {
                Form frm = (Form)thisObj;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
                int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                int width = frm.Width;
                int height = frm.Height;
                int difHeight = screenHeight - height;
                int difWidth = screenWidth - width;
                if (difWidth < 0)
                {
                    frm.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    int x = difWidth / 2;
                    frm.Location = new Point(x, difHeight);
                }
            }

        }
    }
}
