using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DAPRO
{
    public partial class BillPayment : Form
    {
        private string msg = "";
        public event Action OnClose;
        private string mTransectionIDForEdit;
        private double mTotalPaidAmount = 0f, mAccountBalance = 0d;
        private DataTable mDtBillDetailsIndivisual;
        List<string> lstQuery = new List<string>();
        private double mRemaningBalance = 0d;
        private string mQuery = "";
        private bool mIsProceesToPayment = false;

        public enum _FromWhere
        {
            Purchase_Bill,
            Expense_Bill
        }
        _FromWhere mFromWhere;
        public BillPayment(_FromWhere fromwhere, string transectionID, string supplier)
        {
            InitializeComponent();
            this.FitOnDown();
            mFromWhere = fromwhere;
            cmbSuppliersName.SelectedIndexChanged -= cmbSuppliersName_SelectedIndexChanged;
            cmbSuppliersName.ADDSupplierLedgers();
            cmbSuppliersName.SelectedIndexChanged += cmbSuppliersName_SelectedIndexChanged;
            cmbSuppliersName.Text = supplier;
            cmbPaymentMethod.SelectedIndex = 0;
            switch (mFromWhere)
            {
                case _FromWhere.Purchase_Bill:
                    GenerateSlNoForPurchaseBill();
                    break;
                case _FromWhere.Expense_Bill:
                    GenerateSlNoForExpenseBill();
                    break;
                default:
                    break;
            }
        }
        //private void GenerateSlNo()
        //{
        //    string query = "select Max(No) as maxno from transection where TransectionType='Receipt_Payment'";
        //    object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
        //    try
        //    {
        //        mSlNoOfReceipt = long.Parse(o.ToString()) + 1;
        //    }
        //    catch (Exception) { }

        //    lblVoucherNo.Text = mSlNoOfReceipt.ToString();

        //}
        private void GenerateSlNoForPurchaseBill()
        {
            int slno = 1;
            string query = "select MAX(no) as maxno from (select convert(bigint,No) as no from Transection where TransectionType='Bill_Payment') as Transection";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                try
                {
                    slno = (int.Parse(obj.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblSlNo.Text = slno.ToString();
        }
        private void GenerateSlNoForExpenseBill()
        {
            int slno = 1;
            string query = "select MAX(no) as maxno from (select convert(bigint,No) as no from Transection where TransectionType='Expense') as Transection";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj != null)
            {
                try
                {
                    slno = (int.Parse(obj.ToString()) + 1);
                }
                catch (Exception)
                {
                }
            }
            lblSlNo.Text = slno.ToString();
        }

        /// <summary>
        /// Due Bills
        /// </summary>
        private void GetPurchaseDueBills()
        {
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                string supplierLedgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
                dgvBills.Rows.Clear();
                int i = 0;
                string query = "Select BillID, BillNo, InvoiceNo, convert(varchar(11),InvoiceDate,106) as BillDate, " +
                               "TotalAmount, DueAmount, convert(varchar(11),DueDate,106) as DueDate FROM PurchaseBill " +
                               "where LedgerId='" + supplierLedgerID + "' and DueAmount>0 order by slno";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string billno = item["BillNo"].ToString();
                        string billID = item["BillID"].ToString();
                        string billDate = item["BillDate"].ToString();
                        string trantype = "Purchase Bill";
                        string invoiceno = item["InvoiceNo"].ToString();
                        string billDescription = "Bill#  " + invoiceno + " (" + billDate + ")";
                        string dueDate = item["DueDate"].ToString();
                        double totAmount = double.Parse(item["TotalAmount"].ToString());
                        double dueAmount = double.Parse(item["DueAmount"].ToString());
                        bool chkValue = false;
                        string paidAmount = "";
                        if (i == 0)
                        {
                            chkValue = true;
                            paidAmount = dueAmount.toString();
                        }
                        else
                        {
                            chkValue = false;
                            paidAmount = "";
                        }
                        dgvBills.Rows.Add(billID, chkValue, trantype, billDescription, dueDate,
                                          totAmount.toString(), dueAmount.toString(), billno);

                        DataGridViewTextBoxCell txtCol = new DataGridViewTextBoxCell();
                        txtCol.MaxInputLength = 11;
                        txtCol.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        txtCol.Value = paidAmount;
                        dgvBills.Rows[i].Cells["PaymentAmount"] = txtCol;
                        i++;
                    }
                }
            }
        }

        private void GetExpanseDueBills()
        {
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                string supplierLedgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
                dgvBills.Rows.Clear();
                int i = 0;
                string query = "Select SlNo, SlNo, BillNo, convert(varchar(11),BillingDate,106) as BillingDate, " +
                               "TotalAmount, DueAmount , convert(varchar(11),DueDate,106) as DueDate FROM Expense " +
                               "where LedgerId='" + supplierLedgerID + "' and DueAmount>0 order by BillingDate";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string billno = item["SlNo"].ToString();
                        string billID = item["SlNo"].ToString();
                        string billDate = item["BillingDate"].ToString();
                        string trantype = "Expense Bill";
                        string invoiceno = item["BillNo"].ToString();
                        string billDescription = "Expense Bill#  " + invoiceno + " (" + billDate + ")";
                        string dueDate = item["DueDate"].ToString();
                        double totAmount = double.Parse(item["TotalAmount"].ToString());
                        double dueAmount = double.Parse(item["DueAmount"].ToString());
                        bool chkValue = false;
                        string paidAmount = "";
                        if (i == 0)
                        {
                            chkValue = true;
                            paidAmount = dueAmount.toString();
                        }
                        else
                        {
                            chkValue = false;
                            paidAmount = "";
                        }
                        dgvBills.Rows.Add(billID, chkValue, trantype, billDescription, dueDate,
                                          totAmount.toString(), dueAmount.toString(), billno);

                        DataGridViewTextBoxCell txtCol = new DataGridViewTextBoxCell();
                        txtCol.MaxInputLength = 11;
                        txtCol.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        txtCol.Value = paidAmount;
                        dgvBills.Rows[i].Cells["PaymentAmount"] = txtCol;
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Supplier Credit
        /// </summary>
        private void GetAdvanceBills()
        {
            splitContainer1.Panel2.Hide();
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                string supplierLedgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
                dgvAdvance.Rows.Clear();
                int i = 0;
                string query = "Select  SlNo, PaymentNo, convert(varchar(11),PaymentDate,106) as BillDate, " +
                               "LedgerId, Total, DueAmount FROM AdvancePayment " +
                               "where LedgerId='" + supplierLedgerID + "' and  DueAmount>0 order by slno";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    ///View Supplier credit Panel
                    splitContainer1.Panel2.Show();
                    foreach (DataRow item in dt.Rows)
                    {
                        #region MyRegion
                        string billID = item["PaymentNo"].ToString();
                        string billDate = item["BillDate"].ToString();
                        string billNo = item["PaymentNo"].ToString();
                        string billDescription = "Advance Payment # " + billNo + " (" + billDate + ")";
                        double totAmount = double.Parse(item["Total"].ToString());
                        double dueAmount = double.Parse(item["DueAmount"].ToString());
                        bool chkValue = false;
                        string paidAmount = "";
                        //if (i == 0)
                        //{
                        //    chkValue = true;
                        //    paidAmount = dueAmount.toString();
                        //}
                        //else
                        //{
                            chkValue = false;
                            paidAmount = "";
                        //}
                        dgvAdvance.Rows.Add(billID, chkValue, billDescription, totAmount.toString(),
                                            dueAmount.toString(), dueAmount.toString());

                        DataGridViewTextBoxCell txtCol = new DataGridViewTextBoxCell();
                        txtCol.MaxInputLength = 11;
                        txtCol.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        txtCol.Value = paidAmount;
                        dgvAdvance.Rows[i].Cells["AdjustAmount"] = txtCol;
                        i++;
                        #endregion
                    }
                }
            }
        }

        private void CalcutateTotalAmount()
        {
            mRemaningBalance = 0;
            mIsProceesToPayment = false;
            double totAmount = 0f;
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                double amt = 0f;
                try { amt = double.Parse(row.Cells["PaymentAmount"].Value.ToString()); }
                catch (Exception) { }
                if (amt > 0)
                {
                    mIsProceesToPayment = true;
                    row.Cells["ChkColumn"].Value = true;
                }
                else
                {
                    row.Cells["ChkColumn"].Value = false;
                }
                totAmount += amt;
            }
            mRemaningBalance = mRemaningBalance + totAmount;
            double advAmount = 0f;
            foreach (DataGridViewRow row in dgvAdvance.Rows)
            {
                double amt = 0f;
                try { amt = double.Parse(row.Cells["AdjustAmount"].Value.ToString()); }
                catch (Exception) { }
                if (amt > 0)
                {
                    row.Cells["ChkAdv"].Value = true;
                }
                else
                {
                    row.Cells["ChkAdv"].Value = false;
                }
                advAmount += amt;
            }
            mRemaningBalance = mRemaningBalance - advAmount;
            if (totAmount > advAmount)
            {
                mTotalPaidAmount = totAmount - advAmount;
            }
            else
            {
                mTotalPaidAmount = 0f;
            }
            lblTotPaymentAmt.Text = "Receipt Amount Rs. " + mTotalPaidAmount.toString();
            txtPaymentTotalAmount.Text = mTotalPaidAmount.toString();
            if (mTotalPaidAmount > 0)
            {
                pnlPaymentMethod.Show();
            }
            else
            {
                pnlPaymentMethod.Hide();
            }
        }

        private bool IsValid()
        {
            if (cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select supplier's name.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbSuppliersName.Select();
                return false;
            }
            if (mTotalPaidAmount > 0 && cmbPaymentMethod.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment method.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentMethod.Select();
                return false;
            }
            if (mTotalPaidAmount > 0 && cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment account.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentAccount.Select();
                return false;
            }
            if (!mIsProceesToPayment)
            {
                MessageBox.Show("Select any bill.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dgvBills.Focus();
                return false;
            }
            if (mAccountBalance < mTotalPaidAmount)
            {
                MessageBox.Show("Insufficent balance in selected account!!!.\n account and try again", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentAccount.Focus();
                return false;
            }
            if (cmbPaymentMethod.Text == "Cheque")
            {

                if (txtChequeNo.Text.ISNullOrWhiteSpace() || txtChequeNo.Text.Length < 6)
                {
                    MessageBox.Show("Enter a valid cheque no.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtChequeNo.Select();
                    return false;
                }
            }

            return true;
        }
        private string AdvancePaymentByID(string transectionID)
        {
            string query = "Select AccountHeadIDTo from TransectionDetailsView where TransectionID='" + transectionID + "'";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (o.ISValidObject())
            {
                return o.ToString();
            }
            return null;
        }
        private void BillPaid()
        {
            lstQuery.Clear();
            string transectionid = Guid.NewGuid().ToString();
            string payeeladgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
            string paymentDate = dtpBillDate.Text;
            string transectiontype = "";
            if (mFromWhere == _FromWhere.Purchase_Bill)
            {
                transectiontype = "Bill_Payment";
            }
            else if (mFromWhere == _FromWhere.Expense_Bill)
            {
                transectiontype = "Expense";

            }
            string paidAmountStr = mTotalPaidAmount.ToString();
            string advSlNo = lblSlNo.Text;

            string mode = "'" + cmbPaymentMethod.Text + "'";
            string bankname = "NULL";
            string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
            string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
            string crledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
            string drledgerid = payeeladgerID;

            if (mTotalPaidAmount == 0)
            {
                crledgerid = LedgerTools._DicCashLedgers.Keys.First();
            }

            InsertOrUpdateTransection(transectionid, paymentDate, advSlNo, paidAmountStr, drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate);

            #region CurrentBalanceUpdate

            lstQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, paidAmountStr, out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
            lstQuery.Add(mQuery);
            #endregion
            //UpdateCurrentBalance(crledgerid, paidAmountStr);
            if (mFromWhere == _FromWhere.Purchase_Bill)
            {
                UpdatePurchaseBillLastTransectionId(transectionid);
            }
            else if (mFromWhere == _FromWhere.Expense_Bill)
            {
                UpdateExpenseLastTransectionId(transectionid);
            }
            UpdateAdvancePaymentLastTransectionId(transectionid);

            //query = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
            //        "LedgerIdTo, Amount_Dr,Mode, BankName, ChequeNo, ChequeDate, Narration) " +
            //        "Values('" + transectionID + "','" + paymentDate + "','" + advSlNo + "','" +
            //        transectiontype + "','" + payeeladgerID + "'," + mTotalPaidAmount + ",'" + mode + "','" +
            //        bankName + "'," + chequeNo + ",'" + issueDate + "','" + narration + "')";
            //lstQuery.Add(query);

            #region Execute
            if (MessageBox.Show("Are you sure you want to proceed to payment Rs. " + mTotalPaidAmount + " ?", "Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(lstQuery, out msg))
                {
                    MessageBox.Show("Bill payment complete Rs." + lblTotPaymentAmt.Text, "Bill Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            #endregion
        }
        private void InsertOrUpdateTransection(string tranid, string invoiceDate, string invoiceNo, string totalinvoicevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate)
        {
            string transectionid = tranid;
            mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                    "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                    invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + drledgerid + "','" +
                    crledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                    + "," + ChequeNo + "," + ChequeDate + ")";
            lstQuery.Add(mQuery);
            transectionid = Guid.NewGuid().ToString();
            mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                    "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate) values('" + transectionid + "','" +
                    invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + crledgerid + "','" +
                    drledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                    + "," + ChequeNo + "," + ChequeDate + ")";
            lstQuery.Add(mQuery);
        }
        private void UpdateCurrentBalance(string ledgerid, string paidAmountStr)
        {
            //if (!mReceiptNoForEdit.ISNullOrWhiteSpace())
            //{
            //    RestoreCurrentBalance();
            //}
            mQuery = "update LedgerStatus set CurrentBalance=(select (CurrentBalance-" + paidAmountStr + ") as currentbal  from LedgerStatus where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "')  where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "' ";
            lstQuery.Add(mQuery);
        }
        private void UpdatePurchaseBillLastTransectionId(string transectionid)
        {
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkColumn"].Value.ToString());
                if (ChekRow)
                {
                    double paymentamt = 0f, dueamount = 0f;
                    try
                    {
                        paymentamt = double.Parse(row.Cells["PaymentAmount"].Value.ToString());
                        dueamount = double.Parse(row.Cells["DueAmount"].Value.ToString());
                    }
                    catch (Exception) { }
                    string billid = row.Cells["billid"].Value.ToString();
                    string billno = row.Cells["Billno"].Value.ToString();
                    string billtype = row.Cells["BillType"].Value.ToString();
                    string status = "";
                    if (paymentamt < dueamount)
                    {
                        status = "Due";
                        dueamount = dueamount - paymentamt;
                    }
                    else
                    {
                        status = "Paid";
                        dueamount = 0;
                    }
                    mQuery = "update PurchaseBill set LastTransecetionID='" + transectionid + "',status='" + status + "',dueamount='" + dueamount + "' where BillNo='" + billno + "' ";
                    lstQuery.Add(mQuery);
                    mQuery = "insert into PaymentHistory( TransectionID, Type, VchNo, Amount) values('" + transectionid + "','" + billtype + "','" + billno + "','" + paymentamt + "')";
                    lstQuery.Add(mQuery);
                }
            }

        }

        private void UpdateExpenseLastTransectionId(string transectionid)
        {
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkColumn"].Value.ToString());
                if (ChekRow)
                {
                    double paymentamt = 0f, dueamount = 0f;
                    try
                    {
                        paymentamt = double.Parse(row.Cells["PaymentAmount"].Value.ToString());
                        dueamount = double.Parse(row.Cells["DueAmount"].Value.ToString());
                    }
                    catch (Exception) { }
                    string billno = row.Cells["billid"].Value.ToString();
                    //string billno = row.Cells["Billno"].Value.ToString();
                    string billtype = row.Cells["BillType"].Value.ToString();
                    string status = "";
                    if (paymentamt < dueamount)
                    {
                        status = "Due";
                        dueamount = dueamount - paymentamt;
                    }
                    else
                    {
                        status = "Paid";
                        dueamount = 0;
                    }
                    mQuery = "update Expense set LastTransectionID='" + transectionid + "',Status='" + status + "',DueAmount='" + dueamount + "' where SlNo='" + billno + "' ";
                    lstQuery.Add(mQuery);
                    mQuery = "insert into PaymentHistory( TransectionID, Type, VchNo, Amount) values('" + transectionid + "','" + billtype + "','" + billno + "','" + paymentamt + "')";
                    lstQuery.Add(mQuery);
                }
            }

        }

        private void UpdateAdvancePaymentLastTransectionId(string transectionid)
        {
            foreach (DataGridViewRow row in dgvAdvance.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkAdv"].Value.ToString());
                if (ChekRow)
                {
                    double adjustamt = 0f, openamount = 0f;
                    try
                    {
                        adjustamt = double.Parse(row.Cells["AdjustAmount"].Value.ToString());
                        openamount = double.Parse(row.Cells["OpenAmountAdv"].Value.ToString());
                    }
                    catch (Exception) { }
                    string paymentNo = row.Cells["advancePaymentNO"].Value.ToString();
                    string status = "";
                    if (adjustamt < openamount)
                    {
                        status = "Due";
                        openamount = openamount - adjustamt;
                    }
                    else
                    {
                        status = "Paid";
                        openamount = 0;
                    }
                    mQuery = "update AdvancePayment set LastTransecetionID='" + transectionid + "',Status='" + status + "',dueamount='" + openamount + "' where PaymentNo='" + paymentNo + "' ";
                    lstQuery.Add(mQuery);
                    mQuery = "insert into PaymentHistory( TransectionID, Type, VchNo, Amount) values('" + transectionid + "','Advance_Payment','" + paymentNo + "','" + adjustamt + "')";
                    lstQuery.Add(mQuery);
                }
            }

        }

        private void cmbSuppliersName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                if (mFromWhere == _FromWhere.Purchase_Bill)
                {
                    GetPurchaseDueBills();
                }
                else if (mFromWhere == _FromWhere.Expense_Bill)
                {
                    GetExpanseDueBills();
                }
                GetAdvanceBills();
                CalcutateTotalAmount();

            }
        }
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.Text == "Cash")
            {
                cmbPaymentAccount.ADDCashLedgers();
                pnlChequeDetails.Visible = false;
            }
            else if (cmbPaymentMethod.Text == "Cheque")
            {
                cmbPaymentAccount.ADDBankLedgers();
                pnlChequeDetails.Visible = true;
            }
            else
            {
                cmbPaymentAccount.ADDBankLedgers();
                pnlChequeDetails.Visible = false;
            }
            if (cmbPaymentAccount.Items.Count > 0)
            {
                cmbPaymentAccount.SelectedIndex = 0;
            }
        }
        private void dgvBills_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvBills.CurrentCell = dgvBills.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception)
            {
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBills.Columns[dgvBills.CurrentCell.ColumnIndex].Name == "PaymentAmount")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress += new KeyPressEventHandler(OpenBills_KeyPress);
                }
            }
        }
        void OpenBills_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            int l = str.Length;
            if (!(char.IsDigit(e.KeyChar)))// If not digit
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
                else
                {
                    if (str.Length > 0)
                    {
                        str = str.Substring(0, l - 1);
                    }
                }
            }
            else
            {
                str = str + e.KeyChar;
            }

            double dueAmount = 0f, paymentAmount = 0f;
            try { paymentAmount = double.Parse(str); } catch (Exception) { }
            string due = dgvBills.CurrentRow.Cells["DueAmount"].Value.ToString();
            try { dueAmount = double.Parse(due); } catch (Exception) { }
            if (paymentAmount <= 0)
            {
                dgvBills.CurrentRow.Cells["ChkColumn"].Value = false;
                if (e.KeyChar == '\b')
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
            {
                dgvBills.CurrentRow.Cells["ChkColumn"].Value = true;
                if (paymentAmount > dueAmount)
                {
                    e.Handled = true;
                }

            }
        }
        private void dgvBills_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcutateTotalAmount();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                BillPaid();
            }
        }
        private void BillPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose();
            }
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.Columns[e.ColumnIndex].Name == "ChkColumn")
            {
                foreach (DataGridViewRow dgvAdvancerow in dgvAdvance.Rows)
                {
                    dgvAdvancerow.Cells["AdjustAmount"].Value = "";
                }
                dgvBills.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool chk = bool.Parse(dgvBills.CurrentRow.Cells["ChkColumn"].Value.ToString());
                string dueAmount = dgvBills.CurrentRow.Cells["DueAmount"].Value.ToString();
                if (!chk)
                {
                    dgvBills.CurrentRow.Cells["PaymentAmount"].Value = dueAmount;
                }
                else
                {
                    dgvBills.CurrentRow.Cells["PaymentAmount"].Value = "";
                }
                CalcutateTotalAmount();
            }
        }
        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string ledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                double balance = AccountHeadTools.GetCurrentBalance(ledgerid);
                mAccountBalance = balance;
                lblBalance.Text = "Rs. " + balance.toString();
            }
        }
        private void dgvAdvance_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvAdvance.Columns[dgvAdvance.CurrentCell.ColumnIndex].Name == "AdjustAmount")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress += new KeyPressEventHandler(AdvanceBills_KeyPress);
                }
            }
        }
        void AdvanceBills_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            int l = str.Length;
            if (!(char.IsDigit(e.KeyChar)))// If not digit
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
                else
                {
                    if (str.Length > 0)
                    {
                        str = str.Substring(0, l - 1);
                    }
                }
            }
            else
            {
                str = str + e.KeyChar;
            }
            if (str.ISNullOrWhiteSpace())
            {
                dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value = 0;
                CalcutateTotalAmount();
            }
            double openamountAmount = 0f, paymentAmount = 0f;
            try { paymentAmount = double.Parse(str); } catch (Exception) { }
            string due = dgvAdvance.CurrentRow.Cells["OpenAmountAdv"].Value.ToString();
            try { openamountAmount = double.Parse(due); } catch (Exception) { }
            if (paymentAmount <= 0)
            {
                if (e.KeyChar == '\b')
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
            {
                if (paymentAmount > openamountAmount || paymentAmount > mRemaningBalance)
                {
                    if (e.KeyChar == '\b')
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
        }

        void AdvanceBills_KeyPress2(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string str = txt.Text;
            int l = str.Length;
            if (!(char.IsDigit(e.KeyChar)))// If not digit
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
                else
                {
                    if (str.Length > 0)
                    {
                        str = str.Substring(0, l - 1);
                    }
                }
            }
            else
            {
                str = str + e.KeyChar;
            }

            double dueAmount = 0f, paymentAmount = 0f;
            try { paymentAmount = double.Parse(str); } catch (Exception) { }
            string due = dgvAdvance.CurrentRow.Cells["OpenAmountAdv"].Value.ToString();
            try { dueAmount = double.Parse(due); } catch (Exception) { }
            if (paymentAmount <= 0)
            {
                if (e.KeyChar == '\b')
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
            {
                if (paymentAmount > dueAmount)
                {
                    e.Handled = true;
                }
            }
        }
        private void dgvAdvance_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvAdvance.CurrentCell = dgvAdvance.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception)
            {
            }
        }

        private void txtPaymentTotalAmount_Leave(object sender, EventArgs e)
        {
            ClearData();
            FillPaymentAmount();
            CalcutateTotalAmount();
        }
        private void FillPaymentAmount()
        {
            string TotalPaymentAmountstr = txtPaymentTotalAmount.Text;
            double TotalPaymentAmount = 0, Dueamount = 0;
            double.TryParse(TotalPaymentAmountstr, out TotalPaymentAmount);
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                if (TotalPaymentAmount > 0)
                {
                    double.TryParse(row.Cells["DueAmount"].Value.ToString(), out Dueamount);
                    if (Dueamount >= TotalPaymentAmount)
                    {
                        row.Cells["PaymentAmount"].Value = TotalPaymentAmount;
                        TotalPaymentAmount = 0;
                        break;
                    }
                    else
                    {
                        row.Cells["PaymentAmount"].Value = Dueamount;
                        TotalPaymentAmount = TotalPaymentAmount - Dueamount;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void ClearData()
        {
            foreach (DataGridViewRow dgvBillsrow in dgvBills.Rows)
            {
                dgvBillsrow.Cells["PaymentAmount"].Value = "";
            }
            foreach (DataGridViewRow dgvAdvancerow in dgvAdvance.Rows)
            {
                dgvAdvancerow.Cells["AdjustAmount"].Value = "";
            }
        }

        private void txtPaymentTotalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == '.')
            {
                e.Handled = false;
                if (e.KeyChar == '.' && txtPaymentTotalAmount.Text.Contains('.'))
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void dgvAdvance_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcutateTotalAmount();
        }

        private void dgvBills_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.Columns[e.ColumnIndex].Name=="PaymentAmount")
            {
                foreach (DataGridViewRow dgvAdvancerow in dgvAdvance.Rows)
                {
                    dgvAdvancerow.Cells["AdjustAmount"].Value = "";
                }
                CalcutateTotalAmount();
            }
        }

        private void dgvAdvance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAdvance.Columns[e.ColumnIndex].Name == "ChkAdv")
            {
                dgvAdvance.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool chk = bool.Parse(dgvAdvance.CurrentRow.Cells["ChkAdv"].Value.ToString());
                string dueAmountstr = dgvAdvance.CurrentRow.Cells["OpenAmountAdv"].Value.ToString();
                double dueamount = 0d;
                double.TryParse(dueAmountstr, out dueamount);
                if (!chk)
                {
                    if (mRemaningBalance > dueamount)
                    {
                        dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value = dueAmountstr;
                        mRemaningBalance = mRemaningBalance - dueamount;
                    }
                    else
                    {
                        dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value = mRemaningBalance;
                        mRemaningBalance = 0;
                    }
                }
                else
                {
                    string adjustAmountstr = dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value.ToString();
                    double adjustAmoun = 0d;
                    double.TryParse(adjustAmountstr, out adjustAmoun);
                    mRemaningBalance = mRemaningBalance + adjustAmoun;
                    dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value = "";


                }
                CalcutateTotalAmount();

            }
        }
    }
}
