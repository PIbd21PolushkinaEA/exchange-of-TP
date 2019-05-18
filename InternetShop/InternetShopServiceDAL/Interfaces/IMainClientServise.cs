using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IMainClientServise
    {
        //покупки
        List<BasketViewModel> GetList();

        //ProductViewModel GetElement(int id);

        void AddToBasket(BasketBindingModel model);

        void MakeReservation(ProductBasketBindingModel model);

    }
}
