USE [CTMDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_TITradeInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_TITradeInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_TIRecordProfit]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_TIRecordProfit]
GO
/****** Object:  StoredProcedure [dbo].[sp_TIKLineData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_TIKLineData]
GO
/****** Object:  StoredProcedure [dbo].[sp_TIDayProfit]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_TIDayProfit]
GO
/****** Object:  StoredProcedure [dbo].[sp_StockPositionQuery]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_StockPositionQuery]
GO
/****** Object:  StoredProcedure [dbo].[sp_MSDeliveryProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_MSDeliveryProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_MSDailyProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_MSDailyProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_InvestorProfitRiskInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_InvestorProfitRiskInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_InvestmentSubjectProfit]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_InvestmentSubjectProfit]
GO
/****** Object:  StoredProcedure [dbo].[sp_InvestmentSubjectEditProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_InvestmentSubjectEditProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_InvestmentDecisionVoteProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_InvestmentDecisionVoteProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationVoteProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_IDOperationVoteProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationExecuteProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_IDOperationExecuteProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationDeleteProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_IDOperationDeleteProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationAccuracyProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_IDOperationAccuracyProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStockProfitContrastData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetStockProfitContrastData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStockDailyClosePrices]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetStockDailyClosePrices]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLogInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetLogInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInvestmentDecisionForm]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetInvestmentDecisionForm]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDVoteResult]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDVoteResult]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationVoteResult]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDOperationVoteResult]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationTradeRecord]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDOperationTradeRecord]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationRelateRecord]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDOperationRelateRecord]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationDetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDOperationDetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDIdentify]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDIdentify]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDApplicationAndIDOperation]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetIDApplicationAndIDOperation]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDeliveryAndDailyContrastData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetDeliveryAndDailyContrastData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAccountProfitContrastData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetAccountProfitContrastData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAccountPositionContrastData]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetAccountPositionContrastData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAccountDetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GetAccountDetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_GeneratePSAInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GeneratePSAInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_GeneratePSADetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GeneratePSADetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateMTFInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GenerateMTFInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateMTFDetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GenerateMTFDetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateIPRSummary]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GenerateIPRSummary]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateIDOperationAccuracyInfo]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_GenerateIDOperationAccuracyInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_DSDeliveryProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_DSDeliveryProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_DSDailyProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_DSDailyProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeliveryAccountInvestIncomeDetail_01]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_DeliveryAccountInvestIncomeDetail_01]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeliveryAccountInvestIncomeDetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_DeliveryAccountInvestIncomeDetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_AccountInvestFundDetail]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_AccountInvestFundDetail]
GO
/****** Object:  StoredProcedure [dbo].[sp_AccountFundSettleProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_AccountFundSettleProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_AccountFundRevokeProcess]    Script Date: 2017/4/26 14:43:32 ******/
DROP PROCEDURE [dbo].[sp_AccountFundRevokeProcess]
GO
/****** Object:  StoredProcedure [dbo].[sp_AccountFundRevokeProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_AccountFundSettleProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_AccountInvestFundDetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_DeliveryAccountInvestIncomeDetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

	--insert into t001([QueryPeriod],[AccountDetail],[StockDetail],[HoldingVolume],)
	SELECT 
		(CONVERT(varchar(10),@DateFrom,111) + ' - ' +  CONVERT(varchar(10),@DateTo,111)) QueryPeriod,
		(AI.Name + ' - ' + AI.SecurityCompanyName + ' - ' + AI.AttributeName + ' - ' + AI.TypeName) AccountDetail,
		(E.SCode + ' - ' + E.SName) StockDetail,	
		CAST(E.HoldingVolume AS decimal(18,0)) HoldingVolume,
		E.LatestPrice,
		E.PositionValue,
		(ISNULL(E.AccumulatedProfit,0) - ISNULL(S.AccumulatedProfit,0)) Profit,
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
/****** Object:  StoredProcedure [dbo].[sp_DeliveryAccountInvestIncomeDetail_01]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DeliveryAccountInvestIncomeDetail_01]
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

	insert into [dbo].[t001](
    [QueryPeriod]
	,[AccountDetail],[StockDetail],[HoldingVolume],[LatestPrice],[PositionValue],[Profit],[AccumulatedProfit]
	,[StockCode],[StockName],[AccountId],[AccountName],[SecurityCompanyName]
	,[AttributeName]
	)
	SELECT 
		(CONVERT(varchar(10),@DateFrom,111) + ' - ' +  CONVERT(varchar(10),@DateTo,111)) QueryPeriod,
		(AI.Name + ' - ' + AI.SecurityCompanyName + ' - ' + AI.AttributeName + ' - ' + AI.TypeName) AccountDetail,
		(E.SCode + ' - ' + E.SName) StockDetail,	
		CAST(E.HoldingVolume AS decimal(18,0)) HoldingVolume,
		E.LatestPrice,
		E.PositionValue,
		(ISNULL(E.AccumulatedProfit,0) - ISNULL(S.AccumulatedProfit,0)) Profit,
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
	--ORDER BY AccountDetail, StockDetail

	DROP TABLE #TableStart
	DROP TABLE #TableEnd

END

/*
exec sp_DeliveryAccountInvestIncomeDetail_01 '2016-11-01','2016-11-30'

select * from [dbo].[AccountInfo]
where TypeName like '三方%'

delete from [dbo].[t001]
where AccountId in(63,110,1110,1111,1124,1125)

delete from [dbo].[t001]
where StockName like 'GC%' or StockName like 'ETF%'


select substring([QueryPeriod],19,2)yf,StockCode,StockName,Profit into #table1
from [dbo].[t001]


declare @sql varchar(max)

set @sql = 'select StockCode,StockName'
select @sql = @sql + ',sum(case yf when ''' + yf + ''' then Profit else 0 end) [' + yf + '] '
from (select distinct yf from #table1)as a
set @sql = @sql + ',sum(Profit) ''合计：'' from #table1 group by StockCode,StockName order by sum(Profit)'

--print @sql
exec(@sql)

drop table #table1

*/





GO
/****** Object:  StoredProcedure [dbo].[sp_DSDailyProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[sp_DSDailyProcess]
(
	@TradeDate datetime
)
AS
BEGIN

	SET NOCOUNT ON			
	
	DECLARE @weekDay int = CASE WHEN DATEPART(DW,@TradeDate) = 1 THEN 7 ELSE  DATEPART(DW,@TradeDate) -1 END
	DECLARE @lastDSDate datetime = DATEADD(DD,-1,@TradeDate)
	DECLARE @isSameYear bit = CASE WHEN DATEPART(YYYY,@TradeDate) = DATEPART(YYYY,@LastDsDate) THEN 1 ELSE 0 END

	-- 当日交易记录 --
	SELECT 
		Beneficiary
		,AccountId
		,StockCode	
		,StockName 		
		,TradeType
		,DealFlag
		,DealVolume 	
		,DealAmount
		,ActualAmount 
	INTO #TradeInfo
	FROM DailyRecord
	WHERE TradeDate = @TradeDate
	
	/* 收益明细日结处理 */

	-- 前一日结明细 --
	SELECT 
		InvestorCode
		,AccountId
		,StockCode
		,StockName
		,PositionVolume 
		,PositionValue
		,AccumulatedProfit
		,YearProfit
		,DayProfit
	INTO #LastDSDetail
	FROM DSDailyDetail 
	WHERE TradeDate = @lastDSDate

	-- 日结明细 --
	SELECT 
		P.*		
		,PositionValue = ABS(P.PositionVolume) * ISNULL(TKC.[Close],0)
		,Profit = P.PositionVolume * ISNULL(TKC.[Close],0) - (CASE WHEN P.LastPositionVolume >0 THEN P.LastPositionValue ELSE -P.LastPositionValue END) + P.ActualAmount 			
	INTO #CurrentDSDetail
	FROM
	(
		SELECT 	
			InvestorCode = ISNULL(T.Beneficiary,L.InvestorCode)
			,AccountId = ISNULL(T.AccountId,L.AccountId)	
			,StockCode = ISNULL(T.StockCode,L.StockCode)
			,StockName = ISNULL(T.StockName,L.StockName)			
			,ActualAmount = ISNULL(T.ActualAmount,0)	
			,PositionVolume = ISNULL(T.DealVolume,0) + ISNULL(L.PositionVolume,0)	
			,LastPositionVolume = ISNULL(L.PositionVolume,0)
			,LastPositionValue = ISNULL(L.PositionValue,0)
			,LastAccumulatedProfit = ISNULL(L.AccumulatedProfit,0)
			,LastYearProfit = ISNULL(L.YearProfit,0)		
		FROM 
		(
			SELECT 
				Beneficiary
				,AccountId
				,StockCode	
				,StockName = MAX(StockName)
				,DealVolume = SUM(DealVolume)		
				,ActualAmount = SUM(ActualAmount)
			FROM #TradeInfo
			GROUP BY Beneficiary, AccountId, StockCode
		) T
		FULL JOIN #LastDSDetail L
		ON L.InvestorCode = T.Beneficiary AND L.AccountId = T.AccountId AND L.StockCode = T.StockCode
	) P
	LEFT JOIN TKLineToday TKC
	ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @TradeDate 

		
	-- 保存明细日结数据 --
	INSERT INTO DSDailyDetail (TradeDate,[WeekDay],InvestorCode,AccountId, AccountCode, StockCode, StockName, PositionVolume, PositionValue,AccumulatedProfit, YearProfit, DayProfit)
	SELECT 
		@TradeDate
		,@weekDay
		,InvestorCode
		,AccountId
		,NULL
		,StockCode
		,StockName
		,PositionVolume 
		,PositionValue		
		,LastAccumulatedProfit + Profit
		,CASE WHEN @isSameYear = 1 THEN LastYearProfit + Profit ELSE Profit END
		,Profit
	FROM #CurrentDSDetail 


	DROP TABLE #LastDSDetail  	
	DROP TABLE #CurrentDSDetail 


	/* 交易员日结处理 */

	-- 交易员当日持仓信息 --
	SELECT 
		P.InvestorCode
		,P.StockCode
		,PositionValue = ABS(P.PositionVolume) * ISNULL(TKC.[Close],0)
	INTO #CurrentInvestorPosition
	FROM(
		SELECT 	
			InvestorCode
			,StockCode
			,PositionVolume = SUM(PositionVolume)		
		FROM DSDailyDetail 
		WHERE TradeDate = @TradeDate
		GROUP BY InvestorCode,StockCode
	) P
	LEFT JOIN TKLineToday TKC
	ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @TradeDate 


	-- 交易员当日交易收益信息 --	
	SELECT 
		TradeDate = @TradeDate
		,D.InvestorCode
		,P.PositionValue
		,BuyAmount = ISNULL(T.BuyAmount,0)
		,SellAmount = ISNULL(T.SellAmount,0)
		,DealAmount = CASE WHEN ISNULL(T.BuyAmount,0) > ISNULL(T.SellAmount,0) THEN ISNULL(T.BuyAmount,0) ELSE ISNULL(T.SellAmount,0) END	
		,D.AccumulatedProfit
		,D.YearProfit
		,D.DayProfit
	INTO #CurrentInvestorProfit
	FROM
	(	
		SELECT 	
			InvestorCode
			,PositionValue = SUM(PositionValue)	
			,AccumulatedProfit = SUM(AccumulatedProfit)
			,YearProfit = SUM(YearProfit)
			,DayProfit = SUM(DayProfit)
		FROM DSDailyDetail 
		WHERE TradeDate = @TradeDate
		GROUP BY InvestorCode
	) D
	LEFT JOIN 
	(
		SELECT 
			Beneficiary
			,BuyAmount = SUM(CASE WHEN DealFlag = 1 THEN DealAmount ELSE 0 END)
			,SellAmount = SUM(CASE WHEN DealFlag = 0 THEN DealAmount ELSE 0 END)		
		FROM #TradeInfo
		GROUP BY Beneficiary
	) T
	ON T.Beneficiary = D.InvestorCode
	LEFT JOIN
	(
		SELECT 
			InvestorCode
			,PositionValue = SUM(PositionValue)
		FROM #CurrentInvestorPosition 
		GROUP BY InvestorCode
	) P
	ON P.InvestorCode = D.InvestorCode


	-- 交易员计划融券表 --
	CREATE TABLE #InvestorPlanMargin
	(	
		[InvestorCode] [nvarchar](20) NOT NULL,
		--[TradeType] [int] NOT NULL,
		[MarginAmount] [decimal](24,4) NOT NULL,	
		[IsIn] [int] NOT NULL,
	)

	-- 交易员融进的融券信息 --
	INSERT INTO #InvestorPlanMargin
	SELECT 
		InvestorCode
		--,TradeType
		,SUM(CASE WHEN IsRepay = 1 THEN -Amount ELSE Amount END)	
		,1
	FROM MarginTradingInfo
	WHERE (MarginDate = @TradeDate AND TradeType = 3) OR (MarginDate <= @TradeDate AND (TradeType = 2 OR TradeType = 1))
	GROUP BY InvestorCode --, TradeType	

	-- 交易员融出的融券信息 --
	INSERT INTO #InvestorPlanMargin
	SELECT 		
		LoanOwnerCode
		--,TradeType
		,-SUM(CASE WHEN IsRepay = 1 THEN -Amount ELSE Amount END)		
		,0
	FROM MarginTradingInfo
	WHERE (MarginDate = @TradeDate AND TradeType = 3) OR (MarginDate <= @TradeDate AND (TradeType = 2 OR TradeType = 1))
	GROUP BY LoanOwnerCode --,TradeType
	HAVING LoanOwnerCode IS NOT NULL

	-- 交易员实际融券信息 --
	SELECT 
		InvestorCode = ISNULL(P.InvestorCode,C.InvestorCode)
		,MarginAmount = [dbo].[f_GetMaxNumber](ISNULL(P.PlanMarginAmount,0),ISNULL(C.PositionValue,0),ISNULL(C.DealAmount,0))
	INTO #ActualMargin	
	FROM #CurrentInvestorProfit C
	LEFT JOIN
	(
		SELECT 
			InvestorCode		
			,PlanMarginAmount = SUM(MarginAmount)
		FROM #InvestorPlanMargin 
		GROUP BY InvestorCode
	) P
	ON P.InvestorCode = C.InvestorCode

	-- 前一日交易员日结信息 --
	SELECT 			
		InvestorCode
		,PositionValue
		,AccumulatedInterest
		,YearInterest = CASE WHEN @isSameYear = 1 THEN ISNULL(YearInterest,0) ELSE 0 END 
		,YearProfit = CASE WHEN @isSameYear = 1 THEN ISNULL(YearProfit,0) ELSE 0 END
	INTO #LastDSInvestor
	FROM DSDailyInvestor 
	WHERE TradeDate = @lastDSDate
	

	-- 融资融券年里率 --
	DECLARE @marginTradingAPR decimal(18,4) = 0.08
	DECLARE @accountingDay int = 360

	-- 当日交易员融资及利息信息 -- 
	SELECT 
		InvestorCode = ISNULL(L.InvestorCode,M.InvestorCode)
		,AccumulatedInterest = ISNULL(L.AccumulatedInterest,0)
		,YearInterest = ISNULL(L.YearInterest,0)
		,DayInterest = CASE WHEN ISNULL(M.MarginAmount,0) > ISNULL(L.YearProfit,0) THEN (ISNULL(M.MarginAmount,0) - ISNULL(L.YearProfit,0)) * @marginTradingAPR / @accountingDay  ELSE 0 END
		,MarginAmount = ISNULL(M.MarginAmount,0)
	INTO #InvestorMarginInterest
	FROM #LastDSInvestor L	
	FULL JOIN #ActualMargin M
	ON M.InvestorCode = L.InvestorCode
	

	-- 保存交易员日结信息 --
	INSERT INTO DSDailyInvestor(TradeDate,[WeekDay],InvestorCode,PositionValue,BuyAmount,SellAmount,DealAmount,MarginAmount,AccumulatedInterest,YearInterest,DayInterest,AccumulatedProfit,YearProfit,DayProfit)
	SELECT 
		C.TradeDate
		,@weekDay
		,C.InvestorCode
		,C.PositionValue
		,C.BuyAmount
		,C.SellAmount
		,C.DealAmount
		,ISNULL(I.MarginAmount,0)
		,ISNULL(I.AccumulatedInterest,0) + ISNULL(I.DayInterest,0)
		,ISNULL(I.YearInterest,0) + ISNULL(I.DayInterest,0)
		,ISNULL(I.DayInterest,0)
		,C.AccumulatedProfit
		,C.YearProfit
		,C.DayProfit
	FROM #CurrentInvestorProfit C
	LEFT JOIN #InvestorMarginInterest I
	ON I.InvestorCode = C.InvestorCode

	
	DROP TABLE #TradeInfo
	DROP TABLE #CurrentInvestorPosition 
	DROP TABLE #CurrentInvestorProfit
	DROP TABLE #InvestorPlanMargin
	DROP TABLE #ActualMargin
	DROP TABLE #LastDSInvestor
	DROP TABLE #InvestorMarginInterest

END









GO
/****** Object:  StoredProcedure [dbo].[sp_DSDeliveryProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_DSDeliveryProcess]
(
	@TradeDate datetime
)
AS
BEGIN

	SET NOCOUNT ON		

	DECLARE @weekDay int = CASE WHEN DATEPART(DW,@TradeDate) = 1 THEN 7 ELSE  DATEPART(DW,@TradeDate) -1 END
	DECLARE @lastDSDate datetime = DATEADD(DD,-1,@TradeDate)
	DECLARE @isSameYear bit = CASE WHEN DATEPART(YYYY,@TradeDate) = DATEPART(YYYY,@LastDsDate) THEN 1 ELSE 0 END

	-- 当日交易信息 --
	SELECT 
		AccountId
		,StockCode	
		,StockName = MAX(StockName)
		,DealVolume = SUM(DealVolume)		
		,ActualAmount = SUM(ActualAmount)
	INTO #TradeInfo
	FROM DeliveryRecord
	WHERE TradeDate = @TradeDate
	GROUP BY AccountId, StockCode
	
	-- 前一日结明细 --
	SELECT 
		AccountId
		,StockCode
		,StockName
		,PositionVolume 
		,PositionValue 
		,CostPrice
		,AccumulatedProfit
		,YearProfit
		,DayProfit
	INTO #LastDSInfo
	FROM DSDeliveryDetail 
	WHERE TradeDate = @lastDSDate

	-- 当日日结明细 --
	SELECT 
		P.*
		,PositionValue =ABS(P.PositionVolume) * ISNULL(TKC.[Close],0)
		,Profit =  P.PositionVolume * ISNULL(TKC.[Close],0) - (CASE WHEN P.LastPositionVolume >0 THEN P.LastPositionValue ELSE -P.LastPositionValue END) + P.ActualAmount 		
		,CostPrice = CASE WHEN P.PositionVolume	= 0 THEN 0 ELSE ( P.LastPositionVolume * P.LastCostPrice - P.ActualAmount)/P.PositionVolume END	
	INTO #CurrentDSInfo
	FROM
	(
		SELECT 	
			AccountId = ISNULL(T.AccountId,L.AccountId)	
			,StockCode = ISNULL(T.StockCode,L.StockCode)
			,StockName = ISNULL(T.StockName,L.StockName)		
			,ActualAmount = ISNULL(T.ActualAmount,0)
			,PositionVolume = ISNULL(T.DealVolume,0) + ISNULL(L.PositionVolume,0)			
			,LastCostPrice = ISNULL(L.CostPrice,0)
			,LastPositionVolume = ISNULL(l.PositionVolume,0)
			,LastPositionValue = ISNULL(L.PositionValue,0)
			,LastAccumulatedProfit = ISNULL(L.AccumulatedProfit,0)
			,LastYearProfit = ISNULL(L.YearProfit,0)		
		FROM #TradeInfo T
		FULL JOIN #LastDSInfo L
		ON L.AccountId = T.AccountId AND L.StockCode = T.StockCode
	) P
	LEFT JOIN TKLineToday TKC
	ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @TradeDate 
		
	-- 保存明细日结数据 --
	INSERT INTO DSDeliveryDetail (TradeDate,[WeekDay], AccountId, AccountCode, StockCode, StockName, PositionVolume, PositionValue, CostPrice, AccumulatedProfit, YearProfit, DayProfit)
	SELECT 
		@TradeDate
		,@weekDay
		,AccountId
		,NULL
		,StockCode
		,StockName
		,PositionVolume 
		,PositionValue
		,CostPrice
		,LastAccumulatedProfit + Profit
		,CASE WHEN DATEPART(YYYY,@lastDSDate) = DATEPART(YYYY,@TradeDate) THEN LastYearProfit + Profit ELSE Profit END
		,Profit
	FROM #CurrentDSInfo 

	-- 保存账号日结数据 --
	INSERT INTO DSDeliveryAccount(TradeDate,[WeekDay],AccountId,AccountCode,TotalAsset,AvailableFund,PositionValue,FinancingLimit,FinancedAmount,AccumulatedProfit,YearProfit,DayProfit)
	SELECT 
		@TradeDate
		,@weekDay
		,AccountId
		,NULL
		,0
		,0
		,SUM(PositionValue)
		,0
		,0
		,SUM(AccumulatedProfit)
		,SUM(YearProfit)
		,SUM(DayProfit)
	FROM DSDeliveryDetail 
	WHERE TradeDate = @TradeDate
	GROUP BY AccountId
	

	DROP TABLE #TradeInfo
	DROP TABLE #LastDSInfo  	
	DROP TABLE #CurrentDSInfo 
END








GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateIDOperationAccuracyInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_GenerateIDOperationAccuracyInfo]
(
	@ApplyNo varchar(50),
	@OperateNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	
	DELETE FROM InvestmentDecisionOperationAccuracy  WHERE OperateNo = @OperateNo

	INSERT INTO InvestmentDecisionOperationAccuracy (ApplyNo, OperateNo, UserCode, [Weight], Flag, Reason, JudgeTime,IsAdminVeto)
	SELECT 
		 @ApplyNo
		,@OperateNo
		,C.Code
		,C.[Weight]
		,0
		,NULL
		,GETDATE()
		,0
	FROM InvestmentDecisionCommittee C
END











GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateIPRSummary]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GenerateIPRSummary]
(	
	@AnalysisDate datetime
)
AS
BEGIN

	SET NOCOUNT ON
	
	DECLARE @serialNo varchar(50)
	SELECT @serialNo =SerialNo FROM InvestmentPlanRecordSummary WHERE AnalysisDate = @AnalysisDate

	IF(ISNULL(@serialNo,'') = '')
		BEGIN
			DECLARE @maxSerialNo varchar(50) = (SELECT MAX(SerialNo) FROM InvestmentPlanRecordSummary)			

			DECLARE @suffix varchar(10)
			IF( ISNULL(@maxSerialNo,'') = '')
				SET @suffix = '000001'
			ELSE
				SET @suffix =RIGHT('000000'+ CAST((CAST(SUBSTRING(@maxSerialNo,LEN(@maxSerialNo) - 5,6) AS int) + 1) AS varchar),6)

			SET @serialNo = 'JH' + @suffix

			INSERT INTO InvestmentPlanRecordSummary (SerialNo,AnalysisDate,CreateTime)
			VALUES(@serialNo,@AnalysisDate, GETDATE())		
			
			
		END
 
END




GO
/****** Object:  StoredProcedure [dbo].[sp_GenerateMTFDetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_GenerateMTFInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_GeneratePSADetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

	IF(@AnalysisDate > DATEADD(D,-1, GETDATE()))
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
			WHERE P.StockCode NOT IN (SELECT StockCode FROM PositionStockAnalysisDetail WHERE SerialNo = @serialNo AND InvestorCode = @InvestorCode )

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
/****** Object:  StoredProcedure [dbo].[sp_GeneratePSAInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_GetAccountDetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

	SET @commandText = N'SELECT 
							IndustryName =[dbo].[f_GetIndustryFullName](AI.IndustryId,N''->'')
							,OperatorNames =[dbo].[f_GetAccountOperatorNames](AI.Id)
							,AI.*
							,DisplayMember = AI.Name + '' - '' + AI.SecurityCompanyName + '' - '' +AI.AttributeName				
							,OwnerName = UO.Name							 
						FROM [dbo].[AccountInfo] AI 
						LEFT JOIN IndustryInfo II 
						ON II.Id = AI.IndustryId
						LEFT JOIN UserInfo UO
						ON UO.Code = AI.Owner
						WHERE 1 = 1 '

	IF( @IndustyId > 0 )
		SET @commandText +=N' AND AI.IndustryId = ' + CAST(@IndustyId AS varchar(8))
	IF(LEN(@AccountIds)>0)
		SET @commandText +=N' AND AI.Id IN(' + @AccountIds +')'
	IF( @SecurityCode > 0 )
		SET @commandText +=N' AND AI.SecurityCompanyCode = ' +  CAST(@SecurityCode AS varchar(8))  
	IF( @AttributeCode > 0 )
		SET @commandText +=N' AND AI.AttributeCode = ' +  CAST(@AttributeCode AS varchar(8))  
	IF( @PlanCode > 0 )
		SET @commandText +=N' AND AI.PlanCode = ' +  CAST(@PlanCode AS varchar(8))  
	IF( @TypeCode > 0 )
		SET @commandText +=N' AND AI.TypeCode = ' +  CAST(@TypeCode AS varchar(8))  
	IF( @OnlyNeedAccounting = 1 )
		SET @commandText +=N' AND AI.NeedAccounting = 1'  
	IF( @ShowDisabled = 0 )
		SET @commandText +=N' AND AI.IsDisabled = 0' 	

	EXEC( @commandText )
	
END








GO
/****** Object:  StoredProcedure [dbo].[sp_GetAccountPositionContrastData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_GetAccountPositionContrastData]
(
	 @Year int
	,@Month int
	,@AccountIds varchar(1000)	/* 以逗号隔开的字符串 */
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @firstDayOfCurrentMonth datetime = CONVERT(datetime,CAST(@Year AS varchar) + '-' + CAST(@Month AS varchar) + '-01')
	DECLARE @firstDayOfLastMonth datetime = DATEADD(M,-1,@firstDayOfCurrentMonth)
	DECLARE @firstDayOfNextMonth datetime = DATEADD(M,1,@firstDayOfCurrentMonth)
	DECLARE @lastDayOfCurrentMonth datetime = DATEADD(D,-1,@firstDayOfNextMonth)


	/* 取得财务持仓【下月期初】 */
	SELECT 
		 P.AccountId 
		,P.StockCode		
		,P.PositionVolume
	INTO #AccountingPosition
	FROM MIAccountPosition  P
	WHERE P.YearMonth = (DATEPART(YYYY,@firstDayOfNextMonth) * 100 + DATEPART(MM,@firstDayOfNextMonth)) AND CHARINDEX (',' + CAST(P.AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0


	/* 取得交割单持仓【交割单当月合计成交数量 + 上月月结持仓】 */
	SELECT 
		 AccountId = ISNULL(DEP.AccountId,MSD.AccountId)
		,StockCode = ISNULL(DEP.StockCode,MSD.StockCode)
		,PositionVolume = ISNULL(DEP.PositionVolume,0) + ISNULL(MSD.PositionVolume,0)
	INTO #DeliveryPosition
	FROM 
	(
		SELECT 
			 DE.AccountId 			
			,DE.StockCode		
			,PositionVolume = SUM(DE.DealVolume) 
		FROM DeliveryRecord DE
		WHERE DE.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND CHARINDEX (',' + CAST(DE.AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
		GROUP BY DE.AccountId , DE.StockCode
	) DEP
	FULL JOIN 
	(
		SELECT 
			AccountId
			,StockCode
			,PositionVolume
		FROM MSDeliveryDetail 
		WHERE YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth)*100 + DATEPART(MM,@firstDayOfLastMonth)) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
	) MSD
	ON MSD.AccountId = DEP.AccountId AND MSD.StockCode = DEP.StockCode	


	/* 取得当日委托持仓【当日委托当月合计成交数量 + 上月月结持仓】 */
	SELECT 
		 AccountId = ISNULL(DAP.AccountId,MSD.AccountId)
		,StockCode = ISNULL(DAP.StockCode,MSD.StockCode)
		,PositionVolume = ISNULL(DAP.PositionVolume,0) + ISNULL(MSD.PositionVolume,0)
	INTO #DailyPosition
	FROM 
	(
		SELECT 
			 DA.AccountId 
			,DA.StockCode
			,PositionVolume = SUM(DA.DealVolume)
		FROM DailyRecord DA
		WHERE DA.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND CHARINDEX (',' + CAST(DA.AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
		GROUP BY DA.AccountId , DA.StockCode
	) DAP
	FULL JOIN 
	(
		SELECT 
			AccountId
			,StockCode
			,PositionVolume = SUM(PositionVolume)
		FROM MSDailyDetail  		
		WHERE YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth)*100 + DATEPART(MM,@firstDayOfLastMonth)) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
		GROUP BY AccountId, StockCode
	) MSD
	ON MSD.AccountId = DAP.AccountId AND MSD.StockCode = DAP.StockCode	
	
	
	/* 取得账户持仓核对数据 */
	SELECT 		
		 StockCode = SI.FullCode 
		,StockName = SI.Name
		,StockDetail = SI.FullCode + ' - ' + SI.Name
		,AccountId = AI.Id
		,AccountName = AI.Name
		,AI.SecurityCompanyName 
		,AI.AttributeName 
		,AccountDetail = AI.Name + ' - ' + AI.SecurityCompanyName + ' - ' + AI.AttributeName
		,OwnerName = UI.Name	
		,T.AccountingVolume 
		,T.DeliveryVolume
		,T.DeliveryDifference
		,T.DailyVolume 
		,T.DailyDifference 
	FROM
	(
		SELECT 
			 AccountId = COALESCE(AP.AccountId, DEP.AccountId, DAP.AccountId)
			,StockCode = COALESCE(AP.StockCode, DEP.StockCode, DAP.StockCode)
			,AccountingVolume = ISNULL(AP.PositionVolume,0)
			,DeliveryVolume = ISNULL(DEP.PositionVolume,0)
			,DeliveryDifference = ISNULL(AP.PositionVolume,0) - ISNULL(DEP.PositionVolume,0) 
			,DailyVolume = ISNULL(DAP.PositionVolume,0)
			,DailyDifference = ISNULL(DEP.PositionVolume,0) - ISNULL(DAP.PositionVolume,0)
		FROM #AccountingPosition AP
		FULL JOIN #DeliveryPosition DEP
		ON DEP.AccountId = AP.AccountId AND DEP.StockCode = AP.StockCode
		FULL JOIN #DailyPosition DAP
		ON DAP.AccountId = ISNULL(AP.AccountId,DEP.AccountId) AND DAP.StockCode = ISNULL(AP.StockCode,DEP.StockCode)
	) T
	LEFT JOIN AccountInfo AI
	ON AI.Id = T.AccountId
	LEFT JOIN UserInfo UI
	ON UI.Code = AI.[Owner]
	LEFT JOIN StockInfo SI
	ON SI.FullCode = T.StockCode
	WHERE T.AccountingVolume !=0 OR T.DeliveryVolume !=0 OR T.DailyVolume !=0
	ORDER BY AccountDetail,StockDetail

	DROP TABLE #AccountingPosition
	DROP TABLE #DeliveryPosition
	DROP TABLE #DailyPosition
 
END











GO
/****** Object:  StoredProcedure [dbo].[sp_GetAccountProfitContrastData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_GetAccountProfitContrastData]
(
	 @Year int
	,@Month int
	,@AccountIds varchar(1000)	/* 以逗号隔开的字符串 */
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @firstDayOfCurrentMonth datetime = CONVERT(datetime,CAST(@Year AS varchar) + '-' + CAST(@Month AS varchar) + '-01')
	DECLARE @firstDayOfLastMonth datetime = DATEADD(M,-1,@firstDayOfCurrentMonth)
	DECLARE @firstDayOfNextMonth datetime = DATEADD(M,1,@firstDayOfCurrentMonth)
	DECLARE @lastDayOfCurrentMonth datetime = DATEADD(D,-1,@firstDayOfNextMonth)
	DECLARE @lastDayOfLastMonth datetime = DATEADD(D,-1,@firstDayOfCurrentMonth)


	/* 财务收益核算 */	

	-- 下月期初资金 --
	SELECT 
		 AccountId
		,NetAsset = TotalAsset - FinancedAmount 
	INTO #NextMIFund	
	FROM MIAccountFund 
	WHERE YearMonth = (DATEPART(YYYY,@firstDayOfNextMonth) * 100 + DATEPART(MM,@firstDayOfNextMonth)) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0

	-- 本月期初资金 --
	SELECT 
		 AccountId
		,NetAsset = TotalAsset - FinancedAmount 
	INTO #CurrentMIFund		
	FROM MIAccountFund 
	WHERE YearMonth = (@Year * 100 + @Month) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0

	-- 本月转入转出资金 --
	SELECT 
		 AccountId
		,TransferAmount = SUM(TransferAmount)
	INTO #CurrentFundTransfer 
	FROM AccountFundTransfer
	WHERE TransferDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
	GROUP BY AccountId

	-- 本月财务收益 --
	SELECT 
		 AccountId =COALESCE(NF.AccountId, CF.AccountId, T.AccountId)
		,Profit = ISNULL(NF.NetAsset,0) - ISNULL(CF.NetAsset,0) - ISNULL(T.TransferAmount,0)
	INTO #AccountingProfit
	FROM #NextMIFund NF
	FULL JOIN #CurrentMIFund CF
	ON CF.AccountId = NF.AccountId
	FULL JOIN #CurrentFundTransfer T
	ON T.AccountId = ISNULL(NF.AccountId,CF.AccountId)


	/* 交割单收益核算 */
	SELECT
		T.AccountId
		,Profit = SUM(T.StockProfit)
	INTO #DeliveryProfit
	FROM
	(
		SELECT 
			P.AccountId
			,P.StockCode
			,StockProfit = P.LastMSPositionVolume *(ISNULL(TKC.[Close],0) - ISNULL(TKL.[Close],0)) + P.AccumulatedDealVolume * ISNULL(TKC.[Close],0) + P.AccumulatedActualAmount 	
		FROM
		(
			SELECT 
				 AccountId = ISNULL(DEP.AccountId,MSD.AccountId)
				,StockCode = ISNULL(DEP.StockCode,MSD.StockCode)
				,LastMSPositionVolume = ISNULL(MSD.PositionVolume,0)
				,AccumulatedDealVolume = ISNULL(DEP.PositionVolume,0) 
				,AccumulatedActualAmount = ISNULL(DEP.ActualAmount,0)	
			FROM 
			(
				SELECT 
					 DE.AccountId 			
					,DE.StockCode		
					,PositionVolume = SUM(DE.DealVolume) 
					,ActualAmount = SUM(DE.ActualAmount)
				FROM DeliveryRecord DE
				WHERE DE.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND CHARINDEX (',' + CAST(DE.AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
				GROUP BY DE.AccountId , DE.StockCode
			) DEP
			FULL JOIN 
			(
				SELECT 
					AccountId
					,StockCode
					,PositionVolume
				FROM MSDeliveryDetail 
				WHERE YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth) * 100 + DATEPART(MM,@firstDayOfLastMonth)) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
			) MSD
			ON MSD.AccountId = DEP.AccountId AND MSD.StockCode = DEP.StockCode	
		) P
		LEFT JOIN TKLineToday TKC
		ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @lastDayOfCurrentMonth 
		LEFT JOIN TKLineToday TKL
		ON TKL.StockCode = P.StockCode AND TKL.TradeDate = @lastDayOfLastMonth 
	) T
	GROUP BY T.AccountId

	/* 当日委托收益核算 */
	SELECT
		T.AccountId
		,Profit = SUM(T.StockProfit)
	INTO #DailyProfit
	FROM
	(
		SELECT 
			P.AccountId
			,P.StockCode
			,StockProfit = P.LastMSPositionVolume *(ISNULL(TKC.[Close],0) - ISNULL(TKL.[Close],0)) + P.AccumulatedDealVolume * ISNULL(TKC.[Close],0) + P.AccumulatedActualAmount 	
		FROM
		(
			SELECT 
				 AccountId = ISNULL(DAP.AccountId,MSD.AccountId)
				,StockCode = ISNULL(DAP.StockCode,MSD.StockCode)
				,LastMSPositionVolume = ISNULL(MSD.PositionVolume,0)
				,AccumulatedDealVolume = ISNULL(DAP.PositionVolume,0) 
				,AccumulatedActualAmount = ISNULL(DAP.ActualAmount,0)	
			FROM 
			(
				SELECT 
					 DA.AccountId 			
					,DA.StockCode		
					,PositionVolume = SUM(DA.DealVolume) 
					,ActualAmount = SUM(DA.ActualAmount)
				FROM DailyRecord DA
				WHERE DA.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND CHARINDEX (',' + CAST(DA.AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
				GROUP BY DA.AccountId , DA.StockCode
			) DAP
			FULL JOIN 
			(
				SELECT 
					AccountId
					,StockCode
					,PositionVolume = SUM(PositionVolume)
				FROM MSDailyDetail 		
				WHERE YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth) *100 + DATEPART(MM,@firstDayOfLastMonth)) AND CHARINDEX (',' + CAST(AccountId AS varchar) + ',' , ',' + @AccountIds + ',' )>0
				GROUP BY AccountId, StockCode
			) MSD
			ON MSD.AccountId = DAP.AccountId AND MSD.StockCode = DAP.StockCode
		) P
		LEFT JOIN TKLineToday TKC
		ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @lastDayOfCurrentMonth 
		LEFT JOIN TKLineToday TKL
		ON TKL.StockCode = P.StockCode AND TKL.TradeDate = @lastDayOfLastMonth
	) T
	GROUP BY T.AccountId

	/* 取得账户收益核对数据 */
	SELECT 	
		AccountId = AI.Id
		,AccountName = AI.Name
		,AI.SecurityCompanyName 
		,AI.AttributeName 		
		,OwnerName = UI.Name	
		,T.AccountingAmount 
		,T.DeliveryAmount
		,T.DeliveryDifference
		,T.DailyAmount 
		,T.DailyDifference 
	FROM
	(
		SELECT 
			 AccountId = COALESCE(AP.AccountId, DEP.AccountId, DAP.AccountId)			
			,AccountingAmount = ISNULL(AP.Profit,0)
			,DeliveryAmount= ISNULL(DEP.Profit,0)
			,DeliveryDifference =  ISNULL(AP.Profit,0) - ISNULL(DEP.Profit,0)
			,DailyAmount = ISNULL(DAP.Profit,0)
			,DailyDifference = ISNULL(DEP.Profit,0) - ISNULL(DAP.Profit,0)
		FROM #AccountingProfit AP
		FULL JOIN #DeliveryProfit DEP
		ON DEP.AccountId = AP.AccountId 
		FULL JOIN #DailyProfit DAP
		ON DAP.AccountId = ISNULL(AP.AccountId,DEP.AccountId)
	) T
	LEFT JOIN AccountInfo AI
	ON AI.Id = T.AccountId
	LEFT JOIN UserInfo UI
	ON UI.Code = AI.[Owner]	
	WHERE T.AccountingAmount !=0 OR T.DeliveryAmount !=0 OR T.DailyAmount !=0
	ORDER BY AccountName, SecurityCompanyName, AttributeName 

	DROP TABLE #CurrentMIFund 
	DROP TABLE #NextMIFund
	DROP TABLE #CurrentFundTransfer 
	DROP TABLE #AccountingProfit 
	DROP TABLE #DeliveryProfit 
	DROP TABLE #DailyProfit 
END












GO
/****** Object:  StoredProcedure [dbo].[sp_GetDeliveryAndDailyContrastData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetDeliveryAndDailyContrastData]
(
	@AccountId int,
	@StockCode varchar(20),
	@FromDate datetime,
	@ToDate datetime,
	@DealFlag bit
)
AS
BEGIN 
	SET NOCOUNT ON

		
	SELECT	
		Id,
		StockCode,
		StockName,	
		TradeDate,
		TradeTime,
		CASE DealFlag
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		DealPrice,
		DealVolume,
		ActualAmount
	FROM DeliveryRecord
	WHERE TradeDate BETWEEN @FromDate AND @ToDate AND  AccountId = @AccountId AND StockCode = @StockCode AND DealFlag = @DealFlag
	ORDER BY TradeDate,TradeTime


	SELECT 
		StockCode,
		StockName,	
		TradeDate,
		TradeTime,		
		CASE DealFlag
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		DealPrice,
		DealVolume,
		ActualAmount,
		CASE DataType
			WHEN 1 THEN '当日委托'
			WHEN 2 THEN '交割单'
			WHEN 3 THEN '当日成交'
			WHEN 77 THEN '虚拟交易'
			WHEN 88 THEN '股票转移'
			WHEN 99 THEN '旧系统'
			ELSE ''
		END DataTypeName,
		CASE TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '日内'
			ELSE ''
		END TradeTypeName,
		B.Name BeneficiaryName,
		I.Name ImportUserName
	FROM DailyRecord
	LEFT JOIN UserInfo B
	ON Beneficiary = B.Code
	LEFT JOIN UserInfo I
	ON ImportUser = I.Code
	WHERE TradeDate BETWEEN @FromDate AND @ToDate AND  AccountId = @AccountId AND StockCode = @StockCode AND DealFlag = @DealFlag
	ORDER BY TradeDate,TradeTime

END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetDiffBetweenDeliveryAndDailyData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
			ISNULL(Delivery.DE_TradeDate,Daily.DA_TradeDate)DE_TradeDate
			,Delivery.DE_StockCode
			,Delivery.DE_StockName
			,Delivery.DE_DealFlag
			,Delivery.DE_DealName
			,Delivery.DE_TotalActualAmount
			,Delivery.DE_TotalDealVolume	
			,(ISNULL(Delivery.DE_TotalActualAmount,0) - ISNULL(Daily.DA_TotalActualAmount,0))AmountDiff
			,(ISNULL(Delivery.DE_TotalDealVolume,0) - ISNULL(Daily.DA_TotalDealVolume,0)) VolumeDiff
			,ISNULL(Daily.DA_TradeDate,Delivery.DE_TradeDate)DA_TradeDate
			,Daily.DA_StockCode
			,Daily.DA_StockName
			,Daily.DA_DealFlag
			,Daily.DA_DealName
			,Daily.DA_TotalActualAmount
			,Daily.DA_TotalDealVolume
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
					WHEN 1 THEN '买入(虚拟)'
					WHEN 0 THEN '卖出(虚拟)'
				END DA_DealName,
				SUM(ActualAmount) DA_TotalActualAmount,
				SUM(DealVolume) DA_TotalDealVolume
			FROM DailyRecord 
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo AND DataType = 77
			GROUP BY TradeDate,StockCode,DealFlag 
			UNION ALL
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
			WHERE AccountId = @AccountId AND TradeDate >= @DateFrom  AND TradeDate <= @DateTo AND DataType != 77
			GROUP BY TradeDate,StockCode,DealFlag 
		) Daily 
		ON Daily.DA_TradeDate=Delivery.DE_TradeDate AND Daily.DA_StockCode=Delivery.DE_StockCode AND Daily.DA_DealName = Delivery.DE_DealName
		ORDER BY DE_TradeDate,DE_StockCode    

	ELSE

		SELECT	
			Delivery.* 
			,(ISNULL(Delivery.DE_TotalActualAmount,0) - ISNULL(Daily.DA_TotalActualAmount,0))AmountDiff
			,(ISNULL(Delivery.DE_TotalDealVolume,0) - ISNULL(Daily.DA_TotalDealVolume,0)) VolumeDiff
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
/****** Object:  StoredProcedure [dbo].[sp_GetIDApplicationAndIDOperation]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetIDApplicationAndIDOperation]
(
	@Status int = -1,
	@ApplyUser varchar(20) = NULL,
	@ApplyDateFrom datetime = NULL,
	@ApplyDateTo datetime = NULL,
	@StockCode varchar(20) = NULL
)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @sql  nvarchar(2000) = N'SELECT * INTO #Temp1 FROM [dbo].[v_IDApplication] '
	DECLARE @whereCondition nvarchar (1000) = N'WHERE 1 = 1 '

	IF(@Status > -1)
		SET @whereCondition += N'AND Status = ' + CAST(@Status AS varchar(2) )

	IF(ISNULL(@ApplyUser,'') != '') 
		SET @whereCondition += N' AND ApplyUser = ' + N'''' + @ApplyUser + N''''

	IF ((ISNULL(@ApplyDateFrom ,'') != '') AND (ISNULL(@ApplyDateTo ,'') != ''))
		SET @whereCondition += N' AND ApplyDate BETWEEN  ' + N'''' +  CONVERT(varchar(10), @ApplyDateFrom,120) + N'''' + N' AND '+ N'''' + CONVERT(varchar(10), @ApplyDateTo,120) + N''''

	IF(ISNULL(@StockCode,'') != '') 
		SET @whereCondition += N' AND StockCode = ' + N'''' + @StockCode + N''''

	SET @sql += @whereCondition

	SET @sql += N'; SELECT * FROM #Temp1 ORDER BY ApplyNo DESC'

	SET @sql += N'; SELECT * FROM [dbo].[v_IDOperation] WHERE ApplyNo IN (SELECT ApplyNo FROM #Temp1) ORDER BY OperateNo'

	SET @sql += N'; DROP TABLE #Temp1'

	--PRINT @sql

	EXEC (@sql) 

	
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDIdentify]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetIDIdentify]
(
	@InvestorCode varchar(50),
	@ApplyNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON	
	
	SELECT 
		 @ApplyNo ApplyNo
		,IDO.OperateNo 
		,[dbo].[f_IDOperationInvestorVoteCheck](@InvestorCode,IDO.OperateNo) NeedVote
		,[dbo].[f_IDOperationInvestorAccuracyCheck](@InvestorCode,IDO.OperateNo)  NeedAccuracy
	INTO #OperationIdentify
	FROM InvestmentDecisionOperation IDO
	WHERE IDO.ApplyNo = @ApplyNo AND IDO.IsDeleted = 0 AND IsStopped =0
			
	SELECT 
		 OI.ApplyNo
		,SUM(CASE OI.NeedVote WHEN 1 THEN 1 ELSE 0 END) NeedVote
		,SUM(CASE OI.NeedAccuracy WHEN 1 THEN 1 ELSE 0 END) NeedAccuracy
	FROM #OperationIdentify OI
	GROUP BY OI.ApplyNo


	DROP TABLE #OperationIdentify
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationDetail]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetIDOperationDetail]
(
	@OperateNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT * FROM [dbo].[v_IDOperation] WHERE OperateNo = @OperateNo 

	EXEC [dbo].[sp_GetIDOperationVoteResult] @OperateNo = @OperateNo

	EXEC [dbo].[sp_GetIDOperationTradeRecord] @OperateNo = @OperateNo
	
	SELECT * FROM [dbo].[v_IDOperationAccuracy] WHERE OperateNo = @OperateNo
 
END










GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationRelateRecord]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetIDOperationRelateRecord]
(
	@OperateNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT 
		DR.Id RecordId,
		DR.StockCode,
		DR.StockName,	
		DR.TradeDate,
		DR.TradeTime,		
		CASE DR.DealFlag
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		DR.DealPrice,
		DR.DealVolume,
		DR.ActualAmount,
		CASE DR.DataType
			WHEN 1 THEN '当日委托'
			WHEN 2 THEN '交割单'
			WHEN 3 THEN '当日成交'
			WHEN 77 THEN '虚拟交易'
			WHEN 88 THEN '股票转移'
			WHEN 99 THEN '旧系统'
			ELSE ''
		END DataTypeName,
		CASE DR.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '日内'
			ELSE ''
		END TradeTypeName,
		B.Name BeneficiaryName,
		I.Name ImportUserName,
		DR.Remarks,
		AccountDetail = AI.Name + ' - ' + AI.SecurityCompanyName  + ' - ' + AI.AttributeName 
	FROM DailyRecord DR	
	LEFT JOIN AccountInfo AI
	ON AI.Id = DR.AccountId 
	LEFT JOIN UserInfo B
	ON DR.Beneficiary = B.Code
	LEFT JOIN UserInfo I
	ON DR.ImportUser = I.Code 
	INNER JOIN 
			(
				SELECT IDA.ApplyNo, IDA.ApplyUser,IDA.StockCode ,IDO.DealFlag, IDO.OperateNo, IDO.OperateDate    
				FROM InvestmentDecisionOperation IDO
				LEFT JOIN InvestmentDecisionApplication IDA
				ON IDA.ApplyNo = IDO.ApplyNo 
				WHERE IDA.ApplyNo = IDO.ApplyNo AND IDO.OperateNo = @OperateNo
					
			) T	
	ON  T.StockCode = DR.StockCode AND T.DealFlag = DR.DealFlag AND T.ApplyUser =DR.Beneficiary	AND DR.TradeDate >= T.OperateDate 
	ORDER BY DR.TradeDate ,DR.TradeTime 
 
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationTradeRecord]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetIDOperationTradeRecord]
(
	@OperateNo varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT 
		DR.Id RecordId,
		DR.StockCode,
		DR.StockName,	
		DR.TradeDate,
		DR.TradeTime,		
		CASE DR.DealFlag
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		DR.DealPrice,
		DR.DealVolume,
		DR.ActualAmount,
		CASE DR.DataType
			WHEN 1 THEN '当日委托'
			WHEN 2 THEN '交割单'
			WHEN 3 THEN '当日成交'
			WHEN 77 THEN '虚拟交易'
			WHEN 88 THEN '股票转移'
			WHEN 99 THEN '旧系统'
			ELSE ''
		END DataTypeName,
		CASE DR.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '日内'
			ELSE ''
		END TradeTypeName,
		B.Name BeneficiaryName,
		I.Name ImportUserName,
		DR.Remarks,
		AccountDetail = AI.Name + ' - ' + AI.SecurityCompanyName  + ' - ' + AI.AttributeName 
	FROM DailyRecord DR	
	LEFT JOIN AccountInfo AI
	ON AI.Id = DR.AccountId 
	LEFT JOIN UserInfo B
	ON DR.Beneficiary = B.Code
	LEFT JOIN UserInfo I
	ON DR.ImportUser = I.Code 
	WHERE DR.Id IN ( SELECT DailyRecordId FROM InvestmentDecisionTradeRecord WHERE OperateNo = @OperateNo)
 
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDOperationVoteResult]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetIDOperationVoteResult]
(
	@OperateNo varchar(50)
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
			WHEN 99 THEN '管理员否决权'
		END	TypeName,
		[dbo].[f_GetReasonCategoryNameWithParent](V.ReasonCategoryId,N'->') ReasonCategoryName,
		V.ReasonContent,
		CASE V.Flag
			WHEN 0 THEN NULL
			ELSE V.VoteTime
		END VoteTime,
		''  ConfirmTime
	FROM InvestmentDecisionOperationVote V
	LEFT JOIN UserInfo U
	ON V.UserCode = U.Code	
	WHERE V.OperateNo = @OperateNo AND((V.Flag != 0) OR (V.Flag = 0 AND V.[Type] != 3))
	ORDER BY [Type], InvestorName
 
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetIDVoteResult]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[sp_GetInvestmentDecisionForm]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
			WHEN 3 THEN '通过'
			WHEN 4 THEN '不通过'
			ELSE ''
		END StatusName,
		CASE IDF.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '隔日短差'
			ELSE ''
		END TradeTypeName,
		CASE IDF.DealFlag 
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		U.Name ApplyUserName,
		CAST(IDF.Point AS decimal(18,0)) Point,
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
	ORDER BY IDF.ApplyDate DESC,IDF.SerialNo DESC
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetLogInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetLogInfo]
(
	@LogDate datetime,
	@InvestorCode varchar(20)
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @searchDate varchar(20) = CONVERT(varchar(10),@LogDate,120)
	DECLARE @sqlScript varchar(1000) = N'SELECT * FROM LoginLog WHERE [Time] BETWEEN ' + N'''' + @searchDate + N'''' + N' AND DATEADD(DAY,1,' + N'''' + @searchDate + N''''+N') '

	IF ISNULL(@InvestorCode,'') != ''
		SET @sqlScript += N' AND UserCode = ' + N''''+ @InvestorCode + N''''

	--PRINT @sqlScript
	EXEC (@sqlScript) 
 
END









GO
/****** Object:  StoredProcedure [dbo].[sp_GetStockDailyClosePrices]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
		
			INSERT [dbo].[TKLineToday]
			SELECT 
				[StockCode] 
				,[TradeDate]= CONVERT(datetime,@loopDate,120) 
				,[Close] 
			FROM
			(
				SELECT 
					[StockCode]
					,[TradeDate] 
					,[Close]  
					,ROW_NUMBER() OVER(PARTITION BY StockCode ORDER BY TradeDate DESC) RowNumber 
				FROM [FinancialCenter].[dbo].[TKLine_Today] 
				WHERE [TradeDate] < DATEADD(DAY,1,@loopDate)
			) T
			WHERE T.RowNumber =1					
		END

	IF(@loopDate = @nowDate)
		BEGIN
			DELETE FROM [dbo].[TKLineToday] WHERE [TradeDate] = @nowDate
				
			INSERT [dbo].[TKLineToday]
			SELECT 
				[StockCode] 
				,[TradeDate]= CONVERT(datetime,@loopDate,120) 
				,[Close] 
			FROM
			(
				SELECT 
					[StockCode]
					,[TradeDate] 
					,[Close]  
					,ROW_NUMBER() OVER(PARTITION BY StockCode ORDER BY TradeDate DESC) RowNumber 
				FROM [FinancialCenter].[dbo].[TKLine_Today] 
				WHERE [TradeDate] < DATEADD(DAY,1,@loopDate)
			) T
			WHERE T.RowNumber =1
		END

END



GO
/****** Object:  StoredProcedure [dbo].[sp_GetStockProfitContrastData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetStockProfitContrastData]
(
	 @Year int
	,@Month int
	,@AccountId int
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @firstDayOfCurrentMonth datetime = CONVERT(datetime,CAST(@Year AS varchar) + '-' + CAST(@Month AS varchar) + '-01')
	DECLARE @firstDayOfLastMonth datetime = DATEADD(M,-1,@firstDayOfCurrentMonth)
	DECLARE @firstDayOfNextMonth datetime = DATEADD(M,1,@firstDayOfCurrentMonth)
	DECLARE @lastDayOfCurrentMonth datetime = DATEADD(D,-1,@firstDayOfNextMonth)
	DECLARE @lastDayOfLastMonth datetime = DATEADD(D,-1,@firstDayOfCurrentMonth)


	/* 交割单收益核算 */
	SELECT 
		P.AccountId
		,P.StockCode
		,Profit = P.LastMSPositionVolume *(ISNULL(TKC.[Close],0) - ISNULL(TKL.[Close],0)) + P.AccumulatedDealVolume * ISNULL(TKC.[Close],0) + P.AccumulatedActualAmount 	
	INTO #DeliveryProfit
	FROM
	(
		SELECT 
			 AccountId = ISNULL(DEP.AccountId,MSD.AccountId)
			,StockCode = ISNULL(DEP.StockCode,MSD.StockCode)
			,LastMSPositionVolume = ISNULL(MSD.PositionVolume,0)
			,AccumulatedDealVolume = ISNULL(DEP.AccumulatedDealVolume,0) 
			,AccumulatedActualAmount = ISNULL(DEP.AccumulatedActualAmount,0)	
		FROM 
		(
			SELECT 
				 DE.AccountId 			
				,DE.StockCode		
				,AccumulatedDealVolume = SUM(DE.DealVolume) 
				,AccumulatedActualAmount = SUM(DE.ActualAmount)
			FROM DeliveryRecord DE
			WHERE DE.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND DE.AccountId = @AccountId
			GROUP BY DE.AccountId , DE.StockCode
		) DEP
		FULL JOIN 
		(
			SELECT 
				AccountId
				,StockCode
				,PositionVolume
			FROM MSDeliveryDetail 
			WHERE AccountId = @AccountId AND YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth) * 100 + DATEPART(MM,@firstDayOfLastMonth))		
		) MSD
		ON MSD.AccountId = DEP.AccountId AND MSD.StockCode = DEP.StockCode	
	) P
	LEFT JOIN TKLineToday TKC
	ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @lastDayOfCurrentMonth 
	LEFT JOIN TKLineToday TKL
	ON TKL.StockCode = P.StockCode AND TKL.TradeDate = @lastDayOfLastMonth 


	/* 当日委托收益核算 */	
	SELECT 
		P.AccountId
		,P.StockCode
		,Profit = P.LastMSPositionVolume *(ISNULL(TKC.[Close],0) - ISNULL(TKL.[Close],0)) + P.AccumulatedDealVolume * ISNULL(TKC.[Close],0) + P.AccumulatedActualAmount 	
	INTO #DailyProfit
	FROM
	(
		SELECT 
			 AccountId = ISNULL(DAP.AccountId,MSD.AccountId)
			,StockCode = ISNULL(DAP.StockCode,MSD.StockCode)
			,LastMSPositionVolume = ISNULL(MSD.PositionVolume,0)
			,AccumulatedDealVolume = ISNULL(DAP.AccumulatedDealVolume,0) 
			,AccumulatedActualAmount = ISNULL(DAP.AccumulatedActualAmount,0)	
		FROM 
		(
			SELECT 
				 DA.AccountId 			
				,DA.StockCode		
				,AccumulatedDealVolume = SUM(DA.DealVolume) 
				,AccumulatedActualAmount = SUM(DA.ActualAmount)
			FROM DailyRecord DA
			WHERE DA.TradeDate BETWEEN @firstDayOfCurrentMonth AND @lastDayOfCurrentMonth AND DA.AccountId = @AccountId
			GROUP BY DA.AccountId , DA.StockCode
		) DAP
		FULL JOIN 
		(
			SELECT 
				AccountId
				,StockCode
				,PositionVolume = SUM(PositionVolume)
			FROM MSDailyDetail 
			WHERE AccountId = @AccountId AND YearMonth = (DATEPART(YYYY,@firstDayOfLastMonth) * 100 + DATEPART(MM,@firstDayOfLastMonth))
			GROUP BY AccountId, StockCode
		) MSD
		ON MSD.AccountId = DAP.AccountId AND MSD.StockCode = DAP.StockCode
	) P
	LEFT JOIN TKLineToday TKC
	ON TKC.StockCode = P.StockCode AND TKC.TradeDate = @lastDayOfCurrentMonth 
	LEFT JOIN TKLineToday TKL
	ON TKL.StockCode = P.StockCode AND TKL.TradeDate = @lastDayOfLastMonth


	/* 取得账户个股收益核对数据 */
	SELECT 	
		StockCode = SI.FullCode
		,StockName = SI.Name
		,T.AccountId		
		,T.DeliveryAmount		
		,T.DailyAmount 
		,T.ProfitDifference 
	FROM
	(
		SELECT 
			 AccountId = COALESCE(DEP.AccountId, DAP.AccountId)	
			,StockCode = COALESCE(DEP.StockCode, DAP.StockCode)	
			,DeliveryAmount= ISNULL(DEP.Profit,0)			
			,DailyAmount = ISNULL(DAP.Profit,0)
			,ProfitDifference = ISNULL(DEP.Profit,0) - ISNULL(DAP.Profit,0)
		FROM #DeliveryProfit DEP		
		FULL JOIN #DailyProfit DAP
		ON DAP.StockCode = DEP.StockCode
	) T	
	LEFT JOIN StockInfo SI
	ON SI.FullCode = T.StockCode	
	WHERE T.DailyAmount !=0 OR T.DeliveryAmount !=0
	ORDER BY StockCode


	DROP TABLE #DeliveryProfit 
	DROP TABLE #DailyProfit
END













GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationAccuracyProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_IDOperationAccuracyProcess]
(
	@InvestorCode varchar(50),
	@ApplyNo varchar(50),
	@OperateNo varchar(50),
	@VoteFlag int,
	@ReasonContent varchar(1000),
	@IsAdminVeto bit
)
AS
BEGIN

	SET NOCOUNT ON

	IF @IsAdminVeto =1 AND EXISTS ( SELECT 1 FROM InvestmentDecisionOperationAccuracy WHERE OperateNo = @OperateNo AND IsAdminVeto = 1)
		BEGIN 			
			UPDATE InvestmentDecisionOperationAccuracy 
			SET UserCode = @InvestorCode, Flag = @VoteFlag,  Reason = @ReasonContent, JudgeTime = GETDATE()
			WHERE OperateNo = @OperateNo AND IsAdminVeto = 1			
		END 
	ELSE IF @IsAdminVeto = 0 AND EXISTS( SELECT 1 FROM InvestmentDecisionOperationAccuracy WHERE OperateNo = @OperateNo  AND UserCode = @InvestorCode AND IsAdminVeto = 0)	
		BEGIN
			UPDATE InvestmentDecisionOperationAccuracy 
			SET Flag = @VoteFlag,  Reason = @ReasonContent, JudgeTime = GETDATE()
			WHERE  OperateNo = @OperateNo  AND UserCode = @InvestorCode AND IsAdminVeto = 0
		END	
	ELSE
		BEGIN		
			INSERT INTO InvestmentDecisionOperationAccuracy( ApplyNo, OperateNo, UserCode, [Weight], Flag, Reason, JudgeTime, IsAdminVeto)
			VALUES(@ApplyNo, @OperateNo, @InvestorCode, 0,@VoteFlag, @ReasonContent, GETDATE(), @IsAdminVeto)				
		END


	DECLARE @accuracyStatus int = [dbo].[f_GetIDOperationAccuracyStatus](@OperateNo)
	UPDATE InvestmentDecisionOperation
	SET 
		AccuracyPoint =	[dbo].[f_GetIDOperationAccuracyPoint](@OperateNo),
		AccuracyStatus  = @accuracyStatus, 
		UpdateTime = GETDATE()
	WHERE OperateNo = @OperateNo

	/* 准确度投票结束 */
	IF(@accuracyStatus = 3 OR @accuracyStatus =4 )
		UPDATE InvestmentDecisionOperation SET IsStopped =1 , UpdateTime = GETDATE() 
		WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND OperateNo != @OperateNo AND Step = (SELECT Step FROM InvestmentDecisionOperation IDO WHERE IDO.OperateNo = @OperateNo )
 
END












GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationDeleteProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_IDOperationDeleteProcess]
(
	@ApplyNo varchar(50),
	@OperateNo varchar(50)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE InvestmentDecisionOperation  SET IsDeleted = 1, UpdateTime = GETDATE()  WHERE OperateNo = @OperateNo 

	IF NOT EXISTS( SELECT 1 FROM InvestmentDecisionOperation WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 )
		UPDATE InvestmentDecisionApplication SET IsDeleted = 1, UpdateTime = GETDATE() WHERE ApplyNo = @ApplyNo

END








GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationExecuteProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_IDOperationExecuteProcess]
(	
	@ApplyNo varchar (50),
	@OperateNo varchar(50),
	@ExecuteFlag int	
)
AS
BEGIN

	SET NOCOUNT ON

	/*  ExecuteFlag : 1 - 待确认 2 - 已执行 3 - 未执行 */

	IF @ExecuteFlag = 3		
		BEGIN
			DELETE FROM InvestmentDecisionTradeRecord WHERE OperateNo = @OperateNo
			EXEC [dbo].[sp_GenerateIDOperationAccuracyInfo] @ApplyNo = @ApplyNo, @OperateNo = @OperateNo
			UPDATE InvestmentDecisionOperation SET ExecuteFlag = @ExecuteFlag, TradeRecordRelateFlag = 0 , UpdateTime = GETDATE() WHERE OperateNo = @OperateNo
		END
	ELSE IF @ExecuteFlag = 2		
		BEGIN
			DELETE FROM InvestmentDecisionOperationAccuracy WHERE OperateNo = @OperateNo			
			UPDATE InvestmentDecisionOperation SET ExecuteFlag = @ExecuteFlag, AccuracyStatus = 1,IsStopped = 0, UpdateTime = GETDATE() WHERE OperateNo = @OperateNo 
			UPDATE InvestmentDecisionOperation SET IsStopped =1 , UpdateTime = GETDATE() 
			WHERE ApplyNo = @ApplyNo AND IsDeleted = 0 AND OperateNo != @OperateNo AND Step = (SELECT Step FROM InvestmentDecisionOperation IDO WHERE IDO.OperateNo = @OperateNo )
		END
END













GO
/****** Object:  StoredProcedure [dbo].[sp_IDOperationVoteProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_IDOperationVoteProcess]
(
	@InvestorCode varchar(50),
	@ApplyNo varchar(50),
	@OperateNo varchar(50),
	@VoteFlag int,
	@ReasonCategoryId int,
	@ReasonContent varchar(1000),
	@IsAdminVeto bit
)
AS
BEGIN

	SET NOCOUNT ON

	IF @IsAdminVeto = 1 AND EXISTS (SELECT 1 FROM InvestmentDecisionOperationVote WHERE OperateNo = @OperateNo	AND [Type] = 99)
		BEGIN
			UPDATE InvestmentDecisionOperationVote 
			SET UserCode = @InvestorCode , Flag = @VoteFlag, ReasonCategoryId = @ReasonCategoryId, ReasonContent = @ReasonContent, VoteTime = GETDATE()
			WHERE OperateNo = @OperateNo AND [Type] = 99
		END
	ELSE IF @IsAdminVeto = 0 AND EXISTS(SELECT 1 FROM InvestmentDecisionOperationVote WHERE OperateNo = @OperateNo AND UserCode = @InvestorCode AND [Type] < 99)
		BEGIN
			UPDATE InvestmentDecisionOperationVote 
			SET Flag = @VoteFlag, ReasonCategoryId = @ReasonCategoryId, ReasonContent = @ReasonContent, VoteTime = GETDATE()
			WHERE OperateNo = @OperateNo AND UserCode = @InvestorCode  AND [Type] < 99
		END
	ELSE
		BEGIN
			INSERT INTO InvestmentDecisionOperationVote (AuthorityLevel,Flag,ApplyNo,OperateNo,ReasonCategoryId, ReasonContent,[Type], UserCode,VoteTime,[Weight])
			VALUES(0,@VoteFlag,@ApplyNo,@OperateNo,@ReasonCategoryId,@ReasonContent,[dbo].[f_GetIDOperationVoteType](@OperateNo,@InvestorCode,@IsAdminVeto) ,@InvestorCode,GETDATE(),0)
		END


	DECLARE @voteStatus int = [dbo].[f_GetIDOperationVoteStatus](@OperateNo)
	UPDATE InvestmentDecisionOperation
	SET 
		VotePoint =	[dbo].[f_GetIDOperationVotePoint](@OperateNo),
		VoteStatus = @voteStatus, 
		ExecuteFlag = CASE @voteStatus	WHEN 4 THEN  3	ELSE 1 END,
		UpdateTime = GETDATE()
	WHERE OperateNo = @OperateNo

	-- 决策不通过
	IF(@voteStatus = 4)		
		EXEC [dbo].[sp_GenerateIDOperationAccuracyInfo] @ApplyNo = @ApplyNo, @OperateNo = @OperateNo

		
 
END











GO
/****** Object:  StoredProcedure [dbo].[sp_InvestmentDecisionVoteProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

	IF NOT EXISTS( SELECT 1 FROM InvestmentDecisionVote WHERE UserCode = @InvestorCode AND FormSerialNo = @FormSerialNo)
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
/****** Object:  StoredProcedure [dbo].[sp_InvestmentSubjectEditProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[sp_InvestmentSubjectEditProcess]
(	
	@IndustryId int,
	@InvestFund decimal(24,4),
	@NetAsset decimal(24,4),
	@FinancingAmount decimal(24,4),	
	@Remarks varchar(1000)
)
AS
BEGIN

	SET NOCOUNT ON

	IF NOT EXISTS(SELECT 1 FROM InvestmentSubject WHERE IndustryId = @IndustryId)
		INSERT INTO InvestmentSubject(IndustryId,TotalFund,InvestFund,NetAsset,FinancingAmount,Remarks)
		VALUES(@IndustryId,0,@InvestFund,@NetAsset,@FinancingAmount,@Remarks)
	ELSE
		UPDATE InvestmentSubject SET InvestFund = @InvestFund, NetAsset = @NetAsset, FinancingAmount = @FinancingAmount, Remarks = @Remarks WHERE IndustryId = @IndustryId
END














GO
/****** Object:  StoredProcedure [dbo].[sp_InvestmentSubjectProfit]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ================= 修改履历 ==================
-- 2017/1/16 国债回购记录单独核算 
--
-- =============================================

CREATE PROCEDURE [dbo].[sp_InvestmentSubjectProfit]
(
	@FromDate datetime,
	@ToDate datetime
)
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #SubjectAccount
	(
		SubjectId int,
		SubjectName varchar(20),
		SubjectInvestFund decimal(24,4),
		SubjectNetAsset decimal(24,4),
		AccountId int,
		AccountName varchar(50),
		AttributeCode int,
		AttributeName varchar(20),		
		AccountInvestFund decimal(24,4)
	)
	
	INSERT INTO #SubjectAccount
	SELECT
		II.Id 
		,II.Name
		,S.InvestFund
		,S.NetAsset
		,AI.Id
		,AI.Name
		,AI.AttributeCode
		,AI.AttributeName 	
		,AI.InvestFund
	FROM InvestmentSubject S
	LEFT JOIN IndustryInfo II 
	ON S.IndustryId = II.Id
	LEFT JOIN AccountInfo AI
	ON AI.IndustryId = II.Id
	WHERE II.ParentId =3 AND AI.IsDisabled = 0


	INSERT INTO #SubjectAccount
	SELECT 
		4
		,N'自有'
		,0
		,0
		,AI.Id
		,AI.Name
		,AI.AttributeCode
		,AI.AttributeName 
		,AI.InvestFund		
	FROM IndustryInfo II 
	LEFT JOIN AccountInfo AI
	ON AI.IndustryId = II.Id
	WHERE (II.ParentId = 4  OR II.Id = 1) AND AI.IsDisabled = 0	


	UPDATE #SubjectAccount 
	SET 
		SubjectInvestFund = ( SELECT ISNULL(S.InvestFund,0) FROM InvestmentSubject S WHERE S.IndustryId  = 4 )
		,SubjectNetAsset = ( SELECT ISNULL(S.NetAsset,0) FROM InvestmentSubject S WHERE S.IndustryId  = 4 )
	WHERE #SubjectAccount.SubjectId = 4

	SELECT	
		DR.AccountId	
		,DR.StockCode 
		,MAX(DR.StockName) StockName
		,SUM(ISNULL(DR.DealVolume,0)) PositionVolume
		,SUM(ISNULL(DR.ActualAmount,0)) DealProfit
		-- 2017/1/16 国债交易数据的持仓市值和累计收益独立核算
		--,SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0)) PositionValue
		,CASE WHEN MAX(DR.StockName) LIKE 'GC%' THEN SUM(ISNULL(DR.DealVolume,0)) * 100 ELSE SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0)) END PositionValue
		,SUM(ISNULL(DR.ActualAmount,0)) + CASE WHEN MAX(DR.StockName) LIKE 'GC%' THEN SUM(ISNULL(DR.DealVolume,0)) * 100 ELSE SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0)) END AccumulateProfit		
	INTO #AccountPositionFrom
	FROM #SubjectAccount SA
	LEFT JOIN DeliveryRecord DR 
	ON DR.AccountId = SA.AccountId
	LEFT JOIN TKLineToday TKA
	ON TKA.StockCode = DR.StockCode AND TKA.TradeDate = DATEADD(D,-1,@FromDate)
	WHERE DR.TradeDate < @FromDate
	GROUP BY DR.AccountId, DR.StockCode
	
	
	SELECT	
		DR.AccountId	
		,DR.StockCode 
		,MAX(DR.StockName) StockName
		,SUM(ISNULL(DR.DealVolume,0)) PositionVolume
		,SUM(ISNULL(DR.ActualAmount,0)) DealProfit	
		-- 2017/1/16 国债交易数据的持仓市值和累计收益独立核算
		--,SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0)) PositionValue
		,CASE WHEN MAX(DR.StockName) LIKE 'GC%' THEN SUM(ISNULL(DR.DealVolume,0)) * 100 ELSE SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0))  END PositionValue
		,SUM(ISNULL(DR.ActualAmount,0)) + CASE WHEN MAX(DR.StockName) LIKE 'GC%' THEN SUM(ISNULL(DR.DealVolume,0)) * 100 ELSE SUM(ISNULL(DR.DealVolume,0)) * MAX(ISNULL(TKA.[Close],0)) END AccumulateProfit			
		,MAX(ISNULL(TKT.[Close],0)) CurrentPrice
		,MAX(ISNULL(TKY.[Close],0)) LastPrice
		,CASE SUM(ISNULL(DR.DealVolume,0)) WHEN 0 THEN 0.00 ELSE SUM(ISNULL(DR.DealPrice,0) * ISNULL(DR.DealVolume,0)) / SUM(ISNULL(DR.DealVolume,0)) END CostPrice
	INTO #AccountPositionTo
	FROM #SubjectAccount SA
	LEFT JOIN DeliveryRecord DR 
	ON DR.AccountId = SA.AccountId
	LEFT JOIN TKLineToday TKA
	ON TKA.StockCode = DR.StockCode AND TKA.TradeDate = @ToDate
	LEFT JOIN TKLineToday TKT
	ON TKT.StockCode = DR.StockCode AND TKT.TradeDate = CONVERT(varchar(10),GETDATE(),120)
	LEFT JOIN TKLineToday TKY
	ON TKY.StockCode = DR.StockCode AND TKY.TradeDate = CONVERT(varchar(10),DATEADD(D,-1,GETDATE()),120)	
	WHERE DR.TradeDate < DATEADD(D,1, @ToDate)
	GROUP BY DR.AccountId, DR.StockCode
	

	SELECT 
		ROW_NUMBER() OVER (ORDER BY MAX(SA.SubjectName) ) UniqueSerialNo
		,SA.SubjectId 
		,MAX(SA.SubjectName) SubjectName
		,MAX(ISNULL(SA.SubjectInvestFund,0))/10000  SubjectInvestFund
		,MAX(ISNULL(SA.SubjectNetAsset,0))/10000  SubjectNetAsset
		,(SUM(ISNULL(APT.AccumulateProfit ,0))  -  SUM(ISNULL(APF.AccumulateProfit,0)))/10000  SubjectNetProfit
		,SUM(ISNULL(APT.PositionValue,0))/10000 SubjectPositionValue		
	INTO #SubjectProfit		
	FROM #AccountPositionTo APT
	FULL JOIN #AccountPositionFrom APF
	ON APF.AccountId = APT.AccountId AND APF.StockCode = APT.StockCode
	LEFT JOIN #SubjectAccount SA
	ON SA.AccountId = APT.AccountId
	GROUP BY SA.SubjectId

	SELECT 			
		SA.SubjectId 
		,SA.SubjectName
		,SA.AccountName
		,SA.AttributeName 
		,APT.AccountId
		,APT.StockCode
		,APT.StockName
		,APT.CurrentPrice
		,APT.CostPrice
		,APT.LastPrice
		,CASE APT.LastPrice WHEN 0 THEN '0.00%' ELSE (CAST(CAST((APT.CurrentPrice - APT.LastPrice)/APT.LastPrice * 100 AS decimal(18,2)) AS varchar) + '%')END ChangePercentage
		,CAST(APT.PositionVolume /10000.0000 AS decimal(24,2)) StockPositionVolume
		,CAST(APT.PositionValue /10000 AS decimal(24,2)) StockPositionValue 
		,CAST((ISNULL(APT.AccumulateProfit,0) - ISNULL(APF.AccumulateProfit,0))/10000 AS decimal(24,2)) StockProfit 
	INTO #AccoutStockProfit
	FROM #AccountPositionTo APT
	FULL JOIN #AccountPositionFrom APF
	ON APF.AccountId = APT.AccountId AND APF.StockCode = APT.StockCode
	LEFT JOIN #SubjectAccount SA
	ON SA.AccountId = APT.AccountId
	--2017/1/16 账户明细中过滤国债数据
	--WHERE APT.PositionVolume !=0 
	WHERE APT.PositionVolume !=0 AND APT.StockName NOT LIKE 'GC%'

	SELECT 	
		SP.UniqueSerialNo
		,SP.SubjectId 
		,SP.SubjectName
		,SP.SubjectInvestFund
		,(SP.SubjectNetAsset + SP.SubjectNetProfit) SubjectNetAsset
		,SP.SubjectPositionValue 
		,CASE SP.SubjectNetAsset WHEN 0 THEN '0.00%' ELSE CAST(CAST((SP.SubjectPositionValue / SP.SubjectNetAsset) * 100 AS decimal(18,2)) AS varchar) + '%' END SubjectPositionRate
		,SP.SubjectNetProfit
		,CASE SP.SubjectNetAsset WHEN 0 THEN '0.00%' ELSE CAST(CAST((SP.SubjectNetProfit / SP.SubjectNetAsset) * 100 AS decimal(18,2)) AS varchar) + '%' END SubjectNetProfitRate
		,ASP.AccountId
		,ASP.AccountName + ' - ' + ASP.AttributeName AccountDetail
		,ASP.StockCode
		,ASP.StockName
		,ASP.CurrentPrice
		,ASP.CostPrice
		,ASP.ChangePercentage
		,ASP.StockPositionVolume 
		,ASP.StockPositionValue
		,ASP.StockProfit 
		,CASE SP.SubjectNetAsset WHEN 0 THEN '0.00%' ELSE CAST(CAST(ASP.StockProfit / SP.SubjectNetAsset * 100 AS decimal(18,2)) AS varchar) + '%' END StockProfitRate
		,CASE (SP.SubjectNetAsset + SP.SubjectNetProfit) WHEN 0 THEN '0.00%' ELSE CAST(CAST(ASP.StockPositionValue / (SP.SubjectNetAsset + SP.SubjectNetProfit) * 100 AS decimal(18,2)) AS varchar) + '%' END StockPositionValueRate
	FROM #AccoutStockProfit ASP 
	LEFT JOIN #SubjectProfit SP
	ON SP.SubjectId = ASP.SubjectId	
	ORDER BY SubjectName,AccountDetail,StockCode

	--SELECT * FROM #SubjectAccount  ORDER BY SubjectName
	--SELECT * FROM #AccountPositionFrom WHERE PositionVolume !=0  ORDER BY AccountId, StockCode
	--SELECT * FROM #AccountPositionTo WHERE PositionVolume !=0 ORDER BY AccountId, StockCode
	--SELECT * FROM #SubjectProfit ORDER BY SubjectName
	--SELECT * FROM #AccoutStockProfit ORDER BY AccountId, StockCode


	DROP TABLE #SubjectAccount 
	DROP TABLE #AccountPositionFrom 
	DROP TABLE #AccountPositionTo
	DROP TABLE #SubjectProfit
	DROP TABLE #AccoutStockProfit
END









GO
/****** Object:  StoredProcedure [dbo].[sp_InvestorProfitRiskInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InvestorProfitRiskInfo]
(
	@DeptName varchar(20)
	,@EndDate datetime
)
AS
BEGIN

	SET NOCOUNT ON		

	DECLARE @riskLine decimal(18,4) = 0.003

	DECLARE @startDate datetime = @endDate
	DECLARE @loopTime int = 4
	WHILE(@loopTime > 0)
	BEGIN
		SET @startDate = DATEADD(DD,-1,@startDate)
		IF (DATEPART(DW,@startDate) BETWEEN 2 AND 6)
			SET @looptime -= 1
	END


	SELECT 
		InvestorName = U.Name
		,DSI.*	
	INTO #DailyInfo
	FROM
	(
		SELECT 
			InvestorCode
			,TradeDate
			,YearProfit = YearProfit / 10000
			,YearInterest = YearInterest / 10000
			,NetYearProfit = (YearProfit - YearInterest) / 10000 
			,DayProfit = DayProfit / 10000
			,DayInterest 
			,NetDayProfit = (DayProfit - DayInterest) / 10000
			,RateOfReturn = CASE WHEN MarginAmount = 0 THEN 0 ELSE (DayProfit - DayInterest) / MarginAmount END 
			,BuyAmount = BuyAmount / 10000
			,SellAmount = SellAmount / 10000		
			,DealAmount = DealAmount / 10000
			,MarginAmount = MarginAmount / 10000
			,PositionValue = PositionValue / 10000
		FROM DSDailyInvestor  
		WHERE TradeDate BETWEEN @startDate AND @endDate AND [WeekDay] < 6
	) DSI
	INNER JOIN
	(
		SELECT 
			Code
			,Name
		FROM UserInfo U
		WHERE IsDeleted =0 AND DepartmentId IN (SELECT Id FROM DepartmentInfo WHERE Name = @deptName)
	) U
	ON U.Code = DSI.InvestorCode



	SELECT 
		InvestorCode
		,CountOfBreakRiskLine = COUNT(1)
	INTO #RiskInfo
	FROM #DailyInfo
	WHERE RateOfReturn < -@riskLine
	GROUP BY InvestorCode

	TRUNCATE TABLE InvestorProfitRisk
 
	INSERT INTO InvestorProfitRisk
	SELECT 
		D.*
		,CountOfBreakRiskLine = ISNULL(R.CountOfBreakRiskLine,0)
	FROM #DailyInfo D
	LEFT JOIN #RiskInfo	R
	ON R.InvestorCode = D.InvestorCode
	ORDER BY InvestorName, TradeDate

	DROP TABLE #DailyInfo
	DROP TABLE #RiskInfo
END
GO
/****** Object:  StoredProcedure [dbo].[sp_MSDailyProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[sp_MSDailyProcess]
(
	@Year int
	,@Month int
)
AS
BEGIN

	SET NOCOUNT ON			

	DECLARE @currentYearMonth int = @Year * 100 + @Month
	DECLARE @firstDayOfCurrentMonth datetime = CONVERT(datetime,CAST(@Year AS varchar) + '-' + CAST(@Month AS varchar) + '-01')
	DECLARE @lastDayOfCurrentMonth datetime = DATEADD(D,-1, DATEADD(MM,1,@firstDayOfCurrentMonth))
	DECLARE @firstDayOfNextMonth datetime = DATEADD(M,1,@firstDayOfCurrentMonth)
	DECLARE @lastDayOfLastMonth datetime = DATEADD(D,-1,@firstDayOfCurrentMonth)	
	
	DECLARE @currentDay datetime = @firstDayOfCurrentMonth

	WHILE( @currentDay < @firstDayOfNextMonth)
	BEGIN
		EXEC [dbo].[sp_DSDailyProcess] @TradeDate = @currentDay
		SET @currentDay = DATEADD(DAY,1,@currentDay)
	END

	DECLARE @lastDSDateOfLastMonth datetime = (SELECT MAX(TradeDate) FROM DSDeliveryAccount WHERE TradeDate <= @lastDayOfLastMonth)
	DECLARE @lastDSDateOfCurrentMonth datetime =(SELECT MAX(TradeDate) FROM DSDeliveryAccount WHERE TradeDate <= @lastDayOfCurrentMonth )

	INSERT INTO MSDailyInvestor(YearMonth,InvestorCode,PositionValue,BuyAmount,SellAmount,DealAmount,MarginAmount,AccumulatedInterest,YearInterest,MonthInterest,AccumulatedProfit,YearProfit,MonthProfit,WithDrawAmount)	
	SELECT 
		@currentYearMonth
		,C.InvestorCode
		,C.PositionValue
		,C.BuyAmount
		,C.SellAmount
		,C.DealAmount
		,C.MarginAmount
		,C.AccumulatedInterest
		,C.YearInterest
		,C.AccumulatedInterest - ISNULL(L.AccumulatedInterest,0)
		,C.AccumulatedProfit	
		,C.YearProfit
		,C.AccumulatedProfit - ISNULL(L.AccumulatedProfit,0)
		,0
	FROM 
	(
		SELECT 
			InvestorCode
			,PositionValue
			,BuyAmount
			,SellAmount
			,DealAmount
			,MarginAmount
			,AccumulatedInterest
			,YearInterest
			,AccumulatedProfit
			,YearProfit					
		FROM DSDailyInvestor 
		WHERE TradeDate = @lastDSDateOfCurrentMonth 
	) C
	LEFT JOIN 
	(
		SELECT
			InvestorCode
			,AccumulatedInterest
			,AccumulatedProfit
		FROM DSDailyInvestor
		WHERE TradeDate = @lastDSDateOfLastMonth 
	) L
	ON L.InvestorCode = C.InvestorCode



	INSERT INTO MSDailyDetail(YearMonth,AccountId,AccountCode,InvestorCode,StockCode,StockName,PositionVolume,PositionValue,AccumulatedProfit,YearProfit,MonthProfit)
	SELECT
		@currentYearMonth 
		,C.AccountId 
		,C.AccountCode
		,C.InvestorCode
		,C.StockCode
		,C.StockName
		,C.PositionVolume
		,C.PositionValue	
		,C.AccumulatedProfit
		,C.YearProfit
		,C.AccumulatedProfit - ISNULL(L.AccumulatedProfit,0)
	FROM
	(
		SELECT
			AccountId
			,AccountCode
			,InvestorCode
			,StockCode
			,StockName
			,PositionVolume 
			,PositionValue			
			,AccumulatedProfit
			,YearProfit			
		FROM DSDailyDetail 
		WHERE TradeDate = @lastDSDateOfCurrentMonth 
	) C
	LEFT JOIN
	(
		SELECT 
			AccountId
			,InvestorCode
			,StockCode
			,AccumulatedProfit
		FROM DSDailyDetail 
		WHERE TradeDate = @lastDSDateOfLastMonth 
	) L
	ON L.AccountId = C.AccountId AND L.StockCode = C.StockCode AND L.InvestorCode = C.InvestorCode

END









GO
/****** Object:  StoredProcedure [dbo].[sp_MSDeliveryProcess]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_MSDeliveryProcess]
(
	@Year int
	,@Month int
)
AS
BEGIN

	SET NOCOUNT ON			

	DECLARE @currentYearMonth int = @Year * 100 + @Month
	DECLARE @firstDayOfCurrentMonth datetime = CONVERT(datetime,CAST(@Year AS varchar) + '-' + CAST(@Month AS varchar) + '-01')
	DECLARE @lastDayOfCurrentMonth datetime = DATEADD(D,-1, DATEADD(MM,1,@firstDayOfCurrentMonth))
	DECLARE @firstDayOfNextMonth datetime = DATEADD(M,1,@firstDayOfCurrentMonth)
	DECLARE @lastDayOfLastMonth datetime = DATEADD(D,-1,@firstDayOfCurrentMonth)	
	
	DECLARE @currentDay datetime = @firstDayOfCurrentMonth

	WHILE( @currentDay < @firstDayOfNextMonth)
	BEGIN
		EXEC [dbo].[sp_DSDeliveryProcess] @TradeDate = @currentDay
		SET @currentDay = DATEADD(DAY,1,@currentDay)
	END

	DECLARE @lastDSDateOfLastMonth datetime = (SELECT MAX(TradeDate) FROM DSDeliveryAccount WHERE TradeDate <= @lastDayOfLastMonth)
	DECLARE @lastDSDateOfCurrentMonth datetime =(SELECT MAX(TradeDate) FROM DSDeliveryAccount WHERE TradeDate <= @lastDayOfCurrentMonth )

	INSERT INTO MSDeliveryAccount(AccountId,AccountCode,YearMonth,TotalAsset,AvailableFund,PositionValue,FinancingLimit,FinancedAmount,AccumulatedProfit,YearProfit,MonthProfit)	
	SELECT 
		ISNULL(C.AccountId,M.AccountId)
		,ISNULL(C.AccountCode,M.AccountCode)
		,@currentYearMonth
		,ISNULL(M.TotalAsset,0)
		,ISNULL(M.AvailableFund,0)
		,ISNULL(C.PositionValue,M.PositionValue)
		,ISNULL(M.FinancingLimit,0)
		,ISNULL(M.FinancedAmount,0)
		,ISNULL(C.AccumulatedProfit,0)		
		,ISNULL(C.YearProfit,0)
		,ISNULL(C.AccumulatedProfit,0) - ISNULL(L.AccumulatedProfit,0)
	FROM 
	(
		SELECT 
			AccountId
			,AccountCode
			,PositionValue
			,AccumulatedProfit
			,YearProfit		
		FROM DSDeliveryAccount 
		WHERE TradeDate = @lastDSDateOfCurrentMonth 
	) C
	LEFT JOIN 
	(
		SELECT
			AccountId
			,AccumulatedProfit
		FROM DSDeliveryAccount
		WHERE TradeDate = @lastDSDateOfLastMonth 
	) L
	ON L.AccountId = C.AccountId
	FULL JOIN 
	(
		SELECT 
			*
		FROM
		(
			SELECT *
			,RowNumber = ROW_NUMBER() OVER(PARTITION BY AccountId ORDER BY YearMonth DESC) 
			FROM MIAccountFund
			WHERE YearMonth <= @currentYearMonth 
		) T
		WHERE T.RowNumber =1
	) M
	ON M.AccountId = C.AccountId


	INSERT INTO MSDeliveryDetail(AccountId,AccountCode,YearMonth,[StockCode],[StockName],[PositionVolume],[PositionValue],[CostPrice],[AccumulatedProfit],[YearProfit],[MonthProfit])
	SELECT
		C.AccountId 
		,C.AccountCode
		,@currentYearMonth	
		,C.StockCode
		,C.StockName
		,C.PositionVolume
		,C.PositionValue
		,C.CostPrice
		,C.AccumulatedProfit
		,C.YearProfit
		,C.AccumulatedProfit - ISNULL(L.AccumulatedProfit,0)
	FROM
	(
		SELECT
			AccountId
			,AccountCode
			,StockCode
			,StockName
			,PositionVolume 
			,PositionValue
			,CostPrice
			,AccumulatedProfit
			,YearProfit			
		FROM DSDeliveryDetail 
		WHERE TradeDate = @lastDSDateOfCurrentMonth 
	) C
	LEFT JOIN
	(
		SELECT 
			AccountId
			,StockCode
			,AccumulatedProfit
		FROM DSDeliveryDetail 
		WHERE TradeDate = @lastDSDateOfLastMonth 
	) L
	ON L.AccountId = C.AccountId AND L.StockCode = C.StockCode

END








GO
/****** Object:  StoredProcedure [dbo].[sp_StockPositionQuery]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_StockPositionQuery]
(
	@EndDate datetime,
	@InvestorCode varchar(20)
)
AS
BEGIN

	SET NOCOUNT ON


	SELECT * INTO #TempPosition FROM
	(
		SELECT 
			DR.AccountId,			
			(MAX(C.Name) + ' - ' + MAX(C.SecurityCompanyName) + ' - ' + MAX(C.AttributeName)) AccountInfo,
			DR.Beneficiary InvestorCode,
			MAX(U.Name) InvestorName,
			DR.StockCode,
			MAX(DR.StockName) StockName,
			SUM(DR.DealVolume) PositionVolume	
		FROM DailyRecord DR
		LEFT JOIN UserInfo U
		ON DR.Beneficiary = U.Code
		LEFT JOIN AccountInfo C
		ON DR.AccountId = C.Id
		WHERE DR.TradeDate < = @EndDate
		GROUP BY DR.AccountId,DR.Beneficiary,DR.StockCode 
	) T
	WHERE T.PositionVolume !=0

	DECLARE @IsAdmin bit = (SELECT IsAdmin FROM UserInfo WHERE Code = @InvestorCode)

	IF @IsAdmin =1
		BEGIN
			SELECT * FROM #TempPosition P
			INNER JOIN AccountInfo AI
			ON P.AccountId = AI.Id
			WHERE AI.IsDisabled =0			
		END
	ELSE
		BEGIN
			SELECT * FROM #TempPosition P
			WHERE P.AccountId IN 
				(
					SELECT AO.AccountId FROM AccountOperator AO
					INNER JOIN AccountInfo AI
					ON AI.Id = AO.AccountId
					WHERE AI.IsDisabled =0 AND AO.OperatorId = (SELECT ID FROM UserInfo WHERE Code = @InvestorCode)
				)
		END

	DROP TABLE #TempPosition
END








GO
/****** Object:  StoredProcedure [dbo].[sp_TIDayProfit]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_TIDayProfit]
(
	@InvestorCode varchar(20),
	@StockCode varchar(20),
	@TradeDate datetime	
)
AS
BEGIN

	SET NOCOUNT ON

	IF ISNULL(@InvestorCode,'') = ''
		SELECT	
			StockCode = MAX(StockCode)
			,PositionVolume = SUM(PositionVolume)
			,PositionValue = SUM(PositionValue)
			,DayProfit = SUM(DayProfit)			
		FROM DSDailyDetail
		WHERE TradeDate = @TradeDate AND StockCode = @StockCode 		
	ELSE
		SELECT 
			StockCode = MAX(StockCode)
			,PositionVolume = SUM(PositionVolume)
			,PositionValue = SUM(PositionValue)
			,DayProfit = SUM(DayProfit)
		FROM DSDailyDetail
		WHERE TradeDate = @TradeDate AND StockCode = @StockCode AND InvestorCode = @InvestorCode
END





GO
/****** Object:  StoredProcedure [dbo].[sp_TIKLineData]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_TIKLineData]
(
	@StartDate datetime,
	@EndDate datetime,
	@StockCode varchar(20),
	@InvestorCode varchar(20)
)
AS
BEGIN

	SET NOCOUNT ON



	/* 取得日K线数据 */
	SELECT 
		TradeDate
		,StockCode
		,[Open]
		,[Close]
		,[High]
		,[Low]
		,[Volume]
		,[Amount]
		,PreClose
	FROM [FinancialCenter].[dbo].[TKLine_Today]
	WHERE Volume !=0 AND TradeDate BETWEEN @StartDate AND @EndDate AND StockCode = @StockCode
	ORDER BY TradeDate



END




GO
/****** Object:  StoredProcedure [dbo].[sp_TIRecordProfit]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_TIRecordProfit]
(
	@InvestorCode varchar(20),
	@StockCode varchar(20),
	@TradeDate datetime	
)
AS
BEGIN

	SET NOCOUNT ON

	IF ISNULL(@InvestorCode,'') = ''
		SELECT	
			StockCode = MAX(StockCode)
			,PositionVolume = SUM(PositionVolume)
			,PositionValue = SUM(PositionValue)
			,DayProfit = SUM(DayProfit)			
		FROM DSDailyDetail
		WHERE TradeDate = @TradeDate AND StockCode = @StockCode 		
	ELSE
		SELECT 
			StockCode = MAX(StockCode)
			,PositionVolume = SUM(PositionVolume)
			,PositionValue = SUM(PositionValue)
			,DayProfit = SUM(DayProfit)
		FROM DSDailyDetail
		WHERE TradeDate = @TradeDate AND StockCode = @StockCode AND InvestorCode = @InvestorCode
END





GO
/****** Object:  StoredProcedure [dbo].[sp_TITradeInfo]    Script Date: 2017/4/26 14:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[sp_TITradeInfo]
(
	@StartDate datetime,
	@EndDate datetime
)
AS
BEGIN

	SET NOCOUNT ON

	CREATE TABLE #TradeInfo
	(
		InvestorCode varchar(20) NOT NULL,
		InvestorName varchar(20) NOT NULL,
		StockCode varchar(20) NOT NULL,
		StockName varchar(20) NOT NULL,
		TypeSerial int NOT NULL,
	)

	INSERT INTO #TradeInfo
	SELECT 
		InvestorCode = T.Beneficiary
		,InvestorName = U.Name
		,StockCode = T.StockCode
		,StockName = S.Name
		,0
	FROM
	(
		SELECT DISTINCT
			StockCode
			,Beneficiary	
		FROM DailyRecord 
		WHERE TradeDate BETWEEN @StartDate AND @EndDate AND DealVolume !=0
	) T
	LEFT JOIN UserInfo U
	ON U.Code = T.Beneficiary 
	LEFT JOIN StockInfo S
	ON S.FullCode = T.StockCode

	INSERT INTO #TradeInfo
	SELECT
		N''
		,N'全部人员'
		,T.StockCode
		,T.StockName
		,1
	FROM
	(
		SELECT
			StockCode
			,StockName			
		FROM #TradeInfo
		GROUP BY StockCode,StockName
		HAVING COUNT(*) > 1
	) T

	SELECT 
		*
		,TradeCode = StockCode + '-' + InvestorCode
		,DisplayText =  StockCode + ' - ' + StockName + ' - ' + InvestorName
	FROM #TradeInfo
	ORDER BY  StockCode,TypeSerial,InvestorName

	DROP TABLE #TradeInfo
END





GO
