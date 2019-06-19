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

        List<BasketViewModel> GetListBuy(int ClientId);

        BasketViewModel GetElement(int id);

        void AddBuy(BasketBindingModel model);

        void UpdBuy(BasketBindingModel model);

        void DelBuy(int id);

        DateTime MakeReservation(int id);

        void SendEmail(string mailAddress, string subject, string text, string path);
    }
}
