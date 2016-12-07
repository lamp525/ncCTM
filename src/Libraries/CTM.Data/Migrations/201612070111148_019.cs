namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentDecisionAccuracy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplyNo = c.String(),
                        OperateNo = c.String(),
                        UserCode = c.String(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Flag = c.Int(nullable: false),
                        Reason = c.String(),
                        JudgeTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionApplication",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradePlanNo = c.String(),
                        ApplyNo = c.String(),
                        ApplyUser = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        TradeType = c.Int(nullable: false),
                        StopProfitPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        StopProfitBound = c.Decimal(nullable: false, precision: 18, scale: 4),
                        StopLossPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        StopLossBound = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionOperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplyNo = c.String(),
                        OperateNo = c.String(),
                        InitialFlag = c.Boolean(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        DealFlag = c.Boolean(nullable: false),
                        OperateName = c.String(),
                        DealPrice = c.Decimal(nullable: false, precision: 18, scale: 4),
                        PriceBound = c.Decimal(nullable: false, precision: 18, scale: 4),
                        DealVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        DealAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        ReasonCategoryId = c.Int(nullable: false),
                        ReasonContent = c.String(),
                        VoteStatus = c.Int(nullable: false),
                        VotePoint = c.Int(nullable: false),
                        ExecuteFlag = c.Boolean(nullable: false),
                        TradeRecordRelateFlag = c.Boolean(nullable: false),
                        AccuracyStatus = c.Int(nullable: false),
                        AccuracyPoint = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionOperationVote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplyNo = c.String(),
                        OperateNo = c.String(),
                        UserCode = c.String(),
                        Type = c.Int(nullable: false),
                        AuthorityLevel = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Flag = c.Int(nullable: false),
                        ReasonCategoryId = c.String(),
                        ReasonContent = c.String(),
                        VoteTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionTradeRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplyNo = c.String(),
                        OperateNo = c.String(),
                        DailyRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvestmentDecisionTradeRecord");
            DropTable("dbo.InvestmentDecisionOperationVote");
            DropTable("dbo.InvestmentDecisionOperation");
            DropTable("dbo.InvestmentDecisionApplication");
            DropTable("dbo.InvestmentDecisionAccuracy");
        }
    }
}
