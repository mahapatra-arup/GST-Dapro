using System;
using System.Data;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class AccountHeadList : Form
    {
        private string msg = "";
        public AccountHeadList()
        {
            InitializeComponent();

            dgvLedgerAccount.Columns["LedgerName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvLedgerAccount.Columns["Under"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvLedgerAccount.Columns["Debit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLedgerAccount.Columns["Credit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void GetLedgerAccounts()
        {
            dgvLedgerAccount.Rows.Clear();
            string query = "Select LadgerID,TemplateName,SubAccount,LedgerStatus.CurrentBalance from LadgerMain " +
                           "Left outer join LedgerStatus on LadgerMain.LadgerID=LedgerStatus.LedgerID " +
                           "where SubAccount not in ('Sundry Debtors','Sundry Creditor') and FinYearID=" + FinancialYearTools._YearID + "";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string ledgerID = item["LadgerID"].ToString();
                    string ledgerName = item["TemplateName"].ToString();
                    string under = item["SubAccount"].ToString();
                    string balanceStr = item["CurrentBalance"].ToString();
                    double balance = 0d;
                    double.TryParse(balanceStr, out balance);
                  
                    if (balance == 0)
                    {
                        dgvLedgerAccount.Rows.Add(ledgerID, ledgerName, under, null, null, "Run report");
                    }
                    else if (balance > 0)
                    {
                        dgvLedgerAccount.Rows.Add(ledgerID, ledgerName, under, balance.toString(), null, "Run report");
                    }
                    else
                    {
                        dgvLedgerAccount.Rows.Add(ledgerID, ledgerName, under, null, Math.Abs(balance).toString(), "Run report");
                    }
                }
            }
        }
        private void ShowOthersAcHead_Shown(object sender, EventArgs e)
        {
            GetLedgerAccounts();
        }
        private void AddAccountHead_Click(object sender, EventArgs e)
        {
            AddAccountLedger frmAddOthersAcHead = new AddAccountLedger();
            frmAddOthersAcHead.OnClose += new Action<string>(frmAddOthersAcHead_OnClose);
            frmAddOthersAcHead.Show();
        }
        void frmAddOthersAcHead_OnClose(string obj)
        {
            GetLedgerAccounts();
        }
        private void btnEditAccountHead_Click(object sender, EventArgs e)
        {
            if (dgvLedgerAccount.SelectedCells.Count > 0)
            {
                string ledgerID = dgvLedgerAccount.CurrentRow.Cells["LedgerID"].Value.ToString();

                if (!ledgerID.ISNullOrWhiteSpace())
                {
                    AddAccountLedger editAcHead = new AddAccountLedger(ledgerID);
                    editAcHead.OnClose += new Action<string>(frmAddOthersAcHead_OnClose);
                    editAcHead.Show();
                }
            }
        }
        private void dgvLedgerAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLedgerAccount.SelectedCells.Count > 0 && dgvLedgerAccount.Columns[e.ColumnIndex].Name == "LinkColumn")
            {
                string ledgerID = dgvLedgerAccount.CurrentRow.Cells["LedgerID"].Value.ToString();
                string ledgerName = dgvLedgerAccount.CurrentRow.Cells["LedgerName"].Value.ToString();
                if (!ledgerID.ISNullOrWhiteSpace())
                {
                    //CashBook frmCashBook = new CashBook(CashBook._Type., ledgerID, ledgerName);
                    //frmCashBook.Size = new System.Drawing.Size(1250, 500);
                    //frmCashBook.FitToVertical();
                    //frmCashBook.StartPosition = FormStartPosition.Manual;
                    //frmCashBook.ShowDialog();
                }
            }
        }
    }
}
