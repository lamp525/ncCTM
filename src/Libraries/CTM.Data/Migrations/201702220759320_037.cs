namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _037 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountMonthlyFund",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        YearMonth = c.Int(nullable: false),
                        TotalAsset = c.Decimal(nullable: false, precision: 24, scale: 4),
                        AvailableFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        PositionValue = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancingLimit = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancedAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountMonthlyPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        YearMonth = c.Int(nullable: false),
                        StockCode = c.String(),
                        StockName = c.String(),
                        PositionVolume = c.Decimal(nullable: false, precision: 24, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountMonthlyPosition");
            DropTable("dbo.AccountMonthlyFund");
        }
    }
}
