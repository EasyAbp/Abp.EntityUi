using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AbpEntityUiApplicationContractsModule : AbpModule
    {

    }
}
