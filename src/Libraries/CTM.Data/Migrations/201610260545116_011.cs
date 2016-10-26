namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _011 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MarketTrendForecastDetail", "AcquaintanceGraphDate", c => c.DateTime());
            AlterColumn("dbo.MarketTrendForecastDetail", "ForecastTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MarketTrendForecastDetail", "ForecastTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MarketTrendForecastDetail", "AcquaintanceGraphDate", c => c.DateTime(nullable: false));
        }
    }
}
