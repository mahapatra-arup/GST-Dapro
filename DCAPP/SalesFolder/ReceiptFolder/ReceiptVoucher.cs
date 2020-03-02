using DAPRO.ReceiptVoucherView;
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
    public partial class ReceiptVoucher : Form
    {
        private string msg = "";
        public event Action OnClose;
        private string mTransectionIDForEdit;
        private double mTotalReceiptAmount = 0f;
        private DataTable mDtBillDetailsIndivisual;
        List<string> mlstQuery = new List<string>();
        private bool mIsProceesToPayment = false;
        private long mSlNoOfReceipt = 1;
        private double mRemaningBalance = 0d;
        string mQuery = "";

        public ReceiptVoucher(string billID, string customer)
        {
            InitializeComponent();
            mTransectionIDForEdit = billID;
            GenerateRefNo();
            InitIndivisualBillDetails();
            cmbSuppliersName.SelectedIndexChanged -= cmbSuppliersName_SelectedIndexChanged;
            cmbSuppliersName.AddCustomers();
            cmbSuppliersName.SelectedIndexChanged += cmbSuppliersName_SelectedIndexChanged;
            cmbSuppliersName.Text = customer;
            cmbPaymentMethod.SelectedIndex = 0;
            if (cmbPaymentAccount.Items.Count>0)
            {
                cmbPaymentAccount.SelectedIndex = 0;
            }

            this.FitOnDown();
        }
        private void GenerateRefNo()
        {
            string query = "select MAX(no) as maxno from (select convert(bigint,No) as no from Transection where TransectionType='Receipt_Payment') as Transection";
            object o = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            try
            {
                mSlNoOfReceipt = long.Parse(o.ToString()) + 1;
            }
            catch (Exception) { }

            lblVoucherNo.Text = mSlNoOfReceipt.ToString();

        }
        private void InitIndivisualBillDetails()
        {
            mDtBillDetailsIndivisual = new DataTable();
            mDtBillDetailsIndivisual.Columns.Add("BillID", typeof(string));
            mDtBillDetailsIndivisual.Columns.Add("AccountHeadID", typeof(string));
            mDtBillDetailsIndivisual.Columns.Add("DueAmount", typeof(string));
        }
        private void GetBillingDetails()
        {
            string ladgerId = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
            string mbillingName, mbillingAddress, mbillingSateName, mbillingStateCode, mbillingGSTNo;
            LedgerTools.GetLadgerBillingDetails(ladgerId, out mbillingName, out mbillingAddress, out mbillingSateName, out mbillingStateCode, out mbillingGSTNo);
            lblAddress.Text = mbillingAddress;
            lblState.Text = mbillingSateName + " - " + mbillingSateName;
            lblGSTIN.Text = mbillingGSTNo;
        }
        private void cmbSuppliersName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                lblAddress.Text = "";
                lblState.Text = "";
                lblGSTIN.Text = "";
            }
            else
            {
                GetBillingDetails();
                GetDueInvoice();
                GetAdvanceReceiptVoucher();
                CalcutateTotalAmount();
            }
        }

        /// <summary>
        /// Due Invoice
        /// </summary>
        private void GetDueInvoice()
        {
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                string ledgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
                dgvBills.Rows.Clear();
                int i = 0;
                string query = "Select InvoiceNo,InvoiceDate,DueDate,DueAmount,TotalInvoiceAmount from Invoice "+
                               "where dueamount>0 and LedgerId='" + ledgerID + "' order by slno";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string invoiceno = item["InvoiceNo"].ToString();
                        string invoicedate = item["InvoiceDate"].ToString();
                        string invoiceDescription = "Invoice # "+invoiceno+ "(" + DateTime.Parse(invoicedate).ToString("dd-MMM-yyyy") + ")";
                        string dueDate = item["DueDate"].ToString();
                        double totAmount = double.Parse(item["TotalInvoiceAmount"].ToString());
                        double dueAmount = double.Parse(item["DueAmount"].ToString());
                        bool chkValue = false;
                        string paidAmount = "";
                        if (i == 0)
                        {
                            chkValue = true;
                            paidAmount = dueAmount.ToString("0.00");
                        }
                        else
                        {
                            chkValue = false;
                            paidAmount = "";
                        }
                        dgvBills.Rows.Add(invoiceno, chkValue, invoiceDescription, dueDate, 
                                          totAmount.toString(), dueAmount.toString());

                        DataGridViewTextBoxCell txtCol = new DataGridViewTextBoxCell();
                        txtCol.MaxInputLength = 11;
                        txtCol.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        txtCol.Value = paidAmount;
                        dgvBills.Rows[i].Cells["ReceiptAmount"] = txtCol;
                        i++;
                    }
                }
            }
        }
        /// <summary>
        /// Supplier Credit
        /// </summary>
        private void GetAdvanceReceiptVoucher()
        {
            splitContainer1.Panel2.Hide();
            if (!cmbSuppliersName.Text.ISNullOrWhiteSpace())
            {
                string supplierLedgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
                dgvAdvance.Rows.Clear();
                int i = 0;
                string query = "select ReceiptNo,ReceiptDate,DueAmount,Total from AdvanceReceiptVoucher  where dueamount>0 and LedgerId='" + supplierLedgerID + "' order by slno ";
                DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
                if (dt.IsValidDataTable())
                {
                    ///View Supplier credit Panel
                    splitContainer1.Panel2.Show();
                    foreach (DataRow item in dt.Rows)
                    {
                        #region MyRegion
                        string advanceReceiptNo = item["ReceiptNo"].ToString();
                        string AdvancereceiptDate = item["ReceiptDate"].ToString();
                        string Description = "Advance Receipt # " + advanceReceiptNo + " (" + DateTime.Parse(AdvancereceiptDate).ToString("dd-MMM-yyyy") + ")";
                        double advanceAmount = double.Parse(item["Total"].ToString());
                        double openAmount = double.Parse(item["DueAmount"].ToString());
                        bool chkValue = false;
                        string adjustAmount = "";
                        //if (i == 0)
                        //{
                        //    chkValue = true;
                        //    adjustAmount = openAmount.toString();
                        //}
                        //else
                        //{
                            chkValue = false;
                            adjustAmount = "";
                        //}
                        dgvAdvance.Rows.Add(advanceReceiptNo, chkValue,Description, 
                                           advanceAmount.toString(), openAmount.toString());

                        DataGridViewTextBoxCell txtCol = new DataGridViewTextBoxCell();
                        txtCol.MaxInputLength = 11;
                        txtCol.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        txtCol.Value = adjustAmount;
                        dgvAdvance.Rows[i].Cells["AdjustAmount"] = txtCol;
                        i++;
                        #endregion
                    }
                }
            }
        }
        private void CalcutateTotalAmount()
        {
            mRemaningBalance=0;
            mIsProceesToPayment = false;
            double totAmount = 0f;
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                double amt = 0f;
                try { amt = double.Parse(row.Cells["ReceiptAmount"].Value.ToString()); }
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
            if (totAmount> advAmount)
            {
                mTotalReceiptAmount = totAmount - advAmount;
            }
            else
            {
                mTotalReceiptAmount = 0f;
            }
            lblTotReceiptAmt.Text = "Receipt Amount Rs. " + mTotalReceiptAmount.toString();
            txtPaymentTotalAmount.Text = mTotalReceiptAmount.toString();
            if (mTotalReceiptAmount > 0)
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
            if (mTotalReceiptAmount > 0 && cmbPaymentMethod.Text.ISNullOrWhiteSpace())
            {
                MessageBox.Show("Select payment method.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPaymentMethod.Select();
                return false;
            }
            if (mTotalReceiptAmount > 0 && cmbPaymentAccount.Text.ISNullOrWhiteSpace())
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
            if (cmbPaymentMethod.Text == "Cheque")
            {
                if (cmbBank.Text.ISNullOrWhiteSpace())
                {
                    MessageBox.Show("Select bank name.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbBank.Select();
                    return false;
                }
                if (txtChequeNo.Text.ISNullOrWhiteSpace() || txtChequeNo.Text.Length < 6)
                {
                    MessageBox.Show("Enter a valid cheque no.", "Bill Pay", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtChequeNo.Select();
                    return false;
                }
            }
            return true;
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
                    dgvBills.CurrentRow.Cells["ReceiptAmount"].Value = dueAmount;
                }
                else
                {
                    dgvBills.CurrentRow.Cells["ReceiptAmount"].Value = "";
                }
                CalcutateTotalAmount();
            }
        }
        private void dgvBills_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcutateTotalAmount();
        }
        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBills.Columns[dgvBills.CurrentCell.ColumnIndex].Name == "ReceiptAmount")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress += new KeyPressEventHandler(OpenInvoice_KeyPress);
                }
            }
        }
        void OpenInvoice_KeyPress(object sender, KeyPressEventArgs e)
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
        private void dgvAdvance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAdvance.Columns[e.ColumnIndex].Name == "ChkAdv")
            {
                dgvAdvance.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool chk = bool.Parse(dgvAdvance.CurrentRow.Cells["ChkAdv"].Value.ToString());
                string dueAmountstr = dgvAdvance.CurrentRow.Cells["OpenAmountAdv"].Value.ToString();
                double dueamount = 0d;
                double.TryParse(dueAmountstr,out dueamount);
                if (!chk)
                {
                    if (mRemaningBalance> dueamount)
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
        private void dgvAdvance_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcutateTotalAmount();
        }
        private void dgvAdvance_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvAdvance.Columns[dgvAdvance.CurrentCell.ColumnIndex].Name == "AdjustAmount")
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress += new KeyPressEventHandler(AdvanceReceipt_KeyPress);
                }
            }
        }
        void AdvanceReceipt_KeyPress(object sender, KeyPressEventArgs e)
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
                if (paymentAmount > openamountAmount || paymentAmount>mRemaningBalance)
                {
                    if (e.KeyChar == '\b')
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
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
                cmbBank.AddBank();
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                ReceiptSave();
            }
        }
        private void ReceiptSave()
        {
            mlstQuery.Clear();
            string ladgerID = ((KeyValuePair<string, string>)cmbSuppliersName.SelectedItem).Key.ToString();
            string paymentDate = dtpBillDate.Text;
            string paidAmountStr = mTotalReceiptAmount.ToString();
            string voucherNo = lblVoucherNo.Text;

            string transectionid = Guid.NewGuid().ToString();
            string transectiontype = "Receipt_Payment";
            string drledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
            string crledgerid = ladgerID;
            string status = "Due";
            string mode = "'" + cmbPaymentMethod.Text + "'";
            string bankname = cmbPaymentMethod.Text == "Cheque" ? "'" + cmbBank.Text.GetDBFormatString() + "'" : "NULL";
            string checkno = cmbPaymentMethod.Text == "Cheque" ? "'" + txtChequeNo.Text.GetDBFormatString() + "'" : "NULL";
            string checkdate = cmbPaymentMethod.Text == "Cheque" ? "'" + dtpDateCheque.Text.GetDBFormatString() + "'" : "NULL";
            if (mTotalReceiptAmount==0)
            {
                drledgerid =  LedgerTools._DicCashLedgers.Keys.First();
            }
            string partpayment = string.Empty;
            string fullpayment = string.Empty;

            UpdateInvoiceLastTransectionId(transectionid,out partpayment,out fullpayment);
            UpdateAdvanceReceiptLastTransectionId(transectionid);

            partpayment = partpayment.ISNullOrWhiteSpace() ? string.Empty : "Part Payment of(" + partpayment + ")";
            fullpayment = fullpayment.ISNullOrWhiteSpace() ? string.Empty : "Full Payment of(" + fullpayment + ")";

            string naration = fullpayment.ISNullOrWhiteSpace() ? partpayment : partpayment.ISNullOrWhiteSpace() ? fullpayment : fullpayment + ", " + partpayment;
            InsertOrUpdateTransection(transectionid, paymentDate, voucherNo, paidAmountStr, drledgerid, crledgerid, transectiontype, mode, bankname, checkno, checkdate, naration);
            
            #region CurrentBalanceUpdate

            mlstQuery.Add(LedgerStatus.UpdateLedgerStatus(drledgerid, crledgerid, paidAmountStr, out mQuery));//drQuery Add in the ListQuery and Out CrQuery in mQuery
            mlstQuery.Add(mQuery);
            #endregion
            //UpdateCurrentBalance(drledgerid, paidAmountStr);

            #region Execute
            if (MessageBox.Show("Are you sure you want to receipt Rs. " + mTotalReceiptAmount + " ?", "Receipt Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(mlstQuery, out msg))
                {
                    if (MessageBox.Show("Do you want to print the receipt copy?","Receipt Voucher",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ReceiptVoucherReportView receiptVoucherReportView = new ReceiptVoucherReportView(transectionid);
                        receiptVoucherReportView.Show();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg,"Receipt Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            #endregion
        }
        private void InsertOrUpdateTransection(string tranid, string invoiceDate, string invoiceNo, string totalinvoicevalue, string drledgerid, string crledgerid, string transectiontype, string Mode, string BankName, string ChequeNo, string ChequeDate,string narration)
        {
            string transectionid = tranid;
            mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                    "LedgerIdTo, Amount_Dr,Mode,BankName, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                    invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + drledgerid + "','" +
                    crledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                    + "," + ChequeNo + "," + ChequeDate + ",'"+ narration + "')";
            mlstQuery.Insert(0, mQuery);
            transectionid = Guid.NewGuid().ToString();
            mQuery = "Insert into Transection(TransectionID, Date, No, TransectionType, LedgerIdFrom, " +
                    "LedgerIdTo, Amount_Cr,Mode,BankName, ChequeNo, ChequeDate,Narration) values('" + transectionid + "','" +
                    invoiceDate + "','" + invoiceNo + "','" + transectiontype + "','" + crledgerid + "','" +
                    drledgerid + "'," + totalinvoicevalue + "," + Mode + "," + BankName
                    + "," + ChequeNo + "," + ChequeDate + ",'"+ narration + "')";
            mlstQuery.Insert(1, mQuery);

        }
        private void UpdateCurrentBalance(string ledgerid,string paidAmountStr)
        {
            //if (!mReceiptNoForEdit.ISNullOrWhiteSpace())
            //{
            //    RestoreCurrentBalance();
            //}
            mQuery = "update LedgerStatus set CurrentBalance=(select (CurrentBalance+" + paidAmountStr + ") as currentbal  from LedgerStatus where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "')  where LedgerID='" + ledgerid + "' and FinYearID='" + FinancialYearTools._YearID + "' ";
            mlstQuery.Add(mQuery);
        }
        private void UpdateInvoiceLastTransectionId(string transectionid,out string partPayment, out string fullpayment)
        {
            partPayment = string.Empty;
            fullpayment = string.Empty;
            foreach (DataGridViewRow row in dgvBills.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkColumn"].Value.ToString());
                if (ChekRow)
                {
                    double receiptamt = 0f,dueamount=0f;
                    try {
                        receiptamt = double.Parse(row.Cells["ReceiptAmount"].Value.ToString());
                        dueamount = double.Parse(row.Cells["DueAmount"].Value.ToString());
                    }
                    catch (Exception) { }
                    string inVoiceno = row.Cells["InVoiceno"].Value.ToString();
                    string status = "";
                    if (receiptamt<dueamount)
                    {
                        status = "Due";
                        dueamount = dueamount - receiptamt;
                        partPayment = partPayment.ISNullOrWhiteSpace() ? partPayment + inVoiceno : partPayment + "," + inVoiceno;
                    }
                    else
                    {
                        status = "Paid";
                        dueamount = 0;
                        fullpayment = fullpayment.ISNullOrWhiteSpace() ? fullpayment + inVoiceno : fullpayment + "," + inVoiceno;
                    }
                    mQuery = "update Invoice set LastTransecetionID='" + transectionid + "',status='" + status + "',dueamount='" + dueamount + "' where invoiceno='" + inVoiceno + "' ";
                    mlstQuery.Add(mQuery);
                    mQuery = "insert into ReceiptPaymentStatus( LastTransectionId, VoucherType, VoucharNo, Amount) values('"+transectionid+"','InVoice','"+inVoiceno+"','"+receiptamt+"')";
                    mlstQuery.Add(mQuery);
                }
            }
            
        }
        private void UpdateAdvanceReceiptLastTransectionId(string transectionid)
        {
            foreach (DataGridViewRow row in dgvAdvance.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkAdv"].Value.ToString());
                if (ChekRow)
                {
                    double adjustamt = 0f,openamount=0f;
                    try {
                        adjustamt = double.Parse(row.Cells["AdjustAmount"].Value.ToString());
                        openamount = double.Parse(row.Cells["OpenAmountAdv"].Value.ToString());
                    }
                    catch (Exception) { }
                    string advancereceiptNo = row.Cells["AdvanceReceiptNo"].Value.ToString();
                    string status = "";
                    if (adjustamt<openamount)
                    {
                        status = "Open";
                        openamount = openamount - adjustamt;
                    }
                    else
                    {
                        status = "Close";
                        openamount = 0;
                    }
                    mQuery = "update AdvanceReceiptVoucher set LastTransecetionID='" + transectionid + "',status='" + status + "',dueamount='" + openamount + "' where ReceiptNo='" + advancereceiptNo + "' ";
                    mlstQuery.Add(mQuery);
                    mQuery = "insert into ReceiptPaymentStatus( LastTransectionId, VoucherType, VoucharNo, Amount) values('"+transectionid+"','Advance_Receipt','"+ advancereceiptNo + "','"+adjustamt+"')";
                    mlstQuery.Add(mQuery);
                }
            }
            
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
        private void ReceiptVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose!=null)
            {
                OnClose();
            }
        }

        private void cmbPaymentAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBalance.Text = "Rs. ";
            if (!cmbPaymentAccount.Text.ISNullOrWhiteSpace())
            {
                string ledgerid = ((KeyValuePair<string, string>)cmbPaymentAccount.SelectedItem).Key.ToString();
                double balance = AccountHeadTools.GetCurrentBalance(ledgerid);
                lblBalance.Text = "Rs. " + balance.toString();
            }
        }
        private void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            txtChequeNo.ForeColor = Color.Red;
            if (txtChequeNo.Text.Length == 6)
            {
                txtChequeNo.ForeColor = Color.Black;
            }
        }
        private void txtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else { e.Handled = true; }
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
                        row.Cells["ReceiptAmount"].Value = TotalPaymentAmount;
                        TotalPaymentAmount = 0;
                        break;
                    }
                    else
                    {
                        row.Cells["ReceiptAmount"].Value = Dueamount;
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
                dgvBillsrow.Cells["ReceiptAmount"].Value = "";
            }
            foreach (DataGridViewRow dgvAdvancerow in dgvAdvance.Rows)
            {
                dgvAdvancerow.Cells["AdjustAmount"].Value = "";
            }
        }

        private void dgvBills_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.Columns[e.ColumnIndex].Name == "ReceiptAmount")
            {
                foreach (DataGridViewRow dgvAdvancerow in dgvAdvance.Rows)
                {
                    dgvAdvancerow.Cells["AdjustAmount"].Value = "";
                }
                CalcutateTotalAmount();
            }
        }
    }
}
