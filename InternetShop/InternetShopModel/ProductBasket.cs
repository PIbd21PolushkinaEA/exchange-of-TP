using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class ProductBasket
    {
        public int ProductID { get; set; }
        public int BasketID { get; set; }
        public int Count { get; set; }
        public bool IsReserved { get; set; }
    }
}
 