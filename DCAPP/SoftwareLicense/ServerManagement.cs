using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;


namespace DAPRO
{
    public partial class ServerManagement : Form
    {
        public event Action<bool> OnClose;
        private bool mIsSuccess = false;

        public ServerManagement()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private string GetConnectionString()
        {
            //string mUserPass = "user id=sa;password=!dcuseronly";
            string connectionString = string.Empty;
            if (radioButton1.Checked)
            {
                connectionString = string.Concat("Data Source=" + listBox1.SelectedValue.ToString() + "; Initial Catalog=" + listBox2.SelectedItem.ToString() + "; integrated security=true");
            }
            else
            {
                connectionString = string.Concat("Data Source=" + txtServer.Text.Trim() +"; Integrated Security = " + SQLHelper.mIntegratedSecurity + "; Initial Catalog = " + txtDB.Text + "; persist security info = False; Trusted_Connection = Yes");
            }
            return connectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DataTable dataTable = SmoApplication.EnumAvailableSqlServers(false);//Local ClientServer
         //  dataTable.Rows.Add(SmoApplication.EnumAvailableSqlServers(true));//MainServer
            listBox1.ValueMember = "Name";
            listBox1.DataSource = dataTable;
            Cursor = Cursors.Default;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (SqlConnection cn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    cn.Open();
                    string query = "SELECT 1";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    object o = cmd.ExecuteScalar();
                    Cursor = Cursors.Default;
                    MessageBox.Show("Connection successfully created....", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    mIsSuccess = true;
                }
                catch (Exception ex)
                {
                    mIsSuccess = false;
                    Cursor = Cursors.Default;
                    MessageBox.Show("Error!\n" + ex.Message, "Connection not established.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            Cursor = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool updateConfigFile()
        {
            string serverName = string.Empty;
            string dbName = string.Empty;
            if (radioButton1.Checked)
            {
                serverName = listBox1.GetItemText(listBox1.SelectedItem).ToString().GetDBFormatString();
                dbName = listBox2.SelectedItem.ToString().GetDBFormatString();
            }
            else
            {
                serverName = txtServer.Text.GetDBFormatString();
                dbName = txtDB.Text.GetDBFormatString();
            }
            try
            {
                //string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DataSource"].Value = serverName.ToString();
                config.AppSettings.Settings["InitialCatalog"].Value = dbName.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sarver Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);
            if (mIsSuccess == true)
            {
                if (updateConfigFile())
                {
                    this.Close();
                    //Process.Start(Application.ExecutablePath);
                    //Application.Exit(); 
                }
            }
        }

        private void ServerManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose(mIsSuccess);
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                string serverName = listBox1.SelectedValue.ToString();
                Server server = new Server(serverName);
                try
                {
                    foreach (Database database in server.Databases)
                    {
                        listBox2.Items.Add(database.Name.ToString());
                        listBox2.SelectedItem = 0;
                    }
                }
                catch (Exception ex)
                {
                    listBox2.Items.Add("DAPRO");
                    string exception = ex.Message;
                }
            }
        }
    }
}
