namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSDailyDetail", "WeekDay", c => c.Int(nullable: false));
            AddColumn("dbo.DSDailyInvestor", "WeekDay", c => c.Int(nullable: false));
            AddColumn("dbo.DSDeliveryAccount", "WeekDay", c => c.Int(nullable: false));
            AddColumn("dbo.DSDeliveryDetail", "WeekDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DSDeliveryDetail", "WeekDay");
            DropColumn("dbo.DSDeliveryAccount", "WeekDay");
            DropColumn("dbo.DSDailyInvestor", "WeekDay");
            DropColumn("dbo.DSDailyDetail", "WeekDay");
        }
    }
}
