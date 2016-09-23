namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFundInfo", "FieldA", c => c.String());
            AddColumn("dbo.UserFundInfo", "FieldB", c => c.String());
            AddColumn("dbo.UserFundInfo", "FieldC", c => c.String());
            AddColumn("dbo.UserIncome", "FieldA", c => c.String());
            AddColumn("dbo.UserIncome", "FieldB", c => c.String());
            AddColumn("dbo.UserIncome", "FieldC", c => c.String());
            AddColumn("dbo.UserInfo", "FieldA", c => c.String());
            AddColumn("dbo.UserInfo", "FieldB", c => c.String());
            AddColumn("dbo.UserInfo", "FieldC", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldA", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldB", c => c.String());
            AddColumn("dbo.AccountOperator", "FieldC", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldA", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldB", c => c.String());
            AddColumn("dbo.AccountInfo", "FieldC", c => c.String());
            AddColumn("dbo.InvestType", "FieldA", c => c.String());
            AddColumn("dbo.InvestType", "FieldB", c => c.String());
            AddColumn("dbo.InvestType", "FieldC", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldA", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldB", c => c.String());
            AddColumn("dbo.IndustryInfo", "FieldC", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldA", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldB", c => c.String());
            AddColumn("dbo.DictionaryType", "FieldC", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldA", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldB", c => c.String());
            AddColumn("dbo.DictionaryInfo", "FieldC", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldA", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldB", c => c.String());
            AddColumn("dbo.DepartmentInfo", "FieldC", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldA", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldB", c => c.String());
            AddColumn("dbo.DeliveryRecord", "FieldC", c => c.String());
            AddColumn("dbo.DailyRecord", "SplitNo", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldA", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldB", c => c.String());
            AddColumn("dbo.DailyRecord", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolLog", "FieldC", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockTransferInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldA", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldB", c => c.String());
            AddColumn("dbo.StockTransferRecord", "FieldC", c => c.String());
            AddColumn("dbo.StockInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolInfo", "FieldC", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldA", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldB", c => c.String());
            AddColumn("dbo.StockPoolEntry", "FieldC", c => c.String());
            AddColumn("dbo.SecuritiesLoanInfo", "FieldA", c => c.String());
            AddColumn("dbo.SecuritiesLoanInfo", "FieldB", c => c.String());
            AddColumn("dbo.SecuritiesLoanInfo", "FieldC", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.SecuritiesLoanInfo", "FieldC");
            DropColumn("dbo.SecuritiesLoanInfo", "FieldB");
            DropColumn("dbo.SecuritiesLoanInfo", "FieldA");
            DropColumn("dbo.StockPoolEntry", "FieldC");
            DropColumn("dbo.StockPoolEntry", "FieldB");
            DropColumn("dbo.StockPoolEntry", "FieldA");
            DropColumn("dbo.StockPoolInfo", "FieldC");
            DropColumn("dbo.StockPoolInfo", "FieldB");
            DropColumn("dbo.StockPoolInfo", "FieldA");
            DropColumn("dbo.StockInfo", "FieldC");
            DropColumn("dbo.StockInfo", "FieldB");
            DropColumn("dbo.StockInfo", "FieldA");
            DropColumn("dbo.StockTransferRecord", "FieldC");
            DropColumn("dbo.StockTransferRecord", "FieldB");
            DropColumn("dbo.StockTransferRecord", "FieldA");
            DropColumn("dbo.StockTransferInfo", "FieldC");
            DropColumn("dbo.StockTransferInfo", "FieldB");
            DropColumn("dbo.StockTransferInfo", "FieldA");
            DropColumn("dbo.StockPoolLog", "FieldC");
            DropColumn("dbo.StockPoolLog", "FieldB");
            DropColumn("dbo.StockPoolLog", "FieldA");
            DropColumn("dbo.DailyRecord", "FieldC");
            DropColumn("dbo.DailyRecord", "FieldB");
            DropColumn("dbo.DailyRecord", "FieldA");
            DropColumn("dbo.DailyRecord", "SplitNo");
            DropColumn("dbo.DeliveryRecord", "FieldC");
            DropColumn("dbo.DeliveryRecord", "FieldB");
            DropColumn("dbo.DeliveryRecord", "FieldA");
            DropColumn("dbo.DepartmentInfo", "FieldC");
            DropColumn("dbo.DepartmentInfo", "FieldB");
            DropColumn("dbo.DepartmentInfo", "FieldA");
            DropColumn("dbo.DictionaryInfo", "FieldC");
            DropColumn("dbo.DictionaryInfo", "FieldB");
            DropColumn("dbo.DictionaryInfo", "FieldA");
            DropColumn("dbo.DictionaryType", "FieldC");
            DropColumn("dbo.DictionaryType", "FieldB");
            DropColumn("dbo.DictionaryType", "FieldA");
            DropColumn("dbo.IndustryInfo", "FieldC");
            DropColumn("dbo.IndustryInfo", "FieldB");
            DropColumn("dbo.IndustryInfo", "FieldA");
            DropColumn("dbo.InvestType", "FieldC");
            DropColumn("dbo.InvestType", "FieldB");
            DropColumn("dbo.InvestType", "FieldA");
            DropColumn("dbo.AccountInfo", "FieldC");
            DropColumn("dbo.AccountInfo", "FieldB");
            DropColumn("dbo.AccountInfo", "FieldA");
            DropColumn("dbo.AccountOperator", "FieldC");
            DropColumn("dbo.AccountOperator", "FieldB");
            DropColumn("dbo.AccountOperator", "FieldA");
            DropColumn("dbo.UserInfo", "FieldC");
            DropColumn("dbo.UserInfo", "FieldB");
            DropColumn("dbo.UserInfo", "FieldA");
            DropColumn("dbo.UserIncome", "FieldC");
            DropColumn("dbo.UserIncome", "FieldB");
            DropColumn("dbo.UserIncome", "FieldA");
            DropColumn("dbo.UserFundInfo", "FieldC");
            DropColumn("dbo.UserFundInfo", "FieldB");
            DropColumn("dbo.UserFundInfo", "FieldA");
        }
    }
}