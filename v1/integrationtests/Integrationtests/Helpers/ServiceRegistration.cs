using Integrationtests.Clients.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestEase;

namespace Integrationtests.Helpers
{
    public abstract class ServiceRegistration
    {
        protected ServiceProvider ServiceProvider { get; }
        public ServiceRegistration() 
        {
            var services = new ServiceCollection();
            services.AddSingleton(RestClient.For<IProxyApiClient>(TestContext.Parameters["proxyApiBaseUrl"]));
            services.AddSingleton(RestClient.For<IUserBackendApiClient>(TestContext.Parameters["userApiBaseUrl"]));

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}