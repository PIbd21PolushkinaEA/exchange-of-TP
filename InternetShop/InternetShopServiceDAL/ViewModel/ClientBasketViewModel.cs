using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.ViewModel
{
    public class ClientBasketViewModel
    {
        public string Name { get; set; }

        public string Date { get; set; }

        public bool isReserved { get; set; }

        public List<ProductViewModel> productList { get; set; }

    }
}
