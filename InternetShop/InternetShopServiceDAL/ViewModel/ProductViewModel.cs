using System.Collections.Generic;

namespace InternetShopServiceDAL.ViewModel {
    public class ProductViewModel {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public List<ProductBasketViewModel> ProductsBasket { get; set; }

        public List<ComponentProductViewModel> ComponentsProduct { get; set; }
    }
}