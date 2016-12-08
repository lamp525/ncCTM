namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "OperateUser", c => c.String());
            AlterColumn("dbo.InvestmentDecisionOperationVote", "ReasonCategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.InvestmentDecisionOperation", "OperateName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "OperateName", c => c.String());
            AlterColumn("dbo.InvestmentDecisionOperationVote", "ReasonCategoryId", c => c.String());
            DropColumn("dbo.InvestmentDecisionOperation", "OperateUser");
        }
    }
}
