using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopImplementations.Implementations
{
    public class BasketServiceDB : IBasketService
    {
        private AbstractWebDbContext context;

        public BasketServiceDB(AbstractWebDbContext context)
        {
            this.context = context;
        }

        public List<ProductBasketViewModel> GetList(int index)
        {
            throw new NotImplementedException();
        }
        public BasketViewModel GetElement(int id)
        {
            throw new NotImplementedException();
        }
        public void DelElement(int id)
        {
            throw new NotImplementedException();
        }
        public List<BasketViewModel> GetBaskets()
        {
            throw new NotImplementedException();
        }




        public void UpdElement(BasketBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
