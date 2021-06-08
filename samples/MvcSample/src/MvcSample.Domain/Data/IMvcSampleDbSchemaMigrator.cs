using System.Threading.Tasks;

namespace MvcSample.Data
{
    public interface IMvcSampleDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
