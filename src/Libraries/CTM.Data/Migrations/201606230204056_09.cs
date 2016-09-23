namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _09 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "RandomKey", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserInfo", "Superior", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserInfo", "CooperatorCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "OperatorCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "Beneficiary", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "TradeTime", c => c.String(maxLength: 30));
            AlterColumn("dbo.DailyRecord", "StockCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "StockName", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "DealNo", c => c.String(maxLength: 30));
            AlterColumn("dbo.DailyRecord", "StockHolderCode", c => c.String(maxLength: 30));
            AlterColumn("dbo.DailyRecord", "ContractNo", c => c.String(maxLength: 30));
            AlterColumn("dbo.DailyRecord", "ImportUser", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "UpdateUser", c => c.String(maxLength: 20));
            AlterColumn("dbo.DailyRecord", "Remarks", c => c.String(maxLength: 200));
        }

        public override void Down()
        {
            AlterColumn("dbo.DailyRecord", "Remarks", c => c.String());
            AlterColumn("dbo.DailyRecord", "UpdateUser", c => c.String());
            AlterColumn("dbo.DailyRecord", "ImportUser", c => c.String());
            AlterColumn("dbo.DailyRecord", "ContractNo", c => c.String());
            AlterColumn("dbo.DailyRecord", "StockHolderCode", c => c.String());
            AlterColumn("dbo.DailyRecord", "DealNo", c => c.String());
            AlterColumn("dbo.DailyRecord", "StockName", c => c.String());
            AlterColumn("dbo.DailyRecord", "StockCode", c => c.String());
            AlterColumn("dbo.DailyRecord", "TradeTime", c => c.String());
            AlterColumn("dbo.DailyRecord", "Beneficiary", c => c.String());
            AlterColumn("dbo.DailyRecord", "OperatorCode", c => c.String());
            AlterColumn("dbo.UserInfo", "CooperatorCode", c => c.String());
            AlterColumn("dbo.UserInfo", "Superior", c => c.String());
            AlterColumn("dbo.UserInfo", "RandomKey", c => c.String());
        }
    }
}