using System;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class EmployeeWindow : Form
    {
        DataTable mdt = new DataTable();
        string msg = "";

        public EmployeeWindow()
        {
            InitializeComponent();
            InitDataTable();
            ShowList();
        }
        private void InitDataTable()
        {
            mdt.Columns.Add("LedgerId", (typeof(string)));
            mdt.Columns.Add("EmpCode", (typeof(string)));
            mdt.Columns.Add("EmpName", (typeof(string)));
            mdt.Columns.Add("Address", (typeof(string)));

            mdt.Columns.Add("ContactNo", (typeof(string)));
            mdt.Columns.Add("AlterNativeNo", (typeof(string)));
            mdt.Columns.Add("MailId", (typeof(string)));
            mdt.Columns.Add("JoiningDate", (typeof(string)));
            mdt.Columns.Add("BLoodGroup", (typeof(string)));
            mdt.Columns.Add("Gender", (typeof(string)));
            //mdt.Columns.Add("DOB", (typeof(string)));

            mdt.Columns.Add("PAN", (typeof(string)));
            mdt.Columns.Add("AccountNo", (typeof(string)));
            mdt.Columns.Add("IFSCCode", (typeof(string)));
            mdt.Columns.Add("BankName", (typeof(string)));
            mdt.Columns.Add("BranchNAme", (typeof(string)));

            grd.DataSource = mdt;
            //grd.DataBind();
            grd.Columns["LedgerId"].Visible = false;
            grd.Columns["EmpCode"].HeaderText = "Employee Code";
            grd.Columns["EmpName"].HeaderText = "Name";
            grd.Columns["Address"].HeaderText = "Address";
            grd.Columns["ContactNo"].HeaderText = "Contact No";
            grd.Columns["AlterNativeNo"].HeaderText = "Alternative Contact No";
            grd.Columns["MailId"].HeaderText = "Mail Id";
            grd.Columns["JoiningDate"].HeaderText = "Joining Date ";
            grd.Columns["BLoodGroup"].HeaderText = "Blood Group";
            grd.Columns["Gender"].HeaderText = "Gender";
            //grd.Columns["DOB"].HeaderText = "DOB";
            grd.Columns["PAN"].HeaderText = "PAN";
            grd.Columns["AccountNo"].HeaderText = "Account No";
            grd.Columns["IFSCCode"].HeaderText = "IFSC";
            grd.Columns["BankName"].HeaderText = "Bank";
            grd.Columns["BranchNAme"].HeaderText = "Branch";

            for (int i = 0; i < grd.Columns.Count; i++)
            {
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void ShowList()
        {
            mdt.Rows.Clear();

            string query = "select * from Ledgers,Employee where Ledgers.LedgerID=Employee.LadgerID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);

            if (dt.IsValidDataTable())
            {
                foreach (DataRow i in dt.Rows)
                {
                    string legerId = i["LadgerID"].ToString();
                    string EmpCode = i["EmpCode"].ToString();
                    string EmpName = i["LedgerName"].ToString();

                    string at = i["Address"].ToString();
                    string city = i["City_Town"].ToString();
                    string dist = i["Dist"].ToString();
                    //string stateid = GetSateName(stateid);
                    string state = i["State"].ToString();
                    string pin = i["PinCode"].ToString();

                    string Addresss = at + ", " + city + ", " + dist + ", " + state + ", " + pin;
                    string ContactNo = i["Mobile"].ToString();
                    string AlternativeContact = i["Phone"].ToString();
                    string MAil = i["Email"].ToString();
                    string JOINingDate = i["JoiningDate"].ToString();
                    try
                    {
                        DateTime join = DateTime.Parse(JOINingDate);
                        JOINingDate = join.ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {
                    }

                    string BloodGroup = i["BloodGroup"].ToString();
                    string Gender = i["Gender"].ToString();
                    //DateTime date = DateTime.Parse(i["DOB"].ToString());
                    //string  Dob = date.ToString("dd-MMM-yyyy");
                    string PAn = i["PAN"].ToString();
                    string AccountNo = i["AccountNo"].ToString();
                    string IFSC = i["IFSC"].ToString();
                    string bankId = i["BankID"].ToString();
                    string BAnkNAme = GetBankName(bankId);
                    string Branch = i["BranchName"].ToString();


                    mdt.Rows.Add(legerId, EmpCode, EmpName, Addresss, ContactNo, AlternativeContact, MAil, JOINingDate, BloodGroup, Gender,
                               PAn, AccountNo, IFSC, BAnkNAme, Branch);


                }
            }

        }
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {

        }
        private void FrmLedgerDetails_OnClose(string obj)
        {
            ShowList();
        }
        private string GetBankName(string id)
        {
            string query = "Select BankName from Bank where ID='" + id + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
        private string GetSateName(string id)
        {
            string query = "Select StateName from State where ID='" + id + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
        //private string GetDistName(string id)
        //{
        //    string query = "Select BankName from Bank where ID='" + id + "'";
        //    object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
        //    if (obj != null)
        //    {
        //        return obj.ToString();
        //    }

        //    return null ;
        //}
        private void AddNewEmployee_Click(object sender, EventArgs e)
        {
            LedgerDetails frmLedgerDetails = new LedgerDetails(LedgerDetails._LedgerCategory.Employee, LedgerDetails._Type.show);
            frmLedgerDetails.OnClose += FrmLedgerDetails_OnClose;
            frmLedgerDetails.ShowDialog();
        }
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (grd.SelectedRows.Count > 0)
            {
                string LedgerId = grd.SelectedRows[0].Cells["LedgerId"].Value.ToString();
                if (MessageBox.Show("Once record deleted can't be undo.Are you sure ?", "Delect Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string query = "delete from LadgerMain where LadgerID='" + LedgerId + "'";
                    if (SQLHelper.GetInstance().ExcuteQuery(query, out msg))
                    {
                        ShowList();
                    }
                }
            }
        }
        private void btnEditEmploye_Click(object sender, EventArgs e)
        {

        }
        private void grd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (UserTools._IsEdit)
                {
                    int rowindex = e.RowIndex;
                    string LedgerId = grd.Rows[rowindex].Cells["LedgerId"].Value.ToString();

                    LedgerDetails frmLedgerDetailsEdit = new LedgerDetails(LedgerDetails._LedgerCategory.Employee, LedgerDetails._Type.showDialog, LedgerId);
                    frmLedgerDetailsEdit.OnClose += FrmLedgerDetails_OnClose;
                    frmLedgerDetailsEdit.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }
    }
}
