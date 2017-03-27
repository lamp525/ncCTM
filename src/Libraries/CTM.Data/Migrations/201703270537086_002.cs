namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyRecord", "EntrustVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
            AddColumn("dbo.DailyRecord", "EntrustPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.DailyRecord", "EntrustAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyRecord", "EntrustAmount");
            DropColumn("dbo.DailyRecord", "EntrustPrice");
            DropColumn("dbo.DailyRecord", "EntrustVolume");
        }
    }
}
