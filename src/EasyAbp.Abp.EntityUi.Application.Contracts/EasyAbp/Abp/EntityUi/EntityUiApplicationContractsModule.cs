using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(EntityUiDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EntityUiApplicationContractsModule : AbpModule
    {

    }
}
