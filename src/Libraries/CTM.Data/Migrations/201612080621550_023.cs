namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _023 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "OperateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.InvestmentDecisionOperation", "OperateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "OperateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.InvestmentDecisionOperation", "OperateDate");
        }
    }
}
