namespace CTM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class _08 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DictionaryType", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.DictionaryType", "Remarks", c => c.String(maxLength: 200));
            AlterColumn("dbo.DictionaryInfo", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.DictionaryInfo", "Remarks", c => c.String(maxLength: 200));
            AlterColumn("dbo.DepartmentInfo", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.DepartmentInfo", "PrincipalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.DepartmentInfo", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.DepartmentInfo", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.DepartmentInfo", "Remarks", c => c.String(maxLength: 200));
        }

        public override void Down()
        {
            AlterColumn("dbo.DepartmentInfo", "Remarks", c => c.String());
            AlterColumn("dbo.DepartmentInfo", "Description", c => c.String());
            AlterColumn("dbo.DepartmentInfo", "Name", c => c.String());
            AlterColumn("dbo.DepartmentInfo", "PrincipalCode", c => c.String());
            AlterColumn("dbo.DepartmentInfo", "Code", c => c.String());
            AlterColumn("dbo.DictionaryInfo", "Remarks", c => c.String());
            AlterColumn("dbo.DictionaryInfo", "Name", c => c.String());
            AlterColumn("dbo.DictionaryType", "Remarks", c => c.String());
            AlterColumn("dbo.DictionaryType", "Name", c => c.String());
        }
    }
}