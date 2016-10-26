namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _010 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentDecisionStockPool",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockCode = c.String(),
                        StockName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarketTrendForecastDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        InvestorCode = c.String(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        AcquaintanceGraphDate = c.DateTime(nullable: false),
                        Trend = c.String(),
                        Open = c.String(),
                        Forenoon = c.String(),
                        Afternoon = c.String(),
                        Close = c.String(),
                        Reason = c.String(),
                        Accuracy = c.String(),
                        ForecastTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarketTrendForecastInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        Status = c.Int(nullable: false),
                        ApplyUser = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MarketTrendForecastInfo");
            DropTable("dbo.MarketTrendForecastDetail");
            DropTable("dbo.InvestmentDecisionStockPool");
        }
    }
}
