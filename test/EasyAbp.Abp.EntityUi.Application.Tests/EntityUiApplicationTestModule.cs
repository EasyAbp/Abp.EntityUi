using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(EntityUiApplicationModule),
        typeof(EntityUiDomainTestModule)
        )]
    public class EntityUiApplicationTestModule : AbpModule
    {

    }
}
