using EasyAbp.Abp.EntityUi.Options;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpEntityUiDomainSharedModule)
    )]
    public class AbpEntityUiDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpEntityUiOptions>(options =>
            {
                options.Modules.Add("EasyAbp.Abp.EntityUi", typeof(AbpEntityUiDomainModule));
            });
        }
    }
}
