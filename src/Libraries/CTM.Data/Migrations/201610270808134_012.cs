namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _012 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CloseStockAnalysisInfo");
            DropTable("dbo.CloseStockAnalysisDetail");
        }
    }
}
