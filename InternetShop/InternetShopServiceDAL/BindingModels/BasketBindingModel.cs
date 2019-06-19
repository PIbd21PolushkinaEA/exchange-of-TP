using System.Collections.Generic;

namespace InternetShopServiceDAL.BindingModels {
    public class BasketBindingModel {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string NameBuy { get; set; }

        public int CountOfChoosedProducts { get; set; }

        public int SumOfChoosedProducts { get; set; }

        public bool IsReserved { get; set; }

        public List<ProductBasketBindingModel> ProductsBasket { get; set; }
    }
}