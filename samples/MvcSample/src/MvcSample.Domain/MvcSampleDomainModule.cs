﻿using EasyAbp.Abp.DynamicEntity;
using EasyAbp.Abp.DynamicMenu;
using EasyAbp.Abp.DynamicPermission;
using EasyAbp.Abp.EntityUi;
using EasyAbp.Abp.EntityUi.DynamicEntity;
using EasyAbp.Abp.EntityUi.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MvcSample.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule),
        typeof(AbpEntityUiDomainModule),
        typeof(AbpEntityUiDynamicEntityDomainModule),
        typeof(AbpDynamicPermissionDomainModule),
        typeof(AbpDynamicEntityDomainModule),
        typeof(AbpDynamicMenuDomainModule)
    )]
    public class MvcSampleDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
            
            Configure<AbpEntityUiOptions>(options =>
            {
                options.Modules.Add("MvcSample", new AbpEntityUiModuleOptions(typeof(MvcSampleDomainModule)));
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}
