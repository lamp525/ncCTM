
exec sp_configure 'show advanced options',1 
GO
reconfigure   
GO 

exec sp_configure 'Ad Hoc Distributed Queries',1
GO
reconfigure  
GO

USE [FinancialCenter]
GO

DECLARE @maxDate DATE

SELECT @maxDate =  ISNULL(MAX([TradeDate]), '2016-01-01') FROM [FinancialCenter].[dbo].[TKLine_Today]

PRINT @maxDate

INSERT [FinancialCenter].[dbo].[TKLine_Today]
SELECT * FROM OPENDATASOURCE(
         'SQLOLEDB',
         'Data Source=10.10.0.168;User ID=sa;Password=sa123'
         ).[FinancialCenter].[dbo].[TKLine_Today] AS FC
WHERE FC.TradeDate > @maxDate
GO

exec sp_configure 'Ad Hoc Distributed Queries',0
GO

reconfigure
GO

exec sp_configure 'show advanced options',0
GO

reconfigure 
GO
