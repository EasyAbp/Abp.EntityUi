using EasyAbp.Abp.DynamicEntity;
using EasyAbp.Abp.DynamicEntity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.Abp.EntityUi.DynamicEntity
{
    [DependsOn(
        typeof(AbpEntityUiDomainSharedModule),
        typeof(DynamicEntityDomainSharedModule)
    )]
    public class AbpEntityUiDynamicEntityDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpEntityUiDynamicEntityDomainSharedModule>();
            });
            
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DynamicEntityResource>()
                    .AddVirtualJson("/EasyAbp/Abp/EntityUi/DynamicEntity/Localization");
            });
        }
    }
}
