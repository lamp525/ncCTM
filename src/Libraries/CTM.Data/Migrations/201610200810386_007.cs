namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _007 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionForm", "UpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionForm", "UpdateTime");
        }
    }
}
