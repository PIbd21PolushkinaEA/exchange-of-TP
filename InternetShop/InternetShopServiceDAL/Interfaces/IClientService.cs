using System.Collections.Generic;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces {
    public interface IClientService {
        List<ClientViewModel> GetList();

        ClientViewModel GetElement(string email, string password);

        void AddElement(ClientBindingModel model);
    }
}