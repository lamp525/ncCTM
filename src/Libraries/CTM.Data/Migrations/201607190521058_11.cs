namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockTransferInfo", "HolderAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.StockTransferInfo", "HolderAccountInfo", c => c.String());
            AddColumn("dbo.StockTransferInfo", "ReceivedAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.StockTransferInfo", "ReceivedAccountInfo", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.StockTransferInfo", "ReceivedAccountInfo");
            DropColumn("dbo.StockTransferInfo", "ReceivedAccountId");
            DropColumn("dbo.StockTransferInfo", "HolderAccountInfo");
            DropColumn("dbo.StockTransferInfo", "HolderAccountId");
        }
    }
}