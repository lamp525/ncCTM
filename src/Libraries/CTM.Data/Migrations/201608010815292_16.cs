namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecuritiesLoanInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SecuritiesLoanInfo", "TotalAmout");
        }

        public override void Down()
        {
            AddColumn("dbo.SecuritiesLoanInfo", "TotalAmout", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SecuritiesLoanInfo", "TotalAmount");
        }
    }
}