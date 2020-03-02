using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAPRO
{
    public static class DbUpdate
    {
        public static string msg = "";
        public static string mquery = "";
        public static int count = 0;
        public static List<string> mListQuery = new List<string>();
        public static void Dbupdate()
        {
            InsertNewTable();

            itemTableUpdate();
            LedgerMainTableUpdate();
            LedgersTableUpdate();
            LedgerStatusTableUpdate();
            PurchaseBillTableUpdate();
            CDRNoteDetailsUpdate();
            FinancialYear();
            UserControls();
            InvoiceSettingTableUpdate();
            StockSummaryTableUpdate();
            StockHistoryTableUpdate();
            ToolsTableUpdate();
            CustomersTableUpdate();

            ExcuteQuery();
            // UpdateUserControls();
        }

        /// <summary>
        /// Item Table Update
        /// </summary>
        public static void itemTableUpdate()
        {
            int counttable = 0;

            #region Change Columns MAXLENGTH
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='item' and COLUMN_NAME = 'ItemName'" +
                           " and CHARACTER_MAXIMUM_LENGTH='50'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE item ALTER COLUMN itemname VARCHAR (500) NOT NULL";
                mListQuery.Add(mquery);
            }

            #endregion

            #region ADD NEW Columns

            #region  Add Columns SubCategory
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='item' and COLUMN_NAME = 'SubCategory'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE item ADD SubCategory varchar(50) NULL";
                mListQuery.Add(mquery);
            }
            #endregion

            #region  Add Columns IsRCM And IsITC
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
            "NUMERIC_PRECISION, DATETIME_PRECISION," +
            " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
            " where TABLE_NAME='item' and (COLUMN_NAME='IsRCM' or COLUMN_NAME = 'IsITC')";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE item ADD IsRCM bit NULL, IsITC bit NULL";
                mListQuery.Add(mquery);
                mquery = "Update item set IsRCM='False',IsITC='True'";
                mListQuery.Add(mquery);
            }
            #endregion

            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// LedgerMaib Table Update & Data Update
        /// </summary>
        public static void LedgerMainTableUpdate()
        {
            int counttable = 0;
            #region Remove then Add COLUMNS
            string query = "Select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, " +
                           "NUMERIC_PRECISION, DATETIME_PRECISION, " +
                           "IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS " +
                           "where TABLE_NAME='LadgerMain' and COLUMN_NAME = 'SubGroup'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE LadgerMain DROP COLUMN SubGroup,Under,GroupName";
                mListQuery.Add(mquery);
                mquery = "ALTER TABLE LadgerMain ADD SubAccount varchar(50) NULL, " +
                                                 "ParentAccount varchar(50) NULL, " +
                                                 "Type varchar(50) NULL";
                mListQuery.Add(mquery);

                #region Update Data
                mquery = "Update LadgerMain set SubAccount='Sundry Debtors',ParentAccount='Current Assets' " +
                         "where Category='Customer'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Sundry Creditors',ParentAccount='Current Liability' " +
                         "where Category='Supplier'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Sales A/C',ParentAccount='Parent',Type='Income' " +
                         "where Category='Sales'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Bank A/C',ParentAccount='Current Assets' " +
                         "where Category='Bank'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Sales Return',ParentAccount='Sales A/C' " +
                         "where Category='Sales_Return'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Purchase A/C',ParentAccount='Parent',Type='Expense' " +
                         "where Category='Purchase'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Purchase Retun',ParentAccount='Purchase A/C' " +
                         "where Category='Purchase_Return'";
                mListQuery.Add(mquery);
                mquery = "Update LadgerMain set SubAccount='Cash A/C',ParentAccount='Current Assets' " +
                         "where Category='Cash'";
                mListQuery.Add(mquery);
                #endregion
            }
            #endregion
            ///Update Cash & Bank Ledgers SubAccount
            mquery = "Update LadgerMain set SubAccount='Cash A/C' where Category='Cash'";
            mListQuery.Add(mquery);
            mquery = "Update LadgerMain set SubAccount='Bank A/C' where Category='Bank'";
            mListQuery.Add(mquery);

            #region Change Columns MAXLENGTH
             query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='LadgerMain' and COLUMN_NAME = 'TemplateName'" +
                           " and CHARACTER_MAXIMUM_LENGTH='300'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE LadgerMain ALTER COLUMN TemplateName VARCHAR (300)";
                mListQuery.Add(mquery);
            }

            #endregion

            count = counttable == 0 ? count : count + 1;
        }
        /// <summary>
        /// Ledgers Table Update
        /// </summary>
        public static void LedgersTableUpdate()
        {
            int counttable = 0;
            #region ADD NEW COLUMNS
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='Ledgers' and COLUMN_NAME = 'AadharNo'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE Ledgers ADD AadharNo varchar(12) NULL";
                mListQuery.Add(mquery);
            }
            #endregion

            #region Change Columns MAXLENGTH
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
          "NUMERIC_PRECISION, DATETIME_PRECISION," +
          " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
          " where TABLE_NAME='Ledgers' and COLUMN_NAME = 'AadharNo' and CHARACTER_MAXIMUM_LENGTH='12'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE Ledgers ALTER COLUMN AadharNo VARCHAR (14) NULL";
                mListQuery.Add(mquery);
            }

            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
        "NUMERIC_PRECISION, DATETIME_PRECISION," +
        " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
        " where TABLE_NAME='Ledgers' and COLUMN_NAME = 'LedgerName' and CHARACTER_MAXIMUM_LENGTH='250'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE Ledgers ALTER COLUMN LedgerName VARCHAR (250) NOT NULL";
                mListQuery.Add(mquery);
            }
            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// FinancialYear Table Update
        /// </summary>
        public static void FinancialYear()
        {
            int counttable = 0;
            #region ADD COLUMN
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='FinancialYear' and COLUMN_NAME = 'BookStart'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE FinancialYear ADD BookStart date NULL";
                mListQuery.Add(mquery);
            }

            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// LedgerStatus Table Update
        /// </summary>
        public static void LedgerStatusTableUpdate()
        {
            int counttable = 0;
            #region ADD NEW COLUMNS
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='LedgerStatus' and COLUMN_NAME = 'Date'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE LedgerStatus ADD Date date NULL";//Create Columns
                mListQuery.Add(mquery);
                mquery = "Update LedgerStatus set Date='01-Apr-2017'";//Update Previous Data
                mListQuery.Add(mquery);
            }
            #endregion
          
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// PurchaseBill Table Update
        /// </summary>
        public static void PurchaseBillTableUpdate()
        {
            int counttable = 0;
            #region ADD NEW COLUMNS
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='PurchaseBill' and COLUMN_NAME = 'BillType'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE PurchaseBill ADD BillType varchar(50) NULL";
                mListQuery.Add(mquery);
            }
            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// CDRNoteDetails Table Update
        /// </summary>
        public static void CDRNoteDetailsUpdate()
        {
            int counttable = 0;
            #region ADD NEW COLUMNS
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='CDRNoteDetails' and (COLUMN_NAME='IsRightProduct' or COLUMN_NAME = 'StockSummaryID' or COLUMN_NAME = 'ReasonType')";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE CDRNoteDetails ADD ReasonType varchar(50) NULL, StockSummaryID bigint NULL,CONSTRAINT FK_CDRNoteDetails_StockSummary  FOREIGN KEY(StockSummaryID) REFERENCES StockSummary(ID)   on  UPDATE CASCADE on delete cascade   , IsRightProduct bit NULL ";
                mListQuery.Add(mquery);
            }
            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        ///UserControls Table Update
        /// </summary>
        public static void UserControls()
        {
            int counttable = 0;
            #region UPDATE COLUMNS
            string query = "select permision from usercontrol where permision='btnDashBoard,btnSales,btnInventory,btnBanking,btnCustomer,btnSupplier,btnEmployee,btnReports,btnSettings,btnUserWindow,'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "Update UserControl set permision='btnDashBoard,btnSales,btnInventory,btnBanking,btnCustomer,btnSupplier,btnEmployee,btnReports,btnSettings,btnUserWindow,' where UserName='Admin'";
                mListQuery.Add(mquery);
            }
            count = counttable == 0 ? count : count + 1;

            #endregion
        }
        /// <summary>
        /// InvoiceSetting Table Update
        /// </summary>
        public static void InvoiceSettingTableUpdate()
        {
            int counttable = 0;
            #region ADD NEW Columns

            #region  Add Columns SubCategory
          string  query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
           "NUMERIC_PRECISION, DATETIME_PRECISION," +
           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
           " where TABLE_NAME='InvoiceSettings' and COLUMN_NAME = 'IsBottomOfPage'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE InvoiceSettings ADD IsBottomOfPage bit NULL";
                mListQuery.Add(mquery);
            }
            #endregion
            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// StockSummary Table Update
        /// </summary>
        public static void StockSummaryTableUpdate()
        {
            int counttable = 0;

            #region Change Columns Allow Not Null To NULL
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='StockSummary' and COLUMN_NAME = 'HighestMRP'" +
                           " and IS_NULLABLE='NO'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE StockSummary ALTER COLUMN HighestMRP float  NULL";
                mListQuery.Add(mquery);
            }

            #endregion
           
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// StockHistory Table Update
        /// </summary>
        public static void StockHistoryTableUpdate()
        {
            int counttable = 0;

            #region Change Columns Allow Not Null To NULL
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='StockHistory' and COLUMN_NAME = 'HighestMRP'" +
                           " and IS_NULLABLE='NO'";
            if (SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE StockHistory ALTER COLUMN HighestMRP float  NULL";
                mListQuery.Add(mquery);
            }

            #endregion

            count = counttable == 0 ? count : count + 1;
        }
        /// <summary>
        /// ToolsTable Table Update
        /// </summary>
        public static void ToolsTableUpdate()
        {
            int counttable = 0;

            #region Change Columns Allow Not Null To NULL
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='ToolsTable' and COLUMN_NAME = 'IsThousandSeparate'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE ToolsTable ADD IsThousandSeparate bit NULL";
                mListQuery.Add(mquery);
                mquery = "Update ToolsTable set IsThousandSeparate='False'";
                mListQuery.Add(mquery);
            }

            #endregion

            count = counttable == 0 ? count : count + 1;
        }


        /// <summary>
        /// Customers Table Update
        /// </summary>
        public static void CustomersTableUpdate()
        {
            int counttable = 0;

            #region Change Columns MAXLENGTH
            string query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='Customers' and COLUMN_NAME = 'BillingName'" +
                           " and CHARACTER_MAXIMUM_LENGTH='300'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE Customers ALTER COLUMN BillingName VARCHAR (300) NULL";
                mListQuery.Add(mquery);
            }

             query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                           "NUMERIC_PRECISION, DATETIME_PRECISION," +
                           " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
                           " where TABLE_NAME='Customers' and COLUMN_NAME = 'ShippingName'" +
                           " and CHARACTER_MAXIMUM_LENGTH='300'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "ALTER TABLE Customers ALTER COLUMN ShippingName VARCHAR (300) NULL";
                mListQuery.Add(mquery);
            }

            #endregion

            count = counttable == 0 ? count : count + 1;
        }


        /// <summary>
        /// Inser Any New Table
        /// </summary>
        public static void InsertNewTable()
        {
            int counttable = 0;
            string query = "";

            #region INSERT TABLE AdjustHistory
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
          "NUMERIC_PRECISION, DATETIME_PRECISION," +
          " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
          " where TABLE_NAME='AdjustHistory'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "create table [dbo].[AdjustHistory](Id bigint NOT NULL IDENTITY(1,1)," +
                    "InVoiceNo varchar(50) NOT NULL,Type varchar(50) NOT NULL ,Cr_Adv_Id" +
                    " varchar(50) NOT NULL,AdjustAmount float NOT NULL, PRIMARY KEY (ID))";
                mListQuery.Add(mquery);
            }
            #endregion

            #region INSERT TABLE DamageProduct
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
          "NUMERIC_PRECISION, DATETIME_PRECISION," +
          " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
          " where TABLE_NAME='DamageProduct'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "create table [dbo].[DamageProduct](ID bigint NOT NULL IDENTITY(1,1)," +
                    "StockSummaryId bigint NOT NULL ,BatchNo varchar(50) NOT NULL ,ItemID" +
                    " bigint NOT NULL,DamageType varchar(50) NOT NULL,Reason varchar(50) NULL,MfgDate date NULL," +
                    " ExpDate date NULL,HighestUnit varchar(50) NULL,HighestDamageQty float NULL,HighestRate float NULL," +
                    " HighestMRP float NULL,MiddleUnit varchar(50) NULL,MiddleDamageQty float NULL,MiddleRate float NULL," +
                    " MiddleMRP float NULL,LowestUnit varchar(50) NULL,LowestDamageQty  float NULL,LowestRate float NULL," +
                    " LowestMRP float NULL,HighestMeasureQty float NULL,LowestMeasureQty float NULL,PurchaseQty float NULL," +
                    " PurchaseRate float NULL,PurchaseUnit varchar(50) NULL,PRIMARY KEY (ID),UNIQUE(StockSummaryId),FOREIGN KEY (StockSummaryId) REFERENCES StockSummary(ID))";
                mListQuery.Add(mquery);
            }
            #endregion

            #region INSERT TABLE ToolsTable
            query = "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
          "NUMERIC_PRECISION, DATETIME_PRECISION," +
          " IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS" +
          " where TABLE_NAME='ToolsTable'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                mquery = "create table [dbo].[ToolsTable](ID bigint NOT NULL IDENTITY(1,1)," +
                    "DecimalPlace int NOT NULL ,IsPercentageInMRP bit NOT NULL," +
                    "CONSTRAINT Pk_ID PRIMARY KEY (ID))";
                mListQuery.Add(mquery);
                mquery = "insert into ToolsTable(DecimalPlace,IsPercentageInMRP) values(2,'False') ";
                mListQuery.Add(mquery);
            }
            #endregion

            #region Create & Data Insert TABLE SubAccount
            query = "Select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                    "NUMERIC_PRECISION, DATETIME_PRECISION, " +
                    "IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS " +
                    "where TABLE_NAME='SubAccount'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                query = "CREATE TABLE [dbo].[SubAccount]( " +
                         "[ID][int] IDENTITY(1, 1) NOT NULL, " +
                         "[AccountName] [varchar](50) NOT NULL, " +
                         "[ParentAccount] [varchar](50) NOT NULL, " +
                         "[Nature] [varchar](50) NULL, " +
                         "[IsFixed][bit] NULL, " +
                         "CONSTRAINT[PK_SubAccount] PRIMARY KEY CLUSTERED( [ID] ASC) " +
                         "WITH(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, " +
                         "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON[PRIMARY], " +
                         "CONSTRAINT[IX_SubAccount] UNIQUE NONCLUSTERED( [AccountName] ASC) " +
                         "WITH(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, " +
                         "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON[PRIMARY] " +
                         ") ON[PRIMARY]";
                mListQuery.Add(query);
                #region Insert Data
                query = "Insert into SubAccount(AccountName,ParentAccount,Nature,IsFixed) " +
                        "Values('Current Assets','Parent','Assets','True'), " +
                              "('Current Liability','Parent','Assets','True'), " +
                              "('Capital A/C','Parent','Liabilities','True'), " +
                             "('Expense[Direct]','Parent','Expense','True'), " +
                             "('Expense[Indirect]','Parent','Expense','True'), " +
                             "('Income[Direct]','Parent','Income','True'), " +
                             "('Income[Indirect]','Parent','Income','True'), " +
                             "('Investment','Parent','Assets','True'), " +
                             "('Loans[Liability]','Parent','Liabilities','True'), " +
                             "('Purchase A/C','Parent','Expense','True'), " +
                             "('Sales A/C','Parent','Income','True'), " +
                             "('Fixed Assets','Parent','Assets','True'), " +
                             "('Duties & Tax','Current Liability',NULL,'True'), " +
                             "('Bank A/C','Current Assets',NULL,'True') , " +
                             "('Bank OD','Loans[Liability]', NULL,'True') , " +
                             "('Cash A/C','Current Assets',NULL,'True') , " +
                             "('Load & Advances','Current Assets', NULL,'True') , " +
                             "('Stock in Hand','Current Assets',NULL,'True') , " +
                             "('Sundry Creditors','Current Liability', NULL,'True') , " +
                             "('Sundry Debtors','Current Assets',NULL,'True'), " +
                             "('Sales Return','Sales A/C', NULL,'True') , " +
                             "('Purchase Retun','Purchase A/C',NULL,'True') ";
                mListQuery.Add(query);
                #endregion
            }
            #endregion

            #region INSERT TABLE Expense
            query = "Select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH," +
                    "NUMERIC_PRECISION, DATETIME_PRECISION, " +
                    "IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS " +
                    "where TABLE_NAME='Expense'";
            if (!SQLHelper.GetInstance().ExcuteScalar(query, out msg).ISValidObject())
            {
                counttable = 1;
                #region Expense
                mquery = "CREATE TABLE [dbo].[Expense]( " +
                               "[ID][bigint] IDENTITY(1, 1) NOT NULL, " +
                               "[SlNo] [bigint]  NOT NULL, " +
                               "[BillNo] [varchar](50) NULL, " +
                               "[BillingDate]  [date]   NOT NULL, " +
                               "[LedgerId] [uniqueidentifier] NULL, " +
                               "[DueDate] [date] NULL, " +
                               "[MemoNo] [varchar](50) NULL, " +
                               "[Description] [varchar](max) NULL, " +
                               "[TotalAmount] [float] NOT NULL, " +
                               "[DueAmount] [float] NULL, " +
                               "[Status] [varchar](50) NULL, " +
                               "[RCM]    [varchar](50) NULL, " +
                               "[LastTransectionID] [uniqueidentifier]  NULL, " +
                               "CONSTRAINT[PK_Expense_1] PRIMARY KEY CLUSTERED( [SlNo] ASC) " +
                               "WITH(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, " +
                               "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON[PRIMARY]) ON[PRIMARY] " +
                               "ALTER TABLE[dbo].[Expense] " +
                               "WITH CHECK ADD CONSTRAINT[FK_Expense_LadgerMain] FOREIGN KEY([LedgerId]) " +
                               "REFERENCES[dbo].[LadgerMain] ([LadgerID]) " +
                               "ON UPDATE CASCADE " +
                               "ON DELETE CASCADE " +
                               "ALTER TABLE[dbo].[Expense] " +
                               "CHECK CONSTRAINT[FK_Expense_LadgerMain]";
                mListQuery.Add(mquery);
                #endregion
                #region ExpenseDetails
                mquery = "CREATE TABLE [dbo].[ExpenseDetails]( " +
                        "[ID][bigint] IDENTITY(1, 1) NOT NULL, " +
                        "[SlNo] [bigint] NOT NULL, " +
                        "[LedgerID] [uniqueidentifier]  NOT NULL, " +
                        "[Description] [varchar](max) NULL, " +
                        "[Amount] [float] NULL, " +
                        "[TransectionID]  [uniqueidentifier]  NULL, " +
                        "CONSTRAINT[PK_ExpenseDetails] PRIMARY KEY CLUSTERED([ID] ASC)  " +
                        "WITH(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,  " +
                        "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +
                        "ALTER TABLE[dbo].[ExpenseDetails] " +
                        "WITH CHECK ADD CONSTRAINT[FK_ExpenseDetails_ExpenseDetails] FOREIGN KEY([SlNo]) " +
                        "REFERENCES[dbo].[Expense] ([SlNo]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[ExpenseDetails] " +
                        "CHECK CONSTRAINT[FK_ExpenseDetails_ExpenseDetails] " +
                        "ALTER TABLE[dbo].[ExpenseDetails] " +
                        "WITH CHECK ADD CONSTRAINT[FK_ExpenseDetails_LadgerMain] FOREIGN KEY([LedgerID]) " +
                        "REFERENCES[dbo].[LadgerMain] ([LadgerID]) " +
                        "ALTER TABLE[dbo].[ExpenseDetails] " +
                        "CHECK CONSTRAINT[FK_ExpenseDetails_LadgerMain]";
                mListQuery.Add(mquery);
                #endregion
            }
            #endregion
            count = counttable == 0 ? count : count + 1;
        }

        /// <summary>
        /// Execute Query
        /// </summary>
        public static void ExcuteQuery()
        {
            if (mListQuery.Count() != 0)
            {
                if (SQLHelper.GetInstance().ExecuteTransection(mListQuery, out msg))
                {
                    MessageBox.Show(+count + " Table And " + mListQuery.Count() + " Columns Update", "Db Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mListQuery.Clear();
                    count = 0;
                }
            }
            else
            {
                MessageBox.Show("No Update found.", "Db Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
