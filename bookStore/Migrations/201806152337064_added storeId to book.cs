namespace bookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstoreIdtobook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Store_Id2", "dbo.Stores");
            DropIndex("dbo.Books", new[] { "Store_Id2" });
            RenameColumn(table: "dbo.Books", name: "Store_Id2", newName: "StoreId");
            AlterColumn("dbo.Books", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "StoreId");
            AddForeignKey("dbo.Books", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "StoreId", "dbo.Stores");
            DropIndex("dbo.Books", new[] { "StoreId" });
            AlterColumn("dbo.Books", "StoreId", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "StoreId", newName: "Store_Id2");
            CreateIndex("dbo.Books", "Store_Id2");
            AddForeignKey("dbo.Books", "Store_Id2", "dbo.Stores", "Id");
        }
    }
}
