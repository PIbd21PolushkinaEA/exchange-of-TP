using System;

namespace InternetShopServiceDAL.BindingModels {
    public class ReportBindingModel {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}