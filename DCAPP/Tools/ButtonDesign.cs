using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    public static class ButtonDesign
    {
        public static Button btnMenuObject;

        public static void SetMenuButtonDesign(ref object sender)
        {
            Button btn = (Button)sender;
            if (btnMenuObject != btn)
            {
                btnMenuObject.BackgroundImage = null;
                btnMenuObject.FlatAppearance.BorderSize = 1;
                btnMenuObject.FlatAppearance.MouseOverBackColor = Color.Transparent;
                //btnMenuObject.BackgroundImage = global::DAPRO.Properties.Resources.btnClearSelect;
                btnMenuObject.FlatStyle = FlatStyle.Standard;
                btnMenuObject.ForeColor = Color.Black; //System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                btnMenuObject.BackColor = Color.Transparent;
                // btnObject.FlatAppearance.BorderColor = Color.DimGray;
                // btnObject.Image = null;
               // btn.BackgroundImage = global::DAPRO.Properties.Resources.btnSelect;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                // btn.FlatAppearance.BorderColor = Color.PaleTurquoise;
                //btnMenuObject.Image = global::DAPRO.Properties.Resources.btnClearSelect;
                // btn.ImageAlign = ContentAlignment.MiddleRight;
                btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
                //btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(162)))), ((int)(((byte)(91)))));
                btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
                btn.ForeColor = Color.Maroon; //System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                btnMenuObject = btn;
            }
        }

        public static void Disabl(this Button btn)
        {
            btn.BackColor = Color.WhiteSmoke;
            btn.ForeColor = Color.Gray;
            btn.Enabled = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackgroundImage = null;
        }

        public static void Enabl(this Button btn)
        {
            btn.ForeColor = Color.Magenta;
            btn.Enabled = true;
            btn.FlatStyle = FlatStyle.Standard;
            //btn.BackgroundImage = global::DAPRO.Properties.Resources.btnClearSelect;
        }
        public static void lblEnabl(this Label lbl)
        {
            lbl.ForeColor = Color.Black;
            lbl.Enabled = true;
        }
        public static void lblDisabl(this Label lbl)
        {
            lbl.ForeColor = Color.Gray;
            lbl.Enabled = false;
            
        }
    }
}
