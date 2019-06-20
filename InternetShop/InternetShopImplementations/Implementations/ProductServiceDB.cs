using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations {
    public class ProductServiceDB : IProductService {
        private AbstractWebDbContext context;

        public ProductServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public List<ProductViewModel> GetList() {
            List<ProductViewModel> result = context.Products.Select(rec =>
                new ProductViewModel {
                    Id = rec.Id,
                    ProductName = rec.ProductName,
                    Price = rec.Price,
                    ProductsBasket = context.ProductsBasket
                        .Where(recPC => recPC.ProductId == rec.Id)
                        .Select(recPC => new ProductBasketViewModel {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            BasketId = recPC.BasketId,
                            Count = recPC.Count
                        }).ToList(),

                    ComponentsProduct = context.ComponentsProduct
                        .Where(recPC => recPC.ProductId == rec.Id)
                        .Select(recPC => new ComponentProductViewModel {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            ComponentId = recPC.ComponentId,
                            ComponentName = recPC.ComponentName,
                            Manuf = recPC.Manuf,
                            Brand = recPC.Brand,
                            ComponentRating = recPC.ComponentRating,
                            Count = recPC.Count
                        }).ToList()
                }).ToList();
            return result;
        }

        public List<ProductViewModel> GetAvailableList() {
            List<ProductViewModel> result = context.Products.Select(rec => new ProductViewModel {
                Id = rec.Id,
                ProductName = rec.ProductName,
                Price = rec.Price,
                ComponentsProduct = context.ComponentsProduct
                    .Where(recPM => recPM.ProductId == rec.Id)
                    .Select(recPM => new ComponentProductViewModel {
                        Id = recPM.Id,
                        ComponentId = recPM.ComponentId,
                        ProductId = recPM.ProductId,
                        ComponentName = recPM.ComponentName,
                        Manuf = recPM.Manuf,
                        Brand = recPM.Brand,
                        ComponentRating = recPM.ComponentRating,
                        Count = recPM.Count
                    }).ToList()
            }).ToList();

            List<ProductViewModel> neww = new List<ProductViewModel>();
            foreach ( var pr in result ) {
                bool add = false;
                foreach ( var m in pr.ComponentsProduct ) {
                    if ( context.Components != null && m.Count <= context.Components
                             .FirstOrDefault(rec => rec.Id == m.ComponentId)
                             .CountOfAvailable ) {
                        add = true;
                    }
                }

                if ( add ) {
                    neww.Add(pr);
                }
            }

            return neww;
        }

        public ProductViewModel GetElement(int id) {
            Product element = context.Products.FirstOrDefault(rec => rec.Id == id);
            if ( element != null ) {
                return new ProductViewModel {
                    Id = element.Id,
                    ProductName = element.ProductName,
                    Price = element.Price,
                    ProductsBasket = context.ProductsBasket
                        .Where(recPC => recPC.ProductId == element.Id)
                        .Select(recPC => new ProductBasketViewModel {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            BasketId = recPC.BasketId,
                            Count = recPC.Count
                        }).ToList(),

                    ComponentsProduct = context.ComponentsProduct
                        .Where(recPC => recPC.ProductId == element.Id)
                        .Select(recPC => new ComponentProductViewModel {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            ComponentId = recPC.ComponentId,
                            ComponentName = recPC.ComponentName,
                            Manuf = recPC.Manuf,
                            Brand = recPC.Brand,
                            ComponentRating = recPC.ComponentRating,
                            Count = recPC.Count
                        }).ToList()
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProductBindingModel model) {
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Product element = context.Products.FirstOrDefault(rec => rec.ProductName == model.ProductName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть тур с таким названием");
                    }
                    element = new Product
                    {
                        ProductName = model.ProductName,
                        Price =  model.Price
                    };
                    context.Products.Add(element);
                    context.SaveChanges();

                    var groupComponents = model.ComponentsProduct
                        .GroupBy(rec => rec.ComponentId)
                        .Select(rec => new
                        {
                            ComponentId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });

                    foreach (var groupComponent in groupComponents) {
                        Component elem = context.Components.SingleOrDefault(component =>
                            component.Id == groupComponent.ComponentId);
                        context.ComponentsProduct.Add(new ComponentProduct
                        {
                            ProductId = element.Id,
                            ComponentId = groupComponent.ComponentId,
                            Manuf = elem?.Manufacturer,
                            Brand = elem?.Brand,
                            ComponentName = elem?.Name,
                            ComponentRating = elem.Rating,
                            Count = groupComponent.Count
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

        public void UpdElement(ProductBindingModel model) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Product element = context.Products.FirstOrDefault(rec =>
                        rec.ProductName == model.ProductName && rec.Id != model.Id);
                    if ( element != null ) {
                        throw new Exception("Уже есть товар с таким названием");
                    }

                    element = context.Products.FirstOrDefault(rec => rec.Id == model.Id);
                    if ( element == null ) {
                        throw new Exception("Элемент не найден");
                    }

                    element.ProductName = model.ProductName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты  
                    var compIds = model.ComponentsProduct.Select(rec => rec.ComponentId).Distinct();
                    var updateComponents = context.ComponentsProduct.Where(rec => rec.ProductId == model.Id &&
                                                                                  compIds.Contains(rec.ComponentId));
                    foreach ( var updateComponent in updateComponents ) {
                        updateComponent.Count = model.ComponentsProduct
                            .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }

                    context.SaveChanges();
                    context.ComponentsProduct.RemoveRange(context.ComponentsProduct.Where(rec =>
                        rec.ProductId == model.Id &&
                        !compIds.Contains(rec.ComponentId)));
                    context.SaveChanges();
                    // новые записи                 
                    var groupComponents = model.ComponentsProduct
                        .Where(rec => rec.Id == 0)
                        .GroupBy(rec => rec.ComponentId)
                        .Select(rec => new {
                            ComponentId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    foreach ( var groupComponent in groupComponents ) {
                        ComponentProduct elementPC = context.ComponentsProduct
                            .FirstOrDefault(rec =>
                                rec.ProductId == model.Id && rec.ComponentId == groupComponent.ComponentId);
                        if ( elementPC != null ) {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else {
                            context.ComponentsProduct.Add(new ComponentProduct {
                                ProductId = model.Id,
                                ComponentId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }

                    /*List<ComponentProduct> list = new List<ComponentProduct>();
                    for ( int i = 0; i < model.ComponentsProduct.Count; i++ ) {
                        if ( model.ComponentsProduct[i].ProductId != 0 ) {
                            continue;
                        }
                        ComponentProductBindingModel elem = model.ComponentsProduct[i];
                        list.Add(new ComponentProduct {
                            Id = elem.Id,
                            ProductId = element.Id,
                            ComponentId = elem.ComponentId,
                            ComponentName = elem.ComponentName,
                            Manuf = elem.Manuf,
                            Brand = elem.Brand,
                            ComponentRating = elem.ComponentRating,
                            Count = elem.Count
                        });
                    }

                    element.ComponentsProduct = list;
                    context.Products.Add(element);
                    context.SaveChanges();*/
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
                    Product element = context.Products.FirstOrDefault(rec => rec.Id == id);
                    if ( element != null ) {
                        // удаяем записи по компонентам при удалении изделия       
                        context.ComponentsProduct.RemoveRange(
                        context.ComponentsProduct.Where(rec => rec.ProductId == id));
                        context.Products.Remove(element);
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
    }
}