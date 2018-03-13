namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeddatetimeautoupdateissues : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vendor", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vendor", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequestLineItem", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequestLineItem", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequest", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequest", "DateUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseRequest", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequest", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequestLineItem", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseRequestLineItem", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vendor", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vendor", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
