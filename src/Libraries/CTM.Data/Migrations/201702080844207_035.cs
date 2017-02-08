namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _035 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentPlanRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        InvestorCode = c.String(),
                        AnalysisDate = c.DateTime(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        Trend = c.Int(nullable: false),
                        Probability = c.Int(),
                        Logic = c.String(),
                        Scheme = c.Int(nullable: false),
                        TradeType = c.Int(nullable: false),
                        OperateMode = c.Int(nullable: false),
                        Expected = c.String(),
                        Unexpected = c.String(),
                        PlanPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        PlanVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        PlanAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LossPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ProfitPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        DealDate = c.DateTime(),
                        DealPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        DealVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        DealAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        Summary = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvestmentPlanRecord");
        }
    }
}
