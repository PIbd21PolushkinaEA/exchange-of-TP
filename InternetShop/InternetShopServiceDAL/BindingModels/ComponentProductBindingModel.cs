namespace InternetShopServiceDAL.BindingModels {
    public class ComponentProductBindingModel {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public string ComponentName { get; set; }
        public string Brand { get; set; }
        public string Manuf { get; set; }
        public float ComponentRating { get; set; }
        public int Count { get; set; }
    }
}