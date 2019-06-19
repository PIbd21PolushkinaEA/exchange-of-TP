using System.Collections.Generic;

namespace InternetShopServiceDAL.BindingModels {
    public class ProductBindingModel {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public List<ProductBasketBindingModel> ProductsBasket { get; set; }

        public List<ComponentProductBindingModel> ComponentsProduct { get; set; }
    }
}