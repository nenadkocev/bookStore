namespace bookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.book_Id)
                .Index(t => t.book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "book_Id", "dbo.Books");
            DropIndex("dbo.Carts", new[] { "book_Id" });
            DropTable("dbo.Carts");
        }
    }
}
