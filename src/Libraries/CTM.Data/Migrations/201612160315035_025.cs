namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _025 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "AccuracyStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionOperation", "AccuracyPoint", c => c.Int(nullable: false));
            DropColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag");
            AddColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.InvestmentDecisionApplication", "AccuracyStatus");
            DropColumn("dbo.InvestmentDecisionApplication", "AccuracyPoint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvestmentDecisionApplication", "AccuracyPoint", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionApplication", "AccuracyStatus", c => c.Int(nullable: false));
            DropColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag");
            AddColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag", c => c.Int(nullable: false));
            DropColumn("dbo.InvestmentDecisionOperation", "AccuracyPoint");
            DropColumn("dbo.InvestmentDecisionOperation", "AccuracyStatus");
        }
    }
}
