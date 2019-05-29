using InternetShopServiceDAL.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopServiceDAL.Interfaces {
    public interface IMainAdmin {
        List<ComponentBindingModel> GetComponents();
        ComponentBindingModel GetComponent(int id);
        List<ProductBindingModel> GetProducts();
        ProductBindingModel GetProduct(int id);
        void CreateProduct(List<ComponentBindingModel> Components);
        void UpdateProduct(List<ComponentBindingModel> Components);
        void DeleteProduct(List<ComponentBindingModel> Components);
        void CreateComponent(ComponentBindingModel component);
        void UpdateComponent(ComponentBindingModel component);
        void DeleteComponent(ComponentBindingModel component);
    }
}
