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
        private AbstractWebDbContext context;

        public MainClientServiceDB(AbstractWebDbContext context)
        {
            this.context = context;
        }

        public MainClientServiceDB()
        {
            this.context = new AbstractWebDbContext();
        }

        public List<BasketViewModel> GetList()
        {
            List<BasketViewModel> result = context.Baskets.Select(rec =>
            new BasketViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                CountOfChoosedProducts = rec.CountOfChoosedProducts,
                SumOfChoosedProducts = rec.SumOfChoosedProducts,
                ProductsBasket = context.ProductsBasket
                    .Where(recPC => recPC.BasketId == rec.Id)
                    .Select(recPC => new ProductBasketViewModel
                    {
                        Id = recPC.Id,
                        ProductId = recPC.ProductId,
                        BasketId = recPC.BasketId,
                        Count = recPC.Count
                    })
                        .ToList()

            })
            .ToList();
            return result;
        }

        //public ProductViewModel GetElement(int id)
        //{
        //    for (int i = 0; i < source.Products.Count; ++i)
        //    {
        //        if (source.Products[i].Id == id)
        //        {
        //            return new ProductViewModel
        //            {
        //                Id = source.Products[i].Id,
        //                ProductName = source.Products[i].ProductName,
        //                Price = source.Products[i].Price
        //            };
        //        }
        //    }
        //    throw new Exception("Элемент не найден");
        //}

        public void AddToBasket(BasketBindingModel model)
        {
            //context.Baskets.Add(new Basket
            //{
            //    ClientID = model.ClientID,
            //    CountOfChoosedProducts = model.CountOfChoosedProducts,
            //    SumOfChoosedProducts = model.SumOfChoosedProducts
            //});
            //context.SaveChanges();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Basket element;
                    //= context.Baskets.FirstOrDefault(rec =>
                    //rec.Id == model.Id);
                    //if (element != null)
                    //{
                    //    throw new Exception("Уже есть изделие с таким названием");
                    // }
                    element = new Basket
                    {
                        ClientId = model.ClientId,
                        CountOfChoosedProducts = model.CountOfChoosedProducts,
                        SumOfChoosedProducts = model.SumOfChoosedProducts
                    };
                    context.Baskets.Add(element);
                    context.SaveChanges();
                    //// убираем дубли по компонентам 
                    var groupProducts = model.ProductsBasket
                        .GroupBy(rec => rec.ProductId)
                        .Select(rec => new
                        {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    // добавляем компоненты  
                    foreach (var groupProduct in groupProducts)
                    {
                        context.ProductsBasket.Add(new ProductBasket
                        {
                            BasketId = element.Id,
                            ProductId = groupProduct.ProductId,
                            Count = groupProduct.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;

                }
            }
        }

        public void MakeReservation(ProductBasketBindingModel model)
        {
            ProductBasket element = context.ProductsBasket.FirstOrDefault(rec =>
            rec.ProductId == model.ProductId && rec.BasketId == model.BasketId);
            if (element != null)
            {
                element.IsReserved = model.IsReserved;
            }
            else
            {
                context.ProductsBasket.Add(new ProductBasket
                {
                    ProductId = model.ProductId,
                    BasketId = model.BasketId,
                    Count = model.Count,
                    IsReserved = model.IsReserved
                });
            }
            context.SaveChanges();
        }
    }
}
