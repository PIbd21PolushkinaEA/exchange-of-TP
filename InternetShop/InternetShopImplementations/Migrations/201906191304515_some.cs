namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "DateImplement", c => c.DateTime());
            AddColumn("dbo.RequestComponents", "ComponentName", c => c.String());
            AddColumn("dbo.Requests", "RequestName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "RequestName");
            DropColumn("dbo.RequestComponents", "ComponentName");
            DropColumn("dbo.Baskets", "DateImplement");
        }
    }
}
