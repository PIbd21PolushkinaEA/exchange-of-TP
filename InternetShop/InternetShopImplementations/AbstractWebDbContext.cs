using System.Data.Entity;
using System.Data.Entity.SqlServer;
using InternetShopModel;

namespace InternetShopImplementations {
    public class AbstractWebDbContext : DbContext {
        public AbstractWebDbContext() : base("AbstractWebDbContext") {
            //настройки конфигурации для entity            
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }

        public virtual DbSet<Component> Components { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ComponentProduct> ComponentsProduct { get; set; }

        public virtual DbSet<ProductBasket> ProductsBasket { get; set; }

        public virtual DbSet<RequestComponent> RequestComponents { get; set; }
    }
}