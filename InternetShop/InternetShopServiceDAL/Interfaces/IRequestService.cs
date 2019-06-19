using System.Collections.Generic;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces {
    public interface IRequestService {
        List<RequestViewModel> GetList();

        RequestViewModel GetElement(int id);

        void AddElement(RequestBindingModel model);

        void UpdElement(RequestBindingModel model);

        void DelElement(int id);
    }
}