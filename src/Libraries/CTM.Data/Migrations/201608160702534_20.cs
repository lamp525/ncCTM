namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BandMarginTradingInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OperatorCode = c.String(),
                    IsBand = c.Boolean(nullable: false),
                    MarginFlag = c.Boolean(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                    AccountId = c.Int(nullable: false),
                    StockFullCode = c.String(),
                    StockName = c.String(),
                    TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LoanVolume = c.Int(nullable: false),
                    FinancingAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LoanOwnerCode = c.String(),
                    LoanDate = c.DateTime(nullable: false),
                    FieldA = c.String(),
                    FieldB = c.String(),
                    FieldC = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.BandMarginTradingInfo");
        }
    }
}