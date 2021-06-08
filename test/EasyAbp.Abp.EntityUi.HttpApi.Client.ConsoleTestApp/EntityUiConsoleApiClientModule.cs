using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(EntityUiHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EntityUiConsoleApiClientModule : AbpModule
    {
        
    }
}
