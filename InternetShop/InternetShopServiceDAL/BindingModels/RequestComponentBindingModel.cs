namespace InternetShopServiceDAL.BindingModels {
    public class RequestComponentBindingModel {
        public int Id { get; set; }
        public int ComponentID { get; set; }
        public int RequestID { get; set; }
        public int CountComponents { get; set; }

        public string ComponentName { get; set; }
    }
}