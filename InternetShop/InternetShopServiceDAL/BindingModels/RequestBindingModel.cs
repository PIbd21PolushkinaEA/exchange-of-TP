using System;
using System.Collections.Generic;

namespace InternetShopServiceDAL.BindingModels {
    public class RequestBindingModel {
        public int Id { get; set; }
        public string RequestName { get; set; }
        public DateTime Date { get; set; }
        public List<RequestComponentBindingModel> RequestComponents { get; set; }
    }
}