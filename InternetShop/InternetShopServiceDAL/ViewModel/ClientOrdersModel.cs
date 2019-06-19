namespace InternetShopServiceDAL.ViewModel {
    public class ClientsOrderModel {
        public string ClientName { get; set; }
        public string DateCreate { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}