namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _022 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "OperateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvestmentDecisionOperation", "OperateTime");
        }
    }
}
