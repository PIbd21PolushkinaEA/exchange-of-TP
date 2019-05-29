using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.BindingModels {
    public class RequestComponentBindingModel {
        public int Id { get; set; }
        public int ComponentID { get; set; }
        public int RequestID { get; set; }
        public int CountCompoments { get; set; }
    }
}
