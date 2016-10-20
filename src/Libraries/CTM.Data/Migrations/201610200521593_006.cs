namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvestmentDecisionCommittee", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.InvestmentDecisionVote", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvestmentDecisionVote", "Weight", c => c.Int(nullable: false));
            AlterColumn("dbo.InvestmentDecisionCommittee", "Weight", c => c.Int(nullable: false));
        }
    }
}
