namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _04 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountInfo", "IndustryId", "dbo.IndustryInfo");
            DropIndex("dbo.AccountInfo", new[] { "IndustryId" });
            AddColumn("dbo.AccountInfo", "IndustryInfo_Id", c => c.Int());
            CreateIndex("dbo.AccountInfo", "IndustryInfo_Id");
            AddForeignKey("dbo.AccountInfo", "IndustryInfo_Id", "dbo.IndustryInfo", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.AccountInfo", "IndustryInfo_Id", "dbo.IndustryInfo");
            DropIndex("dbo.AccountInfo", new[] { "IndustryInfo_Id" });
            DropColumn("dbo.AccountInfo", "IndustryInfo_Id");
            CreateIndex("dbo.AccountInfo", "IndustryId");
            AddForeignKey("dbo.AccountInfo", "IndustryId", "dbo.IndustryInfo", "Id");
        }
    }
}