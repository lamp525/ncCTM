namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionApplication", "BuyVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
            AddColumn("dbo.InvestmentDecisionApplication", "BuyAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "SellVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
            AddColumn("dbo.InvestmentDecisionApplication", "SellAmount", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "PositionVolume", c => c.Decimal(nullable: false, precision: 24, scale: 0));
            AddColumn("dbo.InvestmentDecisionApplication", "Profit", c => c.Decimal(nullable: false, precision: 24, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "AccuracyStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionApplication", "AccuracyPoint", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionApplication", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvestmentDecisionOperation", "StopProfitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionOperation", "StopProfitBound", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionOperation", "StopLossPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionOperation", "StopLossBound", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionOperation", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.InvestmentDecisionApplication", "StopProfitPrice");
            DropColumn("dbo.InvestmentDecisionApplication", "StopProfitBound");
            DropColumn("dbo.InvestmentDecisionApplication", "StopLossPrice");
            DropColumn("dbo.InvestmentDecisionApplication", "StopLossBound");
            DropColumn("dbo.InvestmentDecisionOperation", "AccuracyStatus");
            DropColumn("dbo.InvestmentDecisionOperation", "AccuracyPoint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvestmentDecisionOperation", "AccuracyPoint", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionOperation", "AccuracyStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDecisionApplication", "StopLossBound", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "StopLossPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "StopProfitBound", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AddColumn("dbo.InvestmentDecisionApplication", "StopProfitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            DropColumn("dbo.InvestmentDecisionOperation", "IsDeleted");
            DropColumn("dbo.InvestmentDecisionOperation", "StopLossBound");
            DropColumn("dbo.InvestmentDecisionOperation", "StopLossPrice");
            DropColumn("dbo.InvestmentDecisionOperation", "StopProfitBound");
            DropColumn("dbo.InvestmentDecisionOperation", "StopProfitPrice");
            DropColumn("dbo.InvestmentDecisionApplication", "IsDeleted");
            DropColumn("dbo.InvestmentDecisionApplication", "AccuracyPoint");
            DropColumn("dbo.InvestmentDecisionApplication", "AccuracyStatus");
            DropColumn("dbo.InvestmentDecisionApplication", "Profit");
            DropColumn("dbo.InvestmentDecisionApplication", "PositionVolume");
            DropColumn("dbo.InvestmentDecisionApplication", "SellAmount");
            DropColumn("dbo.InvestmentDecisionApplication", "SellVolume");
            DropColumn("dbo.InvestmentDecisionApplication", "BuyAmount");
            DropColumn("dbo.InvestmentDecisionApplication", "BuyVolume");
            DropColumn("dbo.InvestmentDecisionApplication", "InitialDealFlag");
        }
    }
}
