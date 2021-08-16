using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.DynamicEntity
{
    [DependsOn(
        typeof(AbpEntityUiDomainModule),
        typeof(AbpEntityUiDynamicEntityDomainSharedModule)
    )]
    public class AbpEntityUiDynamicEntityDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpEntityUiDynamicEntityDomainExtensionMappings.Configure();
        }
    }
}
