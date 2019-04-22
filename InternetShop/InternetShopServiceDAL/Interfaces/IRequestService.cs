using InternetShopServiceDAL.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.Interfaces {
    public interface IRequestService {
        RequestBindingModel GetRequest(int id);
        void CrtRequest(RequestBindingModel model);
        void UpdRequest(RequestBindingModel model);
        void DelRequest(int id);
    }
}
