namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        CountOfChoosedProducts = c.Int(nullable: false),
                        SumOfChoosedProducts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        BasketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBaskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        BasketID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        IsReserved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.BasketID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ComponentProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ComponentID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Manufacturer = c.String(nullable: false),
                        Rating = c.Single(nullable: false),
                        Price = c.Int(nullable: false),
                        CountOfAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentID = c.Int(nullable: false),
                        RequestID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentID, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.ComponentID)
                .Index(t => t.RequestID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductBaskets", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ComponentProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.RequestComponents", "RequestID", "dbo.Requests");
            DropForeignKey("dbo.RequestComponents", "ComponentID", "dbo.Components");
            DropForeignKey("dbo.ComponentProducts", "ComponentID", "dbo.Components");
            DropForeignKey("dbo.ProductBaskets", "BasketID", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "Id", "dbo.Clients");
            DropIndex("dbo.RequestComponents", new[] { "RequestID" });
            DropIndex("dbo.RequestComponents", new[] { "ComponentID" });
            DropIndex("dbo.ComponentProducts", new[] { "ProductID" });
            DropIndex("dbo.ComponentProducts", new[] { "ComponentID" });
            DropIndex("dbo.ProductBaskets", new[] { "BasketID" });
            DropIndex("dbo.ProductBaskets", new[] { "ProductID" });
            DropIndex("dbo.Baskets", new[] { "Id" });
            DropTable("dbo.Requests");
            DropTable("dbo.RequestComponents");
            DropTable("dbo.Components");
            DropTable("dbo.ComponentProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductBaskets");
            DropTable("dbo.Clients");
            DropTable("dbo.Baskets");
        }
    }
}
