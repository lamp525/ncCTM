update  AccountInfo 
set AttributeName = (select Name from DictionaryInfo as d  where d.Code = AccountInfo.AttributeCode and d.TypeId=2),
    TypeName =(select Name from DictionaryInfo as d  where d.Code = AccountInfo.TypeCode and d.TypeId=6),
	PlanName =(select Name from DictionaryInfo as d  where d.Code = AccountInfo.PlanCode and d.TypeId=5),
	SecurityCompanyName  =(select Name from DictionaryInfo as d  where d.Code = AccountInfo.SecurityCompanyCode and d.TypeId=1)
