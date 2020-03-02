using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    class FolderCreate
    {
        public static void TEMPFolderCreate()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string name = "\\TEMP";
            try
            {
                if (!Directory.Exists(path + name))
                {
                    Directory.CreateDirectory(path + name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Temp Folder Are Not Created Because : " + e.Message, "Folder Create");
            }
        }
    }
}
