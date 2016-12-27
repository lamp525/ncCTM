namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _028 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "Step", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionOperation", "isStopped", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionOperation", "isStopped");
            DropColumn("dbo.InvestmentDecisionOperation", "Step");
        }
    }
}
