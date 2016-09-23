namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserInfo", "Name", c => c.String(maxLength: 20));
        }

        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "Name", c => c.String());
            AlterColumn("dbo.UserInfo", "Code", c => c.String());
        }
    }
}