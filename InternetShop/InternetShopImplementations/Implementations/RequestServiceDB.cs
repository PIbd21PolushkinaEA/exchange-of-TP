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
    public class RequestServiceDB : IRequestService
    {
        private AbstractDbContext context;
        
        public RequestServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public void AddElement(RequestBindingModel model)
        {
            Request request = context.Requests.FirstOrDefault(rec => rec.Date ==
            model.Date);
            if (request != null)
            {
                throw new Exception("Уже есть заявка с такой датой");
            }
            context.Requests.Add(new Request
            {
                Date = model.Date
                
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if (request != null)
            {
                context.Requests.Remove(request);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public RequestViewModel GetElement(int id)
        {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if (request != null)
            {
                return new RequestViewModel
                {
                    Id = request.Id,
                    Date = request.Date
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<RequestViewModel> GetList()
        {
            List<RequestViewModel> result = context.Requests
                .Select(rec => new RequestViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date
                })
            .ToList();
            return result;
        }

        public void UpdElement(RequestBindingModel model)
        {
            Request request = context.Requests.FirstOrDefault(rec => rec.Date != model.Date);
            if (request != null)
            {
                throw new Exception("Уже есть заявка с такой датой");
            }
            request = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if (request == null)
            {
                throw new Exception("Элемент не найден");
            }
            request.Date = model.Date;
            context.SaveChanges();
        }
    }
}
