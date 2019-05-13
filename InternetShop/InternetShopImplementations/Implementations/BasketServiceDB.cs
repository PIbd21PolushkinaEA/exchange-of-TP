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
        private AbstractDbContext context;

        public BasketServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ProductBasketViewModel> GetList(int index)
        {
            //List<ProductViewModel> products = new List<ProductViewModel>();
            List<ProductBasketViewModel> result = new List<ProductBasketViewModel>();
            //for (int i = 0; i < source.Baskets[index].ProductsBasket.Count; i++)
            {
               // if (source.Baskets[index].ProductsBasket[i].ProductID == products.v)
                //result.Add(new ProductBasketViewModel
                //{
                //    Id = source.Baskets[index].ProductsBasket[i].Id,
                //    BasketID = source.Baskets[index].ProductsBasket[i].BasketID,
                //    ProductID = source.Baskets[index].ProductsBasket[i].ProductID,
                //    Count = source.Baskets[index].ProductsBasket[i].Count
                //});
            }
            return result;
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
