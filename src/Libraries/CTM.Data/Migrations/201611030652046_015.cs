namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _015 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketTrendForecastDetail", "ForecastDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MarketTrendForecastDetail", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MarketTrendForecastDetail", "Remarks", c => c.String());
            AddColumn("dbo.MarketTrendForecastInfo", "ForecastDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MarketTrendForecastInfo", "Result", c => c.String());
            DropColumn("dbo.MarketTrendForecastDetail", "ForecastTime");
            DropColumn("dbo.MarketTrendForecastInfo", "Status");
            DropColumn("dbo.MarketTrendForecastInfo", "ApplyUser");
            DropColumn("dbo.MarketTrendForecastInfo", "ApplyDate");
            DropTable("dbo.CloseStockAnalysisDetail");
            DropTable("dbo.CloseStockAnalysisInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CloseStockAnalysisInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        InvestorCode = c.String(),
                        JudgmentDate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CloseStockAnalysisDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        StockCode = c.String(),
                        StockName = c.String(),
                        TradeType = c.Int(nullable: false),
                        Decision = c.String(),
                        PriceRange = c.String(),
                        Reason = c.String(),
                        Accuracy = c.String(),
                        AnalysisTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MarketTrendForecastInfo", "ApplyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MarketTrendForecastInfo", "ApplyUser", c => c.String());
            AddColumn("dbo.MarketTrendForecastInfo", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.MarketTrendForecastDetail", "ForecastTime", c => c.DateTime());
            DropColumn("dbo.MarketTrendForecastInfo", "Result");
            DropColumn("dbo.MarketTrendForecastInfo", "ForecastDate");
            DropColumn("dbo.MarketTrendForecastDetail", "Remarks");
            DropColumn("dbo.MarketTrendForecastDetail", "CreateTime");
            DropColumn("dbo.MarketTrendForecastDetail", "ForecastDate");
        }
    }
}
