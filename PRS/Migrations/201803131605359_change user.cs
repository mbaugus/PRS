namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PasswordNeedsChanged", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "PasswordNeedsReset");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "PasswordNeedsReset", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "PasswordNeedsChanged");
        }
    }
}
