using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(EntityUiApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EntityUiHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpAbpEntityUi";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EntityUiApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
