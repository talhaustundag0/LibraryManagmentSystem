namespace LibraryManagment.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        piece = c.Int(nullable: false),
                        DOAdd = c.DateTime(nullable: false),
                        WriterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Writers", t => t.WriterID, cascadeDelete: true)
                .Index(t => t.WriterID);
            
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Lastname = c.String(nullable: false, maxLength: 50, unicode: false),
                        TCKNO = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Mail = c.String(maxLength: 100),
                        Password = c.String(),
                        DORegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GivenBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Receiving = c.DateTime(nullable: false),
                        Delivery = c.DateTime(nullable: false),
                        Delivered = c.DateTime(),
                        Members_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.Members_ID)
                .Index(t => t.Members_ID);
            
            CreateTable(
                "dbo.BooksCategories",
                c => new
                    {
                        Books_ID = c.Int(nullable: false),
                        Category_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Books_ID, t.Category_ID })
                .ForeignKey("dbo.Books", t => t.Books_ID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .Index(t => t.Books_ID)
                .Index(t => t.Category_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GivenBooks", "Members_ID", "dbo.Members");
            DropForeignKey("dbo.Books", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.BooksCategories", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.BooksCategories", "Books_ID", "dbo.Books");
            DropIndex("dbo.BooksCategories", new[] { "Category_ID" });
            DropIndex("dbo.BooksCategories", new[] { "Books_ID" });
            DropIndex("dbo.GivenBooks", new[] { "Members_ID" });
            DropIndex("dbo.Books", new[] { "WriterID" });
            DropTable("dbo.BooksCategories");
            DropTable("dbo.GivenBooks");
            DropTable("dbo.Members");
            DropTable("dbo.Writers");
            DropTable("dbo.Books");
            DropTable("dbo.Categories");
        }
    }
}
