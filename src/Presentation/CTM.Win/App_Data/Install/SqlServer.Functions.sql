USE [CTMDB]
GO
/****** Object:  UserDefinedFunction [dbo].[f_IDOperationInvestorVoteCheck]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_IDOperationInvestorVoteCheck]
GO
/****** Object:  UserDefinedFunction [dbo].[f_IDOperationInvestorAccuracyCheck]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_IDOperationInvestorAccuracyCheck]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetReasonCategoryNameWithParent]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetReasonCategoryNameWithParent]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetMaxNumber]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetMaxNumber]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetLastDayOfMonth]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetLastDayOfMonth]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIndustryFullName]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIndustryFullName]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDVoteType]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDVoteType]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDProfit]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDProfit]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDPositionVolume]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDPositionVolume]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVoteType]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationVoteType]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVoteStatus]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationVoteStatus]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVotePoint]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationVotePoint]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationStep]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationStep]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationApplyType]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationApplyType]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationAccuracyStatus]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationAccuracyStatus]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationAccuracyPoint]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDOperationAccuracyPoint]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDFStatus]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDFStatus]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDFinishConfirmFlag]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDFinishConfirmFlag]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDAvgCostPrice]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDAvgCostPrice]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDAccuracyEvaluateOperateNo]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetIDAccuracyEvaluateOperateNo]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetFirstDayOfMonth]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetFirstDayOfMonth]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetAccountOperatorNames]    Script Date: 2017/3/20 12:49:55 ******/
DROP FUNCTION [dbo].[f_GetAccountOperatorNames]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetAccountOperatorNames]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_GetAccountOperatorNames](@AccountId int)
RETURNS varchar(8000)
AS
BEGIN

    DECLARE @names varchar(8000)

    SET @names = ''

    SELECT @names = @names + ';' + U.Name
    FROM [dbo].UserInfo AS U
    WHERE U.Id IN (SELECT AO.OperatorId  FROM AccountOperator AS AO WHERE AO.AccountId = @AccountId) AND U.IsDeleted = 0

    RETURN STUFF(@names, 1, 1, '')
		
END





GO
/****** Object:  UserDefinedFunction [dbo].[f_GetFirstDayOfMonth]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_GetFirstDayOfMonth](@CurrentDate datetime)
RETURNS datetime
AS
BEGIN    

    RETURN DATEADD(m,DATEDIFF(m,0,@CurrentDate),0)	
		
END





GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDAccuracyEvaluateOperateNo]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDAccuracyEvaluateOperateNo]
(
	@ApplyNo varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
	
	DECLARE @operateNo varchar(50)

	IF (( SELECT COUNT(1) FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND IsStopped =0 AND ExecuteFlag = 3 AND (AccuracyStatus = 1 OR AccuracyStatus = 2)) = 1)
		SET @operateNo = (SELECT TOP 1 OperateNo FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND IsStopped =0 AND ExecuteFlag = 3 AND (AccuracyStatus = 1 OR AccuracyStatus = 2))
	ELSE 
		SET  @operateNo = ''

	RETURN @operateNo
END







GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDAvgCostPrice]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDAvgCostPrice]
(
	@ApplyNo varchar(50)
)
RETURNS decimal(18,4)
AS
BEGIN	

	DECLARE @costPrice decimal(18,4) = 0
	DECLARE @currentPosition decimal(24,0) = 0
	DECLARE @totalDealAmount decimal(24,4) = 0

	SELECT   @currentPosition =  ISNULL(SUM(DealVolume),0)
			,@totalDealAmount = ISNULL(SUM(DealPrice * DealVolume),0)
	FROM DailyRecord 
	WHERE Id IN ( 
					SELECT IDTR.DailyRecordId FROM InvestmentDecisionTradeRecord IDTR
					INNER JOIN InvestmentDecisionOperation IDO
					ON IDO.ApplyNo = IDTR.ApplyNo 
					WHERE IDO.ApplyNo = @ApplyNo AND IDO.IsDeleted = 0 AND IDO.IsStopped = 0
				)
	
	IF @currentPosition = 0 
		SET @costPrice = 0
	ELSE
		SET @costPrice = @totalDealAmount / @currentPosition


	RETURN @costPrice

END








GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDFinishConfirmFlag]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_GetIDFinishConfirmFlag] 
(
	@ApplyNo varchar(50)
)
RETURNS bit
AS
BEGIN
	
	DECLARE @finishFlag bit

	IF EXISTS ( SELECT 1 FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND IsStopped = 0 AND Step = 1 AND AccuracyStatus > 2 )
	   OR NOT EXISTS( SELECT 1 FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND IsStopped = 0 )
		SET @finishFlag = 1
	ELSE 
		BEGIN 
			DECLARE @positionVolume decimal(24,0)

			SELECT @positionVolume = ISNULL(SUM(DealVolume),-1)
			FROM DailyRecord DR
			WHERE DR.Id IN
						(
							SELECT IDTR.DailyRecordId
							FROM InvestmentDecisionTradeRecord IDTR
							INNER JOIN InvestmentDecisionOperation IDO
							ON IDO.OperateNo = IDTR.OperateNo
							WHERE IDO.ApplyNo = @ApplyNo AND IsDeleted =0 AND isStopped = 0 AND TradeRecordRelateFlag = 1 	
						)
			
			IF @positionVolume = 0
				SET @finishFlag = 1
			ELSE
				SET @finishFlag = 0
		END

	RETURN @finishFlag

END





GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDFStatus]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationAccuracyPoint]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDOperationAccuracyPoint]
(
	@OperateNo varchar(50)
)
RETURNS int
AS
BEGIN    
	
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --

	RETURN (SELECT ISNULL(SUM([Weight]),0)*100 FROM InvestmentDecisionOperationAccuracy WHERE Flag = 1 AND IsAdminVeto = 0 AND OperateNo = @OperateNo)
		
END







GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationAccuracyStatus]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDOperationAccuracyStatus]
(
	@OperateNo varchar(50)
)
RETURNS int
AS
BEGIN    

	-- 准确度Status：1-未评定 2-评定中 3-准确 4-不准确 --
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --

	DECLARE @status int = 0
	DECLARE @voteNumber int  = 0
	
	SELECT @voteNumber =  COUNT(1) FROM InvestmentDecisionOperationAccuracy WHERE Flag > 0

	IF(@voteNumber = 0) 
		SET @status = 1
	ELSE 
		BEGIN 
			DECLARE @adminVetoFlag int = 0
			SELECT @adminVetoFlag =  Flag FROM InvestmentDecisionOperationAccuracy WHERE IsAdminVeto = 1

			IF ( @adminVetoFlag = 1)
				SET @status = 3
			ELSE IF ( @adminVetoFlag = 2)
				SET @status =4
			ELSE
				BEGIN
					DECLARE @notVoteCommittee int 
					SELECT @notVoteCommittee = COUNT(1) FROM InvestmentDecisionOperationAccuracy WHERE IsAdminVeto = 0 AND Flag =0

					IF ( @notVoteCommittee > 0 )
						SET @status =2
					ELSE
						BEGIN
							DECLARE @point  int = 0
							SET @point = [dbo].[f_GetIDOperationAccuracyPoint](@OperateNo)
							IF(@point >= 60)
								SET @status = 3
							ELSE 
								SET @status = 4
						END
				END				
		END 
	
	RETURN @status
		
END







GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationApplyType]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_GetIDOperationApplyType]
(
	@ApplyNo varchar(50)
)
RETURNS int
AS
BEGIN

	--- 0:不可操作 
	--- 1：买入
	--- 2：卖出
	--- 99：买卖 

	DECLARE @applyType int = 0
	
	DECLARE @maxStep int = (SELECT MAX(Step) FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 )		

    IF EXISTS ( SELECT 1  FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0  AND IsStopped = 0 AND Step = 1 AND AccuracyStatus > 1)
		SET @applyType = 0
	ELSE IF NOT EXISTS ( SELECT 1 FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0  AND IsStopped = 0 AND Step = @maxStep AND ExecuteFlag = 2)
		SET @applyType = CASE( SELECT TOP 1 DealFlag FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0  AND Step = @maxStep) WHEN 1 THEN 1 WHEN 0 THEN 2 ELSE 0 END
	ELSE 
		SET @applyType = 99

	RETURN @applyType   		
END






GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationStep]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDOperationStep]
(
	@ApplyNo varchar(50)
)
RETURNS int
AS
BEGIN	

	DECLARE @currentStep int = 0
	
	DECLARE @maxStep int = (SELECT ISNULL(MAX(Step),0) FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 )
	
	IF EXISTS ( SELECT 1 FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND IsStopped = 0 AND Step = @maxStep AND ExecuteFlag = 2 )
		SET @currentStep = @maxStep + 1
	ELSE
		SET @currentStep = @maxStep

	RETURN @currentStep   		
END







GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVotePoint]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[f_GetIDOperationVotePoint]
(
	@OperateNo varchar(50)
)
RETURNS int
AS
BEGIN    
	
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --
	-- 投票Type：1-申请人 2-决策委员会 3-普通交易员 99-一票否决 -- 

	RETURN (SELECT ISNULL(SUM([Weight]),0)*100 FROM InvestmentDecisionOperationVote WHERE Flag = 1 AND ([Type] =1 OR [Type] =2) AND OperateNo = @OperateNo)
		
END






GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVoteStatus]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[f_GetIDOperationVoteStatus]
(
	@OperateNo varchar(50)
)
RETURNS int
AS
BEGIN    

	-- 投票Status：1-已提交 2-投票中 3-申请通过 4-申请不通过 --
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --
	-- 投票Type：1-申请人 2-决策委员会 3-普通交易员 99-一票否决 -- 

	DECLARE @status int = 0
	DECLARE @voteNumber int = 0	

	SELECT @voteNumber = COUNT(1) FROM InvestmentDecisionOperationVote WHERE OperateNo = @OperateNo  AND [Type] != 1 AND Flag > 0

	-- 除申请者之外没有投票信息
	IF(@voteNumber = 0) 
		SET @status = 1
	ELSE
		BEGIN		
			DECLARE @oneVoteVetoFlag int = 0
			SELECT @oneVoteVetoFlag = Flag FROM InvestmentDecisionOperationVote WHERE OperateNo = @OperateNo  AND [Type] = 99
			-- 一票否决者赞同
			IF (@oneVoteVetoFlag = 1)
				SET @status =  3 		
			ELSE IF (@oneVoteVetoFlag = 2)
				SET @status = 4
			ELSE
				BEGIN
					DECLARE @notVoteCommittee int = 0
					SELECT @notVoteCommittee = COUNT(1) FROM InvestmentDecisionOperationVote WHERE OperateNo = @OperateNo  AND [Type] = 2 AND Flag = 0					
				
					IF(@notVoteCommittee > 0) 
						SET @status =  2 		
					ELSE
						BEGIN									
							DECLARE @point int = 0
							SELECT @point = [dbo].[f_GetIDOperationVotePoint](@OperateNo)
							IF(@point >= 60) 
								SET @status = 3
							ELSE			
								SET @status = 4		
						END	
				END
		END

	RETURN @status
		
END






GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDOperationVoteType]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_GetIDOperationVoteType]
(
	@OperateNo varchar(50),
	@InvestorCode varchar(50),
	@IsAdminVeto bit
)
RETURNS int
AS
BEGIN    
	
	-- 投票Flag：0-未投票 1-赞同 2-反对 3-弃权 --
	-- 投票Type：1-申请人 2-决策委员会 3-普通交易员 99-一票否决 -- 

	DECLARE @voteType int = 0

	IF(@IsAdminVeto = 1) 
		SET @voteType = 99
	ELSE
		BEGIN			
			SELECT @voteType = [Type] FROM InvestmentDecisionOperationVote WHERE UserCode = @InvestorCode AND OperateNo = @OperateNo			
			IF(@voteType = 0 )
				BEGIN
					DECLARE @operateUser varchar(50)
					SELECT @operateUser = OperateUser  FROM InvestmentDecisionOperation  WHERE OperateNo = @OperateNo
					IF(ISNULL(@operateUser,'') = @InvestorCode)
						SET @voteType = 1
					ELSE
						SET @voteType = 3
				END		
		END

	RETURN @voteType
		
END





GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDPositionVolume]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetIDPositionVolume]
(
	@ApplyNo varchar(50)
)
RETURNS decimal(24,0)
AS
BEGIN	

	DECLARE @currentPosition decimal (24,0) = 0

	SELECT  @currentPosition = ISNULL(SUM(DealVolume),0)
	FROM DailyRecord 
	WHERE Id IN ( 
					SELECT IDTR.DailyRecordId FROM InvestmentDecisionTradeRecord IDTR
					INNER JOIN InvestmentDecisionOperation IDO
					ON IDO.ApplyNo = IDTR.ApplyNo 
					WHERE IDO.ApplyNo = @ApplyNo AND IDO.IsDeleted = 0 AND IDO.IsStopped = 0
				)
	
	RETURN @currentPosition

END








GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDProfit]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[f_GetIDProfit]
(
	@ApplyNo varchar(50)
)
RETURNS decimal(24,4)
AS
BEGIN
	
	
	DECLARE @profit decimal(24,4) = 0
	DECLARE @totalActualAmount decimal(24,4) = 0
	DECLARE @currentPosition decimal (24,0) = 0

	SELECT   @totalActualAmount = ISNULL(SUM(ActualAmount),0)
			,@currentPosition = ISNULL(SUM(DealVolume),0)
	FROM DailyRecord 
	WHERE Id IN ( 
					SELECT IDTR.DailyRecordId FROM InvestmentDecisionTradeRecord IDTR
					INNER JOIN InvestmentDecisionOperation IDO
					ON IDO.ApplyNo = IDTR.ApplyNo 
					WHERE IDO.ApplyNo = @ApplyNo AND IDO.IsDeleted = 0 AND IDO.IsStopped = 0
				)

	IF @currentPosition != 0 
		BEGIN
			DECLARE @latestClosePrice decimal(18,4) = ISNULL((SELECT T.[Close] FROM TKLineToday T WHERE TradeDate = CONVERT(varchar(10),GETDATE(),120) AND T.StockCode = (SELECT IDA.StockCode FROM InvestmentDecisionApplication IDA WHERE IDA.ApplyNo = @ApplyNo)),0)
			SET @profit = @totalActualAmount + @currentPosition * @latestClosePrice
		END
	ELSE
		SET @profit = @totalActualAmount

	RETURN @profit

END








GO
/****** Object:  UserDefinedFunction [dbo].[f_GetIDVoteType]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  UserDefinedFunction [dbo].[f_GetIndustryFullName]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[f_GetIndustryFullName]   
(
	@IndustryId int,
	@JointMark varchar(10) =N'->'
) 
RETURNS varchar(1000)  
AS  
BEGIN  

	DECLARE @tempName varchar(100)
	DECLARE @tempId int

	SELECT @tempId =ParentId, @tempName = Name FROM IndustryInfo WHERE Id = @IndustryId

	IF @tempId > 0
		SET @tempName = [dbo].[f_GetIndustryFullName](@tempId, @JointMark) + @JointMark + @tempName
  
   
	RETURN @tempName
END  







GO
/****** Object:  UserDefinedFunction [dbo].[f_GetLastDayOfMonth]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_GetLastDayOfMonth](@CurrentDate datetime)
RETURNS datetime
AS
BEGIN    

    RETURN DATEADD(d,-1,DATEADD(m,DATEDIFF(m,0,@CurrentDate)+1,0))	
		
END





GO
/****** Object:  UserDefinedFunction [dbo].[f_GetMaxNumber]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE FUNCTION [dbo].[f_GetMaxNumber]
(
	@Number1 decimal(24,4)
	,@Number2 decimal(24,4)
	,@Number3 decimal(24,4)
)
RETURNS decimal(24,4)
AS
BEGIN
	
	
	DECLARE @max decimal(24,4) = 0

	IF @Number1 > @Number2 
		SET @max = @Number1
	ELSE 
		SET @max = @Number2 

	IF @Number3 > @max
		SET @max = @Number3 

	RETURN @max

END









GO
/****** Object:  UserDefinedFunction [dbo].[f_GetReasonCategoryNameWithParent]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[f_GetReasonCategoryNameWithParent]   
(
	@CategoryId int,
	@JointMark varchar(10) =N'->'
) 
RETURNS varchar(1000)  
AS  
BEGIN  

	DECLARE @tempName varchar(100)
	DECLARE @tempId int

	SELECT @tempId =ParentId, @tempName = Name FROM DecisionReasonCategory WHERE Id = @CategoryId

	IF @tempId > 0
		SET @tempName = [dbo].[f_GetReasonCategoryNameWithParent](@tempId, @JointMark) + @JointMark + @tempName
  
   
	RETURN @tempName
END  






GO
/****** Object:  UserDefinedFunction [dbo].[f_IDOperationInvestorAccuracyCheck]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[f_IDOperationInvestorAccuracyCheck]
(
	 @InvestorCode varchar(50)
	,@OperateNo varchar(50)
)
RETURNS bit
AS
BEGIN	

	DECLARE @NeedVot bit = 0

	IF (EXISTS ( SELECT 1 FROM UserInfo WHERE Code = @InvestorCode AND IsAdmin = 1) OR EXISTS( SELECT 1 FROM InvestmentDecisionCommittee WHERE Code = @InvestorCode))
	AND EXISTS( SELECT 1 FROM InvestmentDecisionOperation WHERE OperateNo = @OperateNo AND (VoteStatus = 4 OR (VoteStatus = 3 AND ExecuteFlag = 3)))
		BEGIN
			DECLARE @accuracyStatus int = (SELECT AccuracyStatus FROM InvestmentDecisionOperation  WHERE OperateNo = @OperateNo)  

			IF (@accuracyStatus = 1 OR @accuracyStatus = 2) AND NOT EXISTS(SELECT 1 FROM InvestmentDecisionOperationAccuracy WHERE OperateNo =@OperateNo AND UserCode = @InvestorCode AND  IsAdminVeto = 0 AND Flag !=0)
				SET @NeedVot = 1
		END	

	RETURN @NeedVot
	
END








GO
/****** Object:  UserDefinedFunction [dbo].[f_IDOperationInvestorVoteCheck]    Script Date: 2017/3/20 12:49:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[f_IDOperationInvestorVoteCheck]
(
	 @InvestorCode varchar(50)
	,@OperateNo varchar(50)
)
RETURNS bit
AS
BEGIN	

	DECLARE @needvote bit = 0
	DECLARE @voteStatus int
	SELECT @voteStatus = VoteStatus FROM InvestmentDecisionOperation WHERE OperateNo = @OperateNo 

	IF(@voteStatus = 1 OR @voteStatus = 2) AND
	NOT EXISTS(SELECT 1 FROM InvestmentDecisionOperationVote WHERE OperateNo =@OperateNo AND UserCode = @InvestorCode AND [TYPE] != 99 AND Flag !=0)
		SET @needvote = 1

	RETURN @needvote
	
END








GO
