namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _07 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "Password", c => c.String(maxLength: 20));
        }

        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "Password", c => c.String());
        }
    }
}