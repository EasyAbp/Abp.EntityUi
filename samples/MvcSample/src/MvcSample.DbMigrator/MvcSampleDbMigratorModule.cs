using MvcSample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MvcSample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MvcSampleEntityFrameworkCoreDbMigrationsModule),
        typeof(MvcSampleApplicationContractsModule)
        )]
    public class MvcSampleDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
