using EasyAbp.Abp.EntityUi.Options;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

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
                options.Modules.Add("EasyAbp.Abp.EntityUi",
                    new AbpEntityUiModuleOptions(typeof(AbpEntityUiDomainModule),
                        "/EasyAbp/Abp/EntityUi/EntityUiSeed.json"));
            });
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpEntityUiDomainModule>();
            });
        }
    }
}
