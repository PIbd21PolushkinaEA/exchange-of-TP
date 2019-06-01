using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public float Rating { get; set; }
        public int Price { get; set; }
        public int countOfAvailable { get; set; }
    }
}
