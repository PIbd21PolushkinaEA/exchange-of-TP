using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations
{
    public class ComponentServiceDB : IComponentService
    {
        private AbstractDbContext context;

        public ComponentServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ComponentViewModel> GetList()
        {
            List<ComponentViewModel> result = context.Components
                .Select(rec => new ComponentViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Brand = rec.Brand,
                Manufacturer = rec.Manufacturer,
                Price = rec.Price,
                Rating = rec.Rating,
                CountOfAvailable = rec.CountOfAvailable
            })
            .ToList();
            return result;
        }
        public ComponentViewModel GetElement(int id)
        {
            Component component = context.Components.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                return new ComponentViewModel
                {
                    Id = component.Id,
                    Name = component.Name,
                    Brand = component.Brand,
                    Manufacturer = component.Manufacturer,
                    Price = component.Price,
                    Rating = component.Rating,
                    CountOfAvailable = component.CountOfAvailable
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ComponentBindingModel model)
        {
            Component component = context.Components.FirstOrDefault(rec => rec.Name ==
            model.Name);
            if (component != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Components.Add(new Component
            {
                Name = model.Name,
                Brand = model.Brand,
                Manufacturer = model.Manufacturer,
                Price = model.Price,
                Rating = model.Rating,
                CountOfAvailable = model.CountOfAvailable
            });
            context.SaveChanges();
        }
        public void UpdElement(ComponentBindingModel model)
        {
            Component component = context.Components.FirstOrDefault(rec => rec.Name ==
            model.Name && rec.Id != model.Id);
            if (component != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            component = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            component.Name = model.Name;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Component component = context.Components.FirstOrDefault(rec => rec.Id == id);
            if (component != null)
            {
                context.Components.Remove(component);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
