namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _026 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionAccuracy", "IsAdminVeto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionAccuracy", "IsAdminVeto");
        }
    }
}
