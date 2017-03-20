namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _037 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DSDailyDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        InvestorCode = c.String(maxLength: 20),
                        StockCode = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 20),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DayProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DSDailyInvestor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        InvestorCode = c.String(maxLength: 20),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        BuyAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        SellAmont = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DealAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MarginAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DayProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSDailyInvestor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        InvestorCode = c.String(maxLength: 20),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        BuyAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        SellAmont = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DealAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MarginAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthInterest = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        WithDrawAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DSDeliveryAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        TotalAsset = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AvailableFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancingLimit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancedAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DayProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSDeliveryAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        TotalAsset = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AvailableFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancingLimit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancedAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MIAccountFund",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        TotalAsset = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AvailableFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancingLimit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancedAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSDailyDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        InvestorCode = c.String(maxLength: 20),
                        StockCode = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 20),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DSDeliveryDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        StockCode = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 20),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        DayProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSDeliveryDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        StockCode = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 20),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MIAccountPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearMonth = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(maxLength: 20),
                        StockCode = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 20),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MIAccountPosition");
            DropTable("dbo.MSDeliveryDetail");
            DropTable("dbo.DSDeliveryDetail");
            DropTable("dbo.MSDailyDetail");
            DropTable("dbo.MIAccountFund");
            DropTable("dbo.MSDeliveryAccount");
            DropTable("dbo.DSDeliveryAccount");
            DropTable("dbo.MSDailyInvestor");
            DropTable("dbo.DSDailyInvestor");
            DropTable("dbo.DSDailyDetail");
        }
    }
}
