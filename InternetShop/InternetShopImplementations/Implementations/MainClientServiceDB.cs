using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace InternetShopImplementations.Implementations
{
    public class MainClientServiceDB : IMainClientServise
    {
        private AbstractDbContext context;

        public MainClientServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public MainClientServiceDB()
        {
            this.context = new AbstractDbContext();
        }

        public List<BasketViewModel> GetList()
        {
            List<BasketViewModel> result = context.Baskets.Select(rec =>
            new BasketViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                NameBuy = rec.NameBuy,
                CountOfChoosedProducts = rec.CountOfChoosedProducts,
                SumOfChoosedProducts = rec.SumOfChoosedProducts,
                IsReserved = rec.IsReserved,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                SqlFunctions.DateName("yyyy", rec.DateCreate),
                ProductsBasket = context.ProductsBasket
                    .Where(recPC => recPC.BasketId == rec.Id)
                    .Select(recPC => new ProductBasketViewModel
                    {
                        Id = recPC.Id,
                        ProductId = recPC.ProductId,
                        BasketId = recPC.BasketId,
                        ProductName = recPC.Product.ProductName,
                        Count = recPC.Count
                    }).ToList()
            }).ToList();
            return result;
        }

        public List<BasketViewModel> GetListBuy(int ClientId)
        {
            List<BasketViewModel> result = context.Baskets.
                Where(rec => rec.ClientId == ClientId).
                Select(rec => new BasketViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    NameBuy = rec.NameBuy,
                    SumOfChoosedProducts = rec.SumOfChoosedProducts,
                    IsReserved = rec.IsReserved,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                    SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                    SqlFunctions.DateName("yyyy", rec.DateCreate),
                    ProductsBasket = context.ProductsBasket
                    .Where(recPC => recPC.BasketId == rec.Id)
                    .Select(recPC => new ProductBasketViewModel
                    {
                        Id = recPC.Id,
                        ProductId = recPC.ProductId,
                        BasketId = recPC.BasketId,
                        ProductName = recPC.Product.ProductName,
                        Count = recPC.Count,
                    }).ToList()
                }).ToList();
            return result;
        }
        public BasketViewModel GetElement(int id)
        {
            Basket element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new BasketViewModel
                {
                    Id = element.Id,
                    ClientId = element.ClientId,
                    NameBuy = element.NameBuy,
                    CountOfChoosedProducts = element.CountOfChoosedProducts,
                    SumOfChoosedProducts = element.SumOfChoosedProducts,
                    IsReserved = element.IsReserved,
                    ProductsBasket = context.ProductsBasket
                    .Where(recPC => recPC.BasketId == element.Id)
                    .Select(recPC => new ProductBasketViewModel
                    {
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

        public void AddBuy(BasketBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Basket element = context.Baskets.FirstOrDefault(rec =>
                     rec.NameBuy == model.NameBuy);
                    if (element != null)
                    {
                        throw new Exception("Уже есть покупка с таким названием");
                    }
                    element = new Basket
                    {
                        ClientId = model.ClientId,
                        NameBuy = model.NameBuy,
                        SumOfChoosedProducts = model.SumOfChoosedProducts,
                        DateCreate = DateTime.Now
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
                    // запоминаем id и названия продуктов
                    var productName = model.ProductsBasket.Select(rec => new
                    {
                        ProductId = rec.ProductId,
                        ProductName = rec.ProductName
                    });
                    // добавляем компоненты  
                    foreach (var groupProduct in groupProducts)
                    {
                        string Name = null;
                        foreach (var product in productName)
                        {
                            if (groupProduct.ProductId == product.ProductId)
                            {
                                Name = product.ProductName;
                            }
                        }
                        context.ProductsBasket.Add(new ProductBasket
                        {
                            BasketId = element.Id,
                            ProductId = groupProduct.ProductId,
                            ProductName=Name,
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

        public void UpdBuy(BasketBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Basket element = context.Baskets.FirstOrDefault(rec =>
                    rec.NameBuy == model.NameBuy && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть заказ с таким названием");
                    }
                    element = context.Baskets.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.NameBuy = model.NameBuy;
                    element.SumOfChoosedProducts = model.SumOfChoosedProducts;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты 
                    var compIds = model.ProductsBasket.Select(rec => rec.ProductId).Distinct();
                    var updateProducts = context.ProductsBasket.Where(rec =>
                    rec.BasketId == model.Id && compIds.Contains(rec.ProductId));
                    foreach (var updatePart in updateProducts)
                    {
                        updatePart.Count = model.ProductsBasket.FirstOrDefault(rec =>
                        rec.Id == updatePart.Id).Count;
                    }
                    context.SaveChanges();
                    context.ProductsBasket.RemoveRange(context.ProductsBasket.Where(rec =>
                    rec.BasketId == model.Id && !compIds.Contains(rec.ProductId)));
                    context.SaveChanges();
                    // новые записи  
                    var groupProducts = model.ProductsBasket.Where(rec =>
                    rec.Id == 0).GroupBy(rec => rec.ProductId).Select(rec => new
                    {
                        ProductId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    foreach (var groupProduct in groupProducts)
                    {
                        ProductBasket elementPC = context.ProductsBasket.FirstOrDefault(rec =>
                        rec.BasketId == model.Id && rec.ProductId == groupProduct.ProductId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupProduct.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ProductsBasket.Add(new ProductBasket
                            {
                                BasketId = model.Id,
                                ProductId = groupProduct.ProductId,
                                Count = groupProduct.Count
                            });
                            context.SaveChanges();
                        }
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
        public void DelBuy(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Basket element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {                         // удаяем записи по компонентам при удалении изделия  
                        context.ProductsBasket.RemoveRange(context.ProductsBasket.Where(rec =>
                        rec.BasketId == id));
                        context.Baskets.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
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

        public void MakeReservation(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Basket element = context.Baskets.FirstOrDefault(rec => rec.Id == id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.IsReserved)
                    {
                        throw new Exception("Комплектующие по этой покупке уже зарезервированы");
                    }
                    else
                    {
                        element.IsReserved = true;
                    }
                    List<ComponentProductViewModel> componentproduct = new List<ComponentProductViewModel>();
                    var productBaslet = context.ProductsBasket
                        .Where(rec => rec.BasketId == element.Id)
                        .Select(rec => new ProductBasketViewModel
                        {
                            ProductId = rec.ProductId,
                            Count = rec.Count
                        });
                    foreach (var product in productBaslet)
                    {
                        var componentProduct = context.ComponentsProduct
                            .Where(rec => rec.ProductId == product.ProductId)
                            .Select(rec => new ComponentProductViewModel
                            {
                                ComponentId = rec.ComponentId,
                                Count = rec.Count
                            });
                        foreach (var compon in componentProduct)
                        {
                            bool flag = false;
                            for (int i = 0; i < componentproduct.Count(); i++)
                            {
                                if (componentproduct[i].ComponentId == compon.ComponentId)
                                {
                                    componentproduct[i].Count += compon.Count;
                                    flag = true;
                                }
                            }
                            if (!flag)
                            {
                                componentproduct.Add(compon);
                                componentproduct.Last().Count = compon.Count * product.Count;
                            }
                        }
                    }
                    var Compoment = context.Components.Select(rec => new ComponentViewModel
                    {
                        Id = rec.Id,
                        CountOfAvailable = rec.CountOfAvailable
                    }).ToList();

                    for (int i = 0; i < componentproduct.Count(); i++)
                    {
                        var index = componentproduct[i].ComponentId;
                        var Com = context.Components.Where(rec => rec.Id == index);
                        foreach (var com in Com)
                        {
                            if (componentproduct[i].Count <= com.CountOfAvailable)
                            {
                                com.CountOfAvailable -= componentproduct[i].Count;
                                context.SaveChanges();
                            }
                            else
                            {
                                throw new Exception("Не достаточно комплектующих,попробуйте позже");
                            }
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void SendEmail(string mailAddress, string subject, string text, string path)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new MailAddress("labwork15kafis@gmail.com");
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objMailMessage.Attachments.Add(new Attachment(path));

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
                NetworkCredential("labwork15kafis@gmail.com", "passlab15");

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
