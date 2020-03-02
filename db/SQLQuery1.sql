select top(1)ID,Activation_Key from dbo.Dapro_ActivationInfo 
where Activation_Key = (SELECT Activation_Key FROM  Dapro_Activation 
where   Motherboard_ID = '                                 ' and MAC_Id = '00E04E206C18' 
and Dapro_Activation.HDD_No = '9E1FFC2F42E6F92EC2DB7D7006C6BC72'  
and Activated = 'true') 
and (Expiry_date >= CAST(Getdate() AS DATE)) order by ID desc