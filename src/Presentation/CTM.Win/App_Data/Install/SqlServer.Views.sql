USE [CTMDB]
GO
/****** Object:  View [dbo].[v_PSASummary]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_PSASummary]
GO
/****** Object:  View [dbo].[v_PSADetail]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_PSADetail]
GO
/****** Object:  View [dbo].[v_MTFDetail]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_MTFDetail]
GO
/****** Object:  View [dbo].[v_IPRDetail]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IPRDetail]
GO
/****** Object:  View [dbo].[v_InvestmentSubject]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_InvestmentSubject]
GO
/****** Object:  View [dbo].[v_IDStockPoolLog]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IDStockPoolLog]
GO
/****** Object:  View [dbo].[v_IDStockPool]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IDStockPool]
GO
/****** Object:  View [dbo].[v_IDOperationAccuracy]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IDOperationAccuracy]
GO
/****** Object:  View [dbo].[v_IDOperation]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IDOperation]
GO
/****** Object:  View [dbo].[v_IDApplication]    Script Date: 2017/2/9 12:36:45 ******/
DROP VIEW [dbo].[v_IDApplication]
GO
/****** Object:  View [dbo].[v_IDApplication]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_IDApplication]
AS
	SELECT 	
		IDA.*
		,D.Name DepartmentName
		,CASE IDA.[Status]
			WHEN 0 THEN '进行中'
			WHEN 99 THEN '完成'			
			ELSE ''
		 END StatusName
		,CASE IDA.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '隔日短差'
			ELSE ''
		 END TradeTypeName		
		,U.Name ApplyUserName
		,ApplyType = [dbo].[f_GetIDOperationApplyType](IDA.ApplyNo)	
		,AccuracyEvaluateOperateNo = [dbo].[f_GetIDAccuracyEvaluateOperateNo](IDA.ApplyNo)
		,FinishConfirmFlag = [dbo].[f_GetIDFinishConfirmFlag](IDA.ApplyNo)
		,CurrentStep = [dbo].[f_GetIDOperationStep](IDA.ApplyNo)
		,T.[Close] LatestClosePrice
		,CurrentPosition = [dbo].[f_GetIDPositionVolume](IDA.ApplyNo)		
		,AvgCostPrice = [dbo].[f_GetIDAvgCostPrice](IDA.ApplyNo)
		,CurrentProfit = CAST([dbo].[f_GetIDProfit](IDA.ApplyNo)/10000 AS decimal(24,2))
	FROM InvestmentDecisionApplication 	IDA
	LEFT JOIN UserInfo U
	ON IDA.ApplyUser = U.Code
	LEFT JOIN DepartmentInfo D
	ON IDA.DepartmentId = D.Id 	
	LEFT JOIN TKLineToday T
	ON T.StockCode = IDA.StockCode AND T.TradeDate = CONVERT(varchar(10), GETDATE(),120)
	WHERE IDA.IsDeleted = 0














GO
/****** Object:  View [dbo].[v_IDOperation]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_IDOperation]
AS
	SELECT 	
		IDO.*,
		CASE IDO.DealFlag 
			WHEN 1 THEN '买入'
			WHEN 0 THEN '卖出'
			ELSE ''
		END DealFlagName,
		CASE IDO.VoteStatus
			WHEN 1 THEN '待决策'
			WHEN 2 THEN '决策中'
			WHEN 3 THEN '通过'
			WHEN 4 THEN '不通过'
			ELSE ''
		END VoteStatusName,
		CASE IDO.AccuracyStatus
			WHEN 1 THEN '待评定'
			WHEN 2 THEN '评定中'
			WHEN 3 THEN '准确'
			WHEN 4 THEN '不准确'
			ELSE ''
		END AccuracyStatusName,
		CASE IDO.ExecuteFlag
			WHEN 1 THEN '待确认'
			WHEN 2 THEN '已执行'
			WHEN 3 THEN '未执行'
			ELSE ''
		END	ExecuteFlagName,
		CASE IDO.TradeRecordRelateFlag
			WHEN 0 THEN '未关联'
			WHEN 1 THEN '已关联'
			ELSE ''
		END RelateFlagName,
		CASE IDO.IsStopped 
			WHEN 0 THEN '正常'
			WHEN 1 THEN '已中止'
		END StopName,
		U.Name OperateUserName,
		BoundDetail = CAST(CAST(IDO.PriceBound * 100 AS numeric(10,0)) AS varchar) + '% ' + '(' + CAST(CAST((1 - IDO.PriceBound) * IDO.DealPrice AS decimal(18,2)) AS varchar) + ' - ' + CAST(CAST((1 + IDO.PriceBound) * IDO.DealPrice AS numeric(18,2)) AS varchar) + ')',
		ProfitBoundDetail = CAST(CAST(IDO.StopProfitBound * 100 AS numeric(10,0)) AS varchar) + '% ' + '(' + CAST(CAST((1 - IDO.StopProfitBound) * IDO.StopProfitPrice AS decimal(18,2)) AS varchar) + ' - ' + CAST(CAST((1 + IDO.StopProfitBound) * IDO.StopProfitPrice AS numeric(18,2)) AS varchar) + ')',
		LossBoundDetail = CAST(CAST(IDO.StopLossBound * 100 AS numeric(10,0)) AS varchar) + '% ' + '(' + CAST(CAST((1 - IDO.StopLossBound) * IDO.StopLossPrice AS decimal(18,2)) AS varchar) + ' - ' + CAST(CAST((1 + IDO.StopLossBound) * IDO.StopLossPrice AS numeric(18,2)) AS varchar) + ')',		
		FormattedDealAmount = CAST(IDO.DealAmount /10000 AS decimal(24,2)) 
	FROM InvestmentDecisionOperation AS IDO
	LEFT JOIN UserInfo AS U
	ON U.Code = IDO.OperateUser 
	WHERE IDO.IsDeleted = 0
























GO
/****** Object:  View [dbo].[v_IDOperationAccuracy]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[v_IDOperationAccuracy]
AS
	SELECT 	
		IDOA.Id
		,IDOA.ApplyNo
		,IDOA.OperateNo
		,U.Code InvestorCode
		,U.Name InvestorName
		,IDOA.IsAdminVeto
		,CASE IDOA.IsAdminVeto
			WHEN 0 THEN '决策委员会'
			WHEN 1 THEN '管理员否决权'
			ELSE ''
		 END TypeName
		,CAST(CAST(IDOA.[Weight]*100 AS numeric(18,2)) AS varchar ) + '%' WeightPercentage
		,CASE IDOA.Flag
			WHEN 0 THEN '未投票'
			WHEN 1 THEN '准确'
			WHEN 2 THEN '不准确'
			WHEN 3 THEN '弃权'
			ELSE ''
		  END FlagName
		 ,IDOA.Reason 
		 ,CASE IDOA.Flag
			WHEN 0 THEN NULL
			ELSE IDOA.JudgeTime
		  END JudgeTime
	FROM InvestmentDecisionOperationAccuracy AS IDOA
	LEFT JOIN UserInfo AS U
	ON U.Code = IDOA.UserCode 








GO
/****** Object:  View [dbo].[v_IDStockPool]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_IDStockPool]
AS
	SELECT   
		P.*,
		U.Name PrincipalName
	FROM InvestmentDecisionStockPool P
	LEFT JOIN UserInfo U
	ON P.Principal = U.Code





GO
/****** Object:  View [dbo].[v_IDStockPoolLog]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_IDStockPoolLog]
AS
	SELECT   
		 L.StockCode
		,I.Name StockName
		,L.[Type]
		,L.OperateTime 
		,PU.Name PrincipalName
		,OU.Name OperatorName
	FROM InvestmentDecisionStockPoolLog L
	LEFT JOIN StockInfo I
	ON I.FullCode = L.StockCode
	LEFT JOIN UserInfo PU
	ON L.Principal = PU.Code
	LEFT JOIN UserInfo OU
	ON L.OperatorCode =OU.Code



GO
/****** Object:  View [dbo].[v_InvestmentSubject]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_InvestmentSubject]
AS

	SELECT 
		II.Id IndustryId
		,II.Name SubjectName
		,[dbo].[f_GetIndustryFullName](II.Id,N'->') SubjectFullName	
		,ISNULL(S.TotalFund,0)/10000 TotalFund		
		,ISNULL(S.InvestFund,0)/10000 InvestFund
		,ISNULL(S.NetAsset,0)/10000 NetAsset
		,ISNULL(S.FinancingAmount,0)/10000 FinancingAmount
		,ISNULL(S.Remarks,'') Remarks
	 FROM IndustryInfo II
	 LEFT JOIN InvestmentSubject S
 ON S.IndustryId = II.Id




GO
/****** Object:  View [dbo].[v_IPRDetail]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_IPRDetail]
AS
	SELECT  
		 R.*	
		,R.StockCode + ' - ' + R.StockName StockInfo
		,U.Name InvestorName	
	FROM InvestmentPlanRecord R
	LEFT JOIN UserInfo U
	ON R.InvestorCode = U.Code

GO
/****** Object:  View [dbo].[v_MTFDetail]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_MTFDetail]
AS
	SELECT   	
		U.Name AS InvestorName, 
		D.*, 
		CAST(CAST(D.[Weight] * 100 AS decimal(18, 2))AS varchar(20)) + '%' AS WeightPercentage	
	FROM dbo.MarketTrendForecastDetail AS D 
	LEFT JOIN dbo.UserInfo AS U 
	ON D.InvestorCode = U.Code





GO
/****** Object:  View [dbo].[v_PSADetail]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_PSADetail]
AS
	SELECT   		
		U.Name InvestorName,
		D.*,
		(D.StockCode + ' - ' + D.StockName) StockInfo,
		CASE D.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '隔日短差'
			ELSE ''
		END TradeTypeName,
		CASE D.Decision
			WHEN '1' THEN '保留'
			WHEN '2' THEN '加仓'
			WHEN '3' THEN '减仓'
			WHEN '4' THEN '融券卖出'
			WHEN '5' THEN '清仓'
			ELSE ''
		END DecisionName
	FROM PositionStockAnalysisDetail D
	LEFT JOIN UserInfo U
	ON D.InvestorCode = U.Code






GO
/****** Object:  View [dbo].[v_PSASummary]    Script Date: 2017/2/9 12:36:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_PSASummary]
AS
	SELECT   
		S.*,
		U.Name PrincipalName
	FROM PositionStockAnalysisSummary S
	LEFT JOIN UserInfo U
	ON S.Principal = U.Code





GO
