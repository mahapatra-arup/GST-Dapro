using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

using System.Windows.Forms;

namespace DAPRO
{
   public  class UpdateApp
    {
       
        public static  void RunCmd(string CurrentAssemblyProduct, string UpdateSource, string mCurrentApplicationLocation, ref Panel pnl)
        {
            try
            {
               
                string str = AccessPermission.CMDAccessPermission()+ "@echo Automatic Stop your Application..................\r\n" +
                // "TASKKILL /F /IM " + CurrentAssemblyProduct + ".exe /T \r\n " +
                 "@echo Please wait Your process is running...........................\r\n" +
                 "XCopy /Y \"" + UpdateSource + "\" \"" + mCurrentApplicationLocation + "\"\r\n" +
                 "RD /S /Q \"" + UpdateSource + "\"\r\n"+
                 "\""+Application.ExecutablePath+"\"";

                string batPath=WriteBat(str,  mCurrentApplicationLocation);
                 Thread a = new Thread(new ThreadStart(()=> Process.Start(batPath)));
                Application.Exit();
                a.Start();
                //Process.GetCurrentProcess().Kill();
            }
            catch (Exception e)
            {
                pnl.BackColor = Color.Red;
                MessageBox.Show("Alart : " + e.Message);
            }

        }

        private static string WriteBat(string str, string mCurrentApplicationLocation)
        {
            string strFileName = "UpdateApplication.bat";
            //path is tem location
            string strPath = Path.GetTempPath();//mCurrentApplicationLocation+"\\";
            try
            {
                if (System.IO.File.Exists(strPath + "\\" + strFileName))
                {
                    System.IO.File.Delete(strPath + "\\" + strFileName);
                }
                //WRITE THE ERROR TEXT AND THE CURRENT DATE-TIME TO THE ERROR FILE
                System.IO.File.AppendAllText(strPath  + strFileName, str);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alart : Bat file Are not generate Because : "+ex.Message);
            }
            return strPath + strFileName;
        }
    }
}
