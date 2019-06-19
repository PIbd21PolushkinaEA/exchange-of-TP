using System;
using System.Collections.Generic;

namespace InternetShopServiceDAL.ViewModel {
    public class RequestViewModel {
        public int Id { get; set; }
        public string RequestName { get; set; }
        public DateTime Date { get; set; }
        public List<RequestComponentViewModel> RequestComponents { get; set; }
    }
}