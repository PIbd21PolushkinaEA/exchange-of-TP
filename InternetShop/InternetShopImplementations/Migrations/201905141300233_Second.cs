namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductBaskets", new[] { "ProductID" });
            DropIndex("dbo.ProductBaskets", new[] { "BasketID" });
            DropIndex("dbo.ComponentProducts", new[] { "ComponentID" });
            DropIndex("dbo.ComponentProducts", new[] { "ProductID" });
            DropIndex("dbo.RequestComponents", new[] { "ComponentID" });
            DropIndex("dbo.RequestComponents", new[] { "RequestID" });
            CreateIndex("dbo.ProductBaskets", "ProductId");
            CreateIndex("dbo.ProductBaskets", "BasketId");
            CreateIndex("dbo.ComponentProducts", "ComponentId");
            CreateIndex("dbo.ComponentProducts", "ProductId");
            CreateIndex("dbo.RequestComponents", "ComponentId");
            CreateIndex("dbo.RequestComponents", "RequestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestComponents", new[] { "RequestId" });
            DropIndex("dbo.RequestComponents", new[] { "ComponentId" });
            DropIndex("dbo.ComponentProducts", new[] { "ProductId" });
            DropIndex("dbo.ComponentProducts", new[] { "ComponentId" });
            DropIndex("dbo.ProductBaskets", new[] { "BasketId" });
            DropIndex("dbo.ProductBaskets", new[] { "ProductId" });
            CreateIndex("dbo.RequestComponents", "RequestID");
            CreateIndex("dbo.RequestComponents", "ComponentID");
            CreateIndex("dbo.ComponentProducts", "ProductID");
            CreateIndex("dbo.ComponentProducts", "ComponentID");
            CreateIndex("dbo.ProductBaskets", "BasketID");
            CreateIndex("dbo.ProductBaskets", "ProductID");
        }
    }
}
