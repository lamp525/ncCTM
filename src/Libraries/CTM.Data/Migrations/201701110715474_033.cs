namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _033 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryRecord", "DealVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
            AlterColumn("dbo.DailyRecord", "DealVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyRecord", "DealVolume", c => c.Int(nullable: false));
            AlterColumn("dbo.DeliveryRecord", "DealVolume", c => c.Int(nullable: false));
        }
    }
}
