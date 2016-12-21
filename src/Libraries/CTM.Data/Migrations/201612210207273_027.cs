namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _027 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InvestmentDecisionAccuracy", newName: "InvestmentDecisionOperationAccuracy");
            AlterColumn("dbo.InvestmentDecisionOperation", "ExecuteFlag", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvestmentDecisionOperation", "ExecuteFlag", c => c.Boolean(nullable: false));
            RenameTable(name: "dbo.InvestmentDecisionOperationAccuracy", newName: "InvestmentDecisionAccuracy");
        }
    }
}
