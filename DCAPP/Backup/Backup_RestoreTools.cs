using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DAPRO
{
    class Backup_RestoreTools
    {
        #region STORE_PROCEDURE
        private static string mFolderPath = Application.StartupPath + "\\STORE_PROCEDURE\\";

        public void BAT_FILE_CREATE_AND_RUN_CMD(string BackupStorePath, string timeOfBackup)
        {
            //New Folder Create
            NewFolderCreate();
            //string strPath = Application.StartupPath + "\\STORE_PROCEDURE\\";
            string Zippath = "rar a -ep " + BackupStorePath + "\\DB_BACKUP\\" + SQLHelper.mInitalCatalog + ".Zip " + BackupStorePath + "\\DB_BACKUP\\" + SQLHelper.mInitalCatalog + ".bak\r\n";
            #region ALL_DATABASE_BACKUP str
            string ALL_DATABASE_BACKUP = AccessPermission.CMDAccessPermission() +
            "@echo==================MAKE DIIRECTORY============================================\r\n" +
            "IF NOT EXIST " + BackupStorePath + "\\DB_BACKUP (\r\n" +
            "MD " + BackupStorePath + "\\DB_BACKUP\r\n" +
            "@ECHO FOLDER CREATE SUCCESSFULL.....................\r\n" +
            ")\r\n" +
            
            "echo=============BACKUP DATABASE=======================\r\n" +
            "set  DATABASENAME=" + SQLHelper.mInitalCatalog + "\r\n" +
            ":: filename format Name-Date \r\n" +
            "set BACKUPFILENAME=" + BackupStorePath + "\\DB_BACKUP\\" + SQLHelper.mInitalCatalog + ".bak \r\n" +
            "set SERVERNAME=" + SQLHelper.mDataSource + "\r\n" +
            "sqlcmd  -S %SERVERNAME%  -d %DATABASENAME% -Q \"BACKUP DATABASE [%DATABASENAME%] TO DISK = N'%BACKUPFILENAME%' WITH INIT , NOUNLOAD , NAME = N'%DATABASENAME% backup', NOSKIP , STATS = 10, NOFORMAT\"\r\n" +
            "echo .\r\n" +

            //CONVERT ZIP FILE____________________________________________________________
            "IF EXIST %ProgramFiles%\\WinRAR(go to ProgramFiles)\r\n" +
            "IF EXIST C:\\Program Files (x86)\\WinRAR(go to ProgramFiles86)\r\n" +
            ":ProgramFiles\r\n" +
            "set path=%ProgramFiles%\\WinRAR\r\n" +
            Zippath +
            ":ProgramFiles86\r\n" +
            "set path=C:\\Program Files (x86)\\WinRAR\r\n" + Zippath;

            #endregion

            #region SQL_AUTO_BACKUP str
            string SQL_AUTO_BACKUP = AccessPermission.CMDAccessPermission() +
            "@echo==================SQL_BACKUP_AUTORUN USE TASK SHEDULER===========================================\r\n" +
            "SCHTASKS.EXE /CREATE /SC DAILY /MO 1 /TN \"SQL_AUTOMATIC_BACKUP\" /ST " + timeOfBackup + ":00 /SD 01/01/2000 /TR '%CD%\\ALL_DATABASE_BACKUP.bat' /RU SYSTEM /F\r\n";

            #endregion

            //create Task_Shedule Setting bat file
            WriteBat(SQL_AUTO_BACKUP, "SQL_AUTO_BACKUP.BAT");
            // Create run sql ,make directory,backup database 
            WriteBat(ALL_DATABASE_BACKUP, "ALL_DATABASE_BACKUP.bat");
            //run Task_Shedule Code
            RunCmd("SQL_AUTO_BACKUP.BAT");
        }

        private static void RunCmd(string Runbat)
        {
            try
            {
                Thread a = new Thread(new ThreadStart(() => Process.Start(mFolderPath + Runbat)));
                a.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Alart : " + e.Message);
            }

        }

        private static void WriteBat(string str, string strFileName)
        {
            try
            {
                if (System.IO.File.Exists(mFolderPath + strFileName))
                {
                    System.IO.File.Delete(mFolderPath + strFileName);
                }
                System.IO.File.AppendAllText(mFolderPath + strFileName, str);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Alart : Bat file Are not generate Because : " + ex.Message);
            }
        }

        public static void NewFolderCreate()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string name = "\\STORE_PROCEDURE";
            try
            {
                if (!Directory.Exists(path + name))
                {
                    Directory.CreateDirectory(path + name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }

        }
        #endregion

        public static void ComputerStartedBackup(string BackupStorePath)
        {

            string strPath = Application.StartupPath + "\\TEMP\\";
            string Zippath ="rar a -ep " + BackupStorePath + "\\" + SQLHelper.mInitalCatalog + ".Zip " + BackupStorePath + "\\" + SQLHelper.mInitalCatalog + ".bak\r\n";
            #region DATABASE_BACKUP str
            string DATABASE_BACKUP = //AccessPermission.CMDAccessPermission() +
            "echo=============BACKUP DATABASE=======================\r\n" +
            "set  DATABASENAME=" + SQLHelper.mInitalCatalog + "\r\n" +
            ":: filename format Name-Date \r\n" +
            "set BACKUPFILENAME=" + BackupStorePath + "\\" + SQLHelper.mInitalCatalog + ".bak \r\n" +
            "set SERVERNAME=" + SQLHelper.mDataSource + "\r\n" +
            "sqlcmd  -S %SERVERNAME%  -d %DATABASENAME% -Q \"BACKUP DATABASE [%DATABASENAME%] TO DISK = N'%BACKUPFILENAME%' WITH INIT , NOUNLOAD , NAME = N'%DATABASENAME% backup', NOSKIP , STATS = 10, NOFORMAT\"\r\n" +
            "echo .\r\n" +

            //CONVERT ZIP FILE____________________________________________________________
            "IF EXIST %ProgramFiles%\\WinRAR(go to ProgramFiles)\r\n" +
            "IF EXIST C:\\Program Files (x86)\\WinRAR(go to ProgramFiles86)\r\n" +
            ":ProgramFiles\r\n" +
            "set path=%ProgramFiles%\\WinRAR\r\n" + 
            Zippath+
            ":ProgramFiles86\r\n" +
            "set path=C:\\Program Files (x86)\\WinRAR\r\n" + Zippath;
            
            #endregion

            // Create run sql ,make directory,backup database 
            try
            {
                if (System.IO.File.Exists(strPath + "DATABASE_BACKUP.bat"))
                {
                    System.IO.File.Delete(strPath + "DATABASE_BACKUP.bat");
                }
                System.IO.File.AppendAllText(strPath + "DATABASE_BACKUP.bat", DATABASE_BACKUP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alart : Bat file Are not generate Because : " + ex.Message);
            }
        }
    }
}
