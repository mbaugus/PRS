namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Oktryagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorID = c.Int(nullable: false),
                        PartNumber = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(nullable: false, maxLength: 255),
                        PhotoPath = c.String(maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        UpdateLastUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdateLastUserID)
                .ForeignKey("dbo.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.VendorID)
                .Index(t => t.UpdateLastUserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(maxLength: 12),
                        Email = c.String(nullable: false, maxLength: 100),
                        IsReviewer = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdate = c.DateTime(nullable: false),
                        UpdateLastUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdateLastUserID)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.UpdateLastUserID);
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        City = c.String(nullable: false, maxLength: 255),
                        State = c.String(nullable: false, maxLength: 2),
                        PostalCode = c.String(nullable: false, maxLength: 5),
                        Email = c.String(nullable: false, maxLength: 100),
                        IsPreApproved = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        UpdateLastUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdateLastUserID)
                .Index(t => t.Code, unique: true, name: "VendorCodeIndex")
                .Index(t => t.UpdateLastUserID);
            
            CreateTable(
                "dbo.PurchaseRequestLineItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseRequestID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        UpdateLastUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseRequest", t => t.PurchaseRequestID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UpdateLastUserID)
                .Index(t => t.PurchaseRequestID)
                .Index(t => t.ProductID)
                .Index(t => t.UpdateLastUserID);
            
            CreateTable(
                "dbo.PurchaseRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        Justification = c.String(nullable: false, maxLength: 255),
                        DateNeeded = c.DateTime(nullable: false),
                        DeliveryMode = c.String(nullable: false, maxLength: 25),
                        Status = c.String(nullable: false, maxLength: 15),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        ReasonForRejection = c.String(maxLength: 100),
                        DateCated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        UpdateLastUserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdateLastUserID)
                .Index(t => t.UpdateLastUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequestLineItem", "UpdateLastUserID", "dbo.User");
            DropForeignKey("dbo.PurchaseRequestLineItem", "PurchaseRequestID", "dbo.PurchaseRequest");
            DropForeignKey("dbo.PurchaseRequest", "UpdateLastUserID", "dbo.User");
            DropForeignKey("dbo.PurchaseRequestLineItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Vendor", "UpdateLastUserID", "dbo.User");
            DropForeignKey("dbo.Product", "UpdateLastUserID", "dbo.User");
            DropForeignKey("dbo.User", "UpdateLastUserID", "dbo.User");
            DropIndex("dbo.PurchaseRequest", new[] { "UpdateLastUserID" });
            DropIndex("dbo.PurchaseRequestLineItem", new[] { "UpdateLastUserID" });
            DropIndex("dbo.PurchaseRequestLineItem", new[] { "ProductID" });
            DropIndex("dbo.PurchaseRequestLineItem", new[] { "PurchaseRequestID" });
            DropIndex("dbo.Vendor", new[] { "UpdateLastUserID" });
            DropIndex("dbo.Vendor", "VendorCodeIndex");
            DropIndex("dbo.User", new[] { "UpdateLastUserID" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.User", new[] { "UserName" });
            DropIndex("dbo.Product", new[] { "UpdateLastUserID" });
            DropIndex("dbo.Product", new[] { "VendorID" });
            DropTable("dbo.PurchaseRequest");
            DropTable("dbo.PurchaseRequestLineItem");
            DropTable("dbo.Vendor");
            DropTable("dbo.User");
            DropTable("dbo.Product");
        }
    }
}
