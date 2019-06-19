using System.Collections.Generic;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces {
    public interface IComponentService {
        List<ComponentViewModel> GetList();
        ComponentViewModel GetElement(int id);
        void AddElement(ComponentBindingModel model);
        void UpdElement(ComponentBindingModel model);
        void DelElement(int id);
    }
}