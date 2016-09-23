namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TKLineToday",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StockCode = c.String(maxLength: 16),
                    TradeDate = c.DateTime(nullable: false),
                    Close = c.Decimal(nullable: false, precision: 18, scale: 4),
                })
                .PrimaryKey(t => t.Id);

            DropColumn("dbo.UserInfo", "FieldA");
            DropColumn("dbo.UserInfo", "FieldB");
            DropColumn("dbo.UserInfo", "FieldC");
            DropColumn("dbo.AccountOperator", "FieldA");
            DropColumn("dbo.AccountOperator", "FieldB");
            DropColumn("dbo.AccountOperator", "FieldC");
            DropColumn("dbo.AccountInfo", "FieldA");
            DropColumn("dbo.AccountInfo", "FieldB");
            DropColumn("dbo.AccountInfo", "FieldC");
            DropColumn("dbo.InvestType", "FieldA");
            DropColumn("dbo.InvestType", "FieldB");
            DropColumn("dbo.InvestType", "FieldC");
            DropColumn("dbo.IndustryInfo", "FieldA");
            DropColumn("dbo.IndustryInfo", "FieldB");
            DropColumn("dbo.IndustryInfo", "FieldC");
            DropColumn("dbo.DictionaryType", "FieldA");
            DropColumn("dbo.DictionaryType", "FieldB");
            DropColumn("dbo.DictionaryType", "FieldC");
            DropColumn("dbo.DictionaryInfo", "FieldA");
            DropColumn("dbo.DictionaryInfo", "FieldB");
            DropColumn("dbo.DictionaryInfo", "FieldC");
            DropColumn("dbo.DepartmentInfo", "FieldA");
            DropColumn("dbo.DepartmentInfo", "FieldB");
            DropColumn("dbo.DepartmentInfo", "FieldC");
            DropColumn("dbo.DeliveryRecord", "FieldA");
            DropColumn("dbo.DeliveryRecord", "FieldB");
            DropColumn("dbo.DeliveryRecord", "FieldC");
            DropColumn("dbo.DailyRecord", "FieldA");
            DropColumn("dbo.DailyRecord", "FieldB");
            DropColumn("dbo.DailyRecord", "FieldC");
            DropColumn("dbo.StockPoolLog", "FieldA");
            DropColumn("dbo.StockPoolLog", "FieldB");
            DropColumn("dbo.StockPoolLog", "FieldC");
            DropColumn("dbo.StockTransferInfo", "FieldA");
            DropColumn("dbo.StockTransferInfo", "FieldB");
            DropColumn("dbo.StockTransferInfo", "FieldC");
            DropColumn("dbo.StockTransferRecord", "FieldA");
            DropColumn("dbo.StockTransferRecord", "FieldB");
            DropColumn("dbo.StockTransferRecord", "FieldC");
            DropColumn("dbo.StockInfo", "FieldA");
            DropColumn("dbo.StockInfo", "FieldB");
            DropColumn("dbo.StockInfo", "FieldC");
            DropColumn("dbo.StockPoolInfo", "FieldA");
            DropColumn("dbo.StockPoolInfo", "FieldB");
            DropColumn("dbo.StockPoolInfo", "FieldC");
            DropColumn("dbo.StockPoolEntry", "FieldA");
            DropColumn("dbo.StockPoolEntry", "FieldB");
            DropColumn("dbo.StockPoolEntry", "FieldC");
            DropColumn("dbo.BandMarginTradingInfo", "FieldA");
            DropColumn("dbo.BandMarginTradingInfo", "FieldB");
            DropColumn("dbo.BandMarginTradingInfo", "FieldC");
            DropColumn("dbo.DayMarginTradingInfo", "FieldA");
            DropColumn("dbo.DayMarginTradingInfo", "FieldB");
            DropColumn("dbo.DayMarginTradingInfo", "FieldC");
            DropColumn("dbo.TargetMarginTradingInfo", "FieldA");
            DropColumn("dbo.TargetMarginTradingInfo", "FieldB");
            DropColumn("dbo.TargetMarginTradingInfo", "FieldC");
        }

        public override void Down()
        {
            AddColumn("dbo.TargetMarginTradingInfo", "FieldC", c => c.String());
            AddColumn("dbo.TargetMarginTradingInfo", "FieldB", c => c.String());
            AddColumn("dbo.TargetMarginTradingInfo", "FieldA", c => c.String());
            AddColumn("dbo.DayMarginTradingInfo", "FieldC", c => c.String());
            AddColumn("dbo.DayMarginTradingInfo", "FieldB", c => c.String());
            AddColumn("dbo.DayMarginTradingInfo", "FieldA", c => c.String());
            AddColumn("dbo.BandMarginTradingInfo", "FieldC", c => c.String());
            AddColumn("dbo.BandMarginTradingInfo", "FieldB", c => c.String());
            AddColumn("dbo.BandMarginTradingInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldC", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldB", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldA", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldA", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldC", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldB", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldA", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldC", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldB", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldA", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldC", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldB", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldA", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldC", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldB", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldA", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldC", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldB", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldA", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldC", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldB", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldA", c => c.String());
            AddColumn("dbo.InvestType", "FieldC", c => c.String());
            AddColumn("dbo.InvestType", "FieldB", c => c.String());
            AddColumn("dbo.InvestType", "FieldA", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldC", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldB", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldA", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldC", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldB", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldA", c => c.String());
            AddColumn("dbo.UserInfo", "FieldC", c => c.String());
            AddColumn("dbo.UserInfo", "FieldB", c => c.String());
            AddColumn("dbo.UserInfo", "FieldA", c => c.String());
            DropTable("dbo.TKLineToday");
        }
    }
}