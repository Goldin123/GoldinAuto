using GoldinAutoTrade.Interface;
using GoldinAutoTrade.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace GoldinAutoTrade
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IOrderReposotiry, OrderRepository>();
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<IShoppingCartRepository, ShoppingCartRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}