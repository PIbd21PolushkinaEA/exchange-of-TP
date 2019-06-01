using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.ViewModel {
    public class RequestViewModel {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<RequestComponentBindingModel> RequestComponents { get; set; }
    }
}
