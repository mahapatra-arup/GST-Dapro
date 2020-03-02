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
    public partial class PurchaseChallanList : Form
    {
        private string msg = "";
        private DataTable mDtTable;
        private string mFromDate, mToDate;
        public PurchaseChallanList()
        {
            InitializeComponent();
            InitDtTable();
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            
        }

        private void InitDtTable()
        {
            mDtTable = new DataTable();

            mDtTable.Columns.Add("slno", typeof(string));
            mDtTable.Columns.Add("challanId", typeof(string));
            mDtTable.Columns.Add("Orderid", typeof(string));
            mDtTable.Columns.Add("Date", typeof(string));
            mDtTable.Columns.Add("NO", typeof(string));
            mDtTable.Columns.Add("PartyName", typeof(string));
            mDtTable.Columns.Add("Amount", typeof(string));
            mDtTable.Columns.Add("Status", typeof(string));

            dgvChallanList.DataSource = mDtTable;
            DataGridViewLinkColumn lnkCol = new DataGridViewLinkColumn();
            lnkCol.Name = "Action";
            lnkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns.Add(lnkCol);
            dgvChallanList.Columns["challanId"].Visible = false;
            dgvChallanList.Columns["Orderid"].Visible = false;

            dgvChallanList.Columns["slno"].HeaderText = "SL NO.";
            dgvChallanList.Columns["Date"].HeaderText = "DATE";
            dgvChallanList.Columns["NO"].HeaderText = "SUPPLIER CHALLAN NO.";
            dgvChallanList.Columns["PartyName"].HeaderText = "PARTY NAME";
            dgvChallanList.Columns["Amount"].HeaderText = "AMOUNT";
            dgvChallanList.Columns["Status"].HeaderText = "Status";

            dgvChallanList.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChallanList.Columns["NO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChallanList.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChallanList.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChallanList.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvChallanList.Columns["slno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvChallanList.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column in dgvChallanList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private string IsPurchaseOrderChallanStatusClose(string mOrderID)
        {
            string query = "Select StatusForchallan from PurchaseOrder where OrderId='" + mOrderID + "'";
            object obj = SQLHelper.GetInstance().ExcuteScalar(query, out msg);
            if (obj.ISValidObject())
            {
                return obj.ToString();
            }
            return null;
        }
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            OrderEntry frmOrderEntry = new OrderEntry();
            frmOrderEntry.OnClose += GenerateChallanList;
            frmOrderEntry.Show(this);
        }

        private void ChallanList_Load(object sender, EventArgs e)
        {
            toolStripCurrentMonth_Click(null, null);
            GenerateChallanList();
        }

        private void GenerateChallanList()
        {
            mDtTable.Clear();
            int i = 0;
            string query = "SELECT PurchaseChallan.*,CONVERT(varchar(11),date,106) as challandate ,LadgerMain.LadgerName  FROM  PurchaseChallan inner join LadgerMain on PurchaseChallan.LedgerID=LadgerMain.LadgerID where date between '" + mFromDate+"' and '"+mToDate+ "' order by SlNo,challandate desc";
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(query, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    string challanId = item["ChallanID"].ToString();
                    string challanno = item["SupplyChallanNo"].ToString();
                    string chalandate = item["challandate"].ToString();
                    string partyName = item["LadgerName"].ToString();
                    string orderid = item["OrderId"].ToString();
                    string status = item["Status"].ToString();
                    float totAmount = float.Parse(item["TotalAmount"].ToString());
                    string challanstatus = IsPurchaseOrderChallanStatusClose(orderid);
                    #region MyRegion
                    
                    DataGridViewLinkCell lnkCell = new DataGridViewLinkCell();
                    lnkCell.ToolTipText = "Click to genarate Bill.";

                    if (status == "Open" && challanstatus=="Close")
                    {
                        lnkCell.Value = "Bill Entry";
                    }

                    #endregion

                    mDtTable.Rows.Add(i+1,challanId, orderid, chalandate, challanno, partyName, totAmount.ToString("0.00"), status);
                    dgvChallanList.Rows[i].Cells["Action"] = lnkCell ;
                   
                    i++;
                }
            }
        }
        private void toolStripToday_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            GenerateChallanList();
            lblFilterHeader.Text = toolStripToday.Text + " (" + mFromDate + " - " + mToDate + ")";
        }
        private void toolStripCurrentMonth_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = "01-" + DateTime.Now.Month.GetMonthMMM() + "-" + DateTime.Now.Year;
            mToDate = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            lblFilterHeader.Text = toolStripCurrentMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateChallanList();
        }
        private void toolStripPreviousMonth_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            int month = 1, year = 0000;
            if (DateTime.Now.Month > 1)
            {
                month = DateTime.Now.Month - 1;
                year = DateTime.Now.Year;
            }
            else
            {
                month = 12;
                year = DateTime.Now.Year - 1;
            }

            mFromDate = "01-" + month.GetMonthMMM() + "-" + year;
            mToDate = DateTime.DaysInMonth(year, month) + "-" + month.GetMonthMMM() + "-" + year;
            lblFilterHeader.Text = toolStripPreviousMonth.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateChallanList();
        }

        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            if (!txtSearchByName.Text.ISNullOrWhiteSpace())
            {
                DataView dtv = new DataView(mDtTable);
                dtv.RowFilter = string.Format("PartyName like '{0}%'", txtSearchByName.Text);
                dgvChallanList.DataSource = dtv;

            }
            else
            {
                GenerateChallanList();
            }
        }

        private void dgvChallanList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChallanList.Columns[e.ColumnIndex].Name != "Action" && e.RowIndex != -1)
            {
                if (UserTools._IsEdit)
                {
                    int rowindex = dgvChallanList.SelectedCells[0].RowIndex;
                    string challanid = dgvChallanList.Rows[rowindex].Cells["challanId"].Value.ToString();
                    string status = dgvChallanList.Rows[rowindex].Cells["Status"].Value.ToString();
                    PurchaseChallanEntry challanEntry = new PurchaseChallanEntry(challanid, status);
                    challanEntry.OnClose += GenerateChallanList;
                    challanEntry.Show(this); 
                }
                else
                {
                    MessageBox.Show("Permission Denied", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void dgvChallanList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChallanList.Columns[e.ColumnIndex].Name == "Action" && e.RowIndex != -1)
            {
                string challanID = dgvChallanList.CurrentRow.Cells["challanId"].Value.ToString();
                string orderid = dgvChallanList.CurrentRow.Cells["Orderid"].Value.ToString();
                PurchaseBillEntry objBillEntry = new PurchaseBillEntry(PurchaseBillEntry._CameFrom.Challan,challanID,orderid);
                objBillEntry.OnClose += GenerateChallanList;
                objBillEntry.Show(this);
            }
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!cmbFilterBy.Text.ISNullOrWhiteSpace())
            //{
            //    if (cmbFilterBy.Text == "Direct Invoice")
            //    {
            //        DataView dv = new DataView(mDtTable);
            //        dv.RowFilter= "Isnull(NO,'')=''";
            //        dgvChallanList.DataSource = dv;
            //    }
            //    else
            //    {
            //        DataView dv = new DataView(mDtTable);
            //       // dv.RowFilter = "SupplyChallanNo !='' and SupplyChallanNo !=null ";
            //        dgvChallanList.DataSource = dv;
            //    }
            //}
        }

        private void toolStripCurrentFinYear_Click(object sender, EventArgs e)
        {
            //cmbFilterBy.Text = "All";
            mFromDate = FinancialYearTools._StartDate;
            mToDate = FinancialYearTools._EndDate;
            lblFilterHeader.Text = toolStripCurrentFinYear.Text + " (" + mFromDate + " to " + mToDate + ")";
            GenerateChallanList();
        }


    }
}
