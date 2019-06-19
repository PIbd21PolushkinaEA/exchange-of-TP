using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace InternetShopModel
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string ProductName { get; set; }

        [DataMember]
        [Required]
        public int Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ProductBasket> ProductsBasket { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ComponentProduct> ComponentsProduct { get; set; }

    }
}
