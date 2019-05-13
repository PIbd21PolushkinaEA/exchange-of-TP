using InternetShopModel;
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
    public class MainClientServiceDB : IMainClientServise
    {
        private DataListSingleton source;

        public MainClientServiceDB()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ProductViewModel> GetList()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            for (int i = 0; i < source.Products.Count; ++i)
            {
                result.Add(new ProductViewModel
                {
                    Id = source.Products[i].Id,
                    ProductName = source.Products[i].ProductName,
                    Price = source.Products[i].Price
                });
            }
            return result;
        }

        public ProductViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == id)
                {
                    return new ProductViewModel
                    {
                        Id = source.Products[i].Id,
                        ProductName = source.Products[i].ProductName,
                        Price = source.Products[i].Price
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }


        public void AddToBasket(ProductBindingModel model)
        {
            for (int i = 0; i < source.Baskets.Count; i++)
            {
                if (source.Clients[i].Id == source.Baskets[i].ClientID)
                {
                    source.Baskets[i].ProductsBasket.Add(new ProductBasket());

                    if (source.Baskets[i].ProductsBasket[i] != null)
                    {
                        source.Baskets[i].ProductsBasket[i].ProductID = model.Id;
                        source.Baskets[i].ProductsBasket[i].BasketID = source.Baskets[i].Id;
                        break;
                    }
                }
            }
        }

        public void UpdProducts(ProductBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
