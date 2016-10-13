USE [CTMDB]
GO


/*
/****** [f_GetAccountOperatorNames] ******/
*/

DROP FUNCTION [dbo].[f_GetAccountOperatorNames]
GO

CREATE FUNCTION [dbo].[f_GetAccountOperatorNames](@AccountId int)
RETURNS varchar(8000)
AS
BEGIN

    DECLARE @names varchar(8000)

    SET @names = ''

    SELECT @names = @names + ';' + U.Name
    FROM [dbo].UserInfo AS U
    WHERE U.Id IN (SELECT AO.OperatorId  FROM AccountOperator AS AO WHERE AO.AccountId = @AccountId)

    RETURN STUFF(@names, 1, 1, '')
		
END
GO


/*
/****** [f_GetFirstDayOfMonth] ******/
*/

DROP FUNCTION [dbo].[f_GetFirstDayOfMonth]
GO

CREATE FUNCTION [dbo].[f_GetFirstDayOfMonth](@CurrentDate datetime)
RETURNS datetime
AS
BEGIN    

    RETURN DATEADD(m,DATEDIFF(m,0,@CurrentDate),0)	
		
END
GO


/*
/****** [f_GetLastDayOfMonth] ******/
*/

DROP FUNCTION [dbo].[f_GetLastDayOfMonth]
GO

CREATE FUNCTION [dbo].[f_GetLastDayOfMonth](@CurrentDate datetime)
RETURNS datetime
AS
BEGIN    

    RETURN DATEADD(d,-1,DATEADD(m,DATEDIFF(m,0,GETDATE())+1,0))	
		
END
GO

