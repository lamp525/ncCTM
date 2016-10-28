USE [CTMDB]
GO


/*
/****** 1. [f_GetAccountOperatorNames] ******/
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
/****** 2. [f_GetFirstDayOfMonth] ******/
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
/****** 3. [f_GetLastDayOfMonth] ******/
*/
DROP FUNCTION [dbo].[f_GetLastDayOfMonth]
GO
CREATE FUNCTION [dbo].[f_GetLastDayOfMonth](@CurrentDate datetime)
RETURNS datetime
AS
BEGIN    

    RETURN DATEADD(d,-1,DATEADD(m,DATEDIFF(m,0,@CurrentDate)+1,0))	
		
END
GO


/*
/****** 4. [f_GetIDFStatus] ******/
*/
DROP FUNCTION [dbo].[f_GetIDFStatus]
GO
CREATE FUNCTION [dbo].[f_GetIDFStatus](@SerialNo varchar(50))
RETURNS int
AS
BEGIN    

	-- 申请单Status：1-已提交 2-进行中 3-申请通过 4-申请不通过 --
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --
	-- 投票Type：1-申请人 2-决策委员会 3-普通交易员 99-一票否决 -- 

	DECLARE @status int = 0

	DECLARE @voteNumber int = 0	
	SELECT @voteNumber = COUNT(UserCode) FROM InvestmentDecisionVote WHERE FormSerialNo = @SerialNo  AND [Type] != 1 AND Flag > 0

	-- 除申请者之外没有投票信息
	IF(@voteNumber = 0) 
		SET @status = 1
	ELSE
		BEGIN		
			DECLARE @notVoteCommittee int = 0
			SELECT @notVoteCommittee = COUNT(UserCode) FROM InvestmentDecisionVote 	WHERE FormSerialNo = @SerialNo  AND [Type] = 2 AND Flag = 0					
				
			IF(@notVoteCommittee > 0) 
				SET @status =  2 		
			ELSE
				BEGIN		
					DECLARE @lastVoteTime  datetime 
					SELECT @lastVoteTime = MAX(VoteTime) FROM InvestmentDecisionVote WHERE FormSerialNo = @SerialNo  AND ([Type] = 2 OR [Type] = 99) AND Flag !=0

					IF(1 = 2 )--(DATEDIFF(MI,@lastVoteTime,GETDATE()) <= 5)
						SET @status =  2 	
					ELSE
						BEGIN
							DECLARE @point int = 0
							SELECT @point = SUM([Weight]) *100	FROM InvestmentDecisionVote 	WHERE FormSerialNo = @SerialNo AND Flag = 1	
							IF(@point >= 60) 
								SET @status = 3
							ELSE			
								SET @status = 4

							DECLARE @oneVoteVetoFlag int = 0
							SELECT @oneVoteVetoFlag = Flag FROM InvestmentDecisionVote WHERE FormSerialNo = @SerialNo  AND [Type] = 99
							-- 一票否决者赞同
							IF (@oneVoteVetoFlag = 1)
								SET @status =  3 		
							ELSE IF (@oneVoteVetoFlag = 2)
								SET @status = 4
						END							
				END	
		END

	RETURN @status
		
END
GO

/*
/****** 5. [f_GetIDVoteType] ******/
*/
DROP FUNCTION [dbo].[f_GetIDVoteType]
GO
CREATE FUNCTION [dbo].[f_GetIDVoteType]
(
@SerialNo varchar(50),
@InvestorCode varchar(50)
)
RETURNS int
AS
BEGIN    

	-- 申请单Status：1-已提交 2-进行中 3-申请通过 4-申请不通过 --
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --
	-- 投票Type：1-申请人 2-决策委员会 3-普通交易员 99-一票否决 -- 

	DECLARE @voteType int = 0

	IF(@InvestorCode ='888888') 
		SET @voteType = 99
	ELSE
		BEGIN			
			SELECT @voteType = [Type] FROM InvestmentDecisionVote WHERE UserCode = @InvestorCode AND FormSerialNo = @SerialNo			
			IF(@voteType = 0 )
				BEGIN
					DECLARE @applicant varchar(50)
					SELECT @applicant = ApplyUser  FROM InvestmentDecisionForm  WHERE SerialNo = @SerialNo
					IF(@applicant = @InvestorCode)
						SET @voteType = 1
					ELSE
						SET @voteType = 3
				END		
		END

	RETURN @voteType
		
END
GO