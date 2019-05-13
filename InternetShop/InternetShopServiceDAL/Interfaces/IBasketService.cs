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
        List<BasketViewModel> GetBaskets();

        BasketViewModel GetElement(int id);

        List<ProductBasketViewModel> GetList(int index);

        void UpdElement(BasketBindingModel model);

        void DelElement(int id);

    }
}
