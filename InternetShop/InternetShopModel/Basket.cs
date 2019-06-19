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
    public class Basket
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [Required]
        public string NameBuy { get; set; }

        [DataMember]
        [Required]
        public int SumOfChoosedProducts { get; set; }

        [DataMember]
        public bool IsReserved { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("BasketId")]
        public virtual List<ProductBasket> ProductsBasket { get; set; }

    }
}
