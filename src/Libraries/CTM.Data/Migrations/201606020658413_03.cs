namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "Remarks", c => c.String(maxLength: 200));
        }

        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "Remarks", c => c.String());
        }
    }
}