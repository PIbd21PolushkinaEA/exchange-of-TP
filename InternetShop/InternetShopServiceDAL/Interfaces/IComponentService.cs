using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.Interfaces {
    public interface IComponentService {
        List<ComponentViewModel> GetList();
        ComponentViewModel GetElement(int id);
        void AddComponent(ComponentBindingModel model);
        void UpdComponent(ComponentBindingModel model);
        void DelComponent(int id);
    }
}
