namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addsaltandhashhtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Salt", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.User", "PasswordNeedsReset", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PasswordNeedsReset");
            DropColumn("dbo.User", "Salt");
        }
    }
}
