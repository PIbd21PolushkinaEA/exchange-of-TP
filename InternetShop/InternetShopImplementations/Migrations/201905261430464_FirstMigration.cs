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
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        NameBuy = c.String(nullable: false),
                        CountOfChoosedProducts = c.Int(nullable: false),
                        SumOfChoosedProducts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBaskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BasketId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Count = c.Int(nullable: false),
                        IsReserved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BasketId);
            
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
                        ComponentId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ComponentName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ComponentId)
                .Index(t => t.ProductId);
            
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
                        ComponentId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                        CountComponents = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.ComponentId)
                .Index(t => t.RequestId);
            
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
            DropForeignKey("dbo.ProductBaskets", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ComponentProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RequestComponents", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestComponents", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.ComponentProducts", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.ProductBaskets", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "ClientId", "dbo.Clients");
            DropIndex("dbo.RequestComponents", new[] { "RequestId" });
            DropIndex("dbo.RequestComponents", new[] { "ComponentId" });
            DropIndex("dbo.ComponentProducts", new[] { "ProductId" });
            DropIndex("dbo.ComponentProducts", new[] { "ComponentId" });
            DropIndex("dbo.ProductBaskets", new[] { "BasketId" });
            DropIndex("dbo.ProductBaskets", new[] { "ProductId" });
            DropIndex("dbo.Baskets", new[] { "ClientId" });
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
