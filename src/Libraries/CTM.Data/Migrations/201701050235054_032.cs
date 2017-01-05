namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _032 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvestmentSubject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryId = c.Int(nullable: false),
                        TotalFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        InvestFund = c.Decimal(nullable: false, precision: 24, scale: 4),
                        NetAsset = c.Decimal(nullable: false, precision: 24, scale: 4),
                        FinancingAmount = c.Decimal(nullable: false, precision: 24, scale: 4),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvestmentSubject");
        }
    }
}
