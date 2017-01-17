namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _034 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentDecisionStockPoolLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockCode = c.String(),
                        Type = c.Int(nullable: false),
                        OperatorCode = c.String(),
                        OperateTime = c.DateTime(nullable: false),
                        Principal = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.StockPoolLog", "OperateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.StockPoolLog", "OperatorTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockPoolLog", "OperatorTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.StockPoolLog", "OperateTime");
            DropTable("dbo.InvestmentDecisionStockPoolLog");
        }
    }
}
