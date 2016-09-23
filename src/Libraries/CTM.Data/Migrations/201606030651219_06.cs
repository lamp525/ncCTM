namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _06 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockPoolEntry",
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

            AddColumn("dbo.StockPoolLog", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.StockPoolLog", "OperatorCode", c => c.String());
            AddColumn("dbo.StockPoolLog", "OperatorTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.StockPoolLog", "AddFlag");
            DropColumn("dbo.StockPoolLog", "FromDate");
            DropColumn("dbo.StockPoolLog", "ToDate");
            DropColumn("dbo.StockPoolLog", "Remarks");
        }

        public override void Down()
        {
            AddColumn("dbo.StockPoolLog", "Remarks", c => c.String());
            AddColumn("dbo.StockPoolLog", "ToDate", c => c.DateTime());
            AddColumn("dbo.StockPoolLog", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockPoolLog", "AddFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.StockPoolLog", "OperatorTime");
            DropColumn("dbo.StockPoolLog", "OperatorCode");
            DropColumn("dbo.StockPoolLog", "Type");
            DropTable("dbo.StockPoolEntry");
        }
    }
}