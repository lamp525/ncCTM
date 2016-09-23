USE [CTMDB]
GO



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


