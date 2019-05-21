using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopModel
{
    public class Basket
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public string NameBuy { get; set; }

        [Required]
        public int CountOfChoosedProducts { get; set; }

        [Required]
        public int SumOfChoosedProducts { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("BasketId")]
        public virtual List<ProductBasket> ProductsBasket { get; set; }

    }
}
