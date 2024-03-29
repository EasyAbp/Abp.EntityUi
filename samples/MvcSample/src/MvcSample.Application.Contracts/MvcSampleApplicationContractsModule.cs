﻿using EasyAbp.Abp.DynamicEntity;
using EasyAbp.Abp.DynamicMenu;
using EasyAbp.Abp.DynamicPermission;
using EasyAbp.Abp.EntityUi;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(AbpEntityUiApplicationContractsModule),
        typeof(AbpDynamicPermissionApplicationContractsModule),
        typeof(AbpDynamicEntityApplicationContractsModule),
        typeof(AbpDynamicMenuApplicationContractsModule)
    )]
    public class MvcSampleApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            MvcSampleDtoExtensions.Configure();
        }
    }
}
