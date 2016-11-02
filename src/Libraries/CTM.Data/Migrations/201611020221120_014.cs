namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _014 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionStockAnalysisSummary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        Principal = c.String(),
                        AnalysisDate = c.DateTime(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        TradeType = c.Int(nullable: false),
                        Decision = c.String(),
                        PriceRange = c.String(),
                        Reason = c.String(),
                        Accuracy = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PositionStockAnalysisDetail", "Remarks", c => c.String());
            AddColumn("dbo.InvestmentDecisionStockPool", "Principal", c => c.String());
            AddColumn("dbo.InvestmentDecisionStockPool", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionStockPool", "Remarks");
            DropColumn("dbo.InvestmentDecisionStockPool", "Principal");
            DropColumn("dbo.PositionStockAnalysisDetail", "Remarks");
            DropTable("dbo.PositionStockAnalysisSummary");
        }
    }
}
