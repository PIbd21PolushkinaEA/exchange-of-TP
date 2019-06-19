namespace InternetShopImplementations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComponentProducts", "Brand", c => c.String());
            AddColumn("dbo.ComponentProducts", "Manuf", c => c.String());
            AddColumn("dbo.ComponentProducts", "ComponentRating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComponentProducts", "ComponentRating");
            DropColumn("dbo.ComponentProducts", "Manuf");
            DropColumn("dbo.ComponentProducts", "Brand");
        }
    }
}
