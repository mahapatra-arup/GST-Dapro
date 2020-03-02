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

using System.Threading;

namespace DAPRO
{
    class AzureSQLHelper
    {
        public static AzureSQLHelper mInstance;
        public static string mDataSource = string.Empty;
        public static string mInitalCatalog = string.Empty;
        public static string mUserPass = " User ID =drycode; Password =D!dcuseronly; ";
        private static string CONNECTION_STRING = "";
        private static List<SqlParameter> mParameterList = new List<SqlParameter>();


        private AzureSQLHelper()
        {

        }

        public static AzureSQLHelper GetInstance()
        {
            if (mInstance == null)
            {
                CONNECTION_STRING = string.Empty;
                CONNECTION_STRING = "Server =tcp:dtl.database.windows.net,1433; Initial Catalog = DC_DAPRO; " +
                                    "Persist Security Info = False;" + mUserPass + "MultipleActiveResultSets = False; Encrypt = True; " +
                                    "TrustServerCertificate = False; Connection Timeout = 30";
                mInstance = new AzureSQLHelper();
                if (!CheckInternetconnection.CheckForInternetConnection())
                {
                    MessageBox.Show("Internet connection not available.", "Please check your internet connection.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return mInstance;
        }
        public bool ExcuteQuery(string query)
        {
            Array parameterList = mParameterList.ToArray();
            mParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    if (parameterList.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameterList);
                    }
                    cmd.ExecuteReader();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
        }

        public object ExcuteScalar(string query)
        {
            Array parameterList = mParameterList.ToArray();
            mParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    if (parameterList.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameterList);
                    }
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public DataTable ExcuteNonQuery(string query)
        {
            Array parameterList = mParameterList.ToArray();
            mParameterList.Clear();
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    SqlDataAdapter sqlAdptr = new SqlDataAdapter(query, con);
                    if (parameterList.Length > 0)
                    {
                        sqlAdptr.SelectCommand.Parameters.AddRange(parameterList);
                    }
                    DataTable dataTable = new DataTable();
                    sqlAdptr.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    //errMsg = ex.ToString();
                    return null;
                }
            }
        }

        public bool ExecuteTransection(List<string> lstQuery, List<List<SqlParameter>> sqlparam)
        {
            if (lstQuery.Count > 0)
            {
                int index = 0;

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

                            if (sqlparam != null)
                            {
                                //clear parameter then add range
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(sqlparam[index].ToArray());
                            }
                            index++;
                            isExecute = cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(index + " :" + e.Message);
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Add sql paramiter by call this method
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="ParameterValue"></param>
        public static void SetParamiterWithValue(string parameterName, string ParameterValue)
        {
            mParameterList.Add(new SqlParameter("@" + parameterName + "", ParameterValue));
        }

    }
}
