using System.Data.Entity.Migrations;

namespace InternetShopImplementations.Migrations {
    public partial class Second_Migration : DbMigration {
        public override void Up() {
            AddColumn("dbo.RequestComponents", "ComponentName", c => c.String());
            AddColumn("dbo.Requests", "RequestName", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.Requests", "RequestName");
            DropColumn("dbo.RequestComponents", "ComponentName");
        }
    }
}