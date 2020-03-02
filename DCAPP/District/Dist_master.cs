using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class Dist_master : Form
    {
        public event Action<string> onclose;

        string msg = "";
        public Dist_master(string state)
        {
            InitializeComponent();
            cmbState.AddState();
            cmbState.Text = state;
        }
        private void addState_Click(object sender, EventArgs e)
        {
            State_master state = new State_master();
            state.onclose += new Action<string>(state_onclose);
            state.ShowDialog();
        }
        void state_onclose(string state)
        {
            cmbState.AddState();
            cmbState.Text = state;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDist.Text = "";
            cmbState.SelectedIndex = -1;
        }
        private bool IsValidData()
        {
            if (cmbState.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please select state ", "District", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbState.Focus();
                return false;
            }
            if (txtDist.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Please enter district", "District", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDist.Focus();
                return false;
            }
            return true;
        }
        private void SaveDist()
        {
            string distname = txtDist.Text.GetDBFormatString();
            string state_id = ((KeyValuePair<string, string>)cmbState.SelectedItem).Key.ToString();
            string query = "Insert into District(StateId,DistName) values('" + state_id + "','" + distname + "')";
            if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
            {
                DistTool.GetDist();
                this.Close();
            }
            else
            {
                MessageBox.Show("District not save ", "District", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetStateId()
        {
            string query = "select ID from State where StateName='" + cmbState.Text + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                string State_id = obj.ToString();
                return State_id;
            }
            else
                return string.Empty;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                string distname = txtDist.Text.GetDBFormatString();
                string query = "Select DistName from District where DistName='" + distname + "'";
                object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
                if (!o.ISValidObject())
                {
                    SaveDist();
                }
                else
                {
                    MessageBox.Show("Duplicate Entry ", "Dublicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Dist_master_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (onclose != null)
            {
                onclose(txtDist.Text);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string CapitalizeFirstLetters(string sValue)
        {
            char[] array = sValue.ToCharArray();
            // handle the first letter in the string
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            // scan through the letters, checking for spaces
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }

            return new string(array);
        }
        private void txtDist_TextChanged(object sender, EventArgs e)
        {
            txtDist.Text = CapitalizeFirstLetters(txtDist.Text);
            txtDist.SelectionStart = txtDist.Text.Length;
        }
    }
}
