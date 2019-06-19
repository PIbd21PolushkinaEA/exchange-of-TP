using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace InternetShopModel
{
    [DataContract]
    public class ProductBasket
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int BasketId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
 