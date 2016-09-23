namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecuritiesLoanInfo", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.SecuritiesLoanInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SecuritiesLoanInfo", "LoanOwnerCode", c => c.String());
            DropColumn("dbo.SecuritiesLoanInfo", "DayAmount");
        }

        public override void Down()
        {
            AddColumn("dbo.SecuritiesLoanInfo", "DayAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SecuritiesLoanInfo", "LoanOwnerCode");
            DropColumn("dbo.SecuritiesLoanInfo", "FinancingAmount");
            DropColumn("dbo.SecuritiesLoanInfo", "DepartmentId");
        }
    }
}