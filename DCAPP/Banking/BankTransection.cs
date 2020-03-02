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
    public partial class BankTransection : Form
    {
        public event Action OnClose;
        private bool mSuccess = false;
        public enum _Mode
        {
            Deposit,
            Withdrawal
        }
        private _Mode mMode;
        private string mLedgerID = "";
        private string mSlNoEdit = "";
        private string mSlNo = "";
        private string msg = "";
        private double mTotalPreviousAmount = 0d;
        private List<string> mLstQuery = new List<string>();
        public BankTransection(_Mode mode, string ledgerID, string ledgerName)
        {
            InitializeComponent();
            mMode = mode;
            mLedgerID = ledgerID;
            lblBankLedger.Text = ledgerName;
            GenerateSlNo();
            switch (mMode)
            {
                case _Mode.Deposit:
                    rbtnDeposit.Checked = true;
                    break;
                case _Mode.Withdrawal:
                    rbtnWithdrawal.Checked = true;
                    break;
                default:
                    break;
            }
            cmbCashLedger.ADDCashLedgers();

        }
        private void BankTransection_Shown(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }
        private void GenerateSlNo()
        {
            int slno = 1;
            string query = "Select No from Transection where Slno= "+
                           "(Select max(slno) from Transection where TransectionType in ('Deposit','Withdrawal')) ";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o != null)
            {
                try
                {
                    slno = (int.Parse(o.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblJurnalNo.Text = slno.ToString();
        }
        private bool IsValidData()
        {
            if (cmbCashLedger.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select cash account.", mMode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCashLedger.Select();
                return false;
            }
            if (txtAmount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Enter amount.", mMode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAmount.Focus();
                return false;
            }
            return true;
        }
        private void InsertOrUpdateTransection(string date, string no, double totalamount, string drledgerid, string crledgerid, string transectiontype, string chequeNo, string chequeDate, string transectionMode, string narration)
        {
            string transectionid = Guid.NewGuid().ToString();
            string query = "";
            if (mSlNoEdit.ISNullOrWhiteSpace())
            {
                query = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                         "LedgerIdTo, Amount_Dr, Mode, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                        date + "','" + no + "','" + transectiontype + "','" + drledgerid + "','" +
                        crledgerid + "'," + totalamount + ",'" + transectionMode + "','" + chequeNo + "','"
                        + chequeDate + "','" + narration + "')";
                mLstQuery.Add(query);
                transectionid = Guid.NewGuid().ToString();
                query = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                        "LedgerIdTo, Amount_Cr, Mode, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                        date + "','" + no + "','" + transectiontype + "','" + crledgerid + "','" +
                        drledgerid + "'," + totalamount + ",'" + transectionMode + "','" + chequeNo + "','" +
                        chequeDate + "','" + narration + "')";
                mLstQuery.Add(query);
            }
            else
            {
                TransectionTools.GetTransectionId(mSlNoEdit, transectiontype);
                query = "Update Transection Set Date='" + date + "',LedgerIdFrom='" + drledgerid + "', " +
                        "LedgerIdTo='" + crledgerid + "', Amount_Dr=" + totalamount + ",Mode='" + transectionMode + "',ChequeNo='" + chequeNo
                        + "',ChequeDate='" + chequeDate + "',Narration='" + narration +
                        "' where TransectionID='" + TransectionTools._mTransectionIdList[0] + "'";
                mLstQuery.Add(query);

                query = "Update Transection Set Date='" + date + "',LedgerIdFrom='" + crledgerid + "', " +
                        "LedgerIdTo='" + drledgerid + "', Amount_Cr=" + totalamount + ",Mode='" + transectionMode + "',ChequeNo='" + chequeNo
                        + "',ChequeDate='" + chequeDate + "',Narration='" + narration + 
                        "' where TransectionID='" + TransectionTools._mTransectionIdList[1] + "'";
                mLstQuery.Add(query);
            }
        }
        private void DataSave()
        {
            string slNo = lblJurnalNo.Text;
            string date = dtpDate.Text;
            string cashLedgerID = ((KeyValuePair<string, string>)cmbCashLedger.SelectedItem).Key.ToString();
            double amount = 0d;
            double.TryParse(txtAmount.Text, out amount);
            string chequeNo = txtChequeNo.Text.GetDBFormatString();
            string chequeDate = dtpDateCheque.Text;
            string transectionType = mMode.ToString();
            string transectionMode = "";
            string narration = txtDescription.Text.GetDBFormatString();
            string drLedgerid = mLedgerID;
            string crLedgerid = cashLedgerID;
            string mquery = "";
            switch (mMode)
            {
                case _Mode.Deposit:
                    InsertOrUpdateTransection(date, slNo, amount, drLedgerid, crLedgerid, transectionType, chequeNo, chequeDate, transectionMode, narration);
                    #region UpdateLedgerStatus
                    if (!mSlNoEdit.ISNullOrWhiteSpace())
                    {
                        #region CurrentBalanceRestore

                        mLstQuery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mTotalPreviousAmount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                        mLstQuery.Add(mquery);
                        #endregion
                    }
                    #region CurrentBalanceUpdate

                    mLstQuery.Add(LedgerStatus.UpdateLedgerStatus(drLedgerid, crLedgerid, amount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mLstQuery.Add(mquery);
                    #endregion

                    #endregion
                    break;
                case _Mode.Withdrawal:
                    transectionMode = "Cheque";
                    drLedgerid = cashLedgerID;
                    crLedgerid = mLedgerID;

                    InsertOrUpdateTransection(date, slNo, amount, drLedgerid, crLedgerid, transectionType, chequeNo, chequeDate, transectionMode, narration);
                    #region UpdateLedgerStatus
                    if (!mSlNoEdit.ISNullOrWhiteSpace())
                    {
                        #region CurrentBalanceRestore

                        mLstQuery.Add(LedgerStatus.UpdateLedgerStatus(TransectionTools._CRAccountLedgerId, TransectionTools._DRAccountLedgerId, mTotalPreviousAmount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                        mLstQuery.Add(mquery);
                        #endregion
                    }
                    #region CurrentBalanceUpdate

                    mLstQuery.Add(LedgerStatus.UpdateLedgerStatus(drLedgerid, crLedgerid, amount.ToString("0.00"), out mquery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
                    mLstQuery.Add(mquery);
                    #endregion

                    #endregion
                    break;
                default:
                    break;
            }
            if (SQLHelper.GetInstance().ExecuteTransection(mLstQuery, out msg))
            {
                LedgerTools.GetBankLedgers();
                mSuccess = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(msg, mMode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                DataSave();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void rbtnDeposit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDeposit.Checked)
            {
                pnlWithdrwal.Hide();
            }
            else
            {
                pnlWithdrwal.Show();
            }
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtAmount.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void BankTransection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null && mSuccess)
            {
                OnClose();
            }
        }
    }
}
