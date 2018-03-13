namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Issueswithdatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "DateUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.User", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
