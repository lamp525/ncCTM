namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecuritiesLoanInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OperatorCode = c.String(),
                    AccountId = c.Int(nullable: false),
                    StockFullCode = c.String(),
                    StockName = c.String(),
                    LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LoanVolume = c.Int(nullable: false),
                    LoanDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.SecuritiesLoanInfo");
        }
    }
}