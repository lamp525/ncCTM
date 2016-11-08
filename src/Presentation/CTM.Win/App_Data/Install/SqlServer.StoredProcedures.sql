USE [CTMDB]
GO


/****** [dbo].[sp_AccountFundRevokeProcess] ******/
DROP PROCEDURE [dbo].[sp_AccountFundRevokeProcess]
GO
CREATE PROCEDURE [dbo].[sp_AccountFundRevokeProcess]
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @currentSettleMonth int = 0

	SELECT @currentSettleMonth = ISNULL(MAX([Month]),0)	FROM AccountInitialFund WHERE IsInitial = 0

	IF @currentSettleMonth = 0 	RETURN

	DELETE 
	FROM AccountInitialFund 
	WHERE IsInitial = 0 AND [Month] = @currentSettleMonth
	
 
END
GO


/****** [sp_AccountFundSettleProcess] ******/
DROP PROCEDURE [dbo].[sp_AccountFundSettleProcess]
GO
CREATE PROCEDURE [dbo].[sp_AccountFundSettleProcess]
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @currentSettleMonth int = 0

	SELECT @currentSettleMonth = ISNULL(MAX([Month]),0)	FROM AccountInitialFund WHERE IsInitial = 0

	IF @currentSettleMonth = 0 
	BEGIN
		SELECT @currentSettleMonth = ISNULL(MIN([Month]),0)	FROM AccountInitialFund WHERE IsInitial = 1
	END
	
	IF @currentSettleMonth = 0 	RETURN

	DECLARE @firstDayOfSettleMonth varchar(10) = CAST(@currentSettleMonth AS varchar) + '01'
	DECLARE @lastDayOfSettleMonth varchar(10) = [dbo].[f_GetLastDayOfMonth](@firstDayOfSettleMonth)

	DECLARE @initialMonth int = YEAR(DATEADD(M,1,@firstDayOfSettleMonth))*100 + MONTH(DATEADD(M,1,@firstDayOfSettleMonth))

	INSERT INTO AccountInitialFund (AccountId,AccountCode,[Month],Amount,IsInitial)
	SELECT 
		AIF.AccountId,
		AIF.AccountCode,
		@initialMonth,
		AIF.Amount + ISNULL(T.TransferAmount,0),
		0
	FROM AccountInitialFund AIF
	LEFT JOIN
	(
		SELECT 
			MAX(AccountId) AccountId,
			SUM(TransferAmount) TransferAmount
		FROM AccountFundTransfer
		WHERE TransferDate BETWEEN @firstDayOfSettleMonth AND @lastDayOfSettleMonth
	) T
	ON AIF.AccountId = t.AccountId 
	WHERE AIF.[Month] = @currentSettleMonth
 
END
GO


/****** [sp_AccountInvestFundDetail] ******/
DROP PROCEDURE [dbo].[sp_AccountInvestFundDetail]
GO
CREATE PROCEDURE [dbo].[sp_AccountInvestFundDetail]
(
@DateFrom datetime,
@DateTo datetime
)	
AS
BEGIN	

	SET NOCOUNT ON
	
	/****** 账户最新月结信息 ******/
	SELECT 
		MAX(AccountId) AccountId,
		MAX(Amount) Amount,
		MAX([Month]) SettleMonth,
		(CAST(MAX([Month]) AS varchar) + '01') FirstDayOfSettleMonth
	INTO #Settle
	FROM AccountInitialFund 
	GROUP BY AccountId


	/****** 开始查询日期的账户资金信息 ******/
	SELECT 		
		MAX(S.AccountId) AccountId, 		
		(ISNULL(MAX(S.Amount),0) + ISNULL(SUM(T.TransferAmount),0)) InitialAmount	
	INTO #QueryInitial
	FROM #Settle S 	
	LEFT JOIN AccountFundTransfer T
	ON T.AccountId = S.AccountId AND T.TransferDate BETWEEN S.FirstDayOfSettleMonth AND @DateFrom
	GROUP BY S.AccountId		
	

	/****** 查询时间段的账户资金调拨信息 ******/
	SELECT 		
		AccountId, 
		FlowFlag,
		ISNULL(SUM(TransferAmount),0) TransferAmount 
	INTO #Transfer
	FROM AccountFundTransfer 
	WHERE TransferDate BETWEEN @DateFrom AND @DateTo
	GROUP BY AccountId, FlowFlag
	

	/****** 账户投资资金明细查询结果 ******/
	SELECT 
		AI.Id AccountId, 
		AI.Code AccountCode,
		AI.Name AccountName, 
		AI.SecurityCompanyName, 
		AI.AttributeName,
		ISNULL(Q.InitialAmount,0) InitialAmount,
		ISNULL(T1.TransferAmount,0) InAmount, 
		ISNULL(T0.TransferAmount,0) OutAmount, 
		(ISNULL(Q.InitialAmount,0) + ISNULL(T1.TransferAmount,0) + ISNULL(T0.TransferAmount,0)) FinalAmount
	FROM  #QueryInitial Q 
	LEFT JOIN  AccountInfo AI
	ON AI.Id = Q.AccountId 
	LEFT JOIN #Transfer T1
	ON T1.AccountId =Q.AccountId  AND T1.FlowFlag =1
	LEFT JOIN #Transfer T0
	ON T0.AccountId =Q.AccountId  AND T0.FlowFlag = 0
	ORDER BY AccountName, SecurityCompanyName, AttributeName 
	
	/****** Drop Temp Table ******/
	DROP TABLE #Settle 
	DROP TABLE #QueryInitial 
	DROP TABLE #Transfer 
	
  
END
GO


/****** [sp_DeliveryAccountInvestIncomeDetail] ******/
DROP PROCEDURE [dbo].[sp_DeliveryAccountInvestIncomeDetail]
GO
CREATE PROCEDURE [dbo].[sp_DeliveryAccountInvestIncomeDetail]
(
@DateFrom datetime,
@DateTo datetime
)
AS
BEGIN	

	SET NOCOUNT ON
	
	SELECT 	
		MAX(DR.AccountId) AccId,
		MAX(DR.StockCode) SCode, 
		MAX(DR.StockName) SName, 
		SUM(DR.ActualAmount) TotalActualAmount,
		SUM(DR.DealVolume) HoldingVolume,
		ISNULL(MAX(TT.[Close]),0) LatestPrice,
		(ABS(SUM(DR.DealVolume)) * ISNULL(MAX(TT.[Close]),0))PositionValue,
		(SUM(DR.ActualAmount) + SUM(DR.DealVolume) * ISNULL(MAX(TT.[Close]),0))AccumulatedProfit	
	INTO #TableEnd
	FROM DeliveryRecord DR
	LEFT JOIN TKLineToday TT
	ON DR.StockCode = TT.StockCode AND TT.TradeDate = @DateTo
	WHERE DR.TradeDate <= @DateTo
	GROUP BY DR.AccountId, DR.StockCode

	SELECT 	
		MAX(DR.AccountId) AccId,
		MAX(DR.StockCode) SCode, 
		--SUM(DR.ActualAmount) TotalActualAmount,
		--SUM(DR.DealVolume) HoldingVolume,
		--ISNULL(MAX(TT.[Close]),0) LatestPrice,
		--(ABS(SUM(DR.DealVolume)) * ISNULL(MAX(TT.[Close]),0))PositionValue,
		(SUM(DR.ActualAmount) + SUM(DR.DealVolume) * ISNULL(MAX(TT.[Close]),0))AccumulatedProfit
	INTO #TableStart
	FROM DeliveryRecord DR
	LEFT JOIN TKLineToday TT
	ON DR.StockCode = TT.StockCode AND TT.TradeDate = DATEADD(DAY,-1,@DateFrom)
	WHERE DR.TradeDate < @DateFrom
	GROUP BY DR.AccountId, DR.StockCode

	--SELECT COUNT(*)START_NUM FROM #TableStart
	--SELECT COUNT(*)END_NUM FROM #TableEnd 

	SELECT 
		(CONVERT(varchar(10),@DateFrom,111) + ' - ' +  CONVERT(varchar(10),@DateTo,111)) QueryPeriod,
		(AI.Name + ' - ' + AI.SecurityCompanyName + ' - ' + AI.AttributeName + ' - ' + AI.TypeName) AccountDetail,
		(E.SCode + ' - ' + E.SName) StockDetail,	
		CAST(E.HoldingVolume AS decimal(18,0)) HoldingVolume,
		E.LatestPrice,
		E.PositionValue,
		ISNULL((E.AccumulatedProfit - S.AccumulatedProfit),0) Profit,
		E.AccumulatedProfit,	
		E.SCode StockCode,
		E.SName StockName,	
		AI.Id AccountId,
		AI.Name AccountName,
		AI.SecurityCompanyName,
		AI.AttributeName 	
	FROM #TableStart S
	FULL JOIN #TableEnd E
	ON S.AccId = E.AccId AND S.SCode = E.SCode
	LEFT JOIN AccountInfo AI
	ON E.AccId = AI.Id
	ORDER BY AccountDetail, StockDetail

	DROP TABLE #TableStart
	DROP TABLE #TableEnd

END
GO


/****** [sp_GenerateMTFDetail] ******/
DROP PROCEDURE [dbo].[sp_GenerateMTFDetail]
GO
CREATE PROCEDURE [dbo].[sp_GenerateMTFDetail]
(
	@InvestorCode varchar(20),
	@ForecastDate datetime
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @serialNo varchar(50) = (SELECT SerialNo FROM MarketTrendForecastInfo WHERE ForecastDate = @ForecastDate)

	IF(ISNULL(@serialNo,'') = '') RETURN
	
	DECLARE @detailCount int  = (SELECT COUNT(*) FROM MarketTrendForecastDetail WHERE SerialNo = @serialNo AND InvestorCode = @InvestorCode )

	IF(@detailCount = 0)
		BEGIN 

			DECLARE @weight decimal(18,4)
			SELECT @weight = [Weight] FROM InvestmentDecisionCommittee WHERE Code = @InvestorCode
			IF(ISNULL(@weight,0) = 0)
				SET @weight = 0.0000

			INSERT INTO MarketTrendForecastDetail(SerialNo,ForecastDate,InvestorCode,[Weight],CreateTime)	
			VALUES(@serialNo,@ForecastDate,@InvestorCode,@weight,GETDATE())					
		END

	SELECT * FROM [v_MTFDetail] WHERE ForecastDate = @ForecastDate ORDER BY InvestorCode
	 
END
GO


/******  [sp_GenerateMTFInfo] ******/
DROP PROCEDURE [dbo].[sp_GenerateMTFInfo]
GO
CREATE PROCEDURE [dbo].[sp_GenerateMTFInfo]
(
	@ForecastDate datetime
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @serialNo varchar(50)
	SELECT @serialNo =SerialNo FROM MarketTrendForecastInfo WHERE ForecastDate = @ForecastDate

	IF(ISNULL(@serialNo,'') = '')
		BEGIN
			DECLARE @maxSerialNo varchar(50) = (SELECT MAX(SerialNo) FROM MarketTrendForecastInfo)			

			DECLARE @suffix varchar(10)
			IF( ISNULL(@maxSerialNo,'') = '')
				SET @suffix = '000001'
			ELSE
				SET @suffix =RIGHT('000000'+ CAST((CAST(SUBSTRING(@maxSerialNo,LEN(@maxSerialNo) - 5,6) AS int) + 1) AS varchar),6)

			SET @serialNo = 'YC' + @suffix

			INSERT INTO MarketTrendForecastInfo (SerialNo,ForecastDate,CreateTime,Result)
			VALUES(@serialNo,@ForecastDate,GETDATE(),NULL)		

		END
 
END
GO


/****** [sp_GeneratePSADetail] ******/
DROP PROCEDURE [dbo].[sp_GeneratePSADetail]
GO
CREATE PROCEDURE [dbo].[sp_GeneratePSADetail]
(
	@InvestorCode varchar(20),
	@AnalysisDate datetime
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @serialNo varchar(50) = (SELECT SerialNo FROM PositionStockAnalysisInfo WHERE AnalysisDate = @AnalysisDate)

	IF(ISNULL(@serialNo,'') = '') RETURN
	
	DECLARE @detailCount int  = (SELECT COUNT(*) FROM PositionStockAnalysisDetail WHERE SerialNo = @serialNo AND InvestorCode = @InvestorCode )

	IF(@detailCount = 0)
		BEGIN 
			INSERT INTO PositionStockAnalysisDetail(SerialNo, InvestorCode,AnalysisDate, StockCode,StockName,TradeType,Decision,DealAmount,DealRange,CreateTime)
			SELECT 
				@serialNo,
				@InvestorCode,
				@AnalysisDate,
				P.StockCode,
				P.StockName,
				0,
				'0',
				0,
				0,
				GETDATE()			 
			FROM InvestmentDecisionStockPool P

			INSERT INTO PositionStockAnalysisSummary(SerialNo,AnalysisDate,Principal,StockCode,StockName,TradeType,Decision,DealAmount,DealRange,CreateTime)
			SELECT 
				@serialNo,
				@AnalysisDate,
				P.Principal,
				P.StockCode,
				P.StockName,
				0,
				'0',
				0,
				0,
				GETDATE()
			FROM InvestmentDecisionStockPool P
			WHERE P.StockCode NOT IN (SELECT StockCode FROM PositionStockAnalysisSummary WHERE SerialNo = @serialNo )
		END

	SELECT * FROM V_PSADetail WHERE AnalysisDate = @AnalysisDate AND InvestorCode = @InvestorCode
	 
END
GO

/****** [sp_GeneratePSAInfo] ******/
DROP PROCEDURE [dbo].[sp_GeneratePSAInfo]
GO
CREATE PROCEDURE [dbo].[sp_GeneratePSAInfo]
(	
	@AnalysisDate datetime
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @serialNo varchar(50)
	SELECT @serialNo =SerialNo FROM PositionStockAnalysisInfo WHERE AnalysisDate = @AnalysisDate

	IF(ISNULL(@serialNo,'') = '')
		BEGIN
			DECLARE @maxSerialNo varchar(50) = (SELECT MAX(SerialNo) FROM PositionStockAnalysisInfo)			

			DECLARE @suffix varchar(10)
			IF( ISNULL(@maxSerialNo,'') = '')
				SET @suffix = '000001'
			ELSE
				SET @suffix =RIGHT('000000'+ CAST((CAST(SUBSTRING(@maxSerialNo,LEN(@maxSerialNo) - 5,6) AS int) + 1) AS varchar),6)

			SET @serialNo = 'FX' + @suffix

			INSERT INTO PositionStockAnalysisInfo (SerialNo,AnalysisDate,CreateTime,Result)
			VALUES(@serialNo,@AnalysisDate, GETDATE(),NULL)		
			
			INSERT INTO PositionStockAnalysisSummary(SerialNo,AnalysisDate,Principal,StockCode,StockName,TradeType,Decision,DealAmount ,DealRange,CreateTime)
			SELECT 
				@serialNo,
				@AnalysisDate,
				P.Principal,
				P.StockCode,
				P.StockName,
				0,
				'0',
				0,
				0,
				GETDATE()
			FROM InvestmentDecisionStockPool P
		END
 
END
GO

/****** [sp_GetAccountDetail] ******/
DROP PROCEDURE [dbo].[sp_GetAccountDetail]
GO
CREATE PROCEDURE  [dbo].[sp_GetAccountDetail]
(
@IndustyId int = 0,
@AccountIds varchar(1000) = null,
@SecurityCode int = 0,
@AttributeCode int = 0,
@PlanCode int = 0,
@TypeCode int = 0,
@OnlyNeedAccounting bit = 0,
@ShowDisabled bit = 0
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @commandText varchar(8000)

	SET @commandText = N'SELECT IndustryName =II.Name, OperatorNames =[dbo].[f_GetAccountOperatorNames](AI.Id), AI.*, DisplayMember = AI.Name + '' - '' + AI.SecurityCompanyName + '' - '' +AI.AttributeName + '' - '' +AI.TypeName
											 FROM [dbo].[AccountInfo]  AS AI 
											 LEFT JOIN [dbo].[IndustryInfo] AS II ON II.Id = AI.IndustryId
											 WHERE '

	IF( @IndustyId > 0 )
		SET @commandText +=N'#AND AI.IndustryId = ' + CAST(@IndustyId AS varchar(8))
	IF(LEN(@AccountIds)>0)
		SET @commandText +=N'#AND AI.Id IN(' + @AccountIds +')'
	IF( @SecurityCode > 0 )
		SET @commandText +=N'#AND AI.SecurityCompanyCode = ' +  CAST(@SecurityCode AS varchar(8))  
	IF( @AttributeCode > 0 )
		SET @commandText +=N'#AND AI.AttributeCode = ' +  CAST(@AttributeCode AS varchar(8))  
	IF( @PlanCode > 0 )
		SET @commandText +=N'#AND AI.PlanCode = ' +  CAST(@PlanCode AS varchar(8))  
	IF( @TypeCode > 0 )
		SET @commandText +=N'#AND AI.TypeCode = ' +  CAST(@TypeCode AS varchar(8))  
	IF( @OnlyNeedAccounting = 1 )
		SET @commandText +=N'#AND AI.NeedAccounting = 1'  
	IF( @ShowDisabled = 0 )
		SET @commandText +=N'#AND AI.IsDisabled = 0' 

	-- PRINT N'Before CommandText: ' + @commandText		

	DECLARE @sharpIndex int 
	SET @sharpIndex = CHARINDEX('#',@commandText)

	IF(@sharpIndex > 0)
		BEGIN
			SET @commandText=SUBSTRING(@commandText,1,@sharpIndex - 1) + SUBSTRING(@commandText,@sharpIndex + 4,LEN(@commandText))
			SET @commandText =REPLACE(@commandText,'#',' ')
		END
	ELSE
		BEGIN
			SET @commandText=REPLACE(@commandText,'WHERE',' ')			
		END	
		
	 -- PRINT N'After CommandText: ' + @commandText	

	EXEC( @commandText )
	
END
GO


/****** [sp_GetDiffBetweenDeliveryAndDailyData] ******/
DROP PROCEDURE [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]
GO
CREATE PROCEDURE [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]
(
@DisplayType int, -- 0:Detail 1:Summary --
@AccountId int,
@DateFrom datetime,
@DateTo datetime
)
AS
BEGIN	

	SET NOCOUNT ON

	IF(@DisplayType = 0)
		SELECT	
			Delivery.* 
			,(ABS(ISNULL(Delivery.DE_TotalActualAmount,0)) - ABS(ISNULL(Daily.DA_TotalActualAmount,0)))AmountDiff
			,(ABS(ISNULL(Delivery.DE_TotalDealVolume,0)) - ABS(ISNULL(Daily.DA_TotalDealVolume,0))) VolumeDiff
			,Daily.*
		FROM
		(
			SELECT 
				TradeDate DE_TradeDate,
				StockCode DE_StockCode,
				MAX(StockName) DE_StockName, 
				DealFlag DE_DealFlag,
				CASE DealFlag 
					WHEN 1 THEN '买入'
					WHEN 0 THEN '卖出'
				END DE_DealName,
				SUM(ActualAmount) DE_TotalActualAmount,
				SUM(DealVolume) DE_TotalDealVolume
			FROM DeliveryRecord 
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo 
			GROUP BY TradeDate,StockCode,DealFlag 
		) Delivery
		FULL JOIN 
		(
			SELECT 
				TradeDate DA_TradeDate,
				StockCode DA_StockCode,
				MAX(StockName) DA_StockName, 
				DealFlag DA_DealFlag,
				CASE DealFlag 
					WHEN 1 THEN '买入'
					WHEN 0 THEN '卖出'
				END DA_DealName,
				SUM(ActualAmount) DA_TotalActualAmount,
				SUM(DealVolume) DA_TotalDealVolume
			FROM DailyRecord 
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo 
			GROUP BY TradeDate,StockCode,DealFlag 
		) Daily 
		ON Daily.DA_TradeDate=Delivery.DE_TradeDate AND Daily.DA_StockCode=Delivery.DE_StockCode AND Daily.DA_DealFlag = Delivery.DE_DealFlag
		ORDER BY Delivery.DE_StockCode, Delivery.DE_TradeDate

	ELSE

		SELECT	
			Delivery.* 
			,(ABS(ISNULL(Delivery.DE_TotalActualAmount,0)) - ABS(ISNULL(Daily.DA_TotalActualAmount,0)))AmountDiff
			,(ABS(ISNULL(Delivery.DE_TotalDealVolume,0)) - ABS(ISNULL(Daily.DA_TotalDealVolume,0))) VolumeDiff
			,Daily.*
		FROM
		(
			SELECT 
				NULL DE_TradeDate,
				StockCode DE_StockCode,
				MAX(StockName) DE_StockName, 
				DealFlag DE_DealFlag,
				CASE DealFlag 
					WHEN 1 THEN '买入'
					WHEN 0 THEN '卖出'
				END DE_DealName,
				SUM(ActualAmount) DE_TotalActualAmount,
				SUM(DealVolume) DE_TotalDealVolume
			FROM DeliveryRecord 
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo 
			GROUP BY StockCode,DealFlag 
		) Delivery
		FULL JOIN 
		(
			SELECT 
				NULL DA_TradeDate,
				StockCode DA_StockCode,
				MAX(StockName) DA_StockName, 
				DealFlag DA_DealFlag,
				CASE DealFlag 
					WHEN 1 THEN '买入'
					WHEN 0 THEN '卖出'
				END DA_DealName,
				SUM(ActualAmount) DA_TotalActualAmount,
				SUM(DealVolume) DA_TotalDealVolume
			FROM DailyRecord 
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo 
			GROUP BY StockCode,DealFlag 
		) Daily 
		ON Daily.DA_StockCode=Delivery.DE_StockCode AND Daily.DA_DealFlag = Delivery.DE_DealFlag
		ORDER BY Delivery.DE_StockCode
END
GO

/****** [sp_GetIDVoteResult] ******/
DROP PROCEDURE [dbo].[sp_GetIDVoteResult]
GO
CREATE PROCEDURE [dbo].[sp_GetIDVoteResult]
(
@FormSerialNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT 
		--ROW_NUMBER() OVER( ORDER BY V.UserCode) 编号,
		U.Code InvestorCode,
		U.Name InvestorName,
		CAST(CAST(V.[Weight]*100 AS numeric(18,2)) AS varchar ) + '%' WeightPercentage,
		CASE V.Flag
			WHEN 0 THEN '未投票'
			WHEN 1 THEN '赞同'
			WHEN 2 THEN '反对'
			WHEN 3 THEN '弃权'
		END	FlagName,
		CASE V.[Type]
			WHEN 1 THEN '申请人'
			WHEN 2 THEN '决策委员会'
			WHEN 3 THEN '其他交易员'
			WHEN 99 THEN '一票否决'
		END	TypeName,
		V.Reason,
		CASE V.Flag
			WHEN 0 THEN NULL
			ELSE V.VoteTime
		END VoteTime,
		''  ConfirmTime
	FROM InvestmentDecisionVote V
	LEFT JOIN UserInfo U
	ON V.UserCode = U.Code	
	WHERE V.FormSerialNo =@FormSerialNo AND((V.Flag != 0) OR (V.Flag = 0 AND V.[Type] != 3))
	ORDER BY V.[Type], V.UserCode
 
END
GO


/****** [sp_GetInvestmentDecisionForm] ******/
DROP PROCEDURE [dbo].[sp_GetInvestmentDecisionForm]
GO
CREATE PROCEDURE [dbo].[sp_GetInvestmentDecisionForm]
AS
BEGIN

	SET NOCOUNT ON

	SELECT 	
		D.Name DepartmentName,
		CASE IDF.[Status]
			WHEN 1 THEN '已提交'
			WHEN 2 THEN '进行中'
			WHEN 3 THEN '申请通过'
			WHEN 4 THEN '申请不通过'
		END StatusName,
		CASE IDF.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
		END TradeTypeName,
		CASE IDF.DealFlag 
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
		END DealFlagName,
		U.Name ApplyUserName,
		CASE IDF.[Status]
			WHEN 3 THEN CAST(IDF.Point AS decimal(18,0)) 
			WHEN 4 THEN CAST(IDF.Point AS decimal(18,0)) 
			ELSE CAST(IDF.Point AS decimal(18,0)) 
		END Point,
		PriceBoundPercentage = CAST(CAST(IDF.PriceBound * 100 AS numeric(10,0)) AS varchar) + '% ' + '(' + CAST(CAST((1 - IDF.PriceBound) * IDF.Price AS decimal(18,2)) AS varchar) + ' - ' + CAST(CAST((1 + IDF.PriceBound) * IDF.Price AS numeric(18,2)) AS varchar) + ')',
		IDF.SerialNo,
		IDF.ApplyUser,
		IDF.ApplyDate,
		IDF.StockFullCode,
		IDF.StockName,
		IDF.RelateTradePlanNo,
		CAST(IDF.Profit /10000 AS decimal(24,2)) Profit,
		CAST(IDF.Price AS decimal(18,2)) Price,
		IDF.Volume,
		CAST(IDF.Amount /10000 AS decimal(24,2)) Amount,
		IDF.[Status],
		IDF.Reason,
		IDF.CreateTime
	FROM InvestmentDecisionForm	IDF
	LEFT JOIN UserInfo U
	ON IDF.ApplyUser = U.Code
	LEFT JOIN DepartmentInfo D
	ON IDF.DepartmentId = D.Id 	
	ORDER BY IDF.SerialNo DESC
END
GO


/****** [sp_GetStockDailyClosePrices] ******/
DROP PROCEDURE [dbo].[sp_GetStockDailyClosePrices]
GO
CREATE PROCEDURE [dbo].[sp_GetStockDailyClosePrices]
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @loopDate datetime 
	DECLARE @nowDate date =GETDATE()

	SELECT @loopDate = ISNULL(MAX([TradeDate]), '2015-12-31') FROM [dbo].[TKLineToday]
	
	WHILE(@loopDate < @nowDate)
		BEGIN
			SET @loopDate =DATEADD(DAY,1,@loopDate)
			/* PRINT @loopDate */

			INSERT [dbo].[TKLineToday]
			SELECT [StockCode] 
						,[TradeDate]= CONVERT(datetime,@loopDate,120) 
						,[Close] 
			FROM
					(
					 SELECT [StockCode]
								 ,[TradeDate] 
								 ,[Close]  
								 ,ROW_NUMBER() OVER(PARTITION BY StockCode ORDER BY TradeDate DESC) RowNumber 
					 FROM [FinancialCenter].[dbo].[TKLine_Today] 
					 WHERE   [TradeDate] < DATEADD(DAY,1,@loopDate)
					)  AS t
			WHERE t.RowNumber =1					
		END

	IF(@loopDate = @nowDate)
		BEGIN
			DELETE FROM [dbo].[TKLineToday] WHERE [TradeDate] = @nowDate
			/* PRINT N'The Current Date Close Price Has Been Deleted !!!' */
			
			INSERT [dbo].[TKLineToday]
			SELECT [StockCode] 
						,[TradeDate]= CONVERT(datetime,@loopDate,120) 
						,[Close] 
			FROM
					(
					 SELECT [StockCode]
								 ,[TradeDate] 
								 ,[Close]  
								 ,ROW_NUMBER() OVER(PARTITION BY StockCode ORDER BY TradeDate DESC) RowNumber 
					 FROM [FinancialCenter].[dbo].[TKLine_Today] 
					 WHERE [TradeDate] < DATEADD(DAY,1,@loopDate)
					)  AS t
			WHERE t.RowNumber =1
			/* PRINT N'The Current Date Close Price Has Been Inserted !!!' */
		END

END
GO


/****** [sp_InvestmentDecisionVoteProcess] ******/
DROP PROCEDURE [dbo].[sp_InvestmentDecisionVoteProcess]
GO
CREATE PROCEDURE [dbo].[sp_InvestmentDecisionVoteProcess]
(
@InvestorCode varchar(50),
@FormSerialNo varchar(50),
@VoteFlag int,
@Reason varchar(1000)
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @voteNumber int = 0

	SELECT @voteNumber = COUNT(UserCode) FROM InvestmentDecisionVote WHERE UserCode = @InvestorCode AND FormSerialNo = @FormSerialNo

	IF(@voteNumber = 0)
		BEGIN
			INSERT INTO InvestmentDecisionVote (AuthorityLevel,Flag,FormSerialNo,Reason,[Type], UserCode,VoteTime,[Weight])
			VALUES(0,@VoteFlag,@FormSerialNo,@Reason,[dbo].[f_GetIDVoteType](@FormSerialNo,@InvestorCode),@InvestorCode,GETDATE(),0)
		END
	ELSE
		BEGIN
			UPDATE InvestmentDecisionVote 
			SET Flag = @VoteFlag, Reason = @Reason, VoteTime = GETDATE()
			WHERE UserCode = @InvestorCode AND FormSerialNo = @FormSerialNo
		END

	UPDATE InvestmentDecisionForm 
	SET 
		Point =	(SELECT SUM([Weight])*100 FROM InvestmentDecisionVote WHERE Flag = 1 AND ([Type] =1 OR [Type] =2) AND FormSerialNo = @FormSerialNo),
		[Status] = [dbo].[f_GetIDFStatus](@FormSerialNo), 
		UpdateTime = GETDATE()
	WHERE SerialNo = @FormSerialNo 
 
END
GO
