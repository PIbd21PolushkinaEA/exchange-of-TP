using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopModel
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<RequestComponent> RequestComponents { get; set; }
    }
}
