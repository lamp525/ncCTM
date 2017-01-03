namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _031 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InvestmentDecisionOperationVote", "IsAdminVeto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvestmentDecisionOperationVote", "IsAdminVeto", c => c.Boolean(nullable: false));
        }
    }
}
