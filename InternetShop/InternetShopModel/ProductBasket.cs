namespace InternetShopModel {
    public class ProductBasket {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Basket Basket { get; set; }
    }
}