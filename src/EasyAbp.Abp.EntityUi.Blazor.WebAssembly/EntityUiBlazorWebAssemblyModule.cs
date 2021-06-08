using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.Blazor.WebAssembly
{
    [DependsOn(
        typeof(EntityUiBlazorModule),
        typeof(EntityUiHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class EntityUiBlazorWebAssemblyModule : AbpModule
    {
        
    }
}