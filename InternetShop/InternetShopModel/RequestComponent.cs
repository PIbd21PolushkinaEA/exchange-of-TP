using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopModel
{
   public class RequestComponent
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int RequestId { get; set; }
        public int Count { get; set; }
        public virtual Request Request { get; set; }
        public virtual Component Component { get; set; }
    }
}
