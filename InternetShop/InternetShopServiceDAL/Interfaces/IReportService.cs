using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces
{
    public interface IReportService
    {
        List<ClientBasketViewModel> GetClientBaskets(ReportBindingModel model, int ClientId);

        void SaveClientBaskets(ReportBindingModel model, int Clientd);

        void SaveClientAllBaskets(ReportBindingModel model, int ClientId);
    }
}
