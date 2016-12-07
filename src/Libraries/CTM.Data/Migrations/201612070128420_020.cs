namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionApplication", "DepartmentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionApplication", "DepartmentId");
        }
    }
}
