namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "DateCreate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Baskets", "DateCreate");
        }
    }
}
