using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IRequestService
    {
        List<RequestViewModel> GetList();

        RequestViewModel GetElement(int id);

        void AddElement(RequestBindingModel model);

        void UpdElement(RequestBindingModel model);

        void DelElement(int id);
    }
}
