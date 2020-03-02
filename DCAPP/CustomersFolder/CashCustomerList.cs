using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class CashCustomerList : Form
    {
        private string msg = "";
        DataTable mDt;
        public CashCustomerList()
        {
            InitializeComponent();
            GeneratemDtColumns();
        }
        private void GeneratemDtColumns()
        {
            mDt = new DataTable();
            mDt.Columns.Add("LedgerId", typeof(string));
            mDt.Columns.Add("SlNo", typeof(string));
            mDt.Columns.Add("LedgerName", typeof(string));
            mDt.Columns.Add("Mobile", typeof(string));
            mDt.Columns.Add("Email;", typeof(string));

            mDt.Columns.Add("Address", typeof(string));
            mDt.Columns.Add("Town", typeof(string));
            mDt.Columns.Add("Dist", typeof(string));
            mDt.Columns.Add("State", typeof(string));
            mDt.Columns.Add("PinNo", typeof(string));
            mDt.Columns.Add("PanNo", typeof(string));
            dgvSuppliersDetails.DataSource = mDt;

            dgvSuppliersDetails.Columns["LedgerId"].Visible = false;

            dgvSuppliersDetails.Columns["SlNo"].HeaderText = "Sl. No";
            dgvSuppliersDetails.Columns["LedgerName"].HeaderText = "Supplier's Name";
            dgvSuppliersDetails.Columns["Mobile"].HeaderText = "Mobile No.";
            dgvSuppliersDetails.Columns["PanNo"].HeaderText = "PAN No";
            ///Header Alignment
            dgvSuppliersDetails.Columns["LedgerName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliersDetails.Columns["Email"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliersDetails.Columns["Address"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliersDetails.Columns["Town"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliersDetails.Columns["Dist"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliersDetails.Columns["State"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvSuppliersDetails.Columns["SlNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSuppliersDetails.Columns["Mobile"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSuppliersDetails.Columns["PinNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSuppliersDetails.Columns["PanNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSuppliersDetails.Columns["Action"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }
        private void GetCustomersList()
        {
            mDt.Clear();
            int i = 0;
            string query = "Select Ledgers.* from Ledgers "+
                           "inner join LadgerMain on Ledgers.LedgerID=LadgerMain.LadgerID Where Category='Cash_Customer'";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string tableID = item["ID"].ToString();
                    string LedgerName = item["LedgerName"].ToString();
                    string mobileNo = item["Mobile"].ToString();
                    string email = item["Email"].ToString();
                    string address = item["Address"].ToString();
                    string town = item["City_Town"].ToString();
                    string dist = item["Dist"].ToString();
                    string state = item["State"].ToString();
                    string pinNo = item["PinCode"].ToString();
                    string panNo = item["PAN"].ToString();

                    mDt.Rows.Add(tableID, ++i, LedgerName, mobileNo, email, address, town, dist, state, pinNo, panNo);
                }
            }
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
        private void AddSuppliersDetails_Click(object sender, EventArgs e)
        {
            LedgerDetails frmLedgerDetails = new LedgerDetails(LedgerDetails._LedgerCategory.Cash_Customer, LedgerDetails._Type.show);
            //frmLedgerDetails.FitOnDown();
            frmLedgerDetails.StartPosition =FormStartPosition.CenterScreen;
            frmLedgerDetails.OnClose += LedgerDetails_OnClose;
            frmLedgerDetails.ShowDialog();
        }
        private void LedgerDetails_OnClose(string obj)
        {
            GetCustomersList();
        }
        private void CustomerWindow_Shown(object sender, EventArgs e)
        {
            GetCustomersList();
        }
        private void dgvSuppliersDetails_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                dgvSuppliersDetails.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvSuppliersDetails.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Maroon;
                dgvSuppliersDetails.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Maroon;
                dgvSuppliersDetails.Cursor = Cursors.Hand;
            }
        }
        private void dgvSuppliersDetails_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                dgvSuppliersDetails.Cursor = Cursors.Default;

                return;
            }
            else
            {
                dgvSuppliersDetails.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                dgvSuppliersDetails.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvSuppliersDetails.Cursor = Cursors.Default;
            }
        }
        private void dgvSuppliersDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dgvSuppliersDetails.SelectedCells.Count > 0)
                {
                    string tableId = dgvSuppliersDetails.Rows[e.RowIndex].Cells["LedgerId"].Value.ToString();
                    LedgerDetails frm = new LedgerDetails(LedgerDetails._LedgerCategory.Cash_Customer, LedgerDetails._Type.showDialog, tableId);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.OnClose += Frm_OnClose;
                    frm.ShowDialog();
                } 
            }
        }
        private void Frm_OnClose(string obj)
        {
            GetCustomersList();
        }
        private void CustomerWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
