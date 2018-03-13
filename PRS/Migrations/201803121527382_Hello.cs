namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hello : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseRequest", "DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.User", "DateUpdate");
            DropColumn("dbo.PurchaseRequest", "DateCated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequest", "DateCated", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "DateUpdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchaseRequest", "DateCreated");
            DropColumn("dbo.User", "DateUpdated");
        }
    }
}
