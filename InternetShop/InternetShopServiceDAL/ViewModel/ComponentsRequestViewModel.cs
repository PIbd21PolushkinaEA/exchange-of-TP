using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.ViewModel {
    public class ComponentsRequestViewModel {
        public int Id { get; set; }

        public int ComponentId { get; set; }

        public int RequestId { get; set; }

        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }

        [DisplayName("Название изготовителя")]
        public string Manufacterer { get; set; }

        [DisplayName("Название брэнда")]
        public string Brand { get; set; }

        [DisplayName("Количество компонентов")]
        public int Count { get; set; }
    }
}
