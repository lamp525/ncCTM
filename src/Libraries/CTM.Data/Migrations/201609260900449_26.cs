namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryRecord", "DataType", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryRecord", "AccountCode", c => c.String());
            AddColumn("dbo.DailyRecord", "AccountCode", c => c.String());
            DropColumn("dbo.DeliveryRecord", "AuditFlag");
            DropColumn("dbo.DeliveryRecord", "AuditNo");
            DropColumn("dbo.DeliveryRecord", "AuditTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryRecord", "AuditTime", c => c.DateTime());
            AddColumn("dbo.DeliveryRecord", "AuditNo", c => c.Int());
            AddColumn("dbo.DeliveryRecord", "AuditFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.DailyRecord", "AccountCode");
            DropColumn("dbo.DeliveryRecord", "AccountCode");
            DropColumn("dbo.DeliveryRecord", "DataType");
        }
    }
}
