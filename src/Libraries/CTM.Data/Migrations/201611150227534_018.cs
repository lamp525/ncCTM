namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCode = c.String(maxLength: 20),
                        UserName = c.String(maxLength: 20),
                        IP = c.String(maxLength: 20),
                        MAC = c.String(maxLength: 50),
                        Time = c.DateTime(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginLog");
        }
    }
}
