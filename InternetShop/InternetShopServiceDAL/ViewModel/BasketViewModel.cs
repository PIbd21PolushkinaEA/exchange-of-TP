﻿using System.Collections.Generic;

namespace InternetShopServiceDAL.ViewModel {
    public class BasketViewModel {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string NameBuy { get; set; }

        public int CountOfChoosedProducts { get; set; }

        public int SumOfChoosedProducts { get; set; }

        public string ProductName { get; set; }

        public List<ProductBasketViewModel> ProductsBasket { get; set; }
    }
}