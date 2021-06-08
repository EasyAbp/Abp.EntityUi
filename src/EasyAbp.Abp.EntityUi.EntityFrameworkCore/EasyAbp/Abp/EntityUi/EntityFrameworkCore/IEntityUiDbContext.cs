using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public interface IEntityUiDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}