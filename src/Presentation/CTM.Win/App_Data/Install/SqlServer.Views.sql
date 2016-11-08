USE [CTMDB]
GO


/******  [v_IDStockPool] ******/
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


/******  [v_MTFDetail] ******/
DROP VIEW [dbo].[v_MTFDetail]
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


/******  [v_PSADetail] ******/
DROP VIEW [dbo].[v_PSADetail]
GO
CREATE VIEW [dbo].[v_PSADetail]
AS
	SELECT   		
		U.Name InvestorName,
		D.*,
		(D.StockCode + ' - ' + D.StockName) StockInfo,
		CASE D.TradeType
			WHEN 1 THEN 'Ä¿±ê'
			WHEN 2 THEN '²¨¶Î'
			WHEN 3 THEN '¸ôÈÕ¶Ì²î'
			ELSE ''
		END TradeTypeName,
		CASE D.Decision
			WHEN '1' THEN '±£Áô'
			WHEN '2' THEN '¼Ó²Ö'
			WHEN '3' THEN '¼õ²Ö'
			WHEN '4' THEN 'ÈÚÈ¯Âô³ö'
			ELSE ''
		END DecisionName
	FROM PositionStockAnalysisDetail D
	LEFT JOIN UserInfo U
	ON D.InvestorCode = U.Code
GO

/******  [v_PSASummary] ******/
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






