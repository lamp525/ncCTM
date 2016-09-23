namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockTransferInfo", "HolderName", c => c.String());
            AddColumn("dbo.StockTransferInfo", "ReceiverName", c => c.String());
            AddColumn("dbo.StockTransferInfo", "OperatorName", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.StockTransferInfo", "OperatorName");
            DropColumn("dbo.StockTransferInfo", "ReceiverName");
            DropColumn("dbo.StockTransferInfo", "HolderName");
        }
    }
}