using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MvcSample.EntityFrameworkCore
{
    [DependsOn(
        typeof(MvcSampleEntityFrameworkCoreModule)
        )]
    public class MvcSampleEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MvcSampleMigrationsDbContext>();
        }
    }
}
