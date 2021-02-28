namespace Proiect_Tudose_Baias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class andrei2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Products", new[] { "Cart_CartId" });
            DropColumn("dbo.Products", "Cart_CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Cart_CartId", c => c.Int());
            CreateIndex("dbo.Products", "Cart_CartId");
            AddForeignKey("dbo.Products", "Cart_CartId", "dbo.Carts", "CartId");
        }
    }
}
