namespace InternetShopServiceDAL.BindingModels {
    public class ProductBasketBindingModel {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }
    }
}