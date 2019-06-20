using System.ComponentModel;

namespace InternetShopServiceDAL.ViewModel {
    public class ComponentViewModel {
        public int Id { get; set; }

        [DisplayName("Название ")]
        public string Name { get; set; }

        [DisplayName("Изготовитель")]
        public string Manufacturer { get; set; }

        [DisplayName("Брэнд")]
        public string Brand { get; set; }

        public float Rating { get; set; }
        public int Price { get; set; }
        [DisplayName("Количество доступных")]
        public int CountOfAvailable { get; set; }
    }
}