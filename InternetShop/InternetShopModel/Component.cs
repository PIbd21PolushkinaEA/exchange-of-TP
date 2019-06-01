using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopModel
{
    public class Component
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CountOfAvailable { get; set; }
        [ForeignKey("ComponentID")]
        public virtual List<ComponentProduct> ComponentProducts { get; set; }
        [ForeignKey("ComponentID")]
        public virtual List<RequestComponent> ComponentRequests { get; set; }
    }
}
