namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _008 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionForm", "PriceBound", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionForm", "PriceBound");
        }
    }
}
