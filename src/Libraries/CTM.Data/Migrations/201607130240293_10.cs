namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockTransferInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StockFullCode = c.String(),
                    StockName = c.String(),
                    Holder = c.String(),
                    Receiver = c.String(),
                    OperatorCode = c.String(),
                    TransferVolume = c.Int(nullable: false),
                    TransferPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TransferTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StockTransferRecord",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TransferId = c.Int(nullable: false),
                    RecordId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.StockTransferRecord");
            DropTable("dbo.StockTransferInfo");
        }
    }
}