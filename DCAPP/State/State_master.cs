using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class State_master : Form
    {
        string msg = "";
        public event Action<string> onclose;

        public State_master()
        {
            InitializeComponent();
            
            

        }

        private void txtState_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (!(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == 40 || e.KeyChar == 41 || e.KeyChar == 44 || e.KeyChar == 45 || e.KeyChar == 46))
            {
                MessageBox.Show("Please Enter Charecters only", "Charecters Only", MessageBoxButtons.OK, MessageBoxIcon.Information);

                e.Handled = true;
            }
            else
            {
                e.Handled = false;

            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string stateName = txtState.Text.GetDBFormatString();
            string query = "select * from State where StateName='" + stateName + "'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);

            if (dt.Rows.Count <= 0)
            {
                submit();

            }
            else
                MessageBox.Show("Duplicate Entry ", "Dublicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       

        private void State_master_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (onclose != null)
            {
                onclose(txtState.Text);
            }
        }
        private void submit()
        {
            string statename = txtState.Text.GetDBFormatString();
            string query = " Insert into State(StateName)values('" + statename + "') ";
            if (txtState.Text != "")
            {
                DialogResult dialog = MessageBox.Show("Do you Want To Save? ", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    bool bl = SQLHelper.GetInstance().ExcuteQuery(query, out msg);
                    if (bl == true)
                    {

                        //MessageBox.Show("your Data is Save Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show("your Data is Not Save ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                    MessageBox.Show("your Data is Not Save ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


            }

            else
            {
                MessageBox.Show("Please Give State Name ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtState.Focus();
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
        private void txtState_TextChanged(object sender, EventArgs e)
        {
            txtState.Text = CapitalizeFirstLetters(txtState.Text);
            txtState.SelectionStart = txtState.Text.Length;
        }

        

        

    //    private void btnEdit_Click(object sender, EventArgs e)
    //    {
    //        string stateid="";
    //        string query = "update State set StateName='" + txtState.Text + "' where ID='" + stateid + "'";
            
    //        string query2 = "select * from  State where StateName='" + txtState.Text + "'";
    //        if (txtState.Text != "")
    //        {
    //            DataTable dt = SqlHelper.ExecutNonQuery(query2);
    //            if (dt.Rows.Count > 0)
    //            {
                    
                       
    //{
		 
    //}
    //                DialogResult dialog = MessageBox.Show("Do you Want To Edit? ", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    //                if (dialog == DialogResult.Yes)
    //                {
    //                    bool bl = SqlHelper.ExecutQuery(query);
    //                    if (bl == true)
    //                    {

    //                        MessageBox.Show("your Data is Successfully Edit.. ", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);

    //                        this.Close();

    //                    }
    //                    else
    //                    {
    //                        MessageBox.Show("your Given Data Not in Record! ", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);

    //                    }
    //                }
    //                else
    //                    MessageBox.Show("your Data is Not Edit ", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


    //            }
    //            else
    //                MessageBox.Show("your Given Data Not in Record! ", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


    //        }

    //        else

    //            MessageBox.Show("Please Give State Name ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);


    //    }
    }
}
