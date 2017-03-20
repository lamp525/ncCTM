namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _038 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSDailyInvestor", "SellAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.DSDailyInvestor", "DayInterest", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            DropColumn("dbo.DSDailyInvestor", "SellAmont");
            DropColumn("dbo.DSDailyInvestor", "MonthInterest");
            DropColumn("dbo.MIAccountFund", "Year");
            DropColumn("dbo.MIAccountFund", "Month");
            DropColumn("dbo.MIAccountPosition", "Year");
            DropColumn("dbo.MIAccountPosition", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MIAccountPosition", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.MIAccountPosition", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.MIAccountFund", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.MIAccountFund", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.DSDailyInvestor", "MonthInterest", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.DSDailyInvestor", "SellAmont", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            DropColumn("dbo.DSDailyInvestor", "DayInterest");
            DropColumn("dbo.DSDailyInvestor", "SellAmount");
        }
    }
}
