namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "RiskControlLine", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.AccountInfo", "AttributeName", c => c.String());
            AddColumn("dbo.AccountInfo", "TypeName", c => c.String());
            AddColumn("dbo.AccountInfo", "PlanName", c => c.String());
            AddColumn("dbo.AccountInfo", "SecurityCompanyName", c => c.String());
            AlterColumn("dbo.UserInfo", "AllotFund", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.AccountInfo", "TotalFund", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.AccountInfo", "InvestFund", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.AccountInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.AccountInfo", "Balance", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "TradeTime", c => c.String(maxLength: 30));
            AlterColumn("dbo.DeliveryRecord", "StockCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.DeliveryRecord", "StockName", c => c.String(maxLength: 20));
            AlterColumn("dbo.DeliveryRecord", "DealNo", c => c.String(maxLength: 30));
            AlterColumn("dbo.DeliveryRecord", "DealPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "DealAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "ActualAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "StampDuty", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "Commission", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "Incidentals", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DeliveryRecord", "StockHolderCode", c => c.String(maxLength: 30));
            AlterColumn("dbo.DeliveryRecord", "ContractNo", c => c.String(maxLength: 30));
            AlterColumn("dbo.DeliveryRecord", "ImportUser", c => c.String(maxLength: 20));
            AlterColumn("dbo.DeliveryRecord", "UpdateUser", c => c.String(maxLength: 20));
            AlterColumn("dbo.DeliveryRecord", "Remarks", c => c.String(maxLength: 200));
            AlterColumn("dbo.DailyRecord", "DealPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DailyRecord", "DealAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DailyRecord", "ActualAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DailyRecord", "StampDuty", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DailyRecord", "Commission", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DailyRecord", "Incidentals", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.StockTransferInfo", "TransferPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.BandMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.BandMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.BandMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DayMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DayMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.DayMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.TargetMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.TargetMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.TargetMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            DropTable("dbo.UserFundInfo");
            DropTable("dbo.UserIncome");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.UserIncome",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserCode = c.String(),
                    TradeDate = c.DateTime(nullable: false),
                    WeekDay = c.Int(nullable: false),
                    WeekOfYear = c.Int(nullable: false),
                    CurrentAsset = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IncomeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IncomeRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TotalIncomeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TotalIncomeRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    OccupyAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    InputAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    AvailableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PositionRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CreateTime = c.DateTime(nullable: false),
                    CreateUser = c.String(),
                    Remarks = c.String(),
                    FieldA = c.String(),
                    FieldB = c.String(),
                    FieldC = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserFundInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserCode = c.String(),
                    AllotAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Remarks = c.String(),
                    FieldA = c.String(),
                    FieldB = c.String(),
                    FieldC = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AlterColumn("dbo.TargetMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TargetMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TargetMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DayMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DayMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DayMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BandMarginTradingInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BandMarginTradingInfo", "LoanAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BandMarginTradingInfo", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockTransferInfo", "TransferPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "Incidentals", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "Commission", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "StampDuty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "ActualAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "DealAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DailyRecord", "DealPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "Remarks", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "UpdateUser", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "ImportUser", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "ContractNo", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "StockHolderCode", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "Incidentals", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "Commission", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "StampDuty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "ActualAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "DealAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "DealPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryRecord", "DealNo", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "StockName", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "StockCode", c => c.String());
            AlterColumn("dbo.DeliveryRecord", "TradeTime", c => c.String());
            AlterColumn("dbo.AccountInfo", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountInfo", "FinancingAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountInfo", "InvestFund", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountInfo", "TotalFund", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserInfo", "AllotFund", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AccountInfo", "SecurityCompanyName");
            DropColumn("dbo.AccountInfo", "PlanName");
            DropColumn("dbo.AccountInfo", "TypeName");
            DropColumn("dbo.AccountInfo", "AttributeName");
            DropColumn("dbo.UserInfo", "RiskControlLine");
        }
    }
}