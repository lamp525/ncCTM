namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentDecisionCommittee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        Status = c.Int(nullable: false),
                        Point = c.Int(nullable: false),
                        ApplyDate = c.DateTime(nullable: false),
                        ApplyUser = c.String(),
                        StockFullCode = c.String(),
                        StockName = c.String(),
                        TradeType = c.Int(nullable: false),
                        DealFlag = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Volume = c.Decimal(nullable: false, precision: 24, scale: 0),
                        Profit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        RelateTradePlanNo = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentDecisionVote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormSerialNo = c.String(),
                        UserCode = c.String(),
                        AuthorityLevel = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Flag = c.Int(nullable: false),
                        Reason = c.String(),
                        VoteTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvestmentDecisionVote");
            DropTable("dbo.InvestmentDecisionForm");
            DropTable("dbo.InvestmentDecisionCommittee");
        }
    }
}
