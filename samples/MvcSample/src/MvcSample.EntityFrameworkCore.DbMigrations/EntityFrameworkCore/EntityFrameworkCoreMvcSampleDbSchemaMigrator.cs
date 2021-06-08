using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcSample.Data;
using Volo.Abp.DependencyInjection;

namespace MvcSample.EntityFrameworkCore
{
    public class EntityFrameworkCoreMvcSampleDbSchemaMigrator
        : IMvcSampleDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreMvcSampleDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the MvcSampleMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<MvcSampleMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}