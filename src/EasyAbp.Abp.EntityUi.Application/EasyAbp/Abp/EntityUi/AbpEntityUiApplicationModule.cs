using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiDomainModule),
        typeof(AbpEntityUiApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule)
    )]
    public class AbpEntityUiApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<AbpEntityUiApplicationModule>();
        }
    }
}
