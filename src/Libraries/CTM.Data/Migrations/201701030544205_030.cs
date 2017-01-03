namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _030 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperationVote", "IsAdminVeto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionOperationVote", "IsAdminVeto");
        }
    }
}
