using EasyAbp.Abp.DynamicEntity;
using EasyAbp.Abp.DynamicMenu;
using EasyAbp.Abp.DynamicPermission;
using EasyAbp.Abp.EntityUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(AbpSettingManagementHttpApiClientModule),
        typeof(AbpEntityUiHttpApiClientModule),
        typeof(AbpDynamicPermissionHttpApiClientModule),
        typeof(AbpDynamicEntityHttpApiClientModule),
        typeof(AbpDynamicMenuHttpApiClientModule)
    )]
    public class MvcSampleHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(MvcSampleApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MvcSampleApplicationContractsModule>();
            });
        }
    }
}
