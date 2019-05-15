using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class ProductBasket
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public int Count { get; set; }

        public bool IsReserved { get; set; }

        public virtual Product Product { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
 