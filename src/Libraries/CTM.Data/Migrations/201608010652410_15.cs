namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecuritiesLoanInfo", "TotalAmout", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SecuritiesLoanInfo", "DayAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropColumn("dbo.SecuritiesLoanInfo", "DayAmount");
            DropColumn("dbo.SecuritiesLoanInfo", "TotalAmout");
        }
    }
}