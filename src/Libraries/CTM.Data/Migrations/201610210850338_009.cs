namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _009 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionForm", "DepartmentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionForm", "DepartmentId");
        }
    }
}
