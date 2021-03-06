﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.BindingModels
{
    public class BasketBindingModel
    {
        public int Id { get; set; }
        public int CountOfChoosedProducts { get; set; }
        public int SumOfChoosedProducts { get; set; }
        public int ClientID { get; set; }
        public List<ProductBasketBindingModel> ProductsBasket { get; set; }
    }
}
