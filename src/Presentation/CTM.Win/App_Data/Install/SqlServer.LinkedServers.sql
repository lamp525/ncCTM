USE [master]
GO

EXEC master.dbo.sp_dropserver @server=N'10.10.10.2\zb', @droplogins='droplogins'
GO

EXEC master.dbo.sp_addlinkedserver @server = N'10.10.10.2\zb', @srvproduct=N'FinancialCenterLink', @provider=N'SQLNCLI11', @datasrc=N'10.10.10.2\zb'

EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'10.10.10.2\zb',@useself=N'False',@locallogin=NULL,@rmtuser=N'sa',@rmtpassword='*******'

GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'collation compatible', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'data access', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'dist', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'pub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'rpc', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'rpc out', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'sub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'connect timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'collation name', @optvalue=null
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'lazy schema validation', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'query timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'use remote collation', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'10.10.10.2\zb', @optname=N'remote proc transaction promotion', @optvalue=N'true'
GO


