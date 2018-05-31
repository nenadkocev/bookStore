namespace bookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAttributeInBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageURL");
        }
    }
}
