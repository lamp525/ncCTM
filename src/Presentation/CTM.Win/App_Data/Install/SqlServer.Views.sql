USE [CTMDB]
GO


/*
/****** 1. [v_MTFDetail] ******/
*/
DROP VIEW [dbo].[v_MTFDetail]
GO
CREATE VIEW [dbo].[v_MTFDetail]
AS
	SELECT   
		D.Id,
		D.SerialNo, 
		U.Code AS InvestorCode, 
		U.Name AS InvestorName, 
		D.[Weight], 
		CAST(CAST(D.[Weight] * 100 AS decimal(18, 2))AS varchar(20)) + '%' AS WeightPercentage, 
		D.AcquaintanceGraphDate, 
		D.Trend, 
		D.[Open], 
		D.Forenoon, 
		D.Afternoon,     
		D.[Close], 
		D.Reason, 
		D.Accuracy, 
		D.ForecastTime
	FROM dbo.MarketTrendForecastDetail AS D 
	LEFT JOIN dbo.UserInfo AS U 
	ON D.InvestorCode = U.Code
GO


/*
/****** 2. [v_MTFInfo] ******/
*/
DROP VIEW [dbo].[v_MTFInfo]
GO
CREATE VIEW [dbo].[v_MTFInfo]
AS
	SELECT 
		I.Id,
		I.SerialNo,
		U.Code ApplyUser,
		U.Name ApplyUserName,	
		I.[Status],
		'' StatusName,
		I.ApplyDate,
		I.CreateTime 
	FROM MarketTrendForecastInfo I
	LEFT JOIN UserInfo U 
	ON I.ApplyUser = U.Code
GO


/*
/****** 3. [v_CSADetail] ******/
*/
DROP VIEW [dbo].[v_CSADetail]
GO
CREATE VIEW [dbo].[v_CSADetail]
AS
	SELECT   
		D.*,
		CASE D.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '日内'
			ELSE ''
		END TradeTypeName
	FROM dbo.CloseStockAnalysisDetail AS D 	
GO


/*
/****** 4. [v_CSAInfo] ******/
*/
DROP VIEW [dbo].[v_CSAInfo]
GO
CREATE VIEW [dbo].[v_CSAInfo]
AS
	SELECT 
		I.Id,
		I.SerialNo,
		U.Code InvestorCode,
		U.Name InvestorName,		
		I.JudgmentDate,
		I.CreateTime ,
		I.Result
	FROM [dbo].[CloseStockAnalysisInfo] I
	LEFT JOIN UserInfo U 
	ON I.InvestorCode = U.Code
GO

/*
/****** 5. [v_IDStockPool] ******/
*/
DROP VIEW [dbo].[v_IDStockPool]
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


/*
/****** 6. [v_PSADetail] ******/
*/
DROP VIEW [dbo].[v_PSADetail]
GO
CREATE VIEW [dbo].[v_PSADetail]
AS
	SELECT   
		D.*,
		U.Name InvestorName,
		CASE D.TradeType
			WHEN 1 THEN '目标'
			WHEN 2 THEN '波段'
			WHEN 3 THEN '隔日短差'
			ELSE ''
		END TradeTypeName,
		CASE D.Decision
			WHEN '1' THEN '保留'
			WHEN '2' THEN '买'
			WHEN '3' THEN '卖'
			ELSE ''
		END DecisionName
	FROM PositionStockAnalysisDetail D
	LEFT JOIN UserInfo U
	ON D.InvestorCode = U.Code
GO


/*
/****** 7. [v_PSASummary] ******/
*/
DROP VIEW [dbo].[v_PSASummary]
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






