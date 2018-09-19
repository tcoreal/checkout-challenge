using Autofac;
using Checkout.BusinessCommon;
using Checkout.DataAccess.InMemory;

namespace Checkout.CustomerWebApi.Ioc.Modules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryOrdersRepository>()
                .As<IOrdersRepository>();
        }
    }
}