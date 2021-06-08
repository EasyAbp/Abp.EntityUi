using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(EntityUiBlazorModule)
        )]
    public class EntityUiBlazorServerModule : AbpModule
    {
        
    }
}