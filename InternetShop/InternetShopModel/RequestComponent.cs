namespace InternetShopModel {
    public class RequestComponent {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int RequestId { get; set; }
        public string ComponentName { get; set; }
        public int CountComponents { get; set; }
        public virtual Request Request { get; set; }
        public virtual Component Component { get; set; }
    }
}