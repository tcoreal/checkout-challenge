using Autofac;
using Checkout.Infrastructure;
using Checkout.Infrastructure.Fake;

namespace Checkout.CustomerWebApi.Ioc.Modules
{
    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FakeUserService>()
                .As<IUserService>()
                .SingleInstance();
        }
    }
}