using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class ProductBasket
    {
        private int ProductID { get; set; }
        private int BasketID { get; set; }
        private int Count { get; set; }
        private bool isReserved;
    }
}
 