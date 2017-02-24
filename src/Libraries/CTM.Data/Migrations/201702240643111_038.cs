namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _038 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AccountMonthlyFund", newName: "MIAccountFund");
            CreateTable(
                "dbo.MSInvestorProfit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvestorCode = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        WithDrawAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSAccountFund",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
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
                "dbo.MSProfitDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        InvestorCode = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        AccumulatedProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        YearProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        MonthProfit = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSAccountPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MIAccountPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MIAccountFund", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.MIAccountFund", "Month", c => c.Int(nullable: false));
            DropColumn("dbo.MIAccountFund", "YearMonth");
            DropTable("dbo.AccountMonthlyPosition");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccountMonthlyPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        YearMonth = c.Int(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MIAccountFund", "YearMonth", c => c.Int(nullable: false));
            DropColumn("dbo.MIAccountFund", "Month");
            DropColumn("dbo.MIAccountFund", "Year");
            DropTable("dbo.MIAccountPosition");
            DropTable("dbo.MSAccountPosition");
            DropTable("dbo.MSProfitDetail");
            DropTable("dbo.MSAccountFund");
            DropTable("dbo.MSInvestorProfit");
            RenameTable(name: "dbo.MIAccountFund", newName: "AccountMonthlyFund");
        }
    }
}
