using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbpEntityUiBlazorModule),
        typeof(AbpEntityUiHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class AbpEntityUiBlazorWebAssemblyModule : AbpModule
    {
        
    }
}