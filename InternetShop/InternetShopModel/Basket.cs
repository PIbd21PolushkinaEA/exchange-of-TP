using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class Basket
    {
        public int Id { get; set; }
        public int CountOfChoosedProducts { get; set; }
        public int SumOfChoosedProducts { get; set; }
        public int ClientID { get; set; }
    }
}
