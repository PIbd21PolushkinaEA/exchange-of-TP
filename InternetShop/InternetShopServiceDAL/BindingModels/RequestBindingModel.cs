using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.BindingModels {
    public class RequestBindingModel {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<RequestComponentBindingModel> RequestComponents { get; set; }
    }
}
