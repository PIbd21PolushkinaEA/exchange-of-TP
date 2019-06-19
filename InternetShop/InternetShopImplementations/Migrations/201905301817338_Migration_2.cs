namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "IsReserved", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProductBaskets", "IsReserved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductBaskets", "IsReserved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Baskets", "IsReserved");
        }
    }
}
