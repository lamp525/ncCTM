namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountFundTransfer", "TransferAmount", c => c.Decimal(nullable: false, precision: 24, scale: 6));
            AlterColumn("dbo.AccountInitialFund", "Amount", c => c.Decimal(nullable: false, precision: 24, scale: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountInitialFund", "Amount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AlterColumn("dbo.AccountFundTransfer", "TransferAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
        }
    }
}
