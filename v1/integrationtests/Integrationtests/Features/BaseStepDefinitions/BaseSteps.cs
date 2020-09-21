using Integrationtests.Clients.Interfaces;
using Integrationtests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Integrationtests.Features.BaseStepDefinitions
{
    public class BaseSteps : ServiceRegistration
    {
        protected readonly IProxyApiClient _proxyApiClient;
        protected readonly IUserBackendApiClient _userBackendApiClient;

        public BaseSteps() 
        {
            _proxyApiClient = ServiceProvider.GetService<IProxyApiClient>();
            _userBackendApiClient = ServiceProvider.GetService<IUserBackendApiClient>();
        }
    }
}