namespace Proiect_Tudose_Baias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class andrei1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reviews", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Reviews", "UserId");
            RenameColumn(table: "dbo.Reviews", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.Reviews", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "UserId" });
            AlterColumn("dbo.Reviews", "UserId", c => c.String());
            RenameColumn(table: "dbo.Reviews", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Reviews", "UserId", c => c.String());
            CreateIndex("dbo.Reviews", "ApplicationUser_Id");
        }
    }
}
