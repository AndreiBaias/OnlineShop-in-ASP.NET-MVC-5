namespace Proiect_Tudose_Baias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class option71 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "ProductId", newName: "RequestId");
            RenameIndex(table: "dbo.Orders", name: "IX_ProductId", newName: "IX_RequestId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_RequestId", newName: "IX_ProductId");
            RenameColumn(table: "dbo.Orders", name: "RequestId", newName: "ProductId");
        }
    }
}
