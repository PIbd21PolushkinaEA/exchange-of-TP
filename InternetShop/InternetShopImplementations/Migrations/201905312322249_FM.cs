namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "DateCreate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Baskets", "DateImplement", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Baskets", "DateImplement");
            DropColumn("dbo.Baskets", "DateCreate");
        }
    }
}
