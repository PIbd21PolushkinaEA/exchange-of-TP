using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IBasketService
    {
        BasketViewModel GetElement(int id);

        void AddElement(BasketBindingModel model);

        void UpdElement(BasketBindingModel model);

        void DelElement(int id);

    }
}
