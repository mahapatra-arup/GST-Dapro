using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Ionic.Zip;

namespace DAPRO
{
    class SQLHelper
    {
        public static SQLHelper mInstance;
        public static string mDataSource = string.Empty;
        public static string mInitalCatalog = string.Empty;
        public static string mUserInstance = string.Empty;
        public static string mAttachedDb = string.Empty;
        public static string mIntegratedSecurity = string.Empty;
        public static string mUserPass = "user id=sa;password=!dcuseronly";
        private static string CONNECTION_STRING = "";
        public static bool IsChangeAnyDataBase = false;
        private SQLHelper()
        {

        }
        public static SQLHelper GetInstance()
        {
            if (mInstance == null)
            {
                CONNECTION_STRING = string.Empty;
                mDataSource = System.Configuration.ConfigurationManager.AppSettings["DataSource"].ToString();
                mInitalCatalog = System.Configuration.ConfigurationManager.AppSettings["InitialCatalog"].ToString();
                mAttachedDb = System.Configuration.ConfigurationManager.AppSettings["AttachDbFilename"].ToString();
                mIntegratedSecurity = System.Configuration.ConfigurationManager.AppSettings["IntegratedSecurity"].ToString();
                mUserInstance = System.Configuration.ConfigurationManager.AppSettings["UserInstance"].ToString();

                //CONNECTION_STRING = "Data Source=" + mDataSource + ";Initial Catalog=" + mInitalCatalog + ";" + mUserPass;
                //CONNECTION_STRING = "Data Source=" + mDataSource + "; Initial Catalog=" + mInitalCatalog + "; integrated security=" + mIntegratedSecurity + "; persist security info = False; Trusted_Connection = Yes";
                CONNECTION_STRING = "Data Source=" + mDataSource + "; Integrated Security = " + mIntegratedSecurity + "; User Instance=" + mUserInstance + "; Initial Catalog=" + mInitalCatalog + "; persist security info = False; Trusted_Connection = Yes";
                mInstance = new SQLHelper();
            }
            return mInstance;
        }
        public bool ExcuteQuery(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    IsChangeAnyDataBase = true;
                    return true;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    // DebugTracer.Instance.WriteLog(ex.ToString());
                    return false;
                }
            }
        }
        public bool ExcuteQuery(string query, string value, byte[] b, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlParameter p = new SqlParameter(value, SqlDbType.Image);
                    p.Value = b;
                    cmd.Parameters.Add(p);
                    cmd.ExecuteNonQuery();
                    IsChangeAnyDataBase = true;
                    return true;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    // DebugTracer.Instance.WriteLog(ex.ToString());
                    return false;
                }
            }
        }
        public object ExcuteScalar(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    //DebugTracer.Instance.WriteLog(ex.ToString());
                    return null;
                }
            }
        }
        public DataTable ExcuteNonQuery(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    SqlDataAdapter sqlAdptr = new SqlDataAdapter(query, con);

                    DataTable dataTable = new DataTable();

                    sqlAdptr.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    return null;
                }
            }
        }
        public bool ExcuteNonQuery(string query, ref DataTable dt)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    SqlDataAdapter sqlAdptr = new SqlDataAdapter(query, con);

                    sqlAdptr.Fill(dt);
                    return true;

                }
                catch (Exception)
                {
                    //DebugTracer.Instance.WriteLog(ex.ToString());
                    return false;
                }
            }
        }
        public byte[] ExcuteQueryAndGetImageData(string query, out string msg)
        {
            msg = "";
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(query, con));
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.IsValidDataTable())
                    {
                        Byte[] data = new Byte[0];
                        data = (Byte[])(dt.Rows[0][0]);
                        return data;
                    }
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    // DebugTracer.Instance.WriteLog(ex.ToString());                   
                }
            }
            return null;
        }
        public bool ExecuteTransection(List<string> lstQuery, out string msg)
        {
            msg = "";
            if (lstQuery.Count > 0)
            {
                int index = 0;
                string quer = "";
                using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        SqlTransaction trans;
                        trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Connection = con;
                        cmd.Transaction = trans;

                        int isExecute = 0;
                        foreach (var item in lstQuery)
                        {
                            cmd.CommandText = item.ToString();
                            quer = item.ToString();
                            index++;
                            isExecute = cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        IsChangeAnyDataBase = true;
                        return true;
                    }
                    catch (Exception e)
                    {
                        msg = e.Message;
                        MessageBox.Show(index + " :" + e.Message + "\n" + quer);
                    }
                }
            }

            return false;
        }

        #region Restore_Backup_Achive_UnAchiveTools
        public static bool GetNewBackUp(string location)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    string fileName = mInitalCatalog + "-" + DateTime.Now.ToString("ddMMMyyyyhhmmsstt").GetDBFormatString() + ".bak";
                    string query = "Backup database " + mInitalCatalog + " to Disk='" + location + fileName + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    Thread.Sleep(10);
                    CreateZip(location, fileName);
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool GetRestore(string fulPath)
        {
            string backupPath = "";
            string conStr = "Data Source=" + mDataSource + "; Integrated Security = " + mIntegratedSecurity + "; User Instance = " + mUserInstance + "";
            try
            {
                string directoryPath = Path.GetDirectoryName(fulPath);
                string rootDir = Path.GetPathRoot(directoryPath);
                string fileName = Path.GetFileName(fulPath);
                if (directoryPath.Equals(rootDir))
                {
                    backupPath = (directoryPath + fileName.Substring(0, fileName.Length - 4) + ".bak").GetDBFormatString();
                }
                else
                    backupPath = (directoryPath + "\\" + fileName.Substring(0, fileName.Length - 4) + ".bak").GetDBFormatString();
                using (ZipFile zip = ZipFile.Read(fulPath))
                {
                    //zip.Password = "!jai.ho^95.(_)";
                    zip.ExtractAll(directoryPath);
                }
                Thread.Sleep(10);
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = "Alter database " + mInitalCatalog + " set Single_User with rollback immediate;";
                    query += "Restore database " + mInitalCatalog + " from Disk='" + backupPath + "' With Replace;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    query = "Alter database " + mInitalCatalog + " set MULTI_USER with rollback immediate;";
                    cmd.ExecuteNonQuery();
                    File.Delete(backupPath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = "Alter database " + mInitalCatalog + " set MULTI_USER;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
                File.Delete(backupPath);
                string errMsg = ex.Message;
                MessageBox.Show(errMsg, "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private static void CreateZip(string location, string fileName)
        {
            try
            {
                string pathName = location + fileName.Substring(0, fileName.Length - 4);
                string filePath = (location + fileName).GetDBFormatString();
                using (ZipFile zip = new ZipFile())
                {
                    //zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    ////zip.Password = "!jai.ho^95.(_)";
                    zip.AddFile(filePath);
                    zip.Save(pathName + ".zip");
                }
                File.Delete(filePath);
            }
            catch (System.Exception ex1)
            {
                System.Console.Error.WriteLine("exception: " + ex1);
            }
        }
        #endregion
    }
}
