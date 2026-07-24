using EasyAbp.Abp.DynamicEntity;
using EasyAbp.Abp.DynamicMenu;
using EasyAbp.Abp.DynamicPermission;
using EasyAbp.Abp.EntityUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(MvcSampleApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpEntityUiApplicationModule),
        typeof(AbpDynamicPermissionApplicationModule),
        typeof(AbpDynamicEntityApplicationModule),
        typeof(AbpDynamicMenuApplicationModule),
        typeof(AbpMapperlyModule)
    )]
    public class MvcSampleApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<MvcSampleApplicationModule>();
        }
    }
}
