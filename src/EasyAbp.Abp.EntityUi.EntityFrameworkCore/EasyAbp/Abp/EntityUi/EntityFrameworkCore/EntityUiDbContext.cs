using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Modules;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public class EntityUiDbContext : AbpDbContext<EntityUiDbContext>, IEntityUiDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Module> Modules { get; set; }

        public EntityUiDbContext(DbContextOptions<EntityUiDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureAbpEntityUi();
        }
    }
}
