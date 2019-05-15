using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopModel
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ProductBasket> ProductsBasket { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ComponentProduct> ComponentsProduct { get; set; }

    }
}
