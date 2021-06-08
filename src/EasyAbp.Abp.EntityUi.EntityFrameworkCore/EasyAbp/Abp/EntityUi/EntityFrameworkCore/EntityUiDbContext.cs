using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public class EntityUiDbContext : AbpDbContext<EntityUiDbContext>, IEntityUiDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public EntityUiDbContext(DbContextOptions<EntityUiDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEntityUi();
        }
    }
}