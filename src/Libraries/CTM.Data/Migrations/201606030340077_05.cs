namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _05 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StockPoolRecord", newName: "StockPoolLog");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.StockPoolLog", newName: "StockPoolRecord");
        }
    }
}