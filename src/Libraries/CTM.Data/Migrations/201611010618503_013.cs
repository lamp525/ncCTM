namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _013 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionStockAnalysisDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        InvestorCode = c.String(),
                        AnalysisDate = c.DateTime(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        TradeType = c.Int(nullable: false),
                        Decision = c.String(),
                        PriceRange = c.String(),
                        Reason = c.String(),
                        Accuracy = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PositionStockAnalysisInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        AnalysisDate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PositionStockAnalysisInfo");
            DropTable("dbo.PositionStockAnalysisDetail");
        }
    }
}
