using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(EntityUiDomainSharedModule)
    )]
    public class EntityUiDomainModule : AbpModule
    {

    }
}
