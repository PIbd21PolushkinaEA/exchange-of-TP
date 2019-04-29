using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
    public class ComponentProduct
    {
        public int ComponentID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Product Product { get; set; }
    }
}
