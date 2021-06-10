using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EntityUiConsoleApiClientModule : AbpModule
    {
        
    }
}
