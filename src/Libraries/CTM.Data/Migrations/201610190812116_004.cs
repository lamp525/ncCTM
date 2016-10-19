namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionForm", "Amount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.InvestmentDecisionForm", "Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionForm", "Reason");
            DropColumn("dbo.InvestmentDecisionForm", "Amount");
        }
    }
}
