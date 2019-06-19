using System.Collections.Generic;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopServiceDAL.Interfaces {
    public interface IReportService {
        void SaveProductPrice(ReportBindingModel model);
        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);

    }
}