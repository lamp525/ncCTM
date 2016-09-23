namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarginTradingInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvestorCode = c.String(),
                        MarginDate = c.DateTime(nullable: false),
                        TradeType = c.Int(nullable: false),
                        IsRepay = c.Boolean(nullable: false),
                        IsFinancing = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        StockFullCode = c.String(),
                        StockName = c.String(),
                        LoanOwnerCode = c.String(),
                        LoanVolume = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MarginTradingInfo");
        }
    }
}
