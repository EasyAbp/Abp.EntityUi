﻿using Localization.Resources.AbpUi;
using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(EntityUiApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EntityUiHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EntityUiHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EntityUiResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
