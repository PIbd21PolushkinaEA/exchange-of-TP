using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IMainClient
    {
        //покупки
        List<ProductViewModel> GetList();

        void AddToBasket(ProductBindingModel model);

        void UpdToBasket(ProductBindingModel model);

        void DelFromBasket(ProductBindingModel model);

    }
}
