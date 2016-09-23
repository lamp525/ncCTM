namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BandMarginTradingInfo");
            DropTable("dbo.DayMarginTradingInfo");
            DropTable("dbo.TargetMarginTradingInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TargetMarginTradingInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperatorCode = c.String(),
                        MarginFlag = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        StockFullCode = c.String(),
                        StockName = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanVolume = c.Int(nullable: false),
                        FinancingAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanOwnerCode = c.String(),
                        LoanDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DayMarginTradingInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperatorCode = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        StockFullCode = c.String(),
                        StockName = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanVolume = c.Int(nullable: false),
                        FinancingAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanOwnerCode = c.String(),
                        LoanDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BandMarginTradingInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperatorCode = c.String(),
                        MarginFlag = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        StockFullCode = c.String(),
                        StockName = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanVolume = c.Int(nullable: false),
                        FinancingAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        LoanOwnerCode = c.String(),
                        LoanDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
