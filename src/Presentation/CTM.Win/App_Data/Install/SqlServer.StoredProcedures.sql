USE [CTMDB]
GO

/*
/****** [sp_GetStockDailyClosePrices] ******/
*/
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

/*
/****** [sp_GetAccountDetail] ******/
*/

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


/*
/****** [sp_GetDiffBetweenDeliveryAndDailyData] ******/
*/

DROP PROCEDURE [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]
GO

CREATE PROCEDURE [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]
(
@AccountId int,
@DateFrom datetime,
@DateTo datetime
)
AS
BEGIN	

	SET NOCOUNT ON

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

END
GO


/*
/****** [sp_DeliveryAccountInvestIncomeDetail] ******/
*/

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
		(SUM(DR.ActualAmount) + ABS(SUM(DR.DealVolume)) * ISNULL(MAX(TT.[Close]),0))AccumulatedProfit	
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
		(SUM(DR.ActualAmount) + ABS(SUM(DR.DealVolume)) * ISNULL(MAX(TT.[Close]),0))AccumulatedProfit
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


/*
/****** [sp_AccountInvestFundDetail] ******/
*/

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

	DECLARE @currentMonth int = YEAR(@DateFrom) * 100 + MONTH(@DateFrom)
	DECLARE @firstDayOfMonth datetime = [dbo].[f_GetFirstDayOfMonth](@DateFrom)

	SELECT 
		AIF.AccountId,
		(ISNULL(AIF.Amount,0) + ISNULL(T.TransferAmount,0)) InitialAmount
	INTO #QueryInitial
	FROM AccountInitialFund AIF
	LEFT JOIN
	(
		SELECT 		
			AccountId, 		
			ISNULL(SUM(TransferAmount),0) TransferAmount 	
		FROM AccountFundTransfer 
		WHERE TransferDate BETWEEN @firstDayOfMonth AND @DateFrom
		GROUP BY AccountId
	)T
	ON T.AccountId = AIF.AccountId 
	WHERE AIF.[Month] = @currentMonth

	--SELECT * FROM #QueryInitial 

	SELECT 		
		AccountId, 
		FlowFlag,
		ISNULL(SUM(TransferAmount),0) TransferAmount 
	INTO #Transfer
	FROM AccountFundTransfer 
	WHERE TransferDate BETWEEN @DateFrom AND @DateTo
	GROUP BY AccountId, FlowFlag

	--SELECT * FROM #Transfer

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
	FROM #QueryInitial Q 
	LEFT JOIN #Transfer T1
	ON T1.AccountId =Q.AccountId  AND T1.FlowFlag =1
	LEFT JOIN #Transfer T0
	ON T0.AccountId =Q.AccountId  AND T0.FlowFlag = 0
	LEFT JOIN AccountInfo AI
	ON AI.Id = Q.AccountId 
	ORDER BY AccountName, SecurityCompanyName, AttributeName 
	
	/****** Drop Temp Table ******/
	DROP TABLE #QueryInitial 
	DROP TABLE #Transfer 
  
END
GO
