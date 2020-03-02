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
    public partial class AdvancePaymentAndCreditNoteLIST : Form
    {
        private string msg = "";
        public event Action<List<string>,string > OnClose;
        List<string> mlstQuery = new List<string>();
        bool mIsProcessAdvanceRecept = false, mIsProcessCreditnote = false;
        string mQuery = "";
        string mLedgerId = "", minvoiceNo = "";
        double  mRemaningBalance = 0d,mtotalAdjustamount=0d, mtotalAdjustCramount = 0d;

        public AdvancePaymentAndCreditNoteLIST(string InvoiceNo, string totalAmount, string ledgerid)
        {
            InitializeComponent();
            this.FitOnDown();
            mLedgerId = ledgerid;
            minvoiceNo = InvoiceNo;
            double.TryParse(totalAmount.Replace(",", ""), out mRemaningBalance) ;
            lblInvoiceAmount.Text = totalAmount;
            lblTotReceiptAmt.Text = totalAmount;
            GetDueGetDueCreditNoteInvoice();
            GetAdvanceReceiptVoucher();
        }
        /// <summary>
        /// Due AdvanceCredit Note
        /// </summary>
        private void GetDueGetDueCreditNoteInvoice()
        {
            dgvCreditNote.Hide();
            lblCreditNote.Show();
            string ledgerID = mLedgerId;
            dgvCreditNote.Rows.Clear();
            string query = "select NoteID,NoteNo,convert(varchar(11),RefundDate,106)as date, " +
                           "NoteValue,DueAmount from CDRNote " +
                           "where dueamount>0 and LedgerId='" + ledgerID + "' " +
                           "and DocumentType='C' order by ID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                dgvCreditNote.Show();
                lblCreditNote.Hide();
                foreach (DataRow item in dt.Rows)
                {
                    string crnoteid = item["NoteID"].ToString();
                    string crediteNoteDate = item["date"].ToString();
                    string CreditNoteNo = item["NoteNo"].ToString();
                    string crediteNoteDescription = "Credit_Note # " + CreditNoteNo + "(" + DateTime.Parse(crediteNoteDate).ToString("dd-MMM-yyyy") + ")";

                    double totAmount = double.Parse(item["NoteValue"].ToString().Replace(",", ""));
                    double dueAmount = double.Parse(item["DueAmount"].ToString().Replace(",", ""));
                    bool chkValue = false;
                    dgvCreditNote.Rows.Add(crnoteid, chkValue, crediteNoteDescription,
                                           totAmount, dueAmount);
                }
            }
        }
        /// <summary>
        /// Advance Receipt
        /// </summary>
        private void GetAdvanceReceiptVoucher()
        {
            dgvAdvance.Hide();
            lblnoadvancepayment.Show();
            dgvAdvance.Rows.Clear();
            string query = "select ReceiptNo,ReceiptDate,DueAmount,Total " +
                           "from AdvanceReceiptVoucher  " +
                           "where dueamount>0 and LedgerId='" + mLedgerId + "' order by ID";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                dgvAdvance.Show();
                lblnoadvancepayment.Hide();

                foreach (DataRow item in dt.Rows)
                {
                    #region MyRegion
                    string advanceReceiptNo = item["ReceiptNo"].ToString();
                    string AdvancereceiptDate = item["ReceiptDate"].ToString();
                    string Description = "Advance Receipt # " + advanceReceiptNo + " (" + DateTime.Parse(AdvancereceiptDate).ToString("dd-MMM-yyyy") + ")";
                    double advanceAmount = double.Parse(item["Total"].ToString().Replace(",", ""));
                    double openAmount = double.Parse(item["DueAmount"].ToString().Replace(",", ""));
                    bool chkValue = false;

                    dgvAdvance.Rows.Add(advanceReceiptNo, chkValue, Description,
                                 advanceAmount, openAmount);
                    #endregion
                }
            }
        }
        private void UncheckAllBlankCell()
        {
            mIsProcessAdvanceRecept = false;
            mIsProcessCreditnote = false;
            mtotalAdjustamount = 0d;
            mtotalAdjustCramount = 0d;
            foreach (DataGridViewRow row in dgvCreditNote.Rows)
            {
                double amt = 0d;
                try { amt = double.Parse(row.Cells["AdjustAmountCr"].Value.ToString().Replace(",", "")); }
                catch (Exception) { }
                if (amt <= 0)
                {
                    row.Cells["ChkColumn"].Value = false;
                }
                else
                {
                    mIsProcessAdvanceRecept = true;
                    mtotalAdjustamount = mtotalAdjustamount + amt;
                }
            }
            foreach (DataGridViewRow row in dgvAdvance.Rows)
            {

                double amt = 0f;
                try { amt = double.Parse(row.Cells["AdjustAmount"].Value.ToString().Replace(",", "")); }
                catch (Exception) { }
                if (amt <= 0)
                {
                    row.Cells["ChkAdv"].Value = false;
                }
                else
                {
                    mIsProcessCreditnote = true;
                    mtotalAdjustCramount = mtotalAdjustCramount + amt;

                }
            }
            lblAdjustAmount.Text=(mtotalAdjustamount+mtotalAdjustCramount).ToString();
        }
        private void dgvBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCreditNote.Columns[e.ColumnIndex].Name == "ChkColumn")
            {
                dgvCreditNote.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool chk = bool.Parse(dgvCreditNote.CurrentRow.Cells["ChkColumn"].Value.ToString());
                string dueAmountstr = dgvCreditNote.CurrentRow.Cells["OpenAmount"].Value.ToString();
                double dueamount = 0d;
                double.TryParse(dueAmountstr.Replace(",", ""), out dueamount);
                if (!chk && mRemaningBalance != 0)
                {
                    if (mRemaningBalance > dueamount)
                    {
                        dgvCreditNote.CurrentRow.Cells["AdjustAmountCr"].Value = dueAmountstr;
                        mRemaningBalance = mRemaningBalance - dueamount;
                    }
                    else
                    {
                        dgvCreditNote.CurrentRow.Cells["AdjustAmountCr"].Value = mRemaningBalance;
                        mRemaningBalance = 0;
                    }
                }
                else
                {
                    string adjustAmountstr = dgvCreditNote.CurrentRow.Cells["AdjustAmountCr"].Value.ToString();
                    double adjustAmoun = 0d;
                    double.TryParse(adjustAmountstr.Replace(",", ""), out adjustAmoun);
                    mRemaningBalance = mRemaningBalance + adjustAmoun;
                    dgvCreditNote.CurrentRow.Cells["AdjustAmountCr"].Value = "";


                }
                lblTotReceiptAmt.Text = mRemaningBalance.ToString("0.00");
                UncheckAllBlankCell();
            }
        }
        private void dgvBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvCreditNote.Columns[dgvCreditNote.CurrentCell.ColumnIndex].Name == "ReceiptAmount")
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
            try { paymentAmount = double.Parse(str.Replace(",", "")); } catch (Exception) { }
            string due = dgvCreditNote.CurrentRow.Cells["DueAmount"].Value.ToString();
            try { dueAmount = double.Parse(due.Replace(",", "")); } catch (Exception) { }
            if (paymentAmount <= 0)
            {
                dgvCreditNote.CurrentRow.Cells["ChkColumn"].Value = false;
                if (e.KeyChar == '\b')
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
            {
                dgvCreditNote.CurrentRow.Cells["ChkColumn"].Value = true;
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
                bool a = chk;
                double.TryParse(dueAmountstr.Replace(",", ""), out dueamount);
                if (!chk && mRemaningBalance != 0)
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
                    double.TryParse(adjustAmountstr.Replace(",", ""), out adjustAmoun);
                    mRemaningBalance = mRemaningBalance + adjustAmoun;
                    dgvAdvance.CurrentRow.Cells["AdjustAmount"].Value = "";
                }
                lblTotReceiptAmt.Text = mRemaningBalance.ToString("0.00");

                UncheckAllBlankCell();

            }
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
                UncheckAllBlankCell();
            }
            double openamountAmount = 0f, paymentAmount = 0f;
            try { paymentAmount = double.Parse(str.Replace(",", "")); } catch (Exception) { }
            string due = dgvAdvance.CurrentRow.Cells["OpenAmountAdv"].Value.ToString();
            try { openamountAmount = double.Parse(due.Replace(",", "")); } catch (Exception) { }
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidEntry())
            {
                if (MessageBox.Show("Do you want to process?", "Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataSave();
                }

            }
        }

        private bool IsValidEntry()
        {
            if (mIsProcessCreditnote||mIsProcessAdvanceRecept)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please give adjust amount.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dgvAdvance.Select();
                return false;
            }
        }

        private void DataSave()
        {
            mlstQuery.Clear();
            UpdateAdvanceReceiptAndAdjustHistoryTable();
            UpdateCreditNoteAndAdjustHistoryTable();
            this.Close();
        }
        private void UpdateAdvanceReceiptAndAdjustHistoryTable()
        {
            foreach (DataGridViewRow row in dgvAdvance.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkAdv"].Value.ToString());
                if (ChekRow)
                {
                    double adjustamt = 0f, openamount = 0f;
                    try
                    {
                        adjustamt = double.Parse(row.Cells["AdjustAmount"].Value.ToString().Replace(",", ""));
                        openamount = double.Parse(row.Cells["OpenAmountAdv"].Value.ToString().Replace(",", ""));
                    }
                    catch (Exception) { }
                    string advancereceiptNo = row.Cells["AdvanceReceiptNo"].Value.ToString();
                    string status = "";
                    if (adjustamt < openamount)
                    {
                        status = "Open";
                        openamount = openamount - adjustamt;
                    }
                    else
                    {
                        status = "Close";
                        openamount = 0;
                    }
                    mQuery = "update AdvanceReceiptVoucher set status='" + status + "',dueamount='" + openamount + "' where ReceiptNo='" + advancereceiptNo + "' ";
                    mlstQuery.Add(mQuery);
                    mQuery = "insert into AdjustHistory( InVoiceNo, Type, Cr_Adv_Id, AdjustAmount) values('" + minvoiceNo + "','Advance_Receipt','" + advancereceiptNo + "','" + adjustamt + "')";
                    mlstQuery.Add(mQuery);
                }
            }

        }
        private void UpdateCreditNoteAndAdjustHistoryTable()
        {
            foreach (DataGridViewRow row in dgvCreditNote.Rows)
            {
                bool ChekRow = bool.Parse(row.Cells["ChkColumn"].Value.ToString());
                if (ChekRow)
                {
                    double adjustamt = 0f, openamount = 0f;
                    try
                    {
                        adjustamt = double.Parse(row.Cells["AdjustAmountCr"].Value.ToString().Replace(",",""));
                        openamount = double.Parse(row.Cells["OpenAmount"].Value.ToString().Replace(",", ""));
                    }
                    catch (Exception) { }
                    string creditNoteid = row.Cells["CRNoteId"].Value.ToString();
                    string status = "";
                    if (adjustamt < openamount)
                    {
                        status = "Open";
                        openamount = openamount - adjustamt;
                    }
                    else
                    {
                        status = "Close";
                        openamount = 0;
                    }
                    mQuery = "update CDRNote set status='" + status + "',dueamount='" + openamount + "' where NoteID='" + creditNoteid + "' ";
                    mlstQuery.Add(mQuery);
                    mQuery = "insert into AdjustHistory( InVoiceNo, Type, Cr_Adv_Id, AdjustAmount) values('" + minvoiceNo + "','Credit_Note','" + creditNoteid + "','" + adjustamt + "')";
                    mlstQuery.Add(mQuery);
                }
            }

        }
        private void ReceiptVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose(mlstQuery, lblTotReceiptAmt.Text);
            }
        }
    }
}
