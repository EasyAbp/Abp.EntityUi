using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.Abp.EntityUi.Blazor
{
    [DependsOn(
        typeof(AbpEntityUiApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpEntityUiBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpEntityUiBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<EntityUiBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbpEntityUiBlazorModule).Assembly);
            });
        }
    }
}