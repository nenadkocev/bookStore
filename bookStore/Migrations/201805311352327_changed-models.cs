namespace bookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Books", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropIndex("dbo.Books", new[] { "Genre_Id" });
            RenameColumn(table: "dbo.Books", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Books", name: "Genre_Id", newName: "GenreId");
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "GenreId", c => c.Int(nullable: false));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "TelephoneNumber", c => c.String(nullable: false));
            CreateIndex("dbo.Books", "AuthorId");
            CreateIndex("dbo.Books", "GenreId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Books", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            AlterColumn("dbo.Stores", "TelephoneNumber", c => c.String());
            AlterColumn("dbo.Stores", "Address", c => c.String());
            AlterColumn("dbo.Stores", "Name", c => c.String());
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Books", "GenreId", c => c.Int());
            AlterColumn("dbo.Books", "AuthorId", c => c.Int());
            AlterColumn("dbo.Books", "Language", c => c.String());
            AlterColumn("dbo.Books", "ISBN", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
            AlterColumn("dbo.Authors", "Name", c => c.String());
            RenameColumn(table: "dbo.Books", name: "GenreId", newName: "Genre_Id");
            RenameColumn(table: "dbo.Books", name: "AuthorId", newName: "Author_Id");
            CreateIndex("dbo.Books", "Genre_Id");
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Genre_Id", "dbo.Genres", "Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
