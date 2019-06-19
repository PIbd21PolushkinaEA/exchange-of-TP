using System;
using System.Data.Entity;
using System.Windows.Forms;
using InternetShopImplementations;
using InternetShopImplementations.Implementations;
using InternetShopServiceDAL.Interfaces;
using Unity;
using Unity.Lifetime;

namespace InternetShopView {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormAuth>());
        }

        public static IUnityContainer BuildUnityContainer() {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractWebDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRequestService, RequestServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}