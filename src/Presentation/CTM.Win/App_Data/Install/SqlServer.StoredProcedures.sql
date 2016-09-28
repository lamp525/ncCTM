USE [CTMDB]
GO



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
					 WHERE   [TradeDate] < @loopDate
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

	SET @commandText = N'SELECT IndustryName =II.Name, OperatorNames =[dbo].[f_GetAccountOperatorNames](AI.Id), AI.*
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

	SELECT Delivery.*, (ABS(ISNULL(Delivery.DE_TotalActualAmount,0)) - ABS(ISNULL(Daily.DA_TotalActualAmount,0)))AmountDiff, (ABS(ISNULL(Delivery.DE_TotalDealVolume,0)) - ABS(ISNULL(Daily.DA_TotalDealVolume,0)))VolumeDiff, Daily.*
	FROM
	(
		SELECT CONVERT(nvarchar(10),TradeDate,111) DE_TradeDate, StockCode DE_StockCode,MAX(StockName) DE_StockName, DealFlag DE_DealFlag, SUM(ActualAmount) DE_TotalActualAmount, SUM(DealVolume) DE_TotalDealVolume
		FROM DeliveryRecord 
		WHERE AccountId = @AccountId AND TradeDate >= @DateFrom AND TradeDate <= @DateTo
		GROUP BY TradeDate,StockCode,DealFlag 
	) Delivery
	FULL JOIN 
	(
		SELECT CONVERT(nvarchar(10),TradeDate,111) DA_TradeDate, StockCode DA_StockCode, MAX(StockName) DA_StockName, DealFlag DA_DealFlag, SUM(ActualAmount) DA_TotalActualAmount, SUM(DealVolume) DA_TotalDealVolume
		FROM DailyRecord 
		WHERE AccountId = @AccountId AND TradeDate >= @DateFrom AND TradeDate <= @DateTo
		GROUP BY TradeDate,StockCode,DealFlag 
	) Daily 
	ON Daily.DA_TradeDate=Delivery.DE_TradeDate AND Daily.DA_StockCode=Delivery.DE_StockCode AND Daily.DA_DealFlag = Delivery.DE_DealFlag

END
GO

