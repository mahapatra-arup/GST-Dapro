using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DAPRO
{
    public static class CreateCompanyTools
    {
        #region Company Create use CMD
        //**************************************************************************
        //Name :CreateCompanyUtils
        //Description :describe ".sql" file and ".bat" file.
        //***************************************************************************
        public static void CreateCompanyUtils(string databasebam, string bakFileName, List<string> Otherquerylist)
        {
            FolderCreate.TEMPFolderCreate();
            string StartupPath = Application.StartupPath;

            string strPath = Application.StartupPath + "\\TEMP\\";
            #region sql str
            string sqlsb = "/****** Object:  Database [DAPRO] ON  PRIMARY ****/\r\n" +
            "---------------Alrady exist check-------------------------\r\n" +
            " IF (EXISTS (SELECT * FROM sys.databases where name='" + databasebam + "'))\r\n" +
            "       RETURN\r\n" +
            "       else	\r\n" +
            "   \r\n" +
            "---------------else part Alrady not exist check then run -------------------------  \r\n" +
            " \r\n" +
            "CREATE DATABASE [" + databasebam + "] ON  PRIMARY \r\n" +
            "( NAME = N'IMAGINE', FILENAME = N'" + StartupPath + "\\" + databasebam + ".mdf' , SIZE = 13312KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )\r\n" +
            " LOG ON \r\n" +
            "( NAME = N'IMAGINE_log', FILENAME = N'" + StartupPath + "\\" + databasebam + "_log.LDF' , SIZE = 504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)\r\n" +
            "---------------restore -------------------------  \r\n" +
            "RESTORE database " + databasebam + " \r\n" +
            "FROM  DISK=N'" + StartupPath + "\\" + bakFileName + "'\r\n" +
            " WITH  REPLACE, RECOVERY\r\n" +
            //********************Add other Query*********************************
            listQueryToStringConvert(Otherquerylist);
            #endregion

            #region .bat str
            string cmdystr = AccessPermission.CMDAccessPermission() +
            "echo============CLEAR====================================================\r\n" +
            "echo off\r\n" +
            "cls\r\n" +
            "color B0\r\n" +
            "@echo==================MAKE DB============================================\r\n" +
            "sqlcmd -S " + SQLHelper.mDataSource + " -i %CD%\\CreateDBScript.sql";


            #endregion
            //create Task_Shedule Setting bat file
            WriteBat(strPath, sqlsb, "CreateDBScript.sql");
            // Create run sql ,make directory,backup database 
            WriteBat(strPath, cmdystr, "CreateDBWithCMD.bat");
            RunCmd(strPath + "CreateDBWithCMD.bat");
        }

        //**************************************************************************
        //Name :listQueryToStringConvert
        //Description :list Query To String Convert with <\r\n>.
        //**************************************************************************
        private static string listQueryToStringConvert(List<string> Otherquerylist)
        {
            string strqry = "";
            if (Otherquerylist != null)
            {
                foreach (string item in Otherquerylist)
                {
                    strqry += item + " \r\n";
                }
            }
            return strqry;
        }

        //**************************************************************************
        //Name :RunCmd
        //Description :Run cmd use thread
        //***************************************************************************
        private static void RunCmd(string folderPathWithBatfile)
        {
            Process process = new Process();
            try
            {
                //Thread a = new Thread(new ThreadStart(() => Process.Start(folderPathWithBatfile)));
                //// a.IsBackground = true;
                //a.Start();
                //Thread.Sleep(10000);
                process = Process.Start(folderPathWithBatfile);
                //If The Child Thread Is RunnIng Then The Main Thread Us Wait For Exist Chil Thread
                while ((System.Diagnostics.Process.GetProcessesByName(process.ProcessName).Length > 0))
                {
                    Thread.Sleep(10000);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Alart : " + e.Message);
            }

        }
        //**************************************************************************
        //Name :WriteBat
        //Description :Create ".bat" file
        //***************************************************************************
        private static void WriteBat(string folderPath, string str, string strFileName)
        {
            try
            {
                if (System.IO.File.Exists(folderPath + strFileName))
                {
                    System.IO.File.Delete(folderPath + strFileName);
                }
                System.IO.File.AppendAllText(folderPath + strFileName, str);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Alart : Bat file Are not generate Because : " + ex.Message);
            }
        }
        #endregion

        #region Company Create Simple use C# code
        public static bool CreateCompanyDB(string databasebam)
        {
            string CONN_STR = "Data Source=.\\SQLEXPRESS; Integrated Security =true";
            string query = /****** Object:  Database [DAPRO] ON  PRIMARY ****/
                           //---------------Alrady exist check-------------------------
            "IF(EXISTS(SELECT * FROM sys.databases where name = '" + databasebam + "')) " +
            "RETURN " +
            "else	 " +

            //---------------else part Alrady not exist check then run------------------------ -

            "CREATE DATABASE[" + databasebam + "] ON PRIMARY " +
            "(NAME = N'IMAGINE', FILENAME = N'" + Application.StartupPath + "\\" + databasebam + ".mdf', SIZE = 13312KB, MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) " +
            "LOG ON  " +
            "(NAME = N'IMAGINE_log', FILENAME = N'" + Application.StartupPath + "\\" + databasebam + "_log.LDF', SIZE = 504KB, MAXSIZE = UNLIMITED, FILEGROWTH = 10 %) " +
            //---------------restore------------------------ -
            "RESTORE database " + databasebam + " " +
            "FROM DISK = N'" + Application.StartupPath + "\\dapro.bak' " +
            "WITH REPLACE, RECOVERY ";
            using (SqlConnection con = new SqlConnection(CONN_STR))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteScalar();
                    return true;
                }
                catch (Exception e)
                {
                   string msg = e.Message;
                    MessageBox.Show(msg);
                    return false;
                }
            }
            return false;
        } 
        #endregion
    }
}
