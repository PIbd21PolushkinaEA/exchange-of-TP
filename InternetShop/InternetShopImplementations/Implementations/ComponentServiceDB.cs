using System;
using System.Collections.Generic;
using System.Linq;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations {
    public class ComponentServiceDB : IComponentService {
        private AbstractWebDbContext context;

        public ComponentServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public void AddElement(ComponentBindingModel model) {
            Component element = context.Components.FirstOrDefault(rec => rec.Name ==
                                                                         model.Name &&
                                                                         rec.Manufacturer == model.Manufacturer &&
                                                                         rec.Brand == model.Brand);
            if ( element != null ) {
                throw new Exception("Уже есть такое комплектующее");
            }

            context.Components.Add(new Component {
                Name = model.Name,
                Brand = model.Brand,
                Manufacturer = model.Manufacturer,
                Rating = model.Rating,
                Price = model.Price
            });
            context.SaveChanges();
        }

        public void DelElement(int id) {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
            if ( element != null ) {
                context.Components.Remove(element);
                context.SaveChanges();
            }
            else {
                throw new Exception("Элемент не найден");
            }
        }

        public ComponentViewModel GetElement(int id) {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
            if ( element != null ) {
                return new ComponentViewModel {
                    Id = element.Id,
                    Name = element.Name,
                    Brand = element.Brand,
                    Manufacturer = element.Manufacturer,
                    Rating = element.Rating,
                    Price = element.Price
                };
            }

            throw new Exception("Элемент не найден");
        }

        public List<ComponentViewModel> GetList() {
            List<ComponentViewModel> result = context.Components.Select(rec => new
                    ComponentViewModel {
                        Id = rec.Id,
                        Name = rec.Name,
                        Brand = rec.Brand,
                        Manufacturer = rec.Manufacturer,
                        Rating = rec.Rating,
                        Price = rec.Price
                    })
                .ToList();
            return result;
        }

        public void UpdElement(ComponentBindingModel model) {
            Component element = context.Components.FirstOrDefault(rec => rec.Name ==
                                                                         model.Name && rec.Id != model.Id);
            if ( element != null ) {
                throw new Exception("Уже есть такое комплектующее");
            }

            element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if ( element == null ) {
                throw new Exception("Элемент не найден");
            }

            element.Name = model.Name;
            element.Brand = model.Brand;
            element.Manufacturer = model.Manufacturer;
            element.Rating = model.Rating;
            element.Price = model.Price;
            context.SaveChanges();
        }
    }
}