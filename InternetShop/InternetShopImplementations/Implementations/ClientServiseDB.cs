using System;
using System.Collections.Generic;
using System.Linq;
using InternetShopModel;
using InternetShopServiceDAL;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations {
    public class ClientServiceDB : IClientService {
        private readonly AbstractWebDbContext _context;

        public ClientServiceDB(AbstractWebDbContext context) {
            _context = context;
        }

        public List<ClientViewModel> GetList() {
            List<ClientViewModel> result = _context.Clients.Select(rec =>
                    new ClientViewModel {
                        Id = rec.Id,
                        Name = rec.Name,
                        Email = rec.Email,
                        Password = rec.Password
                    })
                .ToList();
            return result;
        }

        public ClientViewModel GetElement(string email, string password) {
            Client element = _context.Clients.FirstOrDefault(rec => rec.Email == email && rec.Password == password);

            if ( element != null ) {
                return new ClientViewModel {
                    Id = element.Id,
                    Name = element.Name,
                    Email = element.Email,
                    Password = element.Password
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(ClientBindingModel model) {
            Client element = _context.Clients.FirstOrDefault(rec => rec.Email == model.Email);

            if ( element != null ) {
                throw new Exception("Уже есть клиент с такой почтой");
            }

            _context.Clients.Add(new Client {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            });
            _context.SaveChanges();
        }
    }
}