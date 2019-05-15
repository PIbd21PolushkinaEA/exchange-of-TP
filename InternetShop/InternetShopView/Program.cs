using InternetShopImplementations.Implementations;
using InternetShopServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace InternetShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(container.Resolve<FormAdmin>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainClientServise, MainClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBasketService, BasketServiceDB>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
