using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO.ReportFolder.GSTR3
{
    public partial class GSTR3_View : Form
    {
        string msg = "";
        DataSet _Dt = new DSGstr3B();
        DateTime _FirstDate, _LastDate;
        public GSTR3_View()
        {
            InitializeComponent();
            CmbFlterBy.SelectedIndex = 0;
        }
        public void Outword_InwordSuppliesLiableToRCMView()
        {
            //Condition
            string condition = " and Invoice.InvoiceDate between '" + _FirstDate.ToString("dd-MMM-yyyy") + "' and '" + _LastDate.ToString("dd-MMM-yyyy") + "' and status<>'Cancel'";
            //
            string qry = " SELECT  sum(InvoiceDetails.TaxAmount) as TotalTaxAmount,sum(InvoiceDetails.IGSTAmount) as TotalIGSTAmount , " +
            "sum(InvoiceDetails.CGSTAmount) as TotalCGSTAmount ,sum(InvoiceDetails.SGSTAmount) as TotalSGSTAmount , " +
            "sum(InvoiceDetails.CeassAmount) as TotalCeassAmount FROM Invoice INNER JOIN " +
            "InvoiceDetails ON Invoice.InvoiceNo = InvoiceDetails.InvoiceNo  INNER JOIN " +
            "item on item.id = InvoiceDetails.ItemID ";
            string Query = qry + "WHERE item.GSTRate not in('0','Exampted','Non GST','Nil') " + condition +
            "union all " +
            qry + "WHERE item.GSTRate  in('0') " + condition +
            "union all " +
            qry + "WHERE item.GSTRate  in('Exampted','Nil') " + condition +
            "union all " +
            //Purchase bill details for inword purpose
            "SELECT  sum(PurchaseBillDetails.TaxAmount) as TotalTaxAmount,sum(PurchaseBillDetails.IGSTAmount) as TotalIGSTAmount ,  " +
            "sum(PurchaseBillDetails.CGSTAmount) as TotalCGSTAmount ,sum(PurchaseBillDetails.SGSTAmount) as TotalSGSTAmount ,   " +
            "sum(PurchaseBillDetails.CeassAmount) as TotalCeassAmount FROM PurchaseBill INNER JOIN  " +
            "PurchaseBillDetails ON PurchaseBill.BillID = PurchaseBillDetails.Billid  INNER JOIN  " +
            "item on item.id = PurchaseBillDetails.ItemID " +
            "WHERE PurchaseBill.RCM='yes' and PurchaseBill.InvoiceDate between '" + _FirstDate.ToString("dd-MMM-yyyy") + "' and '" + _LastDate.ToString("dd-MMM-yyyy") + "'  " +
            "union all " +
            qry + "WHERE item.GSTRate  in('Non GST')" + condition;

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows.Add(ORG_Tools._GSTin, ORG_Tools._OrganizationName, DateTime.Now.Year, cmbMonth.Text);
                int i = 4;
                foreach (DataRow item in dt.Rows)
                {
                    _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows[0][i] = item["TotalTaxAmount"].ToString();
                    i++;
                    _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows[0][i] = item["TotalIGSTAmount"].ToString();
                    i++;
                    _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows[0][i] = item["TotalCGSTAmount"].ToString();
                    i++;
                    _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows[0][i] = item["TotalSGSTAmount"].ToString();
                    i++;
                    _Dt.Tables["InwordOutwordSuppliesLiableToRCM"].Rows[0][i] = item["TotalCeassAmount"].ToString();
                    i++;
                }
            }
        }
        public void DetailsOfInterStateUnregisterSupplies_Composit_UTView()
        {
            //Condition
            string condition = "where ((Invoice.BillingStateCode not in ('" + ORG_Tools._StateCode + "') and (ISNULL(Invoice.BillingGSTNO, '') = '')) or (LadgerMain.GSTRegistrationType='composition')  or (LadgerMain.GSTRegistrationType='UIN')) " +
            "and Invoice.InvoiceDate between '" + _FirstDate.ToString("dd-MMM-yyyy") + "' and '" + _LastDate.ToString("dd-MMM-yyyy") + "' and status<>'Cancel' ";
            //
            string qry = "select Invoice.BillingState,Invoice.BillingStateCode,SUM(InvoiceDetails.TaxAmount) as TotalTaxAmount ,sum(InvoiceDetails.IGSTAmount) as TotalIGSTAmount from Invoice " +
            "inner join InvoiceDetails on InvoiceDetails.InvoiceNo = Invoice.InvoiceNo " +
            "inner join LadgerMain on LadgerMain.LadgerID=Invoice.LedgerId " + condition +
            "group by Invoice.BillingState,Invoice.BillingStateCode " +
            "order by Invoice.BillingState ";

            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(qry, out msg);
            if (dt.IsValidDataTable())
            {
                foreach (DataRow item in dt.Rows)
                {
                    _Dt.Tables["DetailsOfInterUnregisterCopositionUT"].Rows.Add(item["BillingStateCode"] + "-" + item["BillingState"], item["TotalTaxAmount"], item["TotalIGSTAmount"]);
                }
            }
        }
        public void ITCView()
        {
            //Condition
            string condition = " and PurchaseBill.InvoiceDate between '" + _FirstDate.ToString("dd-MMM-yyyy") + "' and '" + _LastDate.ToString("dd-MMM-yyyy") + "' and status<>'Cancel' ";
            //
            string qry = "SELECT  sum(PurchaseBillDetails.IGSTAmount) as TotalIGSTAmount , " +
            "sum(PurchaseBillDetails.CGSTAmount) as TotalCGSTAmount ,sum(PurchaseBillDetails.SGSTAmount) as TotalSGSTAmount , " +
            "sum(PurchaseBillDetails.CeassAmount) as TotalCeassAmount FROM PurchaseBill INNER JOIN " +
            "PurchaseBillDetails ON PurchaseBill.BillID = PurchaseBillDetails.Billid  INNER JOIN " +
            "item on item.id = PurchaseBillDetails.ItemID ";
            string Query = qry + " where PurchaseBill.RCM = 'Yes' " + condition +
            "Union All " +
            qry + " where PurchaseBill.RCM not in ('Yes') " + condition;
            DataTable dt = SQLHelper.GetInstance().ExcuteNonQuery(Query, out msg);
            if (dt.IsValidDataTable())
            {
                _Dt.Tables["ITC"].Rows.Add();
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    _Dt.Tables["ITC"].Rows[0][i] = item["TotalIGSTAmount"];
                    i++;
                    _Dt.Tables["ITC"].Rows[0][i] = item["TotalCGSTAmount"];
                    i++;
                    _Dt.Tables["ITC"].Rows[0][i] = item["TotalSGSTAmount"];
                    i++;
                    _Dt.Tables["ITC"].Rows[0][i] = item["TotalCeassAmount"];
                    i++;
                }
            }
        }
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _Dt = new DSGstr3B();
            int _MonthDigit = DateTime.ParseExact(cmbMonth.Text, "MMM", CultureInfo.InvariantCulture).Month;
            DateTime now = DateTime.Now;
            _FirstDate = new DateTime(now.Year, _MonthDigit, 1);
            _LastDate = _FirstDate.AddMonths(1).AddDays(-1);
            Outword_InwordSuppliesLiableToRCMView();//1
            DetailsOfInterStateUnregisterSupplies_Composit_UTView();//2
            ITCView();//3
            Print();
            Cursor = Cursors.Default;

        }

        private void CmbFlterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbFlterBy.Text == "Month Wise")
            {
                lblmnth.Text = "Month :";
                cmbMonth.Show();
                pnlCustomDate.Hide();
                cmbMonth.Text = DateTime.Now.ToString("MMM");
                cmbMonth_SelectedIndexChanged(null, null);
            }
            else if (CmbFlterBy.Text == "Custom Date")
            {
                cmbMonth.Hide();
                lblmnth.Text = "Date :";
                pnlCustomDate.Show();
                dtmStartDate_ValueChanged(null, null);
            }
        }

        private void dtmStartDate_ValueChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _Dt = new DSGstr3B();
            _FirstDate = dtmStartDate.Value;
            _LastDate = dtmEndDate.Value;
            Outword_InwordSuppliesLiableToRCMView();//1
            DetailsOfInterStateUnregisterSupplies_Composit_UTView();//2
            ITCView();//3
            Print();
            Cursor = Cursors.Default;
        }

        private void dtmEndDate_ValueChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _Dt = new DSGstr3B();
            _FirstDate = dtmStartDate.Value;
            _LastDate = dtmEndDate.Value;
            Outword_InwordSuppliesLiableToRCMView();//1
            DetailsOfInterStateUnregisterSupplies_Composit_UTView();//2
            ITCView();//3
            Print();
            Cursor = Cursors.Default;
        }

        private void Print()
        {
            CRGSTR3Report CRGSTR3 = new CRGSTR3Report();
            CRGSTR3.SetDataSource(_Dt);
            crystalReportViewer1.ReportSource = CRGSTR3;
        }
    }
}
