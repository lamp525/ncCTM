namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StockType");
        }

        public override void Down()
        {
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
        }
    }
}