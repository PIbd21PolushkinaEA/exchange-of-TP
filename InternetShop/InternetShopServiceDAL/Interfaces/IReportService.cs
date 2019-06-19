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
        //List<ClientBasketViewModel> GetClientBaskets(ReportBindingModel model, int ClientId);

        List<ReportViewModel> GetBaskets(ReportBindingModel model, int PatientId);

        List<ReportViewModel> GetRequests(ReportBindingModel model);

        void SaveLoad(ReportBindingModel model, int Clientd);

        void SaveBaskets(ReportBindingModel model, int ClientId);
    }
}
