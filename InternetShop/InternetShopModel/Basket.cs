using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class Basket
    {
        private int Id { get; set; }
        private int CountOfChoosedProducts { get; set; }
        private int SumOfChoosedProducts { get; set; }
        private int ClientID { get; set; }
    }
}
