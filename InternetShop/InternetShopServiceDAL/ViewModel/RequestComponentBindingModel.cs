using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.ViewModel {
    public class RequestComponentBindingModel {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int RequestId { get; set; }
        public int CountComponents { get; set; }
    }
}
