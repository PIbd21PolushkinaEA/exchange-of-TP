using System.Collections.Generic;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces {
    public interface IMainClientServise {
        //покупки
        List<BasketViewModel> GetList();

        BasketViewModel GetElement(int id);

        void AddElement(BasketBindingModel model);

        void UpdElement(BasketBindingModel model);

        void DelElement(int id);

        void MakeReservation(ProductBasketBindingModel model);
    }
}