namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DailyRecord", "TargetPrincipal");
            DropColumn("dbo.DailyRecord", "BandPrincipal");
            DropColumn("dbo.DailyRecord", "DayPrincipal");
        }

        public override void Down()
        {
            AddColumn("dbo.DailyRecord", "DayPrincipal", c => c.String());
            AddColumn("dbo.DailyRecord", "BandPrincipal", c => c.String());
            AddColumn("dbo.DailyRecord", "TargetPrincipal", c => c.String());
        }
    }
}