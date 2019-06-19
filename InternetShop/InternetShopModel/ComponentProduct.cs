namespace InternetShopModel {
    public class ComponentProduct {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public string ComponentName { get; set; }
        public string Brand { get; set; }
        public string Manuf { get; set; }
        public float ComponentRating { get; set; }
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Product Product { get; set; }
    }
}