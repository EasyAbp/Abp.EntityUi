using EasyAbp.Abp.EntityUi.DynamicEntity;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.Web
{
    [DependsOn(
        typeof(AbpEntityUiWebModule),
        typeof(AbpEntityUiDynamicEntityDomainSharedModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpMapperlyModule)
    )]
    public class AbpEntityUiDynamicEntityWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(EntityUiResource),
                    typeof(AbpEntityUiDynamicEntityWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpEntityUiDynamicEntityWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<AbpEntityUiDynamicEntityWebModule>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            NatashaManagement
                .GetInitializer()
                .WithMemoryUsing()
                .WithMemoryReference()
                .Preheating<NatashaDomainCreator>();
        }
    }
}