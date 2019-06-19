using System;
using System.Collections.Generic;
using System.Linq;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations {
    public class MainClientServiceDB : IMainClientServise {
        private AbstractWebDbContext context;

        public MainClientServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public MainClientServiceDB() {
            this.context = new AbstractWebDbContext();
        }

        public List<BasketViewModel> GetList() {
            List<BasketViewModel> result = context.Baskets.Select(rec =>
                new BasketViewModel {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    NameBuy = rec.NameBuy,
                    CountOfChoosedProducts =
                        rec.CountOfChoosedProducts,
                    SumOfChoosedProducts =
                        rec.SumOfChoosedProducts,
                    ProductsBasket = context.ProductsBasket
                        .Where(recPC => recPC.BasketId == rec.Id)
                        .Select(recPC =>
                            new ProductBasketViewModel {
                                Id = recPC.Id,
                                ProductId = recPC
                                    .ProductId,
                                BasketId = recPC.BasketId,
                                ProductName =
                                    recPC.Product
                                        .ProductName,
                                Count = recPC.Count,
                                IsReserved =
                                    recPC.IsReserved
                            }).ToList()
                }).ToList();
            return result;
        }

        public BasketViewModel GetElement(int id) {
            Basket element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
            if ( element != null ) {
                return new BasketViewModel {
                    Id = element.Id,
                    ClientId = element.ClientId,
                    NameBuy = element.NameBuy,
                    CountOfChoosedProducts = element.CountOfChoosedProducts,
                    SumOfChoosedProducts = element.SumOfChoosedProducts,
                    ProductsBasket = context.ProductsBasket
                        .Where(recPC => recPC.BasketId == element.Id)
                        .Select(recPC => new ProductBasketViewModel {
                            Id = recPC.Id,
                            BasketId = recPC.BasketId,
                            ProductId = recPC.ProductId,
                            ProductName = recPC.Product.ProductName,
                            Count = recPC.Count
                        })
                        .ToList()
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(BasketBindingModel model) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Basket element = context.Baskets.FirstOrDefault(rec =>
                        rec.NameBuy == model.NameBuy);
                    if ( element != null ) {
                        throw new Exception("Уже есть покупка с таким названием");
                    }

                    element = new Basket {
                        ClientId = model.ClientId,
                        NameBuy = model.NameBuy,
                        CountOfChoosedProducts = model.CountOfChoosedProducts,
                        SumOfChoosedProducts = model.SumOfChoosedProducts
                    };
                    context.Baskets.Add(element);
                    context.SaveChanges();
                    //// убираем дубли по компонентам 
                    var groupProducts = model.ProductsBasket
                        .GroupBy(rec => rec.ProductId)
                        .Select(rec => new {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    //// подумой!
                    // добавляем компоненты  
                    foreach ( var groupProduct in groupProducts ) {
                        context.ProductsBasket.Add(new ProductBasket {
                            BasketId = element.Id,
                            ProductId = groupProduct.ProductId,
                            Count = groupProduct.Count
                        });
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(BasketBindingModel model) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Basket element = context.Baskets.FirstOrDefault(rec =>
                        rec.NameBuy == model.NameBuy &&
                        rec.Id != model.Id);
                    if ( element != null ) {
                        throw new Exception("Уже есть заказ с таким названием");
                    }

                    element = context.Baskets.FirstOrDefault(rec => rec.Id == model.Id);
                    if ( element == null ) {
                        throw new Exception("Элемент не найден");
                    }

                    element.NameBuy = model.NameBuy;
                    element.SumOfChoosedProducts = model.SumOfChoosedProducts;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты 
                    var compIds = model.ProductsBasket.Select(rec => rec.ProductId).Distinct();
                    var updateProducts = context.ProductsBasket.Where(rec =>
                        rec.BasketId == model.Id &&
                        compIds.Contains(rec.ProductId));
                    foreach ( var updatePart in updateProducts ) {
                        updatePart.Count = model.ProductsBasket.FirstOrDefault(rec =>
                            rec.Id == updatePart.Id).Count;
                    }

                    context.SaveChanges();
                    context.ProductsBasket.RemoveRange(context.ProductsBasket.Where(rec =>
                        rec.BasketId == model.Id &&
                        !compIds
                            .Contains(rec.ProductId)));
                    context.SaveChanges();
                    // новые записи  
                    var groupProducts = model.ProductsBasket.Where(rec =>
                            rec.Id == 0).GroupBy(rec => rec.ProductId)
                        .Select(rec => new {
                            ProductId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    foreach ( var groupProduct in groupProducts ) {
                        ProductBasket elementPC = context.ProductsBasket.FirstOrDefault(rec =>
                            rec.BasketId == model.Id &&
                            rec.ProductId ==
                            groupProduct.ProductId);
                        if ( elementPC != null ) {
                            elementPC.Count += groupProduct.Count;
                            context.SaveChanges();
                        }
                        else {
                            context.ProductsBasket.Add(new ProductBasket {
                                BasketId = model.Id,
                                ProductId = groupProduct.ProductId,
                                Count = groupProduct.Count
                            });
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Basket element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
                    if ( element != null ) {
                        // удаяем записи по компонентам при удалении изделия  
                        context.ProductsBasket.RemoveRange(context.ProductsBasket.Where(rec =>
                            rec.BasketId == id));
                        context.Baskets.Remove(element);
                        context.SaveChanges();
                    }
                    else {
                        throw new Exception("Элемент не найден");
                    }

                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void MakeReservation(ProductBasketBindingModel model) {
            ProductBasket element = context.ProductsBasket.FirstOrDefault(rec =>
                rec.ProductId == model.ProductId &&
                rec.BasketId == model.BasketId);
            if ( element != null ) {
                element.IsReserved = model.IsReserved;
            }
            else {
                context.ProductsBasket.Add(new ProductBasket {
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