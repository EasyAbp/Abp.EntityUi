using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class AbpEntityUiHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpAbpEntityUi";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpEntityUiApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
