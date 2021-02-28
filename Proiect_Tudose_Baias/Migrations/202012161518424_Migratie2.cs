namespace Proiect_Tudose_Baias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migratie2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductTitle = c.String(),
                        ProductDescription = c.String(),
                        ProductPrice = c.Int(nullable: false),
                        CategoryId = c.String(),
                        ProductRating = c.Single(nullable: false),
                        RequestId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false),
                        ColabId = c.String(nullable: false, maxLength: 128),
                        AdminId = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                        RequestTitle = c.String(),
                        RequestDescription = c.String(),
                        RequestPrice = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .ForeignKey("dbo.AspNetUsers", t => t.ColabId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.RequestId)
                .Index(t => t.RequestId)
                .Index(t => t.ColabId)
                .Index(t => t.AdminId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderAdress = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Products", t => t.product_ProductId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.product_ProductId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        ReviewText = c.String(),
                        ReviewRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequestId", "dbo.Products");
            DropForeignKey("dbo.Requests", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Reviews", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "ColabId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Reviews", new[] { "ApplicationUserId" });
            DropIndex("dbo.Orders", new[] { "product_ProductId" });
            DropIndex("dbo.Orders", new[] { "ApplicationUserId" });
            DropIndex("dbo.Requests", new[] { "CategoryId" });
            DropIndex("dbo.Requests", new[] { "AdminId" });
            DropIndex("dbo.Requests", new[] { "ColabId" });
            DropIndex("dbo.Requests", new[] { "RequestId" });
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Orders");
            DropTable("dbo.Requests");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
