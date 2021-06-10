using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.Modules;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public interface IEntityUiDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Entity> Entities { get; set; }
        DbSet<MenuItem> MenuItems { get; set; }
        DbSet<Module> Modules { get; set; }
    }
}
