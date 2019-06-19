namespace InternetShopServiceDAL.BindingModels {
    public class ComponentProductBindingModel {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public string ComponentName { get; set; }
        public int Count { get; set; }
    }
}