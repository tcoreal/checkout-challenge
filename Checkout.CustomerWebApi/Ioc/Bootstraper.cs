using Autofac;
using Autofac.Extensions.DependencyInjection;
using Checkout.CustomerWebApi.Ioc.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.CustomerWebApi.Ioc
{
    public class Bootstraper
    {
        public IContainer CreateContainer(IServiceCollection serviceCollection)
        {
            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);
            RegisterModules(builder);
            return builder.Build();
        }

        private void RegisterModules(ContainerBuilder builder) => 
            builder.RegisterModule<InfrastructureModule>();
    }
}