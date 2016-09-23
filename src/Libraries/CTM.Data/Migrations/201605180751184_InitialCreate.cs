namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFundInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserCode = c.String(),
                    AllotAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

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
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeCode = c.Int(nullable: false),
                    Code = c.String(),
                    Name = c.String(),
                    RandomKey = c.String(),
                    Password = c.String(),
                    PositionCode = c.Int(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                    Superior = c.String(),
                    CooperatorCode = c.String(),
                    AllotFund = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IsAdmin = c.Boolean(nullable: false),
                    IsManager = c.Boolean(nullable: false),
                    IsDealer = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AccountOperator",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AccountId = c.Int(nullable: false),
                    OperatorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountInfo", t => t.AccountId)
                .ForeignKey("dbo.UserInfo", t => t.OperatorId)
                .Index(t => t.AccountId)
                .Index(t => t.OperatorId);

            CreateTable(
                "dbo.AccountInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IndustryId = c.Int(nullable: false),
                    TypeCode = c.Int(nullable: false),
                    AttributeCode = c.Int(nullable: false),
                    PlanCode = c.Int(nullable: false),
                    Name = c.String(maxLength: 20),
                    SecurityCompanyCode = c.Int(nullable: false),
                    TotalFund = c.Decimal(nullable: false, precision: 18, scale: 2),
                    InvestFund = c.Decimal(nullable: false, precision: 18, scale: 2),
                    FinancingAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IncomeRate = c.Decimal(nullable: false, precision: 18, scale: 5),
                    StampDutyRate = c.Decimal(nullable: false, precision: 18, scale: 5),
                    CommissionRate = c.Decimal(nullable: false, precision: 18, scale: 5),
                    IncidentalsRate = c.Decimal(nullable: false, precision: 18, scale: 5),
                    NeedAccounting = c.Boolean(nullable: false),
                    IsDisabled = c.Boolean(nullable: false),
                    Remarks = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IndustryInfo", t => t.IndustryId)
                .Index(t => t.IndustryId);

            CreateTable(
                "dbo.IndustryInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    ParentId = c.Int(nullable: false),
                    Name = c.String(),
                    Level = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StockInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(maxLength: 20),
                    FullCode = c.String(maxLength: 20),
                    Name = c.String(maxLength: 20),
                    Remarks = c.String(maxLength: 200),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StockPoolInfo",
                c => new
                {
                    StockId = c.Int(nullable: false),
                    TargetPrincipal = c.String(),
                    BandPrincipal = c.String(),
                    DayPrincipal = c.String(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.StockInfo", t => t.StockId)
                .Index(t => t.StockId);

            CreateTable(
                "dbo.StockPoolRecord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StockId = c.Int(nullable: false),
                    AddFlag = c.Boolean(nullable: false),
                    TargetPrincipal = c.String(),
                    BandPrincipal = c.String(),
                    FromDate = c.DateTime(nullable: false),
                    ToDate = c.DateTime(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StockType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ParentCode = c.String(),
                    Code = c.String(),
                    Name = c.String(),
                    Description = c.String(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.InvestType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ParentCode = c.String(),
                    Code = c.String(),
                    Name = c.String(),
                    Description = c.String(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DictionaryType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DictionaryInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    Code = c.Int(nullable: false),
                    Name = c.String(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryType", t => t.TypeId)
                .Index(t => t.TypeId);

            CreateTable(
                "dbo.DepartmentInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    ParentId = c.Int(nullable: false),
                    PrincipalCode = c.String(),
                    Level = c.Int(nullable: false),
                    Name = c.String(),
                    Description = c.String(),
                    Remarks = c.String(),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DeliveryRecord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TradeDate = c.DateTime(nullable: false),
                    TradeTime = c.String(),
                    AccountId = c.Int(nullable: false),
                    StockCode = c.String(),
                    StockName = c.String(),
                    DealFlag = c.Boolean(nullable: false),
                    DealNo = c.String(),
                    DealPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DealVolume = c.Int(nullable: false),
                    DealAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ActualAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StampDuty = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Incidentals = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StockHolderCode = c.String(),
                    ContractNo = c.String(),
                    ImportUser = c.String(),
                    ImportTime = c.DateTime(nullable: false),
                    UpdateUser = c.String(),
                    UpdateTime = c.DateTime(nullable: false),
                    AuditFlag = c.Boolean(nullable: false),
                    AuditNo = c.Int(),
                    AuditTime = c.DateTime(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DailyRecord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DataType = c.Int(nullable: false),
                    TradeType = c.Int(nullable: false),
                    OperatorCode = c.String(),
                    Beneficiary = c.String(),
                    TargetPrincipal = c.String(),
                    BandPrincipal = c.String(),
                    DayPrincipal = c.String(),
                    TradeDate = c.DateTime(nullable: false),
                    TradeTime = c.String(),
                    AccountId = c.Int(nullable: false),
                    StockCode = c.String(),
                    StockName = c.String(),
                    DealFlag = c.Boolean(nullable: false),
                    DealNo = c.String(),
                    DealPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DealVolume = c.Int(nullable: false),
                    DealAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ActualAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StampDuty = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Incidentals = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StockHolderCode = c.String(),
                    ContractNo = c.String(),
                    ImportUser = c.String(),
                    ImportTime = c.DateTime(nullable: false),
                    UpdateUser = c.String(),
                    UpdateTime = c.DateTime(nullable: false),
                    AuditFlag = c.Boolean(nullable: false),
                    AuditNo = c.Int(),
                    AuditTime = c.DateTime(),
                    Remarks = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DictionaryInfo", "TypeId", "dbo.DictionaryType");
            DropForeignKey("dbo.StockPoolInfo", "StockId", "dbo.StockInfo");
            DropForeignKey("dbo.AccountOperator", "OperatorId", "dbo.UserInfo");
            DropForeignKey("dbo.AccountOperator", "AccountId", "dbo.AccountInfo");
            DropForeignKey("dbo.AccountInfo", "IndustryId", "dbo.IndustryInfo");
            DropIndex("dbo.DictionaryInfo", new[] { "TypeId" });
            DropIndex("dbo.StockPoolInfo", new[] { "StockId" });
            DropIndex("dbo.AccountInfo", new[] { "IndustryId" });
            DropIndex("dbo.AccountOperator", new[] { "OperatorId" });
            DropIndex("dbo.AccountOperator", new[] { "AccountId" });
            DropTable("dbo.DailyRecord");
            DropTable("dbo.DeliveryRecord");
            DropTable("dbo.DepartmentInfo");
            DropTable("dbo.DictionaryInfo");
            DropTable("dbo.DictionaryType");
            DropTable("dbo.InvestType");
            DropTable("dbo.StockType");
            DropTable("dbo.StockPoolRecord");
            DropTable("dbo.StockPoolInfo");
            DropTable("dbo.StockInfo");
            DropTable("dbo.IndustryInfo");
            DropTable("dbo.AccountInfo");
            DropTable("dbo.AccountOperator");
            DropTable("dbo.UserInfo");
            DropTable("dbo.UserIncome");
            DropTable("dbo.UserFundInfo");
        }
    }
}