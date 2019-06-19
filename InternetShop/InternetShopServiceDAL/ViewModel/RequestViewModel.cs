using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InternetShopServiceDAL.ViewModel {
    public class RequestViewModel {
        public int Id { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}