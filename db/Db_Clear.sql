delete from invoice;
delete from Transection;

delete from Challan
delete from SalesOrder;
delete from Estimate;
delete from CDRNote;

delete from PurchaseBill;
delete from PurchaseChallan;
delete from PurchaseOrder;
delete from StockHistory;
delete from DamageProduct;
delete from StockSummary;

delete from LadgerMain WHERE Category NOT IN('Purchase','Sales','Purchase_Return','Sales_Return') And TemplateName <>'CASH';

delete from item;
delete from ItemCategory;
delete from Unit;

delete from AdjustHistory;
delete from PaymentHistory;
delete from ReceiptPaymentStatus;

DELETE FROM OrganizationDetails;
DELETE FROM OrganizationAddress;

update LedgerStatus set OpeningBalance=0,CurrentBalance=0;
update VoucherSettings set VoucherNoStart=null,VoucherStartFrom=1;

delete from UserControl where UserName<>'Admin'