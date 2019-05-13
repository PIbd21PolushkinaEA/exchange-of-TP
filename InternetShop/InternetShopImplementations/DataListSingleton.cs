using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopModel;
namespace InternetShopImplementations
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Client> Clients { get; set; }
        public List<Product> Products { get; set; }
        public List<Basket> Baskets { get; set; }
        public List<ProductBasket> ProductsBasket { get; set; }
        public List<Component> Components { get; set; }
        public List<Request> Requests { get; set; }
        public List<RequestComponent> RequestComponents { get; set; }
        public List<ComponentProduct> ComponentsProduct { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Products = new List<Product>();
            Baskets = new List<Basket>();
            ProductsBasket = new List<ProductBasket>();
            Components = new List<Component>();
            Requests = new List<Request>();
            RequestComponents = new List<RequestComponent>();
            ComponentsProduct = new List<ComponentProduct>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}

