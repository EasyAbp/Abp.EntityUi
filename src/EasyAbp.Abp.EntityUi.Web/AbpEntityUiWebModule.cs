using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.Abp.EntityUi.Localization;
using EasyAbp.Abp.EntityUi.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.Abp.EntityUi.Permissions;

namespace EasyAbp.Abp.EntityUi.Web
{
    [DependsOn(
        typeof(AbpEntityUiHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpEntityUiWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(EntityUiResource),
                    typeof(AbpEntityUiApplicationContractsModule).Assembly,
                    typeof(AbpEntityUiDomainSharedModule).Assembly,
                    typeof(AbpEntityUiWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpEntityUiWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EntityUiMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpEntityUiWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<AbpEntityUiWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpEntityUiWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("/EntityUi/Index", "/EntityUi/{moduleName}/{entityName}");
                options.Conventions.AddPageRoute("/EntityUi/CreateModal", "/EntityUi/{moduleName}/{entityName}/CreateModal");
                options.Conventions.AddPageRoute("/EntityUi/CreateSubEntityModal", "/EntityUi/{moduleName}/{entityName}/CreateSubEntityModal");
                options.Conventions.AddPageRoute("/EntityUi/EditModal", "/EntityUi/{moduleName}/{entityName}/EditModal");
                options.Conventions.AddPageRoute("/EntityUi/EditSubEntityModal", "/EntityUi/{moduleName}/{entityName}/EditSubEntityModal");
                
                //Configure authorization.
            });
        }
    }
}
