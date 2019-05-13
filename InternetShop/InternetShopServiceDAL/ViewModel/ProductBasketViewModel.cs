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
        public int ProductID { get; set; }
        public int BasketID { get; set; }
        public int Count { get; set; }
        public bool IsReserved { get; set; }
    }
}
