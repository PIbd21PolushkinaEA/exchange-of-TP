using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopModel;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.ViewModel;
using InternetShopServiceDAL;

namespace InternetShopImplementations.Implementations
{
    public class ClientServiceDB : IClientService
    {
        private AbstractDbContext context;

        public ClientServiceDB(AbstractDbContext context) { this.context = context; }

        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = context.Clients.Select(rec => new ClientViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Email = rec.Email,
                Password = rec.Password,
                BasketID = rec.BasketId
            })
            .ToList();
            return result;
        }

        public ClientViewModel GetElement(string email, string password)
        {
            Client element = context.Clients.FirstOrDefault((rec => (rec.Email == email) && (rec.Password == password)));
            if (element != null)
            {
                return new ClientViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    BasketID = element.BasketId
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Email == model.Email);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new Client
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                BasketId = model.BasketID
            });
            context.SaveChanges();
        }

        public void UpdElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Email == model.Email && rec.Password == model.Password);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
            element.Email = model.Email;
            element.Password = model.Password;
            element.BasketId = model.BasketID;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
