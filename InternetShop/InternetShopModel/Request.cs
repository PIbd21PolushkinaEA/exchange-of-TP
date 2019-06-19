using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopModel {
    public class Request {
        public int Id { get; set; }

        public string RequestName { get; set; }
        [Required] public DateTime Date { get; set; }

        [ForeignKey("RequestId")] public virtual List<RequestComponent> RequestComponents { get; set; }
    }
}