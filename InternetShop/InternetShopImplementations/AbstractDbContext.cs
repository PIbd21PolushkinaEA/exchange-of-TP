using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using InternetShopModel;
namespace InternetShopImplementations
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity            
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
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
