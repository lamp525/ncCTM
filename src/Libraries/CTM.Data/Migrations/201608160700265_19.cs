namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SecuritiesLoanInfo", newName: "DayMarginTradingInfo");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.DayMarginTradingInfo", newName: "SecuritiesLoanInfo");
        }
    }
}