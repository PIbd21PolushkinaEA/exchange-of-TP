using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.BindingModels {
    public class ComponentProductBindingModel {
        public int ComponentID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
    }
}
