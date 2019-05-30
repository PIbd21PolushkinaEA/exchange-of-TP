using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.ViewModel
{
    public class ProductBasketViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }
    }
}
