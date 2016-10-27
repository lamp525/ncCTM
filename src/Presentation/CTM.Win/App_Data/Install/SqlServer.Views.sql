
USE [CTMDB]
GO



/*
/****** [v_MTFDetail] ******/
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
/****** [[v_MTFInfo]] ******/
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