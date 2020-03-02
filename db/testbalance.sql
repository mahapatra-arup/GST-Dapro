----------------------------------- Find All Total-----------------------------
--select SUM(CurrentBalance) as Sundry_Creditors from LedgerStatus 
--inner join ladgermain on ladgermain.LadgerID=LedgerStatus.LedgerID where SubAccount='Sundry Creditors'

--select TemplateName,CurrentBalance,LedgerID from LedgerStatus 
--inner join ladgermain on ladgermain.LadgerID=LedgerStatus.LedgerID where SubAccount='Purchase A/C'

--select SUM(TotalAmount) as PurchaseBilltotal from PurchaseBill
 
--select SUM(amount_dr) as transection from Transection where LedgerIdFrom='7D107079-B88A-4D01-8E13-3D8C145EB0C4'

---------------------------------- Find ledger wise ----------------------

--select TemplateName,slno,totalamount,PurchaseBill.LedgerId from PurchaseBill 
--inner join ladgermain on ladgermain.LadgerID=PurchaseBill.LedgerId where TemplateName='MIMUL (9476110259)' order by slno
 
select TemplateName,CurrentBalance,LedgerID from LedgerStatus 
inner join ladgermain on ladgermain.LadgerID=LedgerStatus.LedgerID where SubAccount='Sundry Creditors'

select SUM(TotalAmount) as partytotalinpurchse from PurchaseBill   
inner join ladgermain on ladgermain.LadgerID=PurchaseBill.LedgerId where TemplateName='J.K RAKHIT & CO (3322101972)'


-----------update-------------------

--UPDAte LedgerStatus set CurrentBalance='-10472'  where LedgerID='EB6DC7D4-C458-48CB-9DA0-0E6F580C6A74'

-------count total ledger -----
--select COUNT(*) from LadgerMain where SubAccount='Sundry Creditors'

