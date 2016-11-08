namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PositionStockAnalysisSummary", "DealRange", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.PositionStockAnalysisSummary", "DealAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.PositionStockAnalysisDetail", "DealRange", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.PositionStockAnalysisDetail", "DealAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PositionStockAnalysisDetail", "DealAmount");
            DropColumn("dbo.PositionStockAnalysisDetail", "DealRange");
            DropColumn("dbo.PositionStockAnalysisSummary", "DealAmount");
            DropColumn("dbo.PositionStockAnalysisSummary", "DealRange");
        }
    }
}
