using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiApplicationModule),
        typeof(EntityUiDomainTestModule)
        )]
    public class EntityUiApplicationTestModule : AbpModule
    {

    }
}
