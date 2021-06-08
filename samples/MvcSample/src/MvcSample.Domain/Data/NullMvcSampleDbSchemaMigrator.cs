using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MvcSample.Data
{
    /* This is used if database provider does't define
     * IMvcSampleDbSchemaMigrator implementation.
     */
    public class NullMvcSampleDbSchemaMigrator : IMvcSampleDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}